using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public partial class AddressListViewModel : BaseListViewModel<Address, AddressLookup, AfterSaveAddressEvent>
    {
        public AddressListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CountryListViewModel : BaseListViewModel<Country, CountryLookup, AfterSaveCountryEvent>
    {
        public CountryListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DistrictListViewModel : BaseListViewModel<District, DistrictLookup, AfterSaveDistrictEvent>
    {
        public DistrictListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityListViewModel : BaseListViewModel<Locality, LocalityLookup, AfterSaveLocalityEvent>
    {
        public LocalityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityTypeListViewModel : BaseListViewModel<LocalityType, LocalityTypeLookup, AfterSaveLocalityTypeEvent>
    {
        public LocalityTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class RegionListViewModel : BaseListViewModel<Region, RegionLookup, AfterSaveRegionEvent>
    {
        public RegionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class AdditionalSalesUnitsListViewModel : BaseListViewModel<AdditionalSalesUnits, AdditionalSalesUnitsLookup, AfterSaveAdditionalSalesUnitsEvent>
    {
        public AdditionalSalesUnitsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class BankDetailsListViewModel : BaseListViewModel<BankDetails, BankDetailsLookup, AfterSaveBankDetailsEvent>
    {
        public BankDetailsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyListViewModel : BaseListViewModel<Company, CompanyLookup, AfterSaveCompanyEvent>
    {
        public CompanyListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyFormListViewModel : BaseListViewModel<CompanyForm, CompanyFormLookup, AfterSaveCompanyFormEvent>
    {
        public CompanyFormListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentsRegistrationDetailsListViewModel : BaseListViewModel<DocumentsRegistrationDetails, DocumentsRegistrationDetailsLookup, AfterSaveDocumentsRegistrationDetailsEvent>
    {
        public DocumentsRegistrationDetailsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeesPositionListViewModel : BaseListViewModel<EmployeesPosition, EmployeesPositionLookup, AfterSaveEmployeesPositionEvent>
    {
        public EmployeesPositionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityTypeListViewModel : BaseListViewModel<FacilityType, FacilityTypeLookup, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ActivityFieldListViewModel : BaseListViewModel<ActivityField, ActivityFieldLookup, AfterSaveActivityFieldEvent>
    {
        public ActivityFieldListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ContractListViewModel : BaseListViewModel<Contract, ContractLookup, AfterSaveContractEvent>
    {
        public ContractListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class MeasureListViewModel : BaseListViewModel<Measure, MeasureLookup, AfterSaveMeasureEvent>
    {
        public MeasureListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterListViewModel : BaseListViewModel<Parameter, ParameterLookup, AfterSaveParameterEvent>
    {
        public ParameterListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterGroupListViewModel : BaseListViewModel<ParameterGroup, ParameterGroupLookup, AfterSaveParameterGroupEvent>
    {
        public ParameterGroupListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductRelationListViewModel : BaseListViewModel<ProductRelation, ProductRelationLookup, AfterSaveProductRelationEvent>
    {
        public ProductRelationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class StandartPaymentConditionsListViewModel : BaseListViewModel<StandartPaymentConditions, StandartPaymentConditionsLookup, AfterSaveStandartPaymentConditionsEvent>
    {
        public StandartPaymentConditionsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PersonListViewModel : BaseListViewModel<Person, PersonLookup, AfterSavePersonEvent>
    {
        public PersonListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentPlannedListViewModel : BaseListViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent>
    {
        public PaymentPlannedListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentActualListViewModel : BaseListViewModel<PaymentActual, PaymentActualLookup, AfterSavePaymentActualEvent>
    {
        public PaymentActualListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterRelationListViewModel : BaseListViewModel<ParameterRelation, ParameterRelationLookup, AfterSaveParameterRelationEvent>
    {
        public ParameterRelationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectUnitListViewModel : BaseListViewModel<ProjectUnit, ProjectUnitLookup, AfterSaveProjectUnitEvent>
    {
        public ProjectUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderUnitListViewModel : BaseListViewModel<TenderUnit, TenderUnitLookup, AfterSaveTenderUnitEvent>
    {
        public TenderUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ShipmentUnitListViewModel : BaseListViewModel<ShipmentUnit, ShipmentUnitLookup, AfterSaveShipmentUnitEvent>
    {
        public ShipmentUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductionUnitListViewModel : BaseListViewModel<ProductionUnit, ProductionUnitLookup, AfterSaveProductionUnitEvent>
    {
        public ProductionUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SalesUnitListViewModel : BaseListViewModel<SalesUnit, SalesUnitLookup, AfterSaveSalesUnitEvent>
    {
        public SalesUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendAddressListViewModel : BaseListViewModel<TestFriendAddress, TestFriendAddressLookup, AfterSaveTestFriendAddressEvent>
    {
        public TestFriendAddressListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendListViewModel : BaseListViewModel<TestFriend, TestFriendLookup, AfterSaveTestFriendEvent>
    {
        public TestFriendListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendEmailListViewModel : BaseListViewModel<TestFriendEmail, TestFriendEmailLookup, AfterSaveTestFriendEmailEvent>
    {
        public TestFriendEmailListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendGroupListViewModel : BaseListViewModel<TestFriendGroup, TestFriendGroupLookup, AfterSaveTestFriendGroupEvent>
    {
        public TestFriendGroupListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentListViewModel : BaseListViewModel<Document, DocumentLookup, AfterSaveDocumentEvent>
    {
        public DocumentListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestEntityListViewModel : BaseListViewModel<TestEntity, TestEntityLookup, AfterSaveTestEntityEvent>
    {
        public TestEntityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestHusbandListViewModel : BaseListViewModel<TestHusband, TestHusbandLookup, AfterSaveTestHusbandEvent>
    {
        public TestHusbandListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestWifeListViewModel : BaseListViewModel<TestWife, TestWifeLookup, AfterSaveTestWifeEvent>
    {
        public TestWifeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestChildListViewModel : BaseListViewModel<TestChild, TestChildLookup, AfterSaveTestChildEvent>
    {
        public TestChildListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CostOnDateListViewModel : BaseListViewModel<CostOnDate, CostOnDateLookup, AfterSaveCostOnDateEvent>
    {
        public CostOnDateListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CostListViewModel : BaseListViewModel<Cost, CostLookup, AfterSaveCostEvent>
    {
        public CostListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CurrencyListViewModel : BaseListViewModel<Currency, CurrencyLookup, AfterSaveCurrencyEvent>
    {
        public CurrencyListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ExchangeCurrencyRateListViewModel : BaseListViewModel<ExchangeCurrencyRate, ExchangeCurrencyRateLookup, AfterSaveExchangeCurrencyRateEvent>
    {
        public ExchangeCurrencyRateListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductListViewModel : BaseListViewModel<Product, ProductLookup, AfterSaveProductEvent>
    {
        public ProductListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferListViewModel : BaseListViewModel<Offer, OfferLookup, AfterSaveOfferEvent>
    {
        public OfferListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeeListViewModel : BaseListViewModel<Employee, EmployeeLookup, AfterSaveEmployeeEvent>
    {
        public EmployeeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OrderListViewModel : BaseListViewModel<Order, OrderLookup, AfterSaveOrderEvent>
    {
        public OrderListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentConditionListViewModel : BaseListViewModel<PaymentCondition, PaymentConditionLookup, AfterSavePaymentConditionEvent>
    {
        public PaymentConditionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentDocumentListViewModel : BaseListViewModel<PaymentDocument, PaymentDocumentLookup, AfterSavePaymentDocumentEvent>
    {
        public PaymentDocumentListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityListViewModel : BaseListViewModel<Facility, FacilityLookup, AfterSaveFacilityEvent>
    {
        public FacilityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectListViewModel : BaseListViewModel<Project, ProjectLookup, AfterSaveProjectEvent>
    {
        public ProjectListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserRoleListViewModel : BaseListViewModel<UserRole, UserRoleLookup, AfterSaveUserRoleEvent>
    {
        public UserRoleListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SpecificationListViewModel : BaseListViewModel<Specification, SpecificationLookup, AfterSaveSpecificationEvent>
    {
        public SpecificationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderListViewModel : BaseListViewModel<Tender, TenderLookup, AfterSaveTenderEvent>
    {
        public TenderListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderTypeListViewModel : BaseListViewModel<TenderType, TenderTypeLookup, AfterSaveTenderTypeEvent>
    {
        public TenderTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserListViewModel : BaseListViewModel<User, UserLookup, AfterSaveUserEvent>
    {
        public UserListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferUnitListViewModel : BaseListViewModel<OfferUnit, OfferUnitLookup, AfterSaveOfferUnitEvent>
    {
        public OfferUnitListViewModel(IUnityContainer container) : base(container) { }
    }

}
