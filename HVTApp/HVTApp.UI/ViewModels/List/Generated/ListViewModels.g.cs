using HVTApp.Model.POCOs;
using HVTApp.Model.Events;
using Microsoft.Practices.Unity;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{

    public partial class PaymentActualLookupListViewModel : BaseListViewModel<PaymentActual, PaymentActualLookup, AfterSavePaymentActualEvent, AfterSelectPaymentActualEvent, AfterRemovePaymentActualEvent,  PaymentActualLookupDataService>
    {
        public PaymentActualLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentPlannedLookupListViewModel : BaseListViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent, AfterSelectPaymentPlannedEvent, AfterRemovePaymentPlannedEvent,  PaymentPlannedLookupDataService>
    {
        public PaymentPlannedLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductDesignationLookupListViewModel : BaseListViewModel<ProductDesignation, ProductDesignationLookup, AfterSaveProductDesignationEvent, AfterSelectProductDesignationEvent, AfterRemoveProductDesignationEvent,  ProductDesignationLookupDataService>
    {
        public ProductDesignationLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductTypeLookupListViewModel : BaseListViewModel<ProductType, ProductTypeLookup, AfterSaveProductTypeEvent, AfterSelectProductTypeEvent, AfterRemoveProductTypeEvent,  ProductTypeLookupDataService>
    {
        public ProductTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductTypeDesignationLookupListViewModel : BaseListViewModel<ProductTypeDesignation, ProductTypeDesignationLookup, AfterSaveProductTypeDesignationEvent, AfterSelectProductTypeDesignationEvent, AfterRemoveProductTypeDesignationEvent,  ProductTypeDesignationLookupDataService>
    {
        public ProductTypeDesignationLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProjectTypeLookupListViewModel : BaseListViewModel<ProjectType, ProjectTypeLookup, AfterSaveProjectTypeEvent, AfterSelectProjectTypeEvent, AfterRemoveProjectTypeEvent,  ProjectTypeLookupDataService>
    {
        public ProjectTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CommonOptionLookupListViewModel : BaseListViewModel<CommonOption, CommonOptionLookup, AfterSaveCommonOptionEvent, AfterSelectCommonOptionEvent, AfterRemoveCommonOptionEvent,  CommonOptionLookupDataService>
    {
        public CommonOptionLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class AddressLookupListViewModel : BaseListViewModel<Address, AddressLookup, AfterSaveAddressEvent, AfterSelectAddressEvent, AfterRemoveAddressEvent,  AddressLookupDataService>
    {
        public AddressLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CountryLookupListViewModel : BaseListViewModel<Country, CountryLookup, AfterSaveCountryEvent, AfterSelectCountryEvent, AfterRemoveCountryEvent,  CountryLookupDataService>
    {
        public CountryLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class DistrictLookupListViewModel : BaseListViewModel<District, DistrictLookup, AfterSaveDistrictEvent, AfterSelectDistrictEvent, AfterRemoveDistrictEvent,  DistrictLookupDataService>
    {
        public DistrictLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class LocalityLookupListViewModel : BaseListViewModel<Locality, LocalityLookup, AfterSaveLocalityEvent, AfterSelectLocalityEvent, AfterRemoveLocalityEvent,  LocalityLookupDataService>
    {
        public LocalityLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class LocalityTypeLookupListViewModel : BaseListViewModel<LocalityType, LocalityTypeLookup, AfterSaveLocalityTypeEvent, AfterSelectLocalityTypeEvent, AfterRemoveLocalityTypeEvent,  LocalityTypeLookupDataService>
    {
        public LocalityTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class RegionLookupListViewModel : BaseListViewModel<Region, RegionLookup, AfterSaveRegionEvent, AfterSelectRegionEvent, AfterRemoveRegionEvent,  RegionLookupDataService>
    {
        public RegionLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CalculatePriceTaskLookupListViewModel : BaseListViewModel<CalculatePriceTask, CalculatePriceTaskLookup, AfterSaveCalculatePriceTaskEvent, AfterSelectCalculatePriceTaskEvent, AfterRemoveCalculatePriceTaskEvent,  CalculatePriceTaskLookupDataService>
    {
        public CalculatePriceTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SumLookupListViewModel : BaseListViewModel<Sum, SumLookup, AfterSaveSumEvent, AfterSelectSumEvent, AfterRemoveSumEvent,  SumLookupDataService>
    {
        public SumLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CurrencyExchangeRateLookupListViewModel : BaseListViewModel<CurrencyExchangeRate, CurrencyExchangeRateLookup, AfterSaveCurrencyExchangeRateEvent, AfterSelectCurrencyExchangeRateEvent, AfterRemoveCurrencyExchangeRateEvent,  CurrencyExchangeRateLookupDataService>
    {
        public CurrencyExchangeRateLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class DescribeProductBlockTaskLookupListViewModel : BaseListViewModel<DescribeProductBlockTask, DescribeProductBlockTaskLookup, AfterSaveDescribeProductBlockTaskEvent, AfterSelectDescribeProductBlockTaskEvent, AfterRemoveDescribeProductBlockTaskEvent,  DescribeProductBlockTaskLookupDataService>
    {
        public DescribeProductBlockTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class NoteLookupListViewModel : BaseListViewModel<Note, NoteLookup, AfterSaveNoteEvent, AfterSelectNoteEvent, AfterRemoveNoteEvent,  NoteLookupDataService>
    {
        public NoteLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class OfferUnitLookupListViewModel : BaseListViewModel<OfferUnit, OfferUnitLookup, AfterSaveOfferUnitEvent, AfterSelectOfferUnitEvent, AfterRemoveOfferUnitEvent,  OfferUnitLookupDataService>
    {
        public OfferUnitLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentConditionSetLookupListViewModel : BaseListViewModel<PaymentConditionSet, PaymentConditionSetLookup, AfterSavePaymentConditionSetEvent, AfterSelectPaymentConditionSetEvent, AfterRemovePaymentConditionSetEvent,  PaymentConditionSetLookupDataService>
    {
        public PaymentConditionSetLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductBlockLookupListViewModel : BaseListViewModel<ProductBlock, ProductBlockLookup, AfterSaveProductBlockEvent, AfterSelectProductBlockEvent, AfterRemoveProductBlockEvent,  ProductBlockLookupDataService>
    {
        public ProductBlockLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductDependentLookupListViewModel : BaseListViewModel<ProductDependent, ProductDependentLookup, AfterSaveProductDependentEvent, AfterSelectProductDependentEvent, AfterRemoveProductDependentEvent,  ProductDependentLookupDataService>
    {
        public ProductDependentLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductionTaskLookupListViewModel : BaseListViewModel<ProductionTask, ProductionTaskLookup, AfterSaveProductionTaskEvent, AfterSelectProductionTaskEvent, AfterRemoveProductionTaskEvent,  ProductionTaskLookupDataService>
    {
        public ProductionTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SalesBlockLookupListViewModel : BaseListViewModel<SalesBlock, SalesBlockLookup, AfterSaveSalesBlockEvent, AfterSelectSalesBlockEvent, AfterRemoveSalesBlockEvent,  SalesBlockLookupDataService>
    {
        public SalesBlockLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class BankDetailsLookupListViewModel : BaseListViewModel<BankDetails, BankDetailsLookup, AfterSaveBankDetailsEvent, AfterSelectBankDetailsEvent, AfterRemoveBankDetailsEvent,  BankDetailsLookupDataService>
    {
        public BankDetailsLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CompanyLookupListViewModel : BaseListViewModel<Company, CompanyLookup, AfterSaveCompanyEvent, AfterSelectCompanyEvent, AfterRemoveCompanyEvent,  CompanyLookupDataService>
    {
        public CompanyLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CompanyFormLookupListViewModel : BaseListViewModel<CompanyForm, CompanyFormLookup, AfterSaveCompanyFormEvent, AfterSelectCompanyFormEvent, AfterRemoveCompanyFormEvent,  CompanyFormLookupDataService>
    {
        public CompanyFormLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class DocumentsRegistrationDetailsLookupListViewModel : BaseListViewModel<DocumentsRegistrationDetails, DocumentsRegistrationDetailsLookup, AfterSaveDocumentsRegistrationDetailsEvent, AfterSelectDocumentsRegistrationDetailsEvent, AfterRemoveDocumentsRegistrationDetailsEvent,  DocumentsRegistrationDetailsLookupDataService>
    {
        public DocumentsRegistrationDetailsLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class EmployeesPositionLookupListViewModel : BaseListViewModel<EmployeesPosition, EmployeesPositionLookup, AfterSaveEmployeesPositionEvent, AfterSelectEmployeesPositionEvent, AfterRemoveEmployeesPositionEvent,  EmployeesPositionLookupDataService>
    {
        public EmployeesPositionLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class FacilityTypeLookupListViewModel : BaseListViewModel<FacilityType, FacilityTypeLookup, AfterSaveFacilityTypeEvent, AfterSelectFacilityTypeEvent, AfterRemoveFacilityTypeEvent,  FacilityTypeLookupDataService>
    {
        public FacilityTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ActivityFieldLookupListViewModel : BaseListViewModel<ActivityField, ActivityFieldLookup, AfterSaveActivityFieldEvent, AfterSelectActivityFieldEvent, AfterRemoveActivityFieldEvent,  ActivityFieldLookupDataService>
    {
        public ActivityFieldLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ContractLookupListViewModel : BaseListViewModel<Contract, ContractLookup, AfterSaveContractEvent, AfterSelectContractEvent, AfterRemoveContractEvent,  ContractLookupDataService>
    {
        public ContractLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class MeasureLookupListViewModel : BaseListViewModel<Measure, MeasureLookup, AfterSaveMeasureEvent, AfterSelectMeasureEvent, AfterRemoveMeasureEvent,  MeasureLookupDataService>
    {
        public MeasureLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ParameterLookupListViewModel : BaseListViewModel<Parameter, ParameterLookup, AfterSaveParameterEvent, AfterSelectParameterEvent, AfterRemoveParameterEvent,  ParameterLookupDataService>
    {
        public ParameterLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ParameterGroupLookupListViewModel : BaseListViewModel<ParameterGroup, ParameterGroupLookup, AfterSaveParameterGroupEvent, AfterSelectParameterGroupEvent, AfterRemoveParameterGroupEvent,  ParameterGroupLookupDataService>
    {
        public ParameterGroupLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductRelationLookupListViewModel : BaseListViewModel<ProductRelation, ProductRelationLookup, AfterSaveProductRelationEvent, AfterSelectProductRelationEvent, AfterRemoveProductRelationEvent,  ProductRelationLookupDataService>
    {
        public ProductRelationLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PersonLookupListViewModel : BaseListViewModel<Person, PersonLookup, AfterSavePersonEvent, AfterSelectPersonEvent, AfterRemovePersonEvent,  PersonLookupDataService>
    {
        public PersonLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ParameterRelationLookupListViewModel : BaseListViewModel<ParameterRelation, ParameterRelationLookup, AfterSaveParameterRelationEvent, AfterSelectParameterRelationEvent, AfterRemoveParameterRelationEvent,  ParameterRelationLookupDataService>
    {
        public ParameterRelationLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SalesUnitLookupListViewModel : BaseListViewModel<SalesUnit, SalesUnitLookup, AfterSaveSalesUnitEvent, AfterSelectSalesUnitEvent, AfterRemoveSalesUnitEvent,  SalesUnitLookupDataService>
    {
        public SalesUnitLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ServiceLookupListViewModel : BaseListViewModel<Service, ServiceLookup, AfterSaveServiceEvent, AfterSelectServiceEvent, AfterRemoveServiceEvent,  ServiceLookupDataService>
    {
        public ServiceLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestFriendAddressLookupListViewModel : BaseListViewModel<TestFriendAddress, TestFriendAddressLookup, AfterSaveTestFriendAddressEvent, AfterSelectTestFriendAddressEvent, AfterRemoveTestFriendAddressEvent,  TestFriendAddressLookupDataService>
    {
        public TestFriendAddressLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestFriendLookupListViewModel : BaseListViewModel<TestFriend, TestFriendLookup, AfterSaveTestFriendEvent, AfterSelectTestFriendEvent, AfterRemoveTestFriendEvent,  TestFriendLookupDataService>
    {
        public TestFriendLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestFriendEmailLookupListViewModel : BaseListViewModel<TestFriendEmail, TestFriendEmailLookup, AfterSaveTestFriendEmailEvent, AfterSelectTestFriendEmailEvent, AfterRemoveTestFriendEmailEvent,  TestFriendEmailLookupDataService>
    {
        public TestFriendEmailLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestFriendGroupLookupListViewModel : BaseListViewModel<TestFriendGroup, TestFriendGroupLookup, AfterSaveTestFriendGroupEvent, AfterSelectTestFriendGroupEvent, AfterRemoveTestFriendGroupEvent,  TestFriendGroupLookupDataService>
    {
        public TestFriendGroupLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class DocumentLookupListViewModel : BaseListViewModel<Document, DocumentLookup, AfterSaveDocumentEvent, AfterSelectDocumentEvent, AfterRemoveDocumentEvent,  DocumentLookupDataService>
    {
        public DocumentLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestEntityLookupListViewModel : BaseListViewModel<TestEntity, TestEntityLookup, AfterSaveTestEntityEvent, AfterSelectTestEntityEvent, AfterRemoveTestEntityEvent,  TestEntityLookupDataService>
    {
        public TestEntityLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestHusbandLookupListViewModel : BaseListViewModel<TestHusband, TestHusbandLookup, AfterSaveTestHusbandEvent, AfterSelectTestHusbandEvent, AfterRemoveTestHusbandEvent,  TestHusbandLookupDataService>
    {
        public TestHusbandLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestWifeLookupListViewModel : BaseListViewModel<TestWife, TestWifeLookup, AfterSaveTestWifeEvent, AfterSelectTestWifeEvent, AfterRemoveTestWifeEvent,  TestWifeLookupDataService>
    {
        public TestWifeLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestChildLookupListViewModel : BaseListViewModel<TestChild, TestChildLookup, AfterSaveTestChildEvent, AfterSelectTestChildEvent, AfterRemoveTestChildEvent,  TestChildLookupDataService>
    {
        public TestChildLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SumOnDateLookupListViewModel : BaseListViewModel<SumOnDate, SumOnDateLookup, AfterSaveSumOnDateEvent, AfterSelectSumOnDateEvent, AfterRemoveSumOnDateEvent,  SumOnDateLookupDataService>
    {
        public SumOnDateLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductLookupListViewModel : BaseListViewModel<Product, ProductLookup, AfterSaveProductEvent, AfterSelectProductEvent, AfterRemoveProductEvent,  ProductLookupDataService>
    {
        public ProductLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class OfferLookupListViewModel : BaseListViewModel<Offer, OfferLookup, AfterSaveOfferEvent, AfterSelectOfferEvent, AfterRemoveOfferEvent,  OfferLookupDataService>
    {
        public OfferLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class EmployeeLookupListViewModel : BaseListViewModel<Employee, EmployeeLookup, AfterSaveEmployeeEvent, AfterSelectEmployeeEvent, AfterRemoveEmployeeEvent,  EmployeeLookupDataService>
    {
        public EmployeeLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class OrderLookupListViewModel : BaseListViewModel<Order, OrderLookup, AfterSaveOrderEvent, AfterSelectOrderEvent, AfterRemoveOrderEvent,  OrderLookupDataService>
    {
        public OrderLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentConditionLookupListViewModel : BaseListViewModel<PaymentCondition, PaymentConditionLookup, AfterSavePaymentConditionEvent, AfterSelectPaymentConditionEvent, AfterRemovePaymentConditionEvent,  PaymentConditionLookupDataService>
    {
        public PaymentConditionLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentDocumentLookupListViewModel : BaseListViewModel<PaymentDocument, PaymentDocumentLookup, AfterSavePaymentDocumentEvent, AfterSelectPaymentDocumentEvent, AfterRemovePaymentDocumentEvent,  PaymentDocumentLookupDataService>
    {
        public PaymentDocumentLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class FacilityLookupListViewModel : BaseListViewModel<Facility, FacilityLookup, AfterSaveFacilityEvent, AfterSelectFacilityEvent, AfterRemoveFacilityEvent,  FacilityLookupDataService>
    {
        public FacilityLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProjectLookupListViewModel : BaseListViewModel<Project, ProjectLookup, AfterSaveProjectEvent, AfterSelectProjectEvent, AfterRemoveProjectEvent,  ProjectLookupDataService>
    {
        public ProjectLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class UserRoleLookupListViewModel : BaseListViewModel<UserRole, UserRoleLookup, AfterSaveUserRoleEvent, AfterSelectUserRoleEvent, AfterRemoveUserRoleEvent,  UserRoleLookupDataService>
    {
        public UserRoleLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SpecificationLookupListViewModel : BaseListViewModel<Specification, SpecificationLookup, AfterSaveSpecificationEvent, AfterSelectSpecificationEvent, AfterRemoveSpecificationEvent,  SpecificationLookupDataService>
    {
        public SpecificationLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TenderLookupListViewModel : BaseListViewModel<Tender, TenderLookup, AfterSaveTenderEvent, AfterSelectTenderEvent, AfterRemoveTenderEvent,  TenderLookupDataService>
    {
        public TenderLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TenderTypeLookupListViewModel : BaseListViewModel<TenderType, TenderTypeLookup, AfterSaveTenderTypeEvent, AfterSelectTenderTypeEvent, AfterRemoveTenderTypeEvent,  TenderTypeLookupDataService>
    {
        public TenderTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class UserLookupListViewModel : BaseListViewModel<User, UserLookup, AfterSaveUserEvent, AfterSelectUserEvent, AfterRemoveUserEvent,  UserLookupDataService>
    {
        public UserLookupListViewModel(IUnityContainer container) : base(container) { }
    }


}
