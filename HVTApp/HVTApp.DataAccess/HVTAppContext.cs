using HVTApp.Model;

namespace HVTApp.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;

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

            modelBuilder.Entity<Country>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Country>().HasMany(x => x.Districts).WithRequired(x => x.Country);

            modelBuilder.Entity<District>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<District>().HasMany(x => x.Regions).WithRequired(x => x.District);

            modelBuilder.Entity<Region>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Region>().HasMany(x => x.Localities).WithRequired(x => x.Region);

            modelBuilder.Entity<LocalityType>().Property(x => x.FullName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<LocalityType>().Property(x => x.FullName).HasMaxLength(50);

            modelBuilder.Entity<Locality>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Locality>().HasRequired(x => x.LocalityType);
            modelBuilder.Entity<Locality>().HasOptional(x => x.DeliveryPeriod).WithOptionalPrincipal(x => x.Locality);

            modelBuilder.Entity<Address>().Property(x => x.Description).HasMaxLength(150);
            modelBuilder.Entity<Address>().HasRequired(x => x.Locality);

            #endregion


            #region Company
            modelBuilder.Entity<ActivityField>().Property(x => x.FieldOfActivity).IsRequired();
            modelBuilder.Entity<ActivityField>().Property(x => x.Name).IsRequired().HasMaxLength(25);

            modelBuilder.Entity<CompanyForm>().Property(x => x.FullName).IsRequired().HasMaxLength(50).IsUnicode();
            modelBuilder.Entity<CompanyForm>().Property(x => x.ShortName).IsRequired().HasMaxLength(50).IsUnicode();


            modelBuilder.Entity<Company>().Property(x => x.FullName).IsRequired().HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<Company>().Property(x => x.ShortName).IsRequired().HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<Company>().HasRequired(x => x.Form);
            modelBuilder.Entity<Company>().HasMany(x => x.ActivityFilds).WithMany();
            modelBuilder.Entity<Company>().HasMany(x => x.Employees).WithRequired(x => x.Company);
            modelBuilder.Entity<Company>().HasMany(x => x.ChildCompanies).WithOptional(x => x.ParentCompany);
            //modelBuilder.Entity<Company>().Ignore(x => x.ChildCompanies);

            #endregion

            #region Person, User, Employee

            modelBuilder.Entity<UserRole>().Property(x => x.Role).IsRequired();

            modelBuilder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.PersonalNumber).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<User>().Ignore(x => x.RoleCurrent);
            modelBuilder.Entity<User>().HasRequired(x => x.Employee);

            modelBuilder.Entity<Person>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().Property(x => x.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().HasOptional(x => x.CurrentEmployee);
            modelBuilder.Entity<Person>().HasMany(x => x.Employees).WithRequired(x => x.Person);

            #endregion

            #region SalesUnit

            modelBuilder.Entity<SalesUnit>().HasKey(x => x.Id).ToTable(nameof(SalesUnit));
            modelBuilder.Entity<ProductionUnit>().HasKey(x => x.Id).ToTable(nameof(SalesUnit));
            modelBuilder.Entity<ShipmentUnit>().HasKey(x => x.Id).ToTable(nameof(SalesUnit));
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.CostSingle);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.Facility);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.Project).WithMany(x => x.SalesUnits);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.ProductionUnit).WithRequiredPrincipal(x => x.SalesUnit);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.ShipmentUnit).WithRequiredPrincipal(x => x.SalesUnit);
            modelBuilder.Entity<SalesUnit>().HasOptional(x => x.Specification).WithMany(x => x.SalesUnits);

            #endregion

            #region Project

            modelBuilder.Entity<Project>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Project>().HasRequired(x => x.Manager);
            modelBuilder.Entity<Project>().HasMany(x => x.SalesUnits).WithRequired(x => x.Project);
            modelBuilder.Entity<Project>().HasMany(x => x.Offers).WithRequired(x => x.Project);
            modelBuilder.Entity<Project>().HasMany(x => x.Tenders).WithRequired(x => x.Project);

            #endregion

            #region Contract

            modelBuilder.Entity<Contract>().Property(x => x.Number).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Contract>().HasRequired(x => x.Contragent);
            modelBuilder.Entity<Contract>().HasMany(x => x.Specifications).WithRequired(x => x.Contract);

            modelBuilder.Entity<Specification>().Property(x => x.Number).HasMaxLength(4);
            modelBuilder.Entity<Specification>().HasMany(x => x.SalesUnits).WithOptional(x => x.Specification);

            #endregion

            #region Document

            modelBuilder.Entity<Document>().HasOptional(x => x.Author);
            modelBuilder.Entity<Document>().HasRequired(x => x.SenderEmployee);
            modelBuilder.Entity<Document>().HasRequired(x => x.RecipientEmployee);

            modelBuilder.Entity<RegistrationDetails>().Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<RegistrationDetails>().Property(x => x.RegistrationDate).IsRequired();

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<TestFriendGroup> FriendGroups { get; set; }

        public virtual DbSet<Project> Projects { get; set; }
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
    }
}