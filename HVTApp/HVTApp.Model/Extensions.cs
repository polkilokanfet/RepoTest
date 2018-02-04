using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model
{
    public static class Extensions
    {
        public static Parameter AddRequiredPreviousParameters(this Parameter parameter, IEnumerable<Parameter> requiredPreviousParameters)
        {
            parameter.ParameterRelations.Add(new ParameterRelation
            {
                ParameterId = parameter.Id,
                RequiredParameters = new List<Parameter>(requiredPreviousParameters)
            });
            return parameter;
        }
    }
}