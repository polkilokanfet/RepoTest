using System.Data.Entity;

namespace HVTApp.UI.Wrapper
{
    public partial class WrapperDataService
    {
        public WrapperDataService(DbContext context) : base(context)
        {

            ProjectTypeWrapperRepository = new ProjectTypeWrapperRepository(this);

            CommonOptionWrapperRepository = new CommonOptionWrapperRepository(this);

            AddressWrapperRepository = new AddressWrapperRepository(this);

            CountryWrapperRepository = new CountryWrapperRepository(this);

            DistrictWrapperRepository = new DistrictWrapperRepository(this);

            LocalityWrapperRepository = new LocalityWrapperRepository(this);

            LocalityTypeWrapperRepository = new LocalityTypeWrapperRepository(this);

            RegionWrapperRepository = new RegionWrapperRepository(this);

            CalculatePriceTaskWrapperRepository = new CalculatePriceTaskWrapperRepository(this);

            SumWrapperRepository = new SumWrapperRepository(this);

            CurrencyExchangeRateWrapperRepository = new CurrencyExchangeRateWrapperRepository(this);

            DescribeProductBlockTaskWrapperRepository = new DescribeProductBlockTaskWrapperRepository(this);

            NoteWrapperRepository = new NoteWrapperRepository(this);

            OfferUnitWrapperRepository = new OfferUnitWrapperRepository(this);

            PaymentConditionSetWrapperRepository = new PaymentConditionSetWrapperRepository(this);

            ProductBlockWrapperRepository = new ProductBlockWrapperRepository(this);

            ProductDependentWrapperRepository = new ProductDependentWrapperRepository(this);

            ProductionTaskWrapperRepository = new ProductionTaskWrapperRepository(this);

            SalesBlockWrapperRepository = new SalesBlockWrapperRepository(this);

            BankDetailsWrapperRepository = new BankDetailsWrapperRepository(this);

            CompanyWrapperRepository = new CompanyWrapperRepository(this);

            CompanyFormWrapperRepository = new CompanyFormWrapperRepository(this);

            DocumentsRegistrationDetailsWrapperRepository = new DocumentsRegistrationDetailsWrapperRepository(this);

            EmployeesPositionWrapperRepository = new EmployeesPositionWrapperRepository(this);

            FacilityTypeWrapperRepository = new FacilityTypeWrapperRepository(this);

            ActivityFieldWrapperRepository = new ActivityFieldWrapperRepository(this);

            ContractWrapperRepository = new ContractWrapperRepository(this);

            MeasureWrapperRepository = new MeasureWrapperRepository(this);

            ParameterWrapperRepository = new ParameterWrapperRepository(this);

            ParameterGroupWrapperRepository = new ParameterGroupWrapperRepository(this);

            ProductRelationWrapperRepository = new ProductRelationWrapperRepository(this);

            PersonWrapperRepository = new PersonWrapperRepository(this);

            PaymentPlannedListWrapperRepository = new PaymentPlannedListWrapperRepository(this);

            PaymentPlannedWrapperRepository = new PaymentPlannedWrapperRepository(this);

            PaymentActualWrapperRepository = new PaymentActualWrapperRepository(this);

            ParameterRelationWrapperRepository = new ParameterRelationWrapperRepository(this);

            SalesUnitWrapperRepository = new SalesUnitWrapperRepository(this);

            ServiceWrapperRepository = new ServiceWrapperRepository(this);

            TestFriendAddressWrapperRepository = new TestFriendAddressWrapperRepository(this);

            TestFriendWrapperRepository = new TestFriendWrapperRepository(this);

            TestFriendEmailWrapperRepository = new TestFriendEmailWrapperRepository(this);

            TestFriendGroupWrapperRepository = new TestFriendGroupWrapperRepository(this);

            DocumentWrapperRepository = new DocumentWrapperRepository(this);

            TestEntityWrapperRepository = new TestEntityWrapperRepository(this);

            TestHusbandWrapperRepository = new TestHusbandWrapperRepository(this);

            TestWifeWrapperRepository = new TestWifeWrapperRepository(this);

            TestChildWrapperRepository = new TestChildWrapperRepository(this);

            SumOnDateWrapperRepository = new SumOnDateWrapperRepository(this);

            ProductWrapperRepository = new ProductWrapperRepository(this);

            OfferWrapperRepository = new OfferWrapperRepository(this);

            EmployeeWrapperRepository = new EmployeeWrapperRepository(this);

            OrderWrapperRepository = new OrderWrapperRepository(this);

            PaymentConditionWrapperRepository = new PaymentConditionWrapperRepository(this);

            PaymentDocumentWrapperRepository = new PaymentDocumentWrapperRepository(this);

            FacilityWrapperRepository = new FacilityWrapperRepository(this);

            ProjectWrapperRepository = new ProjectWrapperRepository(this);

            UserRoleWrapperRepository = new UserRoleWrapperRepository(this);

            SpecificationWrapperRepository = new SpecificationWrapperRepository(this);

            TenderWrapperRepository = new TenderWrapperRepository(this);

            TenderTypeWrapperRepository = new TenderTypeWrapperRepository(this);

            UserWrapperRepository = new UserWrapperRepository(this);

        }


