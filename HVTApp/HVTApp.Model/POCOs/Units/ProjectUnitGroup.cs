using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class ProjectUnitGroup : BaseEntity
    {
        public ProjectUnitGroup(List<ProjectUnit> projectUnits)
        {
            ProjectUnits = projectUnits;
        }

        public virtual Project Project
        {
            get { return GetValue<Project>(); }
            set { SetValue(value); }
        }

        public virtual Product Product
        {
            get { return GetValue<Product>(); }
            set { SetValue(value); }
        }

        public virtual Facility Facility
        {
            get { return GetValue<Facility>(); }
            set { SetValue(value); }
        }

        public virtual Company Producer
        {
            get { return GetValue<Company>(); }
            set { SetValue(value); }
        }

        public virtual DateTime DeliveryDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public double Cost
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public int Amount => ProjectUnits.Count;

        public virtual List<ProjectUnit> ProjectUnits { get; set; } = new List<ProjectUnit>();

        private T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            var unit = ProjectUnits.First();
            return (T)unit.GetType().GetProperty(propertyName).GetValue(unit);
        }

        private void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            var unit = ProjectUnits.First();
            var propertyInfo = unit.GetType().GetProperty(propertyName);
            foreach (var offerUnit in ProjectUnits)
                propertyInfo.SetValue(offerUnit, value);
        }

    }
}