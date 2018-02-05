using HVTApp.DataAccess;

namespace HVTApp.UI.Wrapper
{
    public partial class WrapperDataService
    {
        public WrapperDataService(IUnitOfWork unitOfWork)
        {
            AddressWrapperDataService = new AddressWrapperDataService(unitOfWork);
            CountryWrapperDataService = new CountryWrapperDataService(unitOfWork);
            DistrictWrapperDataService = new DistrictWrapperDataService(unitOfWork);
            LocalityWrapperDataService = new LocalityWrapperDataService(unitOfWork);
            LocalityTypeWrapperDataService = new LocalityTypeWrapperDataService(unitOfWork);
            RegionWrapperDataService = new RegionWrapperDataService(unitOfWork);
            AdditionalSalesUnitsWrapperDataService = new AdditionalSalesUnitsWrapperDataService(unitOfWork);
            BankDetailsWrapperDataService = new BankDetailsWrapperDataService(unitOfWork);
            CompanyWrapperDataService = new CompanyWrapperDataService(unitOfWork);
            CompanyFormWrapperDataService = new CompanyFormWrapperDataService(unitOfWork);
            DocumentsRegistrationDetailsWrapperDataService = new DocumentsRegistrationDetailsWrapperDataService(unitOfWork);
            EmployeesPositionWrapperDataService = new EmployeesPositionWrapperDataService(unitOfWork);
            FacilityTypeWrapperDataService = new FacilityTypeWrapperDataService(unitOfWork);
            ActivityFieldWrapperDataService = new ActivityFieldWrapperDataService(unitOfWork);
            ContractWrapperDataService = new ContractWrapperDataService(unitOfWork);
            MeasureWrapperDataService = new MeasureWrapperDataService(unitOfWork);
            ParameterWrapperDataService = new ParameterWrapperDataService(unitOfWork);
            ParameterGroupWrapperDataService = new ParameterGroupWrapperDataService(unitOfWork);
            ProductRelationWrapperDataService = new ProductRelationWrapperDataService(unitOfWork);
            StandartPaymentConditionsWrapperDataService = new StandartPaymentConditionsWrapperDataService(unitOfWork);
            PersonWrapperDataService = new PersonWrapperDataService(unitOfWork);
            PaymentPlannedWrapperDataService = new PaymentPlannedWrapperDataService(unitOfWork);
            PaymentActualWrapperDataService = new PaymentActualWrapperDataService(unitOfWork);
            ParameterRelationWrapperDataService = new ParameterRelationWrapperDataService(unitOfWork);
            ProjectUnitWrapperDataService = new ProjectUnitWrapperDataService(unitOfWork);
            ShipmentUnitWrapperDataService = new ShipmentUnitWrapperDataService(unitOfWork);
            ProductionUnitWrapperDataService = new ProductionUnitWrapperDataService(unitOfWork);
            SalesUnitWrapperDataService = new SalesUnitWrapperDataService(unitOfWork);
            TestFriendAddressWrapperDataService = new TestFriendAddressWrapperDataService(unitOfWork);
            TestFriendWrapperDataService = new TestFriendWrapperDataService(unitOfWork);
            TestFriendEmailWrapperDataService = new TestFriendEmailWrapperDataService(unitOfWork);
            TestFriendGroupWrapperDataService = new TestFriendGroupWrapperDataService(unitOfWork);
            DocumentWrapperDataService = new DocumentWrapperDataService(unitOfWork);
            TestEntityWrapperDataService = new TestEntityWrapperDataService(unitOfWork);
            TestHusbandWrapperDataService = new TestHusbandWrapperDataService(unitOfWork);
            TestWifeWrapperDataService = new TestWifeWrapperDataService(unitOfWork);
            TestChildWrapperDataService = new TestChildWrapperDataService(unitOfWork);
            CostOnDateWrapperDataService = new CostOnDateWrapperDataService(unitOfWork);
            CostWrapperDataService = new CostWrapperDataService(unitOfWork);
            CurrencyWrapperDataService = new CurrencyWrapperDataService(unitOfWork);
            ExchangeCurrencyRateWrapperDataService = new ExchangeCurrencyRateWrapperDataService(unitOfWork);
            ProductWrapperDataService = new ProductWrapperDataService(unitOfWork);
            OfferWrapperDataService = new OfferWrapperDataService(unitOfWork);
            EmployeeWrapperDataService = new EmployeeWrapperDataService(unitOfWork);
            OrderWrapperDataService = new OrderWrapperDataService(unitOfWork);
            PaymentConditionWrapperDataService = new PaymentConditionWrapperDataService(unitOfWork);
            PaymentDocumentWrapperDataService = new PaymentDocumentWrapperDataService(unitOfWork);
            FacilityWrapperDataService = new FacilityWrapperDataService(unitOfWork);
            ProjectWrapperDataService = new ProjectWrapperDataService(unitOfWork);
            UserRoleWrapperDataService = new UserRoleWrapperDataService(unitOfWork);
            SpecificationWrapperDataService = new SpecificationWrapperDataService(unitOfWork);
            TenderWrapperDataService = new TenderWrapperDataService(unitOfWork);
            TenderTypeWrapperDataService = new TenderTypeWrapperDataService(unitOfWork);
            UserWrapperDataService = new UserWrapperDataService(unitOfWork);
            OfferUnitWrapperDataService = new OfferUnitWrapperDataService(unitOfWork);
            ProjectUnitGroupWrapperDataService = new ProjectUnitGroupWrapperDataService(unitOfWork);
        }

