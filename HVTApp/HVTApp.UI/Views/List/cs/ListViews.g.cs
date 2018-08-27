using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
using System.Windows;
using Prism.Events;
using Prism.Regions;
using HVTApp.UI.Tabs;
using HVTApp.UI.ViewModels;
using HVTApp.Model.POCOs;

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

		#region VisibilityProps


        public System.Windows.Visibility OurCompanyIdVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.OurCompanyId)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.OurCompanyId)].Visibility = value; }
        }


        public System.Windows.Visibility CalculationPriceTermVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.CalculationPriceTerm)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.CalculationPriceTerm)].Visibility = value; }
        }


        public System.Windows.Visibility StandartTermFromStartToEndProductionVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromStartToEndProduction)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromStartToEndProduction)].Visibility = value; }
        }


        public System.Windows.Visibility StandartTermFromPickToEndProductionVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromPickToEndProduction)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromPickToEndProduction)].Visibility = value; }
        }


        public System.Windows.Visibility StandartPaymentsConditionSetIdVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.StandartPaymentsConditionSetId)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CommonOption.StandartPaymentsConditionSetId)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DescriptionVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Address.Description)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Address.Description)].Visibility = value; }
        }


        public System.Windows.Visibility LocalityVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Address.Locality)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Address.Locality)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return CountryLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Country.Name)].Visibility; }
            set { CountryLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Country.Name)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.District.Name)].Visibility; }
            set { DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.District.Name)].Visibility = value; }
        }


        public System.Windows.Visibility CountryVisibility
        {
            get { return DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.District.Country)].Visibility; }
            set { DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.District.Country)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.Name)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.Name)].Visibility = value; }
        }


        public System.Windows.Visibility IsCountryCapitalVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.IsCountryCapital)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.IsCountryCapital)].Visibility = value; }
        }


        public System.Windows.Visibility IsDistrictCapitalVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.IsDistrictCapital)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.IsDistrictCapital)].Visibility = value; }
        }


        public System.Windows.Visibility IsRegionCapitalVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.IsRegionCapital)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.IsRegionCapital)].Visibility = value; }
        }


        public System.Windows.Visibility StandartDeliveryPeriodVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.StandartDeliveryPeriod)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.StandartDeliveryPeriod)].Visibility = value; }
        }


        public System.Windows.Visibility DistanceToEkbVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.DistanceToEkb)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.DistanceToEkb)].Visibility = value; }
        }


        public System.Windows.Visibility LocalityTypeVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.LocalityType)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.LocalityType)].Visibility = value; }
        }


        public System.Windows.Visibility RegionVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.Region)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Locality.Region)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.LocalityType.FullName)].Visibility; }
            set { LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.LocalityType.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.LocalityType.ShortName)].Visibility; }
            set { LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.LocalityType.ShortName)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Region.Name)].Visibility; }
            set { RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Region.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DistrictVisibility
        {
            get { return RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Region.District)].Visibility; }
            set { RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Region.District)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CalculatePriceTask.Date)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CalculatePriceTask.Date)].Visibility = value; }
        }


        public System.Windows.Visibility IsActualVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CalculatePriceTask.IsActual)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CalculatePriceTask.IsActual)].Visibility = value; }
        }


        public System.Windows.Visibility ProductBlockVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CalculatePriceTask.ProductBlock)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CalculatePriceTask.ProductBlock)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility TypeVisibility
        {
            get { return SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Sum.Type)].Visibility; }
            set { SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Sum.Type)].Visibility = value; }
        }


        public System.Windows.Visibility CurrencyVisibility
        {
            get { return SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Sum.Currency)].Visibility; }
            set { SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Sum.Currency)].Visibility = value; }
        }


        public System.Windows.Visibility ValueVisibility
        {
            get { return SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Sum.Value)].Visibility; }
            set { SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Sum.Value)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.Date)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.Date)].Visibility = value; }
        }


        public System.Windows.Visibility FirstCurrencyVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.FirstCurrency)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.FirstCurrency)].Visibility = value; }
        }


        public System.Windows.Visibility SecondCurrencyVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.SecondCurrency)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.SecondCurrency)].Visibility = value; }
        }


        public System.Windows.Visibility ExchangeRateVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.ExchangeRate)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.ExchangeRate)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility ProductBlockVisibility
        {
            get { return DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.ProductBlock)].Visibility; }
            set { DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.ProductBlock)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.Product)].Visibility; }
            set { DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.Product)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Note.Date)].Visibility; }
            set { NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Note.Date)].Visibility = value; }
        }


        public System.Windows.Visibility TextVisibility
        {
            get { return NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Note.Text)].Visibility; }
            set { NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Note.Text)].Visibility = value; }
        }


        public System.Windows.Visibility IsImportantVisibility
        {
            get { return NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Note.IsImportant)].Visibility; }
            set { NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Note.IsImportant)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility CostVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.Cost)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.Cost)].Visibility = value; }
        }


        public System.Windows.Visibility ProductionTermVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.ProductionTerm)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.ProductionTerm)].Visibility = value; }
        }


        public System.Windows.Visibility OfferVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.Offer)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.Offer)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.Product)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.Product)].Visibility = value; }
        }


        public System.Windows.Visibility FacilityVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.Facility)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.Facility)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentConditionSetVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.PaymentConditionSet)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.OfferUnit.PaymentConditionSet)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductBlock.Name)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductBlock.Name)].Visibility = value; }
        }


        public System.Windows.Visibility StructureCostNumberVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductBlock.StructureCostNumber)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductBlock.StructureCostNumber)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility AmountVisibility
        {
            get { return ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductDependent.Amount)].Visibility; }
            set { ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductDependent.Amount)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductDependent.Product)].Visibility; }
            set { ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductDependent.Product)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DateTaskVisibility
        {
            get { return ProductionTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductionTask.DateTask)].Visibility; }
            set { ProductionTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductionTask.DateTask)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility BankNameVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.BankDetails.BankName)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.BankDetails.BankName)].Visibility = value; }
        }


        public System.Windows.Visibility BankIdentificationCodeVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.BankDetails.BankIdentificationCode)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.BankDetails.BankIdentificationCode)].Visibility = value; }
        }


        public System.Windows.Visibility CorrespondentAccountVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.BankDetails.CorrespondentAccount)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.BankDetails.CorrespondentAccount)].Visibility = value; }
        }


        public System.Windows.Visibility CheckingAccountVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.BankDetails.CheckingAccount)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.BankDetails.CheckingAccount)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.FullName)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.ShortName)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.ShortName)].Visibility = value; }
        }


        public System.Windows.Visibility InnVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.Inn)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.Inn)].Visibility = value; }
        }


        public System.Windows.Visibility KppVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.Kpp)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.Kpp)].Visibility = value; }
        }


        public System.Windows.Visibility FormVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.Form)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.Form)].Visibility = value; }
        }


        public System.Windows.Visibility ParentCompanyVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.ParentCompany)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.ParentCompany)].Visibility = value; }
        }


        public System.Windows.Visibility AddressLegalVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.AddressLegal)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.AddressLegal)].Visibility = value; }
        }


        public System.Windows.Visibility AddressPostVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.AddressPost)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Company.AddressPost)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CompanyForm.FullName)].Visibility; }
            set { CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CompanyForm.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CompanyForm.ShortName)].Visibility; }
            set { CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.CompanyForm.ShortName)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility RegistrationNumberVisibility
        {
            get { return DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationNumber)].Visibility; }
            set { DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationNumber)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDateVisibility
        {
            get { return DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationDate)].Visibility; }
            set { DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationDate)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return EmployeesPositionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.EmployeesPosition.Name)].Visibility; }
            set { EmployeesPositionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.EmployeesPosition.Name)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.FacilityType.FullName)].Visibility; }
            set { FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.FacilityType.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.FacilityType.ShortName)].Visibility; }
            set { FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.FacilityType.ShortName)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ActivityField.Name)].Visibility; }
            set { ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ActivityField.Name)].Visibility = value; }
        }


        public System.Windows.Visibility ActivityFieldEnumVisibility
        {
            get { return ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ActivityField.ActivityFieldEnum)].Visibility; }
            set { ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ActivityField.ActivityFieldEnum)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Contract.Number)].Visibility; }
            set { ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Contract.Number)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Contract.Date)].Visibility; }
            set { ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Contract.Date)].Visibility = value; }
        }


        public System.Windows.Visibility ContragentVisibility
        {
            get { return ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Contract.Contragent)].Visibility; }
            set { ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Contract.Contragent)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Measure.FullName)].Visibility; }
            set { MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Measure.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Measure.ShortName)].Visibility; }
            set { MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Measure.ShortName)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility ValueVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Parameter.Value)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Parameter.Value)].Visibility = value; }
        }


        public System.Windows.Visibility IsOriginVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Parameter.IsOrigin)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Parameter.IsOrigin)].Visibility = value; }
        }


        public System.Windows.Visibility ParameterGroupVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Parameter.ParameterGroup)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Parameter.ParameterGroup)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ParameterGroup.Name)].Visibility; }
            set { ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ParameterGroup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility MeasureVisibility
        {
            get { return ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ParameterGroup.Measure)].Visibility; }
            set { ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ParameterGroup.Measure)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility ChildProductsAmountVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductRelation.ChildProductsAmount)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductRelation.ChildProductsAmount)].Visibility = value; }
        }


        public System.Windows.Visibility IsUniqueVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductRelation.IsUnique)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ProductRelation.IsUnique)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility SurnameVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Person.Surname)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Person.Surname)].Visibility = value; }
        }


        public System.Windows.Visibility NameVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Person.Name)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Person.Name)].Visibility = value; }
        }


        public System.Windows.Visibility PatronymicVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Person.Patronymic)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Person.Patronymic)].Visibility = value; }
        }


        public System.Windows.Visibility IsManVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Person.IsMan)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Person.IsMan)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility SalesUnitIdVisibility
        {
            get { return PaymentPlannedListLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlannedList.SalesUnitId)].Visibility; }
            set { PaymentPlannedListLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlannedList.SalesUnitId)].Visibility = value; }
        }


        public System.Windows.Visibility ConditionVisibility
        {
            get { return PaymentPlannedListLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlannedList.Condition)].Visibility; }
            set { PaymentPlannedListLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlannedList.Condition)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlanned.Date)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlanned.Date)].Visibility = value; }
        }


        public System.Windows.Visibility SumVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlanned.Sum)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlanned.Sum)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlanned.Comment)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentPlanned.Comment)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility SalesUnitIdVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.SalesUnitId)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.SalesUnitId)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.Date)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.Date)].Visibility = value; }
        }


        public System.Windows.Visibility SumVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.Sum)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.Sum)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.Comment)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.Comment)].Visibility = value; }
        }


        public System.Windows.Visibility DocumentIdVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.DocumentId)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentActual.DocumentId)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility ParameterIdVisibility
        {
            get { return ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ParameterRelation.ParameterId)].Visibility; }
            set { ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.ParameterRelation.ParameterId)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility CostVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Cost)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Cost)].Visibility = value; }
        }


        public System.Windows.Visibility ProductionTermVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.ProductionTerm)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.ProductionTerm)].Visibility = value; }
        }


        public System.Windows.Visibility DeliveryDateExpectedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDateExpected)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDateExpected)].Visibility = value; }
        }


        public System.Windows.Visibility RealizationDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.RealizationDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.RealizationDate)].Visibility = value; }
        }


        public System.Windows.Visibility OrderPositionVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.OrderPosition)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.OrderPosition)].Visibility = value; }
        }


        public System.Windows.Visibility SerialNumberVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.SerialNumber)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.SerialNumber)].Visibility = value; }
        }


        public System.Windows.Visibility AssembleTermVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.AssembleTerm)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.AssembleTerm)].Visibility = value; }
        }


        public System.Windows.Visibility StartProductionDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDate)].Visibility = value; }
        }


        public System.Windows.Visibility PickingDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.PickingDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.PickingDate)].Visibility = value; }
        }


        public System.Windows.Visibility EndProductionDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDate)].Visibility = value; }
        }


        public System.Windows.Visibility ExpectedDeliveryPeriodVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.ExpectedDeliveryPeriod)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.ExpectedDeliveryPeriod)].Visibility = value; }
        }


        public System.Windows.Visibility CostOfShipmentVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.CostOfShipment)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.CostOfShipment)].Visibility = value; }
        }


        public System.Windows.Visibility ShipmentDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentDate)].Visibility = value; }
        }


        public System.Windows.Visibility ShipmentPlanDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentPlanDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentPlanDate)].Visibility = value; }
        }


        public System.Windows.Visibility DeliveryDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDate)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Product)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Product)].Visibility = value; }
        }


        public System.Windows.Visibility FacilityVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Facility)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Facility)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentConditionSetVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.PaymentConditionSet)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.PaymentConditionSet)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Project)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Project)].Visibility = value; }
        }


        public System.Windows.Visibility ProducerVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Producer)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Producer)].Visibility = value; }
        }


        public System.Windows.Visibility OrderVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Order)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Order)].Visibility = value; }
        }


        public System.Windows.Visibility SpecificationVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Specification)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Specification)].Visibility = value; }
        }


        public System.Windows.Visibility AddressVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Address)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SalesUnit.Address)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Service.Name)].Visibility; }
            set { ServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Service.Name)].Visibility = value; }
        }


        public System.Windows.Visibility AmountVisibility
        {
            get { return ServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Service.Amount)].Visibility; }
            set { ServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Service.Amount)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility CityVisibility
        {
            get { return TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendAddress.City)].Visibility; }
            set { TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendAddress.City)].Visibility = value; }
        }


        public System.Windows.Visibility StreetVisibility
        {
            get { return TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendAddress.Street)].Visibility; }
            set { TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendAddress.Street)].Visibility = value; }
        }


        public System.Windows.Visibility StreetNumberVisibility
        {
            get { return TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendAddress.StreetNumber)].Visibility; }
            set { TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendAddress.StreetNumber)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility FriendGroupIdVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.FriendGroupId)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.FriendGroupId)].Visibility = value; }
        }


        public System.Windows.Visibility FirstNameVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.FirstName)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.FirstName)].Visibility = value; }
        }


        public System.Windows.Visibility LastNameVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.LastName)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.LastName)].Visibility = value; }
        }


        public System.Windows.Visibility BirthdayVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.Birthday)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.Birthday)].Visibility = value; }
        }


        public System.Windows.Visibility IsDeveloperVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.IsDeveloper)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.IsDeveloper)].Visibility = value; }
        }


        public System.Windows.Visibility IdGetVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.IdGet)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.IdGet)].Visibility = value; }
        }


        public System.Windows.Visibility TestFriendAddressVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.TestFriendAddress)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.TestFriendAddress)].Visibility = value; }
        }


        public System.Windows.Visibility TestFriendGroupVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.TestFriendGroup)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.TestFriendGroup)].Visibility = value; }
        }


        public System.Windows.Visibility TestFriendEmailGetVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.TestFriendEmailGet)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriend.TestFriendEmailGet)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility EmailVisibility
        {
            get { return TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendEmail.Email)].Visibility; }
            set { TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendEmail.Email)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendEmail.Comment)].Visibility; }
            set { TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendEmail.Comment)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TestFriendGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendGroup.Name)].Visibility; }
            set { TestFriendGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestFriendGroup.Name)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility SenderIdVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.SenderId)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.SenderId)].Visibility = value; }
        }


        public System.Windows.Visibility RecipientIdVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RecipientId)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RecipientId)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.Comment)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.Comment)].Visibility = value; }
        }


        public System.Windows.Visibility RequestDocumentVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RequestDocument)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RequestDocument)].Visibility = value; }
        }


        public System.Windows.Visibility AuthorVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.Author)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.Author)].Visibility = value; }
        }


        public System.Windows.Visibility SenderEmployeeVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.SenderEmployee)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.SenderEmployee)].Visibility = value; }
        }


        public System.Windows.Visibility RecipientEmployeeVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RecipientEmployee)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RecipientEmployee)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDetailsOfSenderVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfSender)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfSender)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDetailsOfRecipientVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfRecipient)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfRecipient)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TestEntityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestEntity.Name)].Visibility; }
            set { TestEntityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestEntity.Name)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestHusband.Name)].Visibility; }
            set { TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestHusband.Name)].Visibility = value; }
        }


        public System.Windows.Visibility WifeVisibility
        {
            get { return TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestHusband.Wife)].Visibility; }
            set { TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestHusband.Wife)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NVisibility
        {
            get { return TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestWife.N)].Visibility; }
            set { TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestWife.N)].Visibility = value; }
        }


        public System.Windows.Visibility NameVisibility
        {
            get { return TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestWife.Name)].Visibility; }
            set { TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestWife.Name)].Visibility = value; }
        }


        public System.Windows.Visibility HusbandVisibility
        {
            get { return TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestWife.Husband)].Visibility; }
            set { TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestWife.Husband)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestChild.Name)].Visibility; }
            set { TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestChild.Name)].Visibility = value; }
        }


        public System.Windows.Visibility HusbandVisibility
        {
            get { return TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestChild.Husband)].Visibility; }
            set { TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestChild.Husband)].Visibility = value; }
        }


        public System.Windows.Visibility WifeVisibility
        {
            get { return TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestChild.Wife)].Visibility; }
            set { TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TestChild.Wife)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SumOnDate.Date)].Visibility; }
            set { SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SumOnDate.Date)].Visibility = value; }
        }


        public System.Windows.Visibility SumVisibility
        {
            get { return SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SumOnDate.Sum)].Visibility; }
            set { SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.SumOnDate.Sum)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DesignationVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Product.Designation)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Product.Designation)].Visibility = value; }
        }


        public System.Windows.Visibility ProductBlockVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Product.ProductBlock)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Product.ProductBlock)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ТКП")]
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

		#region VisibilityProps


        public System.Windows.Visibility ValidityDateVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.ValidityDate)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.ValidityDate)].Visibility = value; }
        }


        public System.Windows.Visibility VatVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.Vat)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.Vat)].Visibility = value; }
        }


        public System.Windows.Visibility SenderIdVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.SenderId)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.SenderId)].Visibility = value; }
        }


        public System.Windows.Visibility RecipientIdVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RecipientId)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RecipientId)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.Comment)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.Comment)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.Project)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.Project)].Visibility = value; }
        }


        public System.Windows.Visibility RequestDocumentVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RequestDocument)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RequestDocument)].Visibility = value; }
        }


        public System.Windows.Visibility AuthorVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.Author)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.Author)].Visibility = value; }
        }


        public System.Windows.Visibility SenderEmployeeVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.SenderEmployee)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.SenderEmployee)].Visibility = value; }
        }


        public System.Windows.Visibility RecipientEmployeeVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RecipientEmployee)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RecipientEmployee)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDetailsOfSenderVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfSender)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfSender)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDetailsOfRecipientVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfRecipient)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfRecipient)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility PhoneNumberVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.PhoneNumber)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.PhoneNumber)].Visibility = value; }
        }


        public System.Windows.Visibility EmailVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.Email)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.Email)].Visibility = value; }
        }


        public System.Windows.Visibility PersonVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.Person)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.Person)].Visibility = value; }
        }


        public System.Windows.Visibility CompanyVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.Company)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.Company)].Visibility = value; }
        }


        public System.Windows.Visibility PositionVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.Position)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Employee.Position)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Order.Number)].Visibility; }
            set { OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Order.Number)].Visibility = value; }
        }


        public System.Windows.Visibility OpenOrderDateVisibility
        {
            get { return OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Order.OpenOrderDate)].Visibility; }
            set { OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Order.OpenOrderDate)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility PartVisibility
        {
            get { return PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentCondition.Part)].Visibility; }
            set { PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentCondition.Part)].Visibility = value; }
        }


        public System.Windows.Visibility DaysToPointVisibility
        {
            get { return PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentCondition.DaysToPoint)].Visibility; }
            set { PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentCondition.DaysToPoint)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentConditionPointVisibility
        {
            get { return PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentCondition.PaymentConditionPoint)].Visibility; }
            set { PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentCondition.PaymentConditionPoint)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentDocument.Number)].Visibility; }
            set { PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentDocument.Number)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentDocument.Date)].Visibility; }
            set { PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.PaymentDocument.Date)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Facility.Name)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Facility.Name)].Visibility = value; }
        }


        public System.Windows.Visibility TypeVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Facility.Type)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Facility.Type)].Visibility = value; }
        }


        public System.Windows.Visibility OwnerCompanyVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Facility.OwnerCompany)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Facility.OwnerCompany)].Visibility = value; }
        }


        public System.Windows.Visibility AddressVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Facility.Address)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Facility.Address)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Project.Name)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Project.Name)].Visibility = value; }
        }


        public System.Windows.Visibility ManagerVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Project.Manager)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Project.Manager)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.UserRole.Name)].Visibility; }
            set { UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.UserRole.Name)].Visibility = value; }
        }


        public System.Windows.Visibility RoleVisibility
        {
            get { return UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.UserRole.Role)].Visibility; }
            set { UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.UserRole.Role)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Specification.Number)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Specification.Number)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Specification.Date)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Specification.Date)].Visibility = value; }
        }


        public System.Windows.Visibility VatVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Specification.Vat)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Specification.Vat)].Visibility = value; }
        }


        public System.Windows.Visibility ContractVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Specification.Contract)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Specification.Contract)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility DateOpenVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.DateOpen)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.DateOpen)].Visibility = value; }
        }


        public System.Windows.Visibility DateCloseVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.DateClose)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.DateClose)].Visibility = value; }
        }


        public System.Windows.Visibility DateNoticeVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.DateNotice)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.DateNotice)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.Project)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.Project)].Visibility = value; }
        }


        public System.Windows.Visibility WinnerVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.Winner)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.Tender.Winner)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("Типы тендера")]
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

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TenderType.Name)].Visibility; }
            set { TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TenderType.Name)].Visibility = value; }
        }


        public System.Windows.Visibility TypeVisibility
        {
            get { return TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TenderType.Type)].Visibility; }
            set { TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.TenderType.Type)].Visibility = value; }
        }



		#endregion
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

		#region VisibilityProps


        public System.Windows.Visibility LoginVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.Login)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.Login)].Visibility = value; }
        }


        public System.Windows.Visibility PasswordVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.Password)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.Password)].Visibility = value; }
        }


        public System.Windows.Visibility PersonalNumberVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.PersonalNumber)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.PersonalNumber)].Visibility = value; }
        }


        public System.Windows.Visibility RoleCurrentVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.RoleCurrent)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.RoleCurrent)].Visibility = value; }
        }


        public System.Windows.Visibility EmployeeVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.Employee)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.Model.POCOs.User.Employee)].Visibility = value; }
        }



		#endregion
    }


}
