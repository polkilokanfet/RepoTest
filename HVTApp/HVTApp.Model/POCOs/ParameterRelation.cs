using System;
using System.Collections.Generic;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ќб€зательные родительские параметры, без которых этот параметр не имеет смысла
    /// </summary>
    [Designation("ќграничение использовани€ параметра")]
    public partial class ParameterRelation : BaseEntity
    {
        [Designation("ќб€зательные параметры перед")]
        public virtual List<Parameter> RequiredParameters { get; set; } = new List<Parameter>();

        public Guid ParameterId { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("ќб€з€тельные параметры: ");
            RequiredParameters.ForEach(x => sb.Append($"{x.ToString().ToLower()}; "));
            return sb.ToString();
        }
    }
}