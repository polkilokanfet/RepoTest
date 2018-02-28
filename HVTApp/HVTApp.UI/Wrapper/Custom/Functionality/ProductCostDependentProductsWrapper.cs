using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Wrapper
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

        protected void RisePropertyChangedEvents()
        {
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(MarginalIncome));
            OnPropertyChanged(nameof(PriceErrors));
            OnPropertyChanged(nameof(HasBlocksWithoutPrice));
        }
    }

    public abstract class ProductCostDependentProductsWrapper<T> : ProductCostWrapper<T>, IProductCostDependentProductsWrapper
        where T : class, IProductCostDependentProducts
    {
        private IValidatableChangeTrackingCollection<ProductDependentWrapper> _dependentProducts;

        protected ProductCostDependentProductsWrapper(T model) : base(model)
        {
        }

        public IValidatableChangeTrackingCollection<ProductDependentWrapper> DependentProducts
        {
            get { return _dependentProducts; }
            protected set
            {
                _dependentProducts = value;
                value.CollectionChanged += OnDependentProductsCollectionChanged;
                value.ForEach(x => x.PropertyChanged += OnDependentProductPropertyChanged);
            }
        }

        public override double Price => Product.GetPrice(PriceDate) +
            DependentProducts.Where(x => !x.IsIndependent).Sum(x => x.Product.GetPrice(PriceDate) * x.Amount);

        public override bool HasBlocksWithoutPrice => Product.GetBlocksWithoutAnyPrice().Any() ||
            DependentProducts.Any(x => x.Product.GetBlocksWithoutAnyPrice().Any());

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
    }
}