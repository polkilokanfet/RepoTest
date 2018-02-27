using HVTApp.Infrastructure;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;
using HVTApp.UI.ViewModels;
using System.Windows;

namespace HVTApp.UI.Views
{

    [RibbonTab(typeof(TabCRUD))]
    public partial class CommonOptionListView : ViewBase
    {
        public CommonOptionListView(IRegionManager regionManager, IEventAggregator eventAggregator, CommonOptionListViewModel CommonOptionListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CommonOptionListViewModel;
			CommonOptionListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CommonOptionListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class AddressListView : ViewBase
    {
        public AddressListView(IRegionManager regionManager, IEventAggregator eventAggregator, AddressListViewModel AddressListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = AddressListViewModel;
			AddressListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((AddressListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class CountryListView : ViewBase
    {
        public CountryListView(IRegionManager regionManager, IEventAggregator eventAggregator, CountryListViewModel CountryListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CountryListViewModel;
			CountryListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CountryListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class DistrictListView : ViewBase
    {
        public DistrictListView(IRegionManager regionManager, IEventAggregator eventAggregator, DistrictListViewModel DistrictListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DistrictListViewModel;
			DistrictListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DistrictListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class LocalityListView : ViewBase
    {
        public LocalityListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityListViewModel LocalityListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityListViewModel;
			LocalityListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((LocalityListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class LocalityTypeListView : ViewBase
    {
        public LocalityTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityTypeListViewModel LocalityTypeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityTypeListViewModel;
			LocalityTypeListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((LocalityTypeListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class RegionListView : ViewBase
    {
        public RegionListView(IRegionManager regionManager, IEventAggregator eventAggregator, RegionListViewModel RegionListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = RegionListViewModel;
			RegionListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((RegionListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class CalculatePriceTaskListView : ViewBase
    {
        public CalculatePriceTaskListView(IRegionManager regionManager, IEventAggregator eventAggregator, CalculatePriceTaskListViewModel CalculatePriceTaskListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CalculatePriceTaskListViewModel;
			CalculatePriceTaskListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CalculatePriceTaskListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class CostListView : ViewBase
    {
        public CostListView(IRegionManager regionManager, IEventAggregator eventAggregator, CostListViewModel CostListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CostListViewModel;
			CostListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CostListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class CurrencyListView : ViewBase
    {
        public CurrencyListView(IRegionManager regionManager, IEventAggregator eventAggregator, CurrencyListViewModel CurrencyListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CurrencyListViewModel;
			CurrencyListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CurrencyListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class CurrencyExchangeRateListView : ViewBase
    {
        public CurrencyExchangeRateListView(IRegionManager regionManager, IEventAggregator eventAggregator, CurrencyExchangeRateListViewModel CurrencyExchangeRateListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CurrencyExchangeRateListViewModel;
			CurrencyExchangeRateListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CurrencyExchangeRateListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class DescribeProductBlockTaskListView : ViewBase
    {
        public DescribeProductBlockTaskListView(IRegionManager regionManager, IEventAggregator eventAggregator, DescribeProductBlockTaskListViewModel DescribeProductBlockTaskListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DescribeProductBlockTaskListViewModel;
			DescribeProductBlockTaskListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DescribeProductBlockTaskListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class NoteListView : ViewBase
    {
        public NoteListView(IRegionManager regionManager, IEventAggregator eventAggregator, NoteListViewModel NoteListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = NoteListViewModel;
			NoteListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((NoteListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class OfferUnitListView : ViewBase
    {
        public OfferUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferUnitListViewModel OfferUnitListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferUnitListViewModel;
			OfferUnitListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((OfferUnitListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentConditionSetListView : ViewBase
    {
        public PaymentConditionSetListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionSetListViewModel PaymentConditionSetListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentConditionSetListViewModel;
			PaymentConditionSetListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentConditionSetListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductBlockListView : ViewBase
    {
        public ProductBlockListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockListViewModel ProductBlockListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductBlockListViewModel;
			ProductBlockListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductBlockListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class SalesBlockListView : ViewBase
    {
        public SalesBlockListView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesBlockListViewModel SalesBlockListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SalesBlockListViewModel;
			SalesBlockListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SalesBlockListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class BankDetailsListView : ViewBase
    {
        public BankDetailsListView(IRegionManager regionManager, IEventAggregator eventAggregator, BankDetailsListViewModel BankDetailsListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = BankDetailsListViewModel;
			BankDetailsListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((BankDetailsListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class CompanyListView : ViewBase
    {
        public CompanyListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyListViewModel CompanyListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyListViewModel;
			CompanyListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CompanyListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class CompanyFormListView : ViewBase
    {
        public CompanyFormListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyFormListViewModel CompanyFormListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyFormListViewModel;
			CompanyFormListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CompanyFormListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class DocumentsRegistrationDetailsListView : ViewBase
    {
        public DocumentsRegistrationDetailsListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentsRegistrationDetailsListViewModel DocumentsRegistrationDetailsListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentsRegistrationDetailsListViewModel;
			DocumentsRegistrationDetailsListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DocumentsRegistrationDetailsListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class EmployeesPositionListView : ViewBase
    {
        public EmployeesPositionListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeesPositionListViewModel EmployeesPositionListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeesPositionListViewModel;
			EmployeesPositionListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((EmployeesPositionListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class FacilityTypeListView : ViewBase
    {
        public FacilityTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityTypeListViewModel FacilityTypeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityTypeListViewModel;
			FacilityTypeListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((FacilityTypeListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ActivityFieldListView : ViewBase
    {
        public ActivityFieldListView(IRegionManager regionManager, IEventAggregator eventAggregator, ActivityFieldListViewModel ActivityFieldListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ActivityFieldListViewModel;
			ActivityFieldListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ActivityFieldListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ContractListView : ViewBase
    {
        public ContractListView(IRegionManager regionManager, IEventAggregator eventAggregator, ContractListViewModel ContractListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ContractListViewModel;
			ContractListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ContractListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class MeasureListView : ViewBase
    {
        public MeasureListView(IRegionManager regionManager, IEventAggregator eventAggregator, MeasureListViewModel MeasureListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = MeasureListViewModel;
			MeasureListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((MeasureListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterListView : ViewBase
    {
        public ParameterListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterListViewModel ParameterListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterListViewModel;
			ParameterListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ParameterListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterGroupListView : ViewBase
    {
        public ParameterGroupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterGroupListViewModel ParameterGroupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterGroupListViewModel;
			ParameterGroupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ParameterGroupListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductRelationListView : ViewBase
    {
        public ProductRelationListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductRelationListViewModel ProductRelationListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductRelationListViewModel;
			ProductRelationListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductRelationListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class PersonListView : ViewBase
    {
        public PersonListView(IRegionManager regionManager, IEventAggregator eventAggregator, PersonListViewModel PersonListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PersonListViewModel;
			PersonListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PersonListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentPlannedListListView : ViewBase
    {
        public PaymentPlannedListListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedListListViewModel PaymentPlannedListListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentPlannedListListViewModel;
			PaymentPlannedListListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentPlannedListListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentPlannedListView : ViewBase
    {
        public PaymentPlannedListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedListViewModel PaymentPlannedListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentPlannedListViewModel;
			PaymentPlannedListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentPlannedListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentActualListView : ViewBase
    {
        public PaymentActualListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentActualListViewModel PaymentActualListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentActualListViewModel;
			PaymentActualListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentActualListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ParameterRelationListView : ViewBase
    {
        public ParameterRelationListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterRelationListViewModel ParameterRelationListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterRelationListViewModel;
			ParameterRelationListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ParameterRelationListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class SalesUnitListView : ViewBase
    {
        public SalesUnitListView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesUnitListViewModel SalesUnitListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SalesUnitListViewModel;
			SalesUnitListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SalesUnitListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendAddressListView : ViewBase
    {
        public TestFriendAddressListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendAddressListViewModel TestFriendAddressListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendAddressListViewModel;
			TestFriendAddressListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestFriendAddressListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendListView : ViewBase
    {
        public TestFriendListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendListViewModel TestFriendListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendListViewModel;
			TestFriendListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestFriendListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendEmailListView : ViewBase
    {
        public TestFriendEmailListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendEmailListViewModel TestFriendEmailListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendEmailListViewModel;
			TestFriendEmailListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestFriendEmailListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TestFriendGroupListView : ViewBase
    {
        public TestFriendGroupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendGroupListViewModel TestFriendGroupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendGroupListViewModel;
			TestFriendGroupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestFriendGroupListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class DocumentListView : ViewBase
    {
        public DocumentListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentListViewModel DocumentListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentListViewModel;
			DocumentListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DocumentListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TestEntityListView : ViewBase
    {
        public TestEntityListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestEntityListViewModel TestEntityListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestEntityListViewModel;
			TestEntityListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestEntityListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TestHusbandListView : ViewBase
    {
        public TestHusbandListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestHusbandListViewModel TestHusbandListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestHusbandListViewModel;
			TestHusbandListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestHusbandListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TestWifeListView : ViewBase
    {
        public TestWifeListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestWifeListViewModel TestWifeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestWifeListViewModel;
			TestWifeListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestWifeListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TestChildListView : ViewBase
    {
        public TestChildListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestChildListViewModel TestChildListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestChildListViewModel;
			TestChildListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestChildListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class CostOnDateListView : ViewBase
    {
        public CostOnDateListView(IRegionManager regionManager, IEventAggregator eventAggregator, CostOnDateListViewModel CostOnDateListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CostOnDateListViewModel;
			CostOnDateListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CostOnDateListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductListView : ViewBase
    {
        public ProductListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductListViewModel ProductListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductListViewModel;
			ProductListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class OfferListView : ViewBase
    {
        public OfferListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferListViewModel OfferListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferListViewModel;
			OfferListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((OfferListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class EmployeeListView : ViewBase
    {
        public EmployeeListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeeListViewModel EmployeeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeeListViewModel;
			EmployeeListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((EmployeeListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class OrderListView : ViewBase
    {
        public OrderListView(IRegionManager regionManager, IEventAggregator eventAggregator, OrderListViewModel OrderListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OrderListViewModel;
			OrderListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((OrderListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentConditionListView : ViewBase
    {
        public PaymentConditionListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionListViewModel PaymentConditionListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentConditionListViewModel;
			PaymentConditionListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentConditionListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentDocumentListView : ViewBase
    {
        public PaymentDocumentListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentDocumentListViewModel PaymentDocumentListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentDocumentListViewModel;
			PaymentDocumentListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentDocumentListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class FacilityListView : ViewBase
    {
        public FacilityListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityListViewModel FacilityListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityListViewModel;
			FacilityListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((FacilityListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectListView : ViewBase
    {
        public ProjectListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectListViewModel ProjectListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProjectListViewModel;
			ProjectListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProjectListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class UserRoleListView : ViewBase
    {
        public UserRoleListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserRoleListViewModel UserRoleListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserRoleListViewModel;
			UserRoleListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((UserRoleListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class SpecificationListView : ViewBase
    {
        public SpecificationListView(IRegionManager regionManager, IEventAggregator eventAggregator, SpecificationListViewModel SpecificationListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SpecificationListViewModel;
			SpecificationListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SpecificationListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TenderListView : ViewBase
    {
        public TenderListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderListViewModel TenderListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderListViewModel;
			TenderListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TenderListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class TenderTypeListView : ViewBase
    {
        public TenderTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderTypeListViewModel TenderTypeListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderTypeListViewModel;
			TenderTypeListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TenderTypeListViewModel)DataContext).LoadAsync();;
        }
    }


    [RibbonTab(typeof(TabCRUD))]
    public partial class UserListView : ViewBase
    {
        public UserListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserListViewModel UserListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserListViewModel;
			UserListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((UserListViewModel)DataContext).LoadAsync();;
        }
    }


}
