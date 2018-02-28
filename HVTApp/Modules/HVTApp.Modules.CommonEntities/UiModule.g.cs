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
            Container.RegisterViewForNavigation<CommonOptionListView>();
            //_dialogService.Register<CommonOptionDetailsViewModel, CommonOptionDetailsView>();
			_selectService.Register<CommonOptionListView, CommonOption>();
            _updateDetailsService.Register<CommonOption, CommonOptionDetailsView>();

            Container.RegisterViewForNavigation<AddressListView>();
            //_dialogService.Register<AddressDetailsViewModel, AddressDetailsView>();
			_selectService.Register<AddressListView, Address>();
            _updateDetailsService.Register<Address, AddressDetailsView>();

            Container.RegisterViewForNavigation<CountryListView>();
            //_dialogService.Register<CountryDetailsViewModel, CountryDetailsView>();
			_selectService.Register<CountryListView, Country>();
            _updateDetailsService.Register<Country, CountryDetailsView>();

            Container.RegisterViewForNavigation<DistrictListView>();
            //_dialogService.Register<DistrictDetailsViewModel, DistrictDetailsView>();
			_selectService.Register<DistrictListView, District>();
            _updateDetailsService.Register<District, DistrictDetailsView>();

            Container.RegisterViewForNavigation<LocalityListView>();
            //_dialogService.Register<LocalityDetailsViewModel, LocalityDetailsView>();
			_selectService.Register<LocalityListView, Locality>();
            _updateDetailsService.Register<Locality, LocalityDetailsView>();

            Container.RegisterViewForNavigation<LocalityTypeListView>();
            //_dialogService.Register<LocalityTypeDetailsViewModel, LocalityTypeDetailsView>();
			_selectService.Register<LocalityTypeListView, LocalityType>();
            _updateDetailsService.Register<LocalityType, LocalityTypeDetailsView>();

            Container.RegisterViewForNavigation<RegionListView>();
            //_dialogService.Register<RegionDetailsViewModel, RegionDetailsView>();
			_selectService.Register<RegionListView, Region>();
            _updateDetailsService.Register<Region, RegionDetailsView>();

            Container.RegisterViewForNavigation<CalculatePriceTaskListView>();
            //_dialogService.Register<CalculatePriceTaskDetailsViewModel, CalculatePriceTaskDetailsView>();
			_selectService.Register<CalculatePriceTaskListView, CalculatePriceTask>();
            _updateDetailsService.Register<CalculatePriceTask, CalculatePriceTaskDetailsView>();

            Container.RegisterViewForNavigation<CostListView>();
            //_dialogService.Register<CostDetailsViewModel, CostDetailsView>();
			_selectService.Register<CostListView, Cost>();
            _updateDetailsService.Register<Cost, CostDetailsView>();

            Container.RegisterViewForNavigation<CurrencyListView>();
            //_dialogService.Register<CurrencyDetailsViewModel, CurrencyDetailsView>();
			_selectService.Register<CurrencyListView, Currency>();
            _updateDetailsService.Register<Currency, CurrencyDetailsView>();

            Container.RegisterViewForNavigation<CurrencyExchangeRateListView>();
            //_dialogService.Register<CurrencyExchangeRateDetailsViewModel, CurrencyExchangeRateDetailsView>();
			_selectService.Register<CurrencyExchangeRateListView, CurrencyExchangeRate>();
            _updateDetailsService.Register<CurrencyExchangeRate, CurrencyExchangeRateDetailsView>();

            Container.RegisterViewForNavigation<DescribeProductBlockTaskListView>();
            //_dialogService.Register<DescribeProductBlockTaskDetailsViewModel, DescribeProductBlockTaskDetailsView>();
			_selectService.Register<DescribeProductBlockTaskListView, DescribeProductBlockTask>();
            _updateDetailsService.Register<DescribeProductBlockTask, DescribeProductBlockTaskDetailsView>();

            Container.RegisterViewForNavigation<NoteListView>();
            //_dialogService.Register<NoteDetailsViewModel, NoteDetailsView>();
			_selectService.Register<NoteListView, Note>();
            _updateDetailsService.Register<Note, NoteDetailsView>();

            Container.RegisterViewForNavigation<OfferUnitListView>();
            //_dialogService.Register<OfferUnitDetailsViewModel, OfferUnitDetailsView>();
			_selectService.Register<OfferUnitListView, OfferUnit>();
            _updateDetailsService.Register<OfferUnit, OfferUnitDetailsView>();

            Container.RegisterViewForNavigation<PaymentConditionSetListView>();
            //_dialogService.Register<PaymentConditionSetDetailsViewModel, PaymentConditionSetDetailsView>();
			_selectService.Register<PaymentConditionSetListView, PaymentConditionSet>();
            _updateDetailsService.Register<PaymentConditionSet, PaymentConditionSetDetailsView>();

            Container.RegisterViewForNavigation<ProductBlockListView>();
            //_dialogService.Register<ProductBlockDetailsViewModel, ProductBlockDetailsView>();
			_selectService.Register<ProductBlockListView, ProductBlock>();
            _updateDetailsService.Register<ProductBlock, ProductBlockDetailsView>();

            Container.RegisterViewForNavigation<ProductDependentListView>();
            //_dialogService.Register<ProductDependentDetailsViewModel, ProductDependentDetailsView>();
			_selectService.Register<ProductDependentListView, ProductDependent>();
            _updateDetailsService.Register<ProductDependent, ProductDependentDetailsView>();

            Container.RegisterViewForNavigation<SalesBlockListView>();
            //_dialogService.Register<SalesBlockDetailsViewModel, SalesBlockDetailsView>();
			_selectService.Register<SalesBlockListView, SalesBlock>();
            _updateDetailsService.Register<SalesBlock, SalesBlockDetailsView>();

            Container.RegisterViewForNavigation<BankDetailsListView>();
            //_dialogService.Register<BankDetailsDetailsViewModel, BankDetailsDetailsView>();
			_selectService.Register<BankDetailsListView, BankDetails>();
            _updateDetailsService.Register<BankDetails, BankDetailsDetailsView>();

            Container.RegisterViewForNavigation<CompanyListView>();
            //_dialogService.Register<CompanyDetailsViewModel, CompanyDetailsView>();
			_selectService.Register<CompanyListView, Company>();
            _updateDetailsService.Register<Company, CompanyDetailsView>();

            Container.RegisterViewForNavigation<CompanyFormListView>();
            //_dialogService.Register<CompanyFormDetailsViewModel, CompanyFormDetailsView>();
			_selectService.Register<CompanyFormListView, CompanyForm>();
            _updateDetailsService.Register<CompanyForm, CompanyFormDetailsView>();

            Container.RegisterViewForNavigation<DocumentsRegistrationDetailsListView>();
            //_dialogService.Register<DocumentsRegistrationDetailsDetailsViewModel, DocumentsRegistrationDetailsDetailsView>();
			_selectService.Register<DocumentsRegistrationDetailsListView, DocumentsRegistrationDetails>();
            _updateDetailsService.Register<DocumentsRegistrationDetails, DocumentsRegistrationDetailsDetailsView>();

            Container.RegisterViewForNavigation<EmployeesPositionListView>();
            //_dialogService.Register<EmployeesPositionDetailsViewModel, EmployeesPositionDetailsView>();
			_selectService.Register<EmployeesPositionListView, EmployeesPosition>();
            _updateDetailsService.Register<EmployeesPosition, EmployeesPositionDetailsView>();

            Container.RegisterViewForNavigation<FacilityTypeListView>();
            //_dialogService.Register<FacilityTypeDetailsViewModel, FacilityTypeDetailsView>();
			_selectService.Register<FacilityTypeListView, FacilityType>();
            _updateDetailsService.Register<FacilityType, FacilityTypeDetailsView>();

            Container.RegisterViewForNavigation<ActivityFieldListView>();
            //_dialogService.Register<ActivityFieldDetailsViewModel, ActivityFieldDetailsView>();
			_selectService.Register<ActivityFieldListView, ActivityField>();
            _updateDetailsService.Register<ActivityField, ActivityFieldDetailsView>();

            Container.RegisterViewForNavigation<ContractListView>();
            //_dialogService.Register<ContractDetailsViewModel, ContractDetailsView>();
			_selectService.Register<ContractListView, Contract>();
            _updateDetailsService.Register<Contract, ContractDetailsView>();

            Container.RegisterViewForNavigation<MeasureListView>();
            //_dialogService.Register<MeasureDetailsViewModel, MeasureDetailsView>();
			_selectService.Register<MeasureListView, Measure>();
            _updateDetailsService.Register<Measure, MeasureDetailsView>();

            Container.RegisterViewForNavigation<ParameterListView>();
            //_dialogService.Register<ParameterDetailsViewModel, ParameterDetailsView>();
			_selectService.Register<ParameterListView, Parameter>();
            _updateDetailsService.Register<Parameter, ParameterDetailsView>();

            Container.RegisterViewForNavigation<ParameterGroupListView>();
            //_dialogService.Register<ParameterGroupDetailsViewModel, ParameterGroupDetailsView>();
			_selectService.Register<ParameterGroupListView, ParameterGroup>();
            _updateDetailsService.Register<ParameterGroup, ParameterGroupDetailsView>();

            Container.RegisterViewForNavigation<ProductRelationListView>();
            //_dialogService.Register<ProductRelationDetailsViewModel, ProductRelationDetailsView>();
			_selectService.Register<ProductRelationListView, ProductRelation>();
            _updateDetailsService.Register<ProductRelation, ProductRelationDetailsView>();

            Container.RegisterViewForNavigation<PersonListView>();
            //_dialogService.Register<PersonDetailsViewModel, PersonDetailsView>();
			_selectService.Register<PersonListView, Person>();
            _updateDetailsService.Register<Person, PersonDetailsView>();

            Container.RegisterViewForNavigation<PaymentPlannedListListView>();
            //_dialogService.Register<PaymentPlannedListDetailsViewModel, PaymentPlannedListDetailsView>();
			_selectService.Register<PaymentPlannedListListView, PaymentPlannedList>();
            _updateDetailsService.Register<PaymentPlannedList, PaymentPlannedListDetailsView>();

            Container.RegisterViewForNavigation<PaymentPlannedListView>();
            //_dialogService.Register<PaymentPlannedDetailsViewModel, PaymentPlannedDetailsView>();
			_selectService.Register<PaymentPlannedListView, PaymentPlanned>();
            _updateDetailsService.Register<PaymentPlanned, PaymentPlannedDetailsView>();

            Container.RegisterViewForNavigation<PaymentActualListView>();
            //_dialogService.Register<PaymentActualDetailsViewModel, PaymentActualDetailsView>();
			_selectService.Register<PaymentActualListView, PaymentActual>();
            _updateDetailsService.Register<PaymentActual, PaymentActualDetailsView>();

            Container.RegisterViewForNavigation<ParameterRelationListView>();
            //_dialogService.Register<ParameterRelationDetailsViewModel, ParameterRelationDetailsView>();
			_selectService.Register<ParameterRelationListView, ParameterRelation>();
            _updateDetailsService.Register<ParameterRelation, ParameterRelationDetailsView>();

            Container.RegisterViewForNavigation<SalesUnitListView>();
            //_dialogService.Register<SalesUnitDetailsViewModel, SalesUnitDetailsView>();
			_selectService.Register<SalesUnitListView, SalesUnit>();
            _updateDetailsService.Register<SalesUnit, SalesUnitDetailsView>();

            Container.RegisterViewForNavigation<ServiceListView>();
            //_dialogService.Register<ServiceDetailsViewModel, ServiceDetailsView>();
			_selectService.Register<ServiceListView, Service>();
            _updateDetailsService.Register<Service, ServiceDetailsView>();

            Container.RegisterViewForNavigation<TestFriendAddressListView>();
            //_dialogService.Register<TestFriendAddressDetailsViewModel, TestFriendAddressDetailsView>();
			_selectService.Register<TestFriendAddressListView, TestFriendAddress>();
            _updateDetailsService.Register<TestFriendAddress, TestFriendAddressDetailsView>();

            Container.RegisterViewForNavigation<TestFriendListView>();
            //_dialogService.Register<TestFriendDetailsViewModel, TestFriendDetailsView>();
			_selectService.Register<TestFriendListView, TestFriend>();
            _updateDetailsService.Register<TestFriend, TestFriendDetailsView>();

            Container.RegisterViewForNavigation<TestFriendEmailListView>();
            //_dialogService.Register<TestFriendEmailDetailsViewModel, TestFriendEmailDetailsView>();
			_selectService.Register<TestFriendEmailListView, TestFriendEmail>();
            _updateDetailsService.Register<TestFriendEmail, TestFriendEmailDetailsView>();

            Container.RegisterViewForNavigation<TestFriendGroupListView>();
            //_dialogService.Register<TestFriendGroupDetailsViewModel, TestFriendGroupDetailsView>();
			_selectService.Register<TestFriendGroupListView, TestFriendGroup>();
            _updateDetailsService.Register<TestFriendGroup, TestFriendGroupDetailsView>();

            Container.RegisterViewForNavigation<DocumentListView>();
            //_dialogService.Register<DocumentDetailsViewModel, DocumentDetailsView>();
			_selectService.Register<DocumentListView, Document>();
            _updateDetailsService.Register<Document, DocumentDetailsView>();

            Container.RegisterViewForNavigation<TestEntityListView>();
            //_dialogService.Register<TestEntityDetailsViewModel, TestEntityDetailsView>();
			_selectService.Register<TestEntityListView, TestEntity>();
            _updateDetailsService.Register<TestEntity, TestEntityDetailsView>();

            Container.RegisterViewForNavigation<TestHusbandListView>();
            //_dialogService.Register<TestHusbandDetailsViewModel, TestHusbandDetailsView>();
			_selectService.Register<TestHusbandListView, TestHusband>();
            _updateDetailsService.Register<TestHusband, TestHusbandDetailsView>();

            Container.RegisterViewForNavigation<TestWifeListView>();
            //_dialogService.Register<TestWifeDetailsViewModel, TestWifeDetailsView>();
			_selectService.Register<TestWifeListView, TestWife>();
            _updateDetailsService.Register<TestWife, TestWifeDetailsView>();

            Container.RegisterViewForNavigation<TestChildListView>();
            //_dialogService.Register<TestChildDetailsViewModel, TestChildDetailsView>();
			_selectService.Register<TestChildListView, TestChild>();
            _updateDetailsService.Register<TestChild, TestChildDetailsView>();

            Container.RegisterViewForNavigation<CostOnDateListView>();
            //_dialogService.Register<CostOnDateDetailsViewModel, CostOnDateDetailsView>();
			_selectService.Register<CostOnDateListView, CostOnDate>();
            _updateDetailsService.Register<CostOnDate, CostOnDateDetailsView>();

            Container.RegisterViewForNavigation<ProductListView>();
            //_dialogService.Register<ProductDetailsViewModel, ProductDetailsView>();
			_selectService.Register<ProductListView, Product>();
            _updateDetailsService.Register<Product, ProductDetailsView>();

            Container.RegisterViewForNavigation<OfferListView>();
            //_dialogService.Register<OfferDetailsViewModel, OfferDetailsView>();
			_selectService.Register<OfferListView, Offer>();
            _updateDetailsService.Register<Offer, OfferDetailsView>();

            Container.RegisterViewForNavigation<EmployeeListView>();
            //_dialogService.Register<EmployeeDetailsViewModel, EmployeeDetailsView>();
			_selectService.Register<EmployeeListView, Employee>();
            _updateDetailsService.Register<Employee, EmployeeDetailsView>();

            Container.RegisterViewForNavigation<OrderListView>();
            //_dialogService.Register<OrderDetailsViewModel, OrderDetailsView>();
			_selectService.Register<OrderListView, Order>();
            _updateDetailsService.Register<Order, OrderDetailsView>();

            Container.RegisterViewForNavigation<PaymentConditionListView>();
            //_dialogService.Register<PaymentConditionDetailsViewModel, PaymentConditionDetailsView>();
			_selectService.Register<PaymentConditionListView, PaymentCondition>();
            _updateDetailsService.Register<PaymentCondition, PaymentConditionDetailsView>();

            Container.RegisterViewForNavigation<PaymentDocumentListView>();
            //_dialogService.Register<PaymentDocumentDetailsViewModel, PaymentDocumentDetailsView>();
			_selectService.Register<PaymentDocumentListView, PaymentDocument>();
            _updateDetailsService.Register<PaymentDocument, PaymentDocumentDetailsView>();

            Container.RegisterViewForNavigation<FacilityListView>();
            //_dialogService.Register<FacilityDetailsViewModel, FacilityDetailsView>();
			_selectService.Register<FacilityListView, Facility>();
            _updateDetailsService.Register<Facility, FacilityDetailsView>();

            Container.RegisterViewForNavigation<ProjectListView>();
            //_dialogService.Register<ProjectDetailsViewModel, ProjectDetailsView>();
			_selectService.Register<ProjectListView, Project>();
            _updateDetailsService.Register<Project, ProjectDetailsView>();

            Container.RegisterViewForNavigation<UserRoleListView>();
            //_dialogService.Register<UserRoleDetailsViewModel, UserRoleDetailsView>();
			_selectService.Register<UserRoleListView, UserRole>();
            _updateDetailsService.Register<UserRole, UserRoleDetailsView>();

            Container.RegisterViewForNavigation<SpecificationListView>();
            //_dialogService.Register<SpecificationDetailsViewModel, SpecificationDetailsView>();
			_selectService.Register<SpecificationListView, Specification>();
            _updateDetailsService.Register<Specification, SpecificationDetailsView>();

            Container.RegisterViewForNavigation<TenderListView>();
            //_dialogService.Register<TenderDetailsViewModel, TenderDetailsView>();
			_selectService.Register<TenderListView, Tender>();
            _updateDetailsService.Register<Tender, TenderDetailsView>();

            Container.RegisterViewForNavigation<TenderTypeListView>();
            //_dialogService.Register<TenderTypeDetailsViewModel, TenderTypeDetailsView>();
			_selectService.Register<TenderTypeListView, TenderType>();
            _updateDetailsService.Register<TenderType, TenderTypeDetailsView>();

            Container.RegisterViewForNavigation<UserListView>();
            //_dialogService.Register<UserDetailsViewModel, UserDetailsView>();
			_selectService.Register<UserListView, User>();
            _updateDetailsService.Register<User, UserDetailsView>();

		}
	}
}
