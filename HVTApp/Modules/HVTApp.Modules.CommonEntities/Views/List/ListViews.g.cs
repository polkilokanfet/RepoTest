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
        public AddressListView(IRegionManager regionManager, IEventAggregator eventAggregator, AddressListServiceViewModel AddressListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = AddressListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((AddressListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CountryListView : ViewBase
    {
        public CountryListView(IRegionManager regionManager, IEventAggregator eventAggregator, CountryListServiceViewModel CountryListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CountryListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((CountryListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class DistrictListView : ViewBase
    {
        public DistrictListView(IRegionManager regionManager, IEventAggregator eventAggregator, DistrictListServiceViewModel DistrictListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DistrictListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((DistrictListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class LocalityListView : ViewBase
    {
        public LocalityListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityListServiceViewModel LocalityListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((LocalityListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class LocalityTypeListView : ViewBase
    {
        public LocalityTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityTypeListServiceViewModel LocalityTypeListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityTypeListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((LocalityTypeListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class RegionListView : ViewBase
    {
        public RegionListView(IRegionManager regionManager, IEventAggregator eventAggregator, RegionListServiceViewModel RegionListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = RegionListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((RegionListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class AdditionalSalesUnitsListView : ViewBase
    {
        public AdditionalSalesUnitsListView(IRegionManager regionManager, IEventAggregator eventAggregator, AdditionalSalesUnitsListServiceViewModel AdditionalSalesUnitsListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = AdditionalSalesUnitsListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((AdditionalSalesUnitsListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class BankDetailsListView : ViewBase
    {
        public BankDetailsListView(IRegionManager regionManager, IEventAggregator eventAggregator, BankDetailsListServiceViewModel BankDetailsListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = BankDetailsListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((BankDetailsListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CompanyListView : ViewBase
    {
        public CompanyListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyListServiceViewModel CompanyListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((CompanyListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CompanyFormListView : ViewBase
    {
        public CompanyFormListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyFormListServiceViewModel CompanyFormListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyFormListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((CompanyFormListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class DocumentsRegistrationDetailsListView : ViewBase
    {
        public DocumentsRegistrationDetailsListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentsRegistrationDetailsListServiceViewModel DocumentsRegistrationDetailsListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentsRegistrationDetailsListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((DocumentsRegistrationDetailsListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class EmployeesPositionListView : ViewBase
    {
        public EmployeesPositionListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeesPositionListServiceViewModel EmployeesPositionListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeesPositionListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((EmployeesPositionListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class FacilityTypeListView : ViewBase
    {
        public FacilityTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityTypeListServiceViewModel FacilityTypeListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityTypeListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((FacilityTypeListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ActivityFieldListView : ViewBase
    {
        public ActivityFieldListView(IRegionManager regionManager, IEventAggregator eventAggregator, ActivityFieldListServiceViewModel ActivityFieldListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ActivityFieldListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ActivityFieldListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ContractListView : ViewBase
    {
        public ContractListView(IRegionManager regionManager, IEventAggregator eventAggregator, ContractListServiceViewModel ContractListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ContractListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ContractListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class MeasureListView : ViewBase
    {
        public MeasureListView(IRegionManager regionManager, IEventAggregator eventAggregator, MeasureListServiceViewModel MeasureListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = MeasureListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((MeasureListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterListView : ViewBase
    {
        public ParameterListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterListServiceViewModel ParameterListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ParameterListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterGroupListView : ViewBase
    {
        public ParameterGroupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterGroupListServiceViewModel ParameterGroupListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterGroupListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ParameterGroupListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductRelationListView : ViewBase
    {
        public ProductRelationListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductRelationListServiceViewModel ProductRelationListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductRelationListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ProductRelationListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class StandartPaymentConditionsListView : ViewBase
    {
        public StandartPaymentConditionsListView(IRegionManager regionManager, IEventAggregator eventAggregator, StandartPaymentConditionsListServiceViewModel StandartPaymentConditionsListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = StandartPaymentConditionsListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((StandartPaymentConditionsListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PersonListView : ViewBase
    {
        public PersonListView(IRegionManager regionManager, IEventAggregator eventAggregator, PersonListServiceViewModel PersonListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PersonListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((PersonListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentPlannedListView : ViewBase
    {
        public PaymentPlannedListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedListServiceViewModel PaymentPlannedListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentPlannedListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((PaymentPlannedListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentActualListView : ViewBase
    {
        public PaymentActualListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentActualListServiceViewModel PaymentActualListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentActualListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((PaymentActualListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterRelationListView : ViewBase
    {
        public ParameterRelationListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterRelationListServiceViewModel ParameterRelationListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterRelationListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ParameterRelationListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectUnitListView : ViewBase
    {
        public ProjectUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectUnitListServiceViewModel ProjectUnitListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProjectUnitListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ProjectUnitListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TenderUnitListView : ViewBase
    {
        public TenderUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderUnitListServiceViewModel TenderUnitListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderUnitListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TenderUnitListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ShipmentUnitListView : ViewBase
    {
        public ShipmentUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, ShipmentUnitListServiceViewModel ShipmentUnitListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ShipmentUnitListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ShipmentUnitListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductionUnitListView : ViewBase
    {
        public ProductionUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductionUnitListServiceViewModel ProductionUnitListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductionUnitListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ProductionUnitListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class SalesUnitListView : ViewBase
    {
        public SalesUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesUnitListServiceViewModel SalesUnitListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SalesUnitListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((SalesUnitListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendAddressListView : ViewBase
    {
        public TestFriendAddressListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendAddressListServiceViewModel TestFriendAddressListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendAddressListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TestFriendAddressListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendListView : ViewBase
    {
        public TestFriendListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendListServiceViewModel TestFriendListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TestFriendListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendEmailListView : ViewBase
    {
        public TestFriendEmailListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendEmailListServiceViewModel TestFriendEmailListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendEmailListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TestFriendEmailListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendGroupListView : ViewBase
    {
        public TestFriendGroupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendGroupListServiceViewModel TestFriendGroupListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendGroupListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TestFriendGroupListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class DocumentListView : ViewBase
    {
        public DocumentListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentListServiceViewModel DocumentListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((DocumentListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestEntityListView : ViewBase
    {
        public TestEntityListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestEntityListServiceViewModel TestEntityListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestEntityListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TestEntityListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestHusbandListView : ViewBase
    {
        public TestHusbandListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestHusbandListServiceViewModel TestHusbandListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestHusbandListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TestHusbandListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestWifeListView : ViewBase
    {
        public TestWifeListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestWifeListServiceViewModel TestWifeListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestWifeListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TestWifeListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TestChildListView : ViewBase
    {
        public TestChildListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestChildListServiceViewModel TestChildListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestChildListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TestChildListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CostOnDateListView : ViewBase
    {
        public CostOnDateListView(IRegionManager regionManager, IEventAggregator eventAggregator, CostOnDateListServiceViewModel CostOnDateListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CostOnDateListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((CostOnDateListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CostListView : ViewBase
    {
        public CostListView(IRegionManager regionManager, IEventAggregator eventAggregator, CostListServiceViewModel CostListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CostListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((CostListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class CurrencyListView : ViewBase
    {
        public CurrencyListView(IRegionManager regionManager, IEventAggregator eventAggregator, CurrencyListServiceViewModel CurrencyListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CurrencyListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((CurrencyListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ExchangeCurrencyRateListView : ViewBase
    {
        public ExchangeCurrencyRateListView(IRegionManager regionManager, IEventAggregator eventAggregator, ExchangeCurrencyRateListServiceViewModel ExchangeCurrencyRateListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ExchangeCurrencyRateListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ExchangeCurrencyRateListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductListView : ViewBase
    {
        public ProductListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductListServiceViewModel ProductListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ProductListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class OfferListView : ViewBase
    {
        public OfferListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferListServiceViewModel OfferListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((OfferListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class EmployeeListView : ViewBase
    {
        public EmployeeListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeeListServiceViewModel EmployeeListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeeListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((EmployeeListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class OrderListView : ViewBase
    {
        public OrderListView(IRegionManager regionManager, IEventAggregator eventAggregator, OrderListServiceViewModel OrderListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OrderListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((OrderListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentConditionListView : ViewBase
    {
        public PaymentConditionListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionListServiceViewModel PaymentConditionListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentConditionListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((PaymentConditionListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentDocumentListView : ViewBase
    {
        public PaymentDocumentListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentDocumentListServiceViewModel PaymentDocumentListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentDocumentListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((PaymentDocumentListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class FacilityListView : ViewBase
    {
        public FacilityListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityListServiceViewModel FacilityListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((FacilityListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectListView : ViewBase
    {
        public ProjectListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectListServiceViewModel ProjectListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProjectListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ProjectListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class UserRoleListView : ViewBase
    {
        public UserRoleListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserRoleListServiceViewModel UserRoleListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserRoleListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((UserRoleListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class SpecificationListView : ViewBase
    {
        public SpecificationListView(IRegionManager regionManager, IEventAggregator eventAggregator, SpecificationListServiceViewModel SpecificationListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SpecificationListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((SpecificationListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TenderListView : ViewBase
    {
        public TenderListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderListServiceViewModel TenderListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TenderListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class TenderTypeListView : ViewBase
    {
        public TenderTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderTypeListServiceViewModel TenderTypeListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderTypeListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((TenderTypeListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class UserListView : ViewBase
    {
        public UserListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserListServiceViewModel UserListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((UserListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
    public partial class OfferUnitListView : ViewBase
    {
        public OfferUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferUnitListServiceViewModel OfferUnitListServiceViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferUnitListServiceViewModel;
            Loaded += OnLoaded;
        }
		        
		private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((OfferUnitListServiceViewModel)DataContext).LoadAsync();
            _loaded = true;
        }
    }

}
