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
        public static IEnumerable<Parameter> RemoveUseLess(this IEnumerable<Parameter> targetParameters, IEnumerable<Parameter> requiredParameters)
        {
            var result = targetParameters.ToList();
            foreach (var requiredParameter in requiredParameters)
            {
                var toExcept = result.Where(x => Equals(x.ParameterGroupId, requiredParameter.ParameterGroup.Id))
                    .Except(new List<Parameter> {requiredParameter});
                result = result.Except(toExcept).ToList();
            }
            return result;
        }
    }
}