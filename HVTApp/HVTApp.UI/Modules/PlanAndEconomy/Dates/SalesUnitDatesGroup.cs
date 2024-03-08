using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates
{
    public class SalesUnitDatesGroup : INotifyPropertyChanged
    {
        private DateTime? _pickingDate;
        private DateTime? _endProductionDate;
        private DateTime? _shipmentDate;
        private DateTime? _deliveryDate;
        private DateTime? _realizationDate;

        public List<SalesUnitDates> Units { get; }
        public SalesUnit Model => Units.First().Model;

        #region Dates

        public DateTime? PickingDate
        {
            get => _pickingDate;
            set
            {
                Units.ForEach(x => x.PickingDate = value);
                _pickingDate = Units.First().PickingDate;
            }
        }

        public DateTime? EndProductionDate
        {
            get => _endProductionDate;
            set
            {
                Units.ForEach(x => x.EndProductionDate = value);
                _endProductionDate = Units.First().EndProductionDate;
            }
        }

        public DateTime? ShipmentDate
        {
            get => _shipmentDate;
            set
            {
                Units.ForEach(x => x.ShipmentDate = value);
                _shipmentDate = Units.First().ShipmentDate;
            }
        }

        public DateTime? DeliveryDate
        {
            get => _deliveryDate;
            set
            {
                Units.ForEach(x => x.DeliveryDate = value);
                _deliveryDate = Units.First().DeliveryDate;
            }
        }

        public DateTime? RealizationDate
        {
            get => _realizationDate;
            set
            {
                Units.ForEach(x => x.RealizationDate = value);
                _realizationDate = Units.First().RealizationDate;
            }
        }

        #endregion

        public string SerialNumber
        {
            get
            {
                var serialNumbers = this.Units
                    .Where(x => string.IsNullOrEmpty(x.SerialNumber) == false)
                    .Select(x => x.SerialNumber)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                return serialNumbers.Any() 
                    ? serialNumbers.ToStringEnum() 
                    : string.Empty;
            }
            set => this.Units.ForEach(unit => unit.SerialNumber = value);
        }

        public bool HasFullInformation => Units.All(x => x.HasFullInformation);

        /// <summary>
        /// Заказ укомплектован?
        /// </summary>
        public bool IsCompleted => Units.All(x => x.IsCompleted);

        private int? GetOrderPosition(SalesUnit salesUnit)
        {
            if(int.TryParse(salesUnit.OrderPosition, out var result))
                return result;
            return null;
        }

        public SalesUnitDatesGroup(IEnumerable<SalesUnitDates> salesUnits)
        {
            Units = salesUnits.OrderBy(x => this.GetOrderPosition(x.Model)).ToList();
            Units.ForEach(unit =>
            {
                unit.ValueSetToPropertyEvent += () =>
                {
                    OnPropertyChanged(nameof(HasFullInformation));
                    OnPropertyChanged(nameof(IsCompleted));
                };

                unit.CalculatedDeliveryDateSetEvent += date =>
                {
                    _deliveryDate = date;
                    OnPropertyChanged(nameof(DeliveryDate));
                };

                unit.CalculatedRealizationDateSetEvent += date =>
                {
                    _realizationDate = date;
                    OnPropertyChanged(nameof(RealizationDate));
                };


                unit.SerialNumberSetIntEvent += sn => { this.Units.ForEach(x => x.SetSerialNumber(sn)); };
                unit.SerialNumberSetStringEvent += sn => { this.Units.ForEach(x => x.SetSerialNumber(sn)); };
            });


            var salesUnit = Units.First();
            _pickingDate = salesUnit.PickingDate;
            _endProductionDate = salesUnit.EndProductionDate;
            _shipmentDate = salesUnit.ShipmentDate;
            _deliveryDate = salesUnit.DeliveryDate;
            _realizationDate = salesUnit.RealizationDate;
        }

        /// <summary>
        /// Простановка даты реализации и даты доставки по дате отгрузки.
        /// </summary>
        public void SetCalculatedDates()
        {
            Units.ForEach(x => x.SetCalculatedDates());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}