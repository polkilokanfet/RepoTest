using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public partial interface ICommonOptionRepository : IRepository<CommonOption> { }
    public partial interface IAddressRepository : IRepository<Address> { }
    public partial interface ICountryRepository : IRepository<Country> { }
    public partial interface IDistrictRepository : IRepository<District> { }
    public partial interface ILocalityRepository : IRepository<Locality> { }
    public partial interface ILocalityTypeRepository : IRepository<LocalityType> { }
    public partial interface IRegionRepository : IRepository<Region> { }
    public partial interface ICalculatePriceTaskRepository : IRepository<CalculatePriceTask> { }
    public partial interface IDescribeProductBlockTaskRepository : IRepository<DescribeProductBlockTask> { }
    public partial interface ISalesBlockRepository : IRepository<SalesBlock> { }
    public partial interface IBankDetailsRepository : IRepository<BankDetails> { }
    public partial interface ICompanyRepository : IRepository<Company> { }
    public partial interface ICompanyFormRepository : IRepository<CompanyForm> { }
    public partial interface IDocumentsRegistrationDetailsRepository : IRepository<DocumentsRegistrationDetails> { }
    public partial interface IEmployeesPositionRepository : IRepository<EmployeesPosition> { }
    public partial interface IFacilityTypeRepository : IRepository<FacilityType> { }
    public partial interface IActivityFieldRepository : IRepository<ActivityField> { }
    public partial interface IContractRepository : IRepository<Contract> { }
    public partial interface IMeasureRepository : IRepository<Measure> { }
    public partial interface IParameterRepository : IRepository<Parameter> { }
    public partial interface IParameterGroupRepository : IRepository<ParameterGroup> { }
    public partial interface IProductRelationRepository : IRepository<ProductRelation> { }
    public partial interface IStandartPaymentConditionsRepository : IRepository<StandartPaymentConditions> { }
    public partial interface IPersonRepository : IRepository<Person> { }
    public partial interface IPaymentPlannedRepository : IRepository<PaymentPlanned> { }
    public partial interface IPaymentActualRepository : IRepository<PaymentActual> { }
    public partial interface IParameterRelationRepository : IRepository<ParameterRelation> { }
    public partial interface ISalesUnitRepository : IRepository<SalesUnit> { }
    public partial interface ITestFriendAddressRepository : IRepository<TestFriendAddress> { }
    public partial interface ITestFriendRepository : IRepository<TestFriend> { }
    public partial interface ITestFriendEmailRepository : IRepository<TestFriendEmail> { }
    public partial interface ITestFriendGroupRepository : IRepository<TestFriendGroup> { }
    public partial interface IDocumentRepository : IRepository<Document> { }
    public partial interface ITestEntityRepository : IRepository<TestEntity> { }
    public partial interface ITestHusbandRepository : IRepository<TestHusband> { }
    public partial interface ITestWifeRepository : IRepository<TestWife> { }
    public partial interface ITestChildRepository : IRepository<TestChild> { }
    public partial interface ICostOnDateRepository : IRepository<CostOnDate> { }
    public partial interface ICostRepository : IRepository<Cost> { }
    public partial interface ICurrencyRepository : IRepository<Currency> { }
    public partial interface IExchangeCurrencyRateRepository : IRepository<ExchangeCurrencyRate> { }
    public partial interface IProductRepository : IRepository<Product> { }
    public partial interface IOfferRepository : IRepository<Offer> { }
    public partial interface IEmployeeRepository : IRepository<Employee> { }
    public partial interface IOrderRepository : IRepository<Order> { }
    public partial interface IPaymentConditionRepository : IRepository<PaymentCondition> { }
    public partial interface IPaymentDocumentRepository : IRepository<PaymentDocument> { }
    public partial interface IFacilityRepository : IRepository<Facility> { }
    public partial interface IProjectRepository : IRepository<Project> { }
    public partial interface IUserRoleRepository : IRepository<UserRole> { }
    public partial interface ISpecificationRepository : IRepository<Specification> { }
    public partial interface ITenderRepository : IRepository<Tender> { }
    public partial interface ITenderTypeRepository : IRepository<TenderType> { }
    public partial interface IUserRepository : IRepository<User> { }
    public partial interface IProductBlockRepository : IRepository<ProductBlock> { }
    public partial interface IPaymentConditionSetRepository : IRepository<PaymentConditionSet> { }

}
