using System;
using System.Runtime.CompilerServices;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates
{
    public class SalesUnitDates : WrapperBase<SalesUnit>
    {
        public string SerialNumber
        {
            get => GetValue<string>();
            set => SetValueNew(value);
        }

        public DateTime? PickingDate
        {
            get => GetValue<DateTime?>();
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
            get => GetValue<DateTime?>();
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
            get => GetValue<DateTime?>();
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
            get => GetValue<DateTime?>();
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
            get => GetValue<DateTime?>();
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

        /// <summary>
        /// Заказ укомплектован?
        /// </summary>
        public bool IsCompleted => PickingDate.HasValue && PickingDate < DateTime.Today;

        public SalesUnitDates(SalesUnit model) : base(model)
        {
        }

        /// <summary>
        /// Простановка даты реализации и даты доставки по дате отгрузки.
        /// </summary>
        public void SetCalculatedDates()
        {
            if (ShipmentDate == null)
                return;

            if (RealizationDate == null)
            {
                RealizationDate = Model.ShipmentDate;
                SettedCalculatedRealizationDate?.Invoke(RealizationDate.Value);
            }

            if (DeliveryDate == null)
            {
                DeliveryDate = Model.DeliveryDateCalculated;
                SettedCalculatedDeliveryDate?.Invoke(DeliveryDate.Value);
            }
        }

        public event Action SettedValueToProperty;
        public event Action<DateTime> SettedCalculatedRealizationDate;
        public event Action<DateTime> SettedCalculatedDeliveryDate;

        private void SetValueNew<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        {
            this.SetValue(newValue, propertyName);
            OnPropertyChanged(nameof(HasFullInformation));
            OnPropertyChanged(nameof(IsCompleted));
            SettedValueToProperty?.Invoke();
        }
    }
}