using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public partial class WrapperDataService
    {
        public WrapperDataService(IUnitOfWork unitOfWork)
        {
            CommonOptionWrapperDataService = new CommonOptionWrapperDataService(unitOfWork);
            AddressWrapperDataService = new AddressWrapperDataService(unitOfWork);
            CountryWrapperDataService = new CountryWrapperDataService(unitOfWork);
            DistrictWrapperDataService = new DistrictWrapperDataService(unitOfWork);
            LocalityWrapperDataService = new LocalityWrapperDataService(unitOfWork);
            LocalityTypeWrapperDataService = new LocalityTypeWrapperDataService(unitOfWork);
            RegionWrapperDataService = new RegionWrapperDataService(unitOfWork);
            CalculatePriceTaskWrapperDataService = new CalculatePriceTaskWrapperDataService(unitOfWork);
            SumWrapperDataService = new SumWrapperDataService(unitOfWork);
            CurrencyExchangeRateWrapperDataService = new CurrencyExchangeRateWrapperDataService(unitOfWork);
            DescribeProductBlockTaskWrapperDataService = new DescribeProductBlockTaskWrapperDataService(unitOfWork);
            NoteWrapperDataService = new NoteWrapperDataService(unitOfWork);
            OfferUnitWrapperDataService = new OfferUnitWrapperDataService(unitOfWork);
            PaymentConditionSetWrapperDataService = new PaymentConditionSetWrapperDataService(unitOfWork);
            ProductBlockWrapperDataService = new ProductBlockWrapperDataService(unitOfWork);
            ProductDependentWrapperDataService = new ProductDependentWrapperDataService(unitOfWork);
            ProductionTaskWrapperDataService = new ProductionTaskWrapperDataService(unitOfWork);
            SalesBlockWrapperDataService = new SalesBlockWrapperDataService(unitOfWork);
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
            PersonWrapperDataService = new PersonWrapperDataService(unitOfWork);
            PaymentPlannedListWrapperDataService = new PaymentPlannedListWrapperDataService(unitOfWork);
            PaymentPlannedWrapperDataService = new PaymentPlannedWrapperDataService(unitOfWork);
            PaymentActualWrapperDataService = new PaymentActualWrapperDataService(unitOfWork);
            ParameterRelationWrapperDataService = new ParameterRelationWrapperDataService(unitOfWork);
            SalesUnitWrapperDataService = new SalesUnitWrapperDataService(unitOfWork);
            ServiceWrapperDataService = new ServiceWrapperDataService(unitOfWork);
            TestFriendAddressWrapperDataService = new TestFriendAddressWrapperDataService(unitOfWork);
            TestFriendWrapperDataService = new TestFriendWrapperDataService(unitOfWork);
            TestFriendEmailWrapperDataService = new TestFriendEmailWrapperDataService(unitOfWork);
            TestFriendGroupWrapperDataService = new TestFriendGroupWrapperDataService(unitOfWork);
            DocumentWrapperDataService = new DocumentWrapperDataService(unitOfWork);
            TestEntityWrapperDataService = new TestEntityWrapperDataService(unitOfWork);
            TestHusbandWrapperDataService = new TestHusbandWrapperDataService(unitOfWork);
            TestWifeWrapperDataService = new TestWifeWrapperDataService(unitOfWork);
            TestChildWrapperDataService = new TestChildWrapperDataService(unitOfWork);
            SumOnDateWrapperDataService = new SumOnDateWrapperDataService(unitOfWork);
            ProductWrapperDataService = new ProductWrapperDataService(unitOfWork);
            OfferWrapperDataService = new OfferWrapperDataService(unitOfWork);
            EmployeeWrapperDataService = new EmployeeWrapperDataService(unitOfWork);
            OrderWrapperDataService = new OrderWrapperDataService(unitOfWork);
            PaymentConditionWrapperDataService = new PaymentConditionWrapperDataService(unitOfWork);
            PaymentDocumentWrapperDataService = new PaymentDocumentWrapperDataService(unitOfWork);
            FacilityWrapperDataService = new FacilityWrapperDataService(unitOfWork);
            ProjectWrapperDataService = new ProjectWrapperDataService(unitOfWork);
            ProjectUnitWrapperDataService = new ProjectUnitWrapperDataService(unitOfWork);
            UserRoleWrapperDataService = new UserRoleWrapperDataService(unitOfWork);
            SpecificationWrapperDataService = new SpecificationWrapperDataService(unitOfWork);
            TenderWrapperDataService = new TenderWrapperDataService(unitOfWork);
            TenderTypeWrapperDataService = new TenderTypeWrapperDataService(unitOfWork);
            UserWrapperDataService = new UserWrapperDataService(unitOfWork);
        }

        public CommonOptionWrapperDataService CommonOptionWrapperDataService { get; }
        public AddressWrapperDataService AddressWrapperDataService { get; }
        public CountryWrapperDataService CountryWrapperDataService { get; }
        public DistrictWrapperDataService DistrictWrapperDataService { get; }
        public LocalityWrapperDataService LocalityWrapperDataService { get; }
        public LocalityTypeWrapperDataService LocalityTypeWrapperDataService { get; }
        public RegionWrapperDataService RegionWrapperDataService { get; }
        public CalculatePriceTaskWrapperDataService CalculatePriceTaskWrapperDataService { get; }
        public SumWrapperDataService SumWrapperDataService { get; }
        public CurrencyExchangeRateWrapperDataService CurrencyExchangeRateWrapperDataService { get; }
        public DescribeProductBlockTaskWrapperDataService DescribeProductBlockTaskWrapperDataService { get; }
        public NoteWrapperDataService NoteWrapperDataService { get; }
        public OfferUnitWrapperDataService OfferUnitWrapperDataService { get; }
        public PaymentConditionSetWrapperDataService PaymentConditionSetWrapperDataService { get; }
        public ProductBlockWrapperDataService ProductBlockWrapperDataService { get; }
        public ProductDependentWrapperDataService ProductDependentWrapperDataService { get; }
        public ProductionTaskWrapperDataService ProductionTaskWrapperDataService { get; }
        public SalesBlockWrapperDataService SalesBlockWrapperDataService { get; }
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
        public PersonWrapperDataService PersonWrapperDataService { get; }
        public PaymentPlannedListWrapperDataService PaymentPlannedListWrapperDataService { get; }
        public PaymentPlannedWrapperDataService PaymentPlannedWrapperDataService { get; }
        public PaymentActualWrapperDataService PaymentActualWrapperDataService { get; }
        public ParameterRelationWrapperDataService ParameterRelationWrapperDataService { get; }
        public SalesUnitWrapperDataService SalesUnitWrapperDataService { get; }
        public ServiceWrapperDataService ServiceWrapperDataService { get; }
        public TestFriendAddressWrapperDataService TestFriendAddressWrapperDataService { get; }
        public TestFriendWrapperDataService TestFriendWrapperDataService { get; }
        public TestFriendEmailWrapperDataService TestFriendEmailWrapperDataService { get; }
        public TestFriendGroupWrapperDataService TestFriendGroupWrapperDataService { get; }
        public DocumentWrapperDataService DocumentWrapperDataService { get; }
        public TestEntityWrapperDataService TestEntityWrapperDataService { get; }
        public TestHusbandWrapperDataService TestHusbandWrapperDataService { get; }
        public TestWifeWrapperDataService TestWifeWrapperDataService { get; }
        public TestChildWrapperDataService TestChildWrapperDataService { get; }
        public SumOnDateWrapperDataService SumOnDateWrapperDataService { get; }
        public ProductWrapperDataService ProductWrapperDataService { get; }
        public OfferWrapperDataService OfferWrapperDataService { get; }
        public EmployeeWrapperDataService EmployeeWrapperDataService { get; }
        public OrderWrapperDataService OrderWrapperDataService { get; }
        public PaymentConditionWrapperDataService PaymentConditionWrapperDataService { get; }
        public PaymentDocumentWrapperDataService PaymentDocumentWrapperDataService { get; }
        public FacilityWrapperDataService FacilityWrapperDataService { get; }
        public ProjectWrapperDataService ProjectWrapperDataService { get; }
        public ProjectUnitWrapperDataService ProjectUnitWrapperDataService { get; }
        public UserRoleWrapperDataService UserRoleWrapperDataService { get; }
        public SpecificationWrapperDataService SpecificationWrapperDataService { get; }
        public TenderWrapperDataService TenderWrapperDataService { get; }
        public TenderTypeWrapperDataService TenderTypeWrapperDataService { get; }
        public UserWrapperDataService UserWrapperDataService { get; }
    }
}
