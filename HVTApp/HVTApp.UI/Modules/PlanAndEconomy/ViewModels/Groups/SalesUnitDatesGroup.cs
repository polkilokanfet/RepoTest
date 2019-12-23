using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups
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

        public DateTime? PickingDate
        {
            get { return _pickingDate; }
            set
            {
                Units.ForEach(x => x.PickingDate = value);
                _pickingDate = Units.First().PickingDate;
            }
        }

        public DateTime? EndProductionDate
        {
            get { return _endProductionDate; }
            set
            {
                Units.ForEach(x => x.EndProductionDate = value);
                _endProductionDate = Units.First().EndProductionDate;
            }
        }

        public DateTime? ShipmentDate
        {
            get { return _shipmentDate; }
            set
            {
                Units.ForEach(x => x.ShipmentDate = value);
                _shipmentDate = Units.First().ShipmentDate;
            }
        }

        public DateTime? DeliveryDate
        {
            get { return _deliveryDate; }
            set
            {
                Units.ForEach(x => x.DeliveryDate = value);
                _deliveryDate = Units.First().DeliveryDate;
            }
        }

        public DateTime? RealizationDate
        {
            get { return _realizationDate; }
            set
            {
                Units.ForEach(x => x.RealizationDate = value);
                _realizationDate = Units.First().RealizationDate;
            }
        }

        public string OrderPosition { get; } = "...";
        public string SerialNumber { get; } = "...";

        public bool HasFullInformation => Units.All(x => x.HasFullInformation);

        public SalesUnitDatesGroup(IEnumerable<SalesUnitDates> salesUnits)
        {
            Units = salesUnits.ToList();
            Units.ForEach(x =>
            {
                x.SettedValueToProperty += () =>
                {
                    OnPropertyChanged(nameof(HasFullInformation));
                };

                x.SettedCalculatedDeliveryDate += date =>
                {
                    _deliveryDate = date;
                    OnPropertyChanged(nameof(DeliveryDate));
                };

                x.SettedCalculatedRealizationDate += date =>
                {
                    _realizationDate = date;
                    OnPropertyChanged(nameof(RealizationDate));
                };
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