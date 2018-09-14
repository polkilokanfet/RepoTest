using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.Modules.Price.ViewModels
{
    public class ProductionGroup : BindableBase
    {
        private readonly List<SalesUnitWrapper> _units;

        public SalesUnitWrapper Unit => _units.First();
        public int Amount => _units.Count;

        public DateTime? Date
        {
            get { return Unit.EndProductionDate; }
            set
            {
                if (Date == value) return;
                if (Groups.Any())
                {
                    Groups.ForEach(x => x.Date = value);
                }
                else
                {
                    Unit.EndProductionDate = value;
                }
                OnPropertyChanged();
            }
        }

        public OrderWrapper Order
        {
            get { return Unit.Order; }
            set
            {
                if(Equals(Order, value)) return;
                if (Groups.Any())
                {
                    Groups.ForEach(x => x.Order = value);
                }
                else
                {
                    Unit.Order = value;
                }
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get { return Groups.Any() ? "..." : Unit.OrderPosition; }
            set
            {
                if (Groups.Any()) return;
                Unit.OrderPosition = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProductionGroup> Groups { get; } = new ObservableCollection<ProductionGroup>();

        public ProductionGroup(IEnumerable<SalesUnitWrapper> units)
        {
            _units = units.ToList();
            if (_units.Count > 1)
            {
                Groups.AddRange(_units.Select(x => new ProductionGroup(new[] {x})));
            }
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