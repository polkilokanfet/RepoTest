using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Shippings
{
    public class ShippingGroup : BindableBase
    {
        private readonly ShippingUnitWrapper _unit;
        private DateTime? _date;

        public ObservableCollection<ShippingUnitWrapper> Units { get; }

        public Facility Facility => _unit.Model.Facility;
        public ProductType ProductType => _unit.Model.Product.ProductType;
        public string ProductDesignation => _unit.Model.Product.Designation;
        public int Amount => Units.Count;
        public string Order => _unit.Model.Order?.Number;
        public Company Company => _unit.Model.Specification?.Contract.Contragent;
        public string Specification => _unit.Model.Specification?.Number;
        public string Contract => _unit.Model.Specification?.Contract.Number;
        public DateTime ShippingDate => _unit.Model.ShipmentDateCalculated;
        public DateTime ProductionDate => _unit.Model.EndProductionDateCalculated;

        public bool IsShipped => _unit.Model.ShipmentDate.HasValue;

        public DateTime? Date
        {
            get
            {
                if (_unit.Model.ShipmentDate.HasValue)
                    return _unit.Model.ShipmentDate.Value;

                return _date;
            }
            set
            {
                if (Equals(_date, value)) return;

                //если прилетело значение позже расчетного
                if (value.HasValue && value.Value < _unit.Model.EndProductionDateCalculated) return;

                _date = value;

                Units.ForEach(x => x.Date = value);
                OnPropertyChanged();
            }
        }

        public ShippingGroup(IEnumerable<ShippingUnitWrapper> units)
        {
            Units = new ObservableCollection<ShippingUnitWrapper>(units);
            _unit = Units.First();
            _date = _unit.ShipmentPlanDate;
        }
    }
}