        public AddressWrapperDataService AddressWrapperDataService { get; }
        public CountryWrapperDataService CountryWrapperDataService { get; }
        public DistrictWrapperDataService DistrictWrapperDataService { get; }
        public LocalityWrapperDataService LocalityWrapperDataService { get; }
        public LocalityTypeWrapperDataService LocalityTypeWrapperDataService { get; }
        public RegionWrapperDataService RegionWrapperDataService { get; }
        public AdditionalSalesUnitsWrapperDataService AdditionalSalesUnitsWrapperDataService { get; }
        public BankDetailsWrapperDataService BankDetailsWrapperDataService { get; }
        public CompanyWrapperDataService CompanyWrapperDataService { get; }
        public CompanyFormWrapperDataService CompanyFormWrapperDataService { get; }
        public DocumentsRegistrationDetailsWrapperDataService DocumentsRegistrationDetailsWrapperDataService { get; }
        public EmployeesPositionWrapperDataService EmployeesPositionWrapperDataService { get; }
        public FacilityTypeWrapperDataService FacilityTypeWrapperDataService { get; }
        public ActivityFieldWrapperDataService ActivityFieldWrapperDataService { get; }
        public ContractWrapperDataService ContractWrapperDataService { get; }
        public MeasureWrapperDataService MeasureWrapperDataService { get; }
        public ParameterWrapperDataService ParameterWrapperDataService { get; }
        public ParameterGroupWrapperDataService ParameterGroupWrapperDataService { get; }
        public ProductRelationWrapperDataService ProductRelationWrapperDataService { get; }
        public StandartPaymentConditionsWrapperDataService StandartPaymentConditionsWrapperDataService { get; }
        public PersonWrapperDataService PersonWrapperDataService { get; }
        public PaymentPlannedWrapperDataService PaymentPlannedWrapperDataService { get; }
        public PaymentActualWrapperDataService PaymentActualWrapperDataService { get; }
        public ParameterRelationWrapperDataService ParameterRelationWrapperDataService { get; }
        public ProjectUnitWrapperDataService ProjectUnitWrapperDataService { get; }
        public ShipmentUnitWrapperDataService ShipmentUnitWrapperDataService { get; }
        public ProductionUnitWrapperDataService ProductionUnitWrapperDataService { get; }
        public SalesUnitWrapperDataService SalesUnitWrapperDataService { get; }
        public TestFriendAddressWrapperDataService TestFriendAddressWrapperDataService { get; }
        public TestFriendWrapperDataService TestFriendWrapperDataService { get; }
        public TestFriendEmailWrapperDataService TestFriendEmailWrapperDataService { get; }
        public TestFriendGroupWrapperDataService TestFriendGroupWrapperDataService { get; }
        public DocumentWrapperDataService DocumentWrapperDataService { get; }
        public TestEntityWrapperDataService TestEntityWrapperDataService { get; }
        public TestHusbandWrapperDataService TestHusbandWrapperDataService { get; }
        public TestWifeWrapperDataService TestWifeWrapperDataService { get; }
        public TestChildWrapperDataService TestChildWrapperDataService { get; }
        public CostOnDateWrapperDataService CostOnDateWrapperDataService { get; }
        public CostWrapperDataService CostWrapperDataService { get; }
        public CurrencyWrapperDataService CurrencyWrapperDataService { get; }
        public ExchangeCurrencyRateWrapperDataService ExchangeCurrencyRateWrapperDataService { get; }
        public ProductWrapperDataService ProductWrapperDataService { get; }
        public OfferWrapperDataService OfferWrapperDataService { get; }
        public EmployeeWrapperDataService EmployeeWrapperDataService { get; }
        public OrderWrapperDataService OrderWrapperDataService { get; }
        public PaymentConditionWrapperDataService PaymentConditionWrapperDataService { get; }
        public PaymentDocumentWrapperDataService PaymentDocumentWrapperDataService { get; }
        public FacilityWrapperDataService FacilityWrapperDataService { get; }
        public ProjectWrapperDataService ProjectWrapperDataService { get; }
        public UserRoleWrapperDataService UserRoleWrapperDataService { get; }
        public SpecificationWrapperDataService SpecificationWrapperDataService { get; }
        public TenderWrapperDataService TenderWrapperDataService { get; }
        public TenderTypeWrapperDataService TenderTypeWrapperDataService { get; }
        public UserWrapperDataService UserWrapperDataService { get; }
        public OfferUnitWrapperDataService OfferUnitWrapperDataService { get; }
        public ProjectUnitGroupWrapperDataService ProjectUnitGroupWrapperDataService { get; }
    }
}
