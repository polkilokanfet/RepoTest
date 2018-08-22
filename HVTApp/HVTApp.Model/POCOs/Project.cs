using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Project : BaseEntity
    {
        public string Name { get; set; }
        public virtual User Manager { get; set; }
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    public class ProjectUnit : BaseEntity
    {
        public Product Product { get; set; }
        public double Cost { get; set; }
    }
}