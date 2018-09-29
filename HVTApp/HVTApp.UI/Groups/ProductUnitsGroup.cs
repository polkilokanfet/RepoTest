using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Groups
{
    public class ProductUnitsGroup
    {
        public static IEnumerable<ProductUnitsGroup> Grouping(IEnumerable<SalesUnit> units)
        {
            var groups = units.GroupBy(x => new
            {
                x.Facility,
                x.Product,
                x.Order,
                x.Project,
                x.Specification,
                x.EndProductionDateCalculated
            }).OrderBy(x => x.Key.EndProductionDateCalculated);

            return groups.Select(x => new ProductUnitsGroup(x));
        }

        public List<SalesUnit> Units { get; }
        private readonly SalesUnit _unit;

        public Facility Facility => _unit.Facility;
        public ProductType ProductType => _unit.Product.ProductType;
        public string ProductDesignation => _unit.Product.Designation;
        public int Amount => Units.Count;
        public string Order => _unit.Order?.Number;
        public Company Company => _unit.Specification?.Contract.Contragent;
        public string Specification => _unit.Specification?.Number;
        public string Contract => _unit.Specification?.Contract.Number;
        public DateTime EndProductionDate => _unit.EndProductionDateCalculated;
        public string TceRequest
        {
            get { return _unit.TceRequest; }
            set { _unit.TceRequest = value; }
        }

        public ObservableCollection<ProductUnitsGroup> Groups { get; } = new ObservableCollection<ProductUnitsGroup>();

        public ProductUnitsGroup(IEnumerable<SalesUnit> units)
        {
            Units = units.ToList();
            _unit = Units.First();

            if (Units.Count > 1)
            {
                Groups.AddRange(Units.Select(x => new ProductUnitsGroup(new[] {x})));
            }
        }

        public void ProductingGroup()
        {
            if (Groups.Any())
            {
                Groups.ForEach(x => x.ProductingGroup());
                return;
            }

            foreach (var unit in Units)
            {
                unit.SignalToStartProduction = DateTime.Today;
            }
        }

        public void SetOrder(Order order)
        {
            if (Groups.Any())
            {
                Groups.ForEach(x => x.SetOrder(order));
                return;
            }

            Units.ForEach(x => x.Order = order);
        }
    }
}