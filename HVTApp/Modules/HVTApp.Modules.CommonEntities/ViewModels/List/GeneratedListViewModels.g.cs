using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class AddressListViewModel : BaseWrapperListViewModel<AddressWrapper, Address, AddressDetailsViewModel, AfterSaveAddressEvent>
    {
        public AddressListViewModel(IUnityContainer container, AddressWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class CountryListViewModel : BaseWrapperListViewModel<CountryWrapper, Country, CountryDetailsViewModel, AfterSaveCountryEvent>
    {
        public CountryListViewModel(IUnityContainer container, CountryWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class DistrictListViewModel : BaseWrapperListViewModel<DistrictWrapper, District, DistrictDetailsViewModel, AfterSaveDistrictEvent>
    {
        public DistrictListViewModel(IUnityContainer container, DistrictWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class LocalityListViewModel : BaseWrapperListViewModel<LocalityWrapper, Locality, LocalityDetailsViewModel, AfterSaveLocalityEvent>
    {
        public LocalityListViewModel(IUnityContainer container, LocalityWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class LocalityTypeListViewModel : BaseWrapperListViewModel<LocalityTypeWrapper, LocalityType, LocalityTypeDetailsViewModel, AfterSaveLocalityTypeEvent>
    {
        public LocalityTypeListViewModel(IUnityContainer container, LocalityTypeWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class RegionListViewModel : BaseWrapperListViewModel<RegionWrapper, Region, RegionDetailsViewModel, AfterSaveRegionEvent>
    {
        public RegionListViewModel(IUnityContainer container, RegionWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class AdditionalSalesUnitsListViewModel : BaseWrapperListViewModel<AdditionalSalesUnitsWrapper, AdditionalSalesUnits, AdditionalSalesUnitsDetailsViewModel, AfterSaveAdditionalSalesUnitsEvent>
    {
        public AdditionalSalesUnitsListViewModel(IUnityContainer container, AdditionalSalesUnitsWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class BankDetailsListViewModel : BaseWrapperListViewModel<BankDetailsWrapper, BankDetails, BankDetailsDetailsViewModel, AfterSaveBankDetailsEvent>
    {
        public BankDetailsListViewModel(IUnityContainer container, BankDetailsWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class CompanyListViewModel : BaseWrapperListViewModel<CompanyWrapper, Company, CompanyDetailsViewModel, AfterSaveCompanyEvent>
    {
        public CompanyListViewModel(IUnityContainer container, CompanyWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class CompanyFormListViewModel : BaseWrapperListViewModel<CompanyFormWrapper, CompanyForm, CompanyFormDetailsViewModel, AfterSaveCompanyFormEvent>
    {
        public CompanyFormListViewModel(IUnityContainer container, CompanyFormWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class DocumentsRegistrationDetailsListViewModel : BaseWrapperListViewModel<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails, DocumentsRegistrationDetailsDetailsViewModel, AfterSaveDocumentsRegistrationDetailsEvent>
    {
        public DocumentsRegistrationDetailsListViewModel(IUnityContainer container, DocumentsRegistrationDetailsWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class EmployeesPositionListViewModel : BaseWrapperListViewModel<EmployeesPositionWrapper, EmployeesPosition, EmployeesPositionDetailsViewModel, AfterSaveEmployeesPositionEvent>
    {
        public EmployeesPositionListViewModel(IUnityContainer container, EmployeesPositionWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class FacilityTypeListViewModel : BaseWrapperListViewModel<FacilityTypeWrapper, FacilityType, FacilityTypeDetailsViewModel, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypeListViewModel(IUnityContainer container, FacilityTypeWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ActivityFieldListViewModel : BaseWrapperListViewModel<ActivityFieldWrapper, ActivityField, ActivityFieldDetailsViewModel, AfterSaveActivityFieldEvent>
    {
        public ActivityFieldListViewModel(IUnityContainer container, ActivityFieldWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ContractListViewModel : BaseWrapperListViewModel<ContractWrapper, Contract, ContractDetailsViewModel, AfterSaveContractEvent>
    {
        public ContractListViewModel(IUnityContainer container, ContractWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class MeasureListViewModel : BaseWrapperListViewModel<MeasureWrapper, Measure, MeasureDetailsViewModel, AfterSaveMeasureEvent>
    {
        public MeasureListViewModel(IUnityContainer container, MeasureWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ParameterListViewModel : BaseWrapperListViewModel<ParameterWrapper, Parameter, ParameterDetailsViewModel, AfterSaveParameterEvent>
    {
        public ParameterListViewModel(IUnityContainer container, ParameterWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ParameterGroupListViewModel : BaseWrapperListViewModel<ParameterGroupWrapper, ParameterGroup, ParameterGroupDetailsViewModel, AfterSaveParameterGroupEvent>
    {
        public ParameterGroupListViewModel(IUnityContainer container, ParameterGroupWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class PartListViewModel : BaseWrapperListViewModel<PartWrapper, Part, PartDetailsViewModel, AfterSavePartEvent>
    {
        public PartListViewModel(IUnityContainer container, PartWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ProductsRelationListViewModel : BaseWrapperListViewModel<ProductsRelationWrapper, ProductsRelation, ProductsRelationDetailsViewModel, AfterSaveProductsRelationEvent>
    {
        public ProductsRelationListViewModel(IUnityContainer container, ProductsRelationWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class StandartPaymentConditionsListViewModel : BaseWrapperListViewModel<StandartPaymentConditionsWrapper, StandartPaymentConditions, StandartPaymentConditionsDetailsViewModel, AfterSaveStandartPaymentConditionsEvent>
    {
        public StandartPaymentConditionsListViewModel(IUnityContainer container, StandartPaymentConditionsWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class PersonListViewModel : BaseWrapperListViewModel<PersonWrapper, Person, PersonDetailsViewModel, AfterSavePersonEvent>
    {
        public PersonListViewModel(IUnityContainer container, PersonWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class PaymentPlannedListViewModel : BaseWrapperListViewModel<PaymentPlannedWrapper, PaymentPlanned, PaymentPlannedDetailsViewModel, AfterSavePaymentPlannedEvent>
    {
        public PaymentPlannedListViewModel(IUnityContainer container, PaymentPlannedWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class PaymentActualListViewModel : BaseWrapperListViewModel<PaymentActualWrapper, PaymentActual, PaymentActualDetailsViewModel, AfterSavePaymentActualEvent>
    {
        public PaymentActualListViewModel(IUnityContainer container, PaymentActualWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class RequiredPreviousParametersListViewModel : BaseWrapperListViewModel<RequiredPreviousParametersWrapper, RequiredPreviousParameters, RequiredPreviousParametersDetailsViewModel, AfterSaveRequiredPreviousParametersEvent>
    {
        public RequiredPreviousParametersListViewModel(IUnityContainer container, RequiredPreviousParametersWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ProjectUnitListViewModel : BaseWrapperListViewModel<ProjectUnitWrapper, ProjectUnit, ProjectUnitDetailsViewModel, AfterSaveProjectUnitEvent>
    {
        public ProjectUnitListViewModel(IUnityContainer container, ProjectUnitWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TenderUnitListViewModel : BaseWrapperListViewModel<TenderUnitWrapper, TenderUnit, TenderUnitDetailsViewModel, AfterSaveTenderUnitEvent>
    {
        public TenderUnitListViewModel(IUnityContainer container, TenderUnitWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ShipmentUnitListViewModel : BaseWrapperListViewModel<ShipmentUnitWrapper, ShipmentUnit, ShipmentUnitDetailsViewModel, AfterSaveShipmentUnitEvent>
    {
        public ShipmentUnitListViewModel(IUnityContainer container, ShipmentUnitWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ProductionUnitListViewModel : BaseWrapperListViewModel<ProductionUnitWrapper, ProductionUnit, ProductionUnitDetailsViewModel, AfterSaveProductionUnitEvent>
    {
        public ProductionUnitListViewModel(IUnityContainer container, ProductionUnitWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class SalesUnitListViewModel : BaseWrapperListViewModel<SalesUnitWrapper, SalesUnit, SalesUnitDetailsViewModel, AfterSaveSalesUnitEvent>
    {
        public SalesUnitListViewModel(IUnityContainer container, SalesUnitWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TestFriendAddressListViewModel : BaseWrapperListViewModel<TestFriendAddressWrapper, TestFriendAddress, TestFriendAddressDetailsViewModel, AfterSaveTestFriendAddressEvent>
    {
        public TestFriendAddressListViewModel(IUnityContainer container, TestFriendAddressWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TestFriendListViewModel : BaseWrapperListViewModel<TestFriendWrapper, TestFriend, TestFriendDetailsViewModel, AfterSaveTestFriendEvent>
    {
        public TestFriendListViewModel(IUnityContainer container, TestFriendWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TestFriendEmailListViewModel : BaseWrapperListViewModel<TestFriendEmailWrapper, TestFriendEmail, TestFriendEmailDetailsViewModel, AfterSaveTestFriendEmailEvent>
    {
        public TestFriendEmailListViewModel(IUnityContainer container, TestFriendEmailWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TestFriendGroupListViewModel : BaseWrapperListViewModel<TestFriendGroupWrapper, TestFriendGroup, TestFriendGroupDetailsViewModel, AfterSaveTestFriendGroupEvent>
    {
        public TestFriendGroupListViewModel(IUnityContainer container, TestFriendGroupWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class DocumentListViewModel : BaseWrapperListViewModel<DocumentWrapper, Document, DocumentDetailsViewModel, AfterSaveDocumentEvent>
    {
        public DocumentListViewModel(IUnityContainer container, DocumentWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TestEntityListViewModel : BaseWrapperListViewModel<TestEntityWrapper, TestEntity, TestEntityDetailsViewModel, AfterSaveTestEntityEvent>
    {
        public TestEntityListViewModel(IUnityContainer container, TestEntityWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TestHusbandListViewModel : BaseWrapperListViewModel<TestHusbandWrapper, TestHusband, TestHusbandDetailsViewModel, AfterSaveTestHusbandEvent>
    {
        public TestHusbandListViewModel(IUnityContainer container, TestHusbandWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TestWifeListViewModel : BaseWrapperListViewModel<TestWifeWrapper, TestWife, TestWifeDetailsViewModel, AfterSaveTestWifeEvent>
    {
        public TestWifeListViewModel(IUnityContainer container, TestWifeWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TestChildListViewModel : BaseWrapperListViewModel<TestChildWrapper, TestChild, TestChildDetailsViewModel, AfterSaveTestChildEvent>
    {
        public TestChildListViewModel(IUnityContainer container, TestChildWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class CostOnDateListViewModel : BaseWrapperListViewModel<CostOnDateWrapper, CostOnDate, CostOnDateDetailsViewModel, AfterSaveCostOnDateEvent>
    {
        public CostOnDateListViewModel(IUnityContainer container, CostOnDateWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class CostListViewModel : BaseWrapperListViewModel<CostWrapper, Cost, CostDetailsViewModel, AfterSaveCostEvent>
    {
        public CostListViewModel(IUnityContainer container, CostWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class CurrencyListViewModel : BaseWrapperListViewModel<CurrencyWrapper, Currency, CurrencyDetailsViewModel, AfterSaveCurrencyEvent>
    {
        public CurrencyListViewModel(IUnityContainer container, CurrencyWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ExchangeCurrencyRateListViewModel : BaseWrapperListViewModel<ExchangeCurrencyRateWrapper, ExchangeCurrencyRate, ExchangeCurrencyRateDetailsViewModel, AfterSaveExchangeCurrencyRateEvent>
    {
        public ExchangeCurrencyRateListViewModel(IUnityContainer container, ExchangeCurrencyRateWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ProductListViewModel : BaseWrapperListViewModel<ProductWrapper, Product, ProductDetailsViewModel, AfterSaveProductEvent>
    {
        public ProductListViewModel(IUnityContainer container, ProductWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class OfferListViewModel : BaseWrapperListViewModel<OfferWrapper, Offer, OfferDetailsViewModel, AfterSaveOfferEvent>
    {
        public OfferListViewModel(IUnityContainer container, OfferWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class EmployeeListViewModel : BaseWrapperListViewModel<EmployeeWrapper, Employee, EmployeeDetailsViewModel, AfterSaveEmployeeEvent>
    {
        public EmployeeListViewModel(IUnityContainer container, EmployeeWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class OrderListViewModel : BaseWrapperListViewModel<OrderWrapper, Order, OrderDetailsViewModel, AfterSaveOrderEvent>
    {
        public OrderListViewModel(IUnityContainer container, OrderWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class PaymentConditionListViewModel : BaseWrapperListViewModel<PaymentConditionWrapper, PaymentCondition, PaymentConditionDetailsViewModel, AfterSavePaymentConditionEvent>
    {
        public PaymentConditionListViewModel(IUnityContainer container, PaymentConditionWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class PaymentDocumentListViewModel : BaseWrapperListViewModel<PaymentDocumentWrapper, PaymentDocument, PaymentDocumentDetailsViewModel, AfterSavePaymentDocumentEvent>
    {
        public PaymentDocumentListViewModel(IUnityContainer container, PaymentDocumentWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class FacilityListViewModel : BaseWrapperListViewModel<FacilityWrapper, Facility, FacilityDetailsViewModel, AfterSaveFacilityEvent>
    {
        public FacilityListViewModel(IUnityContainer container, FacilityWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class ProjectListViewModel : BaseWrapperListViewModel<ProjectWrapper, Project, ProjectDetailsViewModel, AfterSaveProjectEvent>
    {
        public ProjectListViewModel(IUnityContainer container, ProjectWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class UserRoleListViewModel : BaseWrapperListViewModel<UserRoleWrapper, UserRole, UserRoleDetailsViewModel, AfterSaveUserRoleEvent>
    {
        public UserRoleListViewModel(IUnityContainer container, UserRoleWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class SpecificationListViewModel : BaseWrapperListViewModel<SpecificationWrapper, Specification, SpecificationDetailsViewModel, AfterSaveSpecificationEvent>
    {
        public SpecificationListViewModel(IUnityContainer container, SpecificationWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TenderListViewModel : BaseWrapperListViewModel<TenderWrapper, Tender, TenderDetailsViewModel, AfterSaveTenderEvent>
    {
        public TenderListViewModel(IUnityContainer container, TenderWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class TenderTypeListViewModel : BaseWrapperListViewModel<TenderTypeWrapper, TenderType, TenderTypeDetailsViewModel, AfterSaveTenderTypeEvent>
    {
        public TenderTypeListViewModel(IUnityContainer container, TenderTypeWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class UserListViewModel : BaseWrapperListViewModel<UserWrapper, User, UserDetailsViewModel, AfterSaveUserEvent>
    {
        public UserListViewModel(IUnityContainer container, UserWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

    public partial class OfferUnitListViewModel : BaseWrapperListViewModel<OfferUnitWrapper, OfferUnit, OfferUnitDetailsViewModel, AfterSaveOfferUnitEvent>
    {
        public OfferUnitListViewModel(IUnityContainer container, OfferUnitWrapperDataService wrapperDataService) : base(container, wrapperDataService) { }
    }

}
