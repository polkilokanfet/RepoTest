using HVTApp.Model.POCOs;
using HVTApp.Model.Events;
using Microsoft.Practices.Unity;
using HVTApp.UI.Lookup;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure;

namespace HVTApp.UI.ViewModels
{
	public partial class CountryUnionLookupListViewModel : BaseListViewModel<CountryUnion, CountryUnionLookup, AfterSaveCountryUnionEvent, AfterSelectCountryUnionEvent, AfterRemoveCountryUnionEvent>
    {
        public CountryUnionLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class BankGuaranteeLookupListViewModel : BaseListViewModel<BankGuarantee, BankGuaranteeLookup, AfterSaveBankGuaranteeEvent, AfterSelectBankGuaranteeEvent, AfterRemoveBankGuaranteeEvent>
    {
        public BankGuaranteeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class BankGuaranteeTypeLookupListViewModel : BaseListViewModel<BankGuaranteeType, BankGuaranteeTypeLookup, AfterSaveBankGuaranteeTypeEvent, AfterSelectBankGuaranteeTypeEvent, AfterRemoveBankGuaranteeTypeEvent>
    {
        public BankGuaranteeTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class BudgetLookupListViewModel : BaseListViewModel<Budget, BudgetLookup, AfterSaveBudgetEvent, AfterSelectBudgetEvent, AfterRemoveBudgetEvent>
    {
        public BudgetLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class BudgetUnitLookupListViewModel : BaseListViewModel<BudgetUnit, BudgetUnitLookup, AfterSaveBudgetUnitEvent, AfterSelectBudgetUnitEvent, AfterRemoveBudgetUnitEvent>
    {
        public BudgetUnitLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ConstructorParametersListLookupListViewModel : BaseListViewModel<ConstructorParametersList, ConstructorParametersListLookup, AfterSaveConstructorParametersListEvent, AfterSelectConstructorParametersListEvent, AfterRemoveConstructorParametersListEvent>
    {
        public ConstructorParametersListLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ConstructorsParametersLookupListViewModel : BaseListViewModel<ConstructorsParameters, ConstructorsParametersLookup, AfterSaveConstructorsParametersEvent, AfterSelectConstructorsParametersEvent, AfterRemoveConstructorsParametersEvent>
    {
        public ConstructorsParametersLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class CostsPercentsLookupListViewModel : BaseListViewModel<CostsPercents, CostsPercentsLookup, AfterSaveCostsPercentsEvent, AfterSelectCostsPercentsEvent, AfterRemoveCostsPercentsEvent>
    {
        public CostsPercentsLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class CreateNewProductTaskLookupListViewModel : BaseListViewModel<CreateNewProductTask, CreateNewProductTaskLookup, AfterSaveCreateNewProductTaskEvent, AfterSelectCreateNewProductTaskEvent, AfterRemoveCreateNewProductTaskEvent>
    {
        public CreateNewProductTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class InvoiceForPaymentTaskLookupListViewModel : BaseListViewModel<InvoiceForPaymentTask, InvoiceForPaymentTaskLookup, AfterSaveInvoiceForPaymentTaskEvent, AfterSelectInvoiceForPaymentTaskEvent, AfterRemoveInvoiceForPaymentTaskEvent>
    {
        public InvoiceForPaymentTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DesignDepartmentLookupListViewModel : BaseListViewModel<DesignDepartment, DesignDepartmentLookup, AfterSaveDesignDepartmentEvent, AfterSelectDesignDepartmentEvent, AfterRemoveDesignDepartmentEvent>
    {
        public DesignDepartmentLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DirectumTaskLookupListViewModel : BaseListViewModel<DirectumTask, DirectumTaskLookup, AfterSaveDirectumTaskEvent, AfterSelectDirectumTaskEvent, AfterRemoveDirectumTaskEvent>
    {
        public DirectumTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DirectumTaskGroupLookupListViewModel : BaseListViewModel<DirectumTaskGroup, DirectumTaskGroupLookup, AfterSaveDirectumTaskGroupEvent, AfterSelectDirectumTaskGroupEvent, AfterRemoveDirectumTaskGroupEvent>
    {
        public DirectumTaskGroupLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DirectumTaskGroupFileLookupListViewModel : BaseListViewModel<DirectumTaskGroupFile, DirectumTaskGroupFileLookup, AfterSaveDirectumTaskGroupFileEvent, AfterSelectDirectumTaskGroupFileEvent, AfterRemoveDirectumTaskGroupFileEvent>
    {
        public DirectumTaskGroupFileLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DirectumTaskMessageLookupListViewModel : BaseListViewModel<DirectumTaskMessage, DirectumTaskMessageLookup, AfterSaveDirectumTaskMessageEvent, AfterSelectDirectumTaskMessageEvent, AfterRemoveDirectumTaskMessageEvent>
    {
        public DirectumTaskMessageLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DocumentNumberLookupListViewModel : BaseListViewModel<DocumentNumber, DocumentNumberLookup, AfterSaveDocumentNumberEvent, AfterSelectDocumentNumberEvent, AfterRemoveDocumentNumberEvent>
    {
        public DocumentNumberLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class EventServiceUnitLookupListViewModel : BaseListViewModel<EventServiceUnit, EventServiceUnitLookup, AfterSaveEventServiceUnitEvent, AfterSelectEventServiceUnitEvent, AfterRemoveEventServiceUnitEvent>
    {
        public EventServiceUnitLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class IncomingRequestLookupListViewModel : BaseListViewModel<IncomingRequest, IncomingRequestLookup, AfterSaveIncomingRequestEvent, AfterSelectIncomingRequestEvent, AfterRemoveIncomingRequestEvent>
    {
        public IncomingRequestLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class LaborHourCostLookupListViewModel : BaseListViewModel<LaborHourCost, LaborHourCostLookup, AfterSaveLaborHourCostEvent, AfterSelectLaborHourCostEvent, AfterRemoveLaborHourCostEvent>
    {
        public LaborHourCostLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class LaborHoursLookupListViewModel : BaseListViewModel<LaborHours, LaborHoursLookup, AfterSaveLaborHoursEvent, AfterSelectLaborHoursEvent, AfterRemoveLaborHoursEvent>
    {
        public LaborHoursLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class LogUnitLookupListViewModel : BaseListViewModel<LogUnit, LogUnitLookup, AfterSaveLogUnitEvent, AfterSelectLogUnitEvent, AfterRemoveLogUnitEvent>
    {
        public LogUnitLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class LosingReasonLookupListViewModel : BaseListViewModel<LosingReason, LosingReasonLookup, AfterSaveLosingReasonEvent, AfterSelectLosingReasonEvent, AfterRemoveLosingReasonEvent>
    {
        public LosingReasonLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class MarketFieldLookupListViewModel : BaseListViewModel<MarketField, MarketFieldLookup, AfterSaveMarketFieldEvent, AfterSelectMarketFieldEvent, AfterRemoveMarketFieldEvent>
    {
        public MarketFieldLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PaymentActualLookupListViewModel : BaseListViewModel<PaymentActual, PaymentActualLookup, AfterSavePaymentActualEvent, AfterSelectPaymentActualEvent, AfterRemovePaymentActualEvent>
    {
        public PaymentActualLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PaymentConditionPointLookupListViewModel : BaseListViewModel<PaymentConditionPoint, PaymentConditionPointLookup, AfterSavePaymentConditionPointEvent, AfterSelectPaymentConditionPointEvent, AfterRemovePaymentConditionPointEvent>
    {
        public PaymentConditionPointLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PaymentPlannedLookupListViewModel : BaseListViewModel<PaymentPlanned, PaymentPlannedLookup, AfterSavePaymentPlannedEvent, AfterSelectPaymentPlannedEvent, AfterRemovePaymentPlannedEvent>
    {
        public PaymentPlannedLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PenaltyLookupListViewModel : BaseListViewModel<Penalty, PenaltyLookup, AfterSavePenaltyEvent, AfterSelectPenaltyEvent, AfterRemovePenaltyEvent>
    {
        public PenaltyLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceCalculationLookupListViewModel : BaseListViewModel<PriceCalculation, PriceCalculationLookup, AfterSavePriceCalculationEvent, AfterSelectPriceCalculationEvent, AfterRemovePriceCalculationEvent>
    {
        public PriceCalculationLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceCalculationFileLookupListViewModel : BaseListViewModel<PriceCalculationFile, PriceCalculationFileLookup, AfterSavePriceCalculationFileEvent, AfterSelectPriceCalculationFileEvent, AfterRemovePriceCalculationFileEvent>
    {
        public PriceCalculationFileLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceCalculationHistoryItemLookupListViewModel : BaseListViewModel<PriceCalculationHistoryItem, PriceCalculationHistoryItemLookup, AfterSavePriceCalculationHistoryItemEvent, AfterSelectPriceCalculationHistoryItemEvent, AfterRemovePriceCalculationHistoryItemEvent>
    {
        public PriceCalculationHistoryItemLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceCalculationItemLookupListViewModel : BaseListViewModel<PriceCalculationItem, PriceCalculationItemLookup, AfterSavePriceCalculationItemEvent, AfterSelectPriceCalculationItemEvent, AfterRemovePriceCalculationItemEvent>
    {
        public PriceCalculationItemLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DesignDepartmentParametersLookupListViewModel : BaseListViewModel<DesignDepartmentParameters, DesignDepartmentParametersLookup, AfterSaveDesignDepartmentParametersEvent, AfterSelectDesignDepartmentParametersEvent, AfterRemoveDesignDepartmentParametersEvent>
    {
        public DesignDepartmentParametersLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DesignDepartmentParametersAddedBlocksLookupListViewModel : BaseListViewModel<DesignDepartmentParametersAddedBlocks, DesignDepartmentParametersAddedBlocksLookup, AfterSaveDesignDepartmentParametersAddedBlocksEvent, AfterSelectDesignDepartmentParametersAddedBlocksEvent, AfterRemoveDesignDepartmentParametersAddedBlocksEvent>
    {
        public DesignDepartmentParametersAddedBlocksLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DesignDepartmentParametersSubTaskLookupListViewModel : BaseListViewModel<DesignDepartmentParametersSubTask, DesignDepartmentParametersSubTaskLookup, AfterSaveDesignDepartmentParametersSubTaskEvent, AfterSelectDesignDepartmentParametersSubTaskEvent, AfterRemoveDesignDepartmentParametersSubTaskEvent>
    {
        public DesignDepartmentParametersSubTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class NotificationsReportsSettingsLookupListViewModel : BaseListViewModel<NotificationsReportsSettings, NotificationsReportsSettingsLookup, AfterSaveNotificationsReportsSettingsEvent, AfterSelectNotificationsReportsSettingsEvent, AfterRemoveNotificationsReportsSettingsEvent>
    {
        public NotificationsReportsSettingsLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceEngineeringTaskLookupListViewModel : BaseListViewModel<PriceEngineeringTask, PriceEngineeringTaskLookup, AfterSavePriceEngineeringTaskEvent, AfterSelectPriceEngineeringTaskEvent, AfterRemovePriceEngineeringTaskEvent>
    {
        public PriceEngineeringTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceEngineeringTaskFileAnswerLookupListViewModel : BaseListViewModel<PriceEngineeringTaskFileAnswer, PriceEngineeringTaskFileAnswerLookup, AfterSavePriceEngineeringTaskFileAnswerEvent, AfterSelectPriceEngineeringTaskFileAnswerEvent, AfterRemovePriceEngineeringTaskFileAnswerEvent>
    {
        public PriceEngineeringTaskFileAnswerLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceEngineeringTaskFileTechnicalRequirementsLookupListViewModel : BaseListViewModel<PriceEngineeringTaskFileTechnicalRequirements, PriceEngineeringTaskFileTechnicalRequirementsLookup, AfterSavePriceEngineeringTaskFileTechnicalRequirementsEvent, AfterSelectPriceEngineeringTaskFileTechnicalRequirementsEvent, AfterRemovePriceEngineeringTaskFileTechnicalRequirementsEvent>
    {
        public PriceEngineeringTaskFileTechnicalRequirementsLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceEngineeringTaskMessageLookupListViewModel : BaseListViewModel<PriceEngineeringTaskMessage, PriceEngineeringTaskMessageLookup, AfterSavePriceEngineeringTaskMessageEvent, AfterSelectPriceEngineeringTaskMessageEvent, AfterRemovePriceEngineeringTaskMessageEvent>
    {
        public PriceEngineeringTaskMessageLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceEngineeringTaskProductBlockAddedLookupListViewModel : BaseListViewModel<PriceEngineeringTaskProductBlockAdded, PriceEngineeringTaskProductBlockAddedLookup, AfterSavePriceEngineeringTaskProductBlockAddedEvent, AfterSelectPriceEngineeringTaskProductBlockAddedEvent, AfterRemovePriceEngineeringTaskProductBlockAddedEvent>
    {
        public PriceEngineeringTaskProductBlockAddedLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceEngineeringTasksLookupListViewModel : BaseListViewModel<PriceEngineeringTasks, PriceEngineeringTasksLookup, AfterSavePriceEngineeringTasksEvent, AfterSelectPriceEngineeringTasksEvent, AfterRemovePriceEngineeringTasksEvent>
    {
        public PriceEngineeringTasksLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceEngineeringTasksFileTechnicalRequirementsLookupListViewModel : BaseListViewModel<PriceEngineeringTasksFileTechnicalRequirements, PriceEngineeringTasksFileTechnicalRequirementsLookup, AfterSavePriceEngineeringTasksFileTechnicalRequirementsEvent, AfterSelectPriceEngineeringTasksFileTechnicalRequirementsEvent, AfterRemovePriceEngineeringTasksFileTechnicalRequirementsEvent>
    {
        public PriceEngineeringTasksFileTechnicalRequirementsLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PriceEngineeringTaskStatusLookupListViewModel : BaseListViewModel<PriceEngineeringTaskStatus, PriceEngineeringTaskStatusLookup, AfterSavePriceEngineeringTaskStatusEvent, AfterSelectPriceEngineeringTaskStatusEvent, AfterRemovePriceEngineeringTaskStatusEvent>
    {
        public PriceEngineeringTaskStatusLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class StructureCostVersionLookupListViewModel : BaseListViewModel<StructureCostVersion, StructureCostVersionLookup, AfterSaveStructureCostVersionEvent, AfterSelectStructureCostVersionEvent, AfterRemoveStructureCostVersionEvent>
    {
        public StructureCostVersionLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductCategoryLookupListViewModel : BaseListViewModel<ProductCategory, ProductCategoryLookup, AfterSaveProductCategoryEvent, AfterSelectProductCategoryEvent, AfterRemoveProductCategoryEvent>
    {
        public ProductCategoryLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductCategoryPriceAndCostLookupListViewModel : BaseListViewModel<ProductCategoryPriceAndCost, ProductCategoryPriceAndCostLookup, AfterSaveProductCategoryPriceAndCostEvent, AfterSelectProductCategoryPriceAndCostEvent, AfterRemoveProductCategoryPriceAndCostEvent>
    {
        public ProductCategoryPriceAndCostLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductIncludedLookupListViewModel : BaseListViewModel<ProductIncluded, ProductIncludedLookup, AfterSaveProductIncludedEvent, AfterSelectProductIncludedEvent, AfterRemoveProductIncludedEvent>
    {
        public ProductIncludedLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductDesignationLookupListViewModel : BaseListViewModel<ProductDesignation, ProductDesignationLookup, AfterSaveProductDesignationEvent, AfterSelectProductDesignationEvent, AfterRemoveProductDesignationEvent>
    {
        public ProductDesignationLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductTypeLookupListViewModel : BaseListViewModel<ProductType, ProductTypeLookup, AfterSaveProductTypeEvent, AfterSelectProductTypeEvent, AfterRemoveProductTypeEvent>
    {
        public ProductTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductTypeDesignationLookupListViewModel : BaseListViewModel<ProductTypeDesignation, ProductTypeDesignationLookup, AfterSaveProductTypeDesignationEvent, AfterSelectProductTypeDesignationEvent, AfterRemoveProductTypeDesignationEvent>
    {
        public ProductTypeDesignationLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProjectTypeLookupListViewModel : BaseListViewModel<ProjectType, ProjectTypeLookup, AfterSaveProjectTypeEvent, AfterSelectProjectTypeEvent, AfterRemoveProjectTypeEvent>
    {
        public ProjectTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class StandartMarginalIncomeLookupListViewModel : BaseListViewModel<StandartMarginalIncome, StandartMarginalIncomeLookup, AfterSaveStandartMarginalIncomeEvent, AfterSelectStandartMarginalIncomeEvent, AfterRemoveStandartMarginalIncomeEvent>
    {
        public StandartMarginalIncomeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class StandartProductionTermLookupListViewModel : BaseListViewModel<StandartProductionTerm, StandartProductionTermLookup, AfterSaveStandartProductionTermEvent, AfterSelectStandartProductionTermEvent, AfterRemoveStandartProductionTermEvent>
    {
        public StandartProductionTermLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class StructureCostLookupListViewModel : BaseListViewModel<StructureCost, StructureCostLookup, AfterSaveStructureCostEvent, AfterSelectStructureCostEvent, AfterRemoveStructureCostEvent>
    {
        public StructureCostLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class SupervisionLookupListViewModel : BaseListViewModel<Supervision, SupervisionLookup, AfterSaveSupervisionEvent, AfterSelectSupervisionEvent, AfterRemoveSupervisionEvent>
    {
        public SupervisionLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class AnswerFileTceLookupListViewModel : BaseListViewModel<AnswerFileTce, AnswerFileTceLookup, AfterSaveAnswerFileTceEvent, AfterSelectAnswerFileTceEvent, AfterRemoveAnswerFileTceEvent>
    {
        public AnswerFileTceLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ShippingCostFileLookupListViewModel : BaseListViewModel<ShippingCostFile, ShippingCostFileLookup, AfterSaveShippingCostFileEvent, AfterSelectShippingCostFileEvent, AfterRemoveShippingCostFileEvent>
    {
        public ShippingCostFileLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class TechnicalRequrementsLookupListViewModel : BaseListViewModel<TechnicalRequrements, TechnicalRequrementsLookup, AfterSaveTechnicalRequrementsEvent, AfterSelectTechnicalRequrementsEvent, AfterRemoveTechnicalRequrementsEvent>
    {
        public TechnicalRequrementsLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class TechnicalRequrementsFileLookupListViewModel : BaseListViewModel<TechnicalRequrementsFile, TechnicalRequrementsFileLookup, AfterSaveTechnicalRequrementsFileEvent, AfterSelectTechnicalRequrementsFileEvent, AfterRemoveTechnicalRequrementsFileEvent>
    {
        public TechnicalRequrementsFileLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class TechnicalRequrementsTaskLookupListViewModel : BaseListViewModel<TechnicalRequrementsTask, TechnicalRequrementsTaskLookup, AfterSaveTechnicalRequrementsTaskEvent, AfterSelectTechnicalRequrementsTaskEvent, AfterRemoveTechnicalRequrementsTaskEvent>
    {
        public TechnicalRequrementsTaskLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class TechnicalRequrementsTaskHistoryElementLookupListViewModel : BaseListViewModel<TechnicalRequrementsTaskHistoryElement, TechnicalRequrementsTaskHistoryElementLookup, AfterSaveTechnicalRequrementsTaskHistoryElementEvent, AfterSelectTechnicalRequrementsTaskHistoryElementEvent, AfterRemoveTechnicalRequrementsTaskHistoryElementEvent>
    {
        public TechnicalRequrementsTaskHistoryElementLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class UserGroupLookupListViewModel : BaseListViewModel<UserGroup, UserGroupLookup, AfterSaveUserGroupEvent, AfterSelectUserGroupEvent, AfterRemoveUserGroupEvent>
    {
        public UserGroupLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class GlobalPropertiesLookupListViewModel : BaseListViewModel<GlobalProperties, GlobalPropertiesLookup, AfterSaveGlobalPropertiesEvent, AfterSelectGlobalPropertiesEvent, AfterRemoveGlobalPropertiesEvent>
    {
        public GlobalPropertiesLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class AddressLookupListViewModel : BaseListViewModel<Address, AddressLookup, AfterSaveAddressEvent, AfterSelectAddressEvent, AfterRemoveAddressEvent>
    {
        public AddressLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class CountryLookupListViewModel : BaseListViewModel<Country, CountryLookup, AfterSaveCountryEvent, AfterSelectCountryEvent, AfterRemoveCountryEvent>
    {
        public CountryLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DistrictLookupListViewModel : BaseListViewModel<District, DistrictLookup, AfterSaveDistrictEvent, AfterSelectDistrictEvent, AfterRemoveDistrictEvent>
    {
        public DistrictLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class LocalityLookupListViewModel : BaseListViewModel<Locality, LocalityLookup, AfterSaveLocalityEvent, AfterSelectLocalityEvent, AfterRemoveLocalityEvent>
    {
        public LocalityLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class LocalityTypeLookupListViewModel : BaseListViewModel<LocalityType, LocalityTypeLookup, AfterSaveLocalityTypeEvent, AfterSelectLocalityTypeEvent, AfterRemoveLocalityTypeEvent>
    {
        public LocalityTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class RegionLookupListViewModel : BaseListViewModel<Region, RegionLookup, AfterSaveRegionEvent, AfterSelectRegionEvent, AfterRemoveRegionEvent>
    {
        public RegionLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class SumLookupListViewModel : BaseListViewModel<Sum, SumLookup, AfterSaveSumEvent, AfterSelectSumEvent, AfterRemoveSumEvent>
    {
        public SumLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class CurrencyExchangeRateLookupListViewModel : BaseListViewModel<CurrencyExchangeRate, CurrencyExchangeRateLookup, AfterSaveCurrencyExchangeRateEvent, AfterSelectCurrencyExchangeRateEvent, AfterRemoveCurrencyExchangeRateEvent>
    {
        public CurrencyExchangeRateLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class NoteLookupListViewModel : BaseListViewModel<Note, NoteLookup, AfterSaveNoteEvent, AfterSelectNoteEvent, AfterRemoveNoteEvent>
    {
        public NoteLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class OfferUnitLookupListViewModel : BaseListViewModel<OfferUnit, OfferUnitLookup, AfterSaveOfferUnitEvent, AfterSelectOfferUnitEvent, AfterRemoveOfferUnitEvent>
    {
        public OfferUnitLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PaymentConditionSetLookupListViewModel : BaseListViewModel<PaymentConditionSet, PaymentConditionSetLookup, AfterSavePaymentConditionSetEvent, AfterSelectPaymentConditionSetEvent, AfterRemovePaymentConditionSetEvent>
    {
        public PaymentConditionSetLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductBlockLookupListViewModel : BaseListViewModel<ProductBlock, ProductBlockLookup, AfterSaveProductBlockEvent, AfterSelectProductBlockEvent, AfterRemoveProductBlockEvent>
    {
        public ProductBlockLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductDependentLookupListViewModel : BaseListViewModel<ProductDependent, ProductDependentLookup, AfterSaveProductDependentEvent, AfterSelectProductDependentEvent, AfterRemoveProductDependentEvent>
    {
        public ProductDependentLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class BankDetailsLookupListViewModel : BaseListViewModel<BankDetails, BankDetailsLookup, AfterSaveBankDetailsEvent, AfterSelectBankDetailsEvent, AfterRemoveBankDetailsEvent>
    {
        public BankDetailsLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class CompanyLookupListViewModel : BaseListViewModel<Company, CompanyLookup, AfterSaveCompanyEvent, AfterSelectCompanyEvent, AfterRemoveCompanyEvent>
    {
        public CompanyLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class CompanyFormLookupListViewModel : BaseListViewModel<CompanyForm, CompanyFormLookup, AfterSaveCompanyFormEvent, AfterSelectCompanyFormEvent, AfterRemoveCompanyFormEvent>
    {
        public CompanyFormLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DocumentsRegistrationDetailsLookupListViewModel : BaseListViewModel<DocumentsRegistrationDetails, DocumentsRegistrationDetailsLookup, AfterSaveDocumentsRegistrationDetailsEvent, AfterSelectDocumentsRegistrationDetailsEvent, AfterRemoveDocumentsRegistrationDetailsEvent>
    {
        public DocumentsRegistrationDetailsLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class EmployeesPositionLookupListViewModel : BaseListViewModel<EmployeesPosition, EmployeesPositionLookup, AfterSaveEmployeesPositionEvent, AfterSelectEmployeesPositionEvent, AfterRemoveEmployeesPositionEvent>
    {
        public EmployeesPositionLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class FacilityTypeLookupListViewModel : BaseListViewModel<FacilityType, FacilityTypeLookup, AfterSaveFacilityTypeEvent, AfterSelectFacilityTypeEvent, AfterRemoveFacilityTypeEvent>
    {
        public FacilityTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ActivityFieldLookupListViewModel : BaseListViewModel<ActivityField, ActivityFieldLookup, AfterSaveActivityFieldEvent, AfterSelectActivityFieldEvent, AfterRemoveActivityFieldEvent>
    {
        public ActivityFieldLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ContractLookupListViewModel : BaseListViewModel<Contract, ContractLookup, AfterSaveContractEvent, AfterSelectContractEvent, AfterRemoveContractEvent>
    {
        public ContractLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class MeasureLookupListViewModel : BaseListViewModel<Measure, MeasureLookup, AfterSaveMeasureEvent, AfterSelectMeasureEvent, AfterRemoveMeasureEvent>
    {
        public MeasureLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ParameterLookupListViewModel : BaseListViewModel<Parameter, ParameterLookup, AfterSaveParameterEvent, AfterSelectParameterEvent, AfterRemoveParameterEvent>
    {
        public ParameterLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ParameterGroupLookupListViewModel : BaseListViewModel<ParameterGroup, ParameterGroupLookup, AfterSaveParameterGroupEvent, AfterSelectParameterGroupEvent, AfterRemoveParameterGroupEvent>
    {
        public ParameterGroupLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductRelationLookupListViewModel : BaseListViewModel<ProductRelation, ProductRelationLookup, AfterSaveProductRelationEvent, AfterSelectProductRelationEvent, AfterRemoveProductRelationEvent>
    {
        public ProductRelationLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PersonLookupListViewModel : BaseListViewModel<Person, PersonLookup, AfterSavePersonEvent, AfterSelectPersonEvent, AfterRemovePersonEvent>
    {
        public PersonLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ParameterRelationLookupListViewModel : BaseListViewModel<ParameterRelation, ParameterRelationLookup, AfterSaveParameterRelationEvent, AfterSelectParameterRelationEvent, AfterRemoveParameterRelationEvent>
    {
        public ParameterRelationLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class SalesUnitLookupListViewModel : BaseListViewModel<SalesUnit, SalesUnitLookup, AfterSaveSalesUnitEvent, AfterSelectSalesUnitEvent, AfterRemoveSalesUnitEvent>
    {
        public SalesUnitLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class DocumentLookupListViewModel : BaseListViewModel<Document, DocumentLookup, AfterSaveDocumentEvent, AfterSelectDocumentEvent, AfterRemoveDocumentEvent>
    {
        public DocumentLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class SumOnDateLookupListViewModel : BaseListViewModel<SumOnDate, SumOnDateLookup, AfterSaveSumOnDateEvent, AfterSelectSumOnDateEvent, AfterRemoveSumOnDateEvent>
    {
        public SumOnDateLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProductLookupListViewModel : BaseListViewModel<Product, ProductLookup, AfterSaveProductEvent, AfterSelectProductEvent, AfterRemoveProductEvent>
    {
        public ProductLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class OfferLookupListViewModel : BaseListViewModel<Offer, OfferLookup, AfterSaveOfferEvent, AfterSelectOfferEvent, AfterRemoveOfferEvent>
    {
        public OfferLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class EmployeeLookupListViewModel : BaseListViewModel<Employee, EmployeeLookup, AfterSaveEmployeeEvent, AfterSelectEmployeeEvent, AfterRemoveEmployeeEvent>
    {
        public EmployeeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class OrderLookupListViewModel : BaseListViewModel<Order, OrderLookup, AfterSaveOrderEvent, AfterSelectOrderEvent, AfterRemoveOrderEvent>
    {
        public OrderLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PaymentConditionLookupListViewModel : BaseListViewModel<PaymentCondition, PaymentConditionLookup, AfterSavePaymentConditionEvent, AfterSelectPaymentConditionEvent, AfterRemovePaymentConditionEvent>
    {
        public PaymentConditionLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class PaymentDocumentLookupListViewModel : BaseListViewModel<PaymentDocument, PaymentDocumentLookup, AfterSavePaymentDocumentEvent, AfterSelectPaymentDocumentEvent, AfterRemovePaymentDocumentEvent>
    {
        public PaymentDocumentLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class FacilityLookupListViewModel : BaseListViewModel<Facility, FacilityLookup, AfterSaveFacilityEvent, AfterSelectFacilityEvent, AfterRemoveFacilityEvent>
    {
        public FacilityLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class ProjectLookupListViewModel : BaseListViewModel<Project, ProjectLookup, AfterSaveProjectEvent, AfterSelectProjectEvent, AfterRemoveProjectEvent>
    {
        public ProjectLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class UserRoleLookupListViewModel : BaseListViewModel<UserRole, UserRoleLookup, AfterSaveUserRoleEvent, AfterSelectUserRoleEvent, AfterRemoveUserRoleEvent>
    {
        public UserRoleLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class SpecificationLookupListViewModel : BaseListViewModel<Specification, SpecificationLookup, AfterSaveSpecificationEvent, AfterSelectSpecificationEvent, AfterRemoveSpecificationEvent>
    {
        public SpecificationLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class TenderLookupListViewModel : BaseListViewModel<Tender, TenderLookup, AfterSaveTenderEvent, AfterSelectTenderEvent, AfterRemoveTenderEvent>
    {
        public TenderLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class TenderTypeLookupListViewModel : BaseListViewModel<TenderType, TenderTypeLookup, AfterSaveTenderTypeEvent, AfterSelectTenderTypeEvent, AfterRemoveTenderTypeEvent>
    {
        public TenderTypeLookupListViewModel(IUnityContainer container) : base(container) { }
    }

	public partial class UserLookupListViewModel : BaseListViewModel<User, UserLookup, AfterSaveUserEvent, AfterSelectUserEvent, AfterRemoveUserEvent>
    {
        public UserLookupListViewModel(IUnityContainer container) : base(container) { }
    }

}
