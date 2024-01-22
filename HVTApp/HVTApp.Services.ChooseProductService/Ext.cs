using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public static class Ext
    {
        /// <summary>
        /// Удаление из списка параметров, которые в одной группе с обязательными параметрами.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Parameter> GetUsefull(this IEnumerable<Parameter> targetParameters, ProductRelation relation)
        {
            var parameters = targetParameters as List<Parameter> ?? targetParameters.ToList();

            //обязательные параметры
            var goodParams = relation.ChildProductParameters.ToList();
            var groups = goodParams.Select(x => x.ParameterGroup).Distinct();
            //парметры, способные увести в сторону (плохие)
            var badParams = parameters.Where(x => groups.Contains(x.ParameterGroup)).Except(goodParams).ToList();
            //обязательное пересечение путей
            var ints = Intercect(goodParams).ToList();

            //все пути всех параметров
            var paths = parameters.SelectMany(x => x.Paths()).ToList();

            //исключаем пути с плохими параметрами
            paths = paths.Where(x => !x.Parameters.Intersect(badParams).Any()).ToList();
            //исключаем пути без хороших параметров
            paths = paths.Where(x => x.Parameters.Intersect(goodParams).Any()).ToList();
            //исключаем пути без пересечений
            paths = paths.Where(x => ints.AllContainsIn(x.Parameters)).ToList();

            var result = paths.SelectMany(x => x.Parameters).Distinct().ToList();
            return result;
        }

        /// <summary>
        /// Обязательное пересечение в путях параметров.
        /// Иначе нельзя будет достичь всех параметров.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static IEnumerable<Parameter> Intercect(IEnumerable<Parameter> parameters)
        {
            var pmtrs = parameters as Parameter[] ?? parameters.ToArray();

            var result = pmtrs.First().Paths().SelectMany(x => x.Parameters).Distinct().ToList();
            foreach (var parameter in pmtrs)
            {
                var toIntersect = parameter.Paths().SelectMany(x => x.Parameters).Distinct().ToList();
                result = result.Intersect(toIntersect).ToList();
            }

            return result;
        }

        /// <summary>
        /// Поле обязательных параметров.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        static List<Parameter> RequiredParametersField(Parameter parameter)
        {
            var result = new List<Parameter> {parameter};

            foreach (var requiredParameter in parameter.ParameterRelations.SelectMany(x => x.RequiredParameters).Distinct())
            {
                result.Add(requiredParameter);
                result.AddRange(RequiredParametersField(requiredParameter));
            }

            return result.Distinct().ToList();
        }
    }
}