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
            Container.RegisterViewForNavigation<CreateNewProductTaskLookupListView>();
            //_dialogService.Register<CreateNewProductTaskDetailsViewModel, CreateNewProductTaskDetailsView>();
			_selectService.Register<CreateNewProductTaskLookupListView, CreateNewProductTask>();
            _updateDetailsService.Register<CreateNewProductTask, CreateNewProductTaskDetailsView>();

            Container.RegisterViewForNavigation<PaymentActualLookupListView>();
            //_dialogService.Register<PaymentActualDetailsViewModel, PaymentActualDetailsView>();
			_selectService.Register<PaymentActualLookupListView, PaymentActual>();
            _updateDetailsService.Register<PaymentActual, PaymentActualDetailsView>();

            Container.RegisterViewForNavigation<PaymentPlannedLookupListView>();
            //_dialogService.Register<PaymentPlannedDetailsViewModel, PaymentPlannedDetailsView>();
			_selectService.Register<PaymentPlannedLookupListView, PaymentPlanned>();
            _updateDetailsService.Register<PaymentPlanned, PaymentPlannedDetailsView>();

            Container.RegisterViewForNavigation<ProductBlockIsServiceLookupListView>();
            //_dialogService.Register<ProductBlockIsServiceDetailsViewModel, ProductBlockIsServiceDetailsView>();
			_selectService.Register<ProductBlockIsServiceLookupListView, ProductBlockIsService>();
            _updateDetailsService.Register<ProductBlockIsService, ProductBlockIsServiceDetailsView>();

            Container.RegisterViewForNavigation<ProductIncludedLookupListView>();
            //_dialogService.Register<ProductIncludedDetailsViewModel, ProductIncludedDetailsView>();
			_selectService.Register<ProductIncludedLookupListView, ProductIncluded>();
            _updateDetailsService.Register<ProductIncluded, ProductIncludedDetailsView>();

            Container.RegisterViewForNavigation<ProductDesignationLookupListView>();
            //_dialogService.Register<ProductDesignationDetailsViewModel, ProductDesignationDetailsView>();
			_selectService.Register<ProductDesignationLookupListView, ProductDesignation>();
            _updateDetailsService.Register<ProductDesignation, ProductDesignationDetailsView>();

            Container.RegisterViewForNavigation<ProductTypeLookupListView>();
            //_dialogService.Register<ProductTypeDetailsViewModel, ProductTypeDetailsView>();
			_selectService.Register<ProductTypeLookupListView, ProductType>();
            _updateDetailsService.Register<ProductType, ProductTypeDetailsView>();

            Container.RegisterViewForNavigation<ProductTypeDesignationLookupListView>();
            //_dialogService.Register<ProductTypeDesignationDetailsViewModel, ProductTypeDesignationDetailsView>();
			_selectService.Register<ProductTypeDesignationLookupListView, ProductTypeDesignation>();
            _updateDetailsService.Register<ProductTypeDesignation, ProductTypeDesignationDetailsView>();

            Container.RegisterViewForNavigation<ProjectTypeLookupListView>();
            //_dialogService.Register<ProjectTypeDetailsViewModel, ProjectTypeDetailsView>();
			_selectService.Register<ProjectTypeLookupListView, ProjectType>();
            _updateDetailsService.Register<ProjectType, ProjectTypeDetailsView>();

            Container.RegisterViewForNavigation<CommonOptionLookupListView>();
            //_dialogService.Register<CommonOptionDetailsViewModel, CommonOptionDetailsView>();
			_selectService.Register<CommonOptionLookupListView, CommonOption>();
            _updateDetailsService.Register<CommonOption, CommonOptionDetailsView>();

            Container.RegisterViewForNavigation<AddressLookupListView>();
            //_dialogService.Register<AddressDetailsViewModel, AddressDetailsView>();
			_selectService.Register<AddressLookupListView, Address>();
            _updateDetailsService.Register<Address, AddressDetailsView>();

            Container.RegisterViewForNavigation<CountryLookupListView>();
            //_dialogService.Register<CountryDetailsViewModel, CountryDetailsView>();
			_selectService.Register<CountryLookupListView, Country>();
            _updateDetailsService.Register<Country, CountryDetailsView>();

            Container.RegisterViewForNavigation<DistrictLookupListView>();
            //_dialogService.Register<DistrictDetailsViewModel, DistrictDetailsView>();
			_selectService.Register<DistrictLookupListView, District>();
            _updateDetailsService.Register<District, DistrictDetailsView>();

            Container.RegisterViewForNavigation<LocalityLookupListView>();
            //_dialogService.Register<LocalityDetailsViewModel, LocalityDetailsView>();
			_selectService.Register<LocalityLookupListView, Locality>();
            _updateDetailsService.Register<Locality, LocalityDetailsView>();

            Container.RegisterViewForNavigation<LocalityTypeLookupListView>();
            //_dialogService.Register<LocalityTypeDetailsViewModel, LocalityTypeDetailsView>();
			_selectService.Register<LocalityTypeLookupListView, LocalityType>();
            _updateDetailsService.Register<LocalityType, LocalityTypeDetailsView>();

            Container.RegisterViewForNavigation<RegionLookupListView>();
            //_dialogService.Register<RegionDetailsViewModel, RegionDetailsView>();
			_selectService.Register<RegionLookupListView, Region>();
            _updateDetailsService.Register<Region, RegionDetailsView>();

            Container.RegisterViewForNavigation<CalculatePriceTaskLookupListView>();
            //_dialogService.Register<CalculatePriceTaskDetailsViewModel, CalculatePriceTaskDetailsView>();
			_selectService.Register<CalculatePriceTaskLookupListView, CalculatePriceTask>();
            _updateDetailsService.Register<CalculatePriceTask, CalculatePriceTaskDetailsView>();

            Container.RegisterViewForNavigation<SumLookupListView>();
            //_dialogService.Register<SumDetailsViewModel, SumDetailsView>();
			_selectService.Register<SumLookupListView, Sum>();
            _updateDetailsService.Register<Sum, SumDetailsView>();

            Container.RegisterViewForNavigation<CurrencyExchangeRateLookupListView>();
            //_dialogService.Register<CurrencyExchangeRateDetailsViewModel, CurrencyExchangeRateDetailsView>();
			_selectService.Register<CurrencyExchangeRateLookupListView, CurrencyExchangeRate>();
            _updateDetailsService.Register<CurrencyExchangeRate, CurrencyExchangeRateDetailsView>();

            Container.RegisterViewForNavigation<DescribeProductBlockTaskLookupListView>();
            //_dialogService.Register<DescribeProductBlockTaskDetailsViewModel, DescribeProductBlockTaskDetailsView>();
			_selectService.Register<DescribeProductBlockTaskLookupListView, DescribeProductBlockTask>();
            _updateDetailsService.Register<DescribeProductBlockTask, DescribeProductBlockTaskDetailsView>();

            Container.RegisterViewForNavigation<NoteLookupListView>();
            //_dialogService.Register<NoteDetailsViewModel, NoteDetailsView>();
			_selectService.Register<NoteLookupListView, Note>();
            _updateDetailsService.Register<Note, NoteDetailsView>();

            Container.RegisterViewForNavigation<OfferUnitLookupListView>();
            //_dialogService.Register<OfferUnitDetailsViewModel, OfferUnitDetailsView>();
			_selectService.Register<OfferUnitLookupListView, OfferUnit>();
            _updateDetailsService.Register<OfferUnit, OfferUnitDetailsView>();

            Container.RegisterViewForNavigation<PaymentConditionSetLookupListView>();
            //_dialogService.Register<PaymentConditionSetDetailsViewModel, PaymentConditionSetDetailsView>();
			_selectService.Register<PaymentConditionSetLookupListView, PaymentConditionSet>();
            _updateDetailsService.Register<PaymentConditionSet, PaymentConditionSetDetailsView>();

            Container.RegisterViewForNavigation<ProductBlockLookupListView>();
            //_dialogService.Register<ProductBlockDetailsViewModel, ProductBlockDetailsView>();
			_selectService.Register<ProductBlockLookupListView, ProductBlock>();
            _updateDetailsService.Register<ProductBlock, ProductBlockDetailsView>();

            Container.RegisterViewForNavigation<ProductDependentLookupListView>();
            //_dialogService.Register<ProductDependentDetailsViewModel, ProductDependentDetailsView>();
			_selectService.Register<ProductDependentLookupListView, ProductDependent>();
            _updateDetailsService.Register<ProductDependent, ProductDependentDetailsView>();

            Container.RegisterViewForNavigation<ProductionTaskLookupListView>();
            //_dialogService.Register<ProductionTaskDetailsViewModel, ProductionTaskDetailsView>();
			_selectService.Register<ProductionTaskLookupListView, ProductionTask>();
            _updateDetailsService.Register<ProductionTask, ProductionTaskDetailsView>();

            Container.RegisterViewForNavigation<SalesBlockLookupListView>();
            //_dialogService.Register<SalesBlockDetailsViewModel, SalesBlockDetailsView>();
			_selectService.Register<SalesBlockLookupListView, SalesBlock>();
            _updateDetailsService.Register<SalesBlock, SalesBlockDetailsView>();

            Container.RegisterViewForNavigation<BankDetailsLookupListView>();
            //_dialogService.Register<BankDetailsDetailsViewModel, BankDetailsDetailsView>();
			_selectService.Register<BankDetailsLookupListView, BankDetails>();
            _updateDetailsService.Register<BankDetails, BankDetailsDetailsView>();

            Container.RegisterViewForNavigation<CompanyLookupListView>();
            //_dialogService.Register<CompanyDetailsViewModel, CompanyDetailsView>();
			_selectService.Register<CompanyLookupListView, Company>();
            _updateDetailsService.Register<Company, CompanyDetailsView>();

            Container.RegisterViewForNavigation<CompanyFormLookupListView>();
            //_dialogService.Register<CompanyFormDetailsViewModel, CompanyFormDetailsView>();
			_selectService.Register<CompanyFormLookupListView, CompanyForm>();
            _updateDetailsService.Register<CompanyForm, CompanyFormDetailsView>();

            Container.RegisterViewForNavigation<DocumentsRegistrationDetailsLookupListView>();
            //_dialogService.Register<DocumentsRegistrationDetailsDetailsViewModel, DocumentsRegistrationDetailsDetailsView>();
			_selectService.Register<DocumentsRegistrationDetailsLookupListView, DocumentsRegistrationDetails>();
            _updateDetailsService.Register<DocumentsRegistrationDetails, DocumentsRegistrationDetailsDetailsView>();

            Container.RegisterViewForNavigation<EmployeesPositionLookupListView>();
            //_dialogService.Register<EmployeesPositionDetailsViewModel, EmployeesPositionDetailsView>();
			_selectService.Register<EmployeesPositionLookupListView, EmployeesPosition>();
            _updateDetailsService.Register<EmployeesPosition, EmployeesPositionDetailsView>();

            Container.RegisterViewForNavigation<FacilityTypeLookupListView>();
            //_dialogService.Register<FacilityTypeDetailsViewModel, FacilityTypeDetailsView>();
			_selectService.Register<FacilityTypeLookupListView, FacilityType>();
            _updateDetailsService.Register<FacilityType, FacilityTypeDetailsView>();

            Container.RegisterViewForNavigation<ActivityFieldLookupListView>();
            //_dialogService.Register<ActivityFieldDetailsViewModel, ActivityFieldDetailsView>();
			_selectService.Register<ActivityFieldLookupListView, ActivityField>();
            _updateDetailsService.Register<ActivityField, ActivityFieldDetailsView>();

            Container.RegisterViewForNavigation<ContractLookupListView>();
            //_dialogService.Register<ContractDetailsViewModel, ContractDetailsView>();
			_selectService.Register<ContractLookupListView, Contract>();
            _updateDetailsService.Register<Contract, ContractDetailsView>();

            Container.RegisterViewForNavigation<MeasureLookupListView>();
            //_dialogService.Register<MeasureDetailsViewModel, MeasureDetailsView>();
			_selectService.Register<MeasureLookupListView, Measure>();
            _updateDetailsService.Register<Measure, MeasureDetailsView>();

            Container.RegisterViewForNavigation<ParameterLookupListView>();
            //_dialogService.Register<ParameterDetailsViewModel, ParameterDetailsView>();
			_selectService.Register<ParameterLookupListView, Parameter>();
            _updateDetailsService.Register<Parameter, ParameterDetailsView>();

            Container.RegisterViewForNavigation<ParameterGroupLookupListView>();
            //_dialogService.Register<ParameterGroupDetailsViewModel, ParameterGroupDetailsView>();
			_selectService.Register<ParameterGroupLookupListView, ParameterGroup>();
            _updateDetailsService.Register<ParameterGroup, ParameterGroupDetailsView>();

            Container.RegisterViewForNavigation<ProductRelationLookupListView>();
            //_dialogService.Register<ProductRelationDetailsViewModel, ProductRelationDetailsView>();
			_selectService.Register<ProductRelationLookupListView, ProductRelation>();
            _updateDetailsService.Register<ProductRelation, ProductRelationDetailsView>();

            Container.RegisterViewForNavigation<PersonLookupListView>();
            //_dialogService.Register<PersonDetailsViewModel, PersonDetailsView>();
			_selectService.Register<PersonLookupListView, Person>();
            _updateDetailsService.Register<Person, PersonDetailsView>();

            Container.RegisterViewForNavigation<ParameterRelationLookupListView>();
            //_dialogService.Register<ParameterRelationDetailsViewModel, ParameterRelationDetailsView>();
			_selectService.Register<ParameterRelationLookupListView, ParameterRelation>();
            _updateDetailsService.Register<ParameterRelation, ParameterRelationDetailsView>();

            Container.RegisterViewForNavigation<SalesUnitLookupListView>();
            //_dialogService.Register<SalesUnitDetailsViewModel, SalesUnitDetailsView>();
			_selectService.Register<SalesUnitLookupListView, SalesUnit>();
            _updateDetailsService.Register<SalesUnit, SalesUnitDetailsView>();

            Container.RegisterViewForNavigation<TestFriendAddressLookupListView>();
            //_dialogService.Register<TestFriendAddressDetailsViewModel, TestFriendAddressDetailsView>();
			_selectService.Register<TestFriendAddressLookupListView, TestFriendAddress>();
            _updateDetailsService.Register<TestFriendAddress, TestFriendAddressDetailsView>();

            Container.RegisterViewForNavigation<TestFriendLookupListView>();
            //_dialogService.Register<TestFriendDetailsViewModel, TestFriendDetailsView>();
			_selectService.Register<TestFriendLookupListView, TestFriend>();
            _updateDetailsService.Register<TestFriend, TestFriendDetailsView>();

            Container.RegisterViewForNavigation<TestFriendEmailLookupListView>();
            //_dialogService.Register<TestFriendEmailDetailsViewModel, TestFriendEmailDetailsView>();
			_selectService.Register<TestFriendEmailLookupListView, TestFriendEmail>();
            _updateDetailsService.Register<TestFriendEmail, TestFriendEmailDetailsView>();

            Container.RegisterViewForNavigation<TestFriendGroupLookupListView>();
            //_dialogService.Register<TestFriendGroupDetailsViewModel, TestFriendGroupDetailsView>();
			_selectService.Register<TestFriendGroupLookupListView, TestFriendGroup>();
            _updateDetailsService.Register<TestFriendGroup, TestFriendGroupDetailsView>();

            Container.RegisterViewForNavigation<DocumentLookupListView>();
            //_dialogService.Register<DocumentDetailsViewModel, DocumentDetailsView>();
			_selectService.Register<DocumentLookupListView, Document>();
            _updateDetailsService.Register<Document, DocumentDetailsView>();

            Container.RegisterViewForNavigation<TestEntityLookupListView>();
            //_dialogService.Register<TestEntityDetailsViewModel, TestEntityDetailsView>();
			_selectService.Register<TestEntityLookupListView, TestEntity>();
            _updateDetailsService.Register<TestEntity, TestEntityDetailsView>();

            Container.RegisterViewForNavigation<TestHusbandLookupListView>();
            //_dialogService.Register<TestHusbandDetailsViewModel, TestHusbandDetailsView>();
			_selectService.Register<TestHusbandLookupListView, TestHusband>();
            _updateDetailsService.Register<TestHusband, TestHusbandDetailsView>();

            Container.RegisterViewForNavigation<TestWifeLookupListView>();
            //_dialogService.Register<TestWifeDetailsViewModel, TestWifeDetailsView>();
			_selectService.Register<TestWifeLookupListView, TestWife>();
            _updateDetailsService.Register<TestWife, TestWifeDetailsView>();

            Container.RegisterViewForNavigation<TestChildLookupListView>();
            //_dialogService.Register<TestChildDetailsViewModel, TestChildDetailsView>();
			_selectService.Register<TestChildLookupListView, TestChild>();
            _updateDetailsService.Register<TestChild, TestChildDetailsView>();

            Container.RegisterViewForNavigation<SumOnDateLookupListView>();
            //_dialogService.Register<SumOnDateDetailsViewModel, SumOnDateDetailsView>();
			_selectService.Register<SumOnDateLookupListView, SumOnDate>();
            _updateDetailsService.Register<SumOnDate, SumOnDateDetailsView>();

            Container.RegisterViewForNavigation<ProductLookupListView>();
            //_dialogService.Register<ProductDetailsViewModel, ProductDetailsView>();
			_selectService.Register<ProductLookupListView, Product>();
            _updateDetailsService.Register<Product, ProductDetailsView>();

            Container.RegisterViewForNavigation<OfferLookupListView>();
            //_dialogService.Register<OfferDetailsViewModel, OfferDetailsView>();
			_selectService.Register<OfferLookupListView, Offer>();
            _updateDetailsService.Register<Offer, OfferDetailsView>();

            Container.RegisterViewForNavigation<EmployeeLookupListView>();
            //_dialogService.Register<EmployeeDetailsViewModel, EmployeeDetailsView>();
			_selectService.Register<EmployeeLookupListView, Employee>();
            _updateDetailsService.Register<Employee, EmployeeDetailsView>();

            Container.RegisterViewForNavigation<OrderLookupListView>();
            //_dialogService.Register<OrderDetailsViewModel, OrderDetailsView>();
			_selectService.Register<OrderLookupListView, Order>();
            _updateDetailsService.Register<Order, OrderDetailsView>();

            Container.RegisterViewForNavigation<PaymentConditionLookupListView>();
            //_dialogService.Register<PaymentConditionDetailsViewModel, PaymentConditionDetailsView>();
			_selectService.Register<PaymentConditionLookupListView, PaymentCondition>();
            _updateDetailsService.Register<PaymentCondition, PaymentConditionDetailsView>();

            Container.RegisterViewForNavigation<PaymentDocumentLookupListView>();
            //_dialogService.Register<PaymentDocumentDetailsViewModel, PaymentDocumentDetailsView>();
			_selectService.Register<PaymentDocumentLookupListView, PaymentDocument>();
            _updateDetailsService.Register<PaymentDocument, PaymentDocumentDetailsView>();

            Container.RegisterViewForNavigation<FacilityLookupListView>();
            //_dialogService.Register<FacilityDetailsViewModel, FacilityDetailsView>();
			_selectService.Register<FacilityLookupListView, Facility>();
            _updateDetailsService.Register<Facility, FacilityDetailsView>();

            Container.RegisterViewForNavigation<ProjectLookupListView>();
            //_dialogService.Register<ProjectDetailsViewModel, ProjectDetailsView>();
			_selectService.Register<ProjectLookupListView, Project>();
            _updateDetailsService.Register<Project, ProjectDetailsView>();

            Container.RegisterViewForNavigation<UserRoleLookupListView>();
            //_dialogService.Register<UserRoleDetailsViewModel, UserRoleDetailsView>();
			_selectService.Register<UserRoleLookupListView, UserRole>();
            _updateDetailsService.Register<UserRole, UserRoleDetailsView>();

            Container.RegisterViewForNavigation<SpecificationLookupListView>();
            //_dialogService.Register<SpecificationDetailsViewModel, SpecificationDetailsView>();
			_selectService.Register<SpecificationLookupListView, Specification>();
            _updateDetailsService.Register<Specification, SpecificationDetailsView>();

            Container.RegisterViewForNavigation<TenderLookupListView>();
            //_dialogService.Register<TenderDetailsViewModel, TenderDetailsView>();
			_selectService.Register<TenderLookupListView, Tender>();
            _updateDetailsService.Register<Tender, TenderDetailsView>();

            Container.RegisterViewForNavigation<TenderTypeLookupListView>();
            //_dialogService.Register<TenderTypeDetailsViewModel, TenderTypeDetailsView>();
			_selectService.Register<TenderTypeLookupListView, TenderType>();
            _updateDetailsService.Register<TenderType, TenderTypeDetailsView>();

            Container.RegisterViewForNavigation<UserLookupListView>();
            //_dialogService.Register<UserDetailsViewModel, UserDetailsView>();
			_selectService.Register<UserLookupListView, User>();
            _updateDetailsService.Register<User, UserDetailsView>();

		}
	}
}
