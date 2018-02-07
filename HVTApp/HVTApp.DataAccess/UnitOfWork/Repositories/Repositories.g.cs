using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext context) : base(context) {}
    }

    public partial class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(DbContext context) : base(context) {}
    }

    public partial class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(DbContext context) : base(context) {}
    }

    public partial class LocalityRepository : BaseRepository<Locality>, ILocalityRepository
    {
        public LocalityRepository(DbContext context) : base(context) {}
    }

    public partial class LocalityTypeRepository : BaseRepository<LocalityType>, ILocalityTypeRepository
    {
        public LocalityTypeRepository(DbContext context) : base(context) {}
    }

    public partial class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(DbContext context) : base(context) {}
    }

    public partial class AdditionalSalesUnitsRepository : BaseRepository<AdditionalSalesUnits>, IAdditionalSalesUnitsRepository
    {
        public AdditionalSalesUnitsRepository(DbContext context) : base(context) {}
    }

    public partial class BankDetailsRepository : BaseRepository<BankDetails>, IBankDetailsRepository
    {
        public BankDetailsRepository(DbContext context) : base(context) {}
    }

    public partial class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context) {}
    }

    public partial class CompanyFormRepository : BaseRepository<CompanyForm>, ICompanyFormRepository
    {
        public CompanyFormRepository(DbContext context) : base(context) {}
    }

    public partial class DocumentsRegistrationDetailsRepository : BaseRepository<DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsRepository
    {
        public DocumentsRegistrationDetailsRepository(DbContext context) : base(context) {}
    }

    public partial class EmployeesPositionRepository : BaseRepository<EmployeesPosition>, IEmployeesPositionRepository
    {
        public EmployeesPositionRepository(DbContext context) : base(context) {}
    }

    public partial class FacilityTypeRepository : BaseRepository<FacilityType>, IFacilityTypeRepository
    {
        public FacilityTypeRepository(DbContext context) : base(context) {}
    }

    public partial class ActivityFieldRepository : BaseRepository<ActivityField>, IActivityFieldRepository
    {
        public ActivityFieldRepository(DbContext context) : base(context) {}
    }

    public partial class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
        public ContractRepository(DbContext context) : base(context) {}
    }

    public partial class MeasureRepository : BaseRepository<Measure>, IMeasureRepository
    {
        public MeasureRepository(DbContext context) : base(context) {}
    }

    public partial class ParameterRepository : BaseRepository<Parameter>, IParameterRepository
    {
        public ParameterRepository(DbContext context) : base(context) {}
    }

    public partial class ParameterGroupRepository : BaseRepository<ParameterGroup>, IParameterGroupRepository
    {
        public ParameterGroupRepository(DbContext context) : base(context) {}
    }

    public partial class ProductRelationRepository : BaseRepository<ProductRelation>, IProductRelationRepository
    {
        public ProductRelationRepository(DbContext context) : base(context) {}
    }

    public partial class StandartPaymentConditionsRepository : BaseRepository<StandartPaymentConditions>, IStandartPaymentConditionsRepository
    {
        public StandartPaymentConditionsRepository(DbContext context) : base(context) {}
    }

    public partial class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context) : base(context) {}
    }

    public partial class PaymentPlannedRepository : BaseRepository<PaymentPlanned>, IPaymentPlannedRepository
    {
        public PaymentPlannedRepository(DbContext context) : base(context) {}
    }

    public partial class PaymentActualRepository : BaseRepository<PaymentActual>, IPaymentActualRepository
    {
        public PaymentActualRepository(DbContext context) : base(context) {}
    }

    public partial class ParameterRelationRepository : BaseRepository<ParameterRelation>, IParameterRelationRepository
    {
        public ParameterRelationRepository(DbContext context) : base(context) {}
    }

    public partial class ProjectUnitRepository : BaseRepository<ProjectUnit>, IProjectUnitRepository
    {
        public ProjectUnitRepository(DbContext context) : base(context) {}
    }

    public partial class ShipmentUnitRepository : BaseRepository<ShipmentUnit>, IShipmentUnitRepository
    {
        public ShipmentUnitRepository(DbContext context) : base(context) {}
    }

    public partial class ProductionUnitRepository : BaseRepository<ProductionUnit>, IProductionUnitRepository
    {
        public ProductionUnitRepository(DbContext context) : base(context) {}
    }

    public partial class SalesUnitRepository : BaseRepository<SalesUnit>, ISalesUnitRepository
    {
        public SalesUnitRepository(DbContext context) : base(context) {}
    }

    public partial class TestFriendAddressRepository : BaseRepository<TestFriendAddress>, ITestFriendAddressRepository
    {
        public TestFriendAddressRepository(DbContext context) : base(context) {}
    }

    public partial class TestFriendRepository : BaseRepository<TestFriend>, ITestFriendRepository
    {
        public TestFriendRepository(DbContext context) : base(context) {}
    }

    public partial class TestFriendEmailRepository : BaseRepository<TestFriendEmail>, ITestFriendEmailRepository
    {
        public TestFriendEmailRepository(DbContext context) : base(context) {}
    }

    public partial class TestFriendGroupRepository : BaseRepository<TestFriendGroup>, ITestFriendGroupRepository
    {
        public TestFriendGroupRepository(DbContext context) : base(context) {}
    }

    public partial class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context) : base(context) {}
    }

    public partial class TestEntityRepository : BaseRepository<TestEntity>, ITestEntityRepository
    {
        public TestEntityRepository(DbContext context) : base(context) {}
    }

    public partial class TestHusbandRepository : BaseRepository<TestHusband>, ITestHusbandRepository
    {
        public TestHusbandRepository(DbContext context) : base(context) {}
    }

    public partial class TestWifeRepository : BaseRepository<TestWife>, ITestWifeRepository
    {
        public TestWifeRepository(DbContext context) : base(context) {}
    }

    public partial class TestChildRepository : BaseRepository<TestChild>, ITestChildRepository
    {
        public TestChildRepository(DbContext context) : base(context) {}
    }

    public partial class CostOnDateRepository : BaseRepository<CostOnDate>, ICostOnDateRepository
    {
        public CostOnDateRepository(DbContext context) : base(context) {}
    }

    public partial class CostRepository : BaseRepository<Cost>, ICostRepository
    {
        public CostRepository(DbContext context) : base(context) {}
    }

    public partial class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(DbContext context) : base(context) {}
    }

    public partial class ExchangeCurrencyRateRepository : BaseRepository<ExchangeCurrencyRate>, IExchangeCurrencyRateRepository
    {
        public ExchangeCurrencyRateRepository(DbContext context) : base(context) {}
    }

    public partial class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context) {}
    }

    public partial class OfferRepository : BaseRepository<Offer>, IOfferRepository
    {
        public OfferRepository(DbContext context) : base(context) {}
    }

    public partial class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context) {}
    }

    public partial class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context) {}
    }

    public partial class PaymentConditionRepository : BaseRepository<PaymentCondition>, IPaymentConditionRepository
    {
        public PaymentConditionRepository(DbContext context) : base(context) {}
    }

    public partial class PaymentDocumentRepository : BaseRepository<PaymentDocument>, IPaymentDocumentRepository
    {
        public PaymentDocumentRepository(DbContext context) : base(context) {}
    }

    public partial class FacilityRepository : BaseRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(DbContext context) : base(context) {}
    }

    public partial class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context) {}
    }

    public partial class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context) : base(context) {}
    }

    public partial class SpecificationRepository : BaseRepository<Specification>, ISpecificationRepository
    {
        public SpecificationRepository(DbContext context) : base(context) {}
    }

    public partial class TenderRepository : BaseRepository<Tender>, ITenderRepository
    {
        public TenderRepository(DbContext context) : base(context) {}
    }

    public partial class TenderTypeRepository : BaseRepository<TenderType>, ITenderTypeRepository
    {
        public TenderTypeRepository(DbContext context) : base(context) {}
    }

    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) {}
    }

    public partial class OfferUnitRepository : BaseRepository<OfferUnit>, IOfferUnitRepository
    {
        public OfferUnitRepository(DbContext context) : base(context) {}
    }

    public partial class ProjectUnitGroupRepository : BaseRepository<ProjectUnitGroup>, IProjectUnitGroupRepository
    {
        public ProjectUnitGroupRepository(DbContext context) : base(context) {}
    }

    public partial class ProductBlockRepository : BaseRepository<ProductBlock>, IProductBlockRepository
    {
        public ProductBlockRepository(DbContext context) : base(context) {}
    }

}
