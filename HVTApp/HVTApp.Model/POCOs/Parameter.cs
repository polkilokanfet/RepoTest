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
    }

    public partial class Parameter : IComparable
    {
        public bool IsOrigin => !ParameterRelations.Any();

        public override string ToString()
        {
            return $"{ParameterGroup.Name}: {Value}";
        }

        public int CompareTo(object obj)
        {
            var first = this;
            var second = obj as Parameter;

            if (!first.ParameterRelations.Any() && !second.ParameterRelations.Any())
                return 0;

            if (!first.ParameterRelations.Any())
                return -1;

            if (!second.ParameterRelations.Any())
                return 1;

            if (first.ParameterRelations.Any(x => x.RequiredParameters.Select(rp => rp.Id).Contains(second.Id)))
                return -1;
            if (second.ParameterRelations.Any(x => x.RequiredParameters.Select(rp => rp.Id).Contains(first.Id)))
                return 1;
            return 0;
        }
    }
}