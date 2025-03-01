using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Comparers;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Price;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitCalculatedParts : BindableBase
    {
        private readonly IProjectUnit _projectUnit;


        private double CostDelivery
        {
            get
            {
                double result = 0;
                if (_projectUnit.CostDelivery.HasValue)
                    result = _projectUnit.CostDelivery.Value;

                if (_projectUnit is ProjectUnitGroup projectUnitGroup)
                    result /= projectUnitGroup.Amount;

                return result;
            }
        }

        /// <summary>
        /// Минимально возможная цена единицы оборудования (продукты с фиксированной ценой + стоимость доставки)
        /// </summary>
        private double CostMin => _projectUnit.Price.SumFixedTotal + CostDelivery;

        public double? MarginalIncome
        {
            get => _projectUnit.Cost - CostMin <= 0
                ? default(double?)
                : (1.0 - _projectUnit.Price.SumPriceTotal / (_projectUnit.Cost - CostMin)) * 100.0;
            set
            {
                if (value.HasValue == false || value >= 100) 
                    return;

                var marginalIncome = value.Value;
                _projectUnit.Cost = _projectUnit.Price.SumPriceTotal / (1.0 - marginalIncome / 100.0) + CostMin;
                RaisePropertyChanged();
            }
        }

        public ProjectUnitCalculatedParts(IProjectUnit projectUnit)
        {
            _projectUnit = projectUnit;
        }
    }

    public class ProjectUnit : WrapperBase<SalesUnit>, IProjectUnit
    {
        public Specification Specification => this.Model.Specification;

        #region SimpleProperties

        public double Cost
        {
            get => Model.Cost;
            set
            {
                if (value < 0) return;
                SetValue(value);
                RaisePropertyChanged(nameof(CalculatedParts));
            }
        }

        public double? CostDelivery
        {
            get => Model.CostDelivery;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public DateTime DeliveryDateExpected
        {
            get => Model.DeliveryDateExpected;
            set => SetValue(value);
        }

        public bool IsRemoved
        {
            get => Model.IsRemoved;
            set => SetValue(value);
        }

        public int ProductionTerm
        {
            get => Model.ProductionTerm;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public string Comment
        {
            get => Model.Comment;
            set => SetValue(value);
        }

        #endregion

        #region ComplexProperties

        public FacilityEmptyWrapper Facility
        {
            get => GetWrapper<FacilityEmptyWrapper>();
            set => SetComplexValue<Facility, FacilityEmptyWrapper>(Facility, value);
        }

        public ProductEmptyWrapper Product
        {
            get => GetWrapper<ProductEmptyWrapper>();
            set => SetComplexValue<Product, ProductEmptyWrapper>(Product, value);
        }

        public PaymentConditionSetEmptyWrapper PaymentConditionSet
        {
            get => GetWrapper<PaymentConditionSetEmptyWrapper>();
            set => SetComplexValue<PaymentConditionSet, PaymentConditionSetEmptyWrapper>(PaymentConditionSet, value);
        }

        public CompanyEmptyWrapper Producer
        {
            get => GetWrapper<CompanyEmptyWrapper>();
            set => SetComplexValue<Company, CompanyEmptyWrapper>(Producer, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// Включенные продукты
        /// </summary>
        public IValidatableChangeTrackingCollection<ProjectUnitProductIncluded> ProductsIncluded { get; private set; }

        #endregion

        public IEnumerable<ProjectUnitProductIncludedGroup> ProductsIncludedGroups =>
            this.ProductsIncluded
                .GroupBy(x => x, new ProjectUnitProductIncluded.ProjectUnitProductIncludedComparer())
                .Select(x => new ProjectUnitProductIncludedGroup(x))
                .OrderBy(x => x.Name);

        public Price Price => GlobalAppProperties.PriceService.GetPrice(this.Model, this.Model.RealizationDateCalculated, true);
        public ProjectUnitCalculatedParts CalculatedParts { get; }
        public IEnumerable<Price> Prices => new List<Price> { this.Price };

        #region Ctors

        /// <summary>
        /// Для создания по образцу
        /// </summary>
        public ProjectUnit(IProjectUnit projectUnit) : this(new SalesUnit())
        {
            this.CopyProps(projectUnit);
        }

        /// <summary>
        /// Для редактирования
        /// </summary>
        /// <param name="model"></param>
        public ProjectUnit(SalesUnit model) : base(model)
        {
            this.ProductsIncluded.CollectionChanged +=
                (sender, args) => RaisePropertyChanged(nameof(ProductsIncludedGroups));

            CalculatedParts = new ProjectUnitCalculatedParts(this);

            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Cost))
                {
                    RaisePropertyChanged(nameof(CalculatedParts));
                }
                else if (args.PropertyName == nameof(CostDelivery))
                {
                    RaisePropertyChanged(nameof(CalculatedParts));
                }
            };

        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Facility), Model.Facility == null ? null : new FacilityEmptyWrapper(Model.Facility));
            InitializeComplexProperty(nameof(Product), Model.Product == null ? null : new ProductEmptyWrapper(Model.Product));
            InitializeComplexProperty(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetEmptyWrapper(Model.PaymentConditionSet));
            InitializeComplexProperty(nameof(Producer), Model.Producer == null ? null : new CompanyEmptyWrapper(Model.Producer));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.ProductsIncluded == null) throw new ArgumentException($"{nameof(Model.ProductsIncluded)} cannot be null");
            ProductsIncluded = new ValidatableChangeTrackingCollection<ProjectUnitProductIncluded>(Model.ProductsIncluded.Select(e => new ProjectUnitProductIncluded(e)));
            RegisterCollection(ProductsIncluded, Model.ProductsIncluded);
        }

        #endregion

        public void CopyProps(IProjectUnit projectUnit)
        {
            if (projectUnit == null) return;

            Cost = projectUnit.Cost;
            Comment = projectUnit.Comment;
            CostDelivery = projectUnit.CostDelivery;
            DeliveryDateExpected = projectUnit.DeliveryDateExpected;

            Facility = projectUnit.Facility;
            Product = projectUnit.Product;
            PaymentConditionSet = projectUnit.PaymentConditionSet;
            Producer = projectUnit.Producer;
        }

        public void RemoveProductIncluded(ProjectUnitProductIncluded productIncluded)
        {
            this.ProductsIncluded.Remove(productIncluded);
            RaisePropertyChanged(nameof(ProjectUnit.ProductsIncludedGroups));
        }

        public bool HasSameGroup(SalesUnit other)
        {
            return (new ProjectUnitComparer()).Equals(this, new ProjectUnit(other));
        }

        public bool HasSameGroup(ProjectUnit other)
        {
            return (new ProjectUnitComparer()).Equals(this, other);
        }

        public class ProjectUnitComparer : IEqualityComparer<IProjectUnit>
        {
            public virtual bool Equals(IProjectUnit x, IProjectUnit y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                if (!Equals(x.Cost, y.Cost)) return false;
                if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
                if (!Equals(x.CostDelivery, y.CostDelivery)) return false;
                if (!Equals(x.Comment, y.Comment)) return false;
                if (!Equals(x.DeliveryDateExpected, y.DeliveryDateExpected)) return false;
                if (!Equals(x.Product?.Model.Id, y.Product?.Model.Id)) return false;
                if (!Equals(x.Facility?.Model.Id, y.Facility?.Model.Id)) return false;
                if (!Equals(x.PaymentConditionSet?.Model.Id, y.PaymentConditionSet?.Model.Id)) return false;
                if (!Equals(x.Producer?.Model.Id, y.Producer?.Model.Id)) return false;
                if (!Equals(x.Specification?.Id, y.Specification?.Id)) return false;

                var productsInclX = x.ProductsIncludedGroups
                    .SelectMany(productIncludedGroup => productIncludedGroup.Items)
                    .Select(p => new ProductAmount(p.Model.Product.Id, p.Model.Amount, p.CustomFixedPrice))
                    .ToList();
                var productsInclY = y.ProductsIncludedGroups
                    .SelectMany(productIncludedGroup => productIncludedGroup.Items)
                    .Select(p => new ProductAmount(p.Model.Product.Id, p.Model.Amount, p.CustomFixedPrice))
                    .ToList();

                if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
                if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;

                return true;
            }

            public int GetHashCode(IProjectUnit obj)
            {
                return 0;
            }
        }
    }
}