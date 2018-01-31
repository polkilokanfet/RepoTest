using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public partial class AddressListViewModel : BaseListViewModel<Address, AddressLookup, AfterSaveAddressEvent, AfterSelectAddressEvent>
    {
        public AddressListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CountryListViewModel : BaseListViewModel<Country, CountryLookup, AfterSaveCountryEvent, AfterSelectCountryEvent>
    {
        public CountryListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DistrictListViewModel : BaseListViewModel<District, DistrictLookup, AfterSaveDistrictEvent, AfterSelectDistrictEvent>
    {
        public DistrictListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityListViewModel : BaseListViewModel<Locality, LocalityLookup, AfterSaveLocalityEvent, AfterSelectLocalityEvent>
    {
        public LocalityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class LocalityTypeListViewModel : BaseListViewModel<LocalityType, LocalityTypeLookup, AfterSaveLocalityTypeEvent, AfterSelectLocalityTypeEvent>
    {
        public LocalityTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class RegionListViewModel : BaseListViewModel<Region, RegionLookup, AfterSaveRegionEvent, AfterSelectRegionEvent>
    {
        public RegionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class AdditionalSalesUnitsListViewModel : BaseListViewModel<AdditionalSalesUnits, AdditionalSalesUnitsLookup, AfterSaveAdditionalSalesUnitsEvent, AfterSelectAdditionalSalesUnitsEvent>
    {
        public AdditionalSalesUnitsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class BankDetailsListViewModel : BaseListViewModel<BankDetails, BankDetailsLookup, AfterSaveBankDetailsEvent, AfterSelectBankDetailsEvent>
    {
        public BankDetailsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyListViewModel : BaseListViewModel<Company, CompanyLookup, AfterSaveCompanyEvent, AfterSelectCompanyEvent>
    {
        public CompanyListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CompanyFormListViewModel : BaseListViewModel<CompanyForm, CompanyFormLookup, AfterSaveCompanyFormEvent, AfterSelectCompanyFormEvent>
    {
        public CompanyFormListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentsRegistrationDetailsListViewModel : BaseListViewModel<DocumentsRegistrationDetails, DocumentsRegistrationDetailsLookup, AfterSaveDocumentsRegistrationDetailsEvent, AfterSelectDocumentsRegistrationDetailsEvent>
    {
        public DocumentsRegistrationDetailsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeesPositionListViewModel : BaseListViewModel<EmployeesPosition, EmployeesPositionLookup, AfterSaveEmployeesPositionEvent, AfterSelectEmployeesPositionEvent>
    {
        public EmployeesPositionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityTypeListViewModel : BaseListViewModel<FacilityType, FacilityTypeLookup, AfterSaveFacilityTypeEvent, AfterSelectFacilityTypeEvent>
    {
        public FacilityTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ActivityFieldListViewModel : BaseListViewModel<ActivityField, ActivityFieldLookup, AfterSaveActivityFieldEvent, AfterSelectActivityFieldEvent>
    {
        public ActivityFieldListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ContractListViewModel : BaseListViewModel<Contract, ContractLookup, AfterSaveContractEvent, AfterSelectContractEvent>
    {
        public ContractListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class MeasureListViewModel : BaseListViewModel<Measure, MeasureLookup, AfterSaveMeasureEvent, AfterSelectMeasureEvent>
    {
        public MeasureListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterListViewModel : BaseListViewModel<Parameter, ParameterLookup, AfterSaveParameterEvent, AfterSelectParameterEvent>
    {
        public ParameterListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterGroupListViewModel : BaseListViewModel<ParameterGroup, ParameterGroupLookup, AfterSaveParameterGroupEvent, AfterSelectParameterGroupEvent>
    {
        public ParameterGroupListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductRelationListViewModel : BaseListViewModel<ProductRelation, ProductRelationLookup, AfterSaveProductRelationEvent, AfterSelectProductRelationEvent>
    {
        public ProductRelationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class StandartPaymentConditionsListViewModel : BaseListViewModel<StandartPaymentConditions, StandartPaymentConditionsLookup, AfterSaveStandartPaymentConditionsEvent, AfterSelectStandartPaymentConditionsEvent>
    {
        public StandartPaymentConditionsListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PersonListViewModel : BaseListViewModel<Person, PersonLookup, AfterSavePersonEvent, AfterSelectPersonEvent>
    {
        public PersonListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentPlannedListViewModel : BaseListViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent, AfterSelectPaymentPlannedEvent>
    {
        public PaymentPlannedListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentActualListViewModel : BaseListViewModel<PaymentActual, PaymentActualLookup, AfterSavePaymentActualEvent, AfterSelectPaymentActualEvent>
    {
        public PaymentActualListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ParameterRelationListViewModel : BaseListViewModel<ParameterRelation, ParameterRelationLookup, AfterSaveParameterRelationEvent, AfterSelectParameterRelationEvent>
    {
        public ParameterRelationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectUnitListViewModel : BaseListViewModel<ProjectUnit, ProjectUnitLookup, AfterSaveProjectUnitEvent, AfterSelectProjectUnitEvent>
    {
        public ProjectUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderUnitListViewModel : BaseListViewModel<TenderUnit, TenderUnitLookup, AfterSaveTenderUnitEvent, AfterSelectTenderUnitEvent>
    {
        public TenderUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ShipmentUnitListViewModel : BaseListViewModel<ShipmentUnit, ShipmentUnitLookup, AfterSaveShipmentUnitEvent, AfterSelectShipmentUnitEvent>
    {
        public ShipmentUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductionUnitListViewModel : BaseListViewModel<ProductionUnit, ProductionUnitLookup, AfterSaveProductionUnitEvent, AfterSelectProductionUnitEvent>
    {
        public ProductionUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SalesUnitListViewModel : BaseListViewModel<SalesUnit, SalesUnitLookup, AfterSaveSalesUnitEvent, AfterSelectSalesUnitEvent>
    {
        public SalesUnitListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendAddressListViewModel : BaseListViewModel<TestFriendAddress, TestFriendAddressLookup, AfterSaveTestFriendAddressEvent, AfterSelectTestFriendAddressEvent>
    {
        public TestFriendAddressListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendListViewModel : BaseListViewModel<TestFriend, TestFriendLookup, AfterSaveTestFriendEvent, AfterSelectTestFriendEvent>
    {
        public TestFriendListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendEmailListViewModel : BaseListViewModel<TestFriendEmail, TestFriendEmailLookup, AfterSaveTestFriendEmailEvent, AfterSelectTestFriendEmailEvent>
    {
        public TestFriendEmailListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestFriendGroupListViewModel : BaseListViewModel<TestFriendGroup, TestFriendGroupLookup, AfterSaveTestFriendGroupEvent, AfterSelectTestFriendGroupEvent>
    {
        public TestFriendGroupListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class DocumentListViewModel : BaseListViewModel<Document, DocumentLookup, AfterSaveDocumentEvent, AfterSelectDocumentEvent>
    {
        public DocumentListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestEntityListViewModel : BaseListViewModel<TestEntity, TestEntityLookup, AfterSaveTestEntityEvent, AfterSelectTestEntityEvent>
    {
        public TestEntityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestHusbandListViewModel : BaseListViewModel<TestHusband, TestHusbandLookup, AfterSaveTestHusbandEvent, AfterSelectTestHusbandEvent>
    {
        public TestHusbandListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestWifeListViewModel : BaseListViewModel<TestWife, TestWifeLookup, AfterSaveTestWifeEvent, AfterSelectTestWifeEvent>
    {
        public TestWifeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TestChildListViewModel : BaseListViewModel<TestChild, TestChildLookup, AfterSaveTestChildEvent, AfterSelectTestChildEvent>
    {
        public TestChildListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CostOnDateListViewModel : BaseListViewModel<CostOnDate, CostOnDateLookup, AfterSaveCostOnDateEvent, AfterSelectCostOnDateEvent>
    {
        public CostOnDateListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CostListViewModel : BaseListViewModel<Cost, CostLookup, AfterSaveCostEvent, AfterSelectCostEvent>
    {
        public CostListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class CurrencyListViewModel : BaseListViewModel<Currency, CurrencyLookup, AfterSaveCurrencyEvent, AfterSelectCurrencyEvent>
    {
        public CurrencyListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ExchangeCurrencyRateListViewModel : BaseListViewModel<ExchangeCurrencyRate, ExchangeCurrencyRateLookup, AfterSaveExchangeCurrencyRateEvent, AfterSelectExchangeCurrencyRateEvent>
    {
        public ExchangeCurrencyRateListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProductListViewModel : BaseListViewModel<Product, ProductLookup, AfterSaveProductEvent, AfterSelectProductEvent>
    {
        public ProductListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferListViewModel : BaseListViewModel<Offer, OfferLookup, AfterSaveOfferEvent, AfterSelectOfferEvent>
    {
        public OfferListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class EmployeeListViewModel : BaseListViewModel<Employee, EmployeeLookup, AfterSaveEmployeeEvent, AfterSelectEmployeeEvent>
    {
        public EmployeeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OrderListViewModel : BaseListViewModel<Order, OrderLookup, AfterSaveOrderEvent, AfterSelectOrderEvent>
    {
        public OrderListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentConditionListViewModel : BaseListViewModel<PaymentCondition, PaymentConditionLookup, AfterSavePaymentConditionEvent, AfterSelectPaymentConditionEvent>
    {
        public PaymentConditionListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class PaymentDocumentListViewModel : BaseListViewModel<PaymentDocument, PaymentDocumentLookup, AfterSavePaymentDocumentEvent, AfterSelectPaymentDocumentEvent>
    {
        public PaymentDocumentListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class FacilityListViewModel : BaseListViewModel<Facility, FacilityLookup, AfterSaveFacilityEvent, AfterSelectFacilityEvent>
    {
        public FacilityListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class ProjectListViewModel : BaseListViewModel<Project, ProjectLookup, AfterSaveProjectEvent, AfterSelectProjectEvent>
    {
        public ProjectListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserRoleListViewModel : BaseListViewModel<UserRole, UserRoleLookup, AfterSaveUserRoleEvent, AfterSelectUserRoleEvent>
    {
        public UserRoleListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class SpecificationListViewModel : BaseListViewModel<Specification, SpecificationLookup, AfterSaveSpecificationEvent, AfterSelectSpecificationEvent>
    {
        public SpecificationListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderListViewModel : BaseListViewModel<Tender, TenderLookup, AfterSaveTenderEvent, AfterSelectTenderEvent>
    {
        public TenderListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class TenderTypeListViewModel : BaseListViewModel<TenderType, TenderTypeLookup, AfterSaveTenderTypeEvent, AfterSelectTenderTypeEvent>
    {
        public TenderTypeListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class UserListViewModel : BaseListViewModel<User, UserLookup, AfterSaveUserEvent, AfterSelectUserEvent>
    {
        public UserListViewModel(IUnityContainer container) : base(container) { }
    }

    public partial class OfferUnitListViewModel : BaseListViewModel<OfferUnit, OfferUnitLookup, AfterSaveOfferUnitEvent, AfterSelectOfferUnitEvent>
    {
        public OfferUnitListViewModel(IUnityContainer container) : base(container) { }
    }

}
