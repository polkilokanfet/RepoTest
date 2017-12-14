using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;


namespace HVTApp.DataAccess
{
    public partial class UnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
			#region RepositoriesInit
            AddressRepository = new AddressRepository(context);
            CountryRepository = new CountryRepository(context);
            DistrictRepository = new DistrictRepository(context);
            LocalityRepository = new LocalityRepository(context);
            LocalityTypeRepository = new LocalityTypeRepository(context);
            RegionRepository = new RegionRepository(context);
            AdditionalSalesUnitsRepository = new AdditionalSalesUnitsRepository(context);
            BankDetailsRepository = new BankDetailsRepository(context);
            CompanyRepository = new CompanyRepository(context);
            CompanyFormRepository = new CompanyFormRepository(context);
            DocumentsRegistrationDetailsRepository = new DocumentsRegistrationDetailsRepository(context);
            EmployeesPositionRepository = new EmployeesPositionRepository(context);
            FacilityTypeRepository = new FacilityTypeRepository(context);
            ActivityFieldRepository = new ActivityFieldRepository(context);
            ContractRepository = new ContractRepository(context);
            MeasureRepository = new MeasureRepository(context);
            ParameterRepository = new ParameterRepository(context);
            ParameterGroupRepository = new ParameterGroupRepository(context);
            ProductRelationRepository = new ProductRelationRepository(context);
            StandartPaymentConditionsRepository = new StandartPaymentConditionsRepository(context);
            PersonRepository = new PersonRepository(context);
            PaymentPlannedRepository = new PaymentPlannedRepository(context);
            PaymentActualRepository = new PaymentActualRepository(context);
            ParameterRelationRepository = new ParameterRelationRepository(context);
            ProjectUnitRepository = new ProjectUnitRepository(context);
            TenderUnitRepository = new TenderUnitRepository(context);
            ShipmentUnitRepository = new ShipmentUnitRepository(context);
            ProductionUnitRepository = new ProductionUnitRepository(context);
            SalesUnitRepository = new SalesUnitRepository(context);
            TestFriendAddressRepository = new TestFriendAddressRepository(context);
            TestFriendRepository = new TestFriendRepository(context);
            TestFriendEmailRepository = new TestFriendEmailRepository(context);
            TestFriendGroupRepository = new TestFriendGroupRepository(context);
            DocumentRepository = new DocumentRepository(context);
            TestEntityRepository = new TestEntityRepository(context);
            TestHusbandRepository = new TestHusbandRepository(context);
            TestWifeRepository = new TestWifeRepository(context);
            TestChildRepository = new TestChildRepository(context);
            CostOnDateRepository = new CostOnDateRepository(context);
            CostRepository = new CostRepository(context);
            CurrencyRepository = new CurrencyRepository(context);
            ExchangeCurrencyRateRepository = new ExchangeCurrencyRateRepository(context);
            ProductRepository = new ProductRepository(context);
            OfferRepository = new OfferRepository(context);
            EmployeeRepository = new EmployeeRepository(context);
            OrderRepository = new OrderRepository(context);
            PaymentConditionRepository = new PaymentConditionRepository(context);
            PaymentDocumentRepository = new PaymentDocumentRepository(context);
            FacilityRepository = new FacilityRepository(context);
            ProjectRepository = new ProjectRepository(context);
            UserRoleRepository = new UserRoleRepository(context);
            SpecificationRepository = new SpecificationRepository(context);
            TenderRepository = new TenderRepository(context);
            TenderTypeRepository = new TenderTypeRepository(context);
            UserRepository = new UserRepository(context);
            OfferUnitRepository = new OfferUnitRepository(context);
			#endregion
        }


        #region Repositories
        public IAddressRepository AddressRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IDistrictRepository DistrictRepository { get; }
        public ILocalityRepository LocalityRepository { get; }
        public ILocalityTypeRepository LocalityTypeRepository { get; }
        public IRegionRepository RegionRepository { get; }
        public IAdditionalSalesUnitsRepository AdditionalSalesUnitsRepository { get; }
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
        public IProductRelationRepository ProductRelationRepository { get; }
        public IStandartPaymentConditionsRepository StandartPaymentConditionsRepository { get; }
        public IPersonRepository PersonRepository { get; }
        public IPaymentPlannedRepository PaymentPlannedRepository { get; }
        public IPaymentActualRepository PaymentActualRepository { get; }
        public IParameterRelationRepository ParameterRelationRepository { get; }
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
        #endregion
    }
}
