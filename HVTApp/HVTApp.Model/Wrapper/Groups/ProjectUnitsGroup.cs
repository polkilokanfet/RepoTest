using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrapper.Groups
{
    public class ProjectUnitsGroup : 
        BaseWrappersGroup<ProjectUnitsGroup, SalesUnit, ProjectUnit>, 
        IGroupValidatableChangeTrackingWithCollection<ProjectUnitsGroup, SalesUnit>
    {
        private readonly List<SalesUnit> _units;

        public bool CanRemove => _units.All(x => x.Order == null);
        public bool CanTotalRemove => _units.All(x => x.AllowTotalRemove);

        public CompanyWrapper Producer
        {
            get { return GetValue<CompanyWrapper>(); }
            set { SetValue(value); }
        }

        public SpecificationWrapper Specification
        {
            get { return GetValue<SpecificationWrapper>(); }
            set { SetValue(value); }
        }

        public DateTime OrderInTakeDate => GetValue<DateTime>();

        public OrderWrapper Order
        {
            get { return GetValue<OrderWrapper>(); }
            set { SetValue(value); }
        }

        public string TceRequest
        {
            get { return GetValue<string>(); }
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
            _units = units;
        }

        protected override SalesUnit GetSalesUnit()
        {
            return Model;
        }
    }
}