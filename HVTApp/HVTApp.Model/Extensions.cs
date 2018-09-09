using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Model
{
    public static class Extensions
    {
        public static Parameter AddRequiredPreviousParameters(this Parameter parameter, params Parameter[] requiredPreviousParameters)
        {
            parameter.ParameterRelations.Add(new ParameterRelation
            {
                ParameterId = parameter.Id,
                RequiredParameters = new List<Parameter>(requiredPreviousParameters)
            });
            return parameter;
        }

        public static double GetWeight(this ProductBlock block, Parameter parameter)
        {
            if (parameter.IsOrigin) return 1;
            var relations = parameter.ParameterRelations.Where(x => x.RequiredParameters.AllContainsIn(block.Parameters)).ToList();
            if (!relations.Any()) throw new ArgumentException("Передан параметр, который не должен быть в блоке.");
            var relation = relations.OrderBy(x => x.RequiredParameters.Count).Last();
            return 1 / relation.RequiredParameters.Count;
        }

    }
}