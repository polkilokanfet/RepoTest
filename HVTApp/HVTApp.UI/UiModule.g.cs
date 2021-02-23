










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

            Container.RegisterViewForNavigation<CountryUnionLookupListView>();
            //_dialogService.Register<CountryUnionDetailsViewModel, CountryUnionDetailsView>();
			_selectService.Register<CountryUnionLookupListView, CountryUnion>();
            _updateDetailsService.Register<CountryUnion, CountryUnionDetailsView>();

            Container.RegisterViewForNavigation<BankGuaranteeLookupListView>();
            //_dialogService.Register<BankGuaranteeDetailsViewModel, BankGuaranteeDetailsView>();
			_selectService.Register<BankGuaranteeLookupListView, BankGuarantee>();
            _updateDetailsService.Register<BankGuarantee, BankGuaranteeDetailsView>();

            Container.RegisterViewForNavigation<BankGuaranteeTypeLookupListView>();
            //_dialogService.Register<BankGuaranteeTypeDetailsViewModel, BankGuaranteeTypeDetailsView>();
			_selectService.Register<BankGuaranteeTypeLookupListView, BankGuaranteeType>();
            _updateDetailsService.Register<BankGuaranteeType, BankGuaranteeTypeDetailsView>();

            Container.RegisterViewForNavigation<BudgetLookupListView>();
            //_dialogService.Register<BudgetDetailsViewModel, BudgetDetailsView>();
			_selectService.Register<BudgetLookupListView, Budget>();
            _updateDetailsService.Register<Budget, BudgetDetailsView>();

            Container.RegisterViewForNavigation<BudgetUnitLookupListView>();
            //_dialogService.Register<BudgetUnitDetailsViewModel, BudgetUnitDetailsView>();
			_selectService.Register<BudgetUnitLookupListView, BudgetUnit>();
            _updateDetailsService.Register<BudgetUnit, BudgetUnitDetailsView>();

            Container.RegisterViewForNavigation<ConstructorParametersListLookupListView>();
            //_dialogService.Register<ConstructorParametersListDetailsViewModel, ConstructorParametersListDetailsView>();
			_selectService.Register<ConstructorParametersListLookupListView, ConstructorParametersList>();
            _updateDetailsService.Register<ConstructorParametersList, ConstructorParametersListDetailsView>();

            Container.RegisterViewForNavigation<ConstructorsParametersLookupListView>();
            //_dialogService.Register<ConstructorsParametersDetailsViewModel, ConstructorsParametersDetailsView>();
			_selectService.Register<ConstructorsParametersLookupListView, ConstructorsParameters>();
            _updateDetailsService.Register<ConstructorsParameters, ConstructorsParametersDetailsView>();

            Container.RegisterViewForNavigation<CreateNewProductTaskLookupListView>();
            //_dialogService.Register<CreateNewProductTaskDetailsViewModel, CreateNewProductTaskDetailsView>();
			_selectService.Register<CreateNewProductTaskLookupListView, CreateNewProductTask>();
            _updateDetailsService.Register<CreateNewProductTask, CreateNewProductTaskDetailsView>();

            Container.RegisterViewForNavigation<DirectumTaskLookupListView>();
            //_dialogService.Register<DirectumTaskDetailsViewModel, DirectumTaskDetailsView>();
			_selectService.Register<DirectumTaskLookupListView, DirectumTask>();
            _updateDetailsService.Register<DirectumTask, DirectumTaskDetailsView>();

            Container.RegisterViewForNavigation<DirectumTaskGroupLookupListView>();
            //_dialogService.Register<DirectumTaskGroupDetailsViewModel, DirectumTaskGroupDetailsView>();
			_selectService.Register<DirectumTaskGroupLookupListView, DirectumTaskGroup>();
            _updateDetailsService.Register<DirectumTaskGroup, DirectumTaskGroupDetailsView>();

            Container.RegisterViewForNavigation<DirectumTaskGroupFileLookupListView>();
            //_dialogService.Register<DirectumTaskGroupFileDetailsViewModel, DirectumTaskGroupFileDetailsView>();
			_selectService.Register<DirectumTaskGroupFileLookupListView, DirectumTaskGroupFile>();
            _updateDetailsService.Register<DirectumTaskGroupFile, DirectumTaskGroupFileDetailsView>();

            Container.RegisterViewForNavigation<DirectumTaskMessageLookupListView>();
            //_dialogService.Register<DirectumTaskMessageDetailsViewModel, DirectumTaskMessageDetailsView>();
			_selectService.Register<DirectumTaskMessageLookupListView, DirectumTaskMessage>();
            _updateDetailsService.Register<DirectumTaskMessage, DirectumTaskMessageDetailsView>();

            Container.RegisterViewForNavigation<DocumentNumberLookupListView>();
            //_dialogService.Register<DocumentNumberDetailsViewModel, DocumentNumberDetailsView>();
			_selectService.Register<DocumentNumberLookupListView, DocumentNumber>();
            _updateDetailsService.Register<DocumentNumber, DocumentNumberDetailsView>();

            Container.RegisterViewForNavigation<IncomingRequestLookupListView>();
            //_dialogService.Register<IncomingRequestDetailsViewModel, IncomingRequestDetailsView>();
			_selectService.Register<IncomingRequestLookupListView, IncomingRequest>();
            _updateDetailsService.Register<IncomingRequest, IncomingRequestDetailsView>();

            Container.RegisterViewForNavigation<LosingReasonLookupListView>();
            //_dialogService.Register<LosingReasonDetailsViewModel, LosingReasonDetailsView>();
			_selectService.Register<LosingReasonLookupListView, LosingReason>();
            _updateDetailsService.Register<LosingReason, LosingReasonDetailsView>();

            Container.RegisterViewForNavigation<MarketFieldLookupListView>();
            //_dialogService.Register<MarketFieldDetailsViewModel, MarketFieldDetailsView>();
			_selectService.Register<MarketFieldLookupListView, MarketField>();
            _updateDetailsService.Register<MarketField, MarketFieldDetailsView>();

            Container.RegisterViewForNavigation<PaymentActualLookupListView>();
            //_dialogService.Register<PaymentActualDetailsViewModel, PaymentActualDetailsView>();
			_selectService.Register<PaymentActualLookupListView, PaymentActual>();
            _updateDetailsService.Register<PaymentActual, PaymentActualDetailsView>();

            Container.RegisterViewForNavigation<PaymentConditionPointLookupListView>();
            //_dialogService.Register<PaymentConditionPointDetailsViewModel, PaymentConditionPointDetailsView>();
			_selectService.Register<PaymentConditionPointLookupListView, PaymentConditionPoint>();
            _updateDetailsService.Register<PaymentConditionPoint, PaymentConditionPointDetailsView>();

            Container.RegisterViewForNavigation<PaymentPlannedLookupListView>();
            //_dialogService.Register<PaymentPlannedDetailsViewModel, PaymentPlannedDetailsView>();
			_selectService.Register<PaymentPlannedLookupListView, PaymentPlanned>();
            _updateDetailsService.Register<PaymentPlanned, PaymentPlannedDetailsView>();

            Container.RegisterViewForNavigation<PenaltyLookupListView>();
            //_dialogService.Register<PenaltyDetailsViewModel, PenaltyDetailsView>();
			_selectService.Register<PenaltyLookupListView, Penalty>();
            _updateDetailsService.Register<Penalty, PenaltyDetailsView>();

            Container.RegisterViewForNavigation<PriceCalculationLookupListView>();
            //_dialogService.Register<PriceCalculationDetailsViewModel, PriceCalculationDetailsView>();
			_selectService.Register<PriceCalculationLookupListView, PriceCalculation>();
            _updateDetailsService.Register<PriceCalculation, PriceCalculationDetailsView>();

            Container.RegisterViewForNavigation<PriceCalculationFileLookupListView>();
            //_dialogService.Register<PriceCalculationFileDetailsViewModel, PriceCalculationFileDetailsView>();
			_selectService.Register<PriceCalculationFileLookupListView, PriceCalculationFile>();
            _updateDetailsService.Register<PriceCalculationFile, PriceCalculationFileDetailsView>();

            Container.RegisterViewForNavigation<PriceCalculationItemLookupListView>();
            //_dialogService.Register<PriceCalculationItemDetailsViewModel, PriceCalculationItemDetailsView>();
			_selectService.Register<PriceCalculationItemLookupListView, PriceCalculationItem>();
            _updateDetailsService.Register<PriceCalculationItem, PriceCalculationItemDetailsView>();

            Container.RegisterViewForNavigation<ProductCategoryLookupListView>();
            //_dialogService.Register<ProductCategoryDetailsViewModel, ProductCategoryDetailsView>();
			_selectService.Register<ProductCategoryLookupListView, ProductCategory>();
            _updateDetailsService.Register<ProductCategory, ProductCategoryDetailsView>();

            Container.RegisterViewForNavigation<ProductCategoryPriceAndCostLookupListView>();
            //_dialogService.Register<ProductCategoryPriceAndCostDetailsViewModel, ProductCategoryPriceAndCostDetailsView>();
			_selectService.Register<ProductCategoryPriceAndCostLookupListView, ProductCategoryPriceAndCost>();
            _updateDetailsService.Register<ProductCategoryPriceAndCost, ProductCategoryPriceAndCostDetailsView>();

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

            Container.RegisterViewForNavigation<StandartMarginalIncomeLookupListView>();
            //_dialogService.Register<StandartMarginalIncomeDetailsViewModel, StandartMarginalIncomeDetailsView>();
			_selectService.Register<StandartMarginalIncomeLookupListView, StandartMarginalIncome>();
            _updateDetailsService.Register<StandartMarginalIncome, StandartMarginalIncomeDetailsView>();

            Container.RegisterViewForNavigation<StandartProductionTermLookupListView>();
            //_dialogService.Register<StandartProductionTermDetailsViewModel, StandartProductionTermDetailsView>();
			_selectService.Register<StandartProductionTermLookupListView, StandartProductionTerm>();
            _updateDetailsService.Register<StandartProductionTerm, StandartProductionTermDetailsView>();

            Container.RegisterViewForNavigation<StructureCostLookupListView>();
            //_dialogService.Register<StructureCostDetailsViewModel, StructureCostDetailsView>();
			_selectService.Register<StructureCostLookupListView, StructureCost>();
            _updateDetailsService.Register<StructureCost, StructureCostDetailsView>();

            Container.RegisterViewForNavigation<SupervisionLookupListView>();
            //_dialogService.Register<SupervisionDetailsViewModel, SupervisionDetailsView>();
			_selectService.Register<SupervisionLookupListView, Supervision>();
            _updateDetailsService.Register<Supervision, SupervisionDetailsView>();

            Container.RegisterViewForNavigation<AnswerFileTceLookupListView>();
            //_dialogService.Register<AnswerFileTceDetailsViewModel, AnswerFileTceDetailsView>();
			_selectService.Register<AnswerFileTceLookupListView, AnswerFileTce>();
            _updateDetailsService.Register<AnswerFileTce, AnswerFileTceDetailsView>();

            Container.RegisterViewForNavigation<TechnicalRequrementsLookupListView>();
            //_dialogService.Register<TechnicalRequrementsDetailsViewModel, TechnicalRequrementsDetailsView>();
			_selectService.Register<TechnicalRequrementsLookupListView, TechnicalRequrements>();
            _updateDetailsService.Register<TechnicalRequrements, TechnicalRequrementsDetailsView>();

            Container.RegisterViewForNavigation<TechnicalRequrementsFileLookupListView>();
            //_dialogService.Register<TechnicalRequrementsFileDetailsViewModel, TechnicalRequrementsFileDetailsView>();
			_selectService.Register<TechnicalRequrementsFileLookupListView, TechnicalRequrementsFile>();
            _updateDetailsService.Register<TechnicalRequrementsFile, TechnicalRequrementsFileDetailsView>();

            Container.RegisterViewForNavigation<TechnicalRequrementsTaskLookupListView>();
            //_dialogService.Register<TechnicalRequrementsTaskDetailsViewModel, TechnicalRequrementsTaskDetailsView>();
			_selectService.Register<TechnicalRequrementsTaskLookupListView, TechnicalRequrementsTask>();
            _updateDetailsService.Register<TechnicalRequrementsTask, TechnicalRequrementsTaskDetailsView>();

            Container.RegisterViewForNavigation<UserGroupLookupListView>();
            //_dialogService.Register<UserGroupDetailsViewModel, UserGroupDetailsView>();
			_selectService.Register<UserGroupLookupListView, UserGroup>();
            _updateDetailsService.Register<UserGroup, UserGroupDetailsView>();

            Container.RegisterViewForNavigation<GlobalPropertiesLookupListView>();
            //_dialogService.Register<GlobalPropertiesDetailsViewModel, GlobalPropertiesDetailsView>();
			_selectService.Register<GlobalPropertiesLookupListView, GlobalProperties>();
            _updateDetailsService.Register<GlobalProperties, GlobalPropertiesDetailsView>();

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

            Container.RegisterViewForNavigation<SumLookupListView>();
            //_dialogService.Register<SumDetailsViewModel, SumDetailsView>();
			_selectService.Register<SumLookupListView, Sum>();
            _updateDetailsService.Register<Sum, SumDetailsView>();

            Container.RegisterViewForNavigation<CurrencyExchangeRateLookupListView>();
            //_dialogService.Register<CurrencyExchangeRateDetailsViewModel, CurrencyExchangeRateDetailsView>();
			_selectService.Register<CurrencyExchangeRateLookupListView, CurrencyExchangeRate>();
            _updateDetailsService.Register<CurrencyExchangeRate, CurrencyExchangeRateDetailsView>();

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

            Container.RegisterViewForNavigation<DocumentLookupListView>();
            //_dialogService.Register<DocumentDetailsViewModel, DocumentDetailsView>();
			_selectService.Register<DocumentLookupListView, Document>();
            _updateDetailsService.Register<Document, DocumentDetailsView>();

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
