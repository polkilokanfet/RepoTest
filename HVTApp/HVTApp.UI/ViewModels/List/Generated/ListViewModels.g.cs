using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{

    public partial class CommonOptionListViewModel : BaseListViewModel<CommonOption, CommonOptionLookup, AfterSaveCommonOptionEvent, AfterSelectCommonOptionEvent, AfterRemoveCommonOptionEvent,  CommonOptionLookupDataService>
    {
        public CommonOptionListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class AddressListViewModel : BaseListViewModel<Address, AddressLookup, AfterSaveAddressEvent, AfterSelectAddressEvent, AfterRemoveAddressEvent,  AddressLookupDataService>
    {
        public AddressListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CountryListViewModel : BaseListViewModel<Country, CountryLookup, AfterSaveCountryEvent, AfterSelectCountryEvent, AfterRemoveCountryEvent,  CountryLookupDataService>
    {
        public CountryListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class DistrictListViewModel : BaseListViewModel<District, DistrictLookup, AfterSaveDistrictEvent, AfterSelectDistrictEvent, AfterRemoveDistrictEvent,  DistrictLookupDataService>
    {
        public DistrictListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class LocalityListViewModel : BaseListViewModel<Locality, LocalityLookup, AfterSaveLocalityEvent, AfterSelectLocalityEvent, AfterRemoveLocalityEvent,  LocalityLookupDataService>
    {
        public LocalityListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class LocalityTypeListViewModel : BaseListViewModel<LocalityType, LocalityTypeLookup, AfterSaveLocalityTypeEvent, AfterSelectLocalityTypeEvent, AfterRemoveLocalityTypeEvent,  LocalityTypeLookupDataService>
    {
        public LocalityTypeListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class RegionListViewModel : BaseListViewModel<Region, RegionLookup, AfterSaveRegionEvent, AfterSelectRegionEvent, AfterRemoveRegionEvent,  RegionLookupDataService>
    {
        public RegionListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CalculatePriceTaskListViewModel : BaseListViewModel<CalculatePriceTask, CalculatePriceTaskLookup, AfterSaveCalculatePriceTaskEvent, AfterSelectCalculatePriceTaskEvent, AfterRemoveCalculatePriceTaskEvent,  CalculatePriceTaskLookupDataService>
    {
        public CalculatePriceTaskListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SumListViewModel : BaseListViewModel<Sum, SumLookup, AfterSaveSumEvent, AfterSelectSumEvent, AfterRemoveSumEvent,  SumLookupDataService>
    {
        public SumListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CurrencyExchangeRateListViewModel : BaseListViewModel<CurrencyExchangeRate, CurrencyExchangeRateLookup, AfterSaveCurrencyExchangeRateEvent, AfterSelectCurrencyExchangeRateEvent, AfterRemoveCurrencyExchangeRateEvent,  CurrencyExchangeRateLookupDataService>
    {
        public CurrencyExchangeRateListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class DescribeProductBlockTaskListViewModel : BaseListViewModel<DescribeProductBlockTask, DescribeProductBlockTaskLookup, AfterSaveDescribeProductBlockTaskEvent, AfterSelectDescribeProductBlockTaskEvent, AfterRemoveDescribeProductBlockTaskEvent,  DescribeProductBlockTaskLookupDataService>
    {
        public DescribeProductBlockTaskListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class NoteListViewModel : BaseListViewModel<Note, NoteLookup, AfterSaveNoteEvent, AfterSelectNoteEvent, AfterRemoveNoteEvent,  NoteLookupDataService>
    {
        public NoteListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class OfferUnitListViewModel : BaseListViewModel<OfferUnit, OfferUnitLookup, AfterSaveOfferUnitEvent, AfterSelectOfferUnitEvent, AfterRemoveOfferUnitEvent,  OfferUnitLookupDataService>
    {
        public OfferUnitListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentConditionSetListViewModel : BaseListViewModel<PaymentConditionSet, PaymentConditionSetLookup, AfterSavePaymentConditionSetEvent, AfterSelectPaymentConditionSetEvent, AfterRemovePaymentConditionSetEvent,  PaymentConditionSetLookupDataService>
    {
        public PaymentConditionSetListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductBlockListViewModel : BaseListViewModel<ProductBlock, ProductBlockLookup, AfterSaveProductBlockEvent, AfterSelectProductBlockEvent, AfterRemoveProductBlockEvent,  ProductBlockLookupDataService>
    {
        public ProductBlockListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductDependentListViewModel : BaseListViewModel<ProductDependent, ProductDependentLookup, AfterSaveProductDependentEvent, AfterSelectProductDependentEvent, AfterRemoveProductDependentEvent,  ProductDependentLookupDataService>
    {
        public ProductDependentListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductionTaskListViewModel : BaseListViewModel<ProductionTask, ProductionTaskLookup, AfterSaveProductionTaskEvent, AfterSelectProductionTaskEvent, AfterRemoveProductionTaskEvent,  ProductionTaskLookupDataService>
    {
        public ProductionTaskListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SalesBlockListViewModel : BaseListViewModel<SalesBlock, SalesBlockLookup, AfterSaveSalesBlockEvent, AfterSelectSalesBlockEvent, AfterRemoveSalesBlockEvent,  SalesBlockLookupDataService>
    {
        public SalesBlockListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class BankDetailsListViewModel : BaseListViewModel<BankDetails, BankDetailsLookup, AfterSaveBankDetailsEvent, AfterSelectBankDetailsEvent, AfterRemoveBankDetailsEvent,  BankDetailsLookupDataService>
    {
        public BankDetailsListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CompanyListViewModel : BaseListViewModel<Company, CompanyLookup, AfterSaveCompanyEvent, AfterSelectCompanyEvent, AfterRemoveCompanyEvent,  CompanyLookupDataService>
    {
        public CompanyListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class CompanyFormListViewModel : BaseListViewModel<CompanyForm, CompanyFormLookup, AfterSaveCompanyFormEvent, AfterSelectCompanyFormEvent, AfterRemoveCompanyFormEvent,  CompanyFormLookupDataService>
    {
        public CompanyFormListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class DocumentsRegistrationDetailsListViewModel : BaseListViewModel<DocumentsRegistrationDetails, DocumentsRegistrationDetailsLookup, AfterSaveDocumentsRegistrationDetailsEvent, AfterSelectDocumentsRegistrationDetailsEvent, AfterRemoveDocumentsRegistrationDetailsEvent,  DocumentsRegistrationDetailsLookupDataService>
    {
        public DocumentsRegistrationDetailsListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class EmployeesPositionListViewModel : BaseListViewModel<EmployeesPosition, EmployeesPositionLookup, AfterSaveEmployeesPositionEvent, AfterSelectEmployeesPositionEvent, AfterRemoveEmployeesPositionEvent,  EmployeesPositionLookupDataService>
    {
        public EmployeesPositionListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class FacilityTypeListViewModel : BaseListViewModel<FacilityType, FacilityTypeLookup, AfterSaveFacilityTypeEvent, AfterSelectFacilityTypeEvent, AfterRemoveFacilityTypeEvent,  FacilityTypeLookupDataService>
    {
        public FacilityTypeListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ActivityFieldListViewModel : BaseListViewModel<ActivityField, ActivityFieldLookup, AfterSaveActivityFieldEvent, AfterSelectActivityFieldEvent, AfterRemoveActivityFieldEvent,  ActivityFieldLookupDataService>
    {
        public ActivityFieldListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ContractListViewModel : BaseListViewModel<Contract, ContractLookup, AfterSaveContractEvent, AfterSelectContractEvent, AfterRemoveContractEvent,  ContractLookupDataService>
    {
        public ContractListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class MeasureListViewModel : BaseListViewModel<Measure, MeasureLookup, AfterSaveMeasureEvent, AfterSelectMeasureEvent, AfterRemoveMeasureEvent,  MeasureLookupDataService>
    {
        public MeasureListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ParameterListViewModel : BaseListViewModel<Parameter, ParameterLookup, AfterSaveParameterEvent, AfterSelectParameterEvent, AfterRemoveParameterEvent,  ParameterLookupDataService>
    {
        public ParameterListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ParameterGroupListViewModel : BaseListViewModel<ParameterGroup, ParameterGroupLookup, AfterSaveParameterGroupEvent, AfterSelectParameterGroupEvent, AfterRemoveParameterGroupEvent,  ParameterGroupLookupDataService>
    {
        public ParameterGroupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductRelationListViewModel : BaseListViewModel<ProductRelation, ProductRelationLookup, AfterSaveProductRelationEvent, AfterSelectProductRelationEvent, AfterRemoveProductRelationEvent,  ProductRelationLookupDataService>
    {
        public ProductRelationListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PersonListViewModel : BaseListViewModel<Person, PersonLookup, AfterSavePersonEvent, AfterSelectPersonEvent, AfterRemovePersonEvent,  PersonLookupDataService>
    {
        public PersonListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentPlannedListListViewModel : BaseListViewModel<PaymentPlannedList, PaymentPlannedListLookup, AfterSavePaymentPlannedListEvent, AfterSelectPaymentPlannedListEvent, AfterRemovePaymentPlannedListEvent,  PaymentPlannedListLookupDataService>
    {
        public PaymentPlannedListListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentPlannedListViewModel : BaseListViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent, AfterSelectPaymentPlannedEvent, AfterRemovePaymentPlannedEvent,  PaymentPlannedLookupDataService>
    {
        public PaymentPlannedListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentActualListViewModel : BaseListViewModel<PaymentActual, PaymentActualLookup, AfterSavePaymentActualEvent, AfterSelectPaymentActualEvent, AfterRemovePaymentActualEvent,  PaymentActualLookupDataService>
    {
        public PaymentActualListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ParameterRelationListViewModel : BaseListViewModel<ParameterRelation, ParameterRelationLookup, AfterSaveParameterRelationEvent, AfterSelectParameterRelationEvent, AfterRemoveParameterRelationEvent,  ParameterRelationLookupDataService>
    {
        public ParameterRelationListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SalesUnitListViewModel : BaseListViewModel<SalesUnit, SalesUnitLookup, AfterSaveSalesUnitEvent, AfterSelectSalesUnitEvent, AfterRemoveSalesUnitEvent,  SalesUnitLookupDataService>
    {
        public SalesUnitListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ServiceListViewModel : BaseListViewModel<Service, ServiceLookup, AfterSaveServiceEvent, AfterSelectServiceEvent, AfterRemoveServiceEvent,  ServiceLookupDataService>
    {
        public ServiceListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestFriendAddressListViewModel : BaseListViewModel<TestFriendAddress, TestFriendAddressLookup, AfterSaveTestFriendAddressEvent, AfterSelectTestFriendAddressEvent, AfterRemoveTestFriendAddressEvent,  TestFriendAddressLookupDataService>
    {
        public TestFriendAddressListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestFriendListViewModel : BaseListViewModel<TestFriend, TestFriendLookup, AfterSaveTestFriendEvent, AfterSelectTestFriendEvent, AfterRemoveTestFriendEvent,  TestFriendLookupDataService>
    {
        public TestFriendListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestFriendEmailListViewModel : BaseListViewModel<TestFriendEmail, TestFriendEmailLookup, AfterSaveTestFriendEmailEvent, AfterSelectTestFriendEmailEvent, AfterRemoveTestFriendEmailEvent,  TestFriendEmailLookupDataService>
    {
        public TestFriendEmailListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestFriendGroupListViewModel : BaseListViewModel<TestFriendGroup, TestFriendGroupLookup, AfterSaveTestFriendGroupEvent, AfterSelectTestFriendGroupEvent, AfterRemoveTestFriendGroupEvent,  TestFriendGroupLookupDataService>
    {
        public TestFriendGroupListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class DocumentListViewModel : BaseListViewModel<Document, DocumentLookup, AfterSaveDocumentEvent, AfterSelectDocumentEvent, AfterRemoveDocumentEvent,  DocumentLookupDataService>
    {
        public DocumentListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestEntityListViewModel : BaseListViewModel<TestEntity, TestEntityLookup, AfterSaveTestEntityEvent, AfterSelectTestEntityEvent, AfterRemoveTestEntityEvent,  TestEntityLookupDataService>
    {
        public TestEntityListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestHusbandListViewModel : BaseListViewModel<TestHusband, TestHusbandLookup, AfterSaveTestHusbandEvent, AfterSelectTestHusbandEvent, AfterRemoveTestHusbandEvent,  TestHusbandLookupDataService>
    {
        public TestHusbandListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestWifeListViewModel : BaseListViewModel<TestWife, TestWifeLookup, AfterSaveTestWifeEvent, AfterSelectTestWifeEvent, AfterRemoveTestWifeEvent,  TestWifeLookupDataService>
    {
        public TestWifeListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TestChildListViewModel : BaseListViewModel<TestChild, TestChildLookup, AfterSaveTestChildEvent, AfterSelectTestChildEvent, AfterRemoveTestChildEvent,  TestChildLookupDataService>
    {
        public TestChildListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SumOnDateListViewModel : BaseListViewModel<SumOnDate, SumOnDateLookup, AfterSaveSumOnDateEvent, AfterSelectSumOnDateEvent, AfterRemoveSumOnDateEvent,  SumOnDateLookupDataService>
    {
        public SumOnDateListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProductListViewModel : BaseListViewModel<Product, ProductLookup, AfterSaveProductEvent, AfterSelectProductEvent, AfterRemoveProductEvent,  ProductLookupDataService>
    {
        public ProductListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class OfferListViewModel : BaseListViewModel<Offer, OfferLookup, AfterSaveOfferEvent, AfterSelectOfferEvent, AfterRemoveOfferEvent,  OfferLookupDataService>
    {
        public OfferListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class EmployeeListViewModel : BaseListViewModel<Employee, EmployeeLookup, AfterSaveEmployeeEvent, AfterSelectEmployeeEvent, AfterRemoveEmployeeEvent,  EmployeeLookupDataService>
    {
        public EmployeeListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class OrderListViewModel : BaseListViewModel<Order, OrderLookup, AfterSaveOrderEvent, AfterSelectOrderEvent, AfterRemoveOrderEvent,  OrderLookupDataService>
    {
        public OrderListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentConditionListViewModel : BaseListViewModel<PaymentCondition, PaymentConditionLookup, AfterSavePaymentConditionEvent, AfterSelectPaymentConditionEvent, AfterRemovePaymentConditionEvent,  PaymentConditionLookupDataService>
    {
        public PaymentConditionListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class PaymentDocumentListViewModel : BaseListViewModel<PaymentDocument, PaymentDocumentLookup, AfterSavePaymentDocumentEvent, AfterSelectPaymentDocumentEvent, AfterRemovePaymentDocumentEvent,  PaymentDocumentLookupDataService>
    {
        public PaymentDocumentListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class FacilityListViewModel : BaseListViewModel<Facility, FacilityLookup, AfterSaveFacilityEvent, AfterSelectFacilityEvent, AfterRemoveFacilityEvent,  FacilityLookupDataService>
    {
        public FacilityListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class ProjectListViewModel : BaseListViewModel<Project, ProjectLookup, AfterSaveProjectEvent, AfterSelectProjectEvent, AfterRemoveProjectEvent,  ProjectLookupDataService>
    {
        public ProjectListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class UserRoleListViewModel : BaseListViewModel<UserRole, UserRoleLookup, AfterSaveUserRoleEvent, AfterSelectUserRoleEvent, AfterRemoveUserRoleEvent,  UserRoleLookupDataService>
    {
        public UserRoleListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class SpecificationListViewModel : BaseListViewModel<Specification, SpecificationLookup, AfterSaveSpecificationEvent, AfterSelectSpecificationEvent, AfterRemoveSpecificationEvent,  SpecificationLookupDataService>
    {
        public SpecificationListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TenderListViewModel : BaseListViewModel<Tender, TenderLookup, AfterSaveTenderEvent, AfterSelectTenderEvent, AfterRemoveTenderEvent,  TenderLookupDataService>
    {
        public TenderListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class TenderTypeListViewModel : BaseListViewModel<TenderType, TenderTypeLookup, AfterSaveTenderTypeEvent, AfterSelectTenderTypeEvent, AfterRemoveTenderTypeEvent,  TenderTypeLookupDataService>
    {
        public TenderTypeListViewModel(IUnityContainer container) : base(container) { }
    }


    public partial class UserListViewModel : BaseListViewModel<User, UserLookup, AfterSaveUserEvent, AfterSelectUserEvent, AfterRemoveUserEvent,  UserLookupDataService>
    {
        public UserListViewModel(IUnityContainer container) : base(container) { }
    }


}
