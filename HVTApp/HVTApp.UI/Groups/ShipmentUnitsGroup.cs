using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Groups
{
    public class ShipmentUnitsGroup : BindableBase
    {
        private readonly List<ShippingItemWrapper> _units;
        private readonly ShippingItemWrapper _unit;
        private DateTime? _date;

        public Facility Facility => _unit.Model.Facility;
        public ProductType ProductType => _unit.Model.Product.ProductType;
        public string ProductDesignation => _unit.Model.Product.Designation;
        public int Amount => _units.Count;
        public string Order => _unit.Model.Order?.Number;
        public Company Company => _unit.Model.Specification?.Contract.Contragent;
        public string Specification => _unit.Model.Specification?.Number;
        public string Contract => _unit.Model.Specification?.Contract.Number;
        public DateTime ShippingDate => _unit.Model.ShipmentDateCalculated;
        public DateTime ProductionDate => _unit.Model.EndProductionDateCalculated;

        public bool IsShipped => _unit.Model.ShipmentDate.HasValue;

        public DateTime? Date
        {
            get { return _unit.Model.ShipmentDate ?? _date; }
            set
            {
                if (Equals(_date, value)) return;

                //если прилетело значение позже расчетного
                if (value.HasValue && value.Value < _unit.Model.EndProductionDateCalculated) return;

                _date = value;
                if (Groups.Any())
                    Groups.ForEach(x => x.Date = value);
                else
                    _units.ForEach(x => x.ShipmentPlanDate = value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ShipmentUnitsGroup> Groups { get; } = new ObservableCollection<ShipmentUnitsGroup>();

        public ShipmentUnitsGroup(IEnumerable<ShippingItemWrapper> units)
        {
            _units = units.ToList();
            _unit = _units.First();
            _date = _unit.ShipmentPlanDate;

            if (_units.Count > 1)
            {
                Groups.AddRange(_units.Select(x => new ShipmentUnitsGroup(new[] { x })));
            }
        }

        public static IEnumerable<ShipmentUnitsGroup> Grouping(IEnumerable<ShippingItemWrapper> units)
        {
            var groups = units.GroupBy(x => new
            {
                Facility = x.Model.Facility.Id,
                Product = x.Model.Product.Id,
                Order = x.Model.Order?.Id,
                Project = x.Model.Project.Id,
                Specification = x.Model.Specification?.Id,
                ShipmentDateCalculated = x.Model.ShipmentDateCalculated
            }).OrderBy(x => x.Key.ShipmentDateCalculated);

            return groups.Select(x => new ShipmentUnitsGroup(x));
        }

    }
}