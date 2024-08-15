using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Sales.Shippings
{
    public class ShippingUnitWrapper : WrapperBase<SalesUnit>
    {
        public ShippingUnitWrapper(SalesUnit model) : base(model) { }

        public DateTime? Date
        {
            get => Model.ShipmentDate?.Date ?? Model.ShipmentPlanDate;
            set
            {
                if (Model.ShipmentDate.HasValue) return;
                ShipmentPlanDate = value;
                RaisePropertyChanged();
            }
        }

        public DateTime? ShipmentPlanDate
        {
            get => GetValue<DateTime?>();
            set => SetValue(value);
        }
        public DateTime? ShipmentPlanDateOriginalValue => GetOriginalValue<DateTime?>(nameof(ShipmentPlanDate));
        public bool ShipmentPlanDateIsChanged => GetIsChanged(nameof(ShipmentPlanDate));
    }
}