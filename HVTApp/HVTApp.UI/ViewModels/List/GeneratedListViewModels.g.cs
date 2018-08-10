using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public partial class CommonOptionListViewModel : BaseListViewModel<CommonOption, CommonOptionLookup, AfterSaveCommonOptionEvent, AfterSelectCommonOptionEvent, AfterRemoveCommonOptionEvent>
    {
        public CommonOptionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class AddressListViewModel : BaseListViewModel<Address, AddressLookup, AfterSaveAddressEvent, AfterSelectAddressEvent, AfterRemoveAddressEvent>
    {
        public AddressListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CountryListViewModel : BaseListViewModel<Country, CountryLookup, AfterSaveCountryEvent, AfterSelectCountryEvent, AfterRemoveCountryEvent>
    {
        public CountryListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DistrictListViewModel : BaseListViewModel<District, DistrictLookup, AfterSaveDistrictEvent, AfterSelectDistrictEvent, AfterRemoveDistrictEvent>
    {
        public DistrictListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityListViewModel : BaseListViewModel<Locality, LocalityLookup, AfterSaveLocalityEvent, AfterSelectLocalityEvent, AfterRemoveLocalityEvent>
    {
        public LocalityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityTypeListViewModel : BaseListViewModel<LocalityType, LocalityTypeLookup, AfterSaveLocalityTypeEvent, AfterSelectLocalityTypeEvent, AfterRemoveLocalityTypeEvent>
    {
        public LocalityTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class RegionListViewModel : BaseListViewModel<Region, RegionLookup, AfterSaveRegionEvent, AfterSelectRegionEvent, AfterRemoveRegionEvent>
    {
        public RegionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CalculatePriceTaskListViewModel : BaseListViewModel<CalculatePriceTask, CalculatePriceTaskLookup, AfterSaveCalculatePriceTaskEvent, AfterSelectCalculatePriceTaskEvent, AfterRemoveCalculatePriceTaskEvent>
    {
        public CalculatePriceTaskListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SumListViewModel : BaseListViewModel<Sum, SumLookup, AfterSaveSumEvent, AfterSelectSumEvent, AfterRemoveSumEvent>
    {
        public SumListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CurrencyExchangeRateListViewModel : BaseListViewModel<CurrencyExchangeRate, CurrencyExchangeRateLookup, AfterSaveCurrencyExchangeRateEvent, AfterSelectCurrencyExchangeRateEvent, AfterRemoveCurrencyExchangeRateEvent>
    {
        public CurrencyExchangeRateListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DescribeProductBlockTaskListViewModel : BaseListViewModel<DescribeProductBlockTask, DescribeProductBlockTaskLookup, AfterSaveDescribeProductBlockTaskEvent, AfterSelectDescribeProductBlockTaskEvent, AfterRemoveDescribeProductBlockTaskEvent>
    {
        public DescribeProductBlockTaskListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class NoteListViewModel : BaseListViewModel<Note, NoteLookup, AfterSaveNoteEvent, AfterSelectNoteEvent, AfterRemoveNoteEvent>
    {
        public NoteListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferUnitListViewModel : BaseListViewModel<OfferUnit, OfferUnitLookup, AfterSaveOfferUnitEvent, AfterSelectOfferUnitEvent, AfterRemoveOfferUnitEvent>
    {
        public OfferUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentConditionSetListViewModel : BaseListViewModel<PaymentConditionSet, PaymentConditionSetLookup, AfterSavePaymentConditionSetEvent, AfterSelectPaymentConditionSetEvent, AfterRemovePaymentConditionSetEvent>
    {
        public PaymentConditionSetListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductBlockListViewModel : BaseListViewModel<ProductBlock, ProductBlockLookup, AfterSaveProductBlockEvent, AfterSelectProductBlockEvent, AfterRemoveProductBlockEvent>
    {
        public ProductBlockListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductDependentListViewModel : BaseListViewModel<ProductDependent, ProductDependentLookup, AfterSaveProductDependentEvent, AfterSelectProductDependentEvent, AfterRemoveProductDependentEvent>
    {
        public ProductDependentListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductionTaskListViewModel : BaseListViewModel<ProductionTask, ProductionTaskLookup, AfterSaveProductionTaskEvent, AfterSelectProductionTaskEvent, AfterRemoveProductionTaskEvent>
    {
        public ProductionTaskListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SalesBlockListViewModel : BaseListViewModel<SalesBlock, SalesBlockLookup, AfterSaveSalesBlockEvent, AfterSelectSalesBlockEvent, AfterRemoveSalesBlockEvent>
    {
        public SalesBlockListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class BankDetailsListViewModel : BaseListViewModel<BankDetails, BankDetailsLookup, AfterSaveBankDetailsEvent, AfterSelectBankDetailsEvent, AfterRemoveBankDetailsEvent>
    {
        public BankDetailsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyListViewModel : BaseListViewModel<Company, CompanyLookup, AfterSaveCompanyEvent, AfterSelectCompanyEvent, AfterRemoveCompanyEvent>
    {
        public CompanyListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyFormListViewModel : BaseListViewModel<CompanyForm, CompanyFormLookup, AfterSaveCompanyFormEvent, AfterSelectCompanyFormEvent, AfterRemoveCompanyFormEvent>
    {
        public CompanyFormListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentsRegistrationDetailsListViewModel : BaseListViewModel<DocumentsRegistrationDetails, DocumentsRegistrationDetailsLookup, AfterSaveDocumentsRegistrationDetailsEvent, AfterSelectDocumentsRegistrationDetailsEvent, AfterRemoveDocumentsRegistrationDetailsEvent>
    {
        public DocumentsRegistrationDetailsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeesPositionListViewModel : BaseListViewModel<EmployeesPosition, EmployeesPositionLookup, AfterSaveEmployeesPositionEvent, AfterSelectEmployeesPositionEvent, AfterRemoveEmployeesPositionEvent>
    {
        public EmployeesPositionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityTypeListViewModel : BaseListViewModel<FacilityType, FacilityTypeLookup, AfterSaveFacilityTypeEvent, AfterSelectFacilityTypeEvent, AfterRemoveFacilityTypeEvent>
    {
        public FacilityTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ActivityFieldListViewModel : BaseListViewModel<ActivityField, ActivityFieldLookup, AfterSaveActivityFieldEvent, AfterSelectActivityFieldEvent, AfterRemoveActivityFieldEvent>
    {
        public ActivityFieldListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ContractListViewModel : BaseListViewModel<Contract, ContractLookup, AfterSaveContractEvent, AfterSelectContractEvent, AfterRemoveContractEvent>
    {
        public ContractListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class MeasureListViewModel : BaseListViewModel<Measure, MeasureLookup, AfterSaveMeasureEvent, AfterSelectMeasureEvent, AfterRemoveMeasureEvent>
    {
        public MeasureListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterListViewModel : BaseListViewModel<Parameter, ParameterLookup, AfterSaveParameterEvent, AfterSelectParameterEvent, AfterRemoveParameterEvent>
    {
        public ParameterListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterGroupListViewModel : BaseListViewModel<ParameterGroup, ParameterGroupLookup, AfterSaveParameterGroupEvent, AfterSelectParameterGroupEvent, AfterRemoveParameterGroupEvent>
    {
        public ParameterGroupListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductRelationListViewModel : BaseListViewModel<ProductRelation, ProductRelationLookup, AfterSaveProductRelationEvent, AfterSelectProductRelationEvent, AfterRemoveProductRelationEvent>
    {
        public ProductRelationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PersonListViewModel : BaseListViewModel<Person, PersonLookup, AfterSavePersonEvent, AfterSelectPersonEvent, AfterRemovePersonEvent>
    {
        public PersonListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentPlannedListListViewModel : BaseListViewModel<PaymentPlannedList, PaymentPlannedListLookup, AfterSavePaymentPlannedListEvent, AfterSelectPaymentPlannedListEvent, AfterRemovePaymentPlannedListEvent>
    {
        public PaymentPlannedListListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentPlannedListViewModel : BaseListViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent, AfterSelectPaymentPlannedEvent, AfterRemovePaymentPlannedEvent>
    {
        public PaymentPlannedListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentActualListViewModel : BaseListViewModel<PaymentActual, PaymentActualLookup, AfterSavePaymentActualEvent, AfterSelectPaymentActualEvent, AfterRemovePaymentActualEvent>
    {
        public PaymentActualListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterRelationListViewModel : BaseListViewModel<ParameterRelation, ParameterRelationLookup, AfterSaveParameterRelationEvent, AfterSelectParameterRelationEvent, AfterRemoveParameterRelationEvent>
    {
        public ParameterRelationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SalesUnitListViewModel : BaseListViewModel<SalesUnit, SalesUnitLookup, AfterSaveSalesUnitEvent, AfterSelectSalesUnitEvent, AfterRemoveSalesUnitEvent>
    {
        public SalesUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ServiceListViewModel : BaseListViewModel<Service, ServiceLookup, AfterSaveServiceEvent, AfterSelectServiceEvent, AfterRemoveServiceEvent>
    {
        public ServiceListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendAddressListViewModel : BaseListViewModel<TestFriendAddress, TestFriendAddressLookup, AfterSaveTestFriendAddressEvent, AfterSelectTestFriendAddressEvent, AfterRemoveTestFriendAddressEvent>
    {
        public TestFriendAddressListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendListViewModel : BaseListViewModel<TestFriend, TestFriendLookup, AfterSaveTestFriendEvent, AfterSelectTestFriendEvent, AfterRemoveTestFriendEvent>
    {
        public TestFriendListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendEmailListViewModel : BaseListViewModel<TestFriendEmail, TestFriendEmailLookup, AfterSaveTestFriendEmailEvent, AfterSelectTestFriendEmailEvent, AfterRemoveTestFriendEmailEvent>
    {
        public TestFriendEmailListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendGroupListViewModel : BaseListViewModel<TestFriendGroup, TestFriendGroupLookup, AfterSaveTestFriendGroupEvent, AfterSelectTestFriendGroupEvent, AfterRemoveTestFriendGroupEvent>
    {
        public TestFriendGroupListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentListViewModel : BaseListViewModel<Document, DocumentLookup, AfterSaveDocumentEvent, AfterSelectDocumentEvent, AfterRemoveDocumentEvent>
    {
        public DocumentListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestEntityListViewModel : BaseListViewModel<TestEntity, TestEntityLookup, AfterSaveTestEntityEvent, AfterSelectTestEntityEvent, AfterRemoveTestEntityEvent>
    {
        public TestEntityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestHusbandListViewModel : BaseListViewModel<TestHusband, TestHusbandLookup, AfterSaveTestHusbandEvent, AfterSelectTestHusbandEvent, AfterRemoveTestHusbandEvent>
    {
        public TestHusbandListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestWifeListViewModel : BaseListViewModel<TestWife, TestWifeLookup, AfterSaveTestWifeEvent, AfterSelectTestWifeEvent, AfterRemoveTestWifeEvent>
    {
        public TestWifeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestChildListViewModel : BaseListViewModel<TestChild, TestChildLookup, AfterSaveTestChildEvent, AfterSelectTestChildEvent, AfterRemoveTestChildEvent>
    {
        public TestChildListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SumOnDateListViewModel : BaseListViewModel<SumOnDate, SumOnDateLookup, AfterSaveSumOnDateEvent, AfterSelectSumOnDateEvent, AfterRemoveSumOnDateEvent>
    {
        public SumOnDateListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductListViewModel : BaseListViewModel<Product, ProductLookup, AfterSaveProductEvent, AfterSelectProductEvent, AfterRemoveProductEvent>
    {
        public ProductListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferListViewModel : BaseListViewModel<Offer, OfferLookup, AfterSaveOfferEvent, AfterSelectOfferEvent, AfterRemoveOfferEvent>
    {
        public OfferListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeeListViewModel : BaseListViewModel<Employee, EmployeeLookup, AfterSaveEmployeeEvent, AfterSelectEmployeeEvent, AfterRemoveEmployeeEvent>
    {
        public EmployeeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OrderListViewModel : BaseListViewModel<Order, OrderLookup, AfterSaveOrderEvent, AfterSelectOrderEvent, AfterRemoveOrderEvent>
    {
        public OrderListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentConditionListViewModel : BaseListViewModel<PaymentCondition, PaymentConditionLookup, AfterSavePaymentConditionEvent, AfterSelectPaymentConditionEvent, AfterRemovePaymentConditionEvent>
    {
        public PaymentConditionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentDocumentListViewModel : BaseListViewModel<PaymentDocument, PaymentDocumentLookup, AfterSavePaymentDocumentEvent, AfterSelectPaymentDocumentEvent, AfterRemovePaymentDocumentEvent>
    {
        public PaymentDocumentListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityListViewModel : BaseListViewModel<Facility, FacilityLookup, AfterSaveFacilityEvent, AfterSelectFacilityEvent, AfterRemoveFacilityEvent>
    {
        public FacilityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectListViewModel : BaseListViewModel<Project, ProjectLookup, AfterSaveProjectEvent, AfterSelectProjectEvent, AfterRemoveProjectEvent>
    {
        public ProjectListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserRoleListViewModel : BaseListViewModel<UserRole, UserRoleLookup, AfterSaveUserRoleEvent, AfterSelectUserRoleEvent, AfterRemoveUserRoleEvent>
    {
        public UserRoleListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SpecificationListViewModel : BaseListViewModel<Specification, SpecificationLookup, AfterSaveSpecificationEvent, AfterSelectSpecificationEvent, AfterRemoveSpecificationEvent>
    {
        public SpecificationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderListViewModel : BaseListViewModel<Tender, TenderLookup, AfterSaveTenderEvent, AfterSelectTenderEvent, AfterRemoveTenderEvent>
    {
        public TenderListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderTypeListViewModel : BaseListViewModel<TenderType, TenderTypeLookup, AfterSaveTenderTypeEvent, AfterSelectTenderTypeEvent, AfterRemoveTenderTypeEvent>
    {
        public TenderTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserListViewModel : BaseListViewModel<User, UserLookup, AfterSaveUserEvent, AfterSelectUserEvent, AfterRemoveUserEvent>
    {
        public UserListViewModel(IUnityContainer container) : base(container) { }
    }

}