        private ProjectTypeWrapperRepository ProjectTypeWrapperRepository;

        private CommonOptionWrapperRepository CommonOptionWrapperRepository;

        private AddressWrapperRepository AddressWrapperRepository;

        private CountryWrapperRepository CountryWrapperRepository;

        private DistrictWrapperRepository DistrictWrapperRepository;

        private LocalityWrapperRepository LocalityWrapperRepository;

        private LocalityTypeWrapperRepository LocalityTypeWrapperRepository;

        private RegionWrapperRepository RegionWrapperRepository;

        private CalculatePriceTaskWrapperRepository CalculatePriceTaskWrapperRepository;

        private SumWrapperRepository SumWrapperRepository;

        private CurrencyExchangeRateWrapperRepository CurrencyExchangeRateWrapperRepository;

        private DescribeProductBlockTaskWrapperRepository DescribeProductBlockTaskWrapperRepository;

        private NoteWrapperRepository NoteWrapperRepository;

        private OfferUnitWrapperRepository OfferUnitWrapperRepository;

        private PaymentConditionSetWrapperRepository PaymentConditionSetWrapperRepository;

        private ProductBlockWrapperRepository ProductBlockWrapperRepository;

        private ProductDependentWrapperRepository ProductDependentWrapperRepository;

        private ProductionTaskWrapperRepository ProductionTaskWrapperRepository;

        private SalesBlockWrapperRepository SalesBlockWrapperRepository;

        private BankDetailsWrapperRepository BankDetailsWrapperRepository;

        private CompanyWrapperRepository CompanyWrapperRepository;

        private CompanyFormWrapperRepository CompanyFormWrapperRepository;

        private DocumentsRegistrationDetailsWrapperRepository DocumentsRegistrationDetailsWrapperRepository;

        private EmployeesPositionWrapperRepository EmployeesPositionWrapperRepository;

        private FacilityTypeWrapperRepository FacilityTypeWrapperRepository;

        private ActivityFieldWrapperRepository ActivityFieldWrapperRepository;

        private ContractWrapperRepository ContractWrapperRepository;

        private MeasureWrapperRepository MeasureWrapperRepository;

        private ParameterWrapperRepository ParameterWrapperRepository;

        private ParameterGroupWrapperRepository ParameterGroupWrapperRepository;

        private ProductRelationWrapperRepository ProductRelationWrapperRepository;

        private PersonWrapperRepository PersonWrapperRepository;

        private PaymentPlannedListWrapperRepository PaymentPlannedListWrapperRepository;

        private PaymentPlannedWrapperRepository PaymentPlannedWrapperRepository;

        private PaymentActualWrapperRepository PaymentActualWrapperRepository;

        private ParameterRelationWrapperRepository ParameterRelationWrapperRepository;

        private SalesUnitWrapperRepository SalesUnitWrapperRepository;

        private ServiceWrapperRepository ServiceWrapperRepository;

        private TestFriendAddressWrapperRepository TestFriendAddressWrapperRepository;

        private TestFriendWrapperRepository TestFriendWrapperRepository;

        private TestFriendEmailWrapperRepository TestFriendEmailWrapperRepository;

        private TestFriendGroupWrapperRepository TestFriendGroupWrapperRepository;

        private DocumentWrapperRepository DocumentWrapperRepository;

        private TestEntityWrapperRepository TestEntityWrapperRepository;

        private TestHusbandWrapperRepository TestHusbandWrapperRepository;

        private TestWifeWrapperRepository TestWifeWrapperRepository;

        private TestChildWrapperRepository TestChildWrapperRepository;

        private SumOnDateWrapperRepository SumOnDateWrapperRepository;

        private ProductWrapperRepository ProductWrapperRepository;

        private OfferWrapperRepository OfferWrapperRepository;

        private EmployeeWrapperRepository EmployeeWrapperRepository;

        private OrderWrapperRepository OrderWrapperRepository;

        private PaymentConditionWrapperRepository PaymentConditionWrapperRepository;

        private PaymentDocumentWrapperRepository PaymentDocumentWrapperRepository;

        private FacilityWrapperRepository FacilityWrapperRepository;

        private ProjectWrapperRepository ProjectWrapperRepository;

        private UserRoleWrapperRepository UserRoleWrapperRepository;

        private SpecificationWrapperRepository SpecificationWrapperRepository;

        private TenderWrapperRepository TenderWrapperRepository;

        private TenderTypeWrapperRepository TenderTypeWrapperRepository;

        private UserWrapperRepository UserWrapperRepository;

    }
}
