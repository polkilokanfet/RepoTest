using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Groups
{
    public class ShipmentUnitsGroup : BindableBase
    {
        private readonly List<SalesUnitWrapper> _units;
        private readonly SalesUnitWrapper _unit;
        private DateTime? _date;

        public Facility Facility => _unit.Model.Facility;
        public ProductType ProductType => _unit.Model.Product.ProductType;
        public string ProductDesignation => _unit.Model.Product.Designation;
        public int Amount => _units.Count;
        public string Order => _unit.Order?.Number;
        public Company Company => _unit.Model.Specification?.Contract.Contragent;
        public string Specification => _unit.Specification?.Number;
        public string Contract => _unit.Specification?.Contract.Number;
        public DateTime ShippingDate => _unit.ShipmentDateCalculated;

        public DateTime? Date
        {
            get { return _date; }
            set
            {
                if (Equals(_date, value)) return;
                _date = value;
                if (Groups.Any())
                    Groups.ForEach(x => x.Date = value);
                else
                    _units.ForEach(x => x.ShipmentPlanDate = value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ShipmentUnitsGroup> Groups { get; } = new ObservableCollection<ShipmentUnitsGroup>();

        public ShipmentUnitsGroup(IEnumerable<SalesUnitWrapper> units)
        {
            _units = units.ToList();
            _unit = _units.First();
            _date = _unit.ShipmentPlanDate;

            if (_units.Count > 1)
            {
                Groups.AddRange(_units.Select(x => new ShipmentUnitsGroup(new[] { x })));
            }
        }

        public static IEnumerable<ShipmentUnitsGroup> Grouping(IEnumerable<SalesUnitWrapper> units)
        {
            var groups = units.GroupBy(x => new
            {
                Facility = x.Facility.Id,
                Product = x.Product.Id,
                Order = x.Order?.Id,
                Project = x.Project.Id,
                Specification = x.Specification?.Id,
                ShipmentDateCalculated = x.ShipmentDateCalculated
            }).OrderBy(x => x.Key.ShipmentDateCalculated);

            return groups.Select(x => new ShipmentUnitsGroup(x));
        }

    }
}