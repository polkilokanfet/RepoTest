using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public partial class UnitOfWorkTest
    {
        public UnitOfWorkTest(TestData testData)
        {
			#region RepositoriesInit
            NotificationUnitRepository = new NotificationUnitRepositoryTest(testData);
            CountryUnionRepository = new CountryUnionRepositoryTest(testData);
            BudgetRepository = new BudgetRepositoryTest(testData);
            BudgetUnitRepository = new BudgetUnitRepositoryTest(testData);
            ConstructorParametersListRepository = new ConstructorParametersListRepositoryTest(testData);
            ConstructorsParametersRepository = new ConstructorsParametersRepositoryTest(testData);
            CostsPercentsRepository = new CostsPercentsRepositoryTest(testData);
            CreateNewProductTaskRepository = new CreateNewProductTaskRepositoryTest(testData);
            DesignDepartmentRepository = new DesignDepartmentRepositoryTest(testData);
            DirectumTaskRepository = new DirectumTaskRepositoryTest(testData);
            DirectumTaskGroupRepository = new DirectumTaskGroupRepositoryTest(testData);
            DirectumTaskGroupFileRepository = new DirectumTaskGroupFileRepositoryTest(testData);
            DirectumTaskMessageRepository = new DirectumTaskMessageRepositoryTest(testData);
            DocumentNumberRepository = new DocumentNumberRepositoryTest(testData);
            IncomingRequestRepository = new IncomingRequestRepositoryTest(testData);
            LaborHourCostRepository = new LaborHourCostRepositoryTest(testData);
            LaborHoursRepository = new LaborHoursRepositoryTest(testData);
            LogUnitRepository = new LogUnitRepositoryTest(testData);
            LosingReasonRepository = new LosingReasonRepositoryTest(testData);
            MarketFieldRepository = new MarketFieldRepositoryTest(testData);
            PaymentActualRepository = new PaymentActualRepositoryTest(testData);
            PaymentConditionPointRepository = new PaymentConditionPointRepositoryTest(testData);
            PaymentPlannedRepository = new PaymentPlannedRepositoryTest(testData);
            PenaltyRepository = new PenaltyRepositoryTest(testData);
            PriceCalculationRepository = new PriceCalculationRepositoryTest(testData);
            PriceCalculationFileRepository = new PriceCalculationFileRepositoryTest(testData);
            PriceCalculationHistoryItemRepository = new PriceCalculationHistoryItemRepositoryTest(testData);
            PriceCalculationItemRepository = new PriceCalculationItemRepositoryTest(testData);
            DesignDepartmentParametersRepository = new DesignDepartmentParametersRepositoryTest(testData);
            DesignDepartmentParametersAddedBlocksRepository = new DesignDepartmentParametersAddedBlocksRepositoryTest(testData);
            DesignDepartmentParametersSubTaskRepository = new DesignDepartmentParametersSubTaskRepositoryTest(testData);
            NotificationsReportsSettingsRepository = new NotificationsReportsSettingsRepositoryTest(testData);
            PriceEngineeringTaskRepository = new PriceEngineeringTaskRepositoryTest(testData);
            PriceEngineeringTaskFileAnswerRepository = new PriceEngineeringTaskFileAnswerRepositoryTest(testData);
            PriceEngineeringTaskFileTechnicalRequirementsRepository = new PriceEngineeringTaskFileTechnicalRequirementsRepositoryTest(testData);
            PriceEngineeringTaskMessageRepository = new PriceEngineeringTaskMessageRepositoryTest(testData);
            PriceEngineeringTaskProductBlockAddedRepository = new PriceEngineeringTaskProductBlockAddedRepositoryTest(testData);
            PriceEngineeringTasksRepository = new PriceEngineeringTasksRepositoryTest(testData);
            PriceEngineeringTasksFileTechnicalRequirementsRepository = new PriceEngineeringTasksFileTechnicalRequirementsRepositoryTest(testData);
            PriceEngineeringTaskStatusRepository = new PriceEngineeringTaskStatusRepositoryTest(testData);
            StructureCostVersionRepository = new StructureCostVersionRepositoryTest(testData);
            UpdateStructureCostNumberTaskRepository = new UpdateStructureCostNumberTaskRepositoryTest(testData);
            ProductCategoryRepository = new ProductCategoryRepositoryTest(testData);
            ProductCategoryPriceAndCostRepository = new ProductCategoryPriceAndCostRepositoryTest(testData);
            ProductIncludedRepository = new ProductIncludedRepositoryTest(testData);
            ProductDesignationRepository = new ProductDesignationRepositoryTest(testData);
            ProductTypeRepository = new ProductTypeRepositoryTest(testData);
            ProductTypeDesignationRepository = new ProductTypeDesignationRepositoryTest(testData);
            ProjectTypeRepository = new ProjectTypeRepositoryTest(testData);
            StandartMarginalIncomeRepository = new StandartMarginalIncomeRepositoryTest(testData);
            StandartProductionTermRepository = new StandartProductionTermRepositoryTest(testData);
            StructureCostRepository = new StructureCostRepositoryTest(testData);
            SupervisionRepository = new SupervisionRepositoryTest(testData);
            TaskInvoiceForPaymentRepository = new TaskInvoiceForPaymentRepositoryTest(testData);
            TaskInvoiceForPaymentItemRepository = new TaskInvoiceForPaymentItemRepositoryTest(testData);
            AnswerFileTceRepository = new AnswerFileTceRepositoryTest(testData);
            ShippingCostFileRepository = new ShippingCostFileRepositoryTest(testData);
            TechnicalRequrementsRepository = new TechnicalRequrementsRepositoryTest(testData);
            TechnicalRequrementsFileRepository = new TechnicalRequrementsFileRepositoryTest(testData);
            TechnicalRequrementsTaskRepository = new TechnicalRequrementsTaskRepositoryTest(testData);
            TechnicalRequrementsTaskHistoryElementRepository = new TechnicalRequrementsTaskHistoryElementRepositoryTest(testData);
            UserGroupRepository = new UserGroupRepositoryTest(testData);
            GlobalPropertiesRepository = new GlobalPropertiesRepositoryTest(testData);
            AddressRepository = new AddressRepositoryTest(testData);
            CountryRepository = new CountryRepositoryTest(testData);
            DistrictRepository = new DistrictRepositoryTest(testData);
            LocalityRepository = new LocalityRepositoryTest(testData);
            LocalityTypeRepository = new LocalityTypeRepositoryTest(testData);
            RegionRepository = new RegionRepositoryTest(testData);
            SumRepository = new SumRepositoryTest(testData);
            CurrencyExchangeRateRepository = new CurrencyExchangeRateRepositoryTest(testData);
            NoteRepository = new NoteRepositoryTest(testData);
            OfferUnitRepository = new OfferUnitRepositoryTest(testData);
            PaymentConditionSetRepository = new PaymentConditionSetRepositoryTest(testData);
            ProductBlockRepository = new ProductBlockRepositoryTest(testData);
            ProductDependentRepository = new ProductDependentRepositoryTest(testData);
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
            ParameterRelationRepository = new ParameterRelationRepositoryTest(testData);
            SalesUnitRepository = new SalesUnitRepositoryTest(testData);
            DocumentRepository = new DocumentRepositoryTest(testData);
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
        public INotificationUnitRepository NotificationUnitRepository { get; }
        public ICountryUnionRepository CountryUnionRepository { get; }
        public IBudgetRepository BudgetRepository { get; }
        public IBudgetUnitRepository BudgetUnitRepository { get; }
        public IConstructorParametersListRepository ConstructorParametersListRepository { get; }
        public IConstructorsParametersRepository ConstructorsParametersRepository { get; }
        public ICostsPercentsRepository CostsPercentsRepository { get; }
        public ICreateNewProductTaskRepository CreateNewProductTaskRepository { get; }
        public IDesignDepartmentRepository DesignDepartmentRepository { get; }
        public IDirectumTaskRepository DirectumTaskRepository { get; }
        public IDirectumTaskGroupRepository DirectumTaskGroupRepository { get; }
        public IDirectumTaskGroupFileRepository DirectumTaskGroupFileRepository { get; }
        public IDirectumTaskMessageRepository DirectumTaskMessageRepository { get; }
        public IDocumentNumberRepository DocumentNumberRepository { get; }
        public IIncomingRequestRepository IncomingRequestRepository { get; }
        public ILaborHourCostRepository LaborHourCostRepository { get; }
        public ILaborHoursRepository LaborHoursRepository { get; }
        public ILogUnitRepository LogUnitRepository { get; }
        public ILosingReasonRepository LosingReasonRepository { get; }
        public IMarketFieldRepository MarketFieldRepository { get; }
        public IPaymentActualRepository PaymentActualRepository { get; }
        public IPaymentConditionPointRepository PaymentConditionPointRepository { get; }
        public IPaymentPlannedRepository PaymentPlannedRepository { get; }
        public IPenaltyRepository PenaltyRepository { get; }
        public IPriceCalculationRepository PriceCalculationRepository { get; }
        public IPriceCalculationFileRepository PriceCalculationFileRepository { get; }
        public IPriceCalculationHistoryItemRepository PriceCalculationHistoryItemRepository { get; }
        public IPriceCalculationItemRepository PriceCalculationItemRepository { get; }
        public IDesignDepartmentParametersRepository DesignDepartmentParametersRepository { get; }
        public IDesignDepartmentParametersAddedBlocksRepository DesignDepartmentParametersAddedBlocksRepository { get; }
        public IDesignDepartmentParametersSubTaskRepository DesignDepartmentParametersSubTaskRepository { get; }
        public INotificationsReportsSettingsRepository NotificationsReportsSettingsRepository { get; }
        public IPriceEngineeringTaskRepository PriceEngineeringTaskRepository { get; }
        public IPriceEngineeringTaskFileAnswerRepository PriceEngineeringTaskFileAnswerRepository { get; }
        public IPriceEngineeringTaskFileTechnicalRequirementsRepository PriceEngineeringTaskFileTechnicalRequirementsRepository { get; }
        public IPriceEngineeringTaskMessageRepository PriceEngineeringTaskMessageRepository { get; }
        public IPriceEngineeringTaskProductBlockAddedRepository PriceEngineeringTaskProductBlockAddedRepository { get; }
        public IPriceEngineeringTasksRepository PriceEngineeringTasksRepository { get; }
        public IPriceEngineeringTasksFileTechnicalRequirementsRepository PriceEngineeringTasksFileTechnicalRequirementsRepository { get; }
        public IPriceEngineeringTaskStatusRepository PriceEngineeringTaskStatusRepository { get; }
        public IStructureCostVersionRepository StructureCostVersionRepository { get; }
        public IUpdateStructureCostNumberTaskRepository UpdateStructureCostNumberTaskRepository { get; }
        public IProductCategoryRepository ProductCategoryRepository { get; }
        public IProductCategoryPriceAndCostRepository ProductCategoryPriceAndCostRepository { get; }
        public IProductIncludedRepository ProductIncludedRepository { get; }
        public IProductDesignationRepository ProductDesignationRepository { get; }
        public IProductTypeRepository ProductTypeRepository { get; }
        public IProductTypeDesignationRepository ProductTypeDesignationRepository { get; }
        public IProjectTypeRepository ProjectTypeRepository { get; }
        public IStandartMarginalIncomeRepository StandartMarginalIncomeRepository { get; }
        public IStandartProductionTermRepository StandartProductionTermRepository { get; }
        public IStructureCostRepository StructureCostRepository { get; }
        public ISupervisionRepository SupervisionRepository { get; }
        public ITaskInvoiceForPaymentRepository TaskInvoiceForPaymentRepository { get; }
        public ITaskInvoiceForPaymentItemRepository TaskInvoiceForPaymentItemRepository { get; }
        public IAnswerFileTceRepository AnswerFileTceRepository { get; }
        public IShippingCostFileRepository ShippingCostFileRepository { get; }
        public ITechnicalRequrementsRepository TechnicalRequrementsRepository { get; }
        public ITechnicalRequrementsFileRepository TechnicalRequrementsFileRepository { get; }
        public ITechnicalRequrementsTaskRepository TechnicalRequrementsTaskRepository { get; }
        public ITechnicalRequrementsTaskHistoryElementRepository TechnicalRequrementsTaskHistoryElementRepository { get; }
        public IUserGroupRepository UserGroupRepository { get; }
        public IGlobalPropertiesRepository GlobalPropertiesRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IDistrictRepository DistrictRepository { get; }
        public ILocalityRepository LocalityRepository { get; }
        public ILocalityTypeRepository LocalityTypeRepository { get; }
        public IRegionRepository RegionRepository { get; }
        public ISumRepository SumRepository { get; }
        public ICurrencyExchangeRateRepository CurrencyExchangeRateRepository { get; }
        public INoteRepository NoteRepository { get; }
        public IOfferUnitRepository OfferUnitRepository { get; }
        public IPaymentConditionSetRepository PaymentConditionSetRepository { get; }
        public IProductBlockRepository ProductBlockRepository { get; }
        public IProductDependentRepository ProductDependentRepository { get; }
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
        public IParameterRelationRepository ParameterRelationRepository { get; }
        public ISalesUnitRepository SalesUnitRepository { get; }
        public IDocumentRepository DocumentRepository { get; }
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
