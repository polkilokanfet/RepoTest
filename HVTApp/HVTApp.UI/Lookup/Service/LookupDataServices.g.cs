using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{

    public partial class ProjectTypeLookupDataService : LookupDataService<ProjectTypeLookup, ProjectType>, IProjectTypeLookupDataService
    {
        public ProjectTypeLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class CommonOptionLookupDataService : LookupDataService<CommonOptionLookup, CommonOption>, ICommonOptionLookupDataService
    {
        public CommonOptionLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class AddressLookupDataService : LookupDataService<AddressLookup, Address>, IAddressLookupDataService
    {
        public AddressLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class CountryLookupDataService : LookupDataService<CountryLookup, Country>, ICountryLookupDataService
    {
        public CountryLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class DistrictLookupDataService : LookupDataService<DistrictLookup, District>, IDistrictLookupDataService
    {
        public DistrictLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class LocalityLookupDataService : LookupDataService<LocalityLookup, Locality>, ILocalityLookupDataService
    {
        public LocalityLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class LocalityTypeLookupDataService : LookupDataService<LocalityTypeLookup, LocalityType>, ILocalityTypeLookupDataService
    {
        public LocalityTypeLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class RegionLookupDataService : LookupDataService<RegionLookup, Region>, IRegionLookupDataService
    {
        public RegionLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class CalculatePriceTaskLookupDataService : LookupDataService<CalculatePriceTaskLookup, CalculatePriceTask>, ICalculatePriceTaskLookupDataService
    {
        public CalculatePriceTaskLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class SumLookupDataService : LookupDataService<SumLookup, Sum>, ISumLookupDataService
    {
        public SumLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class CurrencyExchangeRateLookupDataService : LookupDataService<CurrencyExchangeRateLookup, CurrencyExchangeRate>, ICurrencyExchangeRateLookupDataService
    {
        public CurrencyExchangeRateLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class DescribeProductBlockTaskLookupDataService : LookupDataService<DescribeProductBlockTaskLookup, DescribeProductBlockTask>, IDescribeProductBlockTaskLookupDataService
    {
        public DescribeProductBlockTaskLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class NoteLookupDataService : LookupDataService<NoteLookup, Note>, INoteLookupDataService
    {
        public NoteLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class OfferUnitLookupDataService : LookupDataService<OfferUnitLookup, OfferUnit>, IOfferUnitLookupDataService
    {
        public OfferUnitLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class PaymentConditionSetLookupDataService : LookupDataService<PaymentConditionSetLookup, PaymentConditionSet>, IPaymentConditionSetLookupDataService
    {
        public PaymentConditionSetLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ProductBlockLookupDataService : LookupDataService<ProductBlockLookup, ProductBlock>, IProductBlockLookupDataService
    {
        public ProductBlockLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ProductDependentLookupDataService : LookupDataService<ProductDependentLookup, ProductDependent>, IProductDependentLookupDataService
    {
        public ProductDependentLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ProductionTaskLookupDataService : LookupDataService<ProductionTaskLookup, ProductionTask>, IProductionTaskLookupDataService
    {
        public ProductionTaskLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class SalesBlockLookupDataService : LookupDataService<SalesBlockLookup, SalesBlock>, ISalesBlockLookupDataService
    {
        public SalesBlockLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class BankDetailsLookupDataService : LookupDataService<BankDetailsLookup, BankDetails>, IBankDetailsLookupDataService
    {
        public BankDetailsLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class CompanyLookupDataService : LookupDataService<CompanyLookup, Company>, ICompanyLookupDataService
    {
        public CompanyLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class CompanyFormLookupDataService : LookupDataService<CompanyFormLookup, CompanyForm>, ICompanyFormLookupDataService
    {
        public CompanyFormLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class DocumentsRegistrationDetailsLookupDataService : LookupDataService<DocumentsRegistrationDetailsLookup, DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsLookupDataService
    {
        public DocumentsRegistrationDetailsLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class EmployeesPositionLookupDataService : LookupDataService<EmployeesPositionLookup, EmployeesPosition>, IEmployeesPositionLookupDataService
    {
        public EmployeesPositionLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class FacilityTypeLookupDataService : LookupDataService<FacilityTypeLookup, FacilityType>, IFacilityTypeLookupDataService
    {
        public FacilityTypeLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ActivityFieldLookupDataService : LookupDataService<ActivityFieldLookup, ActivityField>, IActivityFieldLookupDataService
    {
        public ActivityFieldLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ContractLookupDataService : LookupDataService<ContractLookup, Contract>, IContractLookupDataService
    {
        public ContractLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class MeasureLookupDataService : LookupDataService<MeasureLookup, Measure>, IMeasureLookupDataService
    {
        public MeasureLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ParameterLookupDataService : LookupDataService<ParameterLookup, Parameter>, IParameterLookupDataService
    {
        public ParameterLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ParameterGroupLookupDataService : LookupDataService<ParameterGroupLookup, ParameterGroup>, IParameterGroupLookupDataService
    {
        public ParameterGroupLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ProductRelationLookupDataService : LookupDataService<ProductRelationLookup, ProductRelation>, IProductRelationLookupDataService
    {
        public ProductRelationLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class PersonLookupDataService : LookupDataService<PersonLookup, Person>, IPersonLookupDataService
    {
        public PersonLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class PaymentPlannedListLookupDataService : LookupDataService<PaymentPlannedListLookup, PaymentPlannedList>, IPaymentPlannedListLookupDataService
    {
        public PaymentPlannedListLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class PaymentLookupDataService : LookupDataService<PaymentLookup, Payment>, IPaymentLookupDataService
    {
        public PaymentLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ParameterRelationLookupDataService : LookupDataService<ParameterRelationLookup, ParameterRelation>, IParameterRelationLookupDataService
    {
        public ParameterRelationLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class SalesUnitLookupDataService : LookupDataService<SalesUnitLookup, SalesUnit>, ISalesUnitLookupDataService
    {
        public SalesUnitLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ServiceLookupDataService : LookupDataService<ServiceLookup, Service>, IServiceLookupDataService
    {
        public ServiceLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TestFriendAddressLookupDataService : LookupDataService<TestFriendAddressLookup, TestFriendAddress>, ITestFriendAddressLookupDataService
    {
        public TestFriendAddressLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TestFriendLookupDataService : LookupDataService<TestFriendLookup, TestFriend>, ITestFriendLookupDataService
    {
        public TestFriendLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TestFriendEmailLookupDataService : LookupDataService<TestFriendEmailLookup, TestFriendEmail>, ITestFriendEmailLookupDataService
    {
        public TestFriendEmailLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TestFriendGroupLookupDataService : LookupDataService<TestFriendGroupLookup, TestFriendGroup>, ITestFriendGroupLookupDataService
    {
        public TestFriendGroupLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class DocumentLookupDataService : LookupDataService<DocumentLookup, Document>, IDocumentLookupDataService
    {
        public DocumentLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TestEntityLookupDataService : LookupDataService<TestEntityLookup, TestEntity>, ITestEntityLookupDataService
    {
        public TestEntityLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TestHusbandLookupDataService : LookupDataService<TestHusbandLookup, TestHusband>, ITestHusbandLookupDataService
    {
        public TestHusbandLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TestWifeLookupDataService : LookupDataService<TestWifeLookup, TestWife>, ITestWifeLookupDataService
    {
        public TestWifeLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TestChildLookupDataService : LookupDataService<TestChildLookup, TestChild>, ITestChildLookupDataService
    {
        public TestChildLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class SumOnDateLookupDataService : LookupDataService<SumOnDateLookup, SumOnDate>, ISumOnDateLookupDataService
    {
        public SumOnDateLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ProductLookupDataService : LookupDataService<ProductLookup, Product>, IProductLookupDataService
    {
        public ProductLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class OfferLookupDataService : LookupDataService<OfferLookup, Offer>, IOfferLookupDataService
    {
        public OfferLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class EmployeeLookupDataService : LookupDataService<EmployeeLookup, Employee>, IEmployeeLookupDataService
    {
        public EmployeeLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class OrderLookupDataService : LookupDataService<OrderLookup, Order>, IOrderLookupDataService
    {
        public OrderLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class PaymentConditionLookupDataService : LookupDataService<PaymentConditionLookup, PaymentCondition>, IPaymentConditionLookupDataService
    {
        public PaymentConditionLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class PaymentDocumentLookupDataService : LookupDataService<PaymentDocumentLookup, PaymentDocument>, IPaymentDocumentLookupDataService
    {
        public PaymentDocumentLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class FacilityLookupDataService : LookupDataService<FacilityLookup, Facility>, IFacilityLookupDataService
    {
        public FacilityLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class ProjectLookupDataService : LookupDataService<ProjectLookup, Project>, IProjectLookupDataService
    {
        public ProjectLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class UserRoleLookupDataService : LookupDataService<UserRoleLookup, UserRole>, IUserRoleLookupDataService
    {
        public UserRoleLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class SpecificationLookupDataService : LookupDataService<SpecificationLookup, Specification>, ISpecificationLookupDataService
    {
        public SpecificationLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TenderLookupDataService : LookupDataService<TenderLookup, Tender>, ITenderLookupDataService
    {
        public TenderLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class TenderTypeLookupDataService : LookupDataService<TenderTypeLookup, TenderType>, ITenderTypeLookupDataService
    {
        public TenderTypeLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }


    public partial class UserLookupDataService : LookupDataService<UserLookup, User>, IUserLookupDataService
    {
        public UserLookupDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
