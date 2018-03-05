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
        private readonly Dictionary<ProductDependentWrapper, List<ProductDependentWrapper>> _dictionary = 
            new Dictionary<ProductDependentWrapper, List<ProductDependentWrapper>>();

        public OfferUnitsGroup(IEnumerable<OfferUnitWrapper> units) : base(units)
        {
            if(units == null || !units.Any())
                throw new ArgumentNullException(nameof(units));

            var dependentProducts = units.First().DependentProducts;
            DependentProducts = new ValidatableChangeTrackingCollection<ProductDependentWrapper>(dependentProducts);
            foreach (var productDependentWrapper in DependentProducts)
            {
                var list = units.Select(x => x.DependentProducts.First(dp => 
                    Equals(dp.Model.GetHashCode(), productDependentWrapper.Model.GetHashCode()))).ToList();
                _dictionary.Add(productDependentWrapper, list);
            }
            DependentProducts.CollectionChanged += OnDependentProductsCollectionChanged;
            DependentProducts.ForEach(x => x.PropertyChanged += DependentProductOnPropertyChanged);
        }

        private void DependentProductOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!Equals(e.PropertyName, nameof(ProductDependent.Product)) &&
                !Equals(e.PropertyName, nameof(ProductDependent.Amount)) &&
                !Equals(e.PropertyName, nameof(ProductDependent.Cost)) &&
                !Equals(e.PropertyName, nameof(ProductDependent.IsIndependent)))
                return;

            ProductDependentWrapper productDependentWrapper = (ProductDependentWrapper)sender;
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

        private void OnDependentProductsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                foreach (var newItem in args.NewItems)
                {
                    var product = (ProductDependentWrapper) newItem;
                    _dictionary.Add(product, new List<ProductDependentWrapper>());
                    foreach (var unit in Units)
                    {
                        var productDependentWrapper = new ProductDependentWrapper(new ProductDependent
                            {
                                Product = product.Product.Model,
                                Amount = product.Amount,
                                Cost = product.Cost,
                                IsIndependent = product.IsIndependent
                            });
                        unit.DependentProducts.Add(productDependentWrapper);
                        productDependentWrapper.PropertyChanged += DependentProductOnPropertyChanged;
                        _dictionary[product].Add(productDependentWrapper);
                    }
                }
            }

            if (args.OldItems != null)
            {
                foreach (var oldItem in args.OldItems)
                {
                    var oldProduct = (ProductDependentWrapper) oldItem;
                    foreach (var target in _dictionary[oldProduct])
                    {
                        Units.Single(x => x.DependentProducts.Contains(target)).DependentProducts.Remove(target);
                        target.PropertyChanged -= DependentProductOnPropertyChanged;
                    }
                    _dictionary.Remove(oldProduct);
                }
            }
        }

        private ProductDependentWrapper GetTarget(IEnumerable<ProductDependentWrapper> enumerable, ProductDependentWrapper pdw)
        {
            return enumerable.First(x => Equals(x.Product.Model, pdw.Product.Model) &&
                                    x.Amount == pdw.Amount &&
                                    Math.Abs(x.Cost - pdw.Cost) < 0.00001 &&
                                    x.IsIndependent == pdw.IsIndependent);
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

        public IValidatableChangeTrackingCollection<ProductDependentWrapper> DependentProducts { get; }

        public double Total => Units.Sum(x => x.Cost);

        public bool HasBlocksWithoutPrice => GetValue<bool>();

    }
}