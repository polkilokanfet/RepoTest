using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    using System.Data.Entity;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(x => x.Login).IsRequired().HasMaxLength(20);
            Property(x => x.Password).IsRequired();
            Property(x => x.PersonalNumber).IsRequired().HasMaxLength(10);
            Ignore(x => x.RoleCurrent);
            HasRequired(x => x.Employee).WithOptional().WillCascadeOnDelete(false);
        }
   }

    public class HVTAppContext : DbContext
    {
        // Your context has been configured to use a 'HVTAppContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'HVTApp.DataAccess.HVTAppContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'HVTAppContext' 
        // connection string in the application configuration file.
        public HVTAppContext() : base("name=HVTAppContext")
        {
            Database.SetInitializer(new HVTAppDataBaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Address
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new DistrictConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new LocalityTypeConfiguration());
            modelBuilder.Configurations.Add(new LocalityConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            #endregion

            modelBuilder.Configurations.Add(new ContractConfiguration());
            modelBuilder.Configurations.Add(new SpecificationConfiguration());

            #region Company
            modelBuilder.Configurations.Add(new ActivityFieldConfiguration());
            modelBuilder.Configurations.Add(new CompanyFormConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            #endregion

            modelBuilder.Configurations.Add(new UserConfiguration());


            #region Person, User, Employee

            modelBuilder.Entity<UserRole>().Property(x => x.Role).IsRequired();


            modelBuilder.Entity<Person>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().Property(x => x.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().HasOptional(x => x.CurrentEmployee);
            modelBuilder.Entity<Person>().HasMany(x => x.Employees).WithRequired(x => x.Person);

            #endregion

            #region SalesUnits

            #region SalesUnit


            //modelBuilder.Entity<SalesUnit>().HasRequired(x => x.SalesUnit).WithRequiredPrincipal(x => x.SalesUnit);
            //modelBuilder.Entity<SalesUnit>().HasRequired(x => x.ProductProductionUnit).WithRequiredPrincipal(x => x.SalesUnit);
            //modelBuilder.Entity<SalesUnit>().HasRequired(x => x.ShipmentUnit).WithRequiredPrincipal(x => x.SalesUnit);

            //modelBuilder.Entity<SalesUnit>().HasMany(x => x.TenderUnits).WithRequired(x => x.SalesUnit).WillCascadeOnDelete(false);
            //modelBuilder.Entity<SalesUnit>().HasMany(x => x.ProductOfferUnits).WithRequired(x => x.SalesUnit).WillCascadeOnDelete(false);

            #endregion

            //#region ProjectsUnit

            //modelBuilder.Entity<ProjectsUnit>().HasRequired(x => x.SalesUnit).WithRequiredDependent(x => x.ProjectsUnit);
            //modelBuilder.Entity<ProjectsUnit>().HasRequired(x => x.Product).WithMany();
            //modelBuilder.Entity<ProjectsUnit>().HasRequired(x => x.Cost);
            //modelBuilder.Entity<ProjectsUnit>().HasRequired(x => x.Project).WithMany(x => x.ProjectsUnits);

            //#endregion

            #region TenderUnit

            modelBuilder.Entity<TenderUnit>().HasRequired(x => x.Tender).WithMany(x => x.TenderUnits);
            modelBuilder.Entity<TenderUnit>().HasRequired(x => x.Product).WithMany();
            modelBuilder.Entity<TenderUnit>().HasOptional(x => x.ProducerWinner).WithMany();
            
            #endregion

            #region OfferUnit

            modelBuilder.Entity<OfferUnit>().HasRequired(x => x.Offer).WithMany(x => x.ProductOfferUnits);
            modelBuilder.Entity<OfferUnit>().HasRequired(x => x.Product).WithMany();

            #endregion

            #endregion

            #region Tender

            modelBuilder.Entity<Tender>().HasRequired(x => x.Type).WithMany();
            modelBuilder.Entity<Tender>().HasRequired(x => x.Project).WithMany(x => x.Tenders);
            modelBuilder.Entity<Tender>().HasRequired(x => x.Sum).WithOptional();
            modelBuilder.Entity<Tender>().HasMany(x => x.Offers).WithRequired(x => x.Tender);
            modelBuilder.Entity<Tender>().HasMany(x => x.TenderUnits).WithRequired(x => x.Tender);
            modelBuilder.Entity<Tender>().HasMany(x => x.Participants).WithMany();
            modelBuilder.Entity<Tender>().HasOptional(x => x.Winner).WithMany();

            #endregion

            #region Offer

            modelBuilder.Entity<Offer>().Property(x => x.ValidityDate).IsRequired();
            modelBuilder.Entity<Offer>().HasRequired(x => x.Project).WithMany(x => x.Offers);
            modelBuilder.Entity<Offer>().HasRequired(x => x.Tender).WithMany(x => x.Offers);
            modelBuilder.Entity<Offer>().HasMany(x => x.ProductOfferUnits).WithRequired(x => x.Offer);

            #endregion



            #region Facility

            modelBuilder.Entity<FacilityType>().Property(x => x.FullName).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<FacilityType>().Property(x => x.ShortName).IsOptional().HasMaxLength(25);

            modelBuilder.Entity<Facility>().Property(x => x.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Facility>().HasRequired(x => x.OwnerCompany).WithMany();
            modelBuilder.Entity<Facility>().HasRequired(x => x.Type).WithMany();

            #endregion

            #region Project

            modelBuilder.Entity<Project>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Project>().HasRequired(x => x.Manager);

            #endregion

            #region Contract


            #endregion

            #region Document

            modelBuilder.Entity<Document>().HasOptional(x => x.Author);

            modelBuilder.Entity<RegistrationDetails>().Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<RegistrationDetails>().Property(x => x.RegistrationDate).IsRequired();

            #endregion

            #region Parameter

            modelBuilder.Entity<Parameter>().Property(x => x.Value).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Parameter>().HasRequired(x => x.Group).WithMany(x => x.Parameters);
            modelBuilder.Entity<Parameter>().HasMany(x => x.RequiredParents).WithMany();

            modelBuilder.Entity<RequiredParameters>().HasMany(x => x.Parameters).WithMany();

            modelBuilder.Entity<ParameterGroup>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<ParameterGroup>().HasOptional(x => x.Measure);

            modelBuilder.Entity<PhysicalQuantity>().Property(x => x.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Measure>().Property(x => x.FullName).HasMaxLength(50);
            modelBuilder.Entity<Measure>().Property(x => x.ShortName).IsOptional().HasMaxLength(50);
            modelBuilder.Entity<Measure>().HasRequired(x => x.PhysicalQuantity).WithMany(x => x.Measures);

            #endregion

            #region Product

            modelBuilder.Entity<ProductItem>().Property(x => x.Designation).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<ProductItem>().HasMany(x => x.Parameters).WithMany();

            modelBuilder.Entity<Product>().Property(x => x.Designation).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().HasRequired(x => x.ProductItem).WithMany();
            modelBuilder.Entity<Product>().HasMany(x => x.ChildProducts).WithMany();

            #endregion

            #region PaymentActual

            modelBuilder.Entity<PaymentActual>().Property(x => x.Date).IsRequired();
            modelBuilder.Entity<PaymentActual>().Property(x => x.Comment).IsOptional().HasMaxLength(100);

            modelBuilder.Entity<PaymentCondition>().Property(x => x.Part).IsRequired();
            modelBuilder.Entity<PaymentCondition>().Property(x => x.DaysToPoint).IsRequired();
            modelBuilder.Entity<PaymentCondition>().Property(x => x.PaymentConditionPoint).IsRequired();

            modelBuilder.Entity<PaymentConditionStandart>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<PaymentConditionStandart>().HasMany(x => x.PaymentsConditions).WithMany();

            modelBuilder.Entity<PaymentDocument>().Property(x => x.Number).IsOptional().HasMaxLength(25);
            modelBuilder.Entity<PaymentDocument>().HasMany(x => x.Payments).WithRequired(x => x.Document);

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<TestFriendGroup> FriendGroups { get; set; }

        public virtual DbSet<FacilityType> FacilityTypes { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Tender> Tenders { get; set; }
        public virtual DbSet<EmployeesPosition> EmployeesPositions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ActivityField> ActivityFilds { get; set; }
        public virtual DbSet<CompanyForm> CompanyForms { get; set; }
        public virtual DbSet<PaymentCondition> PaymentConditions { get; set; }
        public virtual DbSet<PaymentDocument> PaymentDocuments { get; set; }
        public virtual DbSet<PaymentConditionStandart> StandartPaymentConditions { get; set; }
        public virtual DbSet<ParameterGroup> ParameterGroups { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<RequiredChildProductParameters> RequiredProductsChildses { get; set; }
        public virtual DbSet<ProductItem> ProductItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
    }
}