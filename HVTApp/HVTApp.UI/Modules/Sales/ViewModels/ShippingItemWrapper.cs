using System;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ShippingItemWrapper : WrapperBase<SalesUnit>
    {
        public ShippingItemWrapper(SalesUnit model) : base(model) { }

        public DateTime? ShipmentPlanDate
        {
            get { return GetValue<DateTime?>(); }
            set { SetValue(value); }
        }
        public DateTime? ShipmentPlanDateOriginalValue => GetOriginalValue<DateTime?>(nameof(ShipmentPlanDate));
        public bool ShipmentPlanDateIsChanged => GetIsChanged(nameof(ShipmentPlanDate));
    }
}