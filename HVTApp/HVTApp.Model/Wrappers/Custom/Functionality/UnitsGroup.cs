using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public class UnitsGroup : ObservableCollection<UnitWrapper>
    {
        public ProductWrapper Product => this.First().Product;
        public string ProductName => Product.Designation;

        public UnitsGroup(IEnumerable<UnitWrapper> units) : base(units)
        {
            if (units == null) throw new NullReferenceException();
            if (!units.Any()) throw new ArgumentException();
        }
    }

    public class UnitsGroupsCollection : ObservableCollection<UnitsGroup>
    {
        private readonly IValidatableChangeTrackingCollection<UnitWrapper> _sourceUnits; 
        public UnitsGroupsCollection(IValidatableChangeTrackingCollection<UnitWrapper> sourceUnits)
        {
            if (sourceUnits == null) throw new NullReferenceException();

            _sourceUnits = sourceUnits;

            foreach (var units in sourceUnits.GroupBy(x => x.Product))
            {
                var unitGroup = new UnitsGroup(units);
                this.Add(unitGroup);
                unitGroup.CollectionChanged += UnitGroupOnCollectionChanged;
            }

            _sourceUnits.CollectionChanged += SourceUnitsOnCollectionChanged;
        }

        //реакция на изменение в группе
        private void UnitGroupOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            foreach (UnitWrapper unit in eventArgs.NewItems.Cast<UnitWrapper>())
            {
                if (!_sourceUnits.Contains(unit)) _sourceUnits.Add(unit);
            }

            foreach (UnitWrapper unit in eventArgs.OldItems.Cast<UnitWrapper>())
            {
                if (_sourceUnits.Contains(unit)) _sourceUnits.Remove(unit);
            }

            //удаляем группу, если она опустошена
            UnitsGroup unitsGroup = sender as UnitsGroup;
            if (!unitsGroup.Any())
            {
                this.Remove(unitsGroup);
                unitsGroup.CollectionChanged -= UnitGroupOnCollectionChanged;
            }
        }

        //реакция на изменение в коллекции-источнике.
        private void SourceUnitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            foreach (var oldUnit in eventArgs.OldItems.Cast<UnitWrapper>())
            {
                if (this.Any(x => x.Contains(oldUnit)))
                    this.Single(x => x.Contains(oldUnit)).Remove(oldUnit);
            }

            foreach (UnitWrapper newUnit in eventArgs.NewItems.Cast<UnitWrapper>())
            {
                if (this.Any(x => x.Contains(newUnit)))
                    continue;

                if (this.Any(x => Equals(x.Product, newUnit.Product)))
                {
                    this.Single(x => Equals(x.Product, newUnit.Product)).Add(newUnit);
                    continue;
                }

                UnitsGroup unitsGroup = new UnitsGroup(new[] {newUnit});
                this.Add(unitsGroup);
                unitsGroup.CollectionChanged += UnitGroupOnCollectionChanged;
            }
        }
    }
}
