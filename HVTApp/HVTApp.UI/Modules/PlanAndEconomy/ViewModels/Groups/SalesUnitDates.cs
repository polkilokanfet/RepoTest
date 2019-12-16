using System;
using System.Runtime.CompilerServices;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class SalesUnitDates : WrapperBase<SalesUnit>
    {
        public string SerialNumber
        {
            get { return GetValue<string>(); }
            set { SetValueNew(value); }
        }

        public DateTime? PickingDate
        {
            get { return GetValue<DateTime?>(); }
            set
            {
                var date = EndProductionDate ?? ShipmentDate ?? DeliveryDate ?? RealizationDate;
                if (date.HasValue && value.HasValue && date < value)
                    return;
                SetValueNew(value);
            }
        }

        public DateTime? EndProductionDate
        {
            get { return GetValue<DateTime?>(); }
            set
            {
                if (value.HasValue)
                {
                    if (PickingDate.HasValue && PickingDate > value)
                        return;

                    var date = ShipmentDate ?? DeliveryDate ?? RealizationDate;
                    if (date.HasValue && date < value)
                        return;
                }
                SetValueNew(value);
            }
        }

        public DateTime? ShipmentDate
        {
            get { return GetValue<DateTime?>(); }
            set
            {
                if (value.HasValue)
                {
                    if (DeliveryDate.HasValue && DeliveryDate < value)
                        return;

                    var date = EndProductionDate ?? PickingDate;
                    if (date.HasValue && date > value)
                        return;
                }

                SetValueNew(value);
            }
        }

        public DateTime? DeliveryDate
        {
            get { return GetValue<DateTime?>(); }
            set
            {
                var date = ShipmentDate ?? EndProductionDate ?? PickingDate;
                if (date.HasValue && value.HasValue && date > value)
                    return;
                SetValueNew(value);
            }
        }

        public DateTime? RealizationDate
        {
            get { return GetValue<DateTime?>(); }
            set
            {
                if (value.HasValue)
                {
                    var date = EndProductionDate ?? PickingDate;
                    if(date.HasValue && date > value)
                        return;
                }
                SetValueNew(value);
            }
        }

        public bool HasFullInformation => !string.IsNullOrEmpty(SerialNumber) &&
                                          PickingDate.HasValue && 
                                          EndProductionDate.HasValue && 
                                          ShipmentDate.HasValue &&
                                          DeliveryDate.HasValue && RealizationDate.HasValue;

        public SalesUnitDates(SalesUnit model) : base(model)
        {
        }

        public event Action SettedValueToProperty;

        private void SetValueNew<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        {
            this.SetValue(newValue, propertyName);
            OnPropertyChanged(nameof(HasFullInformation));
            SettedValueToProperty?.Invoke();
        }
    }
}