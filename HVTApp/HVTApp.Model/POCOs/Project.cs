using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Project : BaseEntity
    {
        public string Name { get; set; }
        public virtual User Manager { get; set; }
        public virtual List<SalesUnit> SalesUnits { get; set; }

        public override string ToString()
        {
            return $"Project: {Name}";
        }
    }
}