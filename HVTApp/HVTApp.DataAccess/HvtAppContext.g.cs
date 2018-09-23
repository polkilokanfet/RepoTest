using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    using System.Data.Entity;

    public partial class HvtAppContext
    {
        protected void AddConfigurations(DbModelBuilder modelBuilder)
        {
			#region Configurations
            modelBuilder.Configurations.Add(new CreateNewProductTaskConfiguration());
            modelBuilder.Configurations.Add(new PaymentActualConfiguration());
            modelBuilder.Configurations.Add(new PaymentPlannedConfiguration());
            modelBuilder.Configurations.Add(new ProductBlockIsServiceConfiguration());
            modelBuilder.Configurations.Add(new ProductIncludedConfiguration());
            modelBuilder.Configurations.Add(new ProductDesignationConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeConfiguration());
            modelBuilder.Configurations.Add(new ProductTypeDesignationConfiguration());
            modelBuilder.Configurations.Add(new ProjectTypeConfiguration());
            modelBuilder.Configurations.Add(new CommonOptionConfiguration());
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
            modelBuilder.Configurations.Add(new DocumentNumberConfiguration());
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
        public virtual DbSet<CreateNewProductTask> CreateNewProductTaskDbSet { get; set; }
        public virtual DbSet<PaymentActual> PaymentActualDbSet { get; set; }
        public virtual DbSet<PaymentPlanned> PaymentPlannedDbSet { get; set; }
        public virtual DbSet<ProductBlockIsService> ProductBlockIsServiceDbSet { get; set; }
        public virtual DbSet<ProductIncluded> ProductIncludedDbSet { get; set; }
        public virtual DbSet<ProductDesignation> ProductDesignationDbSet { get; set; }
        public virtual DbSet<ProductType> ProductTypeDbSet { get; set; }
        public virtual DbSet<ProductTypeDesignation> ProductTypeDesignationDbSet { get; set; }
        public virtual DbSet<ProjectType> ProjectTypeDbSet { get; set; }
        public virtual DbSet<CommonOption> CommonOptionDbSet { get; set; }
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
        public virtual DbSet<DocumentNumber> DocumentNumberDbSet { get; set; }
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
