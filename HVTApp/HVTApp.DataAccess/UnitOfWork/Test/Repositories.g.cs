using HVTApp.TestDataGenerator;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PaymentActualRepositoryTest : TestBaseRepository<PaymentActual>, IPaymentActualRepository
    {
        public PaymentActualRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class PaymentPlannedRepositoryTest : TestBaseRepository<PaymentPlanned>, IPaymentPlannedRepository
    {
        public PaymentPlannedRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProductDesignationRepositoryTest : TestBaseRepository<ProductDesignation>, IProductDesignationRepository
    {
        public ProductDesignationRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProductTypeDesignationRepositoryTest : TestBaseRepository<ProductTypeDesignation>, IProductTypeDesignationRepository
    {
        public ProductTypeDesignationRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProjectTypeRepositoryTest : TestBaseRepository<ProjectType>, IProjectTypeRepository
    {
        public ProjectTypeRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class CommonOptionRepositoryTest : TestBaseRepository<CommonOption>, ICommonOptionRepository
    {
        public CommonOptionRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class AddressRepositoryTest : TestBaseRepository<Address>, IAddressRepository
    {
        public AddressRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class CountryRepositoryTest : TestBaseRepository<Country>, ICountryRepository
    {
        public CountryRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class DistrictRepositoryTest : TestBaseRepository<District>, IDistrictRepository
    {
        public DistrictRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class LocalityRepositoryTest : TestBaseRepository<Locality>, ILocalityRepository
    {
        public LocalityRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class LocalityTypeRepositoryTest : TestBaseRepository<LocalityType>, ILocalityTypeRepository
    {
        public LocalityTypeRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class RegionRepositoryTest : TestBaseRepository<Region>, IRegionRepository
    {
        public RegionRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class CalculatePriceTaskRepositoryTest : TestBaseRepository<CalculatePriceTask>, ICalculatePriceTaskRepository
    {
        public CalculatePriceTaskRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class SumRepositoryTest : TestBaseRepository<Sum>, ISumRepository
    {
        public SumRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class CurrencyExchangeRateRepositoryTest : TestBaseRepository<CurrencyExchangeRate>, ICurrencyExchangeRateRepository
    {
        public CurrencyExchangeRateRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class DescribeProductBlockTaskRepositoryTest : TestBaseRepository<DescribeProductBlockTask>, IDescribeProductBlockTaskRepository
    {
        public DescribeProductBlockTaskRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class NoteRepositoryTest : TestBaseRepository<Note>, INoteRepository
    {
        public NoteRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class OfferUnitRepositoryTest : TestBaseRepository<OfferUnit>, IOfferUnitRepository
    {
        public OfferUnitRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class PaymentConditionSetRepositoryTest : TestBaseRepository<PaymentConditionSet>, IPaymentConditionSetRepository
    {
        public PaymentConditionSetRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProductTypeRepositoryTest : TestBaseRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProductBlockRepositoryTest : TestBaseRepository<ProductBlock>, IProductBlockRepository
    {
        public ProductBlockRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProductDependentRepositoryTest : TestBaseRepository<ProductDependent>, IProductDependentRepository
    {
        public ProductDependentRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProductionTaskRepositoryTest : TestBaseRepository<ProductionTask>, IProductionTaskRepository
    {
        public ProductionTaskRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class SalesBlockRepositoryTest : TestBaseRepository<SalesBlock>, ISalesBlockRepository
    {
        public SalesBlockRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class BankDetailsRepositoryTest : TestBaseRepository<BankDetails>, IBankDetailsRepository
    {
        public BankDetailsRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class CompanyRepositoryTest : TestBaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class CompanyFormRepositoryTest : TestBaseRepository<CompanyForm>, ICompanyFormRepository
    {
        public CompanyFormRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class DocumentsRegistrationDetailsRepositoryTest : TestBaseRepository<DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsRepository
    {
        public DocumentsRegistrationDetailsRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class EmployeesPositionRepositoryTest : TestBaseRepository<EmployeesPosition>, IEmployeesPositionRepository
    {
        public EmployeesPositionRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class FacilityTypeRepositoryTest : TestBaseRepository<FacilityType>, IFacilityTypeRepository
    {
        public FacilityTypeRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ActivityFieldRepositoryTest : TestBaseRepository<ActivityField>, IActivityFieldRepository
    {
        public ActivityFieldRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ContractRepositoryTest : TestBaseRepository<Contract>, IContractRepository
    {
        public ContractRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class MeasureRepositoryTest : TestBaseRepository<Measure>, IMeasureRepository
    {
        public MeasureRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ParameterRepositoryTest : TestBaseRepository<Parameter>, IParameterRepository
    {
        public ParameterRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ParameterGroupRepositoryTest : TestBaseRepository<ParameterGroup>, IParameterGroupRepository
    {
        public ParameterGroupRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProductRelationRepositoryTest : TestBaseRepository<ProductRelation>, IProductRelationRepository
    {
        public ProductRelationRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class PersonRepositoryTest : TestBaseRepository<Person>, IPersonRepository
    {
        public PersonRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ParameterRelationRepositoryTest : TestBaseRepository<ParameterRelation>, IParameterRelationRepository
    {
        public ParameterRelationRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class SalesUnitRepositoryTest : TestBaseRepository<SalesUnit>, ISalesUnitRepository
    {
        public SalesUnitRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ServiceRepositoryTest : TestBaseRepository<Service>, IServiceRepository
    {
        public ServiceRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TestFriendAddressRepositoryTest : TestBaseRepository<TestFriendAddress>, ITestFriendAddressRepository
    {
        public TestFriendAddressRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TestFriendRepositoryTest : TestBaseRepository<TestFriend>, ITestFriendRepository
    {
        public TestFriendRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TestFriendEmailRepositoryTest : TestBaseRepository<TestFriendEmail>, ITestFriendEmailRepository
    {
        public TestFriendEmailRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TestFriendGroupRepositoryTest : TestBaseRepository<TestFriendGroup>, ITestFriendGroupRepository
    {
        public TestFriendGroupRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class DocumentRepositoryTest : TestBaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TestEntityRepositoryTest : TestBaseRepository<TestEntity>, ITestEntityRepository
    {
        public TestEntityRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TestHusbandRepositoryTest : TestBaseRepository<TestHusband>, ITestHusbandRepository
    {
        public TestHusbandRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TestWifeRepositoryTest : TestBaseRepository<TestWife>, ITestWifeRepository
    {
        public TestWifeRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TestChildRepositoryTest : TestBaseRepository<TestChild>, ITestChildRepository
    {
        public TestChildRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class SumOnDateRepositoryTest : TestBaseRepository<SumOnDate>, ISumOnDateRepository
    {
        public SumOnDateRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProductRepositoryTest : TestBaseRepository<Product>, IProductRepository
    {
        public ProductRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class OfferRepositoryTest : TestBaseRepository<Offer>, IOfferRepository
    {
        public OfferRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class EmployeeRepositoryTest : TestBaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class OrderRepositoryTest : TestBaseRepository<Order>, IOrderRepository
    {
        public OrderRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class PaymentConditionRepositoryTest : TestBaseRepository<PaymentCondition>, IPaymentConditionRepository
    {
        public PaymentConditionRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class PaymentDocumentRepositoryTest : TestBaseRepository<PaymentDocument>, IPaymentDocumentRepository
    {
        public PaymentDocumentRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class FacilityRepositoryTest : TestBaseRepository<Facility>, IFacilityRepository
    {
        public FacilityRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class ProjectRepositoryTest : TestBaseRepository<Project>, IProjectRepository
    {
        public ProjectRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class UserRoleRepositoryTest : TestBaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class SpecificationRepositoryTest : TestBaseRepository<Specification>, ISpecificationRepository
    {
        public SpecificationRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TenderRepositoryTest : TestBaseRepository<Tender>, ITenderRepository
    {
        public TenderRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class TenderTypeRepositoryTest : TestBaseRepository<TenderType>, ITenderTypeRepository
    {
        public TenderTypeRepositoryTest(TestData testData) : base(testData) {}
    }

    public partial class UserRepositoryTest : TestBaseRepository<User>, IUserRepository
    {
        public UserRepositoryTest(TestData testData) : base(testData) {}
    }

}
