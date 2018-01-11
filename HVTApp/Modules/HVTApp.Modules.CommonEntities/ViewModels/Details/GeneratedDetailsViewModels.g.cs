using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class AddressDetailsViewModel : BaseDetailsViewModel<AddressWrapper, Address, AfterSaveAddressEvent>
    {
        public AddressDetailsViewModel(IUnityContainer container, AddressWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class CountryDetailsViewModel : BaseDetailsViewModel<CountryWrapper, Country, AfterSaveCountryEvent>
    {
        public CountryDetailsViewModel(IUnityContainer container, CountryWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class DistrictDetailsViewModel : BaseDetailsViewModel<DistrictWrapper, District, AfterSaveDistrictEvent>
    {
        public DistrictDetailsViewModel(IUnityContainer container, DistrictWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class LocalityDetailsViewModel : BaseDetailsViewModel<LocalityWrapper, Locality, AfterSaveLocalityEvent>
    {
        public LocalityDetailsViewModel(IUnityContainer container, LocalityWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class LocalityTypeDetailsViewModel : BaseDetailsViewModel<LocalityTypeWrapper, LocalityType, AfterSaveLocalityTypeEvent>
    {
        public LocalityTypeDetailsViewModel(IUnityContainer container, LocalityTypeWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class RegionDetailsViewModel : BaseDetailsViewModel<RegionWrapper, Region, AfterSaveRegionEvent>
    {
        public RegionDetailsViewModel(IUnityContainer container, RegionWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class AdditionalSalesUnitsDetailsViewModel : BaseDetailsViewModel<AdditionalSalesUnitsWrapper, AdditionalSalesUnits, AfterSaveAdditionalSalesUnitsEvent>
    {
        public AdditionalSalesUnitsDetailsViewModel(IUnityContainer container, AdditionalSalesUnitsWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class BankDetailsDetailsViewModel : BaseDetailsViewModel<BankDetailsWrapper, BankDetails, AfterSaveBankDetailsEvent>
    {
        public BankDetailsDetailsViewModel(IUnityContainer container, BankDetailsWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class CompanyDetailsViewModel : BaseDetailsViewModel<CompanyWrapper, Company, AfterSaveCompanyEvent>
    {
        public CompanyDetailsViewModel(IUnityContainer container, CompanyWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper, CompanyForm, AfterSaveCompanyFormEvent>
    {
        public CompanyFormDetailsViewModel(IUnityContainer container, CompanyFormWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class DocumentsRegistrationDetailsDetailsViewModel : BaseDetailsViewModel<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails, AfterSaveDocumentsRegistrationDetailsEvent>
    {
        public DocumentsRegistrationDetailsDetailsViewModel(IUnityContainer container, DocumentsRegistrationDetailsWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class EmployeesPositionDetailsViewModel : BaseDetailsViewModel<EmployeesPositionWrapper, EmployeesPosition, AfterSaveEmployeesPositionEvent>
    {
        public EmployeesPositionDetailsViewModel(IUnityContainer container, EmployeesPositionWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper, FacilityType, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypeDetailsViewModel(IUnityContainer container, FacilityTypeWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ActivityFieldDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField, AfterSaveActivityFieldEvent>
    {
        public ActivityFieldDetailsViewModel(IUnityContainer container, ActivityFieldWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper, Contract, AfterSaveContractEvent>
    {
        public ContractDetailsViewModel(IUnityContainer container, ContractWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class MeasureDetailsViewModel : BaseDetailsViewModel<MeasureWrapper, Measure, AfterSaveMeasureEvent>
    {
        public MeasureDetailsViewModel(IUnityContainer container, MeasureWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper, Parameter, AfterSaveParameterEvent>
    {
        public ParameterDetailsViewModel(IUnityContainer container, ParameterWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ParameterGroupDetailsViewModel : BaseDetailsViewModel<ParameterGroupWrapper, ParameterGroup, AfterSaveParameterGroupEvent>
    {
        public ParameterGroupDetailsViewModel(IUnityContainer container, ParameterGroupWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ProductRelationDetailsViewModel : BaseDetailsViewModel<ProductRelationWrapper, ProductRelation, AfterSaveProductRelationEvent>
    {
        public ProductRelationDetailsViewModel(IUnityContainer container, ProductRelationWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class StandartPaymentConditionsDetailsViewModel : BaseDetailsViewModel<StandartPaymentConditionsWrapper, StandartPaymentConditions, AfterSaveStandartPaymentConditionsEvent>
    {
        public StandartPaymentConditionsDetailsViewModel(IUnityContainer container, StandartPaymentConditionsWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class PersonDetailsViewModel : BaseDetailsViewModel<PersonWrapper, Person, AfterSavePersonEvent>
    {
        public PersonDetailsViewModel(IUnityContainer container, PersonWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class PaymentPlannedDetailsViewModel : BaseDetailsViewModel<PaymentPlannedWrapper, PaymentPlanned, AfterSavePaymentPlannedEvent>
    {
        public PaymentPlannedDetailsViewModel(IUnityContainer container, PaymentPlannedWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class PaymentActualDetailsViewModel : BaseDetailsViewModel<PaymentActualWrapper, PaymentActual, AfterSavePaymentActualEvent>
    {
        public PaymentActualDetailsViewModel(IUnityContainer container, PaymentActualWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ParameterRelationDetailsViewModel : BaseDetailsViewModel<ParameterRelationWrapper, ParameterRelation, AfterSaveParameterRelationEvent>
    {
        public ParameterRelationDetailsViewModel(IUnityContainer container, ParameterRelationWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ProjectUnitDetailsViewModel : BaseDetailsViewModel<ProjectUnitWrapper, ProjectUnit, AfterSaveProjectUnitEvent>
    {
        public ProjectUnitDetailsViewModel(IUnityContainer container, ProjectUnitWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TenderUnitDetailsViewModel : BaseDetailsViewModel<TenderUnitWrapper, TenderUnit, AfterSaveTenderUnitEvent>
    {
        public TenderUnitDetailsViewModel(IUnityContainer container, TenderUnitWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ShipmentUnitDetailsViewModel : BaseDetailsViewModel<ShipmentUnitWrapper, ShipmentUnit, AfterSaveShipmentUnitEvent>
    {
        public ShipmentUnitDetailsViewModel(IUnityContainer container, ShipmentUnitWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ProductionUnitDetailsViewModel : BaseDetailsViewModel<ProductionUnitWrapper, ProductionUnit, AfterSaveProductionUnitEvent>
    {
        public ProductionUnitDetailsViewModel(IUnityContainer container, ProductionUnitWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class SalesUnitDetailsViewModel : BaseDetailsViewModel<SalesUnitWrapper, SalesUnit, AfterSaveSalesUnitEvent>
    {
        public SalesUnitDetailsViewModel(IUnityContainer container, SalesUnitWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TestFriendAddressDetailsViewModel : BaseDetailsViewModel<TestFriendAddressWrapper, TestFriendAddress, AfterSaveTestFriendAddressEvent>
    {
        public TestFriendAddressDetailsViewModel(IUnityContainer container, TestFriendAddressWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TestFriendDetailsViewModel : BaseDetailsViewModel<TestFriendWrapper, TestFriend, AfterSaveTestFriendEvent>
    {
        public TestFriendDetailsViewModel(IUnityContainer container, TestFriendWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TestFriendEmailDetailsViewModel : BaseDetailsViewModel<TestFriendEmailWrapper, TestFriendEmail, AfterSaveTestFriendEmailEvent>
    {
        public TestFriendEmailDetailsViewModel(IUnityContainer container, TestFriendEmailWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TestFriendGroupDetailsViewModel : BaseDetailsViewModel<TestFriendGroupWrapper, TestFriendGroup, AfterSaveTestFriendGroupEvent>
    {
        public TestFriendGroupDetailsViewModel(IUnityContainer container, TestFriendGroupWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class DocumentDetailsViewModel : BaseDetailsViewModel<DocumentWrapper, Document, AfterSaveDocumentEvent>
    {
        public DocumentDetailsViewModel(IUnityContainer container, DocumentWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TestEntityDetailsViewModel : BaseDetailsViewModel<TestEntityWrapper, TestEntity, AfterSaveTestEntityEvent>
    {
        public TestEntityDetailsViewModel(IUnityContainer container, TestEntityWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TestHusbandDetailsViewModel : BaseDetailsViewModel<TestHusbandWrapper, TestHusband, AfterSaveTestHusbandEvent>
    {
        public TestHusbandDetailsViewModel(IUnityContainer container, TestHusbandWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TestWifeDetailsViewModel : BaseDetailsViewModel<TestWifeWrapper, TestWife, AfterSaveTestWifeEvent>
    {
        public TestWifeDetailsViewModel(IUnityContainer container, TestWifeWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TestChildDetailsViewModel : BaseDetailsViewModel<TestChildWrapper, TestChild, AfterSaveTestChildEvent>
    {
        public TestChildDetailsViewModel(IUnityContainer container, TestChildWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class CostOnDateDetailsViewModel : BaseDetailsViewModel<CostOnDateWrapper, CostOnDate, AfterSaveCostOnDateEvent>
    {
        public CostOnDateDetailsViewModel(IUnityContainer container, CostOnDateWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class CostDetailsViewModel : BaseDetailsViewModel<CostWrapper, Cost, AfterSaveCostEvent>
    {
        public CostDetailsViewModel(IUnityContainer container, CostWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class CurrencyDetailsViewModel : BaseDetailsViewModel<CurrencyWrapper, Currency, AfterSaveCurrencyEvent>
    {
        public CurrencyDetailsViewModel(IUnityContainer container, CurrencyWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ExchangeCurrencyRateDetailsViewModel : BaseDetailsViewModel<ExchangeCurrencyRateWrapper, ExchangeCurrencyRate, AfterSaveExchangeCurrencyRateEvent>
    {
        public ExchangeCurrencyRateDetailsViewModel(IUnityContainer container, ExchangeCurrencyRateWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product, AfterSaveProductEvent>
    {
        public ProductDetailsViewModel(IUnityContainer container, ProductWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class OfferDetailsViewModel : BaseDetailsViewModel<OfferWrapper, Offer, AfterSaveOfferEvent>
    {
        public OfferDetailsViewModel(IUnityContainer container, OfferWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class EmployeeDetailsViewModel : BaseDetailsViewModel<EmployeeWrapper, Employee, AfterSaveEmployeeEvent>
    {
        public EmployeeDetailsViewModel(IUnityContainer container, EmployeeWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class OrderDetailsViewModel : BaseDetailsViewModel<OrderWrapper, Order, AfterSaveOrderEvent>
    {
        public OrderDetailsViewModel(IUnityContainer container, OrderWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class PaymentConditionDetailsViewModel : BaseDetailsViewModel<PaymentConditionWrapper, PaymentCondition, AfterSavePaymentConditionEvent>
    {
        public PaymentConditionDetailsViewModel(IUnityContainer container, PaymentConditionWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class PaymentDocumentDetailsViewModel : BaseDetailsViewModel<PaymentDocumentWrapper, PaymentDocument, AfterSavePaymentDocumentEvent>
    {
        public PaymentDocumentDetailsViewModel(IUnityContainer container, PaymentDocumentWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class FacilityDetailsViewModel : BaseDetailsViewModel<FacilityWrapper, Facility, AfterSaveFacilityEvent>
    {
        public FacilityDetailsViewModel(IUnityContainer container, FacilityWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper, Project, AfterSaveProjectEvent>
    {
        public ProjectDetailsViewModel(IUnityContainer container, ProjectWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class UserRoleDetailsViewModel : BaseDetailsViewModel<UserRoleWrapper, UserRole, AfterSaveUserRoleEvent>
    {
        public UserRoleDetailsViewModel(IUnityContainer container, UserRoleWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class SpecificationDetailsViewModel : BaseDetailsViewModel<SpecificationWrapper, Specification, AfterSaveSpecificationEvent>
    {
        public SpecificationDetailsViewModel(IUnityContainer container, SpecificationWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper, Tender, AfterSaveTenderEvent>
    {
        public TenderDetailsViewModel(IUnityContainer container, TenderWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class TenderTypeDetailsViewModel : BaseDetailsViewModel<TenderTypeWrapper, TenderType, AfterSaveTenderTypeEvent>
    {
        public TenderTypeDetailsViewModel(IUnityContainer container, TenderTypeWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class UserDetailsViewModel : BaseDetailsViewModel<UserWrapper, User, AfterSaveUserEvent>
    {
        public UserDetailsViewModel(IUnityContainer container, UserWrapper wrapper = null) : base(container, wrapper) { }
    }

    public partial class OfferUnitDetailsViewModel : BaseDetailsViewModel<OfferUnitWrapper, OfferUnit, AfterSaveOfferUnitEvent>
    {
        public OfferUnitDetailsViewModel(IUnityContainer container, OfferUnitWrapper wrapper = null) : base(container, wrapper) { }
    }

}
