using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Parameter : BaseEntity
    {
        public virtual ParameterGroup ParameterGroup { get; set; }
        public string Value { get; set; }
        public virtual List<ParameterRelation> ParameterRelations { get; set; } = new List<ParameterRelation>();


        public bool IsActual(IEnumerable<Parameter> parameters)
        {
            if (!ParameterRelations.Any()) return true;

            return ParameterRelations.Any(x => x.RequiredParameters.AllContainsIn(parameters));
        }
    }
}