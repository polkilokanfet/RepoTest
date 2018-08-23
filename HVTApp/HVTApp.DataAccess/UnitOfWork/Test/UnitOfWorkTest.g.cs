using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public partial class UnitOfWorkTest
    {
        public UnitOfWorkTest(TestData testData)
        {
			#region RepositoriesInit
            CommonOptionRepository = new CommonOptionRepositoryTest(testData);
            AddressRepository = new AddressRepositoryTest(testData);
            CountryRepository = new CountryRepositoryTest(testData);
            DistrictRepository = new DistrictRepositoryTest(testData);
            LocalityRepository = new LocalityRepositoryTest(testData);
            LocalityTypeRepository = new LocalityTypeRepositoryTest(testData);
            RegionRepository = new RegionRepositoryTest(testData);
            CalculatePriceTaskRepository = new CalculatePriceTaskRepositoryTest(testData);
            SumRepository = new SumRepositoryTest(testData);
            CurrencyExchangeRateRepository = new CurrencyExchangeRateRepositoryTest(testData);
            DescribeProductBlockTaskRepository = new DescribeProductBlockTaskRepositoryTest(testData);
            NoteRepository = new NoteRepositoryTest(testData);
            OfferUnitRepository = new OfferUnitRepositoryTest(testData);
            PaymentConditionSetRepository = new PaymentConditionSetRepositoryTest(testData);
            ProductBlockRepository = new ProductBlockRepositoryTest(testData);
            ProductDependentRepository = new ProductDependentRepositoryTest(testData);
            ProductionTaskRepository = new ProductionTaskRepositoryTest(testData);
            SalesBlockRepository = new SalesBlockRepositoryTest(testData);
            BankDetailsRepository = new BankDetailsRepositoryTest(testData);
            CompanyRepository = new CompanyRepositoryTest(testData);
            CompanyFormRepository = new CompanyFormRepositoryTest(testData);
            DocumentsRegistrationDetailsRepository = new DocumentsRegistrationDetailsRepositoryTest(testData);
            EmployeesPositionRepository = new EmployeesPositionRepositoryTest(testData);
            FacilityTypeRepository = new FacilityTypeRepositoryTest(testData);
            ActivityFieldRepository = new ActivityFieldRepositoryTest(testData);
            ContractRepository = new ContractRepositoryTest(testData);
            MeasureRepository = new MeasureRepositoryTest(testData);
            ParameterRepository = new ParameterRepositoryTest(testData);
            ParameterGroupRepository = new ParameterGroupRepositoryTest(testData);
            ProductRelationRepository = new ProductRelationRepositoryTest(testData);
            PersonRepository = new PersonRepositoryTest(testData);
            PaymentPlannedListRepository = new PaymentPlannedListRepositoryTest(testData);
            PaymentPlannedRepository = new PaymentPlannedRepositoryTest(testData);
            PaymentActualRepository = new PaymentActualRepositoryTest(testData);
            ParameterRelationRepository = new ParameterRelationRepositoryTest(testData);
            SalesUnitRepository = new SalesUnitRepositoryTest(testData);
            ServiceRepository = new ServiceRepositoryTest(testData);
            TestFriendAddressRepository = new TestFriendAddressRepositoryTest(testData);
            TestFriendRepository = new TestFriendRepositoryTest(testData);
            TestFriendEmailRepository = new TestFriendEmailRepositoryTest(testData);
            TestFriendGroupRepository = new TestFriendGroupRepositoryTest(testData);
            DocumentRepository = new DocumentRepositoryTest(testData);
            TestEntityRepository = new TestEntityRepositoryTest(testData);
            TestHusbandRepository = new TestHusbandRepositoryTest(testData);
            TestWifeRepository = new TestWifeRepositoryTest(testData);
            TestChildRepository = new TestChildRepositoryTest(testData);
            SumOnDateRepository = new SumOnDateRepositoryTest(testData);
            ProductRepository = new ProductRepositoryTest(testData);
            OfferRepository = new OfferRepositoryTest(testData);
            EmployeeRepository = new EmployeeRepositoryTest(testData);
            OrderRepository = new OrderRepositoryTest(testData);
            PaymentConditionRepository = new PaymentConditionRepositoryTest(testData);
            PaymentDocumentRepository = new PaymentDocumentRepositoryTest(testData);
            FacilityRepository = new FacilityRepositoryTest(testData);
            ProjectRepository = new ProjectRepositoryTest(testData);
            UserRoleRepository = new UserRoleRepositoryTest(testData);
            SpecificationRepository = new SpecificationRepositoryTest(testData);
            TenderRepository = new TenderRepositoryTest(testData);
            TenderTypeRepository = new TenderTypeRepositoryTest(testData);
            UserRepository = new UserRepositoryTest(testData);
			#endregion
        }


        #region Repositories
        public ICommonOptionRepository CommonOptionRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IDistrictRepository DistrictRepository { get; }
        public ILocalityRepository LocalityRepository { get; }
        public ILocalityTypeRepository LocalityTypeRepository { get; }
        public IRegionRepository RegionRepository { get; }
        public ICalculatePriceTaskRepository CalculatePriceTaskRepository { get; }
        public ISumRepository SumRepository { get; }
        public ICurrencyExchangeRateRepository CurrencyExchangeRateRepository { get; }
        public IDescribeProductBlockTaskRepository DescribeProductBlockTaskRepository { get; }
        public INoteRepository NoteRepository { get; }
        public IOfferUnitRepository OfferUnitRepository { get; }
        public IPaymentConditionSetRepository PaymentConditionSetRepository { get; }
        public IProductBlockRepository ProductBlockRepository { get; }
        public IProductDependentRepository ProductDependentRepository { get; }
        public IProductionTaskRepository ProductionTaskRepository { get; }
        public ISalesBlockRepository SalesBlockRepository { get; }
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
        public IPersonRepository PersonRepository { get; }
        public IPaymentPlannedListRepository PaymentPlannedListRepository { get; }
        public IPaymentPlannedRepository PaymentPlannedRepository { get; }
        public IPaymentActualRepository PaymentActualRepository { get; }
        public IParameterRelationRepository ParameterRelationRepository { get; }
        public ISalesUnitRepository SalesUnitRepository { get; }
        public IServiceRepository ServiceRepository { get; }
        public ITestFriendAddressRepository TestFriendAddressRepository { get; }
        public ITestFriendRepository TestFriendRepository { get; }
        public ITestFriendEmailRepository TestFriendEmailRepository { get; }
        public ITestFriendGroupRepository TestFriendGroupRepository { get; }
        public IDocumentRepository DocumentRepository { get; }
        public ITestEntityRepository TestEntityRepository { get; }
        public ITestHusbandRepository TestHusbandRepository { get; }
        public ITestWifeRepository TestWifeRepository { get; }
        public ITestChildRepository TestChildRepository { get; }
        public ISumOnDateRepository SumOnDateRepository { get; }
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
        #endregion
    }
}
