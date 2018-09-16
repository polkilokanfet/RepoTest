using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
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
            //result = KeepUsefull(result, relation);

            //пересечение полей обязательных параметров
            var cross = RequiredParametersField(relation.ChildProductParameters.First());
            foreach (var childProductParameter in relation.ChildProductParameters)
            {
                if (childProductParameter.Equals(relation.ChildProductParameters.First())) continue;
                cross = cross.Intersect(RequiredParametersField(childProductParameter)).ToList();
            }

            //получение уникальных значений из полей
            var list = new List<List<Parameter>>();
            foreach (var childProductParameter in relation.ChildProductParameters)
            {
                list.Add(RequiredParametersField(childProductParameter));
            }
            var uniq = UniqElements(list).ToList();
            uniq = uniq.Where(x => !cross.Select(c => c.ParameterGroup.Id).Contains(x.ParameterGroup.Id)).ToList();

            cross = cross.Concat(relation.ChildProductParameters).Concat(uniq).Distinct().ToList();
            var result = cross.ToList();

            foreach (var parameter in parameters)
            {
                if (cross.AllContainsIn(RequiredParametersField(parameter)))
                {
                    result.Add(parameter);
                }
            }

            result = result.Distinct().ToList();
            return result;

            ////берем обязательные дочерние параметры связи
            //var requiredParameters = relation.ChildProductParameters;

            ////оставляем эти параметры и те, что должны быть выбраны до
            //var parameters = requiredParameters.SelectMany(RequiredParametersField).Concat(requiredParameters).Distinct().ToList();
            //foreach (var parameter in parameters)
            //{
            //    var toExcept = parameters.Where(x => Equals(x.ParameterGroup.Id, parameter.ParameterGroup.Id));
            //    parameters = parameters.Except(toExcept).ToList();
            //    parameters.Add(parameter);
            //}
            //return parameters;
        }

        static IEnumerable<T> UniqElements<T>(IEnumerable<IEnumerable<T>> enumerable)
        {
            var list = new List<T>();
            foreach (var en in enumerable)
            {
                list = list.Concat(en).ToList();
            }

            foreach (var entity in list)
            {
                if (list.Count(x => x.Equals(entity)) == 1)
                    yield return entity;
            }
        }

        static List<Parameter> KeepUsefull(IEnumerable<Parameter> parameters, ProductRelation relation)
        {
            var reqParams = relation.ChildProductParameters;
            return parameters.Where(par => reqParams.Contains(par) || //сами обязательные параметры
                                           reqParams.AllContainsIn(RequiredParametersField(par)) ||
                                           reqParams.Any(req => RequiredParametersField(req).Contains(par))).ToList();
        }

        //static IEnumerable<Parameter> possibleParametersField(Parameter parameter)
        //{
            
        //}

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