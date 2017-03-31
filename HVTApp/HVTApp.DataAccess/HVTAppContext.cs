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
            modelBuilder.Entity<District>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<District>().HasRequired(x => x.Country);
            modelBuilder.Entity<DistrictsRegion>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<DistrictsRegion>().HasRequired(x => x.District);
            modelBuilder.Entity<LocalityType>().Property(x => x.FullName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Locality>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Locality>().HasRequired(x => x.DistrictsRegion);
            modelBuilder.Entity<Locality>().HasRequired(x => x.LocalityType);
            modelBuilder.Entity<Address>().HasRequired(x => x.Locality);
            #endregion

            #region Company
            modelBuilder.Entity<ActivityField>().Property(x => x.FieldOfActivity).IsRequired();
            modelBuilder.Entity<CompanyForm>().Property(x => x.FullName).IsRequired().HasMaxLength(50).IsUnicode();
            modelBuilder.Entity<CompanyForm>().Property(x => x.ShortName).IsRequired().HasMaxLength(50).IsUnicode();
            modelBuilder.Entity<Company>().Property(x => x.FullName).IsRequired().HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<Company>().Property(x => x.ShortName).IsRequired().HasMaxLength(100).IsUnicode();
            modelBuilder.Entity<Company>().HasRequired(x => x.Form);
            modelBuilder.Entity<Company>().Ignore(x => x.ChildCompanies);
            #endregion

            modelBuilder.Entity<ProductMain>().HasRequired(x => x.TenderInfo).WithRequiredPrincipal(x => x.ProductMain);
            modelBuilder.Entity<ProductBase>().HasRequired(x => x.OrderInfo).WithRequiredPrincipal(x => x.Product);
            modelBuilder.Entity<ProductBase>().HasRequired(x => x.DateInfo).WithRequiredPrincipal(x => x.Product);
            modelBuilder.Entity<ProductBase>().HasRequired(x => x.PaymentsInfo).WithRequiredPrincipal(x => x.Product);
            modelBuilder.Entity<ProductBase>().HasRequired(x => x.CostInfo);
            modelBuilder.Entity<ProductBase>().HasRequired(x => x.TermsInfo);

            modelBuilder.Entity<PaymentsInfo>().HasRequired(x => x.Product);

            modelBuilder.Entity<PaymentPlanned>().HasRequired(x => x.PaymentsInfo);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<FriendGroupTest> FriendGroups { get; set; }


        public virtual DbSet<EmployeesPosition> EmployeesPositions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ActivityField> ActivityFilds { get; set; }
        public virtual DbSet<CompanyForm> CompanyForms { get; set; }
        public virtual DbSet<ProductMain> ProductsMain { get; set; }
        public virtual DbSet<ProductOptional> ProductsOptional { get; set; }
        public virtual DbSet<CostInfo> CostInfos { get; set; }
        public virtual DbSet<TenderInfo> TenderInfos { get; set; }
        public virtual DbSet<PaymentsInfo> PaymentsInfos { get; set; }
        public virtual DbSet<PaymentActual> PaymentsActual { get; set; }
        public virtual DbSet<PaymentPlanned> PaymentsPlanned { get; set; }
        public virtual DbSet<PaymentsCondition> PaymentConditions { get; set; }
        public virtual DbSet<PaymentDocument> PaymentDocuments { get; set; }
        public virtual DbSet<StandartPaymentConditions> StandartPaymentConditionses { get; set; }
        public virtual DbSet<TechParameter> TechParameters { get; set; }
    }
}