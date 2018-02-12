using System;
using System.Collections.Generic;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public class ProjectUnitsGrouped : UnitsGrouped<ProjectUnitWrapper>
    {
        public ProjectUnitsGrouped(IEnumerable<ProjectUnitWrapper> unitWrappers) : base(unitWrappers)
        {
        }

        public virtual ProjectWrapper Project
        {
            get { return GetValue<ProjectWrapper>(); }
            set { SetValue(value); }
        }

        public virtual CompanyWrapper Producer
        {
            get { return GetValue<CompanyWrapper>(); }
            set { SetValue(value); }
        }

        public virtual DateTime DeliveryDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
    }
}