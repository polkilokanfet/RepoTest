using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class SalesUnitDatesGroup
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

        public SalesUnitDatesGroup(IEnumerable<SalesUnitDates> salesUnits)
        {
            Units = salesUnits.ToList();
        }
    }
}