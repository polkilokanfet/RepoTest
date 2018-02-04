using System;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
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

        public System.Nullable<System.Guid> LocalityId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> LocalityIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(LocalityId));
        public bool LocalityIdIsChanged => GetIsChanged(nameof(LocalityId));

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

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<DistrictWrapper> Districts { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Districts == null) throw new ArgumentException("Districts cannot be null");
          Districts = new ValidatableChangeTrackingCollection<DistrictWrapper>(Model.Districts.Select(e => new DistrictWrapper(e)));
          RegisterCollection(Districts, Model.Districts);

        }
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

        public System.Guid CountryId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid CountryIdOriginalValue => GetOriginalValue<System.Guid>(nameof(CountryId));
        public bool CountryIdIsChanged => GetIsChanged(nameof(CountryId));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<RegionWrapper> Regions { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Regions == null) throw new ArgumentException("Regions cannot be null");
          Regions = new ValidatableChangeTrackingCollection<RegionWrapper>(Model.Regions.Select(e => new RegionWrapper(e)));
          RegisterCollection(Regions, Model.Regions);

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

        public System.Guid RegionId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid RegionIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RegionId));
        public bool RegionIdIsChanged => GetIsChanged(nameof(RegionId));

        public System.Boolean IsRegionCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsRegionCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsRegionCapital));
        public bool IsRegionCapitalIsChanged => GetIsChanged(nameof(IsRegionCapital));

        public System.Boolean IsDistrictsCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsDistrictsCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDistrictsCapital));
        public bool IsDistrictsCapitalIsChanged => GetIsChanged(nameof(IsDistrictsCapital));

        public System.Boolean IsCountryCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsCountryCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsCountryCapital));
        public bool IsCountryCapitalIsChanged => GetIsChanged(nameof(IsCountryCapital));

        public System.Nullable<System.Double> StandartDeliveryPeriod
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> StandartDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(StandartDeliveryPeriod));
        public bool StandartDeliveryPeriodIsChanged => GetIsChanged(nameof(StandartDeliveryPeriod));

        public System.Nullable<System.Guid> LocalityTypeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> LocalityTypeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(LocalityTypeId));
        public bool LocalityTypeIdIsChanged => GetIsChanged(nameof(LocalityTypeId));

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

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<LocalityTypeWrapper>(nameof(LocalityType), Model.LocalityType == null ? null : new LocalityTypeWrapper(Model.LocalityType));

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

        public System.Guid DistrictId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid DistrictIdOriginalValue => GetOriginalValue<System.Guid>(nameof(DistrictId));
        public bool DistrictIdIsChanged => GetIsChanged(nameof(DistrictId));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<LocalityWrapper> Localities { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Localities == null) throw new ArgumentException("Localities cannot be null");
          Localities = new ValidatableChangeTrackingCollection<LocalityWrapper>(Model.Localities.Select(e => new LocalityWrapper(e)));
          RegisterCollection(Localities, Model.Localities);

        }
	}

		public partial class AdditionalSalesUnitsWrapper : WrapperBase<AdditionalSalesUnits>
	{
	    public AdditionalSalesUnitsWrapper(AdditionalSalesUnits model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> AdditionalSalesUnitId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> AdditionalSalesUnitIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(AdditionalSalesUnitId));
        public bool AdditionalSalesUnitIdIsChanged => GetIsChanged(nameof(AdditionalSalesUnitId));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public SalesUnitWrapper AdditionalSalesUnit 
        {
            get { return GetWrapper<SalesUnitWrapper>(); }
            set { SetComplexValue<SalesUnit, SalesUnitWrapper>(AdditionalSalesUnit, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> ParentSalesUnits { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<SalesUnitWrapper>(nameof(AdditionalSalesUnit), Model.AdditionalSalesUnit == null ? null : new SalesUnitWrapper(Model.AdditionalSalesUnit));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.ParentSalesUnits == null) throw new ArgumentException("ParentSalesUnits cannot be null");
          ParentSalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.ParentSalesUnits.Select(e => new SalesUnitWrapper(e)));
          RegisterCollection(ParentSalesUnits, Model.ParentSalesUnits);

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
        public System.Nullable<System.Guid> FormId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> FormIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(FormId));
        public bool FormIdIsChanged => GetIsChanged(nameof(FormId));

        public System.Nullable<System.Guid> ParentCompanyId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ParentCompanyIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ParentCompanyId));
        public bool ParentCompanyIdIsChanged => GetIsChanged(nameof(ParentCompanyId));

        public System.Nullable<System.Guid> AddressLegalId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> AddressLegalIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(AddressLegalId));
        public bool AddressLegalIdIsChanged => GetIsChanged(nameof(AddressLegalId));

        public System.Nullable<System.Guid> AddressPostId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> AddressPostIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(AddressPostId));
        public bool AddressPostIdIsChanged => GetIsChanged(nameof(AddressPostId));

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
        public System.Nullable<System.Guid> ContragentId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ContragentIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ContragentId));
        public bool ContragentIdIsChanged => GetIsChanged(nameof(ContragentId));

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
        public System.Guid GroupId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid GroupIdOriginalValue => GetOriginalValue<System.Guid>(nameof(GroupId));
        public bool GroupIdIsChanged => GetIsChanged(nameof(GroupId));

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

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterRelationWrapper> RequiredPreviousParameters { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.RequiredPreviousParameters == null) throw new ArgumentException("RequiredPreviousParameters cannot be null");
          RequiredPreviousParameters = new ValidatableChangeTrackingCollection<ParameterRelationWrapper>(Model.RequiredPreviousParameters.Select(e => new ParameterRelationWrapper(e)));
          RegisterCollection(RequiredPreviousParameters, Model.RequiredPreviousParameters);

        }
	}

		public partial class ParameterGroupWrapper : WrapperBase<ParameterGroup>
	{
	    public ParameterGroupWrapper(ParameterGroup model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> MeasureId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> MeasureIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(MeasureId));
        public bool MeasureIdIsChanged => GetIsChanged(nameof(MeasureId));

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

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<MeasureWrapper>(nameof(Measure), Model.Measure == null ? null : new MeasureWrapper(Model.Measure));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);

        }
	}

		public partial class ProductRelationWrapper : WrapperBase<ProductRelation>
	{
	    public ProductRelationWrapper(ProductRelation model) : base(model) { }

	
        #region SimpleProperties
        public System.Int32 Count
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 CountOriginalValue => GetOriginalValue<System.Int32>(nameof(Count));
        public bool CountIsChanged => GetIsChanged(nameof(Count));

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

		public partial class StandartPaymentConditionsWrapper : WrapperBase<StandartPaymentConditions>
	{
	    public StandartPaymentConditionsWrapper(StandartPaymentConditions model) : base(model) { }

	
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
        public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
          PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
          RegisterCollection(PaymentsConditions, Model.PaymentsConditions);

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

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Employees == null) throw new ArgumentException("Employees cannot be null");
          Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.Employees.Select(e => new EmployeeWrapper(e)));
          RegisterCollection(Employees, Model.Employees);

        }
	}

		public partial class PaymentPlannedWrapper : WrapperBase<PaymentPlanned>
	{
	    public PaymentPlannedWrapper(PaymentPlanned model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> ConditionId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ConditionIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ConditionId));
        public bool ConditionIdIsChanged => GetIsChanged(nameof(ConditionId));

        public System.Guid SalesUnitId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid SalesUnitIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SalesUnitId));
        public bool SalesUnitIdIsChanged => GetIsChanged(nameof(SalesUnitId));

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

		public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
	{
	    public PaymentActualWrapper(PaymentActual model) : base(model) { }

	
        #region SimpleProperties
        public System.Guid SalesUnitId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid SalesUnitIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SalesUnitId));
        public bool SalesUnitIdIsChanged => GetIsChanged(nameof(SalesUnitId));

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

        public System.Guid DocumentId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid DocumentIdOriginalValue => GetOriginalValue<System.Guid>(nameof(DocumentId));
        public bool DocumentIdIsChanged => GetIsChanged(nameof(DocumentId));

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
        public System.Guid ParameterId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid ParameterIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ParameterId));
        public bool ParameterIdIsChanged => GetIsChanged(nameof(ParameterId));

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

		public partial class ProjectUnitWrapper : WrapperBase<ProjectUnit>
	{
	    public ProjectUnitWrapper(ProjectUnit model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> FacilityId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> FacilityIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(FacilityId));
        public bool FacilityIdIsChanged => GetIsChanged(nameof(FacilityId));

        public System.Nullable<System.Guid> ProductId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProductIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProductId));
        public bool ProductIdIsChanged => GetIsChanged(nameof(ProductId));

        public System.Guid ProjectId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid ProjectIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ProjectId));
        public bool ProjectIdIsChanged => GetIsChanged(nameof(ProjectId));

        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public System.DateTime RequiredDeliveryDate
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime RequiredDeliveryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(RequiredDeliveryDate));
        public bool RequiredDeliveryDateIsChanged => GetIsChanged(nameof(RequiredDeliveryDate));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

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

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));

            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

        }
	}

		public partial class TenderUnitWrapper : WrapperBase<TenderUnit>
	{
	    public TenderUnitWrapper(TenderUnit model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> TenderId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> TenderIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(TenderId));
        public bool TenderIdIsChanged => GetIsChanged(nameof(TenderId));

        public System.Nullable<System.Guid> ProjectUnitId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProjectUnitIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProjectUnitId));
        public bool ProjectUnitIdIsChanged => GetIsChanged(nameof(ProjectUnitId));

        public System.Nullable<System.Guid> FacilityId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> FacilityIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(FacilityId));
        public bool FacilityIdIsChanged => GetIsChanged(nameof(FacilityId));

        public System.Nullable<System.Guid> ProductId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProductIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProductId));
        public bool ProductIdIsChanged => GetIsChanged(nameof(ProductId));

        public System.Nullable<System.Guid> ProducerWinnerId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProducerWinnerIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProducerWinnerId));
        public bool ProducerWinnerIdIsChanged => GetIsChanged(nameof(ProducerWinnerId));

        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public System.DateTime DeliveryDate
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DeliveryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(DeliveryDate));
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
	    public TenderWrapper Tender 
        {
            get { return GetWrapper<TenderWrapper>(); }
            set { SetComplexValue<Tender, TenderWrapper>(Tender, value); }
        }

	    public ProjectUnitWrapper ProjectUnit 
        {
            get { return GetWrapper<ProjectUnitWrapper>(); }
            set { SetComplexValue<ProjectUnit, ProjectUnitWrapper>(ProjectUnit, value); }
        }

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

	    public CompanyWrapper ProducerWinner 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(ProducerWinner, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<TenderWrapper>(nameof(Tender), Model.Tender == null ? null : new TenderWrapper(Model.Tender));

            InitializeComplexProperty<ProjectUnitWrapper>(nameof(ProjectUnit), Model.ProjectUnit == null ? null : new ProjectUnitWrapper(Model.ProjectUnit));

            InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));

            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

            InitializeComplexProperty<CompanyWrapper>(nameof(ProducerWinner), Model.ProducerWinner == null ? null : new CompanyWrapper(Model.ProducerWinner));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
          PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
          RegisterCollection(PaymentsConditions, Model.PaymentsConditions);

        }
	}

		public partial class ShipmentUnitWrapper : WrapperBase<ShipmentUnit>
	{
	    public ShipmentUnitWrapper(ShipmentUnit model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> AddressId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> AddressIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(AddressId));
        public bool AddressIdIsChanged => GetIsChanged(nameof(AddressId));

        public System.Nullable<System.Int32> ExpectedDeliveryPeriod
        {
          get { return GetValue<System.Nullable<System.Int32>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Int32> ExpectedDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ExpectedDeliveryPeriod));
        public bool ExpectedDeliveryPeriodIsChanged => GetIsChanged(nameof(ExpectedDeliveryPeriod));

        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

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

        public System.Nullable<System.DateTime> RequiredDeliveryDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> RequiredDeliveryDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RequiredDeliveryDate));
        public bool RequiredDeliveryDateIsChanged => GetIsChanged(nameof(RequiredDeliveryDate));

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
	    public AddressWrapper Address 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(Address, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<AddressWrapper>(nameof(Address), Model.Address == null ? null : new AddressWrapper(Model.Address));

        }
	}

		public partial class ProductionUnitWrapper : WrapperBase<ProductionUnit>
	{
	    public ProductionUnitWrapper(ProductionUnit model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> ProductId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProductIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProductId));
        public bool ProductIdIsChanged => GetIsChanged(nameof(ProductId));

        public System.Guid OrderId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid OrderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(OrderId));
        public bool OrderIdIsChanged => GetIsChanged(nameof(OrderId));

        public System.Int32 OrderPosition
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 OrderPositionOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderPosition));
        public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));

        public System.String SerialNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String SerialNumberOriginalValue => GetOriginalValue<System.String>(nameof(SerialNumber));
        public bool SerialNumberIsChanged => GetIsChanged(nameof(SerialNumber));

        public System.Int32 PlannedTermFromStartToEndProduction
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 PlannedTermFromStartToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(PlannedTermFromStartToEndProduction));
        public bool PlannedTermFromStartToEndProductionIsChanged => GetIsChanged(nameof(PlannedTermFromStartToEndProduction));

        public System.Int32 PlannedTermFromPickToEndProduction
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 PlannedTermFromPickToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(PlannedTermFromPickToEndProduction));
        public bool PlannedTermFromPickToEndProductionIsChanged => GetIsChanged(nameof(PlannedTermFromPickToEndProduction));

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

        public System.Nullable<System.DateTime> EndProductionDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> EndProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionDate));
        public bool EndProductionDateIsChanged => GetIsChanged(nameof(EndProductionDate));

        public System.Nullable<System.DateTime> EndProductionDateByPlan
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> EndProductionDateByPlanOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionDateByPlan));
        public bool EndProductionDateByPlanIsChanged => GetIsChanged(nameof(EndProductionDateByPlan));

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

		public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
	{
	    public SalesUnitWrapper(SalesUnit model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> OfferUnitId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> OfferUnitIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(OfferUnitId));
        public bool OfferUnitIdIsChanged => GetIsChanged(nameof(OfferUnitId));

        public System.Nullable<System.Guid> ProductionUnitId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProductionUnitIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProductionUnitId));
        public bool ProductionUnitIdIsChanged => GetIsChanged(nameof(ProductionUnitId));

        public System.Nullable<System.Guid> ShipmentUnitId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ShipmentUnitIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ShipmentUnitId));
        public bool ShipmentUnitIdIsChanged => GetIsChanged(nameof(ShipmentUnitId));

        public System.Nullable<System.Guid> SpecificationId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> SpecificationIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(SpecificationId));
        public bool SpecificationIdIsChanged => GetIsChanged(nameof(SpecificationId));

        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public System.Nullable<System.DateTime> RealizationDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> RealizationDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RealizationDate));
        public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public OfferUnitWrapper OfferUnit 
        {
            get { return GetWrapper<OfferUnitWrapper>(); }
            set { SetComplexValue<OfferUnit, OfferUnitWrapper>(OfferUnit, value); }
        }

	    public ProductionUnitWrapper ProductionUnit 
        {
            get { return GetWrapper<ProductionUnitWrapper>(); }
            set { SetComplexValue<ProductionUnit, ProductionUnitWrapper>(ProductionUnit, value); }
        }

	    public ShipmentUnitWrapper ShipmentUnit 
        {
            get { return GetWrapper<ShipmentUnitWrapper>(); }
            set { SetComplexValue<ShipmentUnit, ShipmentUnitWrapper>(ShipmentUnit, value); }
        }

	    public SpecificationWrapper Specification 
        {
            get { return GetWrapper<SpecificationWrapper>(); }
            set { SetComplexValue<Specification, SpecificationWrapper>(Specification, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

        public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }

        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<OfferUnitWrapper>(nameof(OfferUnit), Model.OfferUnit == null ? null : new OfferUnitWrapper(Model.OfferUnit));

            InitializeComplexProperty<ProductionUnitWrapper>(nameof(ProductionUnit), Model.ProductionUnit == null ? null : new ProductionUnitWrapper(Model.ProductionUnit));

            InitializeComplexProperty<ShipmentUnitWrapper>(nameof(ShipmentUnit), Model.ShipmentUnit == null ? null : new ShipmentUnitWrapper(Model.ShipmentUnit));

            InitializeComplexProperty<SpecificationWrapper>(nameof(Specification), Model.Specification == null ? null : new SpecificationWrapper(Model.Specification));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
          PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
          RegisterCollection(PaymentsConditions, Model.PaymentsConditions);

          if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
          PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => new PaymentActualWrapper(e)));
          RegisterCollection(PaymentsActual, Model.PaymentsActual);

          if (Model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
          PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlanned.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);

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
        public System.Nullable<System.Guid> TestFriendAddressId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> TestFriendAddressIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(TestFriendAddressId));
        public bool TestFriendAddressIdIsChanged => GetIsChanged(nameof(TestFriendAddressId));

        public System.Nullable<System.Guid> TestFriendGroupId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> TestFriendGroupIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(TestFriendGroupId));
        public bool TestFriendGroupIdIsChanged => GetIsChanged(nameof(TestFriendGroupId));

        public System.Nullable<System.Guid> TestFriendEmailGetId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> TestFriendEmailGetIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(TestFriendEmailGetId));
        public bool TestFriendEmailGetIdIsChanged => GetIsChanged(nameof(TestFriendEmailGetId));

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
        public System.Nullable<System.Guid> RequestDocumentId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> RequestDocumentIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(RequestDocumentId));
        public bool RequestDocumentIdIsChanged => GetIsChanged(nameof(RequestDocumentId));

        public System.Nullable<System.Guid> AuthorId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> AuthorIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(AuthorId));
        public bool AuthorIdIsChanged => GetIsChanged(nameof(AuthorId));

        public System.Nullable<System.Guid> SenderEmployeeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> SenderEmployeeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(SenderEmployeeId));
        public bool SenderEmployeeIdIsChanged => GetIsChanged(nameof(SenderEmployeeId));

        public System.Nullable<System.Guid> RecipientEmployeeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> RecipientEmployeeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(RecipientEmployeeId));
        public bool RecipientEmployeeIdIsChanged => GetIsChanged(nameof(RecipientEmployeeId));

        public System.Nullable<System.Guid> RegistrationDetailsOfSenderId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> RegistrationDetailsOfSenderIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(RegistrationDetailsOfSenderId));
        public bool RegistrationDetailsOfSenderIdIsChanged => GetIsChanged(nameof(RegistrationDetailsOfSenderId));

        public System.Nullable<System.Guid> RegistrationDetailsOfRecipientId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> RegistrationDetailsOfRecipientIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(RegistrationDetailsOfRecipientId));
        public bool RegistrationDetailsOfRecipientIdIsChanged => GetIsChanged(nameof(RegistrationDetailsOfRecipientId));

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

	    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfSender 
        {
            get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
            set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfSender, value); }
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
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<DocumentWrapper>(nameof(RequestDocument), Model.RequestDocument == null ? null : new DocumentWrapper(Model.RequestDocument));

            InitializeComplexProperty<EmployeeWrapper>(nameof(Author), Model.Author == null ? null : new EmployeeWrapper(Model.Author));

            InitializeComplexProperty<EmployeeWrapper>(nameof(SenderEmployee), Model.SenderEmployee == null ? null : new EmployeeWrapper(Model.SenderEmployee));

            InitializeComplexProperty<EmployeeWrapper>(nameof(RecipientEmployee), Model.RecipientEmployee == null ? null : new EmployeeWrapper(Model.RecipientEmployee));

            InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfSender), Model.RegistrationDetailsOfSender == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfSender));

            InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient), Model.RegistrationDetailsOfRecipient == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
          CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
          RegisterCollection(CopyToRecipients, Model.CopyToRecipients);

        }
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
        public System.Nullable<System.Guid> WifeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> WifeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(WifeId));
        public bool WifeIdIsChanged => GetIsChanged(nameof(WifeId));

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
        public System.Nullable<System.Guid> HusbandId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> HusbandIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(HusbandId));
        public bool HusbandIdIsChanged => GetIsChanged(nameof(HusbandId));

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
        public System.Nullable<System.Guid> HusbandId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> HusbandIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(HusbandId));
        public bool HusbandIdIsChanged => GetIsChanged(nameof(HusbandId));

        public System.Nullable<System.Guid> WifeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> WifeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(WifeId));
        public bool WifeIdIsChanged => GetIsChanged(nameof(WifeId));

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

		public partial class CostOnDateWrapper : WrapperBase<CostOnDate>
	{
	    public CostOnDateWrapper(CostOnDate model) : base(model) { }

	
        #region SimpleProperties
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion
	}

		public partial class CostWrapper : WrapperBase<Cost>
	{
	    public CostWrapper(Cost model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> CurrencyId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> CurrencyIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(CurrencyId));
        public bool CurrencyIdIsChanged => GetIsChanged(nameof(CurrencyId));

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

        #region ComplexProperties
	    public CurrencyWrapper Currency 
        {
            get { return GetWrapper<CurrencyWrapper>(); }
            set { SetComplexValue<Currency, CurrencyWrapper>(Currency, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CurrencyWrapper>(nameof(Currency), Model.Currency == null ? null : new CurrencyWrapper(Model.Currency));

        }
	}

		public partial class CurrencyWrapper : WrapperBase<Currency>
	{
	    public CurrencyWrapper(Currency model) : base(model) { }

	
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

		public partial class ExchangeCurrencyRateWrapper : WrapperBase<ExchangeCurrencyRate>
	{
	    public ExchangeCurrencyRateWrapper(ExchangeCurrencyRate model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> FirstCurrencyId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> FirstCurrencyIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(FirstCurrencyId));
        public bool FirstCurrencyIdIsChanged => GetIsChanged(nameof(FirstCurrencyId));

        public System.Nullable<System.Guid> SecondCurrencyId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> SecondCurrencyIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(SecondCurrencyId));
        public bool SecondCurrencyIdIsChanged => GetIsChanged(nameof(SecondCurrencyId));

        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        public System.Double FirstCurrencyValue
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double FirstCurrencyValueOriginalValue => GetOriginalValue<System.Double>(nameof(FirstCurrencyValue));
        public bool FirstCurrencyValueIsChanged => GetIsChanged(nameof(FirstCurrencyValue));

        public System.Double SecondCurrencyValue
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double SecondCurrencyValueOriginalValue => GetOriginalValue<System.Double>(nameof(SecondCurrencyValue));
        public bool SecondCurrencyValueIsChanged => GetIsChanged(nameof(SecondCurrencyValue));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region ComplexProperties
	    public CurrencyWrapper FirstCurrency 
        {
            get { return GetWrapper<CurrencyWrapper>(); }
            set { SetComplexValue<Currency, CurrencyWrapper>(FirstCurrency, value); }
        }

	    public CurrencyWrapper SecondCurrency 
        {
            get { return GetWrapper<CurrencyWrapper>(); }
            set { SetComplexValue<Currency, CurrencyWrapper>(SecondCurrency, value); }
        }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CurrencyWrapper>(nameof(FirstCurrency), Model.FirstCurrency == null ? null : new CurrencyWrapper(Model.FirstCurrency));

            InitializeComplexProperty<CurrencyWrapper>(nameof(SecondCurrency), Model.SecondCurrency == null ? null : new CurrencyWrapper(Model.SecondCurrency));

        }
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

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

        public IValidatableChangeTrackingCollection<CostOnDateWrapper> Prices { get; private set; }

        public IValidatableChangeTrackingCollection<ProductWrapper> DependentProducts { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);

          if (Model.Prices == null) throw new ArgumentException("Prices cannot be null");
          Prices = new ValidatableChangeTrackingCollection<CostOnDateWrapper>(Model.Prices.Select(e => new CostOnDateWrapper(e)));
          RegisterCollection(Prices, Model.Prices);

          if (Model.DependentProducts == null) throw new ArgumentException("DependentProducts cannot be null");
          DependentProducts = new ValidatableChangeTrackingCollection<ProductWrapper>(Model.DependentProducts.Select(e => new ProductWrapper(e)));
          RegisterCollection(DependentProducts, Model.DependentProducts);

        }
	}

		public partial class OfferWrapper : WrapperBase<Offer>
	{
	    public OfferWrapper(Offer model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> TenderId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> TenderIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(TenderId));
        public bool TenderIdIsChanged => GetIsChanged(nameof(TenderId));

        public System.Nullable<System.Guid> RequestDocumentId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> RequestDocumentIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(RequestDocumentId));
        public bool RequestDocumentIdIsChanged => GetIsChanged(nameof(RequestDocumentId));

        public System.Nullable<System.Guid> AuthorId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> AuthorIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(AuthorId));
        public bool AuthorIdIsChanged => GetIsChanged(nameof(AuthorId));

        public System.Nullable<System.Guid> SenderEmployeeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> SenderEmployeeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(SenderEmployeeId));
        public bool SenderEmployeeIdIsChanged => GetIsChanged(nameof(SenderEmployeeId));

        public System.Nullable<System.Guid> RecipientEmployeeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> RecipientEmployeeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(RecipientEmployeeId));
        public bool RecipientEmployeeIdIsChanged => GetIsChanged(nameof(RecipientEmployeeId));

        public System.Nullable<System.Guid> RegistrationDetailsOfSenderId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> RegistrationDetailsOfSenderIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(RegistrationDetailsOfSenderId));
        public bool RegistrationDetailsOfSenderIdIsChanged => GetIsChanged(nameof(RegistrationDetailsOfSenderId));

        public System.Nullable<System.Guid> RegistrationDetailsOfRecipientId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> RegistrationDetailsOfRecipientIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(RegistrationDetailsOfRecipientId));
        public bool RegistrationDetailsOfRecipientIdIsChanged => GetIsChanged(nameof(RegistrationDetailsOfRecipientId));

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
	    public TenderWrapper Tender 
        {
            get { return GetWrapper<TenderWrapper>(); }
            set { SetComplexValue<Tender, TenderWrapper>(Tender, value); }
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

	    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfSender 
        {
            get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
            set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfSender, value); }
        }

	    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient 
        {
            get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
            set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfRecipient, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }

        public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<TenderWrapper>(nameof(Tender), Model.Tender == null ? null : new TenderWrapper(Model.Tender));

            InitializeComplexProperty<DocumentWrapper>(nameof(RequestDocument), Model.RequestDocument == null ? null : new DocumentWrapper(Model.RequestDocument));

            InitializeComplexProperty<EmployeeWrapper>(nameof(Author), Model.Author == null ? null : new EmployeeWrapper(Model.Author));

            InitializeComplexProperty<EmployeeWrapper>(nameof(SenderEmployee), Model.SenderEmployee == null ? null : new EmployeeWrapper(Model.SenderEmployee));

            InitializeComplexProperty<EmployeeWrapper>(nameof(RecipientEmployee), Model.RecipientEmployee == null ? null : new EmployeeWrapper(Model.RecipientEmployee));

            InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfSender), Model.RegistrationDetailsOfSender == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfSender));

            InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient), Model.RegistrationDetailsOfRecipient == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
          OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(Model.OfferUnits.Select(e => new OfferUnitWrapper(e)));
          RegisterCollection(OfferUnits, Model.OfferUnits);

          if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
          CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
          RegisterCollection(CopyToRecipients, Model.CopyToRecipients);

        }
	}

		public partial class EmployeeWrapper : WrapperBase<Employee>
	{
	    public EmployeeWrapper(Employee model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> CompanyId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> CompanyIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(CompanyId));
        public bool CompanyIdIsChanged => GetIsChanged(nameof(CompanyId));

        public System.Nullable<System.Guid> PositionId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> PositionIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(PositionId));
        public bool PositionIdIsChanged => GetIsChanged(nameof(PositionId));

        public System.Guid PersonId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid PersonIdOriginalValue => GetOriginalValue<System.Guid>(nameof(PersonId));
        public bool PersonIdIsChanged => GetIsChanged(nameof(PersonId));

        public System.Boolean IsActual
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsActualOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsActual));
        public bool IsActualIsChanged => GetIsChanged(nameof(IsActual));

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

        public System.DateTime OpenOrderDate
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime OpenOrderDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(OpenOrderDate));
        public bool OpenOrderDateIsChanged => GetIsChanged(nameof(OpenOrderDate));

        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProductionUnitWrapper> ProductionUnits { get; private set; }

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.ProductionUnits == null) throw new ArgumentException("ProductionUnits cannot be null");
          ProductionUnits = new ValidatableChangeTrackingCollection<ProductionUnitWrapper>(Model.ProductionUnits.Select(e => new ProductionUnitWrapper(e)));
          RegisterCollection(ProductionUnits, Model.ProductionUnits);

        }
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

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PaymentActualWrapper> Payments { get; private set; }

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
        public System.Nullable<System.Guid> TypeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> TypeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(TypeId));
        public bool TypeIdIsChanged => GetIsChanged(nameof(TypeId));

        public System.Nullable<System.Guid> OwnerCompanyId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> OwnerCompanyIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(OwnerCompanyId));
        public bool OwnerCompanyIdIsChanged => GetIsChanged(nameof(OwnerCompanyId));

        public System.Nullable<System.Guid> AddressId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> AddressIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(AddressId));
        public bool AddressIdIsChanged => GetIsChanged(nameof(AddressId));

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
        public System.Nullable<System.Guid> ManagerId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ManagerIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ManagerId));
        public bool ManagerIdIsChanged => GetIsChanged(nameof(ManagerId));

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
	    public UserWrapper Manager 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Manager, value); }
        }

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProjectUnitWrapper> ProjectUnits { get; private set; }

        public IValidatableChangeTrackingCollection<TenderWrapper> Tenders { get; private set; }

        public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(Manager), Model.Manager == null ? null : new UserWrapper(Model.Manager));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.ProjectUnits == null) throw new ArgumentException("ProjectUnits cannot be null");
          ProjectUnits = new ValidatableChangeTrackingCollection<ProjectUnitWrapper>(Model.ProjectUnits.Select(e => new ProjectUnitWrapper(e)));
          RegisterCollection(ProjectUnits, Model.ProjectUnits);

          if (Model.Tenders == null) throw new ArgumentException("Tenders cannot be null");
          Tenders = new ValidatableChangeTrackingCollection<TenderWrapper>(Model.Tenders.Select(e => new TenderWrapper(e)));
          RegisterCollection(Tenders, Model.Tenders);

          if (Model.Offers == null) throw new ArgumentException("Offers cannot be null");
          Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(Model.Offers.Select(e => new OfferWrapper(e)));
          RegisterCollection(Offers, Model.Offers);

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

        public HVTApp.Model.POCOs.Role Role
        {
          get { return GetValue<HVTApp.Model.POCOs.Role>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.Role RoleOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Role>(nameof(Role));
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
        public System.Nullable<System.Guid> ContractId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ContractIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ContractId));
        public bool ContractIdIsChanged => GetIsChanged(nameof(ContractId));

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
        public System.Nullable<System.Guid> TypeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> TypeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(TypeId));
        public bool TypeIdIsChanged => GetIsChanged(nameof(TypeId));

        public System.Nullable<System.Guid> ProjectId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProjectIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProjectId));
        public bool ProjectIdIsChanged => GetIsChanged(nameof(ProjectId));

        public System.Nullable<System.Guid> WinnerId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> WinnerIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(WinnerId));
        public bool WinnerIdIsChanged => GetIsChanged(nameof(WinnerId));

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
	    public TenderTypeWrapper Type 
        {
            get { return GetWrapper<TenderTypeWrapper>(); }
            set { SetComplexValue<TenderType, TenderTypeWrapper>(Type, value); }
        }

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
        public IValidatableChangeTrackingCollection<CompanyWrapper> Participants { get; private set; }

        public IValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<TenderTypeWrapper>(nameof(Type), Model.Type == null ? null : new TenderTypeWrapper(Model.Type));

            InitializeComplexProperty<ProjectWrapper>(nameof(Project), Model.Project == null ? null : new ProjectWrapper(Model.Project));

            InitializeComplexProperty<CompanyWrapper>(nameof(Winner), Model.Winner == null ? null : new CompanyWrapper(Model.Winner));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.Participants == null) throw new ArgumentException("Participants cannot be null");
          Participants = new ValidatableChangeTrackingCollection<CompanyWrapper>(Model.Participants.Select(e => new CompanyWrapper(e)));
          RegisterCollection(Participants, Model.Participants);

          if (Model.Offers == null) throw new ArgumentException("Offers cannot be null");
          Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(Model.Offers.Select(e => new OfferWrapper(e)));
          RegisterCollection(Offers, Model.Offers);

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
        public System.Nullable<System.Guid> EmployeeId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> EmployeeIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(EmployeeId));
        public bool EmployeeIdIsChanged => GetIsChanged(nameof(EmployeeId));

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

        public HVTApp.Model.POCOs.Role RoleCurrent
        {
          get { return GetValue<HVTApp.Model.POCOs.Role>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.Role RoleCurrentOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Role>(nameof(RoleCurrent));
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

		public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
	{
	    public OfferUnitWrapper(OfferUnit model) : base(model) { }

	
        #region SimpleProperties
        public System.Nullable<System.Guid> OfferId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> OfferIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(OfferId));
        public bool OfferIdIsChanged => GetIsChanged(nameof(OfferId));

        public System.Nullable<System.Guid> ProjectUnitId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProjectUnitIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProjectUnitId));
        public bool ProjectUnitIdIsChanged => GetIsChanged(nameof(ProjectUnitId));

        public System.Nullable<System.Guid> FacilityId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> FacilityIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(FacilityId));
        public bool FacilityIdIsChanged => GetIsChanged(nameof(FacilityId));

        public System.Nullable<System.Guid> ProductId
        {
          get { return GetValue<System.Nullable<System.Guid>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Guid> ProductIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProductId));
        public bool ProductIdIsChanged => GetIsChanged(nameof(ProductId));

        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public System.Int32 ProductionTerm
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductionTerm));
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

	    public ProjectUnitWrapper ProjectUnit 
        {
            get { return GetWrapper<ProjectUnitWrapper>(); }
            set { SetComplexValue<ProjectUnit, ProjectUnitWrapper>(ProjectUnit, value); }
        }

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

        #endregion

        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<OfferWrapper>(nameof(Offer), Model.Offer == null ? null : new OfferWrapper(Model.Offer));

            InitializeComplexProperty<ProjectUnitWrapper>(nameof(ProjectUnit), Model.ProjectUnit == null ? null : new ProjectUnitWrapper(Model.ProjectUnit));

            InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));

            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

        }
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
          PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
          RegisterCollection(PaymentsConditions, Model.PaymentsConditions);

        }
	}

		public partial class TenderUnitGroupWrapper : WrapperBase<TenderUnitGroup>
	{
	    public TenderUnitGroupWrapper(TenderUnitGroup model) : base(model) { }

	
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
        public IValidatableChangeTrackingCollection<TenderUnitWrapper> TenderUnits { get; private set; }

        #endregion

        #region GetProperties
        public System.Double Cost => GetValue<System.Double>(); 

        public HVTApp.Model.POCOs.Product Product => GetValue<HVTApp.Model.POCOs.Product>(); 

        public HVTApp.Model.POCOs.Facility Facility => GetValue<HVTApp.Model.POCOs.Facility>(); 

        #endregion
  
        protected override void InitializeCollectionProperties()
        {
          if (Model.TenderUnits == null) throw new ArgumentException("TenderUnits cannot be null");
          TenderUnits = new ValidatableChangeTrackingCollection<TenderUnitWrapper>(Model.TenderUnits.Select(e => new TenderUnitWrapper(e)));
          RegisterCollection(TenderUnits, Model.TenderUnits);

        }
	}

	}
