using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Prism.Mvvm;

namespace HVTApp.Model.Wrapper
{
    public class UnitsGroup : BindableBase, IUnitsGroup
    {
        private double _marginalIncome;
        private double _price;

        public int Amount => Units.Count;
        public double Total => Cost * Amount;

        public List<IUnitWrapper> Units { get; }
        public ObservableCollection<IUnitsGroup> Groups { get; } = new ObservableCollection<IUnitsGroup>();

        public FacilityWrapper Facility
        {
            get => Units.First().Facility;
            set => SetValue(value);
        }

        public ProductWrapper Product
        {
            get => Units.First().Product;
            set => SetValue(value);
        }

        public PaymentConditionSetWrapper PaymentConditionSet
        {
            get => Units.First().PaymentConditionSet;
            set => SetValue(value);
        }

        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; }

        public double Price
        {
            get => _price;
            set
            {
                if (Math.Abs(_price - value) < 0.0001) return;
                _price = value;
                MarginalIncome = (1 - Price / Cost) * 100;
                Groups.ForEach(x => x.Price = value);
                RaisePropertyChanged();
            }
        }

        public double Cost
        {
            get => Units.First().Cost;
            set
            {
                if (Equals(value, Cost)) return;
                if (value < 0) return;

                Groups.ForEach(x => x.Cost = value);
                Units.ForEach(x => x.Cost = value);
                MarginalIncome = (1 - Price / value) * 100;
                RaisePropertyChanged(nameof(Total));
                RaisePropertyChanged();
            }
        }

        public double? MarginalIncome
        {
            get => _marginalIncome;
            set
            {
                if (Equals(_marginalIncome, value)) return;
                if (value >= 100) return;

                //_marginalIncome = value;
                //Cost = Price / (1 - value / 100);
                //RaisePropertyChanged();
            }
        }

        public int? ProductionTerm
        {
            get => Units.First().ProductionTerm;
            set
            {
                if (value.HasValue && value < 0)
                    return;
                SetValue(value);
            }
        }



        public UnitsGroup(IEnumerable<IUnitWrapper> units)
        {
            Units = units.ToList();
            if (Units.Count > 1)
            {
                GenerateGroups();
            }

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

        protected virtual void GenerateGroups()
        {
            Groups.AddRange(Units.Select(x => new UnitsGroup(new List<IUnitWrapper> {x})));
        }

        protected void SetValue(object value, [CallerMemberName] string propertyName = null)
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
                foreach (var unit in Units)
                {
                    var property = unit.GetType().GetProperty(propertyName);
                    property.SetValue(unit, value);
                }
            }
            RaisePropertyChanged(propertyName);
        }

        public void AcceptChanges()
        {
            throw new NotImplementedException();
        }

        public bool IsChanged { get; }
        public void RejectChanges()
        {
            throw new NotImplementedException();
        }

        public bool IsValid { get; }
    }

    public class UnitsDatedGroup : UnitsGroup, IUnitsDatedGroup
    {
        public DateTime DeliveryDateExpected
        {
            get => ((IUnitWrapperDated)Units.First()).DeliveryDateExpected;
            set => SetValue(value);
        }

        public UnitsDatedGroup(IEnumerable<IUnitWrapperDated> units) : base(units)
        {
        }

        protected override void GenerateGroups()
        {
            Groups.AddRange(Units.Select(x => new UnitsDatedGroup(new List<IUnitWrapperDated> { (IUnitWrapperDated)x })));
        }
    }
}