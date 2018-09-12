using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.UI.Wrapper;
using Prism.Mvvm;

namespace HVTApp.Modules.Price.ViewModels
{
    public class DatesGroup : BindableBase
    {
        private readonly List<SalesUnitWrapper> _units;
        public SalesUnitWrapper Unit => _units.First();

        public string OrderPosition
        {
            get { return !Groups.Any() ? Unit.OrderPosition : "-"; }
            set { Unit.OrderPosition = value; }
        }

        public DateTime? DeliveryDate
        {
            get { return Unit.DeliveryDate; }
            set { SetValue(value); }
        }

        public DateTime? EndProductionDate
        {
            get { return Unit.EndProductionDate; }
            set { SetValue(value); }
        }

        public DateTime? PickingDate
        {
            get { return Unit.PickingDate; }
            set { SetValue(value); }
        }

        public DateTime? RealizationDate
        {
            get { return Unit.RealizationDate; }
            set { SetValue(value); }
        }

        public DateTime? ShipmentDate
        {
            get { return Unit.ShipmentDate; }
            set { SetValue(value); }
        }

        public ObservableCollection<DatesGroup> Groups { get; } = new ObservableCollection<DatesGroup>();

        public DatesGroup(IEnumerable<SalesUnitWrapper> units)
        {
            _units = units.ToList();
            if (_units.Count > 1)
            {
                Groups.AddRange(_units.Select(x => new DatesGroup(new[] {x})));
            }
        }

        private void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            var old = this.GetType().GetProperty(propertyName).GetValue(this);
            if (Equals(old, value))
                return;

            if (Groups.Any())
            {
                foreach (var unitsGroup in Groups)
                {
                    var property = unitsGroup.GetType().GetProperty(propertyName);
                    property.SetValue(unitsGroup, value);
                }
            }
            else
            {
                foreach (var unit in _units)
                {
                    var property = unit.GetType().GetProperty(propertyName);
                    property.SetValue(unit, value);
                }
            }
            OnPropertyChanged(propertyName);
        }

        public static IEnumerable<DatesGroup> GetGroups(IEnumerable<SalesUnitWrapper> units)
        {
            var groups = units.GroupBy(x => new
            {
                Facility = x.Facility.Id,
                Product = x.Product.Id,
                Order = x.Order?.Id,
                Project = x.Project.Id,
                Specification = x.Specification?.Id,
                x.DeliveryDate,
                x.EndProductionDate,
                x.PickingDate,
                x.RealizationDate,
                x.ShipmentDate
            });

            return groups.Select(x => new DatesGroup(x));
        }
    }
}