using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model
{
    public static class Extensions
    {
        public static ParameterGroup AddParameters(this ParameterGroup group, IEnumerable<Parameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                group.Parameters.Add(parameter);
                parameter.Group = group;
            }
            return group;
        }

        public static Parameter AddRequiredPreviousParameters(this Parameter parameter, IEnumerable<Parameter> requiredPreviousParameters)
        {
            parameter.RequiredPreviousParameters.Add(new RequiredPreviousParameters
            {
                Parameter = parameter,
                RequiredParameters = new List<Parameter>(requiredPreviousParameters)
            });
            return parameter;
        }
    }
}