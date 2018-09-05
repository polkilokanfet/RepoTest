using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Wrapper
{
    public class UnitsGroup : IUnitsGroup, INotifyPropertyChanged
    {
        public UnitsGroup(IEnumerable<IUnit> units)
        {
            Units = units.ToList();
            if (Units.Count > 1)
                Groups.AddRange(Units.Select(x => new UnitsGroup(new List<IUnit> {x})));

            ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedWrapper>(Units.First().ProductsIncluded);
            ProductsIncluded.CollectionChanged += (sender, args) =>
            {
                IEnumerable<IUnitWithProductsIncluded> unts = Units;
                if (Groups.Any()) unts = Groups;

                switch (args.Action)
                {
                        
                    case NotifyCollectionChangedAction.Add:
                        foreach (var unit in unts)
                        {
                            foreach (var newPrInc in args.NewItems)
                            {
                                var prInc = (ProductIncludedWrapper)newPrInc;
                                var newPrIncWrap = new ProductIncludedWrapper(new ProductIncluded {Product = prInc.Product.Model, Amount = prInc.Amount});
                                unit.ProductsIncluded.Add(newPrIncWrap);
                            }
                        }
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (var unit in unts)
                        {
                            foreach (var oldPrInc in args.OldItems)
                            {
                                var prInc = (ProductIncludedWrapper)oldPrInc;
                                var oldPrIncWrap = unit.ProductsIncluded.First(x => x.Amount == prInc.Amount && x.Product.Id == prInc.Product.Id);
                                unit.ProductsIncluded.Remove(oldPrIncWrap);
                            }
                        }
                        break;
                }
            };
        }

        private double _marginalIncome;
        private double _price;

        public int Amount => Units.Count;
        public double Total => Cost * Amount;

        public List<IUnit> Units { get; }
        public ObservableCollection<IUnitsGroup> Groups { get; } = new ObservableCollection<IUnitsGroup>();

        public FacilityWrapper Facility
        {
            get { return Units.First().Facility; }
            set
            {
                if(Equals(Facility, value)) return;
                Groups.ForEach(x => x.Facility = value);
                Units.ForEach(x => x.Facility = value);
                OnPropertyChanged();
            }
        }

        public ProductWrapper Product
        {
            get { return Units.First().Product; }
            set
            {
                if (Equals(Product, value)) return;
                Groups.ForEach(x => x.Product = value);
                Units.ForEach(x => x.Product = value);
                OnPropertyChanged();
            }
        }

        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; }

        public double Price
        {
            get { return _price; }
            set
            {
                if (Math.Abs(_price - value) < 0.0001) return;
                _price = value;
                MarginalIncome = (1 - Price / Cost) * 100;
                Groups.ForEach(x => x.Price = value);
                OnPropertyChanged();
            }
        }

        public double Cost
        {
            get { return Units.First().Cost; }
            set
            {
                if (Equals(value, Cost)) return;
                if (value < 0) return;

                Groups.ForEach(x => x.Cost = value);
                Units.ForEach(x => x.Cost = value);
                MarginalIncome = (1 - Price / value) * 100;
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged();
            }
        }

        public double MarginalIncome
        {
            get { return _marginalIncome; }
            set
            {
                if (Equals(_marginalIncome, value)) return;
                if (value >= 100) return;

                _marginalIncome = value;
                Cost = Price / (1 - value / 100);
                OnPropertyChanged();
            }
        }

        public PaymentConditionSetWrapper PaymentConditionSet
        {
            get { return Units.First().PaymentConditionSet; }
            set
            {
                if (Equals(value, PaymentConditionSet)) return;
                Groups.ForEach(x => x.PaymentConditionSet = value);
                Units.ForEach(x => x.PaymentConditionSet = value);
                OnPropertyChanged();
            }
        }



        public int? ProductionTerm
        {
            get { return Units.First().ProductionTerm; }
            set
            {
                if (Equals(value, ProductionTerm)) return;
                if (value < 0) return;
                Groups.ForEach(x => x.ProductionTerm = value);
                Units.ForEach(x => x.ProductionTerm = value);
                OnPropertyChanged();
            }
        }



        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}