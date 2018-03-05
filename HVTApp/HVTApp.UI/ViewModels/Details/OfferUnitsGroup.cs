using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public class OfferUnitsGroup : BaseUnitsGroup<OfferUnitWrapper>
    {
        public OfferUnitsGroup(IEnumerable<OfferUnitWrapper> units) : base(units)
        {
            if(units == null || !units.Any())
                throw new ArgumentNullException(nameof(units));

            DependentProducts = new ValidatableChangeTrackingCollection<ProductDependentWrapper>(units.First().DependentProducts);
            DependentProducts.CollectionChanged += OnDeprndentProductsCollectionChanged;
        }

        private void OnDeprndentProductsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                foreach (var newItem in args.NewItems)
                {
                    var product = (ProductDependentWrapper) newItem;
                    foreach (var unit in Units)
                    {
                        unit.DependentProducts.Add(new ProductDependentWrapper(new ProductDependent
                        {
                            Product = product.Product.Model,
                            Amount = product.Amount,
                            Cost = product.Cost,
                            IsIndependent = product.IsIndependent
                        }));
                    }
                }
            }

            if (args.OldItems != null)
            {
                foreach (var newItem in args.OldItems)
                {
                    var product = (ProductDependentWrapper) newItem;
                    foreach (var unit in Units)
                    {
                        var target = unit.DependentProducts.First(x =>
                            Equals(x.Product.Model, product.Product.Model) &&
                            x.Amount == product.Amount &&
                            Math.Abs(x.Cost - product.Cost) < 0.00001 &&
                            x.IsIndependent == product.IsIndependent);
                        unit.DependentProducts.Remove(target);
                    }
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

        public IValidatableChangeTrackingCollection<ProductDependentWrapper> DependentProducts { get; }

        public double Total => Units.Sum(x => x.Cost);

        public bool HasBlocksWithoutPrice => GetValue<bool>();

    }
}