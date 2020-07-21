using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectUnit : WrapperBase<SalesUnit>, IProjectUnit
    {
        /// <summary>
        /// Можно ли удалить этот юнит
        /// </summary>
        public bool CanRemove => this.Model.Order == null;

        #region SimpleProperties

        public double Cost
        {
            get { return Model.Cost; }
            set { SetValue(value); }
        }
        public double CostOriginalValue => GetOriginalValue<double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public int ProductionTerm
        {
            get { return Model.ProductionTerm; }
            set { SetValue(value); }
        }
        public int ProductionTermOriginalValue => GetOriginalValue<int>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));

        public DateTime DeliveryDateExpected
        {
            get { return Model.DeliveryDateExpected; }
            set { SetValue(value); }
        }
        public DateTime DeliveryDateExpectedOriginalValue => GetOriginalValue<DateTime>(nameof(DeliveryDateExpected));
        public bool DeliveryDateExpectedIsChanged => GetIsChanged(nameof(DeliveryDateExpected));

        public double? CostDelivery
        {
            get { return Model.CostDelivery; }
            set { SetValue(value); }
        }
        public double? CostDeliveryOriginalValue => GetOriginalValue<double?>(nameof(CostDelivery));
        public bool CostDeliveryIsChanged => GetIsChanged(nameof(CostDelivery));

        #endregion

        #region ComplexProperties

        public FacilityWrapper Facility
        {
            get { return GetWrapper<FacilityWrapper>(); }
            set { SetComplexValue<Facility, FacilityWrapper>(Facility, value); }
        }

        public ProductWrapper Product
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }

        public PaymentConditionSetWrapper PaymentConditionSet
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(PaymentConditionSet, value); }
        }

        public CompanyWrapper Producer
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Producer, value); }
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; private set; }

        #endregion

        public ProjectUnit(SalesUnit model) : base(model) { }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(this.Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));
            InitializeComplexProperty(nameof(this.Product), Model.Product == null ? null : new ProductWrapper(Model.Product));
            InitializeComplexProperty(nameof(this.PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));
            InitializeComplexProperty(nameof(this.Producer), Model.Producer == null ? null : new CompanyWrapper(Model.Producer));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.ProductsIncluded == null) throw new ArgumentException("ProductsIncluded cannot be null");
            ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedWrapper>(Model.ProductsIncluded.Select(e => new ProductIncludedWrapper(e)));
            RegisterCollection(ProductsIncluded, Model.ProductsIncluded);
        }

        public double Price { get; set; }
        public double FixedCost { get; set; }
    }

}