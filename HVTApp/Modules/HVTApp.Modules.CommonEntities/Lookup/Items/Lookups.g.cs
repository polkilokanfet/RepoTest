using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
	public partial class AddressLookup : LookupItem<Address>
	{
		public AddressLookup(Address entity) : base(entity) 
		{
			 		//Locality = new LocalityLookup(entity?.Locality);
		}
		
        #region SimpleProperties
        public System.String Description => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public LocalityLookup Locality { get { return GetLookup<LocalityLookup>(); } }

        #endregion
	}
	public partial class CountryLookup : LookupItem<Country>
	{
		public CountryLookup(Country entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion
	}
	public partial class DistrictLookup : LookupItem<District>
	{
		public DistrictLookup(District entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        public System.Guid CountryId => GetValue<System.Guid>();

        #endregion
	}
	public partial class LocalityLookup : LookupItem<Locality>
	{
		public LocalityLookup(Locality entity) : base(entity) 
		{
			 		//LocalityType = new LocalityTypeLookup(entity?.LocalityType);
		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        public System.Guid RegionId => GetValue<System.Guid>();

        public System.Boolean IsRegionCapital => GetValue<System.Boolean>();

        public System.Boolean IsDistrictsCapital => GetValue<System.Boolean>();

        public System.Boolean IsCountryCapital => GetValue<System.Boolean>();

        public System.Nullable<System.Double> StandartDeliveryPeriod => GetValue<System.Nullable<System.Double>>();

        #endregion

        #region ComplexProperties
	    public LocalityTypeLookup LocalityType { get { return GetLookup<LocalityTypeLookup>(); } }

        #endregion
	}
	public partial class LocalityTypeLookup : LookupItem<LocalityType>
	{
		public LocalityTypeLookup(LocalityType entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String FullName => GetValue<System.String>();

        public System.String ShortName => GetValue<System.String>();

        #endregion
	}
	public partial class RegionLookup : LookupItem<Region>
	{
		public RegionLookup(Region entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        public System.Guid DistrictId => GetValue<System.Guid>();

        #endregion
	}
	public partial class AdditionalSalesUnitsLookup : LookupItem<AdditionalSalesUnits>
	{
		public AdditionalSalesUnitsLookup(AdditionalSalesUnits entity) : base(entity) 
		{
			 		//AdditionalSalesUnit = new SalesUnitLookup(entity?.AdditionalSalesUnit);
		}
		
        #region ComplexProperties
	    public SalesUnitLookup AdditionalSalesUnit { get { return GetLookup<SalesUnitLookup>(); } }

        #endregion
	}
	public partial class BankDetailsLookup : LookupItem<BankDetails>
	{
		public BankDetailsLookup(BankDetails entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String BankName => GetValue<System.String>();

        public System.String BankIdentificationCode => GetValue<System.String>();

        public System.String CorrespondentAccount => GetValue<System.String>();

        public System.String CheckingAccount => GetValue<System.String>();

        #endregion
	}
	public partial class CompanyLookup : LookupItem<Company>
	{
		public CompanyLookup(Company entity) : base(entity) 
		{
			 		//Form = new CompanyFormLookup(entity?.Form);
		//ParentCompany = new CompanyLookup(entity?.ParentCompany);
		//AddressLegal = new AddressLookup(entity?.AddressLegal);
		//AddressPost = new AddressLookup(entity?.AddressPost);
		}
		
        #region SimpleProperties
        public System.String FullName => GetValue<System.String>();

        public System.String ShortName => GetValue<System.String>();

        public System.String Inn => GetValue<System.String>();

        public System.String Kpp => GetValue<System.String>();

        public System.Guid FormId => GetValue<System.Guid>();

        public System.Nullable<System.Guid> ParentCompanyId => GetValue<System.Nullable<System.Guid>>();

        #endregion

        #region ComplexProperties
	    public CompanyFormLookup Form { get { return GetLookup<CompanyFormLookup>(); } }

	    public CompanyLookup ParentCompany { get { return GetLookup<CompanyLookup>(); } }

	    public AddressLookup AddressLegal { get { return GetLookup<AddressLookup>(); } }

	    public AddressLookup AddressPost { get { return GetLookup<AddressLookup>(); } }

        #endregion
	}
	public partial class CompanyFormLookup : LookupItem<CompanyForm>
	{
		public CompanyFormLookup(CompanyForm entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String FullName => GetValue<System.String>();

        public System.String ShortName => GetValue<System.String>();

        #endregion
	}
	public partial class DocumentsRegistrationDetailsLookup : LookupItem<DocumentsRegistrationDetails>
	{
		public DocumentsRegistrationDetailsLookup(DocumentsRegistrationDetails entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String RegistrationNumber => GetValue<System.String>();

        public System.DateTime RegistrationDate => GetValue<System.DateTime>();

        #endregion
	}
	public partial class EmployeesPositionLookup : LookupItem<EmployeesPosition>
	{
		public EmployeesPositionLookup(EmployeesPosition entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion
	}
	public partial class FacilityTypeLookup : LookupItem<FacilityType>
	{
		public FacilityTypeLookup(FacilityType entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String FullName => GetValue<System.String>();

        public System.String ShortName => GetValue<System.String>();

        #endregion
	}
	public partial class ActivityFieldLookup : LookupItem<ActivityField>
	{
		public ActivityFieldLookup(ActivityField entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnum => GetValue<HVTApp.Model.POCOs.ActivityFieldEnum>();

        #endregion
	}
	public partial class ContractLookup : LookupItem<Contract>
	{
		public ContractLookup(Contract entity) : base(entity) 
		{
			 		//Contragent = new CompanyLookup(entity?.Contragent);
		}
		
        #region SimpleProperties
        public System.String Number => GetValue<System.String>();

        public System.DateTime Date => GetValue<System.DateTime>();

        #endregion

        #region ComplexProperties
	    public CompanyLookup Contragent { get { return GetLookup<CompanyLookup>(); } }

        #endregion
	}
	public partial class MeasureLookup : LookupItem<Measure>
	{
		public MeasureLookup(Measure entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String FullName => GetValue<System.String>();

        public System.String ShortName => GetValue<System.String>();

        #endregion
	}
	public partial class ParameterLookup : LookupItem<Parameter>
	{
		public ParameterLookup(Parameter entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.Guid GroupId => GetValue<System.Guid>();

        public System.String Value => GetValue<System.String>();

        #endregion
	}
	public partial class ParameterGroupLookup : LookupItem<ParameterGroup>
	{
		public ParameterGroupLookup(ParameterGroup entity) : base(entity) 
		{
			 		//Measure = new MeasureLookup(entity?.Measure);
		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public MeasureLookup Measure { get { return GetLookup<MeasureLookup>(); } }

        #endregion
	}
	public partial class ProductRelationLookup : LookupItem<ProductRelation>
	{
		public ProductRelationLookup(ProductRelation entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.Int32 Count => GetValue<System.Int32>();

        #endregion
	}
	public partial class StandartPaymentConditionsLookup : LookupItem<StandartPaymentConditions>
	{
		public StandartPaymentConditionsLookup(StandartPaymentConditions entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion
	}
	public partial class PersonLookup : LookupItem<Person>
	{
		public PersonLookup(Person entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Surname => GetValue<System.String>();

        public System.String Name => GetValue<System.String>();

        public System.String Patronymic => GetValue<System.String>();

        public System.Boolean IsMan => GetValue<System.Boolean>();

        #endregion
	}
	public partial class PaymentPlannedLookup : LookupItem<PaymentPlanned>
	{
		public PaymentPlannedLookup(PaymentPlanned entity) : base(entity) 
		{
			 		//Condition = new PaymentConditionLookup(entity?.Condition);
		}
		
        #region SimpleProperties
        public System.Guid SalesUnitId => GetValue<System.Guid>();

        public System.DateTime Date => GetValue<System.DateTime>();

        public System.Double Sum => GetValue<System.Double>();

        public System.String Comment => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public PaymentConditionLookup Condition { get { return GetLookup<PaymentConditionLookup>(); } }

        #endregion
	}
	public partial class PaymentActualLookup : LookupItem<PaymentActual>
	{
		public PaymentActualLookup(PaymentActual entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.Guid SalesUnitId => GetValue<System.Guid>();

        public System.DateTime Date => GetValue<System.DateTime>();

        public System.Double Sum => GetValue<System.Double>();

        public System.String Comment => GetValue<System.String>();

        public System.Guid DocumentId => GetValue<System.Guid>();

        #endregion
	}
	public partial class ParameterRelationLookup : LookupItem<ParameterRelation>
	{
		public ParameterRelationLookup(ParameterRelation entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.Guid ParameterId => GetValue<System.Guid>();

        #endregion
	}
	public partial class ProjectUnitLookup : LookupItem<ProjectUnit>
	{
		public ProjectUnitLookup(ProjectUnit entity) : base(entity) 
		{
			 		//Facility = new FacilityLookup(entity?.Facility);
		//Product = new ProductLookup(entity?.Product);
		}
		
        #region SimpleProperties
        public System.Guid ProjectId => GetValue<System.Guid>();

        public System.Double Cost => GetValue<System.Double>();

        public System.DateTime RequiredDeliveryDate => GetValue<System.DateTime>();

        #endregion

        #region ComplexProperties
	    public FacilityLookup Facility { get { return GetLookup<FacilityLookup>(); } }

	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

        #endregion
	}
	public partial class TenderUnitLookup : LookupItem<TenderUnit>
	{
		public TenderUnitLookup(TenderUnit entity) : base(entity) 
		{
			 		//ProjectUnit = new ProjectUnitLookup(entity?.ProjectUnit);
		//Facility = new FacilityLookup(entity?.Facility);
		//Product = new ProductLookup(entity?.Product);
		//ProducerWinner = new CompanyLookup(entity?.ProducerWinner);
		}
		
        #region SimpleProperties
        public System.Guid TenderId => GetValue<System.Guid>();

        public System.Double Cost => GetValue<System.Double>();

        public System.DateTime DeliveryDate => GetValue<System.DateTime>();

        #endregion

        #region ComplexProperties
	    public ProjectUnitLookup ProjectUnit { get { return GetLookup<ProjectUnitLookup>(); } }

	    public FacilityLookup Facility { get { return GetLookup<FacilityLookup>(); } }

	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

	    public CompanyLookup ProducerWinner { get { return GetLookup<CompanyLookup>(); } }

        #endregion
	}
	public partial class ShipmentUnitLookup : LookupItem<ShipmentUnit>
	{
		public ShipmentUnitLookup(ShipmentUnit entity) : base(entity) 
		{
			 		//Address = new AddressLookup(entity?.Address);
		}
		
        #region SimpleProperties
        public System.Nullable<System.Int32> ExpectedDeliveryPeriod => GetValue<System.Nullable<System.Int32>>();

        public System.Double Cost => GetValue<System.Double>();

        public System.Nullable<System.DateTime> ShipmentDate => GetValue<System.Nullable<System.DateTime>>();

        public System.Nullable<System.DateTime> ShipmentPlanDate => GetValue<System.Nullable<System.DateTime>>();

        public System.Nullable<System.DateTime> RequiredDeliveryDate => GetValue<System.Nullable<System.DateTime>>();

        public System.Nullable<System.DateTime> DeliveryDate => GetValue<System.Nullable<System.DateTime>>();

        #endregion

        #region ComplexProperties
	    public AddressLookup Address { get { return GetLookup<AddressLookup>(); } }

        #endregion
	}
	public partial class ProductionUnitLookup : LookupItem<ProductionUnit>
	{
		public ProductionUnitLookup(ProductionUnit entity) : base(entity) 
		{
			 		//Product = new ProductLookup(entity?.Product);
		}
		
        #region SimpleProperties
        public System.Guid OrderId => GetValue<System.Guid>();

        public System.Int32 OrderPosition => GetValue<System.Int32>();

        public System.String SerialNumber => GetValue<System.String>();

        public System.Int32 PlannedTermFromStartToEndProduction => GetValue<System.Int32>();

        public System.Int32 PlannedTermFromPickToEndProduction => GetValue<System.Int32>();

        public System.Nullable<System.DateTime> StartProductionDate => GetValue<System.Nullable<System.DateTime>>();

        public System.Nullable<System.DateTime> PickingDate => GetValue<System.Nullable<System.DateTime>>();

        public System.Nullable<System.DateTime> EndProductionDate => GetValue<System.Nullable<System.DateTime>>();

        public System.Nullable<System.DateTime> EndProductionDateByPlan => GetValue<System.Nullable<System.DateTime>>();

        #endregion

        #region ComplexProperties
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

        #endregion
	}
	public partial class SalesUnitLookup : LookupItem<SalesUnit>
	{
		public SalesUnitLookup(SalesUnit entity) : base(entity) 
		{
			 		//OfferUnit = new OfferUnitLookup(entity?.OfferUnit);
		//ProductionUnit = new ProductionUnitLookup(entity?.ProductionUnit);
		//ShipmentUnit = new ShipmentUnitLookup(entity?.ShipmentUnit);
		}
		
        #region SimpleProperties
        public System.Guid OfferUnitId => GetValue<System.Guid>();

        public System.Double Cost => GetValue<System.Double>();

        public System.Guid SpecificationId => GetValue<System.Guid>();

        public System.Nullable<System.DateTime> RealizationDate => GetValue<System.Nullable<System.DateTime>>();

        #endregion

        #region ComplexProperties
	    public OfferUnitLookup OfferUnit { get { return GetLookup<OfferUnitLookup>(); } }

	    public ProductionUnitLookup ProductionUnit { get { return GetLookup<ProductionUnitLookup>(); } }

	    public ShipmentUnitLookup ShipmentUnit { get { return GetLookup<ShipmentUnitLookup>(); } }

        #endregion
	}
	public partial class TestFriendAddressLookup : LookupItem<TestFriendAddress>
	{
		public TestFriendAddressLookup(TestFriendAddress entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String City => GetValue<System.String>();

        public System.String Street => GetValue<System.String>();

        public System.String StreetNumber => GetValue<System.String>();

        #endregion
	}
	public partial class TestFriendLookup : LookupItem<TestFriend>
	{
		public TestFriendLookup(TestFriend entity) : base(entity) 
		{
			 		//TestFriendAddress = new TestFriendAddressLookup(entity?.TestFriendAddress);
		//TestFriendGroup = new TestFriendGroupLookup(entity?.TestFriendGroup);
		//TestFriendEmailGet = new TestFriendEmailLookup(entity?.TestFriendEmailGet);
		}
		
        #region SimpleProperties
        public System.Int32 FriendGroupId => GetValue<System.Int32>();

        public System.String FirstName => GetValue<System.String>();

        public System.String LastName => GetValue<System.String>();

        public System.Nullable<System.DateTime> Birthday => GetValue<System.Nullable<System.DateTime>>();

        public System.Boolean IsDeveloper => GetValue<System.Boolean>();

        public System.Int32 IdGet => GetValue<System.Int32>();

        #endregion

        #region ComplexProperties
	    public TestFriendAddressLookup TestFriendAddress { get { return GetLookup<TestFriendAddressLookup>(); } }

	    public TestFriendGroupLookup TestFriendGroup { get { return GetLookup<TestFriendGroupLookup>(); } }

	    public TestFriendEmailLookup TestFriendEmailGet { get { return GetLookup<TestFriendEmailLookup>(); } }

        #endregion
	}
	public partial class TestFriendEmailLookup : LookupItem<TestFriendEmail>
	{
		public TestFriendEmailLookup(TestFriendEmail entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Email => GetValue<System.String>();

        public System.String Comment => GetValue<System.String>();

        #endregion
	}
	public partial class TestFriendGroupLookup : LookupItem<TestFriendGroup>
	{
		public TestFriendGroupLookup(TestFriendGroup entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion
	}
	public partial class DocumentLookup : LookupItem<Document>
	{
		public DocumentLookup(Document entity) : base(entity) 
		{
			 		//RequestDocument = new DocumentLookup(entity?.RequestDocument);
		//Author = new EmployeeLookup(entity?.Author);
		//SenderEmployee = new EmployeeLookup(entity?.SenderEmployee);
		//RecipientEmployee = new EmployeeLookup(entity?.RecipientEmployee);
		//RegistrationDetailsOfSender = new DocumentsRegistrationDetailsLookup(entity?.RegistrationDetailsOfSender);
		//RegistrationDetailsOfRecipient = new DocumentsRegistrationDetailsLookup(entity?.RegistrationDetailsOfRecipient);
		}
		
        #region SimpleProperties
        public System.Nullable<System.Guid> AuthorId => GetValue<System.Nullable<System.Guid>>();

        public System.Guid SenderId => GetValue<System.Guid>();

        public System.Guid RecipientId => GetValue<System.Guid>();

        public System.Guid RegistrationDetailsOfSenderId => GetValue<System.Guid>();

        public System.Guid RegistrationDetailsOfRecipientId => GetValue<System.Guid>();

        public System.String Comment => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public DocumentLookup RequestDocument { get { return GetLookup<DocumentLookup>(); } }

	    public EmployeeLookup Author { get { return GetLookup<EmployeeLookup>(); } }

	    public EmployeeLookup SenderEmployee { get { return GetLookup<EmployeeLookup>(); } }

	    public EmployeeLookup RecipientEmployee { get { return GetLookup<EmployeeLookup>(); } }

	    public DocumentsRegistrationDetailsLookup RegistrationDetailsOfSender { get { return GetLookup<DocumentsRegistrationDetailsLookup>(); } }

	    public DocumentsRegistrationDetailsLookup RegistrationDetailsOfRecipient { get { return GetLookup<DocumentsRegistrationDetailsLookup>(); } }

        #endregion
	}
	public partial class TestEntityLookup : LookupItem<TestEntity>
	{
		public TestEntityLookup(TestEntity entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion
	}
	public partial class TestHusbandLookup : LookupItem<TestHusband>
	{
		public TestHusbandLookup(TestHusband entity) : base(entity) 
		{
			 		//Wife = new TestWifeLookup(entity?.Wife);
		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public TestWifeLookup Wife { get { return GetLookup<TestWifeLookup>(); } }

        #endregion
	}
	public partial class TestWifeLookup : LookupItem<TestWife>
	{
		public TestWifeLookup(TestWife entity) : base(entity) 
		{
			 		//Husband = new TestHusbandLookup(entity?.Husband);
		}
		
        #region SimpleProperties
        public System.Int32 N => GetValue<System.Int32>();

        public System.String Name => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public TestHusbandLookup Husband { get { return GetLookup<TestHusbandLookup>(); } }

        #endregion
	}
	public partial class TestChildLookup : LookupItem<TestChild>
	{
		public TestChildLookup(TestChild entity) : base(entity) 
		{
			 		//Husband = new TestHusbandLookup(entity?.Husband);
		//Wife = new TestWifeLookup(entity?.Wife);
		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public TestHusbandLookup Husband { get { return GetLookup<TestHusbandLookup>(); } }

	    public TestWifeLookup Wife { get { return GetLookup<TestWifeLookup>(); } }

        #endregion
	}
	public partial class CostOnDateLookup : LookupItem<CostOnDate>
	{
		public CostOnDateLookup(CostOnDate entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.DateTime Date => GetValue<System.DateTime>();

        public System.Double Cost => GetValue<System.Double>();

        #endregion
	}
	public partial class CostLookup : LookupItem<Cost>
	{
		public CostLookup(Cost entity) : base(entity) 
		{
			 		//Currency = new CurrencyLookup(entity?.Currency);
		}
		
        #region SimpleProperties
        public System.Double Sum => GetValue<System.Double>();

        #endregion

        #region ComplexProperties
	    public CurrencyLookup Currency { get { return GetLookup<CurrencyLookup>(); } }

        #endregion
	}
	public partial class CurrencyLookup : LookupItem<Currency>
	{
		public CurrencyLookup(Currency entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String FullName => GetValue<System.String>();

        public System.String ShortName => GetValue<System.String>();

        #endregion
	}
	public partial class ExchangeCurrencyRateLookup : LookupItem<ExchangeCurrencyRate>
	{
		public ExchangeCurrencyRateLookup(ExchangeCurrencyRate entity) : base(entity) 
		{
			 		//FirstCurrency = new CurrencyLookup(entity?.FirstCurrency);
		//SecondCurrency = new CurrencyLookup(entity?.SecondCurrency);
		}
		
        #region SimpleProperties
        public System.DateTime Date => GetValue<System.DateTime>();

        public System.Double FirstCurrencyValue => GetValue<System.Double>();

        public System.Double SecondCurrencyValue => GetValue<System.Double>();

        #endregion

        #region ComplexProperties
	    public CurrencyLookup FirstCurrency { get { return GetLookup<CurrencyLookup>(); } }

	    public CurrencyLookup SecondCurrency { get { return GetLookup<CurrencyLookup>(); } }

        #endregion
	}
	public partial class ProductLookup : LookupItem<Product>
	{
		public ProductLookup(Product entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Designation => GetValue<System.String>();

        public System.String StructureCostNumber => GetValue<System.String>();

        #endregion
	}
	public partial class OfferLookup : LookupItem<Offer>
	{
		public OfferLookup(Offer entity) : base(entity) 
		{
			 		//RequestDocument = new DocumentLookup(entity?.RequestDocument);
		//Author = new EmployeeLookup(entity?.Author);
		//SenderEmployee = new EmployeeLookup(entity?.SenderEmployee);
		//RecipientEmployee = new EmployeeLookup(entity?.RecipientEmployee);
		//RegistrationDetailsOfSender = new DocumentsRegistrationDetailsLookup(entity?.RegistrationDetailsOfSender);
		//RegistrationDetailsOfRecipient = new DocumentsRegistrationDetailsLookup(entity?.RegistrationDetailsOfRecipient);
		}
		
        #region SimpleProperties
        public System.DateTime ValidityDate => GetValue<System.DateTime>();

        public System.Double Vat => GetValue<System.Double>();

        public System.Nullable<System.Guid> AuthorId => GetValue<System.Nullable<System.Guid>>();

        public System.Guid SenderId => GetValue<System.Guid>();

        public System.Guid RecipientId => GetValue<System.Guid>();

        public System.Guid RegistrationDetailsOfSenderId => GetValue<System.Guid>();

        public System.Guid RegistrationDetailsOfRecipientId => GetValue<System.Guid>();

        public System.String Comment => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public DocumentLookup RequestDocument { get { return GetLookup<DocumentLookup>(); } }

	    public EmployeeLookup Author { get { return GetLookup<EmployeeLookup>(); } }

	    public EmployeeLookup SenderEmployee { get { return GetLookup<EmployeeLookup>(); } }

	    public EmployeeLookup RecipientEmployee { get { return GetLookup<EmployeeLookup>(); } }

	    public DocumentsRegistrationDetailsLookup RegistrationDetailsOfSender { get { return GetLookup<DocumentsRegistrationDetailsLookup>(); } }

	    public DocumentsRegistrationDetailsLookup RegistrationDetailsOfRecipient { get { return GetLookup<DocumentsRegistrationDetailsLookup>(); } }

        #endregion
	}
	public partial class EmployeeLookup : LookupItem<Employee>
	{
		public EmployeeLookup(Employee entity) : base(entity) 
		{
			 		//Company = new CompanyLookup(entity?.Company);
		//Position = new EmployeesPositionLookup(entity?.Position);
		}
		
        #region SimpleProperties
        public System.Guid PersonId => GetValue<System.Guid>();

        public System.Boolean IsActual => GetValue<System.Boolean>();

        public System.String PhoneNumber => GetValue<System.String>();

        public System.String Email => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public CompanyLookup Company { get { return GetLookup<CompanyLookup>(); } }

	    public EmployeesPositionLookup Position { get { return GetLookup<EmployeesPositionLookup>(); } }

        #endregion
	}
	public partial class OrderLookup : LookupItem<Order>
	{
		public OrderLookup(Order entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Number => GetValue<System.String>();

        public System.DateTime OpenOrderDate => GetValue<System.DateTime>();

        #endregion
	}
	public partial class PaymentConditionLookup : LookupItem<PaymentCondition>
	{
		public PaymentConditionLookup(PaymentCondition entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.Double Part => GetValue<System.Double>();

        public System.Int32 DaysToPoint => GetValue<System.Int32>();

        public HVTApp.Model.POCOs.PaymentConditionPoint PaymentConditionPoint => GetValue<HVTApp.Model.POCOs.PaymentConditionPoint>();

        #endregion
	}
	public partial class PaymentDocumentLookup : LookupItem<PaymentDocument>
	{
		public PaymentDocumentLookup(PaymentDocument entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Number => GetValue<System.String>();

        public System.DateTime Date => GetValue<System.DateTime>();

        #endregion
	}
	public partial class FacilityLookup : LookupItem<Facility>
	{
		public FacilityLookup(Facility entity) : base(entity) 
		{
			 		//Type = new FacilityTypeLookup(entity?.Type);
		//OwnerCompany = new CompanyLookup(entity?.OwnerCompany);
		//Address = new AddressLookup(entity?.Address);
		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public FacilityTypeLookup Type { get { return GetLookup<FacilityTypeLookup>(); } }

	    public CompanyLookup OwnerCompany { get { return GetLookup<CompanyLookup>(); } }

	    public AddressLookup Address { get { return GetLookup<AddressLookup>(); } }

        #endregion
	}
	public partial class ProjectLookup : LookupItem<Project>
	{
		public ProjectLookup(Project entity) : base(entity) 
		{
			 		//Manager = new UserLookup(entity?.Manager);
		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        #endregion

        #region ComplexProperties
	    public UserLookup Manager { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	public partial class UserRoleLookup : LookupItem<UserRole>
	{
		public UserRoleLookup(UserRole entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        public HVTApp.Model.POCOs.Role Role => GetValue<HVTApp.Model.POCOs.Role>();

        #endregion
	}
	public partial class SpecificationLookup : LookupItem<Specification>
	{
		public SpecificationLookup(Specification entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Number => GetValue<System.String>();

        public System.DateTime Date => GetValue<System.DateTime>();

        public System.Double Vat => GetValue<System.Double>();

        public System.Guid ContractId => GetValue<System.Guid>();

        #endregion
	}
	public partial class TenderLookup : LookupItem<Tender>
	{
		public TenderLookup(Tender entity) : base(entity) 
		{
			 		//Type = new TenderTypeLookup(entity?.Type);
		//Winner = new CompanyLookup(entity?.Winner);
		}
		
        #region SimpleProperties
        public System.Guid ProjectId => GetValue<System.Guid>();

        public System.DateTime DateOpen => GetValue<System.DateTime>();

        public System.DateTime DateClose => GetValue<System.DateTime>();

        public System.Nullable<System.DateTime> DateNotice => GetValue<System.Nullable<System.DateTime>>();

        #endregion

        #region ComplexProperties
	    public TenderTypeLookup Type { get { return GetLookup<TenderTypeLookup>(); } }

	    public CompanyLookup Winner { get { return GetLookup<CompanyLookup>(); } }

        #endregion
	}
	public partial class TenderTypeLookup : LookupItem<TenderType>
	{
		public TenderTypeLookup(TenderType entity) : base(entity) 
		{
			 		}
		
        #region SimpleProperties
        public System.String Name => GetValue<System.String>();

        public HVTApp.Model.POCOs.TenderTypeEnum Type => GetValue<HVTApp.Model.POCOs.TenderTypeEnum>();

        #endregion
	}
	public partial class UserLookup : LookupItem<User>
	{
		public UserLookup(User entity) : base(entity) 
		{
			 		//Employee = new EmployeeLookup(entity?.Employee);
		}
		
        #region SimpleProperties
        public System.String Login => GetValue<System.String>();

        public System.Guid Password => GetValue<System.Guid>();

        public System.String PersonalNumber => GetValue<System.String>();

        public HVTApp.Model.POCOs.Role RoleCurrent => GetValue<HVTApp.Model.POCOs.Role>();

        #endregion

        #region ComplexProperties
	    public EmployeeLookup Employee { get { return GetLookup<EmployeeLookup>(); } }

        #endregion
	}
	public partial class OfferUnitLookup : LookupItem<OfferUnit>
	{
		public OfferUnitLookup(OfferUnit entity) : base(entity) 
		{
			 		//ProjectUnit = new ProjectUnitLookup(entity?.ProjectUnit);
		//Facility = new FacilityLookup(entity?.Facility);
		//Product = new ProductLookup(entity?.Product);
		}
		
        #region SimpleProperties
        public System.Guid OfferId => GetValue<System.Guid>();

        public System.Double Cost => GetValue<System.Double>();

        public System.Int32 ProductionTerm => GetValue<System.Int32>();

        #endregion

        #region ComplexProperties
	    public ProjectUnitLookup ProjectUnit { get { return GetLookup<ProjectUnitLookup>(); } }

	    public FacilityLookup Facility { get { return GetLookup<FacilityLookup>(); } }

	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

        #endregion
	}
}
