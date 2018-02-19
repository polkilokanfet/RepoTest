using HVTApp.Infrastructure;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;
using HVTApp.UI.ViewModels;
using System.Windows;

namespace HVTApp.UI.Views
{
    public partial class CommonOptionDetailsView : ViewBase
    {
        public CommonOptionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CommonOptionDetailsViewModel CommonOptionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CommonOptionDetailsViewModel;
        }
    }

    public partial class AddressDetailsView : ViewBase
    {
        public AddressDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, AddressDetailsViewModel AddressDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = AddressDetailsViewModel;
        }
    }

    public partial class CountryDetailsView : ViewBase
    {
        public CountryDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CountryDetailsViewModel CountryDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CountryDetailsViewModel;
        }
    }

    public partial class DistrictDetailsView : ViewBase
    {
        public DistrictDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DistrictDetailsViewModel DistrictDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DistrictDetailsViewModel;
        }
    }

    public partial class LocalityDetailsView : ViewBase
    {
        public LocalityDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityDetailsViewModel LocalityDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityDetailsViewModel;
        }
    }

    public partial class LocalityTypeDetailsView : ViewBase
    {
        public LocalityTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityTypeDetailsViewModel LocalityTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityTypeDetailsViewModel;
        }
    }

    public partial class RegionDetailsView : ViewBase
    {
        public RegionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, RegionDetailsViewModel RegionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = RegionDetailsViewModel;
        }
    }

    public partial class CalculatePriceTaskDetailsView : ViewBase
    {
        public CalculatePriceTaskDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CalculatePriceTaskDetailsViewModel CalculatePriceTaskDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CalculatePriceTaskDetailsViewModel;
        }
    }

    public partial class SalesBlockDetailsView : ViewBase
    {
        public SalesBlockDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesBlockDetailsViewModel SalesBlockDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SalesBlockDetailsViewModel;
        }
    }

    public partial class BankDetailsDetailsView : ViewBase
    {
        public BankDetailsDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, BankDetailsDetailsViewModel BankDetailsDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = BankDetailsDetailsViewModel;
        }
    }

    public partial class CompanyDetailsView : ViewBase
    {
        public CompanyDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyDetailsViewModel CompanyDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyDetailsViewModel;
        }
    }

    public partial class CompanyFormDetailsView : ViewBase
    {
        public CompanyFormDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyFormDetailsViewModel CompanyFormDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyFormDetailsViewModel;
        }
    }

    public partial class DocumentsRegistrationDetailsDetailsView : ViewBase
    {
        public DocumentsRegistrationDetailsDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentsRegistrationDetailsDetailsViewModel DocumentsRegistrationDetailsDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentsRegistrationDetailsDetailsViewModel;
        }
    }

    public partial class EmployeesPositionDetailsView : ViewBase
    {
        public EmployeesPositionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeesPositionDetailsViewModel EmployeesPositionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeesPositionDetailsViewModel;
        }
    }

    public partial class FacilityTypeDetailsView : ViewBase
    {
        public FacilityTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityTypeDetailsViewModel FacilityTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityTypeDetailsViewModel;
        }
    }

    public partial class ActivityFieldDetailsView : ViewBase
    {
        public ActivityFieldDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ActivityFieldDetailsViewModel ActivityFieldDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ActivityFieldDetailsViewModel;
        }
    }

    public partial class ContractDetailsView : ViewBase
    {
        public ContractDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ContractDetailsViewModel ContractDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ContractDetailsViewModel;
        }
    }

    public partial class MeasureDetailsView : ViewBase
    {
        public MeasureDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, MeasureDetailsViewModel MeasureDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = MeasureDetailsViewModel;
        }
    }

    public partial class ParameterDetailsView : ViewBase
    {
        public ParameterDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterDetailsViewModel ParameterDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterDetailsViewModel;
        }
    }

    public partial class ParameterGroupDetailsView : ViewBase
    {
        public ParameterGroupDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterGroupDetailsViewModel ParameterGroupDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterGroupDetailsViewModel;
        }
    }

    public partial class ProductRelationDetailsView : ViewBase
    {
        public ProductRelationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductRelationDetailsViewModel ProductRelationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductRelationDetailsViewModel;
        }
    }

    public partial class StandartPaymentConditionsDetailsView : ViewBase
    {
        public StandartPaymentConditionsDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, StandartPaymentConditionsDetailsViewModel StandartPaymentConditionsDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = StandartPaymentConditionsDetailsViewModel;
        }
    }

    public partial class PersonDetailsView : ViewBase
    {
        public PersonDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PersonDetailsViewModel PersonDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PersonDetailsViewModel;
        }
    }

    public partial class PaymentPlannedDetailsView : ViewBase
    {
        public PaymentPlannedDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedDetailsViewModel PaymentPlannedDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentPlannedDetailsViewModel;
        }
    }

    public partial class PaymentActualDetailsView : ViewBase
    {
        public PaymentActualDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentActualDetailsViewModel PaymentActualDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentActualDetailsViewModel;
        }
    }

    public partial class ParameterRelationDetailsView : ViewBase
    {
        public ParameterRelationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterRelationDetailsViewModel ParameterRelationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterRelationDetailsViewModel;
        }
    }

    public partial class SalesUnitDetailsView : ViewBase
    {
        public SalesUnitDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesUnitDetailsViewModel SalesUnitDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SalesUnitDetailsViewModel;
        }
    }

    public partial class TestFriendAddressDetailsView : ViewBase
    {
        public TestFriendAddressDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendAddressDetailsViewModel TestFriendAddressDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendAddressDetailsViewModel;
        }
    }

    public partial class TestFriendDetailsView : ViewBase
    {
        public TestFriendDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendDetailsViewModel TestFriendDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendDetailsViewModel;
        }
    }

    public partial class TestFriendEmailDetailsView : ViewBase
    {
        public TestFriendEmailDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendEmailDetailsViewModel TestFriendEmailDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendEmailDetailsViewModel;
        }
    }

    public partial class TestFriendGroupDetailsView : ViewBase
    {
        public TestFriendGroupDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendGroupDetailsViewModel TestFriendGroupDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendGroupDetailsViewModel;
        }
    }

    public partial class DocumentDetailsView : ViewBase
    {
        public DocumentDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentDetailsViewModel DocumentDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentDetailsViewModel;
        }
    }

    public partial class TestEntityDetailsView : ViewBase
    {
        public TestEntityDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestEntityDetailsViewModel TestEntityDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestEntityDetailsViewModel;
        }
    }

    public partial class TestHusbandDetailsView : ViewBase
    {
        public TestHusbandDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestHusbandDetailsViewModel TestHusbandDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestHusbandDetailsViewModel;
        }
    }

    public partial class TestWifeDetailsView : ViewBase
    {
        public TestWifeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestWifeDetailsViewModel TestWifeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestWifeDetailsViewModel;
        }
    }

    public partial class TestChildDetailsView : ViewBase
    {
        public TestChildDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestChildDetailsViewModel TestChildDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestChildDetailsViewModel;
        }
    }

    public partial class CostOnDateDetailsView : ViewBase
    {
        public CostOnDateDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CostOnDateDetailsViewModel CostOnDateDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CostOnDateDetailsViewModel;
        }
    }

    public partial class CostDetailsView : ViewBase
    {
        public CostDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CostDetailsViewModel CostDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CostDetailsViewModel;
        }
    }

    public partial class CurrencyDetailsView : ViewBase
    {
        public CurrencyDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CurrencyDetailsViewModel CurrencyDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CurrencyDetailsViewModel;
        }
    }

    public partial class ExchangeCurrencyRateDetailsView : ViewBase
    {
        public ExchangeCurrencyRateDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ExchangeCurrencyRateDetailsViewModel ExchangeCurrencyRateDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ExchangeCurrencyRateDetailsViewModel;
        }
    }

    public partial class ProductDetailsView : ViewBase
    {
        public ProductDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductDetailsViewModel ProductDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductDetailsViewModel;
        }
    }

    public partial class OfferDetailsView : ViewBase
    {
        public OfferDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferDetailsViewModel OfferDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferDetailsViewModel;
        }
    }

    public partial class EmployeeDetailsView : ViewBase
    {
        public EmployeeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeeDetailsViewModel EmployeeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeeDetailsViewModel;
        }
    }

    public partial class OrderDetailsView : ViewBase
    {
        public OrderDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, OrderDetailsViewModel OrderDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OrderDetailsViewModel;
        }
    }

    public partial class PaymentConditionDetailsView : ViewBase
    {
        public PaymentConditionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionDetailsViewModel PaymentConditionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentConditionDetailsViewModel;
        }
    }

    public partial class PaymentDocumentDetailsView : ViewBase
    {
        public PaymentDocumentDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentDocumentDetailsViewModel PaymentDocumentDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentDocumentDetailsViewModel;
        }
    }

    public partial class FacilityDetailsView : ViewBase
    {
        public FacilityDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityDetailsViewModel FacilityDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityDetailsViewModel;
        }
    }

    public partial class ProjectDetailsView : ViewBase
    {
        public ProjectDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectDetailsViewModel ProjectDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProjectDetailsViewModel;
        }
    }

    public partial class UserRoleDetailsView : ViewBase
    {
        public UserRoleDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, UserRoleDetailsViewModel UserRoleDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserRoleDetailsViewModel;
        }
    }

    public partial class SpecificationDetailsView : ViewBase
    {
        public SpecificationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SpecificationDetailsViewModel SpecificationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SpecificationDetailsViewModel;
        }
    }

    public partial class TenderDetailsView : ViewBase
    {
        public TenderDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderDetailsViewModel TenderDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderDetailsViewModel;
        }
    }

    public partial class TenderTypeDetailsView : ViewBase
    {
        public TenderTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderTypeDetailsViewModel TenderTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderTypeDetailsViewModel;
        }
    }

    public partial class UserDetailsView : ViewBase
    {
        public UserDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, UserDetailsViewModel UserDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserDetailsViewModel;
        }
    }

    public partial class ProductBlockDetailsView : ViewBase
    {
        public ProductBlockDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockDetailsViewModel ProductBlockDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductBlockDetailsViewModel;
        }
    }

    public partial class PaymentConditionSetDetailsView : ViewBase
    {
        public PaymentConditionSetDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionSetDetailsViewModel PaymentConditionSetDetailsViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentConditionSetDetailsViewModel;
        }
    }

}
