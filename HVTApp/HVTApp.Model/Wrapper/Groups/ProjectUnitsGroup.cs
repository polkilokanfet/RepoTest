using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.Model.Wrapper.Groups
{
    public class ProjectUnitsGroup : 
        BaseWrappersGroup<ProjectUnitsGroup, SalesUnit, ProjectUnit>, 
        IGroupValidatableChangeTrackingWithCollection<ProjectUnitsGroup, SalesUnit>
    {
        public List<SalesUnit> SalesUnits { get; }

        public bool CanRemove => SalesUnits.All(x => x.Order == null);
        public bool CanTotalRemove => SalesUnits.All(x => x.AllowTotalRemove);

        public ProjectSimpleWrapper Project
        {
            get { return GetValue<ProjectSimpleWrapper>(); }
            set { SetValue(value); }
        }

        public CompanySimpleWrapper Producer
        {
            get { return GetValue<CompanySimpleWrapper>(); }
            set { SetValue(value); }
        }

        public SpecificationSimpleWrapper Specification
        {
            get { return GetValue<SpecificationSimpleWrapper>(); }
            set { SetValue(value); }
        }

        public DateTime OrderInTakeDate => GetValue<DateTime>();

        public string TceRequest
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool IsRemoved
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public DateTime SignalToStartProduction
        {
            set { SetValue(value); }
        }

        public DateTime EndProductionDateCalculated => GetValue<DateTime>();

        public DateTime DeliveryDateExpected
        {
            get { return GetValue<DateTime>(); }
            set
            {
                //if(value < DateTime.Today) return;
                SetValue(value);
            }
        }

        public ProductType ProductType => Product.Model.ProductType;

        public ProjectUnitsGroup(List<SalesUnit> units) : base(units)
        {
            SalesUnits = units;
        }

        protected override SalesUnit GetSalesUnit()
        {
            return Model;
        }
    }
}