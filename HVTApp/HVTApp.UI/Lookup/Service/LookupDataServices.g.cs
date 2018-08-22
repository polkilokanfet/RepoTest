using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{

    public partial class CommonOptionLookupDataService : LookupDataService<CommonOptionLookup, CommonOption>, ICommonOptionLookupDataService
    {
        public CommonOptionLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class AddressLookupDataService : LookupDataService<AddressLookup, Address>, IAddressLookupDataService
    {
        public AddressLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class CountryLookupDataService : LookupDataService<CountryLookup, Country>, ICountryLookupDataService
    {
        public CountryLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class DistrictLookupDataService : LookupDataService<DistrictLookup, District>, IDistrictLookupDataService
    {
        public DistrictLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class LocalityLookupDataService : LookupDataService<LocalityLookup, Locality>, ILocalityLookupDataService
    {
        public LocalityLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class LocalityTypeLookupDataService : LookupDataService<LocalityTypeLookup, LocalityType>, ILocalityTypeLookupDataService
    {
        public LocalityTypeLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class RegionLookupDataService : LookupDataService<RegionLookup, Region>, IRegionLookupDataService
    {
        public RegionLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class CalculatePriceTaskLookupDataService : LookupDataService<CalculatePriceTaskLookup, CalculatePriceTask>, ICalculatePriceTaskLookupDataService
    {
        public CalculatePriceTaskLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class SumLookupDataService : LookupDataService<SumLookup, Sum>, ISumLookupDataService
    {
        public SumLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class CurrencyExchangeRateLookupDataService : LookupDataService<CurrencyExchangeRateLookup, CurrencyExchangeRate>, ICurrencyExchangeRateLookupDataService
    {
        public CurrencyExchangeRateLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class DescribeProductBlockTaskLookupDataService : LookupDataService<DescribeProductBlockTaskLookup, DescribeProductBlockTask>, IDescribeProductBlockTaskLookupDataService
    {
        public DescribeProductBlockTaskLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class NoteLookupDataService : LookupDataService<NoteLookup, Note>, INoteLookupDataService
    {
        public NoteLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class OfferUnitLookupDataService : LookupDataService<OfferUnitLookup, OfferUnit>, IOfferUnitLookupDataService
    {
        public OfferUnitLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class PaymentConditionSetLookupDataService : LookupDataService<PaymentConditionSetLookup, PaymentConditionSet>, IPaymentConditionSetLookupDataService
    {
        public PaymentConditionSetLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ProductBlockLookupDataService : LookupDataService<ProductBlockLookup, ProductBlock>, IProductBlockLookupDataService
    {
        public ProductBlockLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ProductDependentLookupDataService : LookupDataService<ProductDependentLookup, ProductDependent>, IProductDependentLookupDataService
    {
        public ProductDependentLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ProductionTaskLookupDataService : LookupDataService<ProductionTaskLookup, ProductionTask>, IProductionTaskLookupDataService
    {
        public ProductionTaskLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class SalesBlockLookupDataService : LookupDataService<SalesBlockLookup, SalesBlock>, ISalesBlockLookupDataService
    {
        public SalesBlockLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class BankDetailsLookupDataService : LookupDataService<BankDetailsLookup, BankDetails>, IBankDetailsLookupDataService
    {
        public BankDetailsLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class CompanyLookupDataService : LookupDataService<CompanyLookup, Company>, ICompanyLookupDataService
    {
        public CompanyLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class CompanyFormLookupDataService : LookupDataService<CompanyFormLookup, CompanyForm>, ICompanyFormLookupDataService
    {
        public CompanyFormLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class DocumentsRegistrationDetailsLookupDataService : LookupDataService<DocumentsRegistrationDetailsLookup, DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsLookupDataService
    {
        public DocumentsRegistrationDetailsLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class EmployeesPositionLookupDataService : LookupDataService<EmployeesPositionLookup, EmployeesPosition>, IEmployeesPositionLookupDataService
    {
        public EmployeesPositionLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class FacilityTypeLookupDataService : LookupDataService<FacilityTypeLookup, FacilityType>, IFacilityTypeLookupDataService
    {
        public FacilityTypeLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ActivityFieldLookupDataService : LookupDataService<ActivityFieldLookup, ActivityField>, IActivityFieldLookupDataService
    {
        public ActivityFieldLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ContractLookupDataService : LookupDataService<ContractLookup, Contract>, IContractLookupDataService
    {
        public ContractLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class MeasureLookupDataService : LookupDataService<MeasureLookup, Measure>, IMeasureLookupDataService
    {
        public MeasureLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ParameterLookupDataService : LookupDataService<ParameterLookup, Parameter>, IParameterLookupDataService
    {
        public ParameterLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ParameterGroupLookupDataService : LookupDataService<ParameterGroupLookup, ParameterGroup>, IParameterGroupLookupDataService
    {
        public ParameterGroupLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ProductRelationLookupDataService : LookupDataService<ProductRelationLookup, ProductRelation>, IProductRelationLookupDataService
    {
        public ProductRelationLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class PersonLookupDataService : LookupDataService<PersonLookup, Person>, IPersonLookupDataService
    {
        public PersonLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class PaymentPlannedListLookupDataService : LookupDataService<PaymentPlannedListLookup, PaymentPlannedList>, IPaymentPlannedListLookupDataService
    {
        public PaymentPlannedListLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class PaymentPlannedLookupDataService : LookupDataService<PaymentPlannedLookup, PaymentPlanned>, IPaymentPlannedLookupDataService
    {
        public PaymentPlannedLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class PaymentActualLookupDataService : LookupDataService<PaymentActualLookup, PaymentActual>, IPaymentActualLookupDataService
    {
        public PaymentActualLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ParameterRelationLookupDataService : LookupDataService<ParameterRelationLookup, ParameterRelation>, IParameterRelationLookupDataService
    {
        public ParameterRelationLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class SalesUnitLookupDataService : LookupDataService<SalesUnitLookup, SalesUnit>, ISalesUnitLookupDataService
    {
        public SalesUnitLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ServiceLookupDataService : LookupDataService<ServiceLookup, Service>, IServiceLookupDataService
    {
        public ServiceLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TestFriendAddressLookupDataService : LookupDataService<TestFriendAddressLookup, TestFriendAddress>, ITestFriendAddressLookupDataService
    {
        public TestFriendAddressLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TestFriendLookupDataService : LookupDataService<TestFriendLookup, TestFriend>, ITestFriendLookupDataService
    {
        public TestFriendLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TestFriendEmailLookupDataService : LookupDataService<TestFriendEmailLookup, TestFriendEmail>, ITestFriendEmailLookupDataService
    {
        public TestFriendEmailLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TestFriendGroupLookupDataService : LookupDataService<TestFriendGroupLookup, TestFriendGroup>, ITestFriendGroupLookupDataService
    {
        public TestFriendGroupLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class DocumentLookupDataService : LookupDataService<DocumentLookup, Document>, IDocumentLookupDataService
    {
        public DocumentLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TestEntityLookupDataService : LookupDataService<TestEntityLookup, TestEntity>, ITestEntityLookupDataService
    {
        public TestEntityLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TestHusbandLookupDataService : LookupDataService<TestHusbandLookup, TestHusband>, ITestHusbandLookupDataService
    {
        public TestHusbandLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TestWifeLookupDataService : LookupDataService<TestWifeLookup, TestWife>, ITestWifeLookupDataService
    {
        public TestWifeLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TestChildLookupDataService : LookupDataService<TestChildLookup, TestChild>, ITestChildLookupDataService
    {
        public TestChildLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class SumOnDateLookupDataService : LookupDataService<SumOnDateLookup, SumOnDate>, ISumOnDateLookupDataService
    {
        public SumOnDateLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ProductLookupDataService : LookupDataService<ProductLookup, Product>, IProductLookupDataService
    {
        public ProductLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class OfferLookupDataService : LookupDataService<OfferLookup, Offer>, IOfferLookupDataService
    {
        public OfferLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class EmployeeLookupDataService : LookupDataService<EmployeeLookup, Employee>, IEmployeeLookupDataService
    {
        public EmployeeLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class OrderLookupDataService : LookupDataService<OrderLookup, Order>, IOrderLookupDataService
    {
        public OrderLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class PaymentConditionLookupDataService : LookupDataService<PaymentConditionLookup, PaymentCondition>, IPaymentConditionLookupDataService
    {
        public PaymentConditionLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class PaymentDocumentLookupDataService : LookupDataService<PaymentDocumentLookup, PaymentDocument>, IPaymentDocumentLookupDataService
    {
        public PaymentDocumentLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class FacilityLookupDataService : LookupDataService<FacilityLookup, Facility>, IFacilityLookupDataService
    {
        public FacilityLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ProjectLookupDataService : LookupDataService<ProjectLookup, Project>, IProjectLookupDataService
    {
        public ProjectLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class ProjectUnitLookupDataService : LookupDataService<ProjectUnitLookup, ProjectUnit>, IProjectUnitLookupDataService
    {
        public ProjectUnitLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class UserRoleLookupDataService : LookupDataService<UserRoleLookup, UserRole>, IUserRoleLookupDataService
    {
        public UserRoleLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class SpecificationLookupDataService : LookupDataService<SpecificationLookup, Specification>, ISpecificationLookupDataService
    {
        public SpecificationLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TenderLookupDataService : LookupDataService<TenderLookup, Tender>, ITenderLookupDataService
    {
        public TenderLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class TenderTypeLookupDataService : LookupDataService<TenderTypeLookup, TenderType>, ITenderTypeLookupDataService
    {
        public TenderTypeLookupDataService(HvtAppContext context) : base(context) { }
    }


    public partial class UserLookupDataService : LookupDataService<UserLookup, User>, IUserLookupDataService
    {
        public UserLookupDataService(HvtAppContext context) : base(context) { }
    }

}
