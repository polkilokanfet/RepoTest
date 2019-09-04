using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Groups
{
    public class SalesUnitsWrappersGroup : 
        BaseWrappersGroup<SalesUnitsWrappersGroup, SalesUnit, SalesUnitWrapper>, 
        IGroupValidatableChangeTrackingWithCollection<SalesUnitsWrappersGroup, SalesUnit>
    {
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
                if(value < DateTime.Today) return;
                SetValue(value);
            }
        }

        public SalesUnitsWrappersGroup(List<SalesUnit> units) : base(units)
        {
        }
    }
}