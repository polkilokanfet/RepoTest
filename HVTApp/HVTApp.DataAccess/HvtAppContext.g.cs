using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    using System.Data.Entity;

    public partial class HvtAppContext
    {
        protected void AddConfigurations(DbModelBuilder modelBuilder)
        {
			#region Configurations
            modelBuilder.Configurations.Add(new NotificationUnitConfiguration());
            modelBuilder.Configurations.Add(new CountryUnionConfiguration());
            modelBuilder.Configurations.Add(new BudgetConfiguration());
            modelBuilder.Configurations.Add(new BudgetUnitConfiguration());
            modelBuilder.Configurations.Add(new ConstructorParametersListConfiguration());
            modelBuilder.Configurations.Add(new ConstructorsParametersConfiguration());
            modelBuilder.Configurations.Add(new CostsPercentsConfiguration());
            modelBuilder.Configurations.Add(new CreateNewProductTaskConfiguration());
            modelBuilder.Configurations.Add(new DesignDepartmentConfiguration());
            modelBuilder.Configurations.Add(new DirectumTaskConfiguration());
            modelBuilder.Configurations.Add(new DirectumTaskGroupConfiguration());
            modelBuilder.Configurations.Add(new DirectumTaskGroupFileConfiguration());
            modelBuilder.Configurations.Add(new DirectumTaskMessageConfiguration());
            modelBuilder.Configurations.Add(new DocumentNumberConfiguration());
            modelBuilder.Configurations.Add(new IncomingRequestConfiguration());
            modelBuilder.Configurations.Add(new LaborHourCostConfiguration());
            modelBuilder.Configurations.Add(new LaborHoursConfiguration());
            modelBuilder.Configurations.Add(new LogUnitConfiguration());
            modelBuilder.Configurations.Add(new LosingReasonConfiguration());
            modelBuilder.Configurations.Add(new MarketFieldConfiguration());
            modelBuilder.Configurations.Add(new PaymentActualConfiguration());
            modelBuilder.Configurations.Add(new PaymentConditionPointConfiguration());
            modelBuilder.Configurations.Add(new PaymentPlannedConfiguration());
            modelBuilder.Configurations.Add(new PenaltyConfiguration());
            modelBuilder.Configurations.Add(new PriceCalculationConfiguration());
            modelBuilder.Configurations.Add(new PriceCalculationFileConfiguration());
            modelBuilder.Configurations.Add(new PriceCalculationHistoryItemConfiguration());
            modelBuilder.Configurations.Add(new PriceCalculationItemConfiguration());
            modelBuilder.Configurations.Add(new DesignDepartmentParametersConfiguration());
            modelBuilder.Configurations.Add(new DesignDepartmentParametersAddedBlocksConfiguration());
            modelBuilder.Configurations.Add(new DesignDepartmentParametersSubTaskConfiguration());
            modelBuilder.Configurations.Add(new NotificationsReportsSettingsConfiguration());
            modelBuilder.Configurations.Add(new PriceEngineeringTaskConfiguration());
            modelBuilder.Configurations.Add(new PriceEngineeringTaskFileAnswerConfiguration());
            modelBuilder.Configurations.Add(new PriceEngineeringTaskFileTechnicalRequirementsConfiguration());
            modelBuilder.Configurations.Add(new PriceEngineeringTaskMessageConfiguration());
            modelBuilder.Configurations.Add(new PriceEngineeringTaskProductBlockAddedConfiguration());
            modelBuilder.Configurations.Add(new PriceEngineeringTasksConfiguration());
            modelBuilder.Configurations.Add(new PriceEngineeringTasksFileTechnicalRequirementsConfiguration());
            modelBuilder.Configurations.Add(new PriceEngineeringTaskStatusConfiguration());
            modelBuilder.Configurations.Add(new StructureCostVersionConfiguration());
            modelBuilder.Configurations.Add(new UpdateStructureCostNumberTaskConfiguration());
            modelBuilder.Configurations.Add(new ProductCategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductCategoryPriceAndCostConfiguration());
            modelBuilder.Configurations.Add(new ProductIncludedConfiguration());
            modelBuilder.Configurations.Add(new ProductDesignationConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeDesignationConfiguration());
            modelBuilder.Configurations.Add(new ProjectTypeConfiguration());
            modelBuilder.Configurations.Add(new StandartMarginalIncomeConfiguration());
            modelBuilder.Configurations.Add(new StandartProductionTermConfiguration());
            modelBuilder.Configurations.Add(new StructureCostConfiguration());
            modelBuilder.Configurations.Add(new SupervisionConfiguration());
            modelBuilder.Configurations.Add(new TaskInvoiceForPaymentConfiguration());
            modelBuilder.Configurations.Add(new TaskInvoiceForPaymentItemConfiguration());
            modelBuilder.Configurations.Add(new AnswerFileTceConfiguration());
            modelBuilder.Configurations.Add(new ShippingCostFileConfiguration());
            modelBuilder.Configurations.Add(new TechnicalRequrementsConfiguration());
            modelBuilder.Configurations.Add(new TechnicalRequrementsFileConfiguration());
            modelBuilder.Configurations.Add(new TechnicalRequrementsTaskConfiguration());
            modelBuilder.Configurations.Add(new TechnicalRequrementsTaskHistoryElementConfiguration());
            modelBuilder.Configurations.Add(new UserGroupConfiguration());
            modelBuilder.Configurations.Add(new GlobalPropertiesConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new DistrictConfiguration());
            modelBuilder.Configurations.Add(new LocalityConfiguration());
            modelBuilder.Configurations.Add(new LocalityTypeConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new SumConfiguration());
            modelBuilder.Configurations.Add(new CurrencyExchangeRateConfiguration());
            modelBuilder.Configurations.Add(new NoteConfiguration());
            modelBuilder.Configurations.Add(new OfferUnitConfiguration());
            modelBuilder.Configurations.Add(new PaymentConditionSetConfiguration());
            modelBuilder.Configurations.Add(new ProductBlockConfiguration());
            modelBuilder.Configurations.Add(new ProductDependentConfiguration());
            modelBuilder.Configurations.Add(new BankDetailsConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new CompanyFormConfiguration());
            modelBuilder.Configurations.Add(new DocumentsRegistrationDetailsConfiguration());
            modelBuilder.Configurations.Add(new EmployeesPositionConfiguration());
            modelBuilder.Configurations.Add(new FacilityTypeConfiguration());
            modelBuilder.Configurations.Add(new ActivityFieldConfiguration());
            modelBuilder.Configurations.Add(new ContractConfiguration());
            modelBuilder.Configurations.Add(new MeasureConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());
            modelBuilder.Configurations.Add(new ParameterGroupConfiguration());
            modelBuilder.Configurations.Add(new ProductRelationConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new ParameterRelationConfiguration());
            modelBuilder.Configurations.Add(new SalesUnitConfiguration());
            modelBuilder.Configurations.Add(new DocumentConfiguration());
            modelBuilder.Configurations.Add(new SumOnDateConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new OfferConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new PaymentConditionConfiguration());
            modelBuilder.Configurations.Add(new PaymentDocumentConfiguration());
            modelBuilder.Configurations.Add(new FacilityConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new SpecificationConfiguration());
            modelBuilder.Configurations.Add(new TenderConfiguration());
            modelBuilder.Configurations.Add(new TenderTypeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
			#endregion
        }

		#region DbSets
        public virtual DbSet<NotificationUnit> NotificationUnitDbSet { get; set; }
        public virtual DbSet<CountryUnion> CountryUnionDbSet { get; set; }
        public virtual DbSet<Budget> BudgetDbSet { get; set; }
        public virtual DbSet<BudgetUnit> BudgetUnitDbSet { get; set; }
        public virtual DbSet<ConstructorParametersList> ConstructorParametersListDbSet { get; set; }
        public virtual DbSet<ConstructorsParameters> ConstructorsParametersDbSet { get; set; }
        public virtual DbSet<CostsPercents> CostsPercentsDbSet { get; set; }
        public virtual DbSet<CreateNewProductTask> CreateNewProductTaskDbSet { get; set; }
        public virtual DbSet<DesignDepartment> DesignDepartmentDbSet { get; set; }
        public virtual DbSet<DirectumTask> DirectumTaskDbSet { get; set; }
        public virtual DbSet<DirectumTaskGroup> DirectumTaskGroupDbSet { get; set; }
        public virtual DbSet<DirectumTaskGroupFile> DirectumTaskGroupFileDbSet { get; set; }
        public virtual DbSet<DirectumTaskMessage> DirectumTaskMessageDbSet { get; set; }
        public virtual DbSet<DocumentNumber> DocumentNumberDbSet { get; set; }
        public virtual DbSet<IncomingRequest> IncomingRequestDbSet { get; set; }
        public virtual DbSet<LaborHourCost> LaborHourCostDbSet { get; set; }
        public virtual DbSet<LaborHours> LaborHoursDbSet { get; set; }
        public virtual DbSet<LogUnit> LogUnitDbSet { get; set; }
        public virtual DbSet<LosingReason> LosingReasonDbSet { get; set; }
        public virtual DbSet<MarketField> MarketFieldDbSet { get; set; }
        public virtual DbSet<PaymentActual> PaymentActualDbSet { get; set; }
        public virtual DbSet<PaymentConditionPoint> PaymentConditionPointDbSet { get; set; }
        public virtual DbSet<PaymentPlanned> PaymentPlannedDbSet { get; set; }
        public virtual DbSet<Penalty> PenaltyDbSet { get; set; }
        public virtual DbSet<PriceCalculation> PriceCalculationDbSet { get; set; }
        public virtual DbSet<PriceCalculationFile> PriceCalculationFileDbSet { get; set; }
        public virtual DbSet<PriceCalculationHistoryItem> PriceCalculationHistoryItemDbSet { get; set; }
        public virtual DbSet<PriceCalculationItem> PriceCalculationItemDbSet { get; set; }
        public virtual DbSet<DesignDepartmentParameters> DesignDepartmentParametersDbSet { get; set; }
        public virtual DbSet<DesignDepartmentParametersAddedBlocks> DesignDepartmentParametersAddedBlocksDbSet { get; set; }
        public virtual DbSet<DesignDepartmentParametersSubTask> DesignDepartmentParametersSubTaskDbSet { get; set; }
        public virtual DbSet<NotificationsReportsSettings> NotificationsReportsSettingsDbSet { get; set; }
        public virtual DbSet<PriceEngineeringTask> PriceEngineeringTaskDbSet { get; set; }
        public virtual DbSet<PriceEngineeringTaskFileAnswer> PriceEngineeringTaskFileAnswerDbSet { get; set; }
        public virtual DbSet<PriceEngineeringTaskFileTechnicalRequirements> PriceEngineeringTaskFileTechnicalRequirementsDbSet { get; set; }
        public virtual DbSet<PriceEngineeringTaskMessage> PriceEngineeringTaskMessageDbSet { get; set; }
        public virtual DbSet<PriceEngineeringTaskProductBlockAdded> PriceEngineeringTaskProductBlockAddedDbSet { get; set; }
        public virtual DbSet<PriceEngineeringTasks> PriceEngineeringTasksDbSet { get; set; }
        public virtual DbSet<PriceEngineeringTasksFileTechnicalRequirements> PriceEngineeringTasksFileTechnicalRequirementsDbSet { get; set; }
        public virtual DbSet<PriceEngineeringTaskStatus> PriceEngineeringTaskStatusDbSet { get; set; }
        public virtual DbSet<StructureCostVersion> StructureCostVersionDbSet { get; set; }
        public virtual DbSet<UpdateStructureCostNumberTask> UpdateStructureCostNumberTaskDbSet { get; set; }
        public virtual DbSet<ProductCategory> ProductCategoryDbSet { get; set; }
        public virtual DbSet<ProductCategoryPriceAndCost> ProductCategoryPriceAndCostDbSet { get; set; }
        public virtual DbSet<ProductIncluded> ProductIncludedDbSet { get; set; }
        public virtual DbSet<ProductDesignation> ProductDesignationDbSet { get; set; }
        public virtual DbSet<ProductType> ProductTypeDbSet { get; set; }
        public virtual DbSet<ProductTypeDesignation> ProductTypeDesignationDbSet { get; set; }
        public virtual DbSet<ProjectType> ProjectTypeDbSet { get; set; }
        public virtual DbSet<StandartMarginalIncome> StandartMarginalIncomeDbSet { get; set; }
        public virtual DbSet<StandartProductionTerm> StandartProductionTermDbSet { get; set; }
        public virtual DbSet<StructureCost> StructureCostDbSet { get; set; }
        public virtual DbSet<Supervision> SupervisionDbSet { get; set; }
        public virtual DbSet<TaskInvoiceForPayment> TaskInvoiceForPaymentDbSet { get; set; }
        public virtual DbSet<TaskInvoiceForPaymentItem> TaskInvoiceForPaymentItemDbSet { get; set; }
        public virtual DbSet<AnswerFileTce> AnswerFileTceDbSet { get; set; }
        public virtual DbSet<ShippingCostFile> ShippingCostFileDbSet { get; set; }
        public virtual DbSet<TechnicalRequrements> TechnicalRequrementsDbSet { get; set; }
        public virtual DbSet<TechnicalRequrementsFile> TechnicalRequrementsFileDbSet { get; set; }
        public virtual DbSet<TechnicalRequrementsTask> TechnicalRequrementsTaskDbSet { get; set; }
        public virtual DbSet<TechnicalRequrementsTaskHistoryElement> TechnicalRequrementsTaskHistoryElementDbSet { get; set; }
        public virtual DbSet<UserGroup> UserGroupDbSet { get; set; }
        public virtual DbSet<GlobalProperties> GlobalPropertiesDbSet { get; set; }
        public virtual DbSet<Address> AddressDbSet { get; set; }
        public virtual DbSet<Country> CountryDbSet { get; set; }
        public virtual DbSet<District> DistrictDbSet { get; set; }
        public virtual DbSet<Locality> LocalityDbSet { get; set; }
        public virtual DbSet<LocalityType> LocalityTypeDbSet { get; set; }
        public virtual DbSet<Region> RegionDbSet { get; set; }
        public virtual DbSet<Sum> SumDbSet { get; set; }
        public virtual DbSet<CurrencyExchangeRate> CurrencyExchangeRateDbSet { get; set; }
        public virtual DbSet<Note> NoteDbSet { get; set; }
        public virtual DbSet<OfferUnit> OfferUnitDbSet { get; set; }
        public virtual DbSet<PaymentConditionSet> PaymentConditionSetDbSet { get; set; }
        public virtual DbSet<ProductBlock> ProductBlockDbSet { get; set; }
        public virtual DbSet<ProductDependent> ProductDependentDbSet { get; set; }
        public virtual DbSet<BankDetails> BankDetailsDbSet { get; set; }
        public virtual DbSet<Company> CompanyDbSet { get; set; }
        public virtual DbSet<CompanyForm> CompanyFormDbSet { get; set; }
        public virtual DbSet<DocumentsRegistrationDetails> DocumentsRegistrationDetailsDbSet { get; set; }
        public virtual DbSet<EmployeesPosition> EmployeesPositionDbSet { get; set; }
        public virtual DbSet<FacilityType> FacilityTypeDbSet { get; set; }
        public virtual DbSet<ActivityField> ActivityFieldDbSet { get; set; }
        public virtual DbSet<Contract> ContractDbSet { get; set; }
        public virtual DbSet<Measure> MeasureDbSet { get; set; }
        public virtual DbSet<Parameter> ParameterDbSet { get; set; }
        public virtual DbSet<ParameterGroup> ParameterGroupDbSet { get; set; }
        public virtual DbSet<ProductRelation> ProductRelationDbSet { get; set; }
        public virtual DbSet<Person> PersonDbSet { get; set; }
        public virtual DbSet<ParameterRelation> ParameterRelationDbSet { get; set; }
        public virtual DbSet<SalesUnit> SalesUnitDbSet { get; set; }
        public virtual DbSet<Document> DocumentDbSet { get; set; }
        public virtual DbSet<SumOnDate> SumOnDateDbSet { get; set; }
        public virtual DbSet<Product> ProductDbSet { get; set; }
        public virtual DbSet<Offer> OfferDbSet { get; set; }
        public virtual DbSet<Employee> EmployeeDbSet { get; set; }
        public virtual DbSet<Order> OrderDbSet { get; set; }
        public virtual DbSet<PaymentCondition> PaymentConditionDbSet { get; set; }
        public virtual DbSet<PaymentDocument> PaymentDocumentDbSet { get; set; }
        public virtual DbSet<Facility> FacilityDbSet { get; set; }
        public virtual DbSet<Project> ProjectDbSet { get; set; }
        public virtual DbSet<UserRole> UserRoleDbSet { get; set; }
        public virtual DbSet<Specification> SpecificationDbSet { get; set; }
        public virtual DbSet<Tender> TenderDbSet { get; set; }
        public virtual DbSet<TenderType> TenderTypeDbSet { get; set; }
        public virtual DbSet<User> UserDbSet { get; set; }
		#endregion
    }
}
