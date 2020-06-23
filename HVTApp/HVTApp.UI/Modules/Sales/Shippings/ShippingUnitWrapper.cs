using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Sales.Shippings
{
    public class ShippingUnitWrapper : WrapperBase<SalesUnit>
    {
        public ShippingUnitWrapper(SalesUnit model) : base(model) { }

        public DateTime? ShipmentPlanDate
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? ShipmentPlanDateOriginalValue => GetOriginalValue<DateTime?>(nameof(ShipmentPlanDate));
        public bool ShipmentPlanDateIsChanged => GetIsChanged(nameof(ShipmentPlanDate));
    }
}