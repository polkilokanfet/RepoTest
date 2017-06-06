using System.Security.Cryptography.X509Certificates;
using HVTApp.Model;
using HVTApp.Model.POCOs;

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
            modelBuilder.Entity<User>().HasRequired(x => x.Employee).WithOptional().WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().Property(x => x.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().HasOptional(x => x.CurrentEmployee);
            modelBuilder.Entity<Person>().HasMany(x => x.Employees).WithRequired(x => x.Person);

            #endregion

            #region Units

            //modelBuilder.Entity<Unit>().HasKey(x => x.Id).ToTable(nameof(Unit));
            //modelBuilder.Entity<ProjectsUnit>().HasKey(x => x.Id).ToTable(nameof(Unit));
            //modelBuilder.Entity<ProductionsUnit>().HasKey(x => x.Id).ToTable(nameof(Unit));
            //modelBuilder.Entity<SalesUnit>().HasKey(x => x.Id).ToTable(nameof(Unit));
            //modelBuilder.Entity<ShipmentsUnit>().HasKey(x => x.Id).ToTable(nameof(Unit));

            #region Unit

            modelBuilder.Entity<Unit>().HasRequired(x => x.Facility).WithMany();
            modelBuilder.Entity<Unit>().HasRequired(x => x.Project).WithMany(x => x.Units);

            //modelBuilder.Entity<Unit>().HasRequired(x => x.SalesUnit).WithRequiredPrincipal(x => x.Unit);
            //modelBuilder.Entity<Unit>().HasRequired(x => x.ProductionsUnit).WithRequiredPrincipal(x => x.Unit);
            //modelBuilder.Entity<Unit>().HasRequired(x => x.ShipmentsUnit).WithRequiredPrincipal(x => x.Unit);

            //modelBuilder.Entity<Unit>().HasMany(x => x.TendersUnits).WithRequired(x => x.Unit).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Unit>().HasMany(x => x.OffersUnits).WithRequired(x => x.Unit).WillCascadeOnDelete(false);

            #endregion

            #region ProjectsUnit

            modelBuilder.Entity<ProjectsUnit>().HasRequired(x => x.Unit).WithRequiredDependent(x => x.ProjectsUnit);
            modelBuilder.Entity<ProjectsUnit>().HasRequired(x => x.Product).WithMany();
            modelBuilder.Entity<ProjectsUnit>().HasRequired(x => x.Cost);

            #endregion

            #region SalesUnit

            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.Unit).WithOptional(x => x.SalesUnit);
            modelBuilder.Entity<SalesUnit>().HasRequired(x => x.Cost);
            modelBuilder.Entity<SalesUnit>().HasOptional(x => x.Specification).WithMany(x => x.SalesUnits);
            modelBuilder.Entity<SalesUnit>().HasMany(x => x.PaymentsActual).WithRequired(x => x.SalesUnit);
            modelBuilder.Entity<SalesUnit>().HasMany(x => x.PaymentsPlanned).WithRequired(x => x.SalesUnit);

            #endregion

            #region ProductionsUnit

            modelBuilder.Entity<ProductionsUnit>().HasRequired(x => x.Unit).WithOptional(x => x.ProductionsUnit);
            modelBuilder.Entity<ProductionsUnit>().HasRequired(x => x.Product).WithMany();
            modelBuilder.Entity<ProductionsUnit>().HasOptional(x => x.Order).WithMany(x => x.ProductionsUnits);

            #endregion

            #region ShipmentsUnit

            modelBuilder.Entity<ShipmentsUnit>().HasRequired(x => x.Unit).WithOptional(x => x.ShipmentsUnit);
            modelBuilder.Entity<ShipmentsUnit>().HasRequired(x => x.Cost);
            modelBuilder.Entity<ShipmentsUnit>().HasRequired(x => x.Address).WithMany();

            #endregion

            #region TendersUnit

            modelBuilder.Entity<TendersUnit>().HasRequired(x => x.Unit).WithMany(x => x.TendersUnits);
            modelBuilder.Entity<TendersUnit>().HasRequired(x => x.Tender).WithMany(x => x.TendersUnits);
            modelBuilder.Entity<TendersUnit>().HasRequired(x => x.Product).WithMany();
            modelBuilder.Entity<TendersUnit>().HasRequired(x => x.Cost);
            modelBuilder.Entity<TendersUnit>().HasOptional(x => x.ProducerWinner).WithMany();
            
            #endregion

            #region OffersUnit

            modelBuilder.Entity<OffersUnit>().HasRequired(x => x.Unit).WithMany(x => x.OffersUnits);
            modelBuilder.Entity<OffersUnit>().HasRequired(x => x.Offer).WithMany(x => x.OfferUnits);
            modelBuilder.Entity<OffersUnit>().HasRequired(x => x.Product).WithMany();
            modelBuilder.Entity<OffersUnit>().HasRequired(x => x.Cost);            

            #endregion

            #endregion

            #region Tender

            modelBuilder.Entity<Tender>().HasRequired(x => x.Type).WithMany();
            modelBuilder.Entity<Tender>().HasRequired(x => x.Project).WithMany(x => x.Tenders);
            modelBuilder.Entity<Tender>().HasRequired(x => x.Sum).WithOptional();
            modelBuilder.Entity<Tender>().HasMany(x => x.Offers).WithRequired(x => x.Tender);
            modelBuilder.Entity<Tender>().HasMany(x => x.TendersUnits).WithRequired(x => x.Tender);
            modelBuilder.Entity<Tender>().HasMany(x => x.Participants).WithMany();
            modelBuilder.Entity<Tender>().HasOptional(x => x.Winner).WithMany();

            #endregion

            #region Offer

            modelBuilder.Entity<Offer>().Property(x => x.ValidityDate).IsRequired();
            modelBuilder.Entity<Offer>().HasRequired(x => x.Project).WithMany(x => x.Offers);
            modelBuilder.Entity<Offer>().HasRequired(x => x.Tender).WithMany(x => x.Offers);
            modelBuilder.Entity<Offer>().HasMany(x => x.OfferUnits).WithRequired(x => x.Offer);

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
            modelBuilder.Entity<Project>().HasMany(x => x.Units).WithRequired(x => x.Project);
            modelBuilder.Entity<Project>().HasMany(x => x.Offers).WithRequired(x => x.Project).WillCascadeOnDelete(false);
            modelBuilder.Entity<Project>().HasMany(x => x.Tenders).WithRequired(x => x.Project).WillCascadeOnDelete(false);

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
            modelBuilder.Entity<Document>().HasRequired(x => x.SenderEmployee).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Document>().HasRequired(x => x.RecipientEmployee).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<RegistrationDetails>().Property(x => x.RegistrationNumber).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<RegistrationDetails>().Property(x => x.RegistrationDate).IsRequired();

            #endregion

            #region Parameter

            modelBuilder.Entity<Parameter>().Property(x => x.Value).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Parameter>().HasRequired(x => x.Group).WithMany(x => x.Parameters);
            modelBuilder.Entity<Parameter>().HasMany(x => x.RequiredParents).WithMany();

            modelBuilder.Entity<RequiredParameters>().HasMany(x => x.Parameters).WithMany();

            modelBuilder.Entity<ParameterGroup>().Property(x => x.Name).IsRequired().HasMaxLength(25);
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

            #region PaymentCommon

            modelBuilder.Entity<PaymentPlan>().Property(x => x.Date).IsRequired();
            modelBuilder.Entity<PaymentPlan>().HasRequired(x => x.SumAndVat).WithOptional().WillCascadeOnDelete(false);
            modelBuilder.Entity<PaymentPlan>().Property(x => x.Comment).IsOptional().HasMaxLength(100);

            modelBuilder.Entity<PaymentActual>().Property(x => x.Date).IsRequired();
            modelBuilder.Entity<PaymentActual>().HasRequired(x => x.SumAndVat).WithOptional().WillCascadeOnDelete(false);
            modelBuilder.Entity<PaymentActual>().Property(x => x.Comment).IsOptional().HasMaxLength(100);

            modelBuilder.Entity<PaymentCondition>().Property(x => x.PartInPercent).IsRequired();
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
        public virtual DbSet<RequiredProductsChilds> RequiredProductsChildses { get; set; }
        public virtual DbSet<ProductItem> ProductItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
    }
}