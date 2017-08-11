using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    using System.Data.Entity;

    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(20);
            Property(x => x.Role).IsRequired();
        }
    }

    public class HVTAppContext : DbContext
    {
        public HVTAppContext() : base("name=HVTAppContext")
        {
            Database.SetInitializer(new HVTAppDataBaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new DistrictConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new LocalityTypeConfiguration());
            modelBuilder.Configurations.Add(new LocalityConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());

            modelBuilder.Configurations.Add(new ContractConfiguration());
            modelBuilder.Configurations.Add(new SpecificationConfiguration());

            modelBuilder.Configurations.Add(new ActivityFieldConfiguration());
            modelBuilder.Configurations.Add(new CompanyFormConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new EmployeesPositionConfiguration());

            modelBuilder.Configurations.Add(new TenderUnitConfiguration());
            modelBuilder.Configurations.Add(new OfferUnitConfiguration());
            modelBuilder.Configurations.Add(new SalesUnitConfiguration());
            modelBuilder.Configurations.Add(new ProductionUnitConfiguration());
            modelBuilder.Configurations.Add(new ShipmentUnitConfiguration());

            modelBuilder.Configurations.Add(new TenderConfiguration());

            modelBuilder.Configurations.Add(new OfferConfiguration());

            modelBuilder.Configurations.Add(new FacilityConfiguration());
            modelBuilder.Configurations.Add(new FacilityTypeConfiguration());

            modelBuilder.Configurations.Add(new ProjectConfiguration());

            modelBuilder.Configurations.Add(new DocumentConfiguration());
            modelBuilder.Configurations.Add(new DocumentsRegistrationDetailsConfiguration());

            modelBuilder.Configurations.Add(new ParameterGroupConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());
            modelBuilder.Configurations.Add(new RequiredPreviousParametersConfiguration());
            modelBuilder.Configurations.Add(new RequiredDependentEquipmentsParametersConfiguration());

            modelBuilder.Configurations.Add(new MeasureConfiguration());

            modelBuilder.Configurations.Add(new ProductItemConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());

            modelBuilder.Configurations.Add(new PaymentConditionConfiguration());
            modelBuilder.Configurations.Add(new PaymentConditionStandartConfiguration());
            modelBuilder.Configurations.Add(new PaymentDocumentConfiguration());
            modelBuilder.Configurations.Add(new PaymentActualConfiguration());
            modelBuilder.Configurations.Add(new PaymentPlannedConfiguration());

            modelBuilder.Configurations.Add(new BankDetailsConfiguration());

            modelBuilder.Configurations.Add(new OrderConfiguration());


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<TestFriendGroup> FriendGroups { get; set; }

        public virtual DbSet<FacilityType> FacilityTypes { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Tender> Tenders { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<EmployeesPosition> EmployeesPositions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ActivityField> ActivityFilds { get; set; }
        public virtual DbSet<CompanyForm> CompanyForms { get; set; }
        public virtual DbSet<PaymentCondition> PaymentConditions { get; set; }
        public virtual DbSet<StandartPaymentConditions> PaymentConditionStandarts { get; set; }
        public virtual DbSet<PaymentDocument> PaymentDocuments { get; set; }
        public virtual DbSet<PaymentActual> PaymentsActual { get; set; }
        public virtual DbSet<PaymentPlanned> PaymentsPlanned { get; set; }
        public virtual DbSet<ParameterGroup> ParameterGroups { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<RequiredDependentEquipmentsParameters> RequiredDependentEquipmentsParameterses { get; set; }
        public virtual DbSet<Product> ProductItems { get; set; }
        public virtual DbSet<Equipment> Products { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<ProjectUnit> ProjectUnits { get; set; }
        public virtual DbSet<TenderUnit> TenderUnits { get; set; }
        public virtual DbSet<OfferUnit> OfferUnits { get; set; }
        public virtual DbSet<SalesUnit> SalesUnits { get; set; }
        public virtual DbSet<ProductionUnit> ProductionUnits { get; set; }

    }
}