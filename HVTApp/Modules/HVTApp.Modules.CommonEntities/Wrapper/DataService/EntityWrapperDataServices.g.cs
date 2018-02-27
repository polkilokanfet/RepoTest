using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{

    public partial class CommonOptionWrapperDataService : EntityWrapperDataService<CommonOption, CommonOptionWrapper>
    {
        public CommonOptionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CommonOptionWrapper GenerateWrapper(CommonOption model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CommonOptionWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class AddressWrapperDataService : EntityWrapperDataService<Address, AddressWrapper>
    {
        public AddressWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override AddressWrapper GenerateWrapper(Address model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new AddressWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class CountryWrapperDataService : EntityWrapperDataService<Country, CountryWrapper>
    {
        public CountryWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CountryWrapper GenerateWrapper(Country model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CountryWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class DistrictWrapperDataService : EntityWrapperDataService<District, DistrictWrapper>
    {
        public DistrictWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override DistrictWrapper GenerateWrapper(District model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new DistrictWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class LocalityWrapperDataService : EntityWrapperDataService<Locality, LocalityWrapper>
    {
        public LocalityWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override LocalityWrapper GenerateWrapper(Locality model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new LocalityWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class LocalityTypeWrapperDataService : EntityWrapperDataService<LocalityType, LocalityTypeWrapper>
    {
        public LocalityTypeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override LocalityTypeWrapper GenerateWrapper(LocalityType model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new LocalityTypeWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class RegionWrapperDataService : EntityWrapperDataService<Region, RegionWrapper>
    {
        public RegionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override RegionWrapper GenerateWrapper(Region model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new RegionWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class CalculatePriceTaskWrapperDataService : EntityWrapperDataService<CalculatePriceTask, CalculatePriceTaskWrapper>
    {
        public CalculatePriceTaskWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CalculatePriceTaskWrapper GenerateWrapper(CalculatePriceTask model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CalculatePriceTaskWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class CostWrapperDataService : EntityWrapperDataService<Cost, CostWrapper>
    {
        public CostWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CostWrapper GenerateWrapper(Cost model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CostWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class CurrencyWrapperDataService : EntityWrapperDataService<Currency, CurrencyWrapper>
    {
        public CurrencyWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CurrencyWrapper GenerateWrapper(Currency model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CurrencyWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class CurrencyExchangeRateWrapperDataService : EntityWrapperDataService<CurrencyExchangeRate, CurrencyExchangeRateWrapper>
    {
        public CurrencyExchangeRateWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CurrencyExchangeRateWrapper GenerateWrapper(CurrencyExchangeRate model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CurrencyExchangeRateWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class DescribeProductBlockTaskWrapperDataService : EntityWrapperDataService<DescribeProductBlockTask, DescribeProductBlockTaskWrapper>
    {
        public DescribeProductBlockTaskWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override DescribeProductBlockTaskWrapper GenerateWrapper(DescribeProductBlockTask model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new DescribeProductBlockTaskWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class PaymentConditionSetWrapperDataService : EntityWrapperDataService<PaymentConditionSet, PaymentConditionSetWrapper>
    {
        public PaymentConditionSetWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentConditionSetWrapper GenerateWrapper(PaymentConditionSet model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new PaymentConditionSetWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ProductBlockWrapperDataService : EntityWrapperDataService<ProductBlock, ProductBlockWrapper>
    {
        public ProductBlockWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProductBlockWrapper GenerateWrapper(ProductBlock model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ProductBlockWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class SalesBlockWrapperDataService : EntityWrapperDataService<SalesBlock, SalesBlockWrapper>
    {
        public SalesBlockWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override SalesBlockWrapper GenerateWrapper(SalesBlock model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new SalesBlockWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class BankDetailsWrapperDataService : EntityWrapperDataService<BankDetails, BankDetailsWrapper>
    {
        public BankDetailsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override BankDetailsWrapper GenerateWrapper(BankDetails model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new BankDetailsWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class CompanyWrapperDataService : EntityWrapperDataService<Company, CompanyWrapper>
    {
        public CompanyWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CompanyWrapper GenerateWrapper(Company model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CompanyWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class CompanyFormWrapperDataService : EntityWrapperDataService<CompanyForm, CompanyFormWrapper>
    {
        public CompanyFormWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CompanyFormWrapper GenerateWrapper(CompanyForm model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CompanyFormWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class DocumentsRegistrationDetailsWrapperDataService : EntityWrapperDataService<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>
    {
        public DocumentsRegistrationDetailsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override DocumentsRegistrationDetailsWrapper GenerateWrapper(DocumentsRegistrationDetails model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new DocumentsRegistrationDetailsWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class EmployeesPositionWrapperDataService : EntityWrapperDataService<EmployeesPosition, EmployeesPositionWrapper>
    {
        public EmployeesPositionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override EmployeesPositionWrapper GenerateWrapper(EmployeesPosition model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new EmployeesPositionWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class FacilityTypeWrapperDataService : EntityWrapperDataService<FacilityType, FacilityTypeWrapper>
    {
        public FacilityTypeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override FacilityTypeWrapper GenerateWrapper(FacilityType model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new FacilityTypeWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ActivityFieldWrapperDataService : EntityWrapperDataService<ActivityField, ActivityFieldWrapper>
    {
        public ActivityFieldWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ActivityFieldWrapper GenerateWrapper(ActivityField model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ActivityFieldWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ContractWrapperDataService : EntityWrapperDataService<Contract, ContractWrapper>
    {
        public ContractWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ContractWrapper GenerateWrapper(Contract model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ContractWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class MeasureWrapperDataService : EntityWrapperDataService<Measure, MeasureWrapper>
    {
        public MeasureWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override MeasureWrapper GenerateWrapper(Measure model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new MeasureWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ParameterWrapperDataService : EntityWrapperDataService<Parameter, ParameterWrapper>
    {
        public ParameterWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ParameterWrapper GenerateWrapper(Parameter model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ParameterWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ParameterGroupWrapperDataService : EntityWrapperDataService<ParameterGroup, ParameterGroupWrapper>
    {
        public ParameterGroupWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ParameterGroupWrapper GenerateWrapper(ParameterGroup model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ParameterGroupWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ProductRelationWrapperDataService : EntityWrapperDataService<ProductRelation, ProductRelationWrapper>
    {
        public ProductRelationWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProductRelationWrapper GenerateWrapper(ProductRelation model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ProductRelationWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class PersonWrapperDataService : EntityWrapperDataService<Person, PersonWrapper>
    {
        public PersonWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PersonWrapper GenerateWrapper(Person model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new PersonWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class PaymentPlannedListWrapperDataService : EntityWrapperDataService<PaymentPlannedList, PaymentPlannedListWrapper>
    {
        public PaymentPlannedListWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentPlannedListWrapper GenerateWrapper(PaymentPlannedList model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new PaymentPlannedListWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class PaymentPlannedWrapperDataService : EntityWrapperDataService<PaymentPlanned, PaymentPlannedWrapper>
    {
        public PaymentPlannedWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentPlannedWrapper GenerateWrapper(PaymentPlanned model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new PaymentPlannedWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class PaymentActualWrapperDataService : EntityWrapperDataService<PaymentActual, PaymentActualWrapper>
    {
        public PaymentActualWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentActualWrapper GenerateWrapper(PaymentActual model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new PaymentActualWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ParameterRelationWrapperDataService : EntityWrapperDataService<ParameterRelation, ParameterRelationWrapper>
    {
        public ParameterRelationWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ParameterRelationWrapper GenerateWrapper(ParameterRelation model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ParameterRelationWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class SalesUnitWrapperDataService : EntityWrapperDataService<SalesUnit, SalesUnitWrapper>
    {
        public SalesUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override SalesUnitWrapper GenerateWrapper(SalesUnit model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new SalesUnitWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TestFriendAddressWrapperDataService : EntityWrapperDataService<TestFriendAddress, TestFriendAddressWrapper>
    {
        public TestFriendAddressWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendAddressWrapper GenerateWrapper(TestFriendAddress model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TestFriendAddressWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TestFriendWrapperDataService : EntityWrapperDataService<TestFriend, TestFriendWrapper>
    {
        public TestFriendWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendWrapper GenerateWrapper(TestFriend model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TestFriendWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TestFriendEmailWrapperDataService : EntityWrapperDataService<TestFriendEmail, TestFriendEmailWrapper>
    {
        public TestFriendEmailWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendEmailWrapper GenerateWrapper(TestFriendEmail model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TestFriendEmailWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TestFriendGroupWrapperDataService : EntityWrapperDataService<TestFriendGroup, TestFriendGroupWrapper>
    {
        public TestFriendGroupWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendGroupWrapper GenerateWrapper(TestFriendGroup model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TestFriendGroupWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class DocumentWrapperDataService : EntityWrapperDataService<Document, DocumentWrapper>
    {
        public DocumentWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override DocumentWrapper GenerateWrapper(Document model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new DocumentWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TestEntityWrapperDataService : EntityWrapperDataService<TestEntity, TestEntityWrapper>
    {
        public TestEntityWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestEntityWrapper GenerateWrapper(TestEntity model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TestEntityWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TestHusbandWrapperDataService : EntityWrapperDataService<TestHusband, TestHusbandWrapper>
    {
        public TestHusbandWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestHusbandWrapper GenerateWrapper(TestHusband model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TestHusbandWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TestWifeWrapperDataService : EntityWrapperDataService<TestWife, TestWifeWrapper>
    {
        public TestWifeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestWifeWrapper GenerateWrapper(TestWife model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TestWifeWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TestChildWrapperDataService : EntityWrapperDataService<TestChild, TestChildWrapper>
    {
        public TestChildWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestChildWrapper GenerateWrapper(TestChild model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TestChildWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class CostOnDateWrapperDataService : EntityWrapperDataService<CostOnDate, CostOnDateWrapper>
    {
        public CostOnDateWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CostOnDateWrapper GenerateWrapper(CostOnDate model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new CostOnDateWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ProductWrapperDataService : EntityWrapperDataService<Product, ProductWrapper>
    {
        public ProductWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProductWrapper GenerateWrapper(Product model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ProductWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class OfferWrapperDataService : EntityWrapperDataService<Offer, OfferWrapper>
    {
        public OfferWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override OfferWrapper GenerateWrapper(Offer model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new OfferWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class EmployeeWrapperDataService : EntityWrapperDataService<Employee, EmployeeWrapper>
    {
        public EmployeeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override EmployeeWrapper GenerateWrapper(Employee model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new EmployeeWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class OrderWrapperDataService : EntityWrapperDataService<Order, OrderWrapper>
    {
        public OrderWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override OrderWrapper GenerateWrapper(Order model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new OrderWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class PaymentConditionWrapperDataService : EntityWrapperDataService<PaymentCondition, PaymentConditionWrapper>
    {
        public PaymentConditionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentConditionWrapper GenerateWrapper(PaymentCondition model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new PaymentConditionWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class PaymentDocumentWrapperDataService : EntityWrapperDataService<PaymentDocument, PaymentDocumentWrapper>
    {
        public PaymentDocumentWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentDocumentWrapper GenerateWrapper(PaymentDocument model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new PaymentDocumentWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class FacilityWrapperDataService : EntityWrapperDataService<Facility, FacilityWrapper>
    {
        public FacilityWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override FacilityWrapper GenerateWrapper(Facility model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new FacilityWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class ProjectWrapperDataService : EntityWrapperDataService<Project, ProjectWrapper>
    {
        public ProjectWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProjectWrapper GenerateWrapper(Project model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new ProjectWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class UserRoleWrapperDataService : EntityWrapperDataService<UserRole, UserRoleWrapper>
    {
        public UserRoleWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override UserRoleWrapper GenerateWrapper(UserRole model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new UserRoleWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class SpecificationWrapperDataService : EntityWrapperDataService<Specification, SpecificationWrapper>
    {
        public SpecificationWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override SpecificationWrapper GenerateWrapper(Specification model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new SpecificationWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TenderWrapperDataService : EntityWrapperDataService<Tender, TenderWrapper>
    {
        public TenderWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TenderWrapper GenerateWrapper(Tender model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TenderWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class TenderTypeWrapperDataService : EntityWrapperDataService<TenderType, TenderTypeWrapper>
    {
        public TenderTypeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TenderTypeWrapper GenerateWrapper(TenderType model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new TenderTypeWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


    public partial class UserWrapperDataService : EntityWrapperDataService<User, UserWrapper>
    {
        public UserWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override UserWrapper GenerateWrapper(User model)
        {
            if (ExistsWrappers.Any(x => x.Model.Id == model.Id))
                return ExistsWrappers.Single(x => x.Model.Id == model.Id);

            var wrapper = new UserWrapper(model);
            ExistsWrappers.Add(wrapper);
            return wrapper;
        }
    }


}
