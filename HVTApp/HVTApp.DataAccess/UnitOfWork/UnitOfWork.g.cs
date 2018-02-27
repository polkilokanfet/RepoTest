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
            CostRepository = new CostRepository(context);
            CurrencyRepository = new CurrencyRepository(context);
            CurrencyExchangeRateRepository = new CurrencyExchangeRateRepository(context);
            DescribeProductBlockTaskRepository = new DescribeProductBlockTaskRepository(context);
            NoteRepository = new NoteRepository(context);
            OfferUnitRepository = new OfferUnitRepository(context);
            PaymentConditionSetRepository = new PaymentConditionSetRepository(context);
            ProductBlockRepository = new ProductBlockRepository(context);
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
        private ICommonOptionRepository CommonOptionRepository;
        private IAddressRepository AddressRepository;
        private ICountryRepository CountryRepository;
        private IDistrictRepository DistrictRepository;
        private ILocalityRepository LocalityRepository;
        private ILocalityTypeRepository LocalityTypeRepository;
        private IRegionRepository RegionRepository;
        private ICalculatePriceTaskRepository CalculatePriceTaskRepository;
        private ICostRepository CostRepository;
        private ICurrencyRepository CurrencyRepository;
        private ICurrencyExchangeRateRepository CurrencyExchangeRateRepository;
        private IDescribeProductBlockTaskRepository DescribeProductBlockTaskRepository;
        private INoteRepository NoteRepository;
        private IOfferUnitRepository OfferUnitRepository;
        private IPaymentConditionSetRepository PaymentConditionSetRepository;
        private IProductBlockRepository ProductBlockRepository;
        private ISalesBlockRepository SalesBlockRepository;
        private IBankDetailsRepository BankDetailsRepository;
        private ICompanyRepository CompanyRepository;
        private ICompanyFormRepository CompanyFormRepository;
        private IDocumentsRegistrationDetailsRepository DocumentsRegistrationDetailsRepository;
        private IEmployeesPositionRepository EmployeesPositionRepository;
        private IFacilityTypeRepository FacilityTypeRepository;
        private IActivityFieldRepository ActivityFieldRepository;
        private IContractRepository ContractRepository;
        private IMeasureRepository MeasureRepository;
        private IParameterRepository ParameterRepository;
        private IParameterGroupRepository ParameterGroupRepository;
        private IProductRelationRepository ProductRelationRepository;
        private IPersonRepository PersonRepository;
        private IPaymentPlannedListRepository PaymentPlannedListRepository;
        private IPaymentPlannedRepository PaymentPlannedRepository;
        private IPaymentActualRepository PaymentActualRepository;
        private IParameterRelationRepository ParameterRelationRepository;
        private ISalesUnitRepository SalesUnitRepository;
        private ITestFriendAddressRepository TestFriendAddressRepository;
        private ITestFriendRepository TestFriendRepository;
        private ITestFriendEmailRepository TestFriendEmailRepository;
        private ITestFriendGroupRepository TestFriendGroupRepository;
        private IDocumentRepository DocumentRepository;
        private ITestEntityRepository TestEntityRepository;
        private ITestHusbandRepository TestHusbandRepository;
        private ITestWifeRepository TestWifeRepository;
        private ITestChildRepository TestChildRepository;
        private ICostOnDateRepository CostOnDateRepository;
        private IProductRepository ProductRepository;
        private IOfferRepository OfferRepository;
        private IEmployeeRepository EmployeeRepository;
        private IOrderRepository OrderRepository;
        private IPaymentConditionRepository PaymentConditionRepository;
        private IPaymentDocumentRepository PaymentDocumentRepository;
        private IFacilityRepository FacilityRepository;
        private IProjectRepository ProjectRepository;
        private IUserRoleRepository UserRoleRepository;
        private ISpecificationRepository SpecificationRepository;
        private ITenderRepository TenderRepository;
        private ITenderTypeRepository TenderTypeRepository;
        private IUserRepository UserRepository;
        #endregion
    }
}
