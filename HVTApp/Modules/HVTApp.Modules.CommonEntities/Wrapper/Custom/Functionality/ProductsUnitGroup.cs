using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public class ProductsUnitGroup : ObservableCollection<IProductWithCost>
    {
        //public PartWrapper Part => this.First().Product;
        public CostWrapper Cost => this.First().Cost;
        //public string ProductName => Part.Designation;

        public ProductsUnitGroup(IEnumerable<IProductWithCost> units) : base(units)
        {
            if (units == null) throw new ArgumentNullException(nameof(units));
            if (!units.Any()) throw new ArgumentException();
        }
    }

    public class ProductsUnitsGroupsCollection<T> : ObservableCollection<ProductsUnitGroup>
        where T : class, IProductWithCost
    {
        private readonly IValidatableChangeTrackingCollection<T> _sourceUnits;

        public ProductsUnitsGroupsCollection(IValidatableChangeTrackingCollection<T> sourceUnits)
        {
            if (sourceUnits == null) throw new ArgumentNullException(nameof(sourceUnits));

            _sourceUnits = sourceUnits;

            foreach (var unitsGroupedByProduct in sourceUnits.GroupBy(x => x.Product)) //группируем по продукту
            {
                foreach (var unitsGroupedByCurrency in unitsGroupedByProduct.GroupBy(x => x.Cost.Currency)) //по валюте
                {
                    foreach (var unitsGroupedBySum in unitsGroupedByCurrency.GroupBy(x => x.Cost.Sum)) //по сумме
                    {
                        var unitGroup = new ProductsUnitGroup(unitsGroupedBySum);
                        this.Add(unitGroup);
                        unitGroup.CollectionChanged += UnitGroupOnCollectionChanged;
                    }
                }
            }

            _sourceUnits.CollectionChanged += SourceUnitsOnCollectionChanged;
        }

        //реакция на изменение в группе
        private void UnitGroupOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            foreach (var unit in eventArgs.NewItems.Cast<T>())
            {
                if (!_sourceUnits.Contains(unit)) _sourceUnits.Add(unit);
            }

            foreach (var unit in eventArgs.OldItems.Cast<T>())
            {
                if (_sourceUnits.Contains(unit)) _sourceUnits.Remove(unit);
            }

            //удаляем группу, если она опустошена
            ProductsUnitGroup productsUnitGroup = sender as ProductsUnitGroup;
            if (!productsUnitGroup.Any())
            {
                this.Remove(productsUnitGroup);
                productsUnitGroup.CollectionChanged -= UnitGroupOnCollectionChanged;
            }
        }

        //реакция на изменение в коллекции-источнике.
        private void SourceUnitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            foreach (var oldUnit in eventArgs.OldItems.Cast<IProductWithCost>())
            {
                if (this.Any(x => x.Contains(oldUnit)))
                    this.Single(x => x.Contains(oldUnit)).Remove(oldUnit);
            }

            foreach (var newUnit in eventArgs.NewItems.Cast<IProductWithCost>())
            {
                if (this.Any(x => x.Contains(newUnit)))
                    continue;

                if (this.Any(x => Equals(x, newUnit.Product)))
                {
                    this.Single(x => Equals(x, newUnit.Product)).Add(newUnit);
                    continue;
                }

                ProductsUnitGroup productsUnitGroup = new ProductsUnitGroup(new[] {newUnit});
                this.Add(productsUnitGroup);
                productsUnitGroup.CollectionChanged += UnitGroupOnCollectionChanged;
            }
        }
    }
}
