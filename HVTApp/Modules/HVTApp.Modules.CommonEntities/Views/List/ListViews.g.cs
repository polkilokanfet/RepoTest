using HVTApp.Infrastructure;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;
using HVTApp.UI.ViewModels;
using System.Windows;

namespace HVTApp.UI.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class AddressListView : ViewBase
    {
        public AddressListView(IRegionManager regionManager, IEventAggregator eventAggregator, AddressListViewModel AddressListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = AddressListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (AddressListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CountryListView : ViewBase
    {
        public CountryListView(IRegionManager regionManager, IEventAggregator eventAggregator, CountryListViewModel CountryListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CountryListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (CountryListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class DistrictListView : ViewBase
    {
        public DistrictListView(IRegionManager regionManager, IEventAggregator eventAggregator, DistrictListViewModel DistrictListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DistrictListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (DistrictListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class LocalityListView : ViewBase
    {
        public LocalityListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityListViewModel LocalityListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (LocalityListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class LocalityTypeListView : ViewBase
    {
        public LocalityTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityTypeListViewModel LocalityTypeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityTypeListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (LocalityTypeListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class RegionListView : ViewBase
    {
        public RegionListView(IRegionManager regionManager, IEventAggregator eventAggregator, RegionListViewModel RegionListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = RegionListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (RegionListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class AdditionalSalesUnitsListView : ViewBase
    {
        public AdditionalSalesUnitsListView(IRegionManager regionManager, IEventAggregator eventAggregator, AdditionalSalesUnitsListViewModel AdditionalSalesUnitsListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = AdditionalSalesUnitsListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (AdditionalSalesUnitsListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class BankDetailsListView : ViewBase
    {
        public BankDetailsListView(IRegionManager regionManager, IEventAggregator eventAggregator, BankDetailsListViewModel BankDetailsListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = BankDetailsListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (BankDetailsListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CompanyListView : ViewBase
    {
        public CompanyListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyListViewModel CompanyListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (CompanyListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CompanyFormListView : ViewBase
    {
        public CompanyFormListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyFormListViewModel CompanyFormListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyFormListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (CompanyFormListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class DocumentsRegistrationDetailsListView : ViewBase
    {
        public DocumentsRegistrationDetailsListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentsRegistrationDetailsListViewModel DocumentsRegistrationDetailsListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentsRegistrationDetailsListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (DocumentsRegistrationDetailsListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class EmployeesPositionListView : ViewBase
    {
        public EmployeesPositionListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeesPositionListViewModel EmployeesPositionListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeesPositionListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (EmployeesPositionListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class FacilityTypeListView : ViewBase
    {
        public FacilityTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityTypeListViewModel FacilityTypeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityTypeListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (FacilityTypeListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ActivityFieldListView : ViewBase
    {
        public ActivityFieldListView(IRegionManager regionManager, IEventAggregator eventAggregator, ActivityFieldListViewModel ActivityFieldListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ActivityFieldListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ActivityFieldListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ContractListView : ViewBase
    {
        public ContractListView(IRegionManager regionManager, IEventAggregator eventAggregator, ContractListViewModel ContractListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ContractListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ContractListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class MeasureListView : ViewBase
    {
        public MeasureListView(IRegionManager regionManager, IEventAggregator eventAggregator, MeasureListViewModel MeasureListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = MeasureListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (MeasureListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterListView : ViewBase
    {
        public ParameterListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterListViewModel ParameterListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ParameterListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterGroupListView : ViewBase
    {
        public ParameterGroupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterGroupListViewModel ParameterGroupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterGroupListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ParameterGroupListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductRelationListView : ViewBase
    {
        public ProductRelationListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductRelationListViewModel ProductRelationListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductRelationListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ProductRelationListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class StandartPaymentConditionsListView : ViewBase
    {
        public StandartPaymentConditionsListView(IRegionManager regionManager, IEventAggregator eventAggregator, StandartPaymentConditionsListViewModel StandartPaymentConditionsListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = StandartPaymentConditionsListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (StandartPaymentConditionsListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PersonListView : ViewBase
    {
        public PersonListView(IRegionManager regionManager, IEventAggregator eventAggregator, PersonListViewModel PersonListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PersonListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (PersonListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentPlannedListView : ViewBase
    {
        public PaymentPlannedListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedListViewModel PaymentPlannedListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentPlannedListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (PaymentPlannedListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentActualListView : ViewBase
    {
        public PaymentActualListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentActualListViewModel PaymentActualListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentActualListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (PaymentActualListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterRelationListView : ViewBase
    {
        public ParameterRelationListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterRelationListViewModel ParameterRelationListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterRelationListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ParameterRelationListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectUnitListView : ViewBase
    {
        public ProjectUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectUnitListViewModel ProjectUnitListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProjectUnitListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ProjectUnitListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ShipmentUnitListView : ViewBase
    {
        public ShipmentUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, ShipmentUnitListViewModel ShipmentUnitListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ShipmentUnitListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ShipmentUnitListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductionUnitListView : ViewBase
    {
        public ProductionUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductionUnitListViewModel ProductionUnitListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductionUnitListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ProductionUnitListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class SalesUnitListView : ViewBase
    {
        public SalesUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesUnitListViewModel SalesUnitListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SalesUnitListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (SalesUnitListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendAddressListView : ViewBase
    {
        public TestFriendAddressListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendAddressListViewModel TestFriendAddressListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendAddressListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TestFriendAddressListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendListView : ViewBase
    {
        public TestFriendListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendListViewModel TestFriendListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TestFriendListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendEmailListView : ViewBase
    {
        public TestFriendEmailListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendEmailListViewModel TestFriendEmailListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendEmailListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TestFriendEmailListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendGroupListView : ViewBase
    {
        public TestFriendGroupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendGroupListViewModel TestFriendGroupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendGroupListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TestFriendGroupListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class DocumentListView : ViewBase
    {
        public DocumentListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentListViewModel DocumentListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (DocumentListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestEntityListView : ViewBase
    {
        public TestEntityListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestEntityListViewModel TestEntityListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestEntityListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TestEntityListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestHusbandListView : ViewBase
    {
        public TestHusbandListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestHusbandListViewModel TestHusbandListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestHusbandListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TestHusbandListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestWifeListView : ViewBase
    {
        public TestWifeListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestWifeListViewModel TestWifeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestWifeListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TestWifeListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestChildListView : ViewBase
    {
        public TestChildListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestChildListViewModel TestChildListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestChildListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TestChildListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CostOnDateListView : ViewBase
    {
        public CostOnDateListView(IRegionManager regionManager, IEventAggregator eventAggregator, CostOnDateListViewModel CostOnDateListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CostOnDateListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (CostOnDateListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CostListView : ViewBase
    {
        public CostListView(IRegionManager regionManager, IEventAggregator eventAggregator, CostListViewModel CostListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CostListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (CostListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CurrencyListView : ViewBase
    {
        public CurrencyListView(IRegionManager regionManager, IEventAggregator eventAggregator, CurrencyListViewModel CurrencyListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CurrencyListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (CurrencyListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ExchangeCurrencyRateListView : ViewBase
    {
        public ExchangeCurrencyRateListView(IRegionManager regionManager, IEventAggregator eventAggregator, ExchangeCurrencyRateListViewModel ExchangeCurrencyRateListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ExchangeCurrencyRateListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ExchangeCurrencyRateListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductListView : ViewBase
    {
        public ProductListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductListViewModel ProductListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ProductListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class OfferListView : ViewBase
    {
        public OfferListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferListViewModel OfferListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (OfferListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class EmployeeListView : ViewBase
    {
        public EmployeeListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeeListViewModel EmployeeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeeListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (EmployeeListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class OrderListView : ViewBase
    {
        public OrderListView(IRegionManager regionManager, IEventAggregator eventAggregator, OrderListViewModel OrderListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OrderListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (OrderListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentConditionListView : ViewBase
    {
        public PaymentConditionListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionListViewModel PaymentConditionListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentConditionListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (PaymentConditionListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentDocumentListView : ViewBase
    {
        public PaymentDocumentListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentDocumentListViewModel PaymentDocumentListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentDocumentListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (PaymentDocumentListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class FacilityListView : ViewBase
    {
        public FacilityListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityListViewModel FacilityListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (FacilityListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectListView : ViewBase
    {
        public ProjectListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectListViewModel ProjectListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProjectListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ProjectListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class UserRoleListView : ViewBase
    {
        public UserRoleListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserRoleListViewModel UserRoleListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserRoleListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (UserRoleListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class SpecificationListView : ViewBase
    {
        public SpecificationListView(IRegionManager regionManager, IEventAggregator eventAggregator, SpecificationListViewModel SpecificationListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SpecificationListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (SpecificationListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TenderListView : ViewBase
    {
        public TenderListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderListViewModel TenderListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TenderListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TenderTypeListView : ViewBase
    {
        public TenderTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderTypeListViewModel TenderTypeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderTypeListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (TenderTypeListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class UserListView : ViewBase
    {
        public UserListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserListViewModel UserListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (UserListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class OfferUnitListView : ViewBase
    {
        public OfferUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferUnitListViewModel OfferUnitListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferUnitListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (OfferUnitListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductBlockListView : ViewBase
    {
        public ProductBlockListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockListViewModel ProductBlockListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductBlockListViewModel;
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			var viewModel = (ProductBlockListViewModel) DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }

}
