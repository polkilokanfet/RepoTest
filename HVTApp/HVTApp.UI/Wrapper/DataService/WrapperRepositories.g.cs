using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{

    public partial class CreateNewProductTaskWrapperRepository : WrapperRepository<CreateNewProductTask, CreateNewProductTaskWrapper>
    {
        public CreateNewProductTaskWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class PaymentActualWrapperRepository : WrapperRepository<PaymentActual, PaymentActualWrapper>
    {
        public PaymentActualWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class PaymentPlannedWrapperRepository : WrapperRepository<PaymentPlanned, PaymentPlannedWrapper>
    {
        public PaymentPlannedWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductBlockIsServiceWrapperRepository : WrapperRepository<ProductBlockIsService, ProductBlockIsServiceWrapper>
    {
        public ProductBlockIsServiceWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductIncludedWrapperRepository : WrapperRepository<ProductIncluded, ProductIncludedWrapper>
    {
        public ProductIncludedWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductDesignationWrapperRepository : WrapperRepository<ProductDesignation, ProductDesignationWrapper>
    {
        public ProductDesignationWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductTypeWrapperRepository : WrapperRepository<ProductType, ProductTypeWrapper>
    {
        public ProductTypeWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductTypeDesignationWrapperRepository : WrapperRepository<ProductTypeDesignation, ProductTypeDesignationWrapper>
    {
        public ProductTypeDesignationWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProjectTypeWrapperRepository : WrapperRepository<ProjectType, ProjectTypeWrapper>
    {
        public ProjectTypeWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class CommonOptionWrapperRepository : WrapperRepository<CommonOption, CommonOptionWrapper>
    {
        public CommonOptionWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class AddressWrapperRepository : WrapperRepository<Address, AddressWrapper>
    {
        public AddressWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class CountryWrapperRepository : WrapperRepository<Country, CountryWrapper>
    {
        public CountryWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class DistrictWrapperRepository : WrapperRepository<District, DistrictWrapper>
    {
        public DistrictWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class LocalityWrapperRepository : WrapperRepository<Locality, LocalityWrapper>
    {
        public LocalityWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class LocalityTypeWrapperRepository : WrapperRepository<LocalityType, LocalityTypeWrapper>
    {
        public LocalityTypeWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class RegionWrapperRepository : WrapperRepository<Region, RegionWrapper>
    {
        public RegionWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class CalculatePriceTaskWrapperRepository : WrapperRepository<CalculatePriceTask, CalculatePriceTaskWrapper>
    {
        public CalculatePriceTaskWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class SumWrapperRepository : WrapperRepository<Sum, SumWrapper>
    {
        public SumWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class CurrencyExchangeRateWrapperRepository : WrapperRepository<CurrencyExchangeRate, CurrencyExchangeRateWrapper>
    {
        public CurrencyExchangeRateWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class DescribeProductBlockTaskWrapperRepository : WrapperRepository<DescribeProductBlockTask, DescribeProductBlockTaskWrapper>
    {
        public DescribeProductBlockTaskWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class NoteWrapperRepository : WrapperRepository<Note, NoteWrapper>
    {
        public NoteWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class OfferUnitWrapperRepository : WrapperRepository<OfferUnit, OfferUnitWrapper>
    {
        public OfferUnitWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class PaymentConditionSetWrapperRepository : WrapperRepository<PaymentConditionSet, PaymentConditionSetWrapper>
    {
        public PaymentConditionSetWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductBlockWrapperRepository : WrapperRepository<ProductBlock, ProductBlockWrapper>
    {
        public ProductBlockWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductDependentWrapperRepository : WrapperRepository<ProductDependent, ProductDependentWrapper>
    {
        public ProductDependentWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductionTaskWrapperRepository : WrapperRepository<ProductionTask, ProductionTaskWrapper>
    {
        public ProductionTaskWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class SalesBlockWrapperRepository : WrapperRepository<SalesBlock, SalesBlockWrapper>
    {
        public SalesBlockWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class BankDetailsWrapperRepository : WrapperRepository<BankDetails, BankDetailsWrapper>
    {
        public BankDetailsWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class CompanyWrapperRepository : WrapperRepository<Company, CompanyWrapper>
    {
        public CompanyWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class CompanyFormWrapperRepository : WrapperRepository<CompanyForm, CompanyFormWrapper>
    {
        public CompanyFormWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class DocumentsRegistrationDetailsWrapperRepository : WrapperRepository<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>
    {
        public DocumentsRegistrationDetailsWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class EmployeesPositionWrapperRepository : WrapperRepository<EmployeesPosition, EmployeesPositionWrapper>
    {
        public EmployeesPositionWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class FacilityTypeWrapperRepository : WrapperRepository<FacilityType, FacilityTypeWrapper>
    {
        public FacilityTypeWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ActivityFieldWrapperRepository : WrapperRepository<ActivityField, ActivityFieldWrapper>
    {
        public ActivityFieldWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ContractWrapperRepository : WrapperRepository<Contract, ContractWrapper>
    {
        public ContractWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class MeasureWrapperRepository : WrapperRepository<Measure, MeasureWrapper>
    {
        public MeasureWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ParameterWrapperRepository : WrapperRepository<Parameter, ParameterWrapper>
    {
        public ParameterWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ParameterGroupWrapperRepository : WrapperRepository<ParameterGroup, ParameterGroupWrapper>
    {
        public ParameterGroupWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductRelationWrapperRepository : WrapperRepository<ProductRelation, ProductRelationWrapper>
    {
        public ProductRelationWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class PersonWrapperRepository : WrapperRepository<Person, PersonWrapper>
    {
        public PersonWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ParameterRelationWrapperRepository : WrapperRepository<ParameterRelation, ParameterRelationWrapper>
    {
        public ParameterRelationWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class SalesUnitWrapperRepository : WrapperRepository<SalesUnit, SalesUnitWrapper>
    {
        public SalesUnitWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ServiceWrapperRepository : WrapperRepository<Service, ServiceWrapper>
    {
        public ServiceWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TestFriendAddressWrapperRepository : WrapperRepository<TestFriendAddress, TestFriendAddressWrapper>
    {
        public TestFriendAddressWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TestFriendWrapperRepository : WrapperRepository<TestFriend, TestFriendWrapper>
    {
        public TestFriendWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TestFriendEmailWrapperRepository : WrapperRepository<TestFriendEmail, TestFriendEmailWrapper>
    {
        public TestFriendEmailWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TestFriendGroupWrapperRepository : WrapperRepository<TestFriendGroup, TestFriendGroupWrapper>
    {
        public TestFriendGroupWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class DocumentWrapperRepository : WrapperRepository<Document, DocumentWrapper>
    {
        public DocumentWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TestEntityWrapperRepository : WrapperRepository<TestEntity, TestEntityWrapper>
    {
        public TestEntityWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TestHusbandWrapperRepository : WrapperRepository<TestHusband, TestHusbandWrapper>
    {
        public TestHusbandWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TestWifeWrapperRepository : WrapperRepository<TestWife, TestWifeWrapper>
    {
        public TestWifeWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TestChildWrapperRepository : WrapperRepository<TestChild, TestChildWrapper>
    {
        public TestChildWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class SumOnDateWrapperRepository : WrapperRepository<SumOnDate, SumOnDateWrapper>
    {
        public SumOnDateWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProductWrapperRepository : WrapperRepository<Product, ProductWrapper>
    {
        public ProductWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class OfferWrapperRepository : WrapperRepository<Offer, OfferWrapper>
    {
        public OfferWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class EmployeeWrapperRepository : WrapperRepository<Employee, EmployeeWrapper>
    {
        public EmployeeWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class OrderWrapperRepository : WrapperRepository<Order, OrderWrapper>
    {
        public OrderWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class PaymentConditionWrapperRepository : WrapperRepository<PaymentCondition, PaymentConditionWrapper>
    {
        public PaymentConditionWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class PaymentDocumentWrapperRepository : WrapperRepository<PaymentDocument, PaymentDocumentWrapper>
    {
        public PaymentDocumentWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class FacilityWrapperRepository : WrapperRepository<Facility, FacilityWrapper>
    {
        public FacilityWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class ProjectWrapperRepository : WrapperRepository<Project, ProjectWrapper>
    {
        public ProjectWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class UserRoleWrapperRepository : WrapperRepository<UserRole, UserRoleWrapper>
    {
        public UserRoleWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class SpecificationWrapperRepository : WrapperRepository<Specification, SpecificationWrapper>
    {
        public SpecificationWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TenderWrapperRepository : WrapperRepository<Tender, TenderWrapper>
    {
        public TenderWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class TenderTypeWrapperRepository : WrapperRepository<TenderType, TenderTypeWrapper>
    {
        public TenderTypeWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


    public partial class UserWrapperRepository : WrapperRepository<User, UserWrapper>
    {
        public UserWrapperRepository(IWrapperDataService wrapperDataService) : base(wrapperDataService) { }
    }


}
