using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class ProjectUnitGroup : BaseEntity
    {
        private ProjectUnit ProjectUnit => ProjectUnits.FirstOrDefault();

        public virtual Product Product => ProjectUnit?.Product;
        public virtual Facility Facility => ProjectUnit?.Facility;
        public virtual DateTime? DeliveryDate => ProjectUnit?.DeliveryDate;
        public double Cost => ProjectUnit.Cost;
        public int Amount => ProjectUnits.Count;

        public virtual List<ProjectUnit> ProjectUnits { get; set; } = new List<ProjectUnit>();
    }
}