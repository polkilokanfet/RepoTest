using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Parameter : BaseEntity
    {
        public virtual Guid GroupId { get; set; }
        public string Value { get; set; }
        public virtual List<ParameterRelation> RequiredPreviousParameters { get; set; } = new List<ParameterRelation>();


        public bool IsActual(IEnumerable<Parameter> parameters)
        {
            if (!RequiredPreviousParameters.Any()) return true;

            return RequiredPreviousParameters.Any(x => x.RequiredParameters.AllContainsIn(parameters));
        }
    }
}