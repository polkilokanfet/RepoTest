namespace HVTApp.DataAccess
{
    public partial class UnitOfWork
    {
        private void InitializeRepositories()
        {
            NotificationUnitRepository = new NotificationUnitRepository(_context);
            NotificationUnitRepository.OperationFailedEvent += OnOperationFailedEvent;

            CountryUnionRepository = new CountryUnionRepository(_context);
            CountryUnionRepository.OperationFailedEvent += OnOperationFailedEvent;

            BudgetRepository = new BudgetRepository(_context);
            BudgetRepository.OperationFailedEvent += OnOperationFailedEvent;

            BudgetUnitRepository = new BudgetUnitRepository(_context);
            BudgetUnitRepository.OperationFailedEvent += OnOperationFailedEvent;

            ConstructorParametersListRepository = new ConstructorParametersListRepository(_context);
            ConstructorParametersListRepository.OperationFailedEvent += OnOperationFailedEvent;

            ConstructorsParametersRepository = new ConstructorsParametersRepository(_context);
            ConstructorsParametersRepository.OperationFailedEvent += OnOperationFailedEvent;

            CostsPercentsRepository = new CostsPercentsRepository(_context);
            CostsPercentsRepository.OperationFailedEvent += OnOperationFailedEvent;

            CreateNewProductTaskRepository = new CreateNewProductTaskRepository(_context);
            CreateNewProductTaskRepository.OperationFailedEvent += OnOperationFailedEvent;

            InvoiceForPaymentTaskRepository = new InvoiceForPaymentTaskRepository(_context);
            InvoiceForPaymentTaskRepository.OperationFailedEvent += OnOperationFailedEvent;

            DesignDepartmentRepository = new DesignDepartmentRepository(_context);
            DesignDepartmentRepository.OperationFailedEvent += OnOperationFailedEvent;

            DirectumTaskRepository = new DirectumTaskRepository(_context);
            DirectumTaskRepository.OperationFailedEvent += OnOperationFailedEvent;

            DirectumTaskGroupRepository = new DirectumTaskGroupRepository(_context);
            DirectumTaskGroupRepository.OperationFailedEvent += OnOperationFailedEvent;

            DirectumTaskGroupFileRepository = new DirectumTaskGroupFileRepository(_context);
            DirectumTaskGroupFileRepository.OperationFailedEvent += OnOperationFailedEvent;

            DirectumTaskMessageRepository = new DirectumTaskMessageRepository(_context);
            DirectumTaskMessageRepository.OperationFailedEvent += OnOperationFailedEvent;

            DocumentNumberRepository = new DocumentNumberRepository(_context);
            DocumentNumberRepository.OperationFailedEvent += OnOperationFailedEvent;

            IncomingRequestRepository = new IncomingRequestRepository(_context);
            IncomingRequestRepository.OperationFailedEvent += OnOperationFailedEvent;

            LaborHourCostRepository = new LaborHourCostRepository(_context);
            LaborHourCostRepository.OperationFailedEvent += OnOperationFailedEvent;

            LaborHoursRepository = new LaborHoursRepository(_context);
            LaborHoursRepository.OperationFailedEvent += OnOperationFailedEvent;

            LogUnitRepository = new LogUnitRepository(_context);
            LogUnitRepository.OperationFailedEvent += OnOperationFailedEvent;

            LosingReasonRepository = new LosingReasonRepository(_context);
            LosingReasonRepository.OperationFailedEvent += OnOperationFailedEvent;

            MarketFieldRepository = new MarketFieldRepository(_context);
            MarketFieldRepository.OperationFailedEvent += OnOperationFailedEvent;

            PaymentActualRepository = new PaymentActualRepository(_context);
            PaymentActualRepository.OperationFailedEvent += OnOperationFailedEvent;

            PaymentConditionPointRepository = new PaymentConditionPointRepository(_context);
            PaymentConditionPointRepository.OperationFailedEvent += OnOperationFailedEvent;

            PaymentPlannedRepository = new PaymentPlannedRepository(_context);
            PaymentPlannedRepository.OperationFailedEvent += OnOperationFailedEvent;

            PenaltyRepository = new PenaltyRepository(_context);
            PenaltyRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceCalculationRepository = new PriceCalculationRepository(_context);
            PriceCalculationRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceCalculationFileRepository = new PriceCalculationFileRepository(_context);
            PriceCalculationFileRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceCalculationHistoryItemRepository = new PriceCalculationHistoryItemRepository(_context);
            PriceCalculationHistoryItemRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceCalculationItemRepository = new PriceCalculationItemRepository(_context);
            PriceCalculationItemRepository.OperationFailedEvent += OnOperationFailedEvent;

            DesignDepartmentParametersRepository = new DesignDepartmentParametersRepository(_context);
            DesignDepartmentParametersRepository.OperationFailedEvent += OnOperationFailedEvent;

            DesignDepartmentParametersAddedBlocksRepository = new DesignDepartmentParametersAddedBlocksRepository(_context);
            DesignDepartmentParametersAddedBlocksRepository.OperationFailedEvent += OnOperationFailedEvent;

            DesignDepartmentParametersSubTaskRepository = new DesignDepartmentParametersSubTaskRepository(_context);
            DesignDepartmentParametersSubTaskRepository.OperationFailedEvent += OnOperationFailedEvent;

            NotificationsReportsSettingsRepository = new NotificationsReportsSettingsRepository(_context);
            NotificationsReportsSettingsRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceEngineeringTaskRepository = new PriceEngineeringTaskRepository(_context);
            PriceEngineeringTaskRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceEngineeringTaskFileAnswerRepository = new PriceEngineeringTaskFileAnswerRepository(_context);
            PriceEngineeringTaskFileAnswerRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceEngineeringTaskFileTechnicalRequirementsRepository = new PriceEngineeringTaskFileTechnicalRequirementsRepository(_context);
            PriceEngineeringTaskFileTechnicalRequirementsRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceEngineeringTaskMessageRepository = new PriceEngineeringTaskMessageRepository(_context);
            PriceEngineeringTaskMessageRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceEngineeringTaskProductBlockAddedRepository = new PriceEngineeringTaskProductBlockAddedRepository(_context);
            PriceEngineeringTaskProductBlockAddedRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceEngineeringTasksRepository = new PriceEngineeringTasksRepository(_context);
            PriceEngineeringTasksRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceEngineeringTasksFileTechnicalRequirementsRepository = new PriceEngineeringTasksFileTechnicalRequirementsRepository(_context);
            PriceEngineeringTasksFileTechnicalRequirementsRepository.OperationFailedEvent += OnOperationFailedEvent;

            PriceEngineeringTaskStatusRepository = new PriceEngineeringTaskStatusRepository(_context);
            PriceEngineeringTaskStatusRepository.OperationFailedEvent += OnOperationFailedEvent;

            StructureCostVersionRepository = new StructureCostVersionRepository(_context);
            StructureCostVersionRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductCategoryRepository = new ProductCategoryRepository(_context);
            ProductCategoryRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductCategoryPriceAndCostRepository = new ProductCategoryPriceAndCostRepository(_context);
            ProductCategoryPriceAndCostRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductIncludedRepository = new ProductIncludedRepository(_context);
            ProductIncludedRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductDesignationRepository = new ProductDesignationRepository(_context);
            ProductDesignationRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductTypeRepository = new ProductTypeRepository(_context);
            ProductTypeRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductTypeDesignationRepository = new ProductTypeDesignationRepository(_context);
            ProductTypeDesignationRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProjectTypeRepository = new ProjectTypeRepository(_context);
            ProjectTypeRepository.OperationFailedEvent += OnOperationFailedEvent;

            StandartMarginalIncomeRepository = new StandartMarginalIncomeRepository(_context);
            StandartMarginalIncomeRepository.OperationFailedEvent += OnOperationFailedEvent;

            StandartProductionTermRepository = new StandartProductionTermRepository(_context);
            StandartProductionTermRepository.OperationFailedEvent += OnOperationFailedEvent;

            StructureCostRepository = new StructureCostRepository(_context);
            StructureCostRepository.OperationFailedEvent += OnOperationFailedEvent;

            SupervisionRepository = new SupervisionRepository(_context);
            SupervisionRepository.OperationFailedEvent += OnOperationFailedEvent;

            TaskInvoiceForPaymentRepository = new TaskInvoiceForPaymentRepository(_context);
            TaskInvoiceForPaymentRepository.OperationFailedEvent += OnOperationFailedEvent;

            TaskInvoiceForPaymentItemRepository = new TaskInvoiceForPaymentItemRepository(_context);
            TaskInvoiceForPaymentItemRepository.OperationFailedEvent += OnOperationFailedEvent;

            AnswerFileTceRepository = new AnswerFileTceRepository(_context);
            AnswerFileTceRepository.OperationFailedEvent += OnOperationFailedEvent;

            ShippingCostFileRepository = new ShippingCostFileRepository(_context);
            ShippingCostFileRepository.OperationFailedEvent += OnOperationFailedEvent;

            TechnicalRequrementsRepository = new TechnicalRequrementsRepository(_context);
            TechnicalRequrementsRepository.OperationFailedEvent += OnOperationFailedEvent;

            TechnicalRequrementsFileRepository = new TechnicalRequrementsFileRepository(_context);
            TechnicalRequrementsFileRepository.OperationFailedEvent += OnOperationFailedEvent;

            TechnicalRequrementsTaskRepository = new TechnicalRequrementsTaskRepository(_context);
            TechnicalRequrementsTaskRepository.OperationFailedEvent += OnOperationFailedEvent;

            TechnicalRequrementsTaskHistoryElementRepository = new TechnicalRequrementsTaskHistoryElementRepository(_context);
            TechnicalRequrementsTaskHistoryElementRepository.OperationFailedEvent += OnOperationFailedEvent;

            UserGroupRepository = new UserGroupRepository(_context);
            UserGroupRepository.OperationFailedEvent += OnOperationFailedEvent;

            GlobalPropertiesRepository = new GlobalPropertiesRepository(_context);
            GlobalPropertiesRepository.OperationFailedEvent += OnOperationFailedEvent;

            AddressRepository = new AddressRepository(_context);
            AddressRepository.OperationFailedEvent += OnOperationFailedEvent;

            CountryRepository = new CountryRepository(_context);
            CountryRepository.OperationFailedEvent += OnOperationFailedEvent;

            DistrictRepository = new DistrictRepository(_context);
            DistrictRepository.OperationFailedEvent += OnOperationFailedEvent;

            LocalityRepository = new LocalityRepository(_context);
            LocalityRepository.OperationFailedEvent += OnOperationFailedEvent;

            LocalityTypeRepository = new LocalityTypeRepository(_context);
            LocalityTypeRepository.OperationFailedEvent += OnOperationFailedEvent;

            RegionRepository = new RegionRepository(_context);
            RegionRepository.OperationFailedEvent += OnOperationFailedEvent;

            SumRepository = new SumRepository(_context);
            SumRepository.OperationFailedEvent += OnOperationFailedEvent;

            CurrencyExchangeRateRepository = new CurrencyExchangeRateRepository(_context);
            CurrencyExchangeRateRepository.OperationFailedEvent += OnOperationFailedEvent;

            NoteRepository = new NoteRepository(_context);
            NoteRepository.OperationFailedEvent += OnOperationFailedEvent;

            OfferUnitRepository = new OfferUnitRepository(_context);
            OfferUnitRepository.OperationFailedEvent += OnOperationFailedEvent;

            PaymentConditionSetRepository = new PaymentConditionSetRepository(_context);
            PaymentConditionSetRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductBlockRepository = new ProductBlockRepository(_context);
            ProductBlockRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductDependentRepository = new ProductDependentRepository(_context);
            ProductDependentRepository.OperationFailedEvent += OnOperationFailedEvent;

            BankDetailsRepository = new BankDetailsRepository(_context);
            BankDetailsRepository.OperationFailedEvent += OnOperationFailedEvent;

            CompanyRepository = new CompanyRepository(_context);
            CompanyRepository.OperationFailedEvent += OnOperationFailedEvent;

            CompanyFormRepository = new CompanyFormRepository(_context);
            CompanyFormRepository.OperationFailedEvent += OnOperationFailedEvent;

            DocumentsRegistrationDetailsRepository = new DocumentsRegistrationDetailsRepository(_context);
            DocumentsRegistrationDetailsRepository.OperationFailedEvent += OnOperationFailedEvent;

            EmployeesPositionRepository = new EmployeesPositionRepository(_context);
            EmployeesPositionRepository.OperationFailedEvent += OnOperationFailedEvent;

            FacilityTypeRepository = new FacilityTypeRepository(_context);
            FacilityTypeRepository.OperationFailedEvent += OnOperationFailedEvent;

            ActivityFieldRepository = new ActivityFieldRepository(_context);
            ActivityFieldRepository.OperationFailedEvent += OnOperationFailedEvent;

            ContractRepository = new ContractRepository(_context);
            ContractRepository.OperationFailedEvent += OnOperationFailedEvent;

            MeasureRepository = new MeasureRepository(_context);
            MeasureRepository.OperationFailedEvent += OnOperationFailedEvent;

            ParameterRepository = new ParameterRepository(_context);
            ParameterRepository.OperationFailedEvent += OnOperationFailedEvent;

            ParameterGroupRepository = new ParameterGroupRepository(_context);
            ParameterGroupRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductRelationRepository = new ProductRelationRepository(_context);
            ProductRelationRepository.OperationFailedEvent += OnOperationFailedEvent;

            PersonRepository = new PersonRepository(_context);
            PersonRepository.OperationFailedEvent += OnOperationFailedEvent;

            ParameterRelationRepository = new ParameterRelationRepository(_context);
            ParameterRelationRepository.OperationFailedEvent += OnOperationFailedEvent;

            SalesUnitRepository = new SalesUnitRepository(_context);
            SalesUnitRepository.OperationFailedEvent += OnOperationFailedEvent;

            DocumentRepository = new DocumentRepository(_context);
            DocumentRepository.OperationFailedEvent += OnOperationFailedEvent;

            SumOnDateRepository = new SumOnDateRepository(_context);
            SumOnDateRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProductRepository = new ProductRepository(_context);
            ProductRepository.OperationFailedEvent += OnOperationFailedEvent;

            OfferRepository = new OfferRepository(_context);
            OfferRepository.OperationFailedEvent += OnOperationFailedEvent;

            EmployeeRepository = new EmployeeRepository(_context);
            EmployeeRepository.OperationFailedEvent += OnOperationFailedEvent;

            OrderRepository = new OrderRepository(_context);
            OrderRepository.OperationFailedEvent += OnOperationFailedEvent;

            PaymentConditionRepository = new PaymentConditionRepository(_context);
            PaymentConditionRepository.OperationFailedEvent += OnOperationFailedEvent;

            PaymentDocumentRepository = new PaymentDocumentRepository(_context);
            PaymentDocumentRepository.OperationFailedEvent += OnOperationFailedEvent;

            FacilityRepository = new FacilityRepository(_context);
            FacilityRepository.OperationFailedEvent += OnOperationFailedEvent;

            ProjectRepository = new ProjectRepository(_context);
            ProjectRepository.OperationFailedEvent += OnOperationFailedEvent;

            UserRoleRepository = new UserRoleRepository(_context);
            UserRoleRepository.OperationFailedEvent += OnOperationFailedEvent;

            SpecificationRepository = new SpecificationRepository(_context);
            SpecificationRepository.OperationFailedEvent += OnOperationFailedEvent;

            TenderRepository = new TenderRepository(_context);
            TenderRepository.OperationFailedEvent += OnOperationFailedEvent;

            TenderTypeRepository = new TenderTypeRepository(_context);
            TenderTypeRepository.OperationFailedEvent += OnOperationFailedEvent;

            UserRepository = new UserRepository(_context);
            UserRepository.OperationFailedEvent += OnOperationFailedEvent;

        }

        private void DisposeRepositories()
        {
            NotificationUnitRepository.OperationFailedEvent -= OnOperationFailedEvent;
            CountryUnionRepository.OperationFailedEvent -= OnOperationFailedEvent;
            BudgetRepository.OperationFailedEvent -= OnOperationFailedEvent;
            BudgetUnitRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ConstructorParametersListRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ConstructorsParametersRepository.OperationFailedEvent -= OnOperationFailedEvent;
            CostsPercentsRepository.OperationFailedEvent -= OnOperationFailedEvent;
            CreateNewProductTaskRepository.OperationFailedEvent -= OnOperationFailedEvent;
            InvoiceForPaymentTaskRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DesignDepartmentRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DirectumTaskRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DirectumTaskGroupRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DirectumTaskGroupFileRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DirectumTaskMessageRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DocumentNumberRepository.OperationFailedEvent -= OnOperationFailedEvent;
            IncomingRequestRepository.OperationFailedEvent -= OnOperationFailedEvent;
            LaborHourCostRepository.OperationFailedEvent -= OnOperationFailedEvent;
            LaborHoursRepository.OperationFailedEvent -= OnOperationFailedEvent;
            LogUnitRepository.OperationFailedEvent -= OnOperationFailedEvent;
            LosingReasonRepository.OperationFailedEvent -= OnOperationFailedEvent;
            MarketFieldRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PaymentActualRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PaymentConditionPointRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PaymentPlannedRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PenaltyRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceCalculationRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceCalculationFileRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceCalculationHistoryItemRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceCalculationItemRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DesignDepartmentParametersRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DesignDepartmentParametersAddedBlocksRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DesignDepartmentParametersSubTaskRepository.OperationFailedEvent -= OnOperationFailedEvent;
            NotificationsReportsSettingsRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceEngineeringTaskRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceEngineeringTaskFileAnswerRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceEngineeringTaskFileTechnicalRequirementsRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceEngineeringTaskMessageRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceEngineeringTaskProductBlockAddedRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceEngineeringTasksRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceEngineeringTasksFileTechnicalRequirementsRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PriceEngineeringTaskStatusRepository.OperationFailedEvent -= OnOperationFailedEvent;
            StructureCostVersionRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductCategoryRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductCategoryPriceAndCostRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductIncludedRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductDesignationRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductTypeRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductTypeDesignationRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProjectTypeRepository.OperationFailedEvent -= OnOperationFailedEvent;
            StandartMarginalIncomeRepository.OperationFailedEvent -= OnOperationFailedEvent;
            StandartProductionTermRepository.OperationFailedEvent -= OnOperationFailedEvent;
            StructureCostRepository.OperationFailedEvent -= OnOperationFailedEvent;
            SupervisionRepository.OperationFailedEvent -= OnOperationFailedEvent;
            TaskInvoiceForPaymentRepository.OperationFailedEvent -= OnOperationFailedEvent;
            TaskInvoiceForPaymentItemRepository.OperationFailedEvent -= OnOperationFailedEvent;
            AnswerFileTceRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ShippingCostFileRepository.OperationFailedEvent -= OnOperationFailedEvent;
            TechnicalRequrementsRepository.OperationFailedEvent -= OnOperationFailedEvent;
            TechnicalRequrementsFileRepository.OperationFailedEvent -= OnOperationFailedEvent;
            TechnicalRequrementsTaskRepository.OperationFailedEvent -= OnOperationFailedEvent;
            TechnicalRequrementsTaskHistoryElementRepository.OperationFailedEvent -= OnOperationFailedEvent;
            UserGroupRepository.OperationFailedEvent -= OnOperationFailedEvent;
            GlobalPropertiesRepository.OperationFailedEvent -= OnOperationFailedEvent;
            AddressRepository.OperationFailedEvent -= OnOperationFailedEvent;
            CountryRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DistrictRepository.OperationFailedEvent -= OnOperationFailedEvent;
            LocalityRepository.OperationFailedEvent -= OnOperationFailedEvent;
            LocalityTypeRepository.OperationFailedEvent -= OnOperationFailedEvent;
            RegionRepository.OperationFailedEvent -= OnOperationFailedEvent;
            SumRepository.OperationFailedEvent -= OnOperationFailedEvent;
            CurrencyExchangeRateRepository.OperationFailedEvent -= OnOperationFailedEvent;
            NoteRepository.OperationFailedEvent -= OnOperationFailedEvent;
            OfferUnitRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PaymentConditionSetRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductBlockRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductDependentRepository.OperationFailedEvent -= OnOperationFailedEvent;
            BankDetailsRepository.OperationFailedEvent -= OnOperationFailedEvent;
            CompanyRepository.OperationFailedEvent -= OnOperationFailedEvent;
            CompanyFormRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DocumentsRegistrationDetailsRepository.OperationFailedEvent -= OnOperationFailedEvent;
            EmployeesPositionRepository.OperationFailedEvent -= OnOperationFailedEvent;
            FacilityTypeRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ActivityFieldRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ContractRepository.OperationFailedEvent -= OnOperationFailedEvent;
            MeasureRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ParameterRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ParameterGroupRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductRelationRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PersonRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ParameterRelationRepository.OperationFailedEvent -= OnOperationFailedEvent;
            SalesUnitRepository.OperationFailedEvent -= OnOperationFailedEvent;
            DocumentRepository.OperationFailedEvent -= OnOperationFailedEvent;
            SumOnDateRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProductRepository.OperationFailedEvent -= OnOperationFailedEvent;
            OfferRepository.OperationFailedEvent -= OnOperationFailedEvent;
            EmployeeRepository.OperationFailedEvent -= OnOperationFailedEvent;
            OrderRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PaymentConditionRepository.OperationFailedEvent -= OnOperationFailedEvent;
            PaymentDocumentRepository.OperationFailedEvent -= OnOperationFailedEvent;
            FacilityRepository.OperationFailedEvent -= OnOperationFailedEvent;
            ProjectRepository.OperationFailedEvent -= OnOperationFailedEvent;
            UserRoleRepository.OperationFailedEvent -= OnOperationFailedEvent;
            SpecificationRepository.OperationFailedEvent -= OnOperationFailedEvent;
            TenderRepository.OperationFailedEvent -= OnOperationFailedEvent;
            TenderTypeRepository.OperationFailedEvent -= OnOperationFailedEvent;
            UserRepository.OperationFailedEvent -= OnOperationFailedEvent;
        }

        #region Repositories
        protected INotificationUnitRepository NotificationUnitRepository;
        protected ICountryUnionRepository CountryUnionRepository;
        protected IBudgetRepository BudgetRepository;
        protected IBudgetUnitRepository BudgetUnitRepository;
        protected IConstructorParametersListRepository ConstructorParametersListRepository;
        protected IConstructorsParametersRepository ConstructorsParametersRepository;
        protected ICostsPercentsRepository CostsPercentsRepository;
        protected ICreateNewProductTaskRepository CreateNewProductTaskRepository;
        protected IInvoiceForPaymentTaskRepository InvoiceForPaymentTaskRepository;
        protected IDesignDepartmentRepository DesignDepartmentRepository;
        protected IDirectumTaskRepository DirectumTaskRepository;
        protected IDirectumTaskGroupRepository DirectumTaskGroupRepository;
        protected IDirectumTaskGroupFileRepository DirectumTaskGroupFileRepository;
        protected IDirectumTaskMessageRepository DirectumTaskMessageRepository;
        protected IDocumentNumberRepository DocumentNumberRepository;
        protected IIncomingRequestRepository IncomingRequestRepository;
        protected ILaborHourCostRepository LaborHourCostRepository;
        protected ILaborHoursRepository LaborHoursRepository;
        protected ILogUnitRepository LogUnitRepository;
        protected ILosingReasonRepository LosingReasonRepository;
        protected IMarketFieldRepository MarketFieldRepository;
        protected IPaymentActualRepository PaymentActualRepository;
        protected IPaymentConditionPointRepository PaymentConditionPointRepository;
        protected IPaymentPlannedRepository PaymentPlannedRepository;
        protected IPenaltyRepository PenaltyRepository;
        protected IPriceCalculationRepository PriceCalculationRepository;
        protected IPriceCalculationFileRepository PriceCalculationFileRepository;
        protected IPriceCalculationHistoryItemRepository PriceCalculationHistoryItemRepository;
        protected IPriceCalculationItemRepository PriceCalculationItemRepository;
        protected IDesignDepartmentParametersRepository DesignDepartmentParametersRepository;
        protected IDesignDepartmentParametersAddedBlocksRepository DesignDepartmentParametersAddedBlocksRepository;
        protected IDesignDepartmentParametersSubTaskRepository DesignDepartmentParametersSubTaskRepository;
        protected INotificationsReportsSettingsRepository NotificationsReportsSettingsRepository;
        protected IPriceEngineeringTaskRepository PriceEngineeringTaskRepository;
        protected IPriceEngineeringTaskFileAnswerRepository PriceEngineeringTaskFileAnswerRepository;
        protected IPriceEngineeringTaskFileTechnicalRequirementsRepository PriceEngineeringTaskFileTechnicalRequirementsRepository;
        protected IPriceEngineeringTaskMessageRepository PriceEngineeringTaskMessageRepository;
        protected IPriceEngineeringTaskProductBlockAddedRepository PriceEngineeringTaskProductBlockAddedRepository;
        protected IPriceEngineeringTasksRepository PriceEngineeringTasksRepository;
        protected IPriceEngineeringTasksFileTechnicalRequirementsRepository PriceEngineeringTasksFileTechnicalRequirementsRepository;
        protected IPriceEngineeringTaskStatusRepository PriceEngineeringTaskStatusRepository;
        protected IStructureCostVersionRepository StructureCostVersionRepository;
        protected IProductCategoryRepository ProductCategoryRepository;
        protected IProductCategoryPriceAndCostRepository ProductCategoryPriceAndCostRepository;
        protected IProductIncludedRepository ProductIncludedRepository;
        protected IProductDesignationRepository ProductDesignationRepository;
        protected IProductTypeRepository ProductTypeRepository;
        protected IProductTypeDesignationRepository ProductTypeDesignationRepository;
        protected IProjectTypeRepository ProjectTypeRepository;
        protected IStandartMarginalIncomeRepository StandartMarginalIncomeRepository;
        protected IStandartProductionTermRepository StandartProductionTermRepository;
        protected IStructureCostRepository StructureCostRepository;
        protected ISupervisionRepository SupervisionRepository;
        protected ITaskInvoiceForPaymentRepository TaskInvoiceForPaymentRepository;
        protected ITaskInvoiceForPaymentItemRepository TaskInvoiceForPaymentItemRepository;
        protected IAnswerFileTceRepository AnswerFileTceRepository;
        protected IShippingCostFileRepository ShippingCostFileRepository;
        protected ITechnicalRequrementsRepository TechnicalRequrementsRepository;
        protected ITechnicalRequrementsFileRepository TechnicalRequrementsFileRepository;
        protected ITechnicalRequrementsTaskRepository TechnicalRequrementsTaskRepository;
        protected ITechnicalRequrementsTaskHistoryElementRepository TechnicalRequrementsTaskHistoryElementRepository;
        protected IUserGroupRepository UserGroupRepository;
        protected IGlobalPropertiesRepository GlobalPropertiesRepository;
        protected IAddressRepository AddressRepository;
        protected ICountryRepository CountryRepository;
        protected IDistrictRepository DistrictRepository;
        protected ILocalityRepository LocalityRepository;
        protected ILocalityTypeRepository LocalityTypeRepository;
        protected IRegionRepository RegionRepository;
        protected ISumRepository SumRepository;
        protected ICurrencyExchangeRateRepository CurrencyExchangeRateRepository;
        protected INoteRepository NoteRepository;
        protected IOfferUnitRepository OfferUnitRepository;
        protected IPaymentConditionSetRepository PaymentConditionSetRepository;
        protected IProductBlockRepository ProductBlockRepository;
        protected IProductDependentRepository ProductDependentRepository;
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
        protected IDocumentRepository DocumentRepository;
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
