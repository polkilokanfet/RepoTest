using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class ParameterGroup : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
        public virtual Measure Measure { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}