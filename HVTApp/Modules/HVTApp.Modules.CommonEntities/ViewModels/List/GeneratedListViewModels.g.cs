using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public partial class AddressListServiceViewModel : BaseListServiceViewModel<Address, AddressLookup, AfterSaveAddressEvent>
    {
        public AddressListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CountryListServiceViewModel : BaseListServiceViewModel<Country, CountryLookup, AfterSaveCountryEvent>
    {
        public CountryListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DistrictListServiceViewModel : BaseListServiceViewModel<District, DistrictLookup, AfterSaveDistrictEvent>
    {
        public DistrictListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityListServiceViewModel : BaseListServiceViewModel<Locality, LocalityLookup, AfterSaveLocalityEvent>
    {
        public LocalityListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityTypeListServiceViewModel : BaseListServiceViewModel<LocalityType, LocalityTypeLookup, AfterSaveLocalityTypeEvent>
    {
        public LocalityTypeListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class RegionListServiceViewModel : BaseListServiceViewModel<Region, RegionLookup, AfterSaveRegionEvent>
    {
        public RegionListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class AdditionalSalesUnitsListServiceViewModel : BaseListServiceViewModel<AdditionalSalesUnits, AdditionalSalesUnitsLookup, AfterSaveAdditionalSalesUnitsEvent>
    {
        public AdditionalSalesUnitsListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class BankDetailsListServiceViewModel : BaseListServiceViewModel<BankDetails, BankDetailsLookup, AfterSaveBankDetailsEvent>
    {
        public BankDetailsListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyListServiceViewModel : BaseListServiceViewModel<Company, CompanyLookup, AfterSaveCompanyEvent>
    {
        public CompanyListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyFormListServiceViewModel : BaseListServiceViewModel<CompanyForm, CompanyFormLookup, AfterSaveCompanyFormEvent>
    {
        public CompanyFormListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentsRegistrationDetailsListServiceViewModel : BaseListServiceViewModel<DocumentsRegistrationDetails, DocumentsRegistrationDetailsLookup, AfterSaveDocumentsRegistrationDetailsEvent>
    {
        public DocumentsRegistrationDetailsListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeesPositionListServiceViewModel : BaseListServiceViewModel<EmployeesPosition, EmployeesPositionLookup, AfterSaveEmployeesPositionEvent>
    {
        public EmployeesPositionListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityTypeListServiceViewModel : BaseListServiceViewModel<FacilityType, FacilityTypeLookup, AfterSaveFacilityTypeEvent>
    {
        public FacilityTypeListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ActivityFieldListServiceViewModel : BaseListServiceViewModel<ActivityField, ActivityFieldLookup, AfterSaveActivityFieldEvent>
    {
        public ActivityFieldListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ContractListServiceViewModel : BaseListServiceViewModel<Contract, ContractLookup, AfterSaveContractEvent>
    {
        public ContractListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class MeasureListServiceViewModel : BaseListServiceViewModel<Measure, MeasureLookup, AfterSaveMeasureEvent>
    {
        public MeasureListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterListServiceViewModel : BaseListServiceViewModel<Parameter, ParameterLookup, AfterSaveParameterEvent>
    {
        public ParameterListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterGroupListServiceViewModel : BaseListServiceViewModel<ParameterGroup, ParameterGroupLookup, AfterSaveParameterGroupEvent>
    {
        public ParameterGroupListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductRelationListServiceViewModel : BaseListServiceViewModel<ProductRelation, ProductRelationLookup, AfterSaveProductRelationEvent>
    {
        public ProductRelationListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class StandartPaymentConditionsListServiceViewModel : BaseListServiceViewModel<StandartPaymentConditions, StandartPaymentConditionsLookup, AfterSaveStandartPaymentConditionsEvent>
    {
        public StandartPaymentConditionsListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PersonListServiceViewModel : BaseListServiceViewModel<Person, PersonLookup, AfterSavePersonEvent>
    {
        public PersonListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentPlannedListServiceViewModel : BaseListServiceViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent>
    {
        public PaymentPlannedListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentActualListServiceViewModel : BaseListServiceViewModel<PaymentActual, PaymentActualLookup, AfterSavePaymentActualEvent>
    {
        public PaymentActualListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterRelationListServiceViewModel : BaseListServiceViewModel<ParameterRelation, ParameterRelationLookup, AfterSaveParameterRelationEvent>
    {
        public ParameterRelationListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectUnitListServiceViewModel : BaseListServiceViewModel<ProjectUnit, ProjectUnitLookup, AfterSaveProjectUnitEvent>
    {
        public ProjectUnitListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderUnitListServiceViewModel : BaseListServiceViewModel<TenderUnit, TenderUnitLookup, AfterSaveTenderUnitEvent>
    {
        public TenderUnitListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ShipmentUnitListServiceViewModel : BaseListServiceViewModel<ShipmentUnit, ShipmentUnitLookup, AfterSaveShipmentUnitEvent>
    {
        public ShipmentUnitListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductionUnitListServiceViewModel : BaseListServiceViewModel<ProductionUnit, ProductionUnitLookup, AfterSaveProductionUnitEvent>
    {
        public ProductionUnitListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SalesUnitListServiceViewModel : BaseListServiceViewModel<SalesUnit, SalesUnitLookup, AfterSaveSalesUnitEvent>
    {
        public SalesUnitListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendAddressListServiceViewModel : BaseListServiceViewModel<TestFriendAddress, TestFriendAddressLookup, AfterSaveTestFriendAddressEvent>
    {
        public TestFriendAddressListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendListServiceViewModel : BaseListServiceViewModel<TestFriend, TestFriendLookup, AfterSaveTestFriendEvent>
    {
        public TestFriendListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendEmailListServiceViewModel : BaseListServiceViewModel<TestFriendEmail, TestFriendEmailLookup, AfterSaveTestFriendEmailEvent>
    {
        public TestFriendEmailListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendGroupListServiceViewModel : BaseListServiceViewModel<TestFriendGroup, TestFriendGroupLookup, AfterSaveTestFriendGroupEvent>
    {
        public TestFriendGroupListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentListServiceViewModel : BaseListServiceViewModel<Document, DocumentLookup, AfterSaveDocumentEvent>
    {
        public DocumentListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestEntityListServiceViewModel : BaseListServiceViewModel<TestEntity, TestEntityLookup, AfterSaveTestEntityEvent>
    {
        public TestEntityListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestHusbandListServiceViewModel : BaseListServiceViewModel<TestHusband, TestHusbandLookup, AfterSaveTestHusbandEvent>
    {
        public TestHusbandListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestWifeListServiceViewModel : BaseListServiceViewModel<TestWife, TestWifeLookup, AfterSaveTestWifeEvent>
    {
        public TestWifeListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestChildListServiceViewModel : BaseListServiceViewModel<TestChild, TestChildLookup, AfterSaveTestChildEvent>
    {
        public TestChildListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CostOnDateListServiceViewModel : BaseListServiceViewModel<CostOnDate, CostOnDateLookup, AfterSaveCostOnDateEvent>
    {
        public CostOnDateListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CostListServiceViewModel : BaseListServiceViewModel<Cost, CostLookup, AfterSaveCostEvent>
    {
        public CostListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CurrencyListServiceViewModel : BaseListServiceViewModel<Currency, CurrencyLookup, AfterSaveCurrencyEvent>
    {
        public CurrencyListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ExchangeCurrencyRateListServiceViewModel : BaseListServiceViewModel<ExchangeCurrencyRate, ExchangeCurrencyRateLookup, AfterSaveExchangeCurrencyRateEvent>
    {
        public ExchangeCurrencyRateListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductListServiceViewModel : BaseListServiceViewModel<Product, ProductLookup, AfterSaveProductEvent>
    {
        public ProductListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferListServiceViewModel : BaseListServiceViewModel<Offer, OfferLookup, AfterSaveOfferEvent>
    {
        public OfferListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeeListServiceViewModel : BaseListServiceViewModel<Employee, EmployeeLookup, AfterSaveEmployeeEvent>
    {
        public EmployeeListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OrderListServiceViewModel : BaseListServiceViewModel<Order, OrderLookup, AfterSaveOrderEvent>
    {
        public OrderListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentConditionListServiceViewModel : BaseListServiceViewModel<PaymentCondition, PaymentConditionLookup, AfterSavePaymentConditionEvent>
    {
        public PaymentConditionListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentDocumentListServiceViewModel : BaseListServiceViewModel<PaymentDocument, PaymentDocumentLookup, AfterSavePaymentDocumentEvent>
    {
        public PaymentDocumentListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityListServiceViewModel : BaseListServiceViewModel<Facility, FacilityLookup, AfterSaveFacilityEvent>
    {
        public FacilityListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectListServiceViewModel : BaseListServiceViewModel<Project, ProjectLookup, AfterSaveProjectEvent>
    {
        public ProjectListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserRoleListServiceViewModel : BaseListServiceViewModel<UserRole, UserRoleLookup, AfterSaveUserRoleEvent>
    {
        public UserRoleListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SpecificationListServiceViewModel : BaseListServiceViewModel<Specification, SpecificationLookup, AfterSaveSpecificationEvent>
    {
        public SpecificationListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderListServiceViewModel : BaseListServiceViewModel<Tender, TenderLookup, AfterSaveTenderEvent>
    {
        public TenderListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderTypeListServiceViewModel : BaseListServiceViewModel<TenderType, TenderTypeLookup, AfterSaveTenderTypeEvent>
    {
        public TenderTypeListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserListServiceViewModel : BaseListServiceViewModel<User, UserLookup, AfterSaveUserEvent>
    {
        public UserListServiceViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferUnitListServiceViewModel : BaseListServiceViewModel<OfferUnit, OfferUnitLookup, AfterSaveOfferUnitEvent>
    {
        public OfferUnitListServiceViewModel(IUnityContainer container) : base(container) { }
    }

}
