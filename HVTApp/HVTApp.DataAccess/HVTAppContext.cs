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

            #region Employee

            modelBuilder.Entity<Person>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().Property(x => x.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().HasOptional(x => x.CurrentEmployee).WithRequired(x => x.Person);

            #endregion

            #region SalesUnit

            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.CostSingle);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.Facility);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.Project).WithMany(x => x.SalesUnits);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.ProductionUnit).WithRequiredPrincipal(x => x.SalesUnit);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.ShipmentUnit).WithRequiredPrincipal(x => x.SalesUnit);
            modelBuilder.Entity<SalesUnit>().HasOptional(x => x.Specification).WithMany(x => x.SalesUnits);
            modelBuilder.Entity<SalesUnit>().HasMany(x => x.PaymentsPlanned).WithRequired(x => x.SalesUnit);
            modelBuilder.Entity<SalesUnit>().HasMany(x => x.PaymentsActual).WithRequired(x => x.SalesUnit);

            #endregion

            //modelBuilder.Entity<ProductMain>().HasRequired(x => x.TenderInfo).WithRequiredPrincipal(x => x.ProductMain);
            //modelBuilder.Entity<ProductBase>().HasRequired(x => x.OrderInfo).WithRequiredPrincipal(x => x.Product);
            //modelBuilder.Entity<ProductBase>().HasRequired(x => x.DateInfo).WithRequiredPrincipal(x => x.Product);
            //modelBuilder.Entity<ProductBase>().HasRequired(x => x.PaymentsInfo).WithRequiredPrincipal(x => x.Product);
            //modelBuilder.Entity<ProductBase>().HasRequired(x => x.ShipmentCost);
            //modelBuilder.Entity<ProductBase>().HasRequired(x => x.TermsInfo);

            //modelBuilder.Entity<PaymentsInfo>().HasRequired(x => x.Product);

            //modelBuilder.Entity<PaymentPlanned>().HasRequired(x => x.PaymentsInfo);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<TestFriendGroup> FriendGroups { get; set; }


        public virtual DbSet<EmployeesPosition> EmployeesPositions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ActivityField> ActivityFilds { get; set; }
        public virtual DbSet<CompanyForm> CompanyForms { get; set; }
        public virtual DbSet<PaymentCondition> PaymentConditions { get; set; }
        public virtual DbSet<PaymentDocument> PaymentDocuments { get; set; }
        public virtual DbSet<PaymentConditionStandart> StandartPaymentConditionses { get; set; }
    }
}