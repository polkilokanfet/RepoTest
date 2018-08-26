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
            CommonOptionRepository = new CommonOptionRepository(context);
            AddressRepository = new AddressRepository(context);
            CountryRepository = new CountryRepository(context);
            DistrictRepository = new DistrictRepository(context);
            LocalityRepository = new LocalityRepository(context);
            LocalityTypeRepository = new LocalityTypeRepository(context);
            RegionRepository = new RegionRepository(context);
            CalculatePriceTaskRepository = new CalculatePriceTaskRepository(context);
            SumRepository = new SumRepository(context);
            CurrencyExchangeRateRepository = new CurrencyExchangeRateRepository(context);
            DescribeProductBlockTaskRepository = new DescribeProductBlockTaskRepository(context);
            NoteRepository = new NoteRepository(context);
            OfferUnitRepository = new OfferUnitRepository(context);
            PaymentConditionSetRepository = new PaymentConditionSetRepository(context);
            ProductBlockRepository = new ProductBlockRepository(context);
            ProductDependentRepository = new ProductDependentRepository(context);
            ProductionTaskRepository = new ProductionTaskRepository(context);
            SalesBlockRepository = new SalesBlockRepository(context);
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
            PersonRepository = new PersonRepository(context);
            PaymentPlannedListRepository = new PaymentPlannedListRepository(context);
            PaymentPlannedRepository = new PaymentPlannedRepository(context);
            PaymentActualRepository = new PaymentActualRepository(context);
            ParameterRelationRepository = new ParameterRelationRepository(context);
            SalesUnitRepository = new SalesUnitRepository(context);
            ServiceRepository = new ServiceRepository(context);
            TestFriendAddressRepository = new TestFriendAddressRepository(context);
            TestFriendRepository = new TestFriendRepository(context);
            TestFriendEmailRepository = new TestFriendEmailRepository(context);
            TestFriendGroupRepository = new TestFriendGroupRepository(context);
            DocumentRepository = new DocumentRepository(context);
            TestEntityRepository = new TestEntityRepository(context);
            TestHusbandRepository = new TestHusbandRepository(context);
            TestWifeRepository = new TestWifeRepository(context);
            TestChildRepository = new TestChildRepository(context);
            SumOnDateRepository = new SumOnDateRepository(context);
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
			#endregion
        }


        #region Repositories
        protected ICommonOptionRepository CommonOptionRepository;
        protected IAddressRepository AddressRepository;
        protected ICountryRepository CountryRepository;
        protected IDistrictRepository DistrictRepository;
        protected ILocalityRepository LocalityRepository;
        protected ILocalityTypeRepository LocalityTypeRepository;
        protected IRegionRepository RegionRepository;
        protected ICalculatePriceTaskRepository CalculatePriceTaskRepository;
        protected ISumRepository SumRepository;
        protected ICurrencyExchangeRateRepository CurrencyExchangeRateRepository;
        protected IDescribeProductBlockTaskRepository DescribeProductBlockTaskRepository;
        protected INoteRepository NoteRepository;
        protected IOfferUnitRepository OfferUnitRepository;
        protected IPaymentConditionSetRepository PaymentConditionSetRepository;
        protected IProductBlockRepository ProductBlockRepository;
        protected IProductDependentRepository ProductDependentRepository;
        protected IProductionTaskRepository ProductionTaskRepository;
        protected ISalesBlockRepository SalesBlockRepository;
        protected IBankDetailsRepository BankDetailsRepository;
        protected ICompanyRepository CompanyRepository;
        protected ICompanyFormRepository CompanyFormRepository;
        protected IDocumentsRegistrationDetailsRepository DocumentsRegistrationDetailsRepository;
        protected IEmployeesPositionRepository EmployeesPositionRepository;
        protected IFacilityTypeRepository FacilityTypeRepository;
        protected IActivityFieldRepository ActivityFieldRepository;
        protected IContractRepository ContractRepository;
        protected IMeasureRepository MeasureRepository;
        protected IParameterRepository ParameterRepository;
        protected IParameterGroupRepository ParameterGroupRepository;
        protected IProductRelationRepository ProductRelationRepository;
        protected IPersonRepository PersonRepository;
        protected IPaymentPlannedListRepository PaymentPlannedListRepository;
        protected IPaymentPlannedRepository PaymentPlannedRepository;
        protected IPaymentActualRepository PaymentActualRepository;
        protected IParameterRelationRepository ParameterRelationRepository;
        protected ISalesUnitRepository SalesUnitRepository;
        protected IServiceRepository ServiceRepository;
        protected ITestFriendAddressRepository TestFriendAddressRepository;
        protected ITestFriendRepository TestFriendRepository;
        protected ITestFriendEmailRepository TestFriendEmailRepository;
        protected ITestFriendGroupRepository TestFriendGroupRepository;
        protected IDocumentRepository DocumentRepository;
        protected ITestEntityRepository TestEntityRepository;
        protected ITestHusbandRepository TestHusbandRepository;
        protected ITestWifeRepository TestWifeRepository;
        protected ITestChildRepository TestChildRepository;
        protected ISumOnDateRepository SumOnDateRepository;
        protected IProductRepository ProductRepository;
        protected IOfferRepository OfferRepository;
        protected IEmployeeRepository EmployeeRepository;
        protected IOrderRepository OrderRepository;
        protected IPaymentConditionRepository PaymentConditionRepository;
        protected IPaymentDocumentRepository PaymentDocumentRepository;
        protected IFacilityRepository FacilityRepository;
        protected IProjectRepository ProjectRepository;
        protected IUserRoleRepository UserRoleRepository;
        protected ISpecificationRepository SpecificationRepository;
        protected ITenderRepository TenderRepository;
        protected ITenderTypeRepository TenderTypeRepository;
        protected IUserRepository UserRepository;
        #endregion
    }
}
