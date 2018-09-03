using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.ViewModels
{
    public class OfferUnitsGroup : BaseUnitsGroup<OfferUnitWrapper>
    {
        private readonly Dictionary<ProductIncludedWrapper, List<ProductIncludedWrapper>> _dictionary = 
            new Dictionary<ProductIncludedWrapper, List<ProductIncludedWrapper>>();

        public OfferUnitsGroup(IEnumerable<OfferUnitWrapper> units) : base(units)
        {
            if(units == null || !units.Any())
                throw new ArgumentNullException(nameof(units));

            var dependentProducts = units.First().ProductsIncluded;
            ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedWrapper>(dependentProducts);
            foreach (var productDependentWrapper in ProductsIncluded)
            {
                var list = units.Select(x => x.ProductsIncluded.First(dp => 
                    Equals(dp.Model.Product.Id.GetHashCode() + dp.Amount.GetHashCode(), 
                    productDependentWrapper.Model.Product.Id.GetHashCode() + productDependentWrapper.Amount.GetHashCode()))).ToList();
                _dictionary.Add(productDependentWrapper, list);
            }
            ProductsIncluded.CollectionChanged += OnProductsIncludedCollectionChanged;
            ProductsIncluded.ForEach(x => x.PropertyChanged += DependentProductOnPropertyChanged);
        }

        private void DependentProductOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!Equals(e.PropertyName, nameof(ProductDependent.Product)) &&
                !Equals(e.PropertyName, nameof(ProductDependent.Amount)))
                return;

            var productDependentWrapper = (ProductIncludedWrapper)sender;
            var propInfo = sender.GetType().GetProperty(e.PropertyName);
            var value = propInfo.GetValue(sender);
            foreach (var target in _dictionary[productDependentWrapper])
            {
                var currentValue = propInfo.GetValue(target);
                if (!Equals(currentValue, value))
                {
                    propInfo.SetValue(target, value);
                }
            }
        }

        private void OnProductsIncludedCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                foreach (var newItem in args.NewItems)
                {
                    var product = (ProductIncludedWrapper) newItem;
                    _dictionary.Add(product, new List<ProductIncludedWrapper>());
                    foreach (var unit in Units)
                    {
                        var productDependentWrapper = new ProductIncludedWrapper(new ProductIncluded
                            {
                                Product = product.Product.Model,
                                Amount = product.Amount
                            });
                        unit.ProductsIncluded.Add(productDependentWrapper);
                        productDependentWrapper.PropertyChanged += DependentProductOnPropertyChanged;
                        _dictionary[product].Add(productDependentWrapper);
                    }
                }
            }

            if (args.OldItems != null)
            {
                foreach (var oldItem in args.OldItems)
                {
                    var oldProduct = (ProductIncludedWrapper) oldItem;
                    foreach (var target in _dictionary[oldProduct])
                    {
                        Units.Single(x => x.ProductsIncluded.Contains(target)).ProductsIncluded.Remove(target);
                        target.PropertyChanged -= DependentProductOnPropertyChanged;
                    }
                    _dictionary.Remove(oldProduct);
                }
            }
        }

        public FacilityWrapper Facility
        {
            get { return GetValue<FacilityWrapper>(); }
            set { SetValue(value); }
        }

        public ProductWrapper Product
        {
            get { return GetValue<ProductWrapper>(); }
            set { SetValue(value); }
        }

        public PaymentConditionSetWrapper PaymentConditionSet
        {
            get { return GetValue<PaymentConditionSetWrapper>(); }
            set { SetValue(value); }
        }

        public double MarginalIncome
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public int ProductionTerm
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public double Cost
        {
            get { return GetValue<double>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged(nameof(Total));
            }
        }

        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; }

        public double Total => Units.Sum(x => x.Cost);

        public bool HasBlocksWithoutPrice => GetValue<bool>();

    }
}