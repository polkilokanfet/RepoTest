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
            modelBuilder.Entity<CompanyForm>().Property(x => x.FullName).IsUnicode();

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