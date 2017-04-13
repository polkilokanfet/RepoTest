using System;
using System.ComponentModel;
using HVTApp.Model.Services;

namespace HVTApp.Model.Wrapper
{
    public partial class ShipmentUnitWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += SipmentDateCalculatedPropertyChanged;
        }

        private void SipmentDateCalculatedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ShipmentPlanDate))
                OnPropertyChanged(this, nameof(ShipmentDateCalculated));
        }

        private DateTime? _shipmentPlanDate;

        public DateTime? ShipmentPlanDate
        {
            get { return _shipmentPlanDate; }
            set
            {
                if (Equals(_shipmentPlanDate, value)) return;
                _shipmentPlanDate = value;
                OnPropertyChanged(this, nameof(ShipmentPlanDate));
            }
        }

        public DateTime ShipmentDateCalculated
        {
            get
            {
                //по реальной дате отгрузки
                if (ShipmentDate.HasValue) return ShipmentDate.Value; 
                //по плановой дате отгрузки
                if (ShipmentPlanDate.HasValue) return ShipmentPlanDate.Value; 
                //по дате исполнения условий для отгрузки
                if (SalesUnit.ShippingConditionsDoneDate != null && 
                    SalesUnit.ShippingConditionsDoneDate >= SalesUnit.ProductionUnit.EndProductionDateCalculated)
                    return SalesUnit.ShippingConditionsDoneDate.Value.GetTodayIfDateFromPastAndSkipWeekend();
                //по дате окончания производства
                return SalesUnit.ProductionUnit.EndProductionDateCalculated.GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        public DateTime DeliveryDateCalculated
        {
            get
            {
                if (DeliveryDate.HasValue) return DeliveryDate.Value;
                return ShipmentDateCalculated.AddDays(ExpectedDeliveryPeriod).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }
    }
}
