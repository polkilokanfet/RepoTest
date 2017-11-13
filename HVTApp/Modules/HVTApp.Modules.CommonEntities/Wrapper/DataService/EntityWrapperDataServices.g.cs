using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class AddressWrapperDataService : EntityWrapperDataService<Address, AddressWrapper>
    {
        public AddressWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override AddressWrapper GenerateWrapper(Address model)
        {
            return new AddressWrapper(model);
        }
    }

    public partial class CountryWrapperDataService : EntityWrapperDataService<Country, CountryWrapper>
    {
        public CountryWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CountryWrapper GenerateWrapper(Country model)
        {
            return new CountryWrapper(model);
        }
    }

    public partial class DistrictWrapperDataService : EntityWrapperDataService<District, DistrictWrapper>
    {
        public DistrictWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override DistrictWrapper GenerateWrapper(District model)
        {
            return new DistrictWrapper(model);
        }
    }

    public partial class LocalityWrapperDataService : EntityWrapperDataService<Locality, LocalityWrapper>
    {
        public LocalityWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override LocalityWrapper GenerateWrapper(Locality model)
        {
            return new LocalityWrapper(model);
        }
    }

    public partial class LocalityTypeWrapperDataService : EntityWrapperDataService<LocalityType, LocalityTypeWrapper>
    {
        public LocalityTypeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override LocalityTypeWrapper GenerateWrapper(LocalityType model)
        {
            return new LocalityTypeWrapper(model);
        }
    }

    public partial class RegionWrapperDataService : EntityWrapperDataService<Region, RegionWrapper>
    {
        public RegionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override RegionWrapper GenerateWrapper(Region model)
        {
            return new RegionWrapper(model);
        }
    }

    public partial class AdditionalSalesUnitsWrapperDataService : EntityWrapperDataService<AdditionalSalesUnits, AdditionalSalesUnitsWrapper>
    {
        public AdditionalSalesUnitsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override AdditionalSalesUnitsWrapper GenerateWrapper(AdditionalSalesUnits model)
        {
            return new AdditionalSalesUnitsWrapper(model);
        }
    }

    public partial class BankDetailsWrapperDataService : EntityWrapperDataService<BankDetails, BankDetailsWrapper>
    {
        public BankDetailsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override BankDetailsWrapper GenerateWrapper(BankDetails model)
        {
            return new BankDetailsWrapper(model);
        }
    }

    public partial class CompanyWrapperDataService : EntityWrapperDataService<Company, CompanyWrapper>
    {
        public CompanyWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CompanyWrapper GenerateWrapper(Company model)
        {
            return new CompanyWrapper(model);
        }
    }

    public partial class CompanyFormWrapperDataService : EntityWrapperDataService<CompanyForm, CompanyFormWrapper>
    {
        public CompanyFormWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CompanyFormWrapper GenerateWrapper(CompanyForm model)
        {
            return new CompanyFormWrapper(model);
        }
    }

    public partial class DocumentsRegistrationDetailsWrapperDataService : EntityWrapperDataService<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>
    {
        public DocumentsRegistrationDetailsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override DocumentsRegistrationDetailsWrapper GenerateWrapper(DocumentsRegistrationDetails model)
        {
            return new DocumentsRegistrationDetailsWrapper(model);
        }
    }

    public partial class EmployeesPositionWrapperDataService : EntityWrapperDataService<EmployeesPosition, EmployeesPositionWrapper>
    {
        public EmployeesPositionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override EmployeesPositionWrapper GenerateWrapper(EmployeesPosition model)
        {
            return new EmployeesPositionWrapper(model);
        }
    }

    public partial class FacilityTypeWrapperDataService : EntityWrapperDataService<FacilityType, FacilityTypeWrapper>
    {
        public FacilityTypeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override FacilityTypeWrapper GenerateWrapper(FacilityType model)
        {
            return new FacilityTypeWrapper(model);
        }
    }

    public partial class ActivityFieldWrapperDataService : EntityWrapperDataService<ActivityField, ActivityFieldWrapper>
    {
        public ActivityFieldWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ActivityFieldWrapper GenerateWrapper(ActivityField model)
        {
            return new ActivityFieldWrapper(model);
        }
    }

    public partial class ContractWrapperDataService : EntityWrapperDataService<Contract, ContractWrapper>
    {
        public ContractWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ContractWrapper GenerateWrapper(Contract model)
        {
            return new ContractWrapper(model);
        }
    }

    public partial class MeasureWrapperDataService : EntityWrapperDataService<Measure, MeasureWrapper>
    {
        public MeasureWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override MeasureWrapper GenerateWrapper(Measure model)
        {
            return new MeasureWrapper(model);
        }
    }

    public partial class ParameterWrapperDataService : EntityWrapperDataService<Parameter, ParameterWrapper>
    {
        public ParameterWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ParameterWrapper GenerateWrapper(Parameter model)
        {
            return new ParameterWrapper(model);
        }
    }

    public partial class ParameterGroupWrapperDataService : EntityWrapperDataService<ParameterGroup, ParameterGroupWrapper>
    {
        public ParameterGroupWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ParameterGroupWrapper GenerateWrapper(ParameterGroup model)
        {
            return new ParameterGroupWrapper(model);
        }
    }

    public partial class PartWrapperDataService : EntityWrapperDataService<Part, PartWrapper>
    {
        public PartWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PartWrapper GenerateWrapper(Part model)
        {
            return new PartWrapper(model);
        }
    }

    public partial class ProductsRelationWrapperDataService : EntityWrapperDataService<ProductsRelation, ProductsRelationWrapper>
    {
        public ProductsRelationWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProductsRelationWrapper GenerateWrapper(ProductsRelation model)
        {
            return new ProductsRelationWrapper(model);
        }
    }

    public partial class StandartPaymentConditionsWrapperDataService : EntityWrapperDataService<StandartPaymentConditions, StandartPaymentConditionsWrapper>
    {
        public StandartPaymentConditionsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override StandartPaymentConditionsWrapper GenerateWrapper(StandartPaymentConditions model)
        {
            return new StandartPaymentConditionsWrapper(model);
        }
    }

    public partial class PersonWrapperDataService : EntityWrapperDataService<Person, PersonWrapper>
    {
        public PersonWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PersonWrapper GenerateWrapper(Person model)
        {
            return new PersonWrapper(model);
        }
    }

    public partial class PaymentPlannedWrapperDataService : EntityWrapperDataService<PaymentPlanned, PaymentPlannedWrapper>
    {
        public PaymentPlannedWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentPlannedWrapper GenerateWrapper(PaymentPlanned model)
        {
            return new PaymentPlannedWrapper(model);
        }
    }

    public partial class PaymentActualWrapperDataService : EntityWrapperDataService<PaymentActual, PaymentActualWrapper>
    {
        public PaymentActualWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentActualWrapper GenerateWrapper(PaymentActual model)
        {
            return new PaymentActualWrapper(model);
        }
    }

    public partial class RequiredPreviousParametersWrapperDataService : EntityWrapperDataService<RequiredPreviousParameters, RequiredPreviousParametersWrapper>
    {
        public RequiredPreviousParametersWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override RequiredPreviousParametersWrapper GenerateWrapper(RequiredPreviousParameters model)
        {
            return new RequiredPreviousParametersWrapper(model);
        }
    }

    public partial class ProjectUnitWrapperDataService : EntityWrapperDataService<ProjectUnit, ProjectUnitWrapper>
    {
        public ProjectUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProjectUnitWrapper GenerateWrapper(ProjectUnit model)
        {
            return new ProjectUnitWrapper(model);
        }
    }

    public partial class TenderUnitWrapperDataService : EntityWrapperDataService<TenderUnit, TenderUnitWrapper>
    {
        public TenderUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TenderUnitWrapper GenerateWrapper(TenderUnit model)
        {
            return new TenderUnitWrapper(model);
        }
    }

    public partial class ShipmentUnitWrapperDataService : EntityWrapperDataService<ShipmentUnit, ShipmentUnitWrapper>
    {
        public ShipmentUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ShipmentUnitWrapper GenerateWrapper(ShipmentUnit model)
        {
            return new ShipmentUnitWrapper(model);
        }
    }

    public partial class ProductionUnitWrapperDataService : EntityWrapperDataService<ProductionUnit, ProductionUnitWrapper>
    {
        public ProductionUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProductionUnitWrapper GenerateWrapper(ProductionUnit model)
        {
            return new ProductionUnitWrapper(model);
        }
    }

    public partial class SalesUnitWrapperDataService : EntityWrapperDataService<SalesUnit, SalesUnitWrapper>
    {
        public SalesUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override SalesUnitWrapper GenerateWrapper(SalesUnit model)
        {
            return new SalesUnitWrapper(model);
        }
    }

    public partial class TestFriendAddressWrapperDataService : EntityWrapperDataService<TestFriendAddress, TestFriendAddressWrapper>
    {
        public TestFriendAddressWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendAddressWrapper GenerateWrapper(TestFriendAddress model)
        {
            return new TestFriendAddressWrapper(model);
        }
    }

    public partial class TestFriendWrapperDataService : EntityWrapperDataService<TestFriend, TestFriendWrapper>
    {
        public TestFriendWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendWrapper GenerateWrapper(TestFriend model)
        {
            return new TestFriendWrapper(model);
        }
    }

    public partial class TestFriendEmailWrapperDataService : EntityWrapperDataService<TestFriendEmail, TestFriendEmailWrapper>
    {
        public TestFriendEmailWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendEmailWrapper GenerateWrapper(TestFriendEmail model)
        {
            return new TestFriendEmailWrapper(model);
        }
    }

    public partial class TestFriendGroupWrapperDataService : EntityWrapperDataService<TestFriendGroup, TestFriendGroupWrapper>
    {
        public TestFriendGroupWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendGroupWrapper GenerateWrapper(TestFriendGroup model)
        {
            return new TestFriendGroupWrapper(model);
        }
    }

    public partial class DocumentWrapperDataService : EntityWrapperDataService<Document, DocumentWrapper>
    {
        public DocumentWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override DocumentWrapper GenerateWrapper(Document model)
        {
            return new DocumentWrapper(model);
        }
    }

    public partial class TestEntityWrapperDataService : EntityWrapperDataService<TestEntity, TestEntityWrapper>
    {
        public TestEntityWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestEntityWrapper GenerateWrapper(TestEntity model)
        {
            return new TestEntityWrapper(model);
        }
    }

    public partial class TestHusbandWrapperDataService : EntityWrapperDataService<TestHusband, TestHusbandWrapper>
    {
        public TestHusbandWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestHusbandWrapper GenerateWrapper(TestHusband model)
        {
            return new TestHusbandWrapper(model);
        }
    }

    public partial class TestWifeWrapperDataService : EntityWrapperDataService<TestWife, TestWifeWrapper>
    {
        public TestWifeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestWifeWrapper GenerateWrapper(TestWife model)
        {
            return new TestWifeWrapper(model);
        }
    }

    public partial class TestChildWrapperDataService : EntityWrapperDataService<TestChild, TestChildWrapper>
    {
        public TestChildWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestChildWrapper GenerateWrapper(TestChild model)
        {
            return new TestChildWrapper(model);
        }
    }

    public partial class CostOnDateWrapperDataService : EntityWrapperDataService<CostOnDate, CostOnDateWrapper>
    {
        public CostOnDateWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CostOnDateWrapper GenerateWrapper(CostOnDate model)
        {
            return new CostOnDateWrapper(model);
        }
    }

    public partial class CostWrapperDataService : EntityWrapperDataService<Cost, CostWrapper>
    {
        public CostWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CostWrapper GenerateWrapper(Cost model)
        {
            return new CostWrapper(model);
        }
    }

    public partial class CurrencyWrapperDataService : EntityWrapperDataService<Currency, CurrencyWrapper>
    {
        public CurrencyWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CurrencyWrapper GenerateWrapper(Currency model)
        {
            return new CurrencyWrapper(model);
        }
    }

    public partial class ExchangeCurrencyRateWrapperDataService : EntityWrapperDataService<ExchangeCurrencyRate, ExchangeCurrencyRateWrapper>
    {
        public ExchangeCurrencyRateWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ExchangeCurrencyRateWrapper GenerateWrapper(ExchangeCurrencyRate model)
        {
            return new ExchangeCurrencyRateWrapper(model);
        }
    }

    public partial class ProductWrapperDataService : EntityWrapperDataService<Product, ProductWrapper>
    {
        public ProductWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProductWrapper GenerateWrapper(Product model)
        {
            return new ProductWrapper(model);
        }
    }

    public partial class OfferWrapperDataService : EntityWrapperDataService<Offer, OfferWrapper>
    {
        public OfferWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override OfferWrapper GenerateWrapper(Offer model)
        {
            return new OfferWrapper(model);
        }
    }

    public partial class EmployeeWrapperDataService : EntityWrapperDataService<Employee, EmployeeWrapper>
    {
        public EmployeeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override EmployeeWrapper GenerateWrapper(Employee model)
        {
            return new EmployeeWrapper(model);
        }
    }

    public partial class OrderWrapperDataService : EntityWrapperDataService<Order, OrderWrapper>
    {
        public OrderWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override OrderWrapper GenerateWrapper(Order model)
        {
            return new OrderWrapper(model);
        }
    }

    public partial class PaymentConditionWrapperDataService : EntityWrapperDataService<PaymentCondition, PaymentConditionWrapper>
    {
        public PaymentConditionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentConditionWrapper GenerateWrapper(PaymentCondition model)
        {
            return new PaymentConditionWrapper(model);
        }
    }

    public partial class PaymentDocumentWrapperDataService : EntityWrapperDataService<PaymentDocument, PaymentDocumentWrapper>
    {
        public PaymentDocumentWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentDocumentWrapper GenerateWrapper(PaymentDocument model)
        {
            return new PaymentDocumentWrapper(model);
        }
    }

    public partial class FacilityWrapperDataService : EntityWrapperDataService<Facility, FacilityWrapper>
    {
        public FacilityWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override FacilityWrapper GenerateWrapper(Facility model)
        {
            return new FacilityWrapper(model);
        }
    }

    public partial class ProjectWrapperDataService : EntityWrapperDataService<Project, ProjectWrapper>
    {
        public ProjectWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProjectWrapper GenerateWrapper(Project model)
        {
            return new ProjectWrapper(model);
        }
    }

    public partial class UserRoleWrapperDataService : EntityWrapperDataService<UserRole, UserRoleWrapper>
    {
        public UserRoleWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override UserRoleWrapper GenerateWrapper(UserRole model)
        {
            return new UserRoleWrapper(model);
        }
    }

    public partial class SpecificationWrapperDataService : EntityWrapperDataService<Specification, SpecificationWrapper>
    {
        public SpecificationWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override SpecificationWrapper GenerateWrapper(Specification model)
        {
            return new SpecificationWrapper(model);
        }
    }

    public partial class TenderWrapperDataService : EntityWrapperDataService<Tender, TenderWrapper>
    {
        public TenderWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TenderWrapper GenerateWrapper(Tender model)
        {
            return new TenderWrapper(model);
        }
    }

    public partial class TenderTypeWrapperDataService : EntityWrapperDataService<TenderType, TenderTypeWrapper>
    {
        public TenderTypeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TenderTypeWrapper GenerateWrapper(TenderType model)
        {
            return new TenderTypeWrapper(model);
        }
    }

    public partial class UserWrapperDataService : EntityWrapperDataService<User, UserWrapper>
    {
        public UserWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override UserWrapper GenerateWrapper(User model)
        {
            return new UserWrapper(model);
        }
    }

    public partial class OfferUnitWrapperDataService : EntityWrapperDataService<OfferUnit, OfferUnitWrapper>
    {
        public OfferUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override OfferUnitWrapper GenerateWrapper(OfferUnit model)
        {
            return new OfferUnitWrapper(model);
        }
    }

}
