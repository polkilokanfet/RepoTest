using HVTApp.Infrastructure.Prism;
using HVTApp.UI.Views;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI
{
    public partial class UiModule : ModuleBase
    {
		private void RegisterViews()
        {
            Container.RegisterViewForNavigation<AddressListView>();
            //_dialogService.Register<AddressDetailsViewModel, AddressDetailsView>();
			_selectService.Register<AddressListView, AddressLookup>();
            _updateDetailsService.Register<Address, AddressDetailsView>();

            Container.RegisterViewForNavigation<CountryListView>();
            //_dialogService.Register<CountryDetailsViewModel, CountryDetailsView>();
			_selectService.Register<CountryListView, CountryLookup>();
            _updateDetailsService.Register<Country, CountryDetailsView>();

            Container.RegisterViewForNavigation<DistrictListView>();
            //_dialogService.Register<DistrictDetailsViewModel, DistrictDetailsView>();
			_selectService.Register<DistrictListView, DistrictLookup>();
            _updateDetailsService.Register<District, DistrictDetailsView>();

            Container.RegisterViewForNavigation<LocalityListView>();
            //_dialogService.Register<LocalityDetailsViewModel, LocalityDetailsView>();
			_selectService.Register<LocalityListView, LocalityLookup>();
            _updateDetailsService.Register<Locality, LocalityDetailsView>();

            Container.RegisterViewForNavigation<LocalityTypeListView>();
            //_dialogService.Register<LocalityTypeDetailsViewModel, LocalityTypeDetailsView>();
			_selectService.Register<LocalityTypeListView, LocalityTypeLookup>();
            _updateDetailsService.Register<LocalityType, LocalityTypeDetailsView>();

            Container.RegisterViewForNavigation<RegionListView>();
            //_dialogService.Register<RegionDetailsViewModel, RegionDetailsView>();
			_selectService.Register<RegionListView, RegionLookup>();
            _updateDetailsService.Register<Region, RegionDetailsView>();

            Container.RegisterViewForNavigation<AdditionalSalesUnitsListView>();
            //_dialogService.Register<AdditionalSalesUnitsDetailsViewModel, AdditionalSalesUnitsDetailsView>();
			_selectService.Register<AdditionalSalesUnitsListView, AdditionalSalesUnitsLookup>();
            _updateDetailsService.Register<AdditionalSalesUnits, AdditionalSalesUnitsDetailsView>();

            Container.RegisterViewForNavigation<BankDetailsListView>();
            //_dialogService.Register<BankDetailsDetailsViewModel, BankDetailsDetailsView>();
			_selectService.Register<BankDetailsListView, BankDetailsLookup>();
            _updateDetailsService.Register<BankDetails, BankDetailsDetailsView>();

            Container.RegisterViewForNavigation<CompanyListView>();
            //_dialogService.Register<CompanyDetailsViewModel, CompanyDetailsView>();
			_selectService.Register<CompanyListView, CompanyLookup>();
            _updateDetailsService.Register<Company, CompanyDetailsView>();

            Container.RegisterViewForNavigation<CompanyFormListView>();
            //_dialogService.Register<CompanyFormDetailsViewModel, CompanyFormDetailsView>();
			_selectService.Register<CompanyFormListView, CompanyFormLookup>();
            _updateDetailsService.Register<CompanyForm, CompanyFormDetailsView>();

            Container.RegisterViewForNavigation<DocumentsRegistrationDetailsListView>();
            //_dialogService.Register<DocumentsRegistrationDetailsDetailsViewModel, DocumentsRegistrationDetailsDetailsView>();
			_selectService.Register<DocumentsRegistrationDetailsListView, DocumentsRegistrationDetailsLookup>();
            _updateDetailsService.Register<DocumentsRegistrationDetails, DocumentsRegistrationDetailsDetailsView>();

            Container.RegisterViewForNavigation<EmployeesPositionListView>();
            //_dialogService.Register<EmployeesPositionDetailsViewModel, EmployeesPositionDetailsView>();
			_selectService.Register<EmployeesPositionListView, EmployeesPositionLookup>();
            _updateDetailsService.Register<EmployeesPosition, EmployeesPositionDetailsView>();

            Container.RegisterViewForNavigation<FacilityTypeListView>();
            //_dialogService.Register<FacilityTypeDetailsViewModel, FacilityTypeDetailsView>();
			_selectService.Register<FacilityTypeListView, FacilityTypeLookup>();
            _updateDetailsService.Register<FacilityType, FacilityTypeDetailsView>();

            Container.RegisterViewForNavigation<ActivityFieldListView>();
            //_dialogService.Register<ActivityFieldDetailsViewModel, ActivityFieldDetailsView>();
			_selectService.Register<ActivityFieldListView, ActivityFieldLookup>();
            _updateDetailsService.Register<ActivityField, ActivityFieldDetailsView>();

            Container.RegisterViewForNavigation<ContractListView>();
            //_dialogService.Register<ContractDetailsViewModel, ContractDetailsView>();
			_selectService.Register<ContractListView, ContractLookup>();
            _updateDetailsService.Register<Contract, ContractDetailsView>();

            Container.RegisterViewForNavigation<MeasureListView>();
            //_dialogService.Register<MeasureDetailsViewModel, MeasureDetailsView>();
			_selectService.Register<MeasureListView, MeasureLookup>();
            _updateDetailsService.Register<Measure, MeasureDetailsView>();

            Container.RegisterViewForNavigation<ParameterListView>();
            //_dialogService.Register<ParameterDetailsViewModel, ParameterDetailsView>();
			_selectService.Register<ParameterListView, ParameterLookup>();
            _updateDetailsService.Register<Parameter, ParameterDetailsView>();

            Container.RegisterViewForNavigation<ParameterGroupListView>();
            //_dialogService.Register<ParameterGroupDetailsViewModel, ParameterGroupDetailsView>();
			_selectService.Register<ParameterGroupListView, ParameterGroupLookup>();
            _updateDetailsService.Register<ParameterGroup, ParameterGroupDetailsView>();

            Container.RegisterViewForNavigation<ProductRelationListView>();
            //_dialogService.Register<ProductRelationDetailsViewModel, ProductRelationDetailsView>();
			_selectService.Register<ProductRelationListView, ProductRelationLookup>();
            _updateDetailsService.Register<ProductRelation, ProductRelationDetailsView>();

            Container.RegisterViewForNavigation<StandartPaymentConditionsListView>();
            //_dialogService.Register<StandartPaymentConditionsDetailsViewModel, StandartPaymentConditionsDetailsView>();
			_selectService.Register<StandartPaymentConditionsListView, StandartPaymentConditionsLookup>();
            _updateDetailsService.Register<StandartPaymentConditions, StandartPaymentConditionsDetailsView>();

            Container.RegisterViewForNavigation<PersonListView>();
            //_dialogService.Register<PersonDetailsViewModel, PersonDetailsView>();
			_selectService.Register<PersonListView, PersonLookup>();
            _updateDetailsService.Register<Person, PersonDetailsView>();

            Container.RegisterViewForNavigation<PaymentPlannedListView>();
            //_dialogService.Register<PaymentPlannedDetailsViewModel, PaymentPlannedDetailsView>();
			_selectService.Register<PaymentPlannedListView, PaymentPlannedLookup>();
            _updateDetailsService.Register<PaymentPlanned, PaymentPlannedDetailsView>();

            Container.RegisterViewForNavigation<PaymentActualListView>();
            //_dialogService.Register<PaymentActualDetailsViewModel, PaymentActualDetailsView>();
			_selectService.Register<PaymentActualListView, PaymentActualLookup>();
            _updateDetailsService.Register<PaymentActual, PaymentActualDetailsView>();

            Container.RegisterViewForNavigation<ParameterRelationListView>();
            //_dialogService.Register<ParameterRelationDetailsViewModel, ParameterRelationDetailsView>();
			_selectService.Register<ParameterRelationListView, ParameterRelationLookup>();
            _updateDetailsService.Register<ParameterRelation, ParameterRelationDetailsView>();

            Container.RegisterViewForNavigation<ProjectUnitListView>();
            //_dialogService.Register<ProjectUnitDetailsViewModel, ProjectUnitDetailsView>();
			_selectService.Register<ProjectUnitListView, ProjectUnitLookup>();
            _updateDetailsService.Register<ProjectUnit, ProjectUnitDetailsView>();

            Container.RegisterViewForNavigation<TenderUnitListView>();
            //_dialogService.Register<TenderUnitDetailsViewModel, TenderUnitDetailsView>();
			_selectService.Register<TenderUnitListView, TenderUnitLookup>();
            _updateDetailsService.Register<TenderUnit, TenderUnitDetailsView>();

            Container.RegisterViewForNavigation<ShipmentUnitListView>();
            //_dialogService.Register<ShipmentUnitDetailsViewModel, ShipmentUnitDetailsView>();
			_selectService.Register<ShipmentUnitListView, ShipmentUnitLookup>();
            _updateDetailsService.Register<ShipmentUnit, ShipmentUnitDetailsView>();

            Container.RegisterViewForNavigation<ProductionUnitListView>();
            //_dialogService.Register<ProductionUnitDetailsViewModel, ProductionUnitDetailsView>();
			_selectService.Register<ProductionUnitListView, ProductionUnitLookup>();
            _updateDetailsService.Register<ProductionUnit, ProductionUnitDetailsView>();

            Container.RegisterViewForNavigation<SalesUnitListView>();
            //_dialogService.Register<SalesUnitDetailsViewModel, SalesUnitDetailsView>();
			_selectService.Register<SalesUnitListView, SalesUnitLookup>();
            _updateDetailsService.Register<SalesUnit, SalesUnitDetailsView>();

            Container.RegisterViewForNavigation<TestFriendAddressListView>();
            //_dialogService.Register<TestFriendAddressDetailsViewModel, TestFriendAddressDetailsView>();
			_selectService.Register<TestFriendAddressListView, TestFriendAddressLookup>();
            _updateDetailsService.Register<TestFriendAddress, TestFriendAddressDetailsView>();

            Container.RegisterViewForNavigation<TestFriendListView>();
            //_dialogService.Register<TestFriendDetailsViewModel, TestFriendDetailsView>();
			_selectService.Register<TestFriendListView, TestFriendLookup>();
            _updateDetailsService.Register<TestFriend, TestFriendDetailsView>();

            Container.RegisterViewForNavigation<TestFriendEmailListView>();
            //_dialogService.Register<TestFriendEmailDetailsViewModel, TestFriendEmailDetailsView>();
			_selectService.Register<TestFriendEmailListView, TestFriendEmailLookup>();
            _updateDetailsService.Register<TestFriendEmail, TestFriendEmailDetailsView>();

            Container.RegisterViewForNavigation<TestFriendGroupListView>();
            //_dialogService.Register<TestFriendGroupDetailsViewModel, TestFriendGroupDetailsView>();
			_selectService.Register<TestFriendGroupListView, TestFriendGroupLookup>();
            _updateDetailsService.Register<TestFriendGroup, TestFriendGroupDetailsView>();

            Container.RegisterViewForNavigation<DocumentListView>();
            //_dialogService.Register<DocumentDetailsViewModel, DocumentDetailsView>();
			_selectService.Register<DocumentListView, DocumentLookup>();
            _updateDetailsService.Register<Document, DocumentDetailsView>();

            Container.RegisterViewForNavigation<TestEntityListView>();
            //_dialogService.Register<TestEntityDetailsViewModel, TestEntityDetailsView>();
			_selectService.Register<TestEntityListView, TestEntityLookup>();
            _updateDetailsService.Register<TestEntity, TestEntityDetailsView>();

            Container.RegisterViewForNavigation<TestHusbandListView>();
            //_dialogService.Register<TestHusbandDetailsViewModel, TestHusbandDetailsView>();
			_selectService.Register<TestHusbandListView, TestHusbandLookup>();
            _updateDetailsService.Register<TestHusband, TestHusbandDetailsView>();

            Container.RegisterViewForNavigation<TestWifeListView>();
            //_dialogService.Register<TestWifeDetailsViewModel, TestWifeDetailsView>();
			_selectService.Register<TestWifeListView, TestWifeLookup>();
            _updateDetailsService.Register<TestWife, TestWifeDetailsView>();

            Container.RegisterViewForNavigation<TestChildListView>();
            //_dialogService.Register<TestChildDetailsViewModel, TestChildDetailsView>();
			_selectService.Register<TestChildListView, TestChildLookup>();
            _updateDetailsService.Register<TestChild, TestChildDetailsView>();

            Container.RegisterViewForNavigation<CostOnDateListView>();
            //_dialogService.Register<CostOnDateDetailsViewModel, CostOnDateDetailsView>();
			_selectService.Register<CostOnDateListView, CostOnDateLookup>();
            _updateDetailsService.Register<CostOnDate, CostOnDateDetailsView>();

            Container.RegisterViewForNavigation<CostListView>();
            //_dialogService.Register<CostDetailsViewModel, CostDetailsView>();
			_selectService.Register<CostListView, CostLookup>();
            _updateDetailsService.Register<Cost, CostDetailsView>();

            Container.RegisterViewForNavigation<CurrencyListView>();
            //_dialogService.Register<CurrencyDetailsViewModel, CurrencyDetailsView>();
			_selectService.Register<CurrencyListView, CurrencyLookup>();
            _updateDetailsService.Register<Currency, CurrencyDetailsView>();

            Container.RegisterViewForNavigation<ExchangeCurrencyRateListView>();
            //_dialogService.Register<ExchangeCurrencyRateDetailsViewModel, ExchangeCurrencyRateDetailsView>();
			_selectService.Register<ExchangeCurrencyRateListView, ExchangeCurrencyRateLookup>();
            _updateDetailsService.Register<ExchangeCurrencyRate, ExchangeCurrencyRateDetailsView>();

            Container.RegisterViewForNavigation<ProductListView>();
            //_dialogService.Register<ProductDetailsViewModel, ProductDetailsView>();
			_selectService.Register<ProductListView, ProductLookup>();
            _updateDetailsService.Register<Product, ProductDetailsView>();

            Container.RegisterViewForNavigation<OfferListView>();
            //_dialogService.Register<OfferDetailsViewModel, OfferDetailsView>();
			_selectService.Register<OfferListView, OfferLookup>();
            _updateDetailsService.Register<Offer, OfferDetailsView>();

            Container.RegisterViewForNavigation<EmployeeListView>();
            //_dialogService.Register<EmployeeDetailsViewModel, EmployeeDetailsView>();
			_selectService.Register<EmployeeListView, EmployeeLookup>();
            _updateDetailsService.Register<Employee, EmployeeDetailsView>();

            Container.RegisterViewForNavigation<OrderListView>();
            //_dialogService.Register<OrderDetailsViewModel, OrderDetailsView>();
			_selectService.Register<OrderListView, OrderLookup>();
            _updateDetailsService.Register<Order, OrderDetailsView>();

            Container.RegisterViewForNavigation<PaymentConditionListView>();
            //_dialogService.Register<PaymentConditionDetailsViewModel, PaymentConditionDetailsView>();
			_selectService.Register<PaymentConditionListView, PaymentConditionLookup>();
            _updateDetailsService.Register<PaymentCondition, PaymentConditionDetailsView>();

            Container.RegisterViewForNavigation<PaymentDocumentListView>();
            //_dialogService.Register<PaymentDocumentDetailsViewModel, PaymentDocumentDetailsView>();
			_selectService.Register<PaymentDocumentListView, PaymentDocumentLookup>();
            _updateDetailsService.Register<PaymentDocument, PaymentDocumentDetailsView>();

            Container.RegisterViewForNavigation<FacilityListView>();
            //_dialogService.Register<FacilityDetailsViewModel, FacilityDetailsView>();
			_selectService.Register<FacilityListView, FacilityLookup>();
            _updateDetailsService.Register<Facility, FacilityDetailsView>();

            Container.RegisterViewForNavigation<ProjectListView>();
            //_dialogService.Register<ProjectDetailsViewModel, ProjectDetailsView>();
			_selectService.Register<ProjectListView, ProjectLookup>();
            _updateDetailsService.Register<Project, ProjectDetailsView>();

            Container.RegisterViewForNavigation<UserRoleListView>();
            //_dialogService.Register<UserRoleDetailsViewModel, UserRoleDetailsView>();
			_selectService.Register<UserRoleListView, UserRoleLookup>();
            _updateDetailsService.Register<UserRole, UserRoleDetailsView>();

            Container.RegisterViewForNavigation<SpecificationListView>();
            //_dialogService.Register<SpecificationDetailsViewModel, SpecificationDetailsView>();
			_selectService.Register<SpecificationListView, SpecificationLookup>();
            _updateDetailsService.Register<Specification, SpecificationDetailsView>();

            Container.RegisterViewForNavigation<TenderListView>();
            //_dialogService.Register<TenderDetailsViewModel, TenderDetailsView>();
			_selectService.Register<TenderListView, TenderLookup>();
            _updateDetailsService.Register<Tender, TenderDetailsView>();

            Container.RegisterViewForNavigation<TenderTypeListView>();
            //_dialogService.Register<TenderTypeDetailsViewModel, TenderTypeDetailsView>();
			_selectService.Register<TenderTypeListView, TenderTypeLookup>();
            _updateDetailsService.Register<TenderType, TenderTypeDetailsView>();

            Container.RegisterViewForNavigation<UserListView>();
            //_dialogService.Register<UserDetailsViewModel, UserDetailsView>();
			_selectService.Register<UserListView, UserLookup>();
            _updateDetailsService.Register<User, UserDetailsView>();

            Container.RegisterViewForNavigation<OfferUnitListView>();
            //_dialogService.Register<OfferUnitDetailsViewModel, OfferUnitDetailsView>();
			_selectService.Register<OfferUnitListView, OfferUnitLookup>();
            _updateDetailsService.Register<OfferUnit, OfferUnitDetailsView>();

		}
	}
}
