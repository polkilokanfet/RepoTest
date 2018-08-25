using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CommonOptionWrapperRepository : WrapperRepository<CommonOption, CommonOptionWrapper>
    {
        public CommonOptionWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class AddressWrapperRepository : WrapperRepository<Address, AddressWrapper>
    {
        public AddressWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class CountryWrapperRepository : WrapperRepository<Country, CountryWrapper>
    {
        public CountryWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class DistrictWrapperRepository : WrapperRepository<District, DistrictWrapper>
    {
        public DistrictWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class LocalityWrapperRepository : WrapperRepository<Locality, LocalityWrapper>
    {
        public LocalityWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class LocalityTypeWrapperRepository : WrapperRepository<LocalityType, LocalityTypeWrapper>
    {
        public LocalityTypeWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class RegionWrapperRepository : WrapperRepository<Region, RegionWrapper>
    {
        public RegionWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class CalculatePriceTaskWrapperRepository : WrapperRepository<CalculatePriceTask, CalculatePriceTaskWrapper>
    {
        public CalculatePriceTaskWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class SumWrapperRepository : WrapperRepository<Sum, SumWrapper>
    {
        public SumWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class CurrencyExchangeRateWrapperRepository : WrapperRepository<CurrencyExchangeRate, CurrencyExchangeRateWrapper>
    {
        public CurrencyExchangeRateWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class DescribeProductBlockTaskWrapperRepository : WrapperRepository<DescribeProductBlockTask, DescribeProductBlockTaskWrapper>
    {
        public DescribeProductBlockTaskWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class NoteWrapperRepository : WrapperRepository<Note, NoteWrapper>
    {
        public NoteWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class OfferUnitWrapperRepository : WrapperRepository<OfferUnit, OfferUnitWrapper>
    {
        public OfferUnitWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class PaymentConditionSetWrapperRepository : WrapperRepository<PaymentConditionSet, PaymentConditionSetWrapper>
    {
        public PaymentConditionSetWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ProductBlockWrapperRepository : WrapperRepository<ProductBlock, ProductBlockWrapper>
    {
        public ProductBlockWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ProductDependentWrapperRepository : WrapperRepository<ProductDependent, ProductDependentWrapper>
    {
        public ProductDependentWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ProductionTaskWrapperRepository : WrapperRepository<ProductionTask, ProductionTaskWrapper>
    {
        public ProductionTaskWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class SalesBlockWrapperRepository : WrapperRepository<SalesBlock, SalesBlockWrapper>
    {
        public SalesBlockWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class BankDetailsWrapperRepository : WrapperRepository<BankDetails, BankDetailsWrapper>
    {
        public BankDetailsWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class CompanyWrapperRepository : WrapperRepository<Company, CompanyWrapper>
    {
        public CompanyWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class CompanyFormWrapperRepository : WrapperRepository<CompanyForm, CompanyFormWrapper>
    {
        public CompanyFormWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class DocumentsRegistrationDetailsWrapperRepository : WrapperRepository<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>
    {
        public DocumentsRegistrationDetailsWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class EmployeesPositionWrapperRepository : WrapperRepository<EmployeesPosition, EmployeesPositionWrapper>
    {
        public EmployeesPositionWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class FacilityTypeWrapperRepository : WrapperRepository<FacilityType, FacilityTypeWrapper>
    {
        public FacilityTypeWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ActivityFieldWrapperRepository : WrapperRepository<ActivityField, ActivityFieldWrapper>
    {
        public ActivityFieldWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ContractWrapperRepository : WrapperRepository<Contract, ContractWrapper>
    {
        public ContractWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class MeasureWrapperRepository : WrapperRepository<Measure, MeasureWrapper>
    {
        public MeasureWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ParameterWrapperRepository : WrapperRepository<Parameter, ParameterWrapper>
    {
        public ParameterWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ParameterGroupWrapperRepository : WrapperRepository<ParameterGroup, ParameterGroupWrapper>
    {
        public ParameterGroupWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ProductRelationWrapperRepository : WrapperRepository<ProductRelation, ProductRelationWrapper>
    {
        public ProductRelationWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class PersonWrapperRepository : WrapperRepository<Person, PersonWrapper>
    {
        public PersonWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class PaymentPlannedListWrapperRepository : WrapperRepository<PaymentPlannedList, PaymentPlannedListWrapper>
    {
        public PaymentPlannedListWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class PaymentPlannedWrapperRepository : WrapperRepository<PaymentPlanned, PaymentPlannedWrapper>
    {
        public PaymentPlannedWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class PaymentActualWrapperRepository : WrapperRepository<PaymentActual, PaymentActualWrapper>
    {
        public PaymentActualWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ParameterRelationWrapperRepository : WrapperRepository<ParameterRelation, ParameterRelationWrapper>
    {
        public ParameterRelationWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class SalesUnitWrapperRepository : WrapperRepository<SalesUnit, SalesUnitWrapper>
    {
        public SalesUnitWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ServiceWrapperRepository : WrapperRepository<Service, ServiceWrapper>
    {
        public ServiceWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TestFriendAddressWrapperRepository : WrapperRepository<TestFriendAddress, TestFriendAddressWrapper>
    {
        public TestFriendAddressWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TestFriendWrapperRepository : WrapperRepository<TestFriend, TestFriendWrapper>
    {
        public TestFriendWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TestFriendEmailWrapperRepository : WrapperRepository<TestFriendEmail, TestFriendEmailWrapper>
    {
        public TestFriendEmailWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TestFriendGroupWrapperRepository : WrapperRepository<TestFriendGroup, TestFriendGroupWrapper>
    {
        public TestFriendGroupWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class DocumentWrapperRepository : WrapperRepository<Document, DocumentWrapper>
    {
        public DocumentWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TestEntityWrapperRepository : WrapperRepository<TestEntity, TestEntityWrapper>
    {
        public TestEntityWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TestHusbandWrapperRepository : WrapperRepository<TestHusband, TestHusbandWrapper>
    {
        public TestHusbandWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TestWifeWrapperRepository : WrapperRepository<TestWife, TestWifeWrapper>
    {
        public TestWifeWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TestChildWrapperRepository : WrapperRepository<TestChild, TestChildWrapper>
    {
        public TestChildWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class SumOnDateWrapperRepository : WrapperRepository<SumOnDate, SumOnDateWrapper>
    {
        public SumOnDateWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ProductWrapperRepository : WrapperRepository<Product, ProductWrapper>
    {
        public ProductWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class OfferWrapperRepository : WrapperRepository<Offer, OfferWrapper>
    {
        public OfferWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class EmployeeWrapperRepository : WrapperRepository<Employee, EmployeeWrapper>
    {
        public EmployeeWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class OrderWrapperRepository : WrapperRepository<Order, OrderWrapper>
    {
        public OrderWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class PaymentConditionWrapperRepository : WrapperRepository<PaymentCondition, PaymentConditionWrapper>
    {
        public PaymentConditionWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class PaymentDocumentWrapperRepository : WrapperRepository<PaymentDocument, PaymentDocumentWrapper>
    {
        public PaymentDocumentWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class FacilityWrapperRepository : WrapperRepository<Facility, FacilityWrapper>
    {
        public FacilityWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class ProjectWrapperRepository : WrapperRepository<Project, ProjectWrapper>
    {
        public ProjectWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class UserRoleWrapperRepository : WrapperRepository<UserRole, UserRoleWrapper>
    {
        public UserRoleWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class SpecificationWrapperRepository : WrapperRepository<Specification, SpecificationWrapper>
    {
        public SpecificationWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TenderWrapperRepository : WrapperRepository<Tender, TenderWrapper>
    {
        public TenderWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class TenderTypeWrapperRepository : WrapperRepository<TenderType, TenderTypeWrapper>
    {
        public TenderTypeWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

    public partial class UserWrapperRepository : WrapperRepository<User, UserWrapper>
    {
        public UserWrapperRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
