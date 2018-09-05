using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class UnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context, IUnityContainer container)
        {
            _context = context;
			#region RepositoriesInit
            CreateNewProductTaskRepository = new CreateNewProductTaskRepository(context, container);
            PaymentActualRepository = new PaymentActualRepository(context, container);
            PaymentPlannedRepository = new PaymentPlannedRepository(context, container);
            ProductBlockIsServiceRepository = new ProductBlockIsServiceRepository(context, container);
            ProductIncludedRepository = new ProductIncludedRepository(context, container);
            ProductDesignationRepository = new ProductDesignationRepository(context, container);
            ProductTypeRepository = new ProductTypeRepository(context, container);
            ProductTypeDesignationRepository = new ProductTypeDesignationRepository(context, container);
            ProjectTypeRepository = new ProjectTypeRepository(context, container);
            CommonOptionRepository = new CommonOptionRepository(context, container);
            AddressRepository = new AddressRepository(context, container);
            CountryRepository = new CountryRepository(context, container);
            DistrictRepository = new DistrictRepository(context, container);
            LocalityRepository = new LocalityRepository(context, container);
            LocalityTypeRepository = new LocalityTypeRepository(context, container);
            RegionRepository = new RegionRepository(context, container);
            CalculatePriceTaskRepository = new CalculatePriceTaskRepository(context, container);
            SumRepository = new SumRepository(context, container);
            CurrencyExchangeRateRepository = new CurrencyExchangeRateRepository(context, container);
            DescribeProductBlockTaskRepository = new DescribeProductBlockTaskRepository(context, container);
            NoteRepository = new NoteRepository(context, container);
            OfferUnitRepository = new OfferUnitRepository(context, container);
            PaymentConditionSetRepository = new PaymentConditionSetRepository(context, container);
            ProductBlockRepository = new ProductBlockRepository(context, container);
            ProductDependentRepository = new ProductDependentRepository(context, container);
            ProductionTaskRepository = new ProductionTaskRepository(context, container);
            SalesBlockRepository = new SalesBlockRepository(context, container);
            BankDetailsRepository = new BankDetailsRepository(context, container);
            CompanyRepository = new CompanyRepository(context, container);
            CompanyFormRepository = new CompanyFormRepository(context, container);
            DocumentsRegistrationDetailsRepository = new DocumentsRegistrationDetailsRepository(context, container);
            EmployeesPositionRepository = new EmployeesPositionRepository(context, container);
            FacilityTypeRepository = new FacilityTypeRepository(context, container);
            ActivityFieldRepository = new ActivityFieldRepository(context, container);
            ContractRepository = new ContractRepository(context, container);
            MeasureRepository = new MeasureRepository(context, container);
            ParameterRepository = new ParameterRepository(context, container);
            ParameterGroupRepository = new ParameterGroupRepository(context, container);
            ProductRelationRepository = new ProductRelationRepository(context, container);
            PersonRepository = new PersonRepository(context, container);
            ParameterRelationRepository = new ParameterRelationRepository(context, container);
            SalesUnitRepository = new SalesUnitRepository(context, container);
            ServiceRepository = new ServiceRepository(context, container);
            TestFriendAddressRepository = new TestFriendAddressRepository(context, container);
            TestFriendRepository = new TestFriendRepository(context, container);
            TestFriendEmailRepository = new TestFriendEmailRepository(context, container);
            TestFriendGroupRepository = new TestFriendGroupRepository(context, container);
            DocumentRepository = new DocumentRepository(context, container);
            TestEntityRepository = new TestEntityRepository(context, container);
            TestHusbandRepository = new TestHusbandRepository(context, container);
            TestWifeRepository = new TestWifeRepository(context, container);
            TestChildRepository = new TestChildRepository(context, container);
            SumOnDateRepository = new SumOnDateRepository(context, container);
            ProductRepository = new ProductRepository(context, container);
            OfferRepository = new OfferRepository(context, container);
            EmployeeRepository = new EmployeeRepository(context, container);
            OrderRepository = new OrderRepository(context, container);
            PaymentConditionRepository = new PaymentConditionRepository(context, container);
            PaymentDocumentRepository = new PaymentDocumentRepository(context, container);
            FacilityRepository = new FacilityRepository(context, container);
            ProjectRepository = new ProjectRepository(context, container);
            UserRoleRepository = new UserRoleRepository(context, container);
            SpecificationRepository = new SpecificationRepository(context, container);
            TenderRepository = new TenderRepository(context, container);
            TenderTypeRepository = new TenderTypeRepository(context, container);
            UserRepository = new UserRepository(context, container);
			#endregion
        }


        #region Repositories
        protected ICreateNewProductTaskRepository CreateNewProductTaskRepository;
        protected IPaymentActualRepository PaymentActualRepository;
        protected IPaymentPlannedRepository PaymentPlannedRepository;
        protected IProductBlockIsServiceRepository ProductBlockIsServiceRepository;
        protected IProductIncludedRepository ProductIncludedRepository;
        protected IProductDesignationRepository ProductDesignationRepository;
        protected IProductTypeRepository ProductTypeRepository;
        protected IProductTypeDesignationRepository ProductTypeDesignationRepository;
        protected IProjectTypeRepository ProjectTypeRepository;
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
