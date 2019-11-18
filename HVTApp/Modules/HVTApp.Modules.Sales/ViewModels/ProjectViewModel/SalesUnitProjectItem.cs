using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitProjectItem : WrapperBase<SalesUnit>
    {
        public SalesUnitProjectItem(SalesUnit model) : base(model) { }

        #region SimpleProperties

        public int Amount { get; } = 1;

        public double Cost
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }
        public double CostOriginalValue => GetOriginalValue<double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));


        public double? Price
        {
            get { return GetValue<double?>(); }
            set { SetValue(value); }
        }
        public double? PriceOriginalValue => GetOriginalValue<double?>(nameof(Price));
        public bool PriceIsChanged => GetIsChanged(nameof(Price));


        public int ProductionTerm
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public int ProductionTermOriginalValue => GetOriginalValue<int>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));


        public DateTime DeliveryDateExpected
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
        public DateTime DeliveryDateExpectedOriginalValue => GetOriginalValue<DateTime>(nameof(DeliveryDateExpected));
        public bool DeliveryDateExpectedIsChanged => GetIsChanged(nameof(DeliveryDateExpected));

        public double? CostDelivery
        {
            get { return GetValue<double?>(); }
            set { SetValue(value); }
        }
        public double? CostDeliveryOriginalValue => GetOriginalValue<double?>(nameof(CostDelivery));
        public bool CostDeliveryIsChanged => GetIsChanged(nameof(CostDelivery));


        public bool CostDeliveryIncluded
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool CostDeliveryIncludedOriginalValue => GetOriginalValue<bool>(nameof(CostDeliveryIncluded));
        public bool CostDeliveryIncludedIsChanged => GetIsChanged(nameof(CostDeliveryIncluded));

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


        public ProjectWrapper Project
        {
            get { return GetWrapper<ProjectWrapper>(); }
            set { SetComplexValue<Project, ProjectWrapper>(Project, value); }
        }


        public CompanyWrapper Producer
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Producer, value); }
        }


        public SpecificationWrapper Specification
        {
            get { return GetWrapper<SpecificationWrapper>(); }
            set { SetComplexValue<Specification, SpecificationWrapper>(Specification, value); }
        }


        public PenaltyWrapper Penalty
        {
            get { return GetWrapper<PenaltyWrapper>(); }
            set { SetComplexValue<Penalty, PenaltyWrapper>(Penalty, value); }
        }


        public AddressWrapper AddressDelivery
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(AddressDelivery, value); }
        }


        public StructureCostsWrapper StructureCosts
        {
            get { return GetWrapper<StructureCostsWrapper>(); }
            set { SetComplexValue<StructureCosts, StructureCostsWrapper>(StructureCosts, value); }
        }


        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; private set; }

        public IValidatableChangeTrackingCollection<LosingReasonWrapper> LosingReasons { get; private set; }

        public IValidatableChangeTrackingCollection<BankGuaranteeWrapper> BankGuarantees { get; private set; }

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));
            InitializeComplexProperty(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));
            InitializeComplexProperty(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));
            InitializeComplexProperty(nameof(Project), Model.Project == null ? null : new ProjectWrapper(Model.Project));
            InitializeComplexProperty(nameof(Producer), Model.Producer == null ? null : new CompanyWrapper(Model.Producer));
            InitializeComplexProperty(nameof(Specification), Model.Specification == null ? null : new SpecificationWrapper(Model.Specification));
            InitializeComplexProperty(nameof(Penalty), Model.Penalty == null ? null : new PenaltyWrapper(Model.Penalty));
            InitializeComplexProperty(nameof(AddressDelivery), Model.AddressDelivery == null ? null : new AddressWrapper(Model.AddressDelivery));
            InitializeComplexProperty(nameof(StructureCosts), Model.StructureCosts == null ? null : new StructureCostsWrapper(Model.StructureCosts));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.ProductsIncluded == null) throw new ArgumentException("ProductsIncluded cannot be null");
            ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedWrapper>(Model.ProductsIncluded.Select(e => new ProductIncludedWrapper(e)));
            RegisterCollection(ProductsIncluded, Model.ProductsIncluded);

            if (Model.LosingReasons == null) throw new ArgumentException("LosingReasons cannot be null");
            LosingReasons = new ValidatableChangeTrackingCollection<LosingReasonWrapper>(Model.LosingReasons.Select(e => new LosingReasonWrapper(e)));
            RegisterCollection(LosingReasons, Model.LosingReasons);

            if (Model.BankGuarantees == null) throw new ArgumentException("BankGuarantees cannot be null");
            BankGuarantees = new ValidatableChangeTrackingCollection<BankGuaranteeWrapper>(Model.BankGuarantees.Select(e => new BankGuaranteeWrapper(e)));
            RegisterCollection(BankGuarantees, Model.BankGuarantees);
        }

    }
}