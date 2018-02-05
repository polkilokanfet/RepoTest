using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class AddressDetailsViewModel : BaseDetailsViewModel<AddressWrapper, Address, AfterSaveAddressEvent>
    {
        public AddressDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CountryDetailsViewModel : BaseDetailsViewModel<CountryWrapper, Country, AfterSaveCountryEvent>
    {
        public CountryDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DistrictDetailsViewModel : BaseDetailsViewModel<DistrictWrapper, District, AfterSaveDistrictEvent>
    {
        public DistrictDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityDetailsViewModel : BaseDetailsViewModel<LocalityWrapper, Locality, AfterSaveLocalityEvent>
    {
        public LocalityDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityTypeDetailsViewModel : BaseDetailsViewModel<LocalityTypeWrapper, LocalityType, AfterSaveLocalityTypeEvent>
    {
        public LocalityTypeDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class RegionDetailsViewModel : BaseDetailsViewModel<RegionWrapper, Region, AfterSaveRegionEvent>
    {
        public RegionDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class AdditionalSalesUnitsDetailsViewModel : BaseDetailsViewModel<AdditionalSalesUnitsWrapper, AdditionalSalesUnits, AfterSaveAdditionalSalesUnitsEvent>
    {
        public AdditionalSalesUnitsDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class BankDetailsDetailsViewModel : BaseDetailsViewModel<BankDetailsWrapper, BankDetails, AfterSaveBankDetailsEvent>
    {
        public BankDetailsDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyDetailsViewModel : BaseDetailsViewModel<CompanyWrapper, Company, AfterSaveCompanyEvent>
    {
        public CompanyDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper, CompanyForm, AfterSaveCompanyFormEvent>
    {
        public CompanyFormDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentsRegistrationDetailsDetailsViewModel : BaseDetailsViewModel<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails, AfterSaveDocumentsRegistrationDetailsEvent>
    {
        public DocumentsRegistrationDetailsDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeesPositionDetailsViewModel : BaseDetailsViewModel<EmployeesPositionWrapper, EmployeesPosition, AfterSaveEmployeesPositionEvent>
    {
        public EmployeesPositionDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper, FacilityType, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypeDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ActivityFieldDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField, AfterSaveActivityFieldEvent>
    {
        public ActivityFieldDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper, Contract, AfterSaveContractEvent>
    {
        public ContractDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class MeasureDetailsViewModel : BaseDetailsViewModel<MeasureWrapper, Measure, AfterSaveMeasureEvent>
    {
        public MeasureDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper, Parameter, AfterSaveParameterEvent>
    {
        public ParameterDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterGroupDetailsViewModel : BaseDetailsViewModel<ParameterGroupWrapper, ParameterGroup, AfterSaveParameterGroupEvent>
    {
        public ParameterGroupDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductRelationDetailsViewModel : BaseDetailsViewModel<ProductRelationWrapper, ProductRelation, AfterSaveProductRelationEvent>
    {
        public ProductRelationDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class StandartPaymentConditionsDetailsViewModel : BaseDetailsViewModel<StandartPaymentConditionsWrapper, StandartPaymentConditions, AfterSaveStandartPaymentConditionsEvent>
    {
        public StandartPaymentConditionsDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PersonDetailsViewModel : BaseDetailsViewModel<PersonWrapper, Person, AfterSavePersonEvent>
    {
        public PersonDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentPlannedDetailsViewModel : BaseDetailsViewModel<PaymentPlannedWrapper, PaymentPlanned, AfterSavePaymentPlannedEvent>
    {
        public PaymentPlannedDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentActualDetailsViewModel : BaseDetailsViewModel<PaymentActualWrapper, PaymentActual, AfterSavePaymentActualEvent>
    {
        public PaymentActualDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterRelationDetailsViewModel : BaseDetailsViewModel<ParameterRelationWrapper, ParameterRelation, AfterSaveParameterRelationEvent>
    {
        public ParameterRelationDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectUnitDetailsViewModel : BaseDetailsViewModel<ProjectUnitWrapper, ProjectUnit, AfterSaveProjectUnitEvent>
    {
        public ProjectUnitDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ShipmentUnitDetailsViewModel : BaseDetailsViewModel<ShipmentUnitWrapper, ShipmentUnit, AfterSaveShipmentUnitEvent>
    {
        public ShipmentUnitDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductionUnitDetailsViewModel : BaseDetailsViewModel<ProductionUnitWrapper, ProductionUnit, AfterSaveProductionUnitEvent>
    {
        public ProductionUnitDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SalesUnitDetailsViewModel : BaseDetailsViewModel<SalesUnitWrapper, SalesUnit, AfterSaveSalesUnitEvent>
    {
        public SalesUnitDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendAddressDetailsViewModel : BaseDetailsViewModel<TestFriendAddressWrapper, TestFriendAddress, AfterSaveTestFriendAddressEvent>
    {
        public TestFriendAddressDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendDetailsViewModel : BaseDetailsViewModel<TestFriendWrapper, TestFriend, AfterSaveTestFriendEvent>
    {
        public TestFriendDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendEmailDetailsViewModel : BaseDetailsViewModel<TestFriendEmailWrapper, TestFriendEmail, AfterSaveTestFriendEmailEvent>
    {
        public TestFriendEmailDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendGroupDetailsViewModel : BaseDetailsViewModel<TestFriendGroupWrapper, TestFriendGroup, AfterSaveTestFriendGroupEvent>
    {
        public TestFriendGroupDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentDetailsViewModel : BaseDetailsViewModel<DocumentWrapper, Document, AfterSaveDocumentEvent>
    {
        public DocumentDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestEntityDetailsViewModel : BaseDetailsViewModel<TestEntityWrapper, TestEntity, AfterSaveTestEntityEvent>
    {
        public TestEntityDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestHusbandDetailsViewModel : BaseDetailsViewModel<TestHusbandWrapper, TestHusband, AfterSaveTestHusbandEvent>
    {
        public TestHusbandDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestWifeDetailsViewModel : BaseDetailsViewModel<TestWifeWrapper, TestWife, AfterSaveTestWifeEvent>
    {
        public TestWifeDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestChildDetailsViewModel : BaseDetailsViewModel<TestChildWrapper, TestChild, AfterSaveTestChildEvent>
    {
        public TestChildDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CostOnDateDetailsViewModel : BaseDetailsViewModel<CostOnDateWrapper, CostOnDate, AfterSaveCostOnDateEvent>
    {
        public CostOnDateDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CostDetailsViewModel : BaseDetailsViewModel<CostWrapper, Cost, AfterSaveCostEvent>
    {
        public CostDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CurrencyDetailsViewModel : BaseDetailsViewModel<CurrencyWrapper, Currency, AfterSaveCurrencyEvent>
    {
        public CurrencyDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ExchangeCurrencyRateDetailsViewModel : BaseDetailsViewModel<ExchangeCurrencyRateWrapper, ExchangeCurrencyRate, AfterSaveExchangeCurrencyRateEvent>
    {
        public ExchangeCurrencyRateDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product, AfterSaveProductEvent>
    {
        public ProductDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferDetailsViewModel : BaseDetailsViewModel<OfferWrapper, Offer, AfterSaveOfferEvent>
    {
        public OfferDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeeDetailsViewModel : BaseDetailsViewModel<EmployeeWrapper, Employee, AfterSaveEmployeeEvent>
    {
        public EmployeeDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OrderDetailsViewModel : BaseDetailsViewModel<OrderWrapper, Order, AfterSaveOrderEvent>
    {
        public OrderDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentConditionDetailsViewModel : BaseDetailsViewModel<PaymentConditionWrapper, PaymentCondition, AfterSavePaymentConditionEvent>
    {
        public PaymentConditionDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentDocumentDetailsViewModel : BaseDetailsViewModel<PaymentDocumentWrapper, PaymentDocument, AfterSavePaymentDocumentEvent>
    {
        public PaymentDocumentDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityDetailsViewModel : BaseDetailsViewModel<FacilityWrapper, Facility, AfterSaveFacilityEvent>
    {
        public FacilityDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper, Project, AfterSaveProjectEvent>
    {
        public ProjectDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserRoleDetailsViewModel : BaseDetailsViewModel<UserRoleWrapper, UserRole, AfterSaveUserRoleEvent>
    {
        public UserRoleDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SpecificationDetailsViewModel : BaseDetailsViewModel<SpecificationWrapper, Specification, AfterSaveSpecificationEvent>
    {
        public SpecificationDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper, Tender, AfterSaveTenderEvent>
    {
        public TenderDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderTypeDetailsViewModel : BaseDetailsViewModel<TenderTypeWrapper, TenderType, AfterSaveTenderTypeEvent>
    {
        public TenderTypeDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserDetailsViewModel : BaseDetailsViewModel<UserWrapper, User, AfterSaveUserEvent>
    {
        public UserDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferUnitDetailsViewModel : BaseDetailsViewModel<OfferUnitWrapper, OfferUnit, AfterSaveOfferUnitEvent>
    {
        public OfferUnitDetailsViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectUnitGroupDetailsViewModel : BaseDetailsViewModel<ProjectUnitGroupWrapper, ProjectUnitGroup, AfterSaveProjectUnitGroupEvent>
    {
        public ProjectUnitGroupDetailsViewModel(IUnityContainer container) : base(container) { }
    }

}
