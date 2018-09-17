using System;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
	public partial class CreateNewProductTaskWrapper : WrapperBase<CreateNewProductTask>
	{
	    public CreateNewProductTaskWrapper(CreateNewProductTask model) : base(model) { }

	
        #region SimpleProperties
        public System.String Designation
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
        public bool DesignationIsChanged => GetIsChanged(nameof(Designation));

        public System.String StructureCostNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String StructureCostNumberOriginalValue => GetOriginalValue<System.String>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

        }
	}

		public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
	{
	    public PaymentActualWrapper(PaymentActual model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Double Sum
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
        public bool SumIsChanged => GetIsChanged(nameof(Sum));

        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class PaymentPlannedWrapper : WrapperBase<PaymentPlanned>
	{
	    public PaymentPlannedWrapper(PaymentPlanned model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.DateTime DateCalculated
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateCalculatedOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateCalculated));
        public bool DateCalculatedIsChanged => GetIsChanged(nameof(DateCalculated));

        public System.Double Part
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PartOriginalValue => GetOriginalValue<System.Double>(nameof(Part));
        public bool PartIsChanged => GetIsChanged(nameof(Part));

        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public PaymentConditionWrapper Condition 
        {
            get { return GetWrapper<PaymentConditionWrapper>(); }
            set { SetComplexValue<PaymentCondition, PaymentConditionWrapper>(Condition, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<PaymentConditionWrapper>(nameof(Condition), Model.Condition == null ? null : new PaymentConditionWrapper(Model.Condition));

        }
	}

		public partial class ProductBlockIsServiceWrapper : WrapperBase<ProductBlockIsService>
	{
	    public ProductBlockIsServiceWrapper(ProductBlockIsService model) : base(model) { }

	
        #region SimpleProperties
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);

        }
	}

		public partial class ProductIncludedWrapper : WrapperBase<ProductIncluded>
	{
	    public ProductIncludedWrapper(ProductIncluded model) : base(model) { }

	
        #region SimpleProperties
        public System.Int32 Amount
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 AmountOriginalValue => GetOriginalValue<System.Int32>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

        }
	}

		public partial class ProductDesignationWrapper : WrapperBase<ProductDesignation>
	{
	    public ProductDesignationWrapper(ProductDesignation model) : base(model) { }

	
        #region SimpleProperties
        public System.String Designation
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
        public bool DesignationIsChanged => GetIsChanged(nameof(Designation));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);

        }
	}

		public partial class ProductTypeWrapper : WrapperBase<ProductType>
	{
	    public ProductTypeWrapper(ProductType model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class ProductTypeDesignationWrapper : WrapperBase<ProductTypeDesignation>
	{
	    public ProductTypeDesignationWrapper(ProductTypeDesignation model) : base(model) { }

	
        #region SimpleProperties
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProductTypeWrapper ProductType 
        {
            get { return GetWrapper<ProductTypeWrapper>(); }
            set { SetComplexValue<ProductType, ProductTypeWrapper>(ProductType, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductTypeWrapper>(nameof(ProductType), Model.ProductType == null ? null : new ProductTypeWrapper(Model.ProductType));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);

        }
	}

		public partial class ProjectTypeWrapper : WrapperBase<ProjectType>
	{
	    public ProjectTypeWrapper(ProjectType model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class CommonOptionWrapper : WrapperBase<CommonOption>
	{
	    public CommonOptionWrapper(CommonOption model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Guid OurCompanyId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid OurCompanyIdOriginalValue => GetOriginalValue<System.Guid>(nameof(OurCompanyId));
        public bool OurCompanyIdIsChanged => GetIsChanged(nameof(OurCompanyId));

        public System.Int32 ActualPriceTerm
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ActualPriceTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ActualPriceTerm));
        public bool ActualPriceTermIsChanged => GetIsChanged(nameof(ActualPriceTerm));

        public System.Int32 StandartTermFromStartToEndProduction
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 StandartTermFromStartToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(StandartTermFromStartToEndProduction));
        public bool StandartTermFromStartToEndProductionIsChanged => GetIsChanged(nameof(StandartTermFromStartToEndProduction));

        public System.Int32 StandartTermFromPickToEndProduction
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 StandartTermFromPickToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(StandartTermFromPickToEndProduction));
        public bool StandartTermFromPickToEndProductionIsChanged => GetIsChanged(nameof(StandartTermFromPickToEndProduction));

        public System.Guid StandartPaymentsConditionSetId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid StandartPaymentsConditionSetIdOriginalValue => GetOriginalValue<System.Guid>(nameof(StandartPaymentsConditionSetId));
        public bool StandartPaymentsConditionSetIdIsChanged => GetIsChanged(nameof(StandartPaymentsConditionSetId));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class AddressWrapper : WrapperBase<Address>
	{
	    public AddressWrapper(Address model) : base(model) { }

	
        #region SimpleProperties
        public System.String Description
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DescriptionOriginalValue => GetOriginalValue<System.String>(nameof(Description));
        public bool DescriptionIsChanged => GetIsChanged(nameof(Description));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public LocalityWrapper Locality 
        {
            get { return GetWrapper<LocalityWrapper>(); }
            set { SetComplexValue<Locality, LocalityWrapper>(Locality, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<LocalityWrapper>(nameof(Locality), Model.Locality == null ? null : new LocalityWrapper(Model.Locality));

        }
	}

		public partial class CountryWrapper : WrapperBase<Country>
	{
	    public CountryWrapper(Country model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class DistrictWrapper : WrapperBase<District>
	{
	    public DistrictWrapper(District model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public CountryWrapper Country 
        {
            get { return GetWrapper<CountryWrapper>(); }
            set { SetComplexValue<Country, CountryWrapper>(Country, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CountryWrapper>(nameof(Country), Model.Country == null ? null : new CountryWrapper(Model.Country));

        }
	}

		public partial class LocalityWrapper : WrapperBase<Locality>
	{
	    public LocalityWrapper(Locality model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Boolean IsCountryCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsCountryCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsCountryCapital));
        public bool IsCountryCapitalIsChanged => GetIsChanged(nameof(IsCountryCapital));

        public System.Boolean IsDistrictCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsDistrictCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDistrictCapital));
        public bool IsDistrictCapitalIsChanged => GetIsChanged(nameof(IsDistrictCapital));

        public System.Boolean IsRegionCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsRegionCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsRegionCapital));
        public bool IsRegionCapitalIsChanged => GetIsChanged(nameof(IsRegionCapital));

        public System.Nullable<System.Double> DistanceToEkb
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> DistanceToEkbOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(DistanceToEkb));
        public bool DistanceToEkbIsChanged => GetIsChanged(nameof(DistanceToEkb));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public LocalityTypeWrapper LocalityType 
        {
            get { return GetWrapper<LocalityTypeWrapper>(); }
            set { SetComplexValue<LocalityType, LocalityTypeWrapper>(LocalityType, value); }
        }

	    public RegionWrapper Region 
        {
            get { return GetWrapper<RegionWrapper>(); }
            set { SetComplexValue<Region, RegionWrapper>(Region, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<LocalityTypeWrapper>(nameof(LocalityType), Model.LocalityType == null ? null : new LocalityTypeWrapper(Model.LocalityType));

            InitializeComplexProperty<RegionWrapper>(nameof(Region), Model.Region == null ? null : new RegionWrapper(Model.Region));

        }
	}

		public partial class LocalityTypeWrapper : WrapperBase<LocalityType>
	{
	    public LocalityTypeWrapper(LocalityType model) : base(model) { }

	
        #region SimpleProperties
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));

        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class RegionWrapper : WrapperBase<Region>
	{
	    public RegionWrapper(Region model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public DistrictWrapper District 
        {
            get { return GetWrapper<DistrictWrapper>(); }
            set { SetComplexValue<District, DistrictWrapper>(District, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<DistrictWrapper>(nameof(District), Model.District == null ? null : new DistrictWrapper(Model.District));

        }
	}

		public partial class CalculatePriceTaskWrapper : WrapperBase<CalculatePriceTask>
	{
	    public CalculatePriceTaskWrapper(CalculatePriceTask model) : base(model) { }

	
        #region SimpleProperties
        public HVTApp.Model.POCOs.CalculatePriceTaskStatus Status
        {
          get { return GetValue<HVTApp.Model.POCOs.CalculatePriceTaskStatus>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.CalculatePriceTaskStatus StatusOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.CalculatePriceTaskStatus>(nameof(Status));
        public bool StatusIsChanged => GetIsChanged(nameof(Status));

        public System.Double Sum
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
        public bool SumIsChanged => GetIsChanged(nameof(Sum));

        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProductBlockWrapper ProductBlock 
        {
            get { return GetWrapper<ProductBlockWrapper>(); }
            set { SetComplexValue<ProductBlock, ProductBlockWrapper>(ProductBlock, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProjectWrapper> Projects { get; private set; }

        public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }

        public IValidatableChangeTrackingCollection<SpecificationWrapper> Specifications { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductBlockWrapper>(nameof(ProductBlock), Model.ProductBlock == null ? null : new ProductBlockWrapper(Model.ProductBlock));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Projects == null) throw new ArgumentException("Projects cannot be null");
          Projects = new ValidatableChangeTrackingCollection<ProjectWrapper>(Model.Projects.Select(e => new ProjectWrapper(e)));
          RegisterCollection(Projects, Model.Projects);

          if (Model.Offers == null) throw new ArgumentException("Offers cannot be null");
          Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(Model.Offers.Select(e => new OfferWrapper(e)));
          RegisterCollection(Offers, Model.Offers);

          if (Model.Specifications == null) throw new ArgumentException("Specifications cannot be null");
          Specifications = new ValidatableChangeTrackingCollection<SpecificationWrapper>(Model.Specifications.Select(e => new SpecificationWrapper(e)));
          RegisterCollection(Specifications, Model.Specifications);

        }
	}

		public partial class SumWrapper : WrapperBase<Sum>
	{
	    public SumWrapper(Sum model) : base(model) { }

	
        #region SimpleProperties
        public HVTApp.Model.POCOs.SumType Type
        {
          get { return GetValue<HVTApp.Model.POCOs.SumType>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.SumType TypeOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.SumType>(nameof(Type));
        public bool TypeIsChanged => GetIsChanged(nameof(Type));

        public HVTApp.Model.POCOs.Currency Currency
        {
          get { return GetValue<HVTApp.Model.POCOs.Currency>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.Currency CurrencyOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Currency>(nameof(Currency));
        public bool CurrencyIsChanged => GetIsChanged(nameof(Currency));

        public System.Decimal Value
        {
          get { return GetValue<System.Decimal>(); }
          set { SetValue(value); }
        }
        public System.Decimal ValueOriginalValue => GetOriginalValue<System.Decimal>(nameof(Value));
        public bool ValueIsChanged => GetIsChanged(nameof(Value));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class CurrencyExchangeRateWrapper : WrapperBase<CurrencyExchangeRate>
	{
	    public CurrencyExchangeRateWrapper(CurrencyExchangeRate model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public HVTApp.Model.POCOs.Currency FirstCurrency
        {
          get { return GetValue<HVTApp.Model.POCOs.Currency>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.Currency FirstCurrencyOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Currency>(nameof(FirstCurrency));
        public bool FirstCurrencyIsChanged => GetIsChanged(nameof(FirstCurrency));

        public HVTApp.Model.POCOs.Currency SecondCurrency
        {
          get { return GetValue<HVTApp.Model.POCOs.Currency>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.Currency SecondCurrencyOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Currency>(nameof(SecondCurrency));
        public bool SecondCurrencyIsChanged => GetIsChanged(nameof(SecondCurrency));

        public System.Double ExchangeRate
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double ExchangeRateOriginalValue => GetOriginalValue<System.Double>(nameof(ExchangeRate));
        public bool ExchangeRateIsChanged => GetIsChanged(nameof(ExchangeRate));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class DescribeProductBlockTaskWrapper : WrapperBase<DescribeProductBlockTask>
	{
	    public DescribeProductBlockTaskWrapper(DescribeProductBlockTask model) : base(model) { }

	
        #region SimpleProperties
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProductBlockWrapper ProductBlock 
        {
            get { return GetWrapper<ProductBlockWrapper>(); }
            set { SetComplexValue<ProductBlock, ProductBlockWrapper>(ProductBlock, value); }
        }

	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductBlockWrapper>(nameof(ProductBlock), Model.ProductBlock == null ? null : new ProductBlockWrapper(Model.ProductBlock));

            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

        }
	}

		public partial class NoteWrapper : WrapperBase<Note>
	{
	    public NoteWrapper(Note model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.String Text
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String TextOriginalValue => GetOriginalValue<System.String>(nameof(Text));
        public bool TextIsChanged => GetIsChanged(nameof(Text));

        public System.Boolean IsImportant
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsImportantOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsImportant));
        public bool IsImportantIsChanged => GetIsChanged(nameof(IsImportant));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
	{
	    public OfferUnitWrapper(OfferUnit model) : base(model) { }

	
        #region SimpleProperties
        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public System.Nullable<System.Int32> ProductionTerm
        {
          get { return GetValue<System.Nullable<System.Int32>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Int32> ProductionTermOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public OfferWrapper Offer 
        {
            get { return GetWrapper<OfferWrapper>(); }
            set { SetComplexValue<Offer, OfferWrapper>(Offer, value); }
        }

	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }

	    public FacilityWrapper Facility 
        {
            get { return GetWrapper<FacilityWrapper>(); }
            set { SetComplexValue<Facility, FacilityWrapper>(Facility, value); }
        }

	    public PaymentConditionSetWrapper PaymentConditionSet 
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(PaymentConditionSet, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<OfferWrapper>(nameof(Offer), Model.Offer == null ? null : new OfferWrapper(Model.Offer));

            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

            InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));

            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.ProductsIncluded == null) throw new ArgumentException("ProductsIncluded cannot be null");
          ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedWrapper>(Model.ProductsIncluded.Select(e => new ProductIncludedWrapper(e)));
          RegisterCollection(ProductsIncluded, Model.ProductsIncluded);

        }
	}

		public partial class PaymentConditionSetWrapper : WrapperBase<PaymentConditionSet>
	{
	    public PaymentConditionSetWrapper(PaymentConditionSet model) : base(model) { }

	
        #region SimpleProperties
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentConditions { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.PaymentConditions == null) throw new ArgumentException("PaymentConditions cannot be null");
          PaymentConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentConditions.Select(e => new PaymentConditionWrapper(e)));
          RegisterCollection(PaymentConditions, Model.PaymentConditions);

        }
	}

		public partial class ProductBlockWrapper : WrapperBase<ProductBlock>
	{
	    public ProductBlockWrapper(ProductBlock model) : base(model) { }

	
        #region SimpleProperties
        public System.String Designation
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
        public bool DesignationIsChanged => GetIsChanged(nameof(Designation));

        public System.String DesignationSpecial
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationSpecialOriginalValue => GetOriginalValue<System.String>(nameof(DesignationSpecial));
        public bool DesignationSpecialIsChanged => GetIsChanged(nameof(DesignationSpecial));

        public System.String StructureCostNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String StructureCostNumberOriginalValue => GetOriginalValue<System.String>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));

        public System.String Design
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignOriginalValue => GetOriginalValue<System.String>(nameof(Design));
        public bool DesignIsChanged => GetIsChanged(nameof(Design));

        public System.Boolean IsService
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsServiceOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsService));
        public bool IsServiceIsChanged => GetIsChanged(nameof(IsService));

        public System.Double Weight
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double WeightOriginalValue => GetOriginalValue<System.Double>(nameof(Weight));
        public bool WeightIsChanged => GetIsChanged(nameof(Weight));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

        public IValidatableChangeTrackingCollection<SumOnDateWrapper> Prices { get; private set; }

        #endregion

        #region GetProperties
        public System.Nullable<System.DateTime> LastPriceDate => GetValue<System.Nullable<System.DateTime>>(); 

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);

          if (Model.Prices == null) throw new ArgumentException("Prices cannot be null");
          Prices = new ValidatableChangeTrackingCollection<SumOnDateWrapper>(Model.Prices.Select(e => new SumOnDateWrapper(e)));
          RegisterCollection(Prices, Model.Prices);

        }
	}

		public partial class ProductDependentWrapper : WrapperBase<ProductDependent>
	{
	    public ProductDependentWrapper(ProductDependent model) : base(model) { }

	
        #region SimpleProperties
        public System.Guid MainProductId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid MainProductIdOriginalValue => GetOriginalValue<System.Guid>(nameof(MainProductId));
        public bool MainProductIdIsChanged => GetIsChanged(nameof(MainProductId));

        public System.Int32 Amount
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 AmountOriginalValue => GetOriginalValue<System.Int32>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

        }
	}

		public partial class ProductionTaskWrapper : WrapperBase<ProductionTask>
	{
	    public ProductionTaskWrapper(ProductionTask model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime DateTask
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateTaskOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateTask));
        public bool DateTaskIsChanged => GetIsChanged(nameof(DateTask));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
          SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.SalesUnits.Select(e => new SalesUnitWrapper(e)));
          RegisterCollection(SalesUnits, Model.SalesUnits);

        }
	}

		public partial class SalesBlockWrapper : WrapperBase<SalesBlock>
	{
	    public SalesBlockWrapper(SalesBlock model) : base(model) { }

	
        #region SimpleProperties
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> ParentSalesUnits { get; private set; }

        public IValidatableChangeTrackingCollection<SalesUnitWrapper> ChildSalesUnits { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.ParentSalesUnits == null) throw new ArgumentException("ParentSalesUnits cannot be null");
          ParentSalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.ParentSalesUnits.Select(e => new SalesUnitWrapper(e)));
          RegisterCollection(ParentSalesUnits, Model.ParentSalesUnits);

          if (Model.ChildSalesUnits == null) throw new ArgumentException("ChildSalesUnits cannot be null");
          ChildSalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.ChildSalesUnits.Select(e => new SalesUnitWrapper(e)));
          RegisterCollection(ChildSalesUnits, Model.ChildSalesUnits);

        }
	}

		public partial class BankDetailsWrapper : WrapperBase<BankDetails>
	{
	    public BankDetailsWrapper(BankDetails model) : base(model) { }

	
        #region SimpleProperties
        public System.String BankName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String BankNameOriginalValue => GetOriginalValue<System.String>(nameof(BankName));
        public bool BankNameIsChanged => GetIsChanged(nameof(BankName));

        public System.String BankIdentificationCode
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String BankIdentificationCodeOriginalValue => GetOriginalValue<System.String>(nameof(BankIdentificationCode));
        public bool BankIdentificationCodeIsChanged => GetIsChanged(nameof(BankIdentificationCode));

        public System.String CorrespondentAccount
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CorrespondentAccountOriginalValue => GetOriginalValue<System.String>(nameof(CorrespondentAccount));
        public bool CorrespondentAccountIsChanged => GetIsChanged(nameof(CorrespondentAccount));

        public System.String CheckingAccount
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CheckingAccountOriginalValue => GetOriginalValue<System.String>(nameof(CheckingAccount));
        public bool CheckingAccountIsChanged => GetIsChanged(nameof(CheckingAccount));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class CompanyWrapper : WrapperBase<Company>
	{
	    public CompanyWrapper(Company model) : base(model) { }

	
        #region SimpleProperties
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));

        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));

        public System.String Inn
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String InnOriginalValue => GetOriginalValue<System.String>(nameof(Inn));
        public bool InnIsChanged => GetIsChanged(nameof(Inn));

        public System.String Kpp
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String KppOriginalValue => GetOriginalValue<System.String>(nameof(Kpp));
        public bool KppIsChanged => GetIsChanged(nameof(Kpp));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public CompanyFormWrapper Form 
        {
            get { return GetWrapper<CompanyFormWrapper>(); }
            set { SetComplexValue<CompanyForm, CompanyFormWrapper>(Form, value); }
        }

	    public CompanyWrapper ParentCompany 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(ParentCompany, value); }
        }

	    public AddressWrapper AddressLegal 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(AddressLegal, value); }
        }

	    public AddressWrapper AddressPost 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(AddressPost, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<BankDetailsWrapper> BankDetailsList { get; private set; }

        public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CompanyFormWrapper>(nameof(Form), Model.Form == null ? null : new CompanyFormWrapper(Model.Form));

            InitializeComplexProperty<CompanyWrapper>(nameof(ParentCompany), Model.ParentCompany == null ? null : new CompanyWrapper(Model.ParentCompany));

            InitializeComplexProperty<AddressWrapper>(nameof(AddressLegal), Model.AddressLegal == null ? null : new AddressWrapper(Model.AddressLegal));

            InitializeComplexProperty<AddressWrapper>(nameof(AddressPost), Model.AddressPost == null ? null : new AddressWrapper(Model.AddressPost));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.BankDetailsList == null) throw new ArgumentException("BankDetailsList cannot be null");
          BankDetailsList = new ValidatableChangeTrackingCollection<BankDetailsWrapper>(Model.BankDetailsList.Select(e => new BankDetailsWrapper(e)));
          RegisterCollection(BankDetailsList, Model.BankDetailsList);

          if (Model.ActivityFilds == null) throw new ArgumentException("ActivityFilds cannot be null");
          ActivityFilds = new ValidatableChangeTrackingCollection<ActivityFieldWrapper>(Model.ActivityFilds.Select(e => new ActivityFieldWrapper(e)));
          RegisterCollection(ActivityFilds, Model.ActivityFilds);

        }
	}

		public partial class CompanyFormWrapper : WrapperBase<CompanyForm>
	{
	    public CompanyFormWrapper(CompanyForm model) : base(model) { }

	
        #region SimpleProperties
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));

        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class DocumentsRegistrationDetailsWrapper : WrapperBase<DocumentsRegistrationDetails>
	{
	    public DocumentsRegistrationDetailsWrapper(DocumentsRegistrationDetails model) : base(model) { }

	
        #region SimpleProperties
        public System.String RegistrationNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String RegistrationNumberOriginalValue => GetOriginalValue<System.String>(nameof(RegistrationNumber));
        public bool RegistrationNumberIsChanged => GetIsChanged(nameof(RegistrationNumber));

        public System.DateTime RegistrationDate
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime RegistrationDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(RegistrationDate));
        public bool RegistrationDateIsChanged => GetIsChanged(nameof(RegistrationDate));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class EmployeesPositionWrapper : WrapperBase<EmployeesPosition>
	{
	    public EmployeesPositionWrapper(EmployeesPosition model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class FacilityTypeWrapper : WrapperBase<FacilityType>
	{
	    public FacilityTypeWrapper(FacilityType model) : base(model) { }

	
        #region SimpleProperties
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));

        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class ActivityFieldWrapper : WrapperBase<ActivityField>
	{
	    public ActivityFieldWrapper(ActivityField model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnum
        {
          get { return GetValue<HVTApp.Model.POCOs.ActivityFieldEnum>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnumOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.ActivityFieldEnum>(nameof(ActivityFieldEnum));
        public bool ActivityFieldEnumIsChanged => GetIsChanged(nameof(ActivityFieldEnum));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class ContractWrapper : WrapperBase<Contract>
	{
	    public ContractWrapper(Contract model) : base(model) { }

	
        #region SimpleProperties
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public CompanyWrapper Contragent 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Contragent, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CompanyWrapper>(nameof(Contragent), Model.Contragent == null ? null : new CompanyWrapper(Model.Contragent));

        }
	}

		public partial class MeasureWrapper : WrapperBase<Measure>
	{
	    public MeasureWrapper(Measure model) : base(model) { }

	
        #region SimpleProperties
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));

        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class ParameterWrapper : WrapperBase<Parameter>
	{
	    public ParameterWrapper(Parameter model) : base(model) { }

	
        #region SimpleProperties
        public System.String Value
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ValueOriginalValue => GetOriginalValue<System.String>(nameof(Value));
        public bool ValueIsChanged => GetIsChanged(nameof(Value));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ParameterGroupWrapper ParameterGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(ParameterGroup, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterRelationWrapper> ParameterRelations { get; private set; }

        #endregion

        #region GetProperties
        public System.Boolean IsOrigin => GetValue<System.Boolean>(); 

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(ParameterGroup), Model.ParameterGroup == null ? null : new ParameterGroupWrapper(Model.ParameterGroup));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.ParameterRelations == null) throw new ArgumentException("ParameterRelations cannot be null");
          ParameterRelations = new ValidatableChangeTrackingCollection<ParameterRelationWrapper>(Model.ParameterRelations.Select(e => new ParameterRelationWrapper(e)));
          RegisterCollection(ParameterRelations, Model.ParameterRelations);

        }
	}

		public partial class ParameterGroupWrapper : WrapperBase<ParameterGroup>
	{
	    public ParameterGroupWrapper(ParameterGroup model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public MeasureWrapper Measure 
        {
            get { return GetWrapper<MeasureWrapper>(); }
            set { SetComplexValue<Measure, MeasureWrapper>(Measure, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<MeasureWrapper>(nameof(Measure), Model.Measure == null ? null : new MeasureWrapper(Model.Measure));

        }
	}

		public partial class ProductRelationWrapper : WrapperBase<ProductRelation>
	{
	    public ProductRelationWrapper(ProductRelation model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Int32 ChildProductsAmount
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ChildProductsAmountOriginalValue => GetOriginalValue<System.Int32>(nameof(ChildProductsAmount));
        public bool ChildProductsAmountIsChanged => GetIsChanged(nameof(ChildProductsAmount));

        public System.Boolean IsUnique
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsUniqueOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsUnique));
        public bool IsUniqueIsChanged => GetIsChanged(nameof(IsUnique));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> ParentProductParameters { get; private set; }

        public IValidatableChangeTrackingCollection<ParameterWrapper> ChildProductParameters { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.ParentProductParameters == null) throw new ArgumentException("ParentProductParameters cannot be null");
          ParentProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.ParentProductParameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(ParentProductParameters, Model.ParentProductParameters);

          if (Model.ChildProductParameters == null) throw new ArgumentException("ChildProductParameters cannot be null");
          ChildProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.ChildProductParameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(ChildProductParameters, Model.ChildProductParameters);

        }
	}

		public partial class PersonWrapper : WrapperBase<Person>
	{
	    public PersonWrapper(Person model) : base(model) { }

	
        #region SimpleProperties
        public System.String Surname
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String SurnameOriginalValue => GetOriginalValue<System.String>(nameof(Surname));
        public bool SurnameIsChanged => GetIsChanged(nameof(Surname));

        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.String Patronymic
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String PatronymicOriginalValue => GetOriginalValue<System.String>(nameof(Patronymic));
        public bool PatronymicIsChanged => GetIsChanged(nameof(Patronymic));

        public System.Boolean IsMan
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsManOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsMan));
        public bool IsManIsChanged => GetIsChanged(nameof(IsMan));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class ParameterRelationWrapper : WrapperBase<ParameterRelation>
	{
	    public ParameterRelationWrapper(ParameterRelation model) : base(model) { }

	
        #region SimpleProperties
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> RequiredParameters { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.RequiredParameters == null) throw new ArgumentException("RequiredParameters cannot be null");
          RequiredParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.RequiredParameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(RequiredParameters, Model.RequiredParameters);

        }
	}

		public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
	{
	    public SalesUnitWrapper(SalesUnit model) : base(model) { }

	
        #region SimpleProperties
        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public System.Nullable<System.Int32> ProductionTerm
        {
          get { return GetValue<System.Nullable<System.Int32>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Int32> ProductionTermOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));

        public System.DateTime DeliveryDateExpected
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DeliveryDateExpectedOriginalValue => GetOriginalValue<System.DateTime>(nameof(DeliveryDateExpected));
        public bool DeliveryDateExpectedIsChanged => GetIsChanged(nameof(DeliveryDateExpected));

        public System.Nullable<System.DateTime> RealizationDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> RealizationDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RealizationDate));
        public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));

        public System.String OrderPosition
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String OrderPositionOriginalValue => GetOriginalValue<System.String>(nameof(OrderPosition));
        public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));

        public System.String SerialNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String SerialNumberOriginalValue => GetOriginalValue<System.String>(nameof(SerialNumber));
        public bool SerialNumberIsChanged => GetIsChanged(nameof(SerialNumber));

        public System.Nullable<System.Int32> AssembleTerm
        {
          get { return GetValue<System.Nullable<System.Int32>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Int32> AssembleTermOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(AssembleTerm));
        public bool AssembleTermIsChanged => GetIsChanged(nameof(AssembleTerm));

        public System.Nullable<System.DateTime> SignalToStartProduction
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> SignalToStartProductionOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(SignalToStartProduction));
        public bool SignalToStartProductionIsChanged => GetIsChanged(nameof(SignalToStartProduction));

        public System.Nullable<System.DateTime> SignalToStartProductionDone
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> SignalToStartProductionDoneOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(SignalToStartProductionDone));
        public bool SignalToStartProductionDoneIsChanged => GetIsChanged(nameof(SignalToStartProductionDone));

        public System.Nullable<System.DateTime> StartProductionDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> StartProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(StartProductionDate));
        public bool StartProductionDateIsChanged => GetIsChanged(nameof(StartProductionDate));

        public System.Nullable<System.DateTime> PickingDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> PickingDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(PickingDate));
        public bool PickingDateIsChanged => GetIsChanged(nameof(PickingDate));

        public System.Nullable<System.DateTime> EndProductionPlanDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> EndProductionPlanDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionPlanDate));
        public bool EndProductionPlanDateIsChanged => GetIsChanged(nameof(EndProductionPlanDate));

        public System.Nullable<System.DateTime> EndProductionDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> EndProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionDate));
        public bool EndProductionDateIsChanged => GetIsChanged(nameof(EndProductionDate));

        public System.Nullable<System.Int32> ExpectedDeliveryPeriod
        {
          get { return GetValue<System.Nullable<System.Int32>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Int32> ExpectedDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ExpectedDeliveryPeriod));
        public bool ExpectedDeliveryPeriodIsChanged => GetIsChanged(nameof(ExpectedDeliveryPeriod));

        public System.Nullable<System.Int32> ExpectedDeliveryPeriodCalculated
        {
          get { return GetValue<System.Nullable<System.Int32>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Int32> ExpectedDeliveryPeriodCalculatedOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ExpectedDeliveryPeriodCalculated));
        public bool ExpectedDeliveryPeriodCalculatedIsChanged => GetIsChanged(nameof(ExpectedDeliveryPeriodCalculated));

        public System.Nullable<System.DateTime> ShipmentDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> ShipmentDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ShipmentDate));
        public bool ShipmentDateIsChanged => GetIsChanged(nameof(ShipmentDate));

        public System.Nullable<System.DateTime> ShipmentPlanDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> ShipmentPlanDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ShipmentPlanDate));
        public bool ShipmentPlanDateIsChanged => GetIsChanged(nameof(ShipmentPlanDate));

        public System.Nullable<System.DateTime> DeliveryDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DeliveryDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DeliveryDate));
        public bool DeliveryDateIsChanged => GetIsChanged(nameof(DeliveryDate));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }

	    public FacilityWrapper Facility 
        {
            get { return GetWrapper<FacilityWrapper>(); }
            set { SetComplexValue<Facility, FacilityWrapper>(Facility, value); }
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

	    public OrderWrapper Order 
        {
            get { return GetWrapper<OrderWrapper>(); }
            set { SetComplexValue<Order, OrderWrapper>(Order, value); }
        }

	    public SpecificationWrapper Specification 
        {
            get { return GetWrapper<SpecificationWrapper>(); }
            set { SetComplexValue<Specification, SpecificationWrapper>(Specification, value); }
        }

	    public AddressWrapper Address 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(Address, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; private set; }

        public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }

        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }

        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlannedActual { get; private set; }

        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlannedGenerated { get; private set; }

        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlannedCalculated { get; private set; }

        #endregion

        #region GetProperties
        public System.Boolean AllowEditCost => GetValue<System.Boolean>(); 

        public System.Boolean AllowEditProduct => GetValue<System.Boolean>(); 

        public System.Boolean IsLoosen => GetValue<System.Boolean>(); 

        public System.Boolean IsPaid => GetValue<System.Boolean>(); 

        public System.Double SumPaid => GetValue<System.Double>(); 

        public System.Double SumNotPaid => GetValue<System.Double>(); 

        public System.Double SumToStartProduction => GetValue<System.Double>(); 

        public System.Double SumToShipping => GetValue<System.Double>(); 

        public System.DateTime OrderInTakeDate => GetValue<System.DateTime>(); 

        public System.Int32 OrderInTakeYear => GetValue<System.Int32>(); 

        public System.Int32 OrderInTakeMonth => GetValue<System.Int32>(); 

        public System.Nullable<System.DateTime> StartProductionConditionsDoneDate => GetValue<System.Nullable<System.DateTime>>(); 

        public System.Nullable<System.DateTime> ShippingConditionsDoneDate => GetValue<System.Nullable<System.DateTime>>(); 

        public System.DateTime StartProductionDateCalculated => GetValue<System.DateTime>(); 

        public System.DateTime EndProductionDateCalculated => GetValue<System.DateTime>(); 

        public System.DateTime RealizationDateCalculated => GetValue<System.DateTime>(); 

        public System.DateTime ShipmentDateCalculated => GetValue<System.DateTime>(); 

        public System.DateTime DeliveryDateCalculated => GetValue<System.DateTime>(); 

        public System.Double DeliveryPeriodCalculated => GetValue<System.Double>(); 

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

            InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));

            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));

            InitializeComplexProperty<ProjectWrapper>(nameof(Project), Model.Project == null ? null : new ProjectWrapper(Model.Project));

            InitializeComplexProperty<CompanyWrapper>(nameof(Producer), Model.Producer == null ? null : new CompanyWrapper(Model.Producer));

            InitializeComplexProperty<OrderWrapper>(nameof(Order), Model.Order == null ? null : new OrderWrapper(Model.Order));

            InitializeComplexProperty<SpecificationWrapper>(nameof(Specification), Model.Specification == null ? null : new SpecificationWrapper(Model.Specification));

            InitializeComplexProperty<AddressWrapper>(nameof(Address), Model.Address == null ? null : new AddressWrapper(Model.Address));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.ProductsIncluded == null) throw new ArgumentException("ProductsIncluded cannot be null");
          ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedWrapper>(Model.ProductsIncluded.Select(e => new ProductIncludedWrapper(e)));
          RegisterCollection(ProductsIncluded, Model.ProductsIncluded);

          if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
          PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => new PaymentActualWrapper(e)));
          RegisterCollection(PaymentsActual, Model.PaymentsActual);

          if (Model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
          PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlanned.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);

          if (Model.PaymentsPlannedActual == null) throw new ArgumentException("PaymentsPlannedActual cannot be null");
          PaymentsPlannedActual = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlannedActual.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlannedActual, Model.PaymentsPlannedActual);

          if (Model.PaymentsPlannedGenerated == null) throw new ArgumentException("PaymentsPlannedGenerated cannot be null");
          PaymentsPlannedGenerated = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlannedGenerated.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlannedGenerated, Model.PaymentsPlannedGenerated);

          if (Model.PaymentsPlannedCalculated == null) throw new ArgumentException("PaymentsPlannedCalculated cannot be null");
          PaymentsPlannedCalculated = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlannedCalculated.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlannedCalculated, Model.PaymentsPlannedCalculated);

        }
	}

		public partial class TestFriendAddressWrapper : WrapperBase<TestFriendAddress>
	{
	    public TestFriendAddressWrapper(TestFriendAddress model) : base(model) { }

	
        #region SimpleProperties
        public System.String City
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CityOriginalValue => GetOriginalValue<System.String>(nameof(City));
        public bool CityIsChanged => GetIsChanged(nameof(City));

        public System.String Street
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String StreetOriginalValue => GetOriginalValue<System.String>(nameof(Street));
        public bool StreetIsChanged => GetIsChanged(nameof(Street));

        public System.String StreetNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String StreetNumberOriginalValue => GetOriginalValue<System.String>(nameof(StreetNumber));
        public bool StreetNumberIsChanged => GetIsChanged(nameof(StreetNumber));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class TestFriendWrapper : WrapperBase<TestFriend>
	{
	    public TestFriendWrapper(TestFriend model) : base(model) { }

	
        #region SimpleProperties
        public System.Int32 FriendGroupId
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 FriendGroupIdOriginalValue => GetOriginalValue<System.Int32>(nameof(FriendGroupId));
        public bool FriendGroupIdIsChanged => GetIsChanged(nameof(FriendGroupId));

        public System.String FirstName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FirstNameOriginalValue => GetOriginalValue<System.String>(nameof(FirstName));
        public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));

        public System.String LastName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String LastNameOriginalValue => GetOriginalValue<System.String>(nameof(LastName));
        public bool LastNameIsChanged => GetIsChanged(nameof(LastName));

        public System.Nullable<System.DateTime> Birthday
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> BirthdayOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(Birthday));
        public bool BirthdayIsChanged => GetIsChanged(nameof(Birthday));

        public System.Boolean IsDeveloper
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsDeveloperOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDeveloper));
        public bool IsDeveloperIsChanged => GetIsChanged(nameof(IsDeveloper));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public TestFriendAddressWrapper TestFriendAddress 
        {
            get { return GetWrapper<TestFriendAddressWrapper>(); }
            set { SetComplexValue<TestFriendAddress, TestFriendAddressWrapper>(TestFriendAddress, value); }
        }

	    public TestFriendGroupWrapper TestFriendGroup 
        {
            get { return GetWrapper<TestFriendGroupWrapper>(); }
            set { SetComplexValue<TestFriendGroup, TestFriendGroupWrapper>(TestFriendGroup, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<TestFriendEmailWrapper> Emails { get; private set; }

        #endregion

        #region GetProperties
        public System.Int32 IdGet => GetValue<System.Int32>(); 

        public HVTApp.Model.POCOs.TestFriendEmail TestFriendEmailGet => GetValue<HVTApp.Model.POCOs.TestFriendEmail>(); 

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<TestFriendAddressWrapper>(nameof(TestFriendAddress), Model.TestFriendAddress == null ? null : new TestFriendAddressWrapper(Model.TestFriendAddress));

            InitializeComplexProperty<TestFriendGroupWrapper>(nameof(TestFriendGroup), Model.TestFriendGroup == null ? null : new TestFriendGroupWrapper(Model.TestFriendGroup));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Emails == null) throw new ArgumentException("Emails cannot be null");
          Emails = new ValidatableChangeTrackingCollection<TestFriendEmailWrapper>(Model.Emails.Select(e => new TestFriendEmailWrapper(e)));
          RegisterCollection(Emails, Model.Emails);

        }
	}

		public partial class TestFriendEmailWrapper : WrapperBase<TestFriendEmail>
	{
	    public TestFriendEmailWrapper(TestFriendEmail model) : base(model) { }

	
        #region SimpleProperties
        public System.String Email
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String EmailOriginalValue => GetOriginalValue<System.String>(nameof(Email));
        public bool EmailIsChanged => GetIsChanged(nameof(Email));

        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class TestFriendGroupWrapper : WrapperBase<TestFriendGroup>
	{
	    public TestFriendGroupWrapper(TestFriendGroup model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<TestFriendWrapper> FriendTests { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.FriendTests == null) throw new ArgumentException("FriendTests cannot be null");
          FriendTests = new ValidatableChangeTrackingCollection<TestFriendWrapper>(Model.FriendTests.Select(e => new TestFriendWrapper(e)));
          RegisterCollection(FriendTests, Model.FriendTests);

        }
	}

		public partial class DocumentWrapper : WrapperBase<Document>
	{
	    public DocumentWrapper(Document model) : base(model) { }

	
        #region SimpleProperties
        public System.String Code
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CodeOriginalValue => GetOriginalValue<System.String>(nameof(Code));
        public bool CodeIsChanged => GetIsChanged(nameof(Code));

        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Guid SenderId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid SenderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SenderId));
        public bool SenderIdIsChanged => GetIsChanged(nameof(SenderId));

        public System.Guid RecipientId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid RecipientIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RecipientId));
        public bool RecipientIdIsChanged => GetIsChanged(nameof(RecipientId));

        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public DocumentNumberWrapper Number 
        {
            get { return GetWrapper<DocumentNumberWrapper>(); }
            set { SetComplexValue<DocumentNumber, DocumentNumberWrapper>(Number, value); }
        }

	    public DocumentWrapper RequestDocument 
        {
            get { return GetWrapper<DocumentWrapper>(); }
            set { SetComplexValue<Document, DocumentWrapper>(RequestDocument, value); }
        }

	    public EmployeeWrapper Author 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(Author, value); }
        }

	    public EmployeeWrapper SenderEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(SenderEmployee, value); }
        }

	    public EmployeeWrapper RecipientEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(RecipientEmployee, value); }
        }

	    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient 
        {
            get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
            set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfRecipient, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }

        #endregion

        #region GetProperties
        public System.String RegNumber => GetValue<System.String>(); 

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<DocumentNumberWrapper>(nameof(Number), Model.Number == null ? null : new DocumentNumberWrapper(Model.Number));

            InitializeComplexProperty<DocumentWrapper>(nameof(RequestDocument), Model.RequestDocument == null ? null : new DocumentWrapper(Model.RequestDocument));

            InitializeComplexProperty<EmployeeWrapper>(nameof(Author), Model.Author == null ? null : new EmployeeWrapper(Model.Author));

            InitializeComplexProperty<EmployeeWrapper>(nameof(SenderEmployee), Model.SenderEmployee == null ? null : new EmployeeWrapper(Model.SenderEmployee));

            InitializeComplexProperty<EmployeeWrapper>(nameof(RecipientEmployee), Model.RecipientEmployee == null ? null : new EmployeeWrapper(Model.RecipientEmployee));

            InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient), Model.RegistrationDetailsOfRecipient == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
          CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
          RegisterCollection(CopyToRecipients, Model.CopyToRecipients);

        }
	}

		public partial class DocumentNumberWrapper : WrapperBase<DocumentNumber>
	{
	    public DocumentNumberWrapper(DocumentNumber model) : base(model) { }

	
        #region SimpleProperties
        public System.Int32 Number
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 NumberOriginalValue => GetOriginalValue<System.Int32>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class TestEntityWrapper : WrapperBase<TestEntity>
	{
	    public TestEntityWrapper(TestEntity model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class TestHusbandWrapper : WrapperBase<TestHusband>
	{
	    public TestHusbandWrapper(TestHusband model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public TestWifeWrapper Wife 
        {
            get { return GetWrapper<TestWifeWrapper>(); }
            set { SetComplexValue<TestWife, TestWifeWrapper>(Wife, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<TestChildWrapper> Children { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<TestWifeWrapper>(nameof(Wife), Model.Wife == null ? null : new TestWifeWrapper(Model.Wife));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Children == null) throw new ArgumentException("Children cannot be null");
          Children = new ValidatableChangeTrackingCollection<TestChildWrapper>(Model.Children.Select(e => new TestChildWrapper(e)));
          RegisterCollection(Children, Model.Children);

        }
	}

		public partial class TestWifeWrapper : WrapperBase<TestWife>
	{
	    public TestWifeWrapper(TestWife model) : base(model) { }

	
        #region SimpleProperties
        public System.Int32 N
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 NOriginalValue => GetOriginalValue<System.Int32>(nameof(N));
        public bool NIsChanged => GetIsChanged(nameof(N));

        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public TestHusbandWrapper Husband 
        {
            get { return GetWrapper<TestHusbandWrapper>(); }
            set { SetComplexValue<TestHusband, TestHusbandWrapper>(Husband, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<TestHusbandWrapper>(nameof(Husband), Model.Husband == null ? null : new TestHusbandWrapper(Model.Husband));

        }
	}

		public partial class TestChildWrapper : WrapperBase<TestChild>
	{
	    public TestChildWrapper(TestChild model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public TestHusbandWrapper Husband 
        {
            get { return GetWrapper<TestHusbandWrapper>(); }
            set { SetComplexValue<TestHusband, TestHusbandWrapper>(Husband, value); }
        }

	    public TestWifeWrapper Wife 
        {
            get { return GetWrapper<TestWifeWrapper>(); }
            set { SetComplexValue<TestWife, TestWifeWrapper>(Wife, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<TestHusbandWrapper>(nameof(Husband), Model.Husband == null ? null : new TestHusbandWrapper(Model.Husband));

            InitializeComplexProperty<TestWifeWrapper>(nameof(Wife), Model.Wife == null ? null : new TestWifeWrapper(Model.Wife));

        }
	}

		public partial class SumOnDateWrapper : WrapperBase<SumOnDate>
	{
	    public SumOnDateWrapper(SumOnDate model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Double Sum
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
        public bool SumIsChanged => GetIsChanged(nameof(Sum));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class ProductWrapper : WrapperBase<Product>
	{
	    public ProductWrapper(Product model) : base(model) { }

	
        #region SimpleProperties
        public System.String Designation
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
        public bool DesignationIsChanged => GetIsChanged(nameof(Designation));

        public System.String DesignationSpecial
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationSpecialOriginalValue => GetOriginalValue<System.String>(nameof(DesignationSpecial));
        public bool DesignationSpecialIsChanged => GetIsChanged(nameof(DesignationSpecial));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProductTypeWrapper ProductType 
        {
            get { return GetWrapper<ProductTypeWrapper>(); }
            set { SetComplexValue<ProductType, ProductTypeWrapper>(ProductType, value); }
        }

	    public ProductBlockWrapper ProductBlock 
        {
            get { return GetWrapper<ProductBlockWrapper>(); }
            set { SetComplexValue<ProductBlock, ProductBlockWrapper>(ProductBlock, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProductDependentWrapper> DependentProducts { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductTypeWrapper>(nameof(ProductType), Model.ProductType == null ? null : new ProductTypeWrapper(Model.ProductType));

            InitializeComplexProperty<ProductBlockWrapper>(nameof(ProductBlock), Model.ProductBlock == null ? null : new ProductBlockWrapper(Model.ProductBlock));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.DependentProducts == null) throw new ArgumentException("DependentProducts cannot be null");
          DependentProducts = new ValidatableChangeTrackingCollection<ProductDependentWrapper>(Model.DependentProducts.Select(e => new ProductDependentWrapper(e)));
          RegisterCollection(DependentProducts, Model.DependentProducts);

        }
	}

		public partial class OfferWrapper : WrapperBase<Offer>
	{
	    public OfferWrapper(Offer model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime ValidityDate
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime ValidityDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(ValidityDate));
        public bool ValidityDateIsChanged => GetIsChanged(nameof(ValidityDate));

        public System.Double Vat
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
        public bool VatIsChanged => GetIsChanged(nameof(Vat));

        public System.String Code
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CodeOriginalValue => GetOriginalValue<System.String>(nameof(Code));
        public bool CodeIsChanged => GetIsChanged(nameof(Code));

        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Guid SenderId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid SenderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SenderId));
        public bool SenderIdIsChanged => GetIsChanged(nameof(SenderId));

        public System.Guid RecipientId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid RecipientIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RecipientId));
        public bool RecipientIdIsChanged => GetIsChanged(nameof(RecipientId));

        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProjectWrapper Project 
        {
            get { return GetWrapper<ProjectWrapper>(); }
            set { SetComplexValue<Project, ProjectWrapper>(Project, value); }
        }

	    public DocumentNumberWrapper Number 
        {
            get { return GetWrapper<DocumentNumberWrapper>(); }
            set { SetComplexValue<DocumentNumber, DocumentNumberWrapper>(Number, value); }
        }

	    public DocumentWrapper RequestDocument 
        {
            get { return GetWrapper<DocumentWrapper>(); }
            set { SetComplexValue<Document, DocumentWrapper>(RequestDocument, value); }
        }

	    public EmployeeWrapper Author 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(Author, value); }
        }

	    public EmployeeWrapper SenderEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(SenderEmployee, value); }
        }

	    public EmployeeWrapper RecipientEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(RecipientEmployee, value); }
        }

	    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient 
        {
            get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
            set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfRecipient, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }

        #endregion

        #region GetProperties
        public System.String RegNumber => GetValue<System.String>(); 

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProjectWrapper>(nameof(Project), Model.Project == null ? null : new ProjectWrapper(Model.Project));

            InitializeComplexProperty<DocumentNumberWrapper>(nameof(Number), Model.Number == null ? null : new DocumentNumberWrapper(Model.Number));

            InitializeComplexProperty<DocumentWrapper>(nameof(RequestDocument), Model.RequestDocument == null ? null : new DocumentWrapper(Model.RequestDocument));

            InitializeComplexProperty<EmployeeWrapper>(nameof(Author), Model.Author == null ? null : new EmployeeWrapper(Model.Author));

            InitializeComplexProperty<EmployeeWrapper>(nameof(SenderEmployee), Model.SenderEmployee == null ? null : new EmployeeWrapper(Model.SenderEmployee));

            InitializeComplexProperty<EmployeeWrapper>(nameof(RecipientEmployee), Model.RecipientEmployee == null ? null : new EmployeeWrapper(Model.RecipientEmployee));

            InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient), Model.RegistrationDetailsOfRecipient == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
          CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
          RegisterCollection(CopyToRecipients, Model.CopyToRecipients);

        }
	}

		public partial class EmployeeWrapper : WrapperBase<Employee>
	{
	    public EmployeeWrapper(Employee model) : base(model) { }

	
        #region SimpleProperties
        public System.String PhoneNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String PhoneNumberOriginalValue => GetOriginalValue<System.String>(nameof(PhoneNumber));
        public bool PhoneNumberIsChanged => GetIsChanged(nameof(PhoneNumber));

        public System.String Email
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String EmailOriginalValue => GetOriginalValue<System.String>(nameof(Email));
        public bool EmailIsChanged => GetIsChanged(nameof(Email));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public PersonWrapper Person 
        {
            get { return GetWrapper<PersonWrapper>(); }
            set { SetComplexValue<Person, PersonWrapper>(Person, value); }
        }

	    public CompanyWrapper Company 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Company, value); }
        }

	    public EmployeesPositionWrapper Position 
        {
            get { return GetWrapper<EmployeesPositionWrapper>(); }
            set { SetComplexValue<EmployeesPosition, EmployeesPositionWrapper>(Position, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<PersonWrapper>(nameof(Person), Model.Person == null ? null : new PersonWrapper(Model.Person));

            InitializeComplexProperty<CompanyWrapper>(nameof(Company), Model.Company == null ? null : new CompanyWrapper(Model.Company));

            InitializeComplexProperty<EmployeesPositionWrapper>(nameof(Position), Model.Position == null ? null : new EmployeesPositionWrapper(Model.Position));

        }
	}

		public partial class OrderWrapper : WrapperBase<Order>
	{
	    public OrderWrapper(Order model) : base(model) { }

	
        #region SimpleProperties
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        public System.DateTime DateOpen
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOpenOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateOpen));
        public bool DateOpenIsChanged => GetIsChanged(nameof(DateOpen));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class PaymentConditionWrapper : WrapperBase<PaymentCondition>
	{
	    public PaymentConditionWrapper(PaymentCondition model) : base(model) { }

	
        #region SimpleProperties
        public System.Double Part
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PartOriginalValue => GetOriginalValue<System.Double>(nameof(Part));
        public bool PartIsChanged => GetIsChanged(nameof(Part));

        public System.Int32 DaysToPoint
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 DaysToPointOriginalValue => GetOriginalValue<System.Int32>(nameof(DaysToPoint));
        public bool DaysToPointIsChanged => GetIsChanged(nameof(DaysToPoint));

        public HVTApp.Model.POCOs.PaymentConditionPoint PaymentConditionPoint
        {
          get { return GetValue<HVTApp.Model.POCOs.PaymentConditionPoint>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.PaymentConditionPoint PaymentConditionPointOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.PaymentConditionPoint>(nameof(PaymentConditionPoint));
        public bool PaymentConditionPointIsChanged => GetIsChanged(nameof(PaymentConditionPoint));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class PaymentDocumentWrapper : WrapperBase<PaymentDocument>
	{
	    public PaymentDocumentWrapper(PaymentDocument model) : base(model) { }

	
        #region SimpleProperties
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PaymentActualWrapper> Payments { get; private set; }

        #endregion

        #region GetProperties
        public System.DateTime Date => GetValue<System.DateTime>(); 

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Payments == null) throw new ArgumentException("Payments cannot be null");
          Payments = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.Payments.Select(e => new PaymentActualWrapper(e)));
          RegisterCollection(Payments, Model.Payments);

        }
	}

		public partial class FacilityWrapper : WrapperBase<Facility>
	{
	    public FacilityWrapper(Facility model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public FacilityTypeWrapper Type 
        {
            get { return GetWrapper<FacilityTypeWrapper>(); }
            set { SetComplexValue<FacilityType, FacilityTypeWrapper>(Type, value); }
        }

	    public CompanyWrapper OwnerCompany 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(OwnerCompany, value); }
        }

	    public AddressWrapper Address 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(Address, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<FacilityTypeWrapper>(nameof(Type), Model.Type == null ? null : new FacilityTypeWrapper(Model.Type));

            InitializeComplexProperty<CompanyWrapper>(nameof(OwnerCompany), Model.OwnerCompany == null ? null : new CompanyWrapper(Model.OwnerCompany));

            InitializeComplexProperty<AddressWrapper>(nameof(Address), Model.Address == null ? null : new AddressWrapper(Model.Address));

        }
	}

		public partial class ProjectWrapper : WrapperBase<Project>
	{
	    public ProjectWrapper(Project model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public System.Boolean HighProbability
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean HighProbabilityOriginalValue => GetOriginalValue<System.Boolean>(nameof(HighProbability));
        public bool HighProbabilityIsChanged => GetIsChanged(nameof(HighProbability));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProjectTypeWrapper ProjectType 
        {
            get { return GetWrapper<ProjectTypeWrapper>(); }
            set { SetComplexValue<ProjectType, ProjectTypeWrapper>(ProjectType, value); }
        }

	    public UserWrapper Manager 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Manager, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<NoteWrapper> Notes { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProjectTypeWrapper>(nameof(ProjectType), Model.ProjectType == null ? null : new ProjectTypeWrapper(Model.ProjectType));

            InitializeComplexProperty<UserWrapper>(nameof(Manager), Model.Manager == null ? null : new UserWrapper(Model.Manager));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Notes == null) throw new ArgumentException("Notes cannot be null");
          Notes = new ValidatableChangeTrackingCollection<NoteWrapper>(Model.Notes.Select(e => new NoteWrapper(e)));
          RegisterCollection(Notes, Model.Notes);

        }
	}

		public partial class UserRoleWrapper : WrapperBase<UserRole>
	{
	    public UserRoleWrapper(UserRole model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public HVTApp.Infrastructure.Role Role
        {
          get { return GetValue<HVTApp.Infrastructure.Role>(); }
          set { SetValue(value); }
        }
        public HVTApp.Infrastructure.Role RoleOriginalValue => GetOriginalValue<HVTApp.Infrastructure.Role>(nameof(Role));
        public bool RoleIsChanged => GetIsChanged(nameof(Role));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class SpecificationWrapper : WrapperBase<Specification>
	{
	    public SpecificationWrapper(Specification model) : base(model) { }

	
        #region SimpleProperties
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Double Vat
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
        public bool VatIsChanged => GetIsChanged(nameof(Vat));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ContractWrapper Contract 
        {
            get { return GetWrapper<ContractWrapper>(); }
            set { SetComplexValue<Contract, ContractWrapper>(Contract, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ContractWrapper>(nameof(Contract), Model.Contract == null ? null : new ContractWrapper(Model.Contract));

        }
	}

		public partial class TenderWrapper : WrapperBase<Tender>
	{
	    public TenderWrapper(Tender model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime DateOpen
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOpenOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateOpen));
        public bool DateOpenIsChanged => GetIsChanged(nameof(DateOpen));

        public System.DateTime DateClose
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateCloseOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateClose));
        public bool DateCloseIsChanged => GetIsChanged(nameof(DateClose));

        public System.Nullable<System.DateTime> DateNotice
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DateNoticeOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateNotice));
        public bool DateNoticeIsChanged => GetIsChanged(nameof(DateNotice));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public ProjectWrapper Project 
        {
            get { return GetWrapper<ProjectWrapper>(); }
            set { SetComplexValue<Project, ProjectWrapper>(Project, value); }
        }

	    public CompanyWrapper Winner 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Winner, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<TenderTypeWrapper> Types { get; private set; }

        public IValidatableChangeTrackingCollection<CompanyWrapper> Participants { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProjectWrapper>(nameof(Project), Model.Project == null ? null : new ProjectWrapper(Model.Project));

            InitializeComplexProperty<CompanyWrapper>(nameof(Winner), Model.Winner == null ? null : new CompanyWrapper(Model.Winner));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Types == null) throw new ArgumentException("Types cannot be null");
          Types = new ValidatableChangeTrackingCollection<TenderTypeWrapper>(Model.Types.Select(e => new TenderTypeWrapper(e)));
          RegisterCollection(Types, Model.Types);

          if (Model.Participants == null) throw new ArgumentException("Participants cannot be null");
          Participants = new ValidatableChangeTrackingCollection<CompanyWrapper>(Model.Participants.Select(e => new CompanyWrapper(e)));
          RegisterCollection(Participants, Model.Participants);

        }
	}

		public partial class TenderTypeWrapper : WrapperBase<TenderType>
	{
	    public TenderTypeWrapper(TenderType model) : base(model) { }

	
        #region SimpleProperties
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));

        public HVTApp.Model.POCOs.TenderTypeEnum Type
        {
          get { return GetValue<HVTApp.Model.POCOs.TenderTypeEnum>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.TenderTypeEnum TypeOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.TenderTypeEnum>(nameof(Type));
        public bool TypeIsChanged => GetIsChanged(nameof(Type));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class UserWrapper : WrapperBase<User>
	{
	    public UserWrapper(User model) : base(model) { }

	
        #region SimpleProperties
        public System.String Login
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String LoginOriginalValue => GetOriginalValue<System.String>(nameof(Login));
        public bool LoginIsChanged => GetIsChanged(nameof(Login));

        public System.Guid Password
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid PasswordOriginalValue => GetOriginalValue<System.Guid>(nameof(Password));
        public bool PasswordIsChanged => GetIsChanged(nameof(Password));

        public System.String PersonalNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String PersonalNumberOriginalValue => GetOriginalValue<System.String>(nameof(PersonalNumber));
        public bool PersonalNumberIsChanged => GetIsChanged(nameof(PersonalNumber));

        public HVTApp.Infrastructure.Role RoleCurrent
        {
          get { return GetValue<HVTApp.Infrastructure.Role>(); }
          set { SetValue(value); }
        }
        public HVTApp.Infrastructure.Role RoleCurrentOriginalValue => GetOriginalValue<HVTApp.Infrastructure.Role>(nameof(RoleCurrent));
        public bool RoleCurrentIsChanged => GetIsChanged(nameof(RoleCurrent));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public EmployeeWrapper Employee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(Employee, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<UserRoleWrapper> Roles { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<EmployeeWrapper>(nameof(Employee), Model.Employee == null ? null : new EmployeeWrapper(Model.Employee));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Roles == null) throw new ArgumentException("Roles cannot be null");
          Roles = new ValidatableChangeTrackingCollection<UserRoleWrapper>(Model.Roles.Select(e => new UserRoleWrapper(e)));
          RegisterCollection(Roles, Model.Roles);

        }
	}

	}
