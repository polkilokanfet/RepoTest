using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitWrapper : ProductCostUnitWrapper<OfferUnit>
    {
    }

    public abstract class ProductCostUnitWrapper<T> : WrapperBase<T>, IProductCostUnitWrapper
        where T : class, IProductCostUnit
    {
        private DateTime _priceDate;
        private IValidatableChangeTrackingCollection<ProductWrapper> _dependentProducts;

        public event Action ProductChanged;
        public event Action CostChanged;

        protected ProductCostUnitWrapper(T model) : base(model)
        {
        }

        public ProductWrapper Product
        {
            get { return GetWrapper<ProductWrapper>(); }
            set
            {
                SetComplexValue<Product, ProductWrapper>(Product, value);
                if (!Equals(Product, value)) ProductChanged?.Invoke();
            }
        }

        public IValidatableChangeTrackingCollection<ProductWrapper> DependentProducts
        {
            get { return _dependentProducts; }
            protected set
            {
                _dependentProducts = value;
                value.CollectionChanged += OnDependentProductsCollectionChanged;
                value.ForEach(x => x.PropertyChanged += OnDependentProductPropertyChanged);
            }
        }

        public double Cost
        {
            get { return GetValue<double>(); }
            set
            {
                SetValue(value);
                if (!Equals(Cost, value)) CostChanged?.Invoke();
            }
        }

        public DateTime PriceDate
        {
            get { return _priceDate; }
            set
            {
                if (Equals(_priceDate, value)) return;
                _priceDate = value;
                OnPropertyChanged();
                RisePropertyChangedEvents();
            }
        }

        public double Price => Product.GetPrice(PriceDate) + DependentProducts.Sum(x => x.GetPrice(PriceDate));

        public bool HasBlocksWithoutPrice => Product.GetBlocksWithoutAnyPrice().Any() || DependentProducts.Any(x => x.GetBlocksWithoutAnyPrice().Any());

        public string PriceErrors
        {
            get
            {
                var blocks = Product.GetBlocksWithoutAnyPrice();
                string result = string.Empty;
                foreach (var block in blocks)
                {
                    result += $"{block.DisplayMember} has no price!!!; ";
                }

                blocks = Product.GetBlocksWithoutActualPriceOnDate(PriceDate);
                foreach (var block in blocks)
                {
                    result += $"{block.DisplayMember} has no actual price; ";
                }
                return result;
            }
        }

        public double MarginalIncome
        {
            get { return (Math.Abs(Cost) > 0.0001) ? 100 * (Cost - Price) / Cost : 0; }
            set
            {
                if (Equals(MarginalIncome, value)) return;
                if (Math.Abs(value - 100) < 0.001) return;
                Cost = Price / (100 - value) * 100;
                OnPropertyChanged();
            }
        }

        private void OnDependentProductsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            DependentProducts.RemovedItems.ForEach(x => x.PropertyChanged -= OnDependentProductPropertyChanged);
            DependentProducts.AddedItems.ForEach(x => x.PropertyChanged += OnDependentProductPropertyChanged);
            RisePropertyChangedEvents();
        }

        private void OnDependentProductPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RisePropertyChangedEvents();
        }

        private void RisePropertyChangedEvents()
        {
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(MarginalIncome));
            OnPropertyChanged(nameof(PriceErrors));
            OnPropertyChanged(nameof(HasBlocksWithoutPrice));
        }
    }
}