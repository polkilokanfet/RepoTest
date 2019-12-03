using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public partial interface ICountryUnionRepository : IRepository<CountryUnion> { }
    public partial interface IBankGuaranteeRepository : IRepository<BankGuarantee> { }
    public partial interface IBankGuaranteeTypeRepository : IRepository<BankGuaranteeType> { }
    public partial interface ICreateNewProductTaskRepository : IRepository<CreateNewProductTask> { }
    public partial interface IDocumentNumberRepository : IRepository<DocumentNumber> { }
    public partial interface IFakeDataRepository : IRepository<FakeData> { }
    public partial interface ILosingReasonRepository : IRepository<LosingReason> { }
    public partial interface IMarketFieldRepository : IRepository<MarketField> { }
    public partial interface IPaymentActualRepository : IRepository<PaymentActual> { }
    public partial interface IPaymentConditionPointRepository : IRepository<PaymentConditionPoint> { }
    public partial interface IPaymentPlannedRepository : IRepository<PaymentPlanned> { }
    public partial interface IPenaltyRepository : IRepository<Penalty> { }
    public partial interface IPriceCalculationRepository : IRepository<PriceCalculation> { }
    public partial interface IPriceCalculationItemRepository : IRepository<PriceCalculationItem> { }
    public partial interface IProductIncludedRepository : IRepository<ProductIncluded> { }
    public partial interface IProductDesignationRepository : IRepository<ProductDesignation> { }
    public partial interface IProductTypeRepository : IRepository<ProductType> { }
    public partial interface IProductTypeDesignationRepository : IRepository<ProductTypeDesignation> { }
    public partial interface IProjectTypeRepository : IRepository<ProjectType> { }
    public partial interface IStandartMarginalIncomeRepository : IRepository<StandartMarginalIncome> { }
    public partial interface IStandartProductionTermRepository : IRepository<StandartProductionTerm> { }
    public partial interface IStructureCostRepository : IRepository<StructureCost> { }
    public partial interface IGlobalPropertiesRepository : IRepository<GlobalProperties> { }
    public partial interface IAddressRepository : IRepository<Address> { }
    public partial interface ICountryRepository : IRepository<Country> { }
    public partial interface IDistrictRepository : IRepository<District> { }
    public partial interface ILocalityRepository : IRepository<Locality> { }
    public partial interface ILocalityTypeRepository : IRepository<LocalityType> { }
    public partial interface IRegionRepository : IRepository<Region> { }
    public partial interface ISumRepository : IRepository<Sum> { }
    public partial interface ICurrencyExchangeRateRepository : IRepository<CurrencyExchangeRate> { }
    public partial interface INoteRepository : IRepository<Note> { }
    public partial interface IOfferUnitRepository : IRepository<OfferUnit> { }
    public partial interface IPaymentConditionSetRepository : IRepository<PaymentConditionSet> { }
    public partial interface IProductBlockRepository : IRepository<ProductBlock> { }
    public partial interface IProductDependentRepository : IRepository<ProductDependent> { }
    public partial interface IBankDetailsRepository : IRepository<BankDetails> { }
    public partial interface ICompanyRepository : IRepository<Company> { }
    public partial interface ICompanyFormRepository : IRepository<CompanyForm> { }
    public partial interface IDocumentsRegistrationDetailsRepository : IRepository<DocumentsRegistrationDetails> { }
    public partial interface IEmployeesPositionRepository : IRepository<EmployeesPosition> { }
    public partial interface IFacilityTypeRepository : IRepository<FacilityType> { }
    public partial interface IActivityFieldRepository : IRepository<ActivityField> { }
    public partial interface IContractRepository : IRepository<Contract> { }
    public partial interface IMeasureRepository : IRepository<Measure> { }
    public partial interface IParameterRepository : IRepository<Parameter> { }
    public partial interface IParameterGroupRepository : IRepository<ParameterGroup> { }
    public partial interface IProductRelationRepository : IRepository<ProductRelation> { }
    public partial interface IPersonRepository : IRepository<Person> { }
    public partial interface IParameterRelationRepository : IRepository<ParameterRelation> { }
    public partial interface ISalesUnitRepository : IRepository<SalesUnit> { }
    public partial interface IDocumentRepository : IRepository<Document> { }
    public partial interface ISumOnDateRepository : IRepository<SumOnDate> { }
    public partial interface IProductRepository : IRepository<Product> { }
    public partial interface IOfferRepository : IRepository<Offer> { }
    public partial interface IEmployeeRepository : IRepository<Employee> { }
    public partial interface IOrderRepository : IRepository<Order> { }
    public partial interface IPaymentConditionRepository : IRepository<PaymentCondition> { }
    public partial interface IPaymentDocumentRepository : IRepository<PaymentDocument> { }
    public partial interface IFacilityRepository : IRepository<Facility> { }
    public partial interface IProjectRepository : IRepository<Project> { }
    public partial interface IUserRoleRepository : IRepository<UserRole> { }
    public partial interface ISpecificationRepository : IRepository<Specification> { }
    public partial interface ITenderRepository : IRepository<Tender> { }
    public partial interface ITenderTypeRepository : IRepository<TenderType> { }
    public partial interface IUserRepository : IRepository<User> { }

}
