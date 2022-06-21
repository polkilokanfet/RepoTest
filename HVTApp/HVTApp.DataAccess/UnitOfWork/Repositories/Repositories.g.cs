using System.Data.Entity;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class CountryUnionRepository : BaseRepository<CountryUnion>, ICountryUnionRepository
    {
		public CountryUnionRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class BankGuaranteeRepository : BaseRepository<BankGuarantee>, IBankGuaranteeRepository
    {
		public BankGuaranteeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class BankGuaranteeTypeRepository : BaseRepository<BankGuaranteeType>, IBankGuaranteeTypeRepository
    {
		public BankGuaranteeTypeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class BudgetRepository : BaseRepository<Budget>, IBudgetRepository
    {
		public BudgetRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class BudgetUnitRepository : BaseRepository<BudgetUnit>, IBudgetUnitRepository
    {
		public BudgetUnitRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ConstructorParametersListRepository : BaseRepository<ConstructorParametersList>, IConstructorParametersListRepository
    {
		public ConstructorParametersListRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ConstructorsParametersRepository : BaseRepository<ConstructorsParameters>, IConstructorsParametersRepository
    {
		public ConstructorsParametersRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class CostsPercentsRepository : BaseRepository<CostsPercents>, ICostsPercentsRepository
    {
		public CostsPercentsRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class CreateNewProductTaskRepository : BaseRepository<CreateNewProductTask>, ICreateNewProductTaskRepository
    {
		public CreateNewProductTaskRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DesignDepartmentRepository : BaseRepository<DesignDepartment>, IDesignDepartmentRepository
    {
		public DesignDepartmentRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DirectumTaskRepository : BaseRepository<DirectumTask>, IDirectumTaskRepository
    {
		public DirectumTaskRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DirectumTaskGroupRepository : BaseRepository<DirectumTaskGroup>, IDirectumTaskGroupRepository
    {
		public DirectumTaskGroupRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DirectumTaskGroupFileRepository : BaseRepository<DirectumTaskGroupFile>, IDirectumTaskGroupFileRepository
    {
		public DirectumTaskGroupFileRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DirectumTaskMessageRepository : BaseRepository<DirectumTaskMessage>, IDirectumTaskMessageRepository
    {
		public DirectumTaskMessageRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DocumentNumberRepository : BaseRepository<DocumentNumber>, IDocumentNumberRepository
    {
		public DocumentNumberRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class EventServiceUnitRepository : BaseRepository<EventServiceUnit>, IEventServiceUnitRepository
    {
		public EventServiceUnitRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class IncomingRequestRepository : BaseRepository<IncomingRequest>, IIncomingRequestRepository
    {
		public IncomingRequestRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class LaborHourCostRepository : BaseRepository<LaborHourCost>, ILaborHourCostRepository
    {
		public LaborHourCostRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class LaborHoursRepository : BaseRepository<LaborHours>, ILaborHoursRepository
    {
		public LaborHoursRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class LogUnitRepository : BaseRepository<LogUnit>, ILogUnitRepository
    {
		public LogUnitRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class LosingReasonRepository : BaseRepository<LosingReason>, ILosingReasonRepository
    {
		public LosingReasonRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class MarketFieldRepository : BaseRepository<MarketField>, IMarketFieldRepository
    {
		public MarketFieldRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PaymentActualRepository : BaseRepository<PaymentActual>, IPaymentActualRepository
    {
		public PaymentActualRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PaymentConditionPointRepository : BaseRepository<PaymentConditionPoint>, IPaymentConditionPointRepository
    {
		public PaymentConditionPointRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PaymentPlannedRepository : BaseRepository<PaymentPlanned>, IPaymentPlannedRepository
    {
		public PaymentPlannedRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PenaltyRepository : BaseRepository<Penalty>, IPenaltyRepository
    {
		public PenaltyRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceCalculationRepository : BaseRepository<PriceCalculation>, IPriceCalculationRepository
    {
		public PriceCalculationRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceCalculationFileRepository : BaseRepository<PriceCalculationFile>, IPriceCalculationFileRepository
    {
		public PriceCalculationFileRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceCalculationHistoryItemRepository : BaseRepository<PriceCalculationHistoryItem>, IPriceCalculationHistoryItemRepository
    {
		public PriceCalculationHistoryItemRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceCalculationItemRepository : BaseRepository<PriceCalculationItem>, IPriceCalculationItemRepository
    {
		public PriceCalculationItemRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DesignDepartmentParametersRepository : BaseRepository<DesignDepartmentParameters>, IDesignDepartmentParametersRepository
    {
		public DesignDepartmentParametersRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DesignDepartmentParametersAddedBlocksRepository : BaseRepository<DesignDepartmentParametersAddedBlocks>, IDesignDepartmentParametersAddedBlocksRepository
    {
		public DesignDepartmentParametersAddedBlocksRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DesignDepartmentParametersSubTaskRepository : BaseRepository<DesignDepartmentParametersSubTask>, IDesignDepartmentParametersSubTaskRepository
    {
		public DesignDepartmentParametersSubTaskRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskRepository : BaseRepository<PriceEngineeringTask>, IPriceEngineeringTaskRepository
    {
		public PriceEngineeringTaskRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskFileAnswerRepository : BaseRepository<PriceEngineeringTaskFileAnswer>, IPriceEngineeringTaskFileAnswerRepository
    {
		public PriceEngineeringTaskFileAnswerRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskFileTechnicalRequirementsRepository : BaseRepository<PriceEngineeringTaskFileTechnicalRequirements>, IPriceEngineeringTaskFileTechnicalRequirementsRepository
    {
		public PriceEngineeringTaskFileTechnicalRequirementsRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskMessageRepository : BaseRepository<PriceEngineeringTaskMessage>, IPriceEngineeringTaskMessageRepository
    {
		public PriceEngineeringTaskMessageRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskProductBlockAddedRepository : BaseRepository<PriceEngineeringTaskProductBlockAdded>, IPriceEngineeringTaskProductBlockAddedRepository
    {
		public PriceEngineeringTaskProductBlockAddedRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTasksRepository : BaseRepository<PriceEngineeringTasks>, IPriceEngineeringTasksRepository
    {
		public PriceEngineeringTasksRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTasksFileTechnicalRequirementsRepository : BaseRepository<PriceEngineeringTasksFileTechnicalRequirements>, IPriceEngineeringTasksFileTechnicalRequirementsRepository
    {
		public PriceEngineeringTasksFileTechnicalRequirementsRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskStatusRepository : BaseRepository<PriceEngineeringTaskStatus>, IPriceEngineeringTaskStatusRepository
    {
		public PriceEngineeringTaskStatusRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskTceRepository : BaseRepository<PriceEngineeringTaskTce>, IPriceEngineeringTaskTceRepository
    {
		public PriceEngineeringTaskTceRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskTceStoryItemRepository : BaseRepository<PriceEngineeringTaskTceStoryItem>, IPriceEngineeringTaskTceStoryItemRepository
    {
		public PriceEngineeringTaskTceStoryItemRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PriceEngineeringTaskTceStructureCostVersionRepository : BaseRepository<PriceEngineeringTaskTceStructureCostVersion>, IPriceEngineeringTaskTceStructureCostVersionRepository
    {
		public PriceEngineeringTaskTceStructureCostVersionRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class StructureCostVersionRepository : BaseRepository<StructureCostVersion>, IStructureCostVersionRepository
    {
		public StructureCostVersionRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
		public ProductCategoryRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductCategoryPriceAndCostRepository : BaseRepository<ProductCategoryPriceAndCost>, IProductCategoryPriceAndCostRepository
    {
		public ProductCategoryPriceAndCostRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductIncludedRepository : BaseRepository<ProductIncluded>, IProductIncludedRepository
    {
		public ProductIncludedRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductDesignationRepository : BaseRepository<ProductDesignation>, IProductDesignationRepository
    {
		public ProductDesignationRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductTypeRepository : BaseRepository<ProductType>, IProductTypeRepository
    {
		public ProductTypeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductTypeDesignationRepository : BaseRepository<ProductTypeDesignation>, IProductTypeDesignationRepository
    {
		public ProductTypeDesignationRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProjectTypeRepository : BaseRepository<ProjectType>, IProjectTypeRepository
    {
		public ProjectTypeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class StandartMarginalIncomeRepository : BaseRepository<StandartMarginalIncome>, IStandartMarginalIncomeRepository
    {
		public StandartMarginalIncomeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class StandartProductionTermRepository : BaseRepository<StandartProductionTerm>, IStandartProductionTermRepository
    {
		public StandartProductionTermRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class StructureCostRepository : BaseRepository<StructureCost>, IStructureCostRepository
    {
		public StructureCostRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class SupervisionRepository : BaseRepository<Supervision>, ISupervisionRepository
    {
		public SupervisionRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class AnswerFileTceRepository : BaseRepository<AnswerFileTce>, IAnswerFileTceRepository
    {
		public AnswerFileTceRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ShippingCostFileRepository : BaseRepository<ShippingCostFile>, IShippingCostFileRepository
    {
		public ShippingCostFileRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class TechnicalRequrementsRepository : BaseRepository<TechnicalRequrements>, ITechnicalRequrementsRepository
    {
		public TechnicalRequrementsRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class TechnicalRequrementsFileRepository : BaseRepository<TechnicalRequrementsFile>, ITechnicalRequrementsFileRepository
    {
		public TechnicalRequrementsFileRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class TechnicalRequrementsTaskRepository : BaseRepository<TechnicalRequrementsTask>, ITechnicalRequrementsTaskRepository
    {
		public TechnicalRequrementsTaskRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class TechnicalRequrementsTaskHistoryElementRepository : BaseRepository<TechnicalRequrementsTaskHistoryElement>, ITechnicalRequrementsTaskHistoryElementRepository
    {
		public TechnicalRequrementsTaskHistoryElementRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class UserGroupRepository : BaseRepository<UserGroup>, IUserGroupRepository
    {
		public UserGroupRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class GlobalPropertiesRepository : BaseRepository<GlobalProperties>, IGlobalPropertiesRepository
    {
		public GlobalPropertiesRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
		public AddressRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
		public CountryRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
		public DistrictRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class LocalityRepository : BaseRepository<Locality>, ILocalityRepository
    {
		public LocalityRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class LocalityTypeRepository : BaseRepository<LocalityType>, ILocalityTypeRepository
    {
		public LocalityTypeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
		public RegionRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class SumRepository : BaseRepository<Sum>, ISumRepository
    {
		public SumRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class CurrencyExchangeRateRepository : BaseRepository<CurrencyExchangeRate>, ICurrencyExchangeRateRepository
    {
		public CurrencyExchangeRateRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class NoteRepository : BaseRepository<Note>, INoteRepository
    {
		public NoteRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class OfferUnitRepository : BaseRepository<OfferUnit>, IOfferUnitRepository
    {
		public OfferUnitRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PaymentConditionSetRepository : BaseRepository<PaymentConditionSet>, IPaymentConditionSetRepository
    {
		public PaymentConditionSetRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductBlockRepository : BaseRepository<ProductBlock>, IProductBlockRepository
    {
		public ProductBlockRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductDependentRepository : BaseRepository<ProductDependent>, IProductDependentRepository
    {
		public ProductDependentRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class BankDetailsRepository : BaseRepository<BankDetails>, IBankDetailsRepository
    {
		public BankDetailsRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
		public CompanyRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class CompanyFormRepository : BaseRepository<CompanyForm>, ICompanyFormRepository
    {
		public CompanyFormRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DocumentsRegistrationDetailsRepository : BaseRepository<DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsRepository
    {
		public DocumentsRegistrationDetailsRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class EmployeesPositionRepository : BaseRepository<EmployeesPosition>, IEmployeesPositionRepository
    {
		public EmployeesPositionRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class FacilityTypeRepository : BaseRepository<FacilityType>, IFacilityTypeRepository
    {
		public FacilityTypeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ActivityFieldRepository : BaseRepository<ActivityField>, IActivityFieldRepository
    {
		public ActivityFieldRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
		public ContractRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class MeasureRepository : BaseRepository<Measure>, IMeasureRepository
    {
		public MeasureRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ParameterRepository : BaseRepository<Parameter>, IParameterRepository
    {
		public ParameterRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ParameterGroupRepository : BaseRepository<ParameterGroup>, IParameterGroupRepository
    {
		public ParameterGroupRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductRelationRepository : BaseRepository<ProductRelation>, IProductRelationRepository
    {
		public ProductRelationRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
		public PersonRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ParameterRelationRepository : BaseRepository<ParameterRelation>, IParameterRelationRepository
    {
		public ParameterRelationRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class SalesUnitRepository : BaseRepository<SalesUnit>, ISalesUnitRepository
    {
		public SalesUnitRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
		public DocumentRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class SumOnDateRepository : BaseRepository<SumOnDate>, ISumOnDateRepository
    {
		public SumOnDateRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProductRepository : BaseRepository<Product>, IProductRepository
    {
		public ProductRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class OfferRepository : BaseRepository<Offer>, IOfferRepository
    {
		public OfferRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
		public EmployeeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
		public OrderRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PaymentConditionRepository : BaseRepository<PaymentCondition>, IPaymentConditionRepository
    {
		public PaymentConditionRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class PaymentDocumentRepository : BaseRepository<PaymentDocument>, IPaymentDocumentRepository
    {
		public PaymentDocumentRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class FacilityRepository : BaseRepository<Facility>, IFacilityRepository
    {
		public FacilityRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
		public ProjectRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
		public UserRoleRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class SpecificationRepository : BaseRepository<Specification>, ISpecificationRepository
    {
		public SpecificationRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class TenderRepository : BaseRepository<Tender>, ITenderRepository
    {
		public TenderRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class TenderTypeRepository : BaseRepository<TenderType>, ITenderTypeRepository
    {
		public TenderTypeRepository(DbContext context) : base(context) 
		{
		}
    }

    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
		public UserRepository(DbContext context) : base(context) 
		{
		}
    }

}
