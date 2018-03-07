using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public static class Ext
    {
        /// <summary>
        /// Удаление из списка параметров, которые в одной группе с обязательными параметрами.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Parameter> GetUsefull(this IEnumerable<Parameter> targetParameters, IEnumerable<Parameter> requiredParameters)
        {
            var result = targetParameters.ToList();
            var parameters = requiredParameters.SelectMany(GetAllRequiredParameters).Concat(requiredParameters).Distinct().ToList();
            foreach (var parameter in parameters)
            {
                var toExcept = result.Where(x => Equals(x.ParameterGroup.Id, parameter.ParameterGroup.Id))
                                     .Except(new List<Parameter> {parameter});
                result = result.Except(toExcept).ToList();
            }
            return result;
        }

        static IEnumerable<Parameter> GetAllRequiredParameters(Parameter parameter)
        {
            foreach (var requiredParameter in parameter.ParameterRelations.SelectMany(x => x.RequiredParameters).Distinct())
            {
                yield return requiredParameter;
                foreach (var requiredParameter2 in GetAllRequiredParameters(requiredParameter))
                {
                    yield return requiredParameter2;
                }
            }

        }
    }
}