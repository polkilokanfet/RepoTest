using HVTApp.Infrastructure;
using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public class UnitOfWorkTest : IUnitOfWork
    {
        private TestData _testData;

        public UnitOfWorkTest()
        {
            _testData = new TestData();
        }

        public void Dispose()
        {
        }

        public int Complete()
        {
            return 0;
        }

        public IRepository<T> GetRepository<T>() where T : class, IBaseEntity
        {
            return _testData.GetAll<T>();
        }

        public IAdditionalSalesUnitsRepository AdditionalSalesUnitsRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public ILocalityRepository LocalityRepository { get; }
        public ILocalityTypeRepository LocalityTypeRepository { get; }
        public IRegionRepository RegionRepository { get; }
        public IDistrictRepository DistrictRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IBankDetailsRepository BankDetailsRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public ICompanyFormRepository CompanyFormRepository { get; }
        public IDocumentsRegistrationDetailsRepository DocumentsRegistrationDetailsRepository { get; }
        public IEmployeesPositionRepository EmployeesPositionRepository { get; }
        public IFacilityTypeRepository FacilityTypeRepository { get; }
        public IActivityFieldRepository ActivityFieldRepository { get; }
        public IContractRepository ContractRepository { get; }
        public IMeasureRepository MeasureRepository { get; }
        public IParameterRepository ParameterRepository { get; }
        public IParameterGroupRepository ParameterGroupRepository { get; }
        public IPartRepository PartRepository { get; }
        public IProductsRelationRepository ProductsRelationRepository { get; }
        public IStandartPaymentConditionsRepository StandartPaymentConditionsRepository { get; }
        public IPersonRepository PersonRepository { get; }
        public IPaymentPlannedRepository PaymentPlannedRepository { get; }
        public IPaymentActualRepository PaymentActualRepository { get; }
        public IRequiredPreviousParametersRepository RequiredPreviousParametersRepository { get; }
        public IProjectUnitRepository ProjectUnitRepository { get; }
        public ITenderUnitRepository TenderUnitRepository { get; }
        public IShipmentUnitRepository ShipmentUnitRepository { get; }
        public IProductionUnitRepository ProductionUnitRepository { get; }
        public ISalesUnitRepository SalesUnitRepository { get; }
        public ITestFriendAddressRepository TestFriendAddressRepository { get; }
        public ITestFriendRepository TestFriendRepository { get; }
        public ITestFriendEmailRepository TestFriendEmailRepository { get; }
        public ITestFriendGroupRepository TestFriendGroupRepository { get; }
        public IDocumentRepository DocumentRepository { get; }
        public ITestEntityRepository TestEntityRepository { get; }
        public ITestHusbandRepository TestHusbandRepository { get; }
        public ITestWifeRepository TestWifeRepository { get; }
        public ITestChildRepository TestChildRepository { get; }
        public ICostOnDateRepository CostOnDateRepository { get; }
        public ICostRepository CostRepository { get; }
        public ICurrencyRepository CurrencyRepository { get; }
        public IExchangeCurrencyRateRepository ExchangeCurrencyRateRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IOfferRepository OfferRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IPaymentConditionRepository PaymentConditionRepository { get; }
        public IPaymentDocumentRepository PaymentDocumentRepository { get; }
        public IFacilityRepository FacilityRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public ISpecificationRepository SpecificationRepository { get; }
        public ITenderRepository TenderRepository { get; }
        public ITenderTypeRepository TenderTypeRepository { get; }
        public IUserRepository UserRepository { get; }
        public IOfferUnitRepository OfferUnitRepository { get; }
    }
}