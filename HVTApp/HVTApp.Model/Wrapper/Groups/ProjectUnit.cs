using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.Model.Wrapper.Groups
{
    public class ProjectUnit : WrapperBase<SalesUnit>, IWrapperGroup<SalesUnit>
    {
        /// <summary>
        /// ����� �� ������� ���� ����
        /// </summary>
        public bool CanRemove => this.Model.Order == null;

        #region SimpleProperties

        public DateTime OrderInTakeDate => Model.OrderInTakeDate;

        public string Comment
        {
            get => Model.Comment;
            set => SetValue(value);
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Cost));


        public double Cost
        {
            get => Model.Cost;
            set => SetValue(value);
        }
        public double CostOriginalValue => GetOriginalValue<double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public int ProductionTerm
        {
            get => Model.ProductionTerm;
            set => SetValue(value);
        }
        public int ProductionTermOriginalValue => GetOriginalValue<int>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));

        public DateTime DeliveryDateExpected
        {
            get => Model.DeliveryDateExpected;
            set => SetValue(value);
        }
        public DateTime DeliveryDateExpectedOriginalValue => GetOriginalValue<DateTime>(nameof(DeliveryDateExpected));
        public bool DeliveryDateExpectedIsChanged => GetIsChanged(nameof(DeliveryDateExpected));

        public double? CostDelivery
        {
            get => Model.CostDelivery;
            set => SetValue(value);
        }
        public double? CostDeliveryOriginalValue => GetOriginalValue<double?>(nameof(CostDelivery));
        public bool CostDeliveryIsChanged => GetIsChanged(nameof(CostDelivery));


        public DateTime SignalToStartProduction
        {
            set => SetValue(value);
        }

        public DateTime EndProductionDateCalculated => Model.EndProductionDateCalculated;

        #endregion

        #region ComplexProperties

        public ProjectSimpleWrapper Project
        {
            get { return GetWrapper<ProjectSimpleWrapper>(); }
            set { SetComplexValue<Project, ProjectSimpleWrapper>(Project, value); }
        }

        public FacilitySimpleWrapper Facility
        {
            get { return GetWrapper<FacilitySimpleWrapper>(); }
            set { SetComplexValue<Facility, FacilitySimpleWrapper>(Facility, value); }
        }

        public ProductSimpleWrapper Product
        {
            get { return GetWrapper<ProductSimpleWrapper>(); }
            set { SetComplexValue<Product, ProductSimpleWrapper>(Product, value); }
        }

        public PaymentConditionSetSimpleWrapper PaymentConditionSet
        {
            get { return GetWrapper<PaymentConditionSetSimpleWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetSimpleWrapper>(PaymentConditionSet, value); }
        }

        public CompanySimpleWrapper Producer
        {
            get { return GetWrapper<CompanySimpleWrapper>(); }
            set => SetComplexValue<Company, CompanySimpleWrapper>(Producer, value);
        }

        public SpecificationSimpleWrapper Specification
        {
            get { return GetWrapper<SpecificationSimpleWrapper>(); }
            set { SetComplexValue<Specification, SpecificationSimpleWrapper>(Specification, value); }
        }

        public ProductType ProductType => Product.Model.ProductType;

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<ProductIncludedSimpleWrapper> ProductsIncluded { get; private set; }

        #endregion

        public ProjectUnit(SalesUnit model) : base(model) { }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(this.Project), Model.Project == null ? null : new ProjectSimpleWrapper(Model.Project));
            InitializeComplexProperty(nameof(this.Facility), Model.Facility == null ? null : new FacilitySimpleWrapper(Model.Facility));
            InitializeComplexProperty(nameof(this.Product), Model.Product == null ? null : new ProductSimpleWrapper(Model.Product));
            InitializeComplexProperty(nameof(this.PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetSimpleWrapper(Model.PaymentConditionSet));
            InitializeComplexProperty(nameof(this.Producer), Model.Producer == null ? null : new CompanySimpleWrapper(Model.Producer));
            InitializeComplexProperty(nameof(this.Specification), Model.Specification == null ? null : new SpecificationSimpleWrapper(Model.Specification));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.ProductsIncluded == null) throw new ArgumentException("ProductsIncluded cannot be null");
            ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedSimpleWrapper>(Model.ProductsIncluded.Select(productIncluded => new ProductIncludedSimpleWrapper(productIncluded)));
            RegisterCollection(ProductsIncluded, Model.ProductsIncluded);
        }

        public double Price { get; set; }
        public double FixedCost { get; set; }
    }
}