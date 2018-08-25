using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
using System.Windows;
using Prism.Events;
using Prism.Regions;
using HVTApp.UI.Tabs;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Views
{
    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("CommonOption")]
    public partial class CommonOptionLookupListView : ViewBase
    {
        public CommonOptionLookupListView()
        {
            InitializeComponent();
        }

        public CommonOptionLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CommonOptionListViewModel CommonOptionListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Адреса")]
    public partial class AddressLookupListView : ViewBase
    {
        public AddressLookupListView()
        {
            InitializeComponent();
        }

        public AddressLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, AddressListViewModel AddressListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Country")]
    public partial class CountryLookupListView : ViewBase
    {
        public CountryLookupListView()
        {
            InitializeComponent();
        }

        public CountryLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CountryListViewModel CountryListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("District")]
    public partial class DistrictLookupListView : ViewBase
    {
        public DistrictLookupListView()
        {
            InitializeComponent();
        }

        public DistrictLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DistrictListViewModel DistrictListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Locality")]
    public partial class LocalityLookupListView : ViewBase
    {
        public LocalityLookupListView()
        {
            InitializeComponent();
        }

        public LocalityLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityListViewModel LocalityListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("LocalityType")]
    public partial class LocalityTypeLookupListView : ViewBase
    {
        public LocalityTypeLookupListView()
        {
            InitializeComponent();
        }

        public LocalityTypeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityTypeListViewModel LocalityTypeListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Region")]
    public partial class RegionLookupListView : ViewBase
    {
        public RegionLookupListView()
        {
            InitializeComponent();
        }

        public RegionLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, RegionListViewModel RegionListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("CalculatePriceTask")]
    public partial class CalculatePriceTaskLookupListView : ViewBase
    {
        public CalculatePriceTaskLookupListView()
        {
            InitializeComponent();
        }

        public CalculatePriceTaskLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CalculatePriceTaskListViewModel CalculatePriceTaskListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Sum")]
    public partial class SumLookupListView : ViewBase
    {
        public SumLookupListView()
        {
            InitializeComponent();
        }

        public SumLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SumListViewModel SumListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SumListViewModel;
			SumListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SumListViewModel)DataContext).LoadAsync();;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("Курсы обмена валют")]
    public partial class CurrencyExchangeRateLookupListView : ViewBase
    {
        public CurrencyExchangeRateLookupListView()
        {
            InitializeComponent();
        }

        public CurrencyExchangeRateLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CurrencyExchangeRateListViewModel CurrencyExchangeRateListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("DescribeProductBlockTask")]
    public partial class DescribeProductBlockTaskLookupListView : ViewBase
    {
        public DescribeProductBlockTaskLookupListView()
        {
            InitializeComponent();
        }

        public DescribeProductBlockTaskLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DescribeProductBlockTaskListViewModel DescribeProductBlockTaskListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Note")]
    public partial class NoteLookupListView : ViewBase
    {
        public NoteLookupListView()
        {
            InitializeComponent();
        }

        public NoteLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, NoteListViewModel NoteListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Единицы ТКП")]
    public partial class OfferUnitLookupListView : ViewBase
    {
        public OfferUnitLookupListView()
        {
            InitializeComponent();
        }

        public OfferUnitLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferUnitListViewModel OfferUnitListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("PaymentConditionSet")]
    public partial class PaymentConditionSetLookupListView : ViewBase
    {
        public PaymentConditionSetLookupListView()
        {
            InitializeComponent();
        }

        public PaymentConditionSetLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionSetListViewModel PaymentConditionSetListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("ProductBlock")]
    public partial class ProductBlockLookupListView : ViewBase
    {
        public ProductBlockLookupListView()
        {
            InitializeComponent();
        }

        public ProductBlockLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockListViewModel ProductBlockListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("ProductDependent")]
    public partial class ProductDependentLookupListView : ViewBase
    {
        public ProductDependentLookupListView()
        {
            InitializeComponent();
        }

        public ProductDependentLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductDependentListViewModel ProductDependentListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductDependentListViewModel;
			ProductDependentListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductDependentListViewModel)DataContext).LoadAsync();;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductionTask")]
    public partial class ProductionTaskLookupListView : ViewBase
    {
        public ProductionTaskLookupListView()
        {
            InitializeComponent();
        }

        public ProductionTaskLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductionTaskListViewModel ProductionTaskListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductionTaskListViewModel;
			ProductionTaskListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductionTaskListViewModel)DataContext).LoadAsync();;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("SalesBlock")]
    public partial class SalesBlockLookupListView : ViewBase
    {
        public SalesBlockLookupListView()
        {
            InitializeComponent();
        }

        public SalesBlockLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesBlockListViewModel SalesBlockListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("BankDetails")]
    public partial class BankDetailsLookupListView : ViewBase
    {
        public BankDetailsLookupListView()
        {
            InitializeComponent();
        }

        public BankDetailsLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, BankDetailsListViewModel BankDetailsListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Компании")]
    public partial class CompanyLookupListView : ViewBase
    {
        public CompanyLookupListView()
        {
            InitializeComponent();
        }

        public CompanyLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyListViewModel CompanyListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Организационные формы")]
    public partial class CompanyFormLookupListView : ViewBase
    {
        public CompanyFormLookupListView()
        {
            InitializeComponent();
        }

        public CompanyFormLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyFormListViewModel CompanyFormListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("DocumentsRegistrationDetails")]
    public partial class DocumentsRegistrationDetailsLookupListView : ViewBase
    {
        public DocumentsRegistrationDetailsLookupListView()
        {
            InitializeComponent();
        }

        public DocumentsRegistrationDetailsLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentsRegistrationDetailsListViewModel DocumentsRegistrationDetailsListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("EmployeesPosition")]
    public partial class EmployeesPositionLookupListView : ViewBase
    {
        public EmployeesPositionLookupListView()
        {
            InitializeComponent();
        }

        public EmployeesPositionLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeesPositionListViewModel EmployeesPositionListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("FacilityType")]
    public partial class FacilityTypeLookupListView : ViewBase
    {
        public FacilityTypeLookupListView()
        {
            InitializeComponent();
        }

        public FacilityTypeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityTypeListViewModel FacilityTypeListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("ActivityField")]
    public partial class ActivityFieldLookupListView : ViewBase
    {
        public ActivityFieldLookupListView()
        {
            InitializeComponent();
        }

        public ActivityFieldLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ActivityFieldListViewModel ActivityFieldListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Контракты")]
    public partial class ContractLookupListView : ViewBase
    {
        public ContractLookupListView()
        {
            InitializeComponent();
        }

        public ContractLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ContractListViewModel ContractListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Measure")]
    public partial class MeasureLookupListView : ViewBase
    {
        public MeasureLookupListView()
        {
            InitializeComponent();
        }

        public MeasureLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, MeasureListViewModel MeasureListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Parameter")]
    public partial class ParameterLookupListView : ViewBase
    {
        public ParameterLookupListView()
        {
            InitializeComponent();
        }

        public ParameterLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterListViewModel ParameterListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("ParameterGroup")]
    public partial class ParameterGroupLookupListView : ViewBase
    {
        public ParameterGroupLookupListView()
        {
            InitializeComponent();
        }

        public ParameterGroupLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterGroupListViewModel ParameterGroupListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("ProductRelation")]
    public partial class ProductRelationLookupListView : ViewBase
    {
        public ProductRelationLookupListView()
        {
            InitializeComponent();
        }

        public ProductRelationLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductRelationListViewModel ProductRelationListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Персоны")]
    public partial class PersonLookupListView : ViewBase
    {
        public PersonLookupListView()
        {
            InitializeComponent();
        }

        public PersonLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PersonListViewModel PersonListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("PaymentPlannedList")]
    public partial class PaymentPlannedListLookupListView : ViewBase
    {
        public PaymentPlannedListLookupListView()
        {
            InitializeComponent();
        }

        public PaymentPlannedListLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedListListViewModel PaymentPlannedListListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("PaymentPlanned")]
    public partial class PaymentPlannedLookupListView : ViewBase
    {
        public PaymentPlannedLookupListView()
        {
            InitializeComponent();
        }

        public PaymentPlannedLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedListViewModel PaymentPlannedListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("PaymentActual")]
    public partial class PaymentActualLookupListView : ViewBase
    {
        public PaymentActualLookupListView()
        {
            InitializeComponent();
        }

        public PaymentActualLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentActualListViewModel PaymentActualListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("ParameterRelation")]
    public partial class ParameterRelationLookupListView : ViewBase
    {
        public ParameterRelationLookupListView()
        {
            InitializeComponent();
        }

        public ParameterRelationLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterRelationListViewModel ParameterRelationListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Единицы продаж")]
    public partial class SalesUnitLookupListView : ViewBase
    {
        public SalesUnitLookupListView()
        {
            InitializeComponent();
        }

        public SalesUnitLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesUnitListViewModel SalesUnitListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Service")]
    public partial class ServiceLookupListView : ViewBase
    {
        public ServiceLookupListView()
        {
            InitializeComponent();
        }

        public ServiceLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ServiceListViewModel ServiceListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ServiceListViewModel;
			ServiceListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ServiceListViewModel)DataContext).LoadAsync();;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestFriendAddress")]
    public partial class TestFriendAddressLookupListView : ViewBase
    {
        public TestFriendAddressLookupListView()
        {
            InitializeComponent();
        }

        public TestFriendAddressLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendAddressListViewModel TestFriendAddressListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("TestFriend")]
    public partial class TestFriendLookupListView : ViewBase
    {
        public TestFriendLookupListView()
        {
            InitializeComponent();
        }

        public TestFriendLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendListViewModel TestFriendListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("TestFriendEmail")]
    public partial class TestFriendEmailLookupListView : ViewBase
    {
        public TestFriendEmailLookupListView()
        {
            InitializeComponent();
        }

        public TestFriendEmailLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendEmailListViewModel TestFriendEmailListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("TestFriendGroup")]
    public partial class TestFriendGroupLookupListView : ViewBase
    {
        public TestFriendGroupLookupListView()
        {
            InitializeComponent();
        }

        public TestFriendGroupLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendGroupListViewModel TestFriendGroupListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Document")]
    public partial class DocumentLookupListView : ViewBase
    {
        public DocumentLookupListView()
        {
            InitializeComponent();
        }

        public DocumentLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentListViewModel DocumentListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("TestEntity")]
    public partial class TestEntityLookupListView : ViewBase
    {
        public TestEntityLookupListView()
        {
            InitializeComponent();
        }

        public TestEntityLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestEntityListViewModel TestEntityListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("TestHusband")]
    public partial class TestHusbandLookupListView : ViewBase
    {
        public TestHusbandLookupListView()
        {
            InitializeComponent();
        }

        public TestHusbandLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestHusbandListViewModel TestHusbandListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("TestWife")]
    public partial class TestWifeLookupListView : ViewBase
    {
        public TestWifeLookupListView()
        {
            InitializeComponent();
        }

        public TestWifeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestWifeListViewModel TestWifeListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("TestChild")]
    public partial class TestChildLookupListView : ViewBase
    {
        public TestChildLookupListView()
        {
            InitializeComponent();
        }

        public TestChildLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestChildListViewModel TestChildListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("SumOnDate")]
    public partial class SumOnDateLookupListView : ViewBase
    {
        public SumOnDateLookupListView()
        {
            InitializeComponent();
        }

        public SumOnDateLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SumOnDateListViewModel SumOnDateListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SumOnDateListViewModel;
			SumOnDateListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SumOnDateListViewModel)DataContext).LoadAsync();;
        }
    }

    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("Product")]
    public partial class ProductLookupListView : ViewBase
    {
        public ProductLookupListView()
        {
            InitializeComponent();
        }

        public ProductLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductListViewModel ProductListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Предложения")]
    public partial class OfferLookupListView : ViewBase
    {
        public OfferLookupListView()
        {
            InitializeComponent();
        }

        public OfferLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferListViewModel OfferListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Сотрудники")]
    public partial class EmployeeLookupListView : ViewBase
    {
        public EmployeeLookupListView()
        {
            InitializeComponent();
        }

        public EmployeeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeeListViewModel EmployeeListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Order")]
    public partial class OrderLookupListView : ViewBase
    {
        public OrderLookupListView()
        {
            InitializeComponent();
        }

        public OrderLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, OrderListViewModel OrderListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("PaymentCondition")]
    public partial class PaymentConditionLookupListView : ViewBase
    {
        public PaymentConditionLookupListView()
        {
            InitializeComponent();
        }

        public PaymentConditionLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionListViewModel PaymentConditionListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("PaymentDocument")]
    public partial class PaymentDocumentLookupListView : ViewBase
    {
        public PaymentDocumentLookupListView()
        {
            InitializeComponent();
        }

        public PaymentDocumentLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentDocumentListViewModel PaymentDocumentListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Объекты")]
    public partial class FacilityLookupListView : ViewBase
    {
        public FacilityLookupListView()
        {
            InitializeComponent();
        }

        public FacilityLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityListViewModel FacilityListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Проекты")]
    public partial class ProjectLookupListView : ViewBase
    {
        public ProjectLookupListView()
        {
            InitializeComponent();
        }

        public ProjectLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectListViewModel ProjectListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("UserRole")]
    public partial class UserRoleLookupListView : ViewBase
    {
        public UserRoleLookupListView()
        {
            InitializeComponent();
        }

        public UserRoleLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserRoleListViewModel UserRoleListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Specification")]
    public partial class SpecificationLookupListView : ViewBase
    {
        public SpecificationLookupListView()
        {
            InitializeComponent();
        }

        public SpecificationLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SpecificationListViewModel SpecificationListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Тендеры")]
    public partial class TenderLookupListView : ViewBase
    {
        public TenderLookupListView()
        {
            InitializeComponent();
        }

        public TenderLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderListViewModel TenderListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("TenderType")]
    public partial class TenderTypeLookupListView : ViewBase
    {
        public TenderTypeLookupListView()
        {
            InitializeComponent();
        }

        public TenderTypeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderTypeListViewModel TenderTypeListViewModel) : base(regionManager, eventAggregator)
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
	[DesignationPlural("Пользователи")]
    public partial class UserLookupListView : ViewBase
    {
        public UserLookupListView()
        {
            InitializeComponent();
        }

        public UserLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserListViewModel UserListViewModel) : base(regionManager, eventAggregator)
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
