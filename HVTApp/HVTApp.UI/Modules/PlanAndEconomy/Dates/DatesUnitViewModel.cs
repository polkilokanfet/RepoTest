using System;
using System.Runtime.CompilerServices;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates
{
    public class DatesUnitViewModel : WrapperBase<SalesUnit>
    {
        public string SerialNumber
        {
            get => Model.SerialNumber;
            set
            {
                if (SerialNumber == value) return;
                SetValueNew(value);

                var orderPosition = this.Model.GetOrderPosition();

                if (int.TryParse(value, out var serialNumber) &&
                    orderPosition.HasValue)
                {
                    SerialNumberSetIntEvent?.Invoke(serialNumber - orderPosition.Value);
                }
                else
                {
                    SerialNumberSetStringEvent?.Invoke(value);
                }
            }
        }

        public void SetSerialNumber(int serialNumberBase)
        {
            if (string.IsNullOrWhiteSpace(SerialNumber) == false) return;
            var orderPosition = this.Model.GetOrderPosition();
            if (orderPosition.HasValue)
            {
                this.SerialNumber = (serialNumberBase + orderPosition).ToString();
            }
        }

        public void SetSerialNumber(string serialNumberBase)
        {
            if (string.IsNullOrWhiteSpace(SerialNumber))
            {
                this.SerialNumber = serialNumberBase;
            }
        }

        public event Action<int> SerialNumberSetIntEvent;
        public event Action<string> SerialNumberSetStringEvent;

        #region Dates

        public DateTime? PickingDate
        {
            get => Model.PickingDate;
            set
            {
                var date = EndProductionDate ?? ShipmentDate ?? DeliveryDate ?? RealizationDate;
                if (date < value) return;
                SetDate(value);
            }
        }

        public DateTime? EndProductionDate
        {
            get => Model.EndProductionDate;
            set
            {
                if (value.HasValue)
                {
                    if (PickingDate > value)
                        return;

                    var date = ShipmentDate ?? DeliveryDate ?? RealizationDate;
                    if (date < value)
                        return;
                }

                SetDate(value);
            }
        }

        public DateTime? ShipmentDate
        {
            get => Model.ShipmentDate;
            set
            {
                if (value.HasValue)
                {
                    if (DeliveryDate < value)
                        return;

                    var date = EndProductionDate ?? PickingDate;
                    if (date > value)
                        return;
                }

                SetDate(value);
            }
        }

        public DateTime? DeliveryDate
        {
            get => Model.DeliveryDate;
            set
            {
                var date = ShipmentDate ?? EndProductionDate ?? PickingDate;
                if (date > value)
                    return;
                SetDate(value);
            }
        }

        public DateTime? RealizationDate
        {
            get => Model.RealizationDate;
            set
            {
                if (value.HasValue)
                {
                    var date = EndProductionDate ?? PickingDate;
                    if(date > value)
                        return;
                }
                SetDate(value);
            }
        }
        
        #endregion

        public bool HasFullInformation => (string.IsNullOrEmpty(SerialNumber) == false || Model.Product.ProductBlock.IsService) &&
                                          PickingDate.HasValue &&
                                          EndProductionDate.HasValue &&
                                          ShipmentDate.HasValue &&
                                          DeliveryDate.HasValue && RealizationDate.HasValue;

        /// <summary>
        /// Заказ укомплектован?
        /// </summary>
        public bool IsCompleted => PickingDate.HasValue && PickingDate < DateTime.Today;

        public DatesUnitViewModel(SalesUnit model) : base(model)
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
                CalculatedRealizationDateSetEvent?.Invoke(RealizationDate.Value);
            }

            if (DeliveryDate == null)
            {
                DeliveryDate = Model.DeliveryDateCalculated;
                CalculatedDeliveryDateSetEvent?.Invoke(DeliveryDate.Value);
            }
        }

        /// <summary>
        /// Событие установки свойства
        /// </summary>
        public event Action ValueSetToPropertyEvent;
        public event Action<DateTime> CalculatedRealizationDateSetEvent;
        public event Action<DateTime> CalculatedDeliveryDateSetEvent;

        private void SetValueNew<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        {
            this.SetValue(newValue, propertyName);
            RaisePropertyChanged(nameof(HasFullInformation));
            RaisePropertyChanged(nameof(IsCompleted));
            ValueSetToPropertyEvent?.Invoke();
        }

        /// <summary>
        /// Установка новых дат
        /// </summary>
        /// <param name="newDate"></param>
        /// <param name="propertyName"></param>
        private void SetDate(DateTime? newDate, [CallerMemberName] string propertyName = null)
        {
            //если ушло за 50 лет - это враньё
            if (newDate.HasValue && newDate > DateTime.Today.AddYears(50)) return;
            SetValueNew(newDate, propertyName);
        }
    }
}