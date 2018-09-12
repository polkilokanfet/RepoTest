using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Modules.Price.ViewModels
{
    public class ProductionGroup
    {
        private readonly List<SalesUnitWrapper> _units;
        private DateTime? _date;

        public SalesUnitWrapper Unit => _units.First();
        public int Amount => _units.Count;

        public DateTime? Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
                if (Groups.Any())
                {
                    Groups.ForEach(x => x.Date = value);
                }
                else
                {
                    _units.ForEach(x => x.EndProductionPlanDate = value);
                }
            }
        }

        public ObservableCollection<ProductionGroup> Groups { get; } = new ObservableCollection<ProductionGroup>();

        public ProductionGroup(IEnumerable<SalesUnitWrapper> units)
        {
            _units = units.ToList();
        }

        public static IEnumerable<ProductionGroup> Grouping(IEnumerable<SalesUnitWrapper> units)
        {
            var groups = units.GroupBy(x => new
            {
                Facility = x.Facility.Id,
                Product = x.Product.Id,
                Order = x.Order?.Id,
                Project = x.Project.Id,
                Specification = x.Specification?.Id,
                x.EndProductionPlanDate
            }).OrderBy(x => x.Key.EndProductionPlanDate);

            return groups.Select(x => new ProductionGroup(x));
        }

        public void SetSignalToStartProductionDone()
        {
            if (Groups.Any())
            {
                Groups.ForEach(x => x.SetSignalToStartProductionDone());
                return;
            }
            _units.ForEach(x => x.SignalToStartProductionDone = DateTime.Today);
        }
    }
}