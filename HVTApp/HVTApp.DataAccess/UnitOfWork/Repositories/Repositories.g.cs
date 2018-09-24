using System.Data.Entity;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class CreateNewProductTaskRepository : BaseRepository<CreateNewProductTask>, ICreateNewProductTaskRepository
    {
        IUnityContainer _container;

		public CreateNewProductTaskRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class DocumentNumberRepository : BaseRepository<DocumentNumber>, IDocumentNumberRepository
    {
        IUnityContainer _container;

		public DocumentNumberRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class MarketFieldRepository : BaseRepository<MarketField>, IMarketFieldRepository
    {
        IUnityContainer _container;

		public MarketFieldRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class PaymentActualRepository : BaseRepository<PaymentActual>, IPaymentActualRepository
    {
        IUnityContainer _container;

		public PaymentActualRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class PaymentPlannedRepository : BaseRepository<PaymentPlanned>, IPaymentPlannedRepository
    {
        IUnityContainer _container;

		public PaymentPlannedRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductBlockIsServiceRepository : BaseRepository<ProductBlockIsService>, IProductBlockIsServiceRepository
    {
        IUnityContainer _container;

		public ProductBlockIsServiceRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductIncludedRepository : BaseRepository<ProductIncluded>, IProductIncludedRepository
    {
        IUnityContainer _container;

		public ProductIncludedRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductDesignationRepository : BaseRepository<ProductDesignation>, IProductDesignationRepository
    {
        IUnityContainer _container;

		public ProductDesignationRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductTypeRepository : BaseRepository<ProductType>, IProductTypeRepository
    {
        IUnityContainer _container;

		public ProductTypeRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductTypeDesignationRepository : BaseRepository<ProductTypeDesignation>, IProductTypeDesignationRepository
    {
        IUnityContainer _container;

		public ProductTypeDesignationRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProjectTypeRepository : BaseRepository<ProjectType>, IProjectTypeRepository
    {
        IUnityContainer _container;

		public ProjectTypeRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class CommonOptionRepository : BaseRepository<CommonOption>, ICommonOptionRepository
    {
        IUnityContainer _container;

		public CommonOptionRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        IUnityContainer _container;

		public AddressRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        IUnityContainer _container;

		public CountryRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        IUnityContainer _container;

		public DistrictRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class LocalityRepository : BaseRepository<Locality>, ILocalityRepository
    {
        IUnityContainer _container;

		public LocalityRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class LocalityTypeRepository : BaseRepository<LocalityType>, ILocalityTypeRepository
    {
        IUnityContainer _container;

		public LocalityTypeRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        IUnityContainer _container;

		public RegionRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class SumRepository : BaseRepository<Sum>, ISumRepository
    {
        IUnityContainer _container;

		public SumRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class CurrencyExchangeRateRepository : BaseRepository<CurrencyExchangeRate>, ICurrencyExchangeRateRepository
    {
        IUnityContainer _container;

		public CurrencyExchangeRateRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        IUnityContainer _container;

		public NoteRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class OfferUnitRepository : BaseRepository<OfferUnit>, IOfferUnitRepository
    {
        IUnityContainer _container;

		public OfferUnitRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class PaymentConditionSetRepository : BaseRepository<PaymentConditionSet>, IPaymentConditionSetRepository
    {
        IUnityContainer _container;

		public PaymentConditionSetRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductBlockRepository : BaseRepository<ProductBlock>, IProductBlockRepository
    {
        IUnityContainer _container;

		public ProductBlockRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductDependentRepository : BaseRepository<ProductDependent>, IProductDependentRepository
    {
        IUnityContainer _container;

		public ProductDependentRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class BankDetailsRepository : BaseRepository<BankDetails>, IBankDetailsRepository
    {
        IUnityContainer _container;

		public BankDetailsRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        IUnityContainer _container;

		public CompanyRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class CompanyFormRepository : BaseRepository<CompanyForm>, ICompanyFormRepository
    {
        IUnityContainer _container;

		public CompanyFormRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class DocumentsRegistrationDetailsRepository : BaseRepository<DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsRepository
    {
        IUnityContainer _container;

		public DocumentsRegistrationDetailsRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class EmployeesPositionRepository : BaseRepository<EmployeesPosition>, IEmployeesPositionRepository
    {
        IUnityContainer _container;

		public EmployeesPositionRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class FacilityTypeRepository : BaseRepository<FacilityType>, IFacilityTypeRepository
    {
        IUnityContainer _container;

		public FacilityTypeRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ActivityFieldRepository : BaseRepository<ActivityField>, IActivityFieldRepository
    {
        IUnityContainer _container;

		public ActivityFieldRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
        IUnityContainer _container;

		public ContractRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class MeasureRepository : BaseRepository<Measure>, IMeasureRepository
    {
        IUnityContainer _container;

		public MeasureRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ParameterRepository : BaseRepository<Parameter>, IParameterRepository
    {
        IUnityContainer _container;

		public ParameterRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ParameterGroupRepository : BaseRepository<ParameterGroup>, IParameterGroupRepository
    {
        IUnityContainer _container;

		public ParameterGroupRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductRelationRepository : BaseRepository<ProductRelation>, IProductRelationRepository
    {
        IUnityContainer _container;

		public ProductRelationRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        IUnityContainer _container;

		public PersonRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ParameterRelationRepository : BaseRepository<ParameterRelation>, IParameterRelationRepository
    {
        IUnityContainer _container;

		public ParameterRelationRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class SalesUnitRepository : BaseRepository<SalesUnit>, ISalesUnitRepository
    {
        IUnityContainer _container;

		public SalesUnitRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        IUnityContainer _container;

		public DocumentRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class SumOnDateRepository : BaseRepository<SumOnDate>, ISumOnDateRepository
    {
        IUnityContainer _container;

		public SumOnDateRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        IUnityContainer _container;

		public ProductRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class OfferRepository : BaseRepository<Offer>, IOfferRepository
    {
        IUnityContainer _container;

		public OfferRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        IUnityContainer _container;

		public EmployeeRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        IUnityContainer _container;

		public OrderRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class PaymentConditionRepository : BaseRepository<PaymentCondition>, IPaymentConditionRepository
    {
        IUnityContainer _container;

		public PaymentConditionRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class PaymentDocumentRepository : BaseRepository<PaymentDocument>, IPaymentDocumentRepository
    {
        IUnityContainer _container;

		public PaymentDocumentRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class FacilityRepository : BaseRepository<Facility>, IFacilityRepository
    {
        IUnityContainer _container;

		public FacilityRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        IUnityContainer _container;

		public ProjectRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        IUnityContainer _container;

		public UserRoleRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class SpecificationRepository : BaseRepository<Specification>, ISpecificationRepository
    {
        IUnityContainer _container;

		public SpecificationRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class TenderRepository : BaseRepository<Tender>, ITenderRepository
    {
        IUnityContainer _container;

		public TenderRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class TenderTypeRepository : BaseRepository<TenderType>, ITenderTypeRepository
    {
        IUnityContainer _container;

		public TenderTypeRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
        IUnityContainer _container;

		public UserRepository(DbContext context, IUnityContainer container) : base(context) 
		{
			_container = container;
		}
    }

}
