using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper
{
    public abstract class ProductCostWrapper<T> : WrapperBase<T>, IProductCostWrapper
        where T : class, IProductCost
    {
        private DateTime _priceDate = DateTime.Today;

        public event Action ProductChanged;
        public event Action CostChanged;

        protected ProductCostWrapper(T model) : base(model)
        {
        }

        public ProductWrapper Product
        {
            get { return GetWrapper<ProductWrapper>(); }
            set
            {
                if (Equals(Product, value)) return;

                SetComplexValue<Product, ProductWrapper>(Product, value);
                ProductChanged?.Invoke();
                RisePropertyChangedEvents();
            }
        }

        public double Cost
        {
            get { return GetValue<double>(); }
            set
            {
                if (Equals(Cost, value)) return;

                SetValue(value);
                CostChanged?.Invoke();
                RisePropertyChangedEvents();
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

        public virtual double Price => Product.GetPrice(PriceDate);

        public virtual bool HasBlocksWithoutPrice => Product.GetBlocksWithoutAnyPrice().Any();

        public string PriceErrors
        {
            get
            {
                var blocks = Product.GetBlocksWithoutAnyPrice();
                string result = string.Empty;
                foreach (var block in blocks)
                {
                    result += $"{block} has no price!!!; ";
                }

                blocks = Product.GetBlocksWithoutActualPriceOnDate(PriceDate);
                foreach (var block in blocks)
                {
                    result += $"{block} has no actual price; ";
                }
                return result;
            }
        }

        public double MarginalIncome
        {
            get => (Math.Abs(Cost) > 0.0001) ? 100 * (Cost - Price) / Cost : 0;
            set
            {
                if (Equals(MarginalIncome, value)) return;
                if (Math.Abs(value - 100) < 0.001) return;
                Cost = Price / (100 - value) * 100;
                OnPropertyChanged();
            }
        }

        protected void RisePropertyChangedEvents()
        {
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(MarginalIncome));
            OnPropertyChanged(nameof(PriceErrors));
            OnPropertyChanged(nameof(HasBlocksWithoutPrice));
        }
    }

    public abstract class ProductCostDependentProductsWrapper<T> : ProductCostWrapper<T>, IProductCostDependentProductsWrapper
        where T : class, IUnit
    {
        private IValidatableChangeTrackingCollection<ProductDependentWrapper> _dependentProducts;

        protected ProductCostDependentProductsWrapper(T model) : base(model)
        {
        }

        public IValidatableChangeTrackingCollection<ProductDependentWrapper> DependentProducts
        {
            get => _dependentProducts;
            protected set
            {
                _dependentProducts = value;
                value.CollectionChanged += OnDependentProductsCollectionChanged;
                value.ForEach(productDependentWrapper => productDependentWrapper.PropertyChanged += OnDependentProductPropertyChanged);
            }
        }

        public override double Price => Product.GetPrice(PriceDate) + 
            DependentProducts.Sum(x => x.Product.GetPrice(PriceDate) * x.Amount);

        public override bool HasBlocksWithoutPrice => Product.GetBlocksWithoutAnyPrice().Any() ||
            DependentProducts.Any(x => x.Product.GetBlocksWithoutAnyPrice().Any());

        private void OnDependentProductsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            DependentProducts.RemovedItems.ForEach(productDependentWrapper => productDependentWrapper.PropertyChanged -= OnDependentProductPropertyChanged);
            DependentProducts.AddedItems.ForEach(productDependentWrapper => productDependentWrapper.PropertyChanged += OnDependentProductPropertyChanged);
            RisePropertyChangedEvents();
        }

        private void OnDependentProductPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RisePropertyChangedEvents();
        }
    }
}