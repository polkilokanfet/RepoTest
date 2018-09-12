using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using System.Windows;
using Prism.Events;
using Prism.Regions;
using HVTApp.UI.Tabs;
using HVTApp.UI.ViewModels;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Views
{

    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ActivityFieldLookup")]
    public partial class ActivityFieldLookupListView : ViewBase
    {
        public ActivityFieldLookupListView()
        {
            InitializeComponent();
        }

        public ActivityFieldLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ActivityFieldLookupListViewModel ActivityFieldLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ActivityFieldLookupListViewModel;
			ActivityFieldLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ActivityFieldLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ActivityFieldLookup.Name)].Visibility; }
            set { ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ActivityFieldLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility ActivityFieldEnumVisibility
        {
            get { return ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ActivityFieldLookup.ActivityFieldEnum)].Visibility; }
            set { ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ActivityFieldLookup.ActivityFieldEnum)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ActivityFieldLookup.DisplayMember)].Visibility; }
            set { ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ActivityFieldLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ActivityFieldLookup.Entity)].Visibility; }
            set { ActivityFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ActivityFieldLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("AddressLookup")]
    public partial class AddressLookupListView : ViewBase
    {
        public AddressLookupListView()
        {
            InitializeComponent();
        }

        public AddressLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, AddressLookupListViewModel AddressLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = AddressLookupListViewModel;
			AddressLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((AddressLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DescriptionVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Description)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Description)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.DisplayMember)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility RegionVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Region)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Region)].Visibility = value; }
        }


        public System.Windows.Visibility DistrictVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.District)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.District)].Visibility = value; }
        }


        public System.Windows.Visibility CountryVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Country)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Country)].Visibility = value; }
        }


        public System.Windows.Visibility LocalityVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Locality)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Locality)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Entity)].Visibility; }
            set { AddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.AddressLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("BankDetailsLookup")]
    public partial class BankDetailsLookupListView : ViewBase
    {
        public BankDetailsLookupListView()
        {
            InitializeComponent();
        }

        public BankDetailsLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, BankDetailsLookupListViewModel BankDetailsLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = BankDetailsLookupListViewModel;
			BankDetailsLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((BankDetailsLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility BankNameVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.BankName)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.BankName)].Visibility = value; }
        }


        public System.Windows.Visibility BankIdentificationCodeVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.BankIdentificationCode)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.BankIdentificationCode)].Visibility = value; }
        }


        public System.Windows.Visibility CorrespondentAccountVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.CorrespondentAccount)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.CorrespondentAccount)].Visibility = value; }
        }


        public System.Windows.Visibility CheckingAccountVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.CheckingAccount)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.CheckingAccount)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.DisplayMember)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.Entity)].Visibility; }
            set { BankDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.BankDetailsLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("CalculatePriceTaskLookup")]
    public partial class CalculatePriceTaskLookupListView : ViewBase
    {
        public CalculatePriceTaskLookupListView()
        {
            InitializeComponent();
        }

        public CalculatePriceTaskLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CalculatePriceTaskLookupListViewModel CalculatePriceTaskLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CalculatePriceTaskLookupListViewModel;
			CalculatePriceTaskLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CalculatePriceTaskLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility StatusStringVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.StatusString)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.StatusString)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectsVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Projects)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Projects)].Visibility = value; }
        }


        public System.Windows.Visibility OffersVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Offers)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Offers)].Visibility = value; }
        }


        public System.Windows.Visibility SpecificationsVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Specifications)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Specifications)].Visibility = value; }
        }


        public System.Windows.Visibility StatusVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Status)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Status)].Visibility = value; }
        }


        public System.Windows.Visibility SumVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Sum)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Sum)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Date)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.DisplayMember)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProductBlockVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.ProductBlock)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.ProductBlock)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Entity)].Visibility; }
            set { CalculatePriceTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CalculatePriceTaskLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("CommonOptionLookup")]
    public partial class CommonOptionLookupListView : ViewBase
    {
        public CommonOptionLookupListView()
        {
            InitializeComponent();
        }

        public CommonOptionLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CommonOptionLookupListViewModel CommonOptionLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CommonOptionLookupListViewModel;
			CommonOptionLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CommonOptionLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.Date)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility OurCompanyIdVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.OurCompanyId)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.OurCompanyId)].Visibility = value; }
        }


        public System.Windows.Visibility ActualPriceTermVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.ActualPriceTerm)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.ActualPriceTerm)].Visibility = value; }
        }


        public System.Windows.Visibility StandartTermFromStartToEndProductionVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.StandartTermFromStartToEndProduction)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.StandartTermFromStartToEndProduction)].Visibility = value; }
        }


        public System.Windows.Visibility StandartTermFromPickToEndProductionVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.StandartTermFromPickToEndProduction)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.StandartTermFromPickToEndProduction)].Visibility = value; }
        }


        public System.Windows.Visibility StandartPaymentsConditionSetIdVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.StandartPaymentsConditionSetId)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.StandartPaymentsConditionSetId)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.DisplayMember)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.Entity)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("CompanyFormLookup")]
    public partial class CompanyFormLookupListView : ViewBase
    {
        public CompanyFormLookupListView()
        {
            InitializeComponent();
        }

        public CompanyFormLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyFormLookupListViewModel CompanyFormLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyFormLookupListViewModel;
			CompanyFormLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CompanyFormLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyFormLookup.FullName)].Visibility; }
            set { CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyFormLookup.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyFormLookup.ShortName)].Visibility; }
            set { CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyFormLookup.ShortName)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyFormLookup.DisplayMember)].Visibility; }
            set { CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyFormLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyFormLookup.Entity)].Visibility; }
            set { CompanyFormLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyFormLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("CompanyLookup")]
    public partial class CompanyLookupListView : ViewBase
    {
        public CompanyLookupListView()
        {
            InitializeComponent();
        }

        public CompanyLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyLookupListViewModel CompanyLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CompanyLookupListViewModel;
			CompanyLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CompanyLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.FullName)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.ShortName)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.ShortName)].Visibility = value; }
        }


        public System.Windows.Visibility InnVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.Inn)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.Inn)].Visibility = value; }
        }


        public System.Windows.Visibility KppVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.Kpp)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.Kpp)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.DisplayMember)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility FormVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.Form)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.Form)].Visibility = value; }
        }


        public System.Windows.Visibility ParentCompanyVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.ParentCompany)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.ParentCompany)].Visibility = value; }
        }


        public System.Windows.Visibility AddressLegalVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.AddressLegal)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.AddressLegal)].Visibility = value; }
        }


        public System.Windows.Visibility AddressPostVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.AddressPost)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.AddressPost)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.Entity)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ContractLookup")]
    public partial class ContractLookupListView : ViewBase
    {
        public ContractLookupListView()
        {
            InitializeComponent();
        }

        public ContractLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ContractLookupListViewModel ContractLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ContractLookupListViewModel;
			ContractLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ContractLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.Number)].Visibility; }
            set { ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.Number)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.Date)].Visibility; }
            set { ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.DisplayMember)].Visibility; }
            set { ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ContragentVisibility
        {
            get { return ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.Contragent)].Visibility; }
            set { ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.Contragent)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.Entity)].Visibility; }
            set { ContractLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ContractLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("CountryLookup")]
    public partial class CountryLookupListView : ViewBase
    {
        public CountryLookupListView()
        {
            InitializeComponent();
        }

        public CountryLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CountryLookupListViewModel CountryLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CountryLookupListViewModel;
			CountryLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CountryLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return CountryLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CountryLookup.Name)].Visibility; }
            set { CountryLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CountryLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return CountryLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CountryLookup.DisplayMember)].Visibility; }
            set { CountryLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CountryLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return CountryLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CountryLookup.Entity)].Visibility; }
            set { CountryLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CountryLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("CreateNewProductTaskLookup")]
    public partial class CreateNewProductTaskLookupListView : ViewBase
    {
        public CreateNewProductTaskLookupListView()
        {
            InitializeComponent();
        }

        public CreateNewProductTaskLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CreateNewProductTaskLookupListViewModel CreateNewProductTaskLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CreateNewProductTaskLookupListViewModel;
			CreateNewProductTaskLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CreateNewProductTaskLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DesignationVisibility
        {
            get { return CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.Designation)].Visibility; }
            set { CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.Designation)].Visibility = value; }
        }


        public System.Windows.Visibility StructureCostNumberVisibility
        {
            get { return CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.StructureCostNumber)].Visibility; }
            set { CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.StructureCostNumber)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.DisplayMember)].Visibility; }
            set { CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.Product)].Visibility; }
            set { CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.Product)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.Entity)].Visibility; }
            set { CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("CurrencyExchangeRateLookup")]
    public partial class CurrencyExchangeRateLookupListView : ViewBase
    {
        public CurrencyExchangeRateLookupListView()
        {
            InitializeComponent();
        }

        public CurrencyExchangeRateLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, CurrencyExchangeRateLookupListViewModel CurrencyExchangeRateLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = CurrencyExchangeRateLookupListViewModel;
			CurrencyExchangeRateLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((CurrencyExchangeRateLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.Date)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility FirstCurrencyVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.FirstCurrency)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.FirstCurrency)].Visibility = value; }
        }


        public System.Windows.Visibility SecondCurrencyVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.SecondCurrency)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.SecondCurrency)].Visibility = value; }
        }


        public System.Windows.Visibility ExchangeRateVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.ExchangeRate)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.ExchangeRate)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.DisplayMember)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.Entity)].Visibility; }
            set { CurrencyExchangeRateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CurrencyExchangeRateLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("DescribeProductBlockTaskLookup")]
    public partial class DescribeProductBlockTaskLookupListView : ViewBase
    {
        public DescribeProductBlockTaskLookupListView()
        {
            InitializeComponent();
        }

        public DescribeProductBlockTaskLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DescribeProductBlockTaskLookupListViewModel DescribeProductBlockTaskLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DescribeProductBlockTaskLookupListViewModel;
			DescribeProductBlockTaskLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DescribeProductBlockTaskLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DescribeProductBlockTaskLookup.DisplayMember)].Visibility; }
            set { DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DescribeProductBlockTaskLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProductBlockVisibility
        {
            get { return DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DescribeProductBlockTaskLookup.ProductBlock)].Visibility; }
            set { DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DescribeProductBlockTaskLookup.ProductBlock)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DescribeProductBlockTaskLookup.Product)].Visibility; }
            set { DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DescribeProductBlockTaskLookup.Product)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DescribeProductBlockTaskLookup.Entity)].Visibility; }
            set { DescribeProductBlockTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DescribeProductBlockTaskLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("DistrictLookup")]
    public partial class DistrictLookupListView : ViewBase
    {
        public DistrictLookupListView()
        {
            InitializeComponent();
        }

        public DistrictLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DistrictLookupListViewModel DistrictLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DistrictLookupListViewModel;
			DistrictLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DistrictLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DistrictLookup.Name)].Visibility; }
            set { DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DistrictLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DistrictLookup.DisplayMember)].Visibility; }
            set { DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DistrictLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility CountryVisibility
        {
            get { return DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DistrictLookup.Country)].Visibility; }
            set { DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DistrictLookup.Country)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DistrictLookup.Entity)].Visibility; }
            set { DistrictLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DistrictLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("DocumentIncomingNumberLookup")]
    public partial class DocumentIncomingNumberLookupListView : ViewBase
    {
        public DocumentIncomingNumberLookupListView()
        {
            InitializeComponent();
        }

        public DocumentIncomingNumberLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentIncomingNumberLookupListViewModel DocumentIncomingNumberLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentIncomingNumberLookupListViewModel;
			DocumentIncomingNumberLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DocumentIncomingNumberLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NumVisibility
        {
            get { return DocumentIncomingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentIncomingNumberLookup.Num)].Visibility; }
            set { DocumentIncomingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentIncomingNumberLookup.Num)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return DocumentIncomingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentIncomingNumberLookup.DisplayMember)].Visibility; }
            set { DocumentIncomingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentIncomingNumberLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return DocumentIncomingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentIncomingNumberLookup.Entity)].Visibility; }
            set { DocumentIncomingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentIncomingNumberLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("DocumentLookup")]
    public partial class DocumentLookupListView : ViewBase
    {
        public DocumentLookupListView()
        {
            InitializeComponent();
        }

        public DocumentLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentLookupListViewModel DocumentLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentLookupListViewModel;
			DocumentLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DocumentLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility SenderIdVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.SenderId)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.SenderId)].Visibility = value; }
        }


        public System.Windows.Visibility RecipientIdVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RecipientId)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RecipientId)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Comment)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Comment)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.DisplayMember)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility RequestDocumentVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RequestDocument)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RequestDocument)].Visibility = value; }
        }


        public System.Windows.Visibility AuthorVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Author)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Author)].Visibility = value; }
        }


        public System.Windows.Visibility SenderEmployeeVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.SenderEmployee)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.SenderEmployee)].Visibility = value; }
        }


        public System.Windows.Visibility RecipientEmployeeVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RecipientEmployee)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RecipientEmployee)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDetailsOfSenderVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RegistrationDetailsOfSender)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RegistrationDetailsOfSender)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDetailsOfRecipientVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RegistrationDetailsOfRecipient)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RegistrationDetailsOfRecipient)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Entity)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("DocumentOutgoingNumberLookup")]
    public partial class DocumentOutgoingNumberLookupListView : ViewBase
    {
        public DocumentOutgoingNumberLookupListView()
        {
            InitializeComponent();
        }

        public DocumentOutgoingNumberLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentOutgoingNumberLookupListViewModel DocumentOutgoingNumberLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentOutgoingNumberLookupListViewModel;
			DocumentOutgoingNumberLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DocumentOutgoingNumberLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NumVisibility
        {
            get { return DocumentOutgoingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentOutgoingNumberLookup.Num)].Visibility; }
            set { DocumentOutgoingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentOutgoingNumberLookup.Num)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return DocumentOutgoingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentOutgoingNumberLookup.DisplayMember)].Visibility; }
            set { DocumentOutgoingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentOutgoingNumberLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return DocumentOutgoingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentOutgoingNumberLookup.Entity)].Visibility; }
            set { DocumentOutgoingNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentOutgoingNumberLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("DocumentsRegistrationDetailsLookup")]
    public partial class DocumentsRegistrationDetailsLookupListView : ViewBase
    {
        public DocumentsRegistrationDetailsLookupListView()
        {
            InitializeComponent();
        }

        public DocumentsRegistrationDetailsLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentsRegistrationDetailsLookupListViewModel DocumentsRegistrationDetailsLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentsRegistrationDetailsLookupListViewModel;
			DocumentsRegistrationDetailsLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DocumentsRegistrationDetailsLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility RegistrationNumberVisibility
        {
            get { return DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.RegistrationNumber)].Visibility; }
            set { DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.RegistrationNumber)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDateVisibility
        {
            get { return DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.RegistrationDate)].Visibility; }
            set { DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.RegistrationDate)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.DisplayMember)].Visibility; }
            set { DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.Entity)].Visibility; }
            set { DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("EmployeeLookup")]
    public partial class EmployeeLookupListView : ViewBase
    {
        public EmployeeLookupListView()
        {
            InitializeComponent();
        }

        public EmployeeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeeLookupListViewModel EmployeeLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeeLookupListViewModel;
			EmployeeLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((EmployeeLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility PhoneNumberVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.PhoneNumber)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.PhoneNumber)].Visibility = value; }
        }


        public System.Windows.Visibility EmailVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Email)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Email)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.DisplayMember)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility PersonVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Person)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Person)].Visibility = value; }
        }


        public System.Windows.Visibility CompanyVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Company)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Company)].Visibility = value; }
        }


        public System.Windows.Visibility PositionVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Position)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Position)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Entity)].Visibility; }
            set { EmployeeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeeLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("EmployeesPositionLookup")]
    public partial class EmployeesPositionLookupListView : ViewBase
    {
        public EmployeesPositionLookupListView()
        {
            InitializeComponent();
        }

        public EmployeesPositionLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeesPositionLookupListViewModel EmployeesPositionLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = EmployeesPositionLookupListViewModel;
			EmployeesPositionLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((EmployeesPositionLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return EmployeesPositionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeesPositionLookup.Name)].Visibility; }
            set { EmployeesPositionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeesPositionLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return EmployeesPositionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeesPositionLookup.DisplayMember)].Visibility; }
            set { EmployeesPositionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeesPositionLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return EmployeesPositionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeesPositionLookup.Entity)].Visibility; }
            set { EmployeesPositionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.EmployeesPositionLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("FacilityLookup")]
    public partial class FacilityLookupListView : ViewBase
    {
        public FacilityLookupListView()
        {
            InitializeComponent();
        }

        public FacilityLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityLookupListViewModel FacilityLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityLookupListViewModel;
			FacilityLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((FacilityLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.Name)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.DisplayMember)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility TypeVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.Type)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.Type)].Visibility = value; }
        }


        public System.Windows.Visibility OwnerCompanyVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.OwnerCompany)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.OwnerCompany)].Visibility = value; }
        }


        public System.Windows.Visibility AddressVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.Address)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.Address)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.Entity)].Visibility; }
            set { FacilityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("FacilityTypeLookup")]
    public partial class FacilityTypeLookupListView : ViewBase
    {
        public FacilityTypeLookupListView()
        {
            InitializeComponent();
        }

        public FacilityTypeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityTypeLookupListViewModel FacilityTypeLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = FacilityTypeLookupListViewModel;
			FacilityTypeLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((FacilityTypeLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityTypeLookup.FullName)].Visibility; }
            set { FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityTypeLookup.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityTypeLookup.ShortName)].Visibility; }
            set { FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityTypeLookup.ShortName)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityTypeLookup.DisplayMember)].Visibility; }
            set { FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityTypeLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityTypeLookup.Entity)].Visibility; }
            set { FacilityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.FacilityTypeLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("LocalityLookup")]
    public partial class LocalityLookupListView : ViewBase
    {
        public LocalityLookupListView()
        {
            InitializeComponent();
        }

        public LocalityLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityLookupListViewModel LocalityLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityLookupListViewModel;
			LocalityLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((LocalityLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.Name)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility IsCountryCapitalVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.IsCountryCapital)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.IsCountryCapital)].Visibility = value; }
        }


        public System.Windows.Visibility IsDistrictCapitalVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.IsDistrictCapital)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.IsDistrictCapital)].Visibility = value; }
        }


        public System.Windows.Visibility IsRegionCapitalVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.IsRegionCapital)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.IsRegionCapital)].Visibility = value; }
        }


        public System.Windows.Visibility StandartDeliveryPeriodVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.StandartDeliveryPeriod)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.StandartDeliveryPeriod)].Visibility = value; }
        }


        public System.Windows.Visibility DistanceToEkbVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.DistanceToEkb)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.DistanceToEkb)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.DisplayMember)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility LocalityTypeVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.LocalityType)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.LocalityType)].Visibility = value; }
        }


        public System.Windows.Visibility RegionVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.Region)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.Region)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.Entity)].Visibility; }
            set { LocalityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("LocalityTypeLookup")]
    public partial class LocalityTypeLookupListView : ViewBase
    {
        public LocalityTypeLookupListView()
        {
            InitializeComponent();
        }

        public LocalityTypeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityTypeLookupListViewModel LocalityTypeLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = LocalityTypeLookupListViewModel;
			LocalityTypeLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((LocalityTypeLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityTypeLookup.FullName)].Visibility; }
            set { LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityTypeLookup.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityTypeLookup.ShortName)].Visibility; }
            set { LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityTypeLookup.ShortName)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityTypeLookup.DisplayMember)].Visibility; }
            set { LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityTypeLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityTypeLookup.Entity)].Visibility; }
            set { LocalityTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.LocalityTypeLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("MeasureLookup")]
    public partial class MeasureLookupListView : ViewBase
    {
        public MeasureLookupListView()
        {
            InitializeComponent();
        }

        public MeasureLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, MeasureLookupListViewModel MeasureLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = MeasureLookupListViewModel;
			MeasureLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((MeasureLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility FullNameVisibility
        {
            get { return MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MeasureLookup.FullName)].Visibility; }
            set { MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MeasureLookup.FullName)].Visibility = value; }
        }


        public System.Windows.Visibility ShortNameVisibility
        {
            get { return MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MeasureLookup.ShortName)].Visibility; }
            set { MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MeasureLookup.ShortName)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MeasureLookup.DisplayMember)].Visibility; }
            set { MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MeasureLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MeasureLookup.Entity)].Visibility; }
            set { MeasureLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MeasureLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("NoteLookup")]
    public partial class NoteLookupListView : ViewBase
    {
        public NoteLookupListView()
        {
            InitializeComponent();
        }

        public NoteLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, NoteLookupListViewModel NoteLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = NoteLookupListViewModel;
			NoteLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((NoteLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.Date)].Visibility; }
            set { NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility TextVisibility
        {
            get { return NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.Text)].Visibility; }
            set { NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.Text)].Visibility = value; }
        }


        public System.Windows.Visibility IsImportantVisibility
        {
            get { return NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.IsImportant)].Visibility; }
            set { NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.IsImportant)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.DisplayMember)].Visibility; }
            set { NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.Entity)].Visibility; }
            set { NoteLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.NoteLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("OfferLookup")]
    public partial class OfferLookupListView : ViewBase
    {
        public OfferLookupListView()
        {
            InitializeComponent();
        }

        public OfferLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferLookupListViewModel OfferLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferLookupListViewModel;
			OfferLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((OfferLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility ValidityDateVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.ValidityDate)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.ValidityDate)].Visibility = value; }
        }


        public System.Windows.Visibility VatVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Vat)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Vat)].Visibility = value; }
        }


        public System.Windows.Visibility SenderIdVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.SenderId)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.SenderId)].Visibility = value; }
        }


        public System.Windows.Visibility RecipientIdVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RecipientId)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RecipientId)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Comment)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Comment)].Visibility = value; }
        }


        public System.Windows.Visibility SumVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Sum)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Sum)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.DisplayMember)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Project)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Project)].Visibility = value; }
        }


        public System.Windows.Visibility RequestDocumentVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RequestDocument)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RequestDocument)].Visibility = value; }
        }


        public System.Windows.Visibility AuthorVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Author)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Author)].Visibility = value; }
        }


        public System.Windows.Visibility SenderEmployeeVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.SenderEmployee)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.SenderEmployee)].Visibility = value; }
        }


        public System.Windows.Visibility RecipientEmployeeVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RecipientEmployee)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RecipientEmployee)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDetailsOfSenderVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RegistrationDetailsOfSender)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RegistrationDetailsOfSender)].Visibility = value; }
        }


        public System.Windows.Visibility RegistrationDetailsOfRecipientVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RegistrationDetailsOfRecipient)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RegistrationDetailsOfRecipient)].Visibility = value; }
        }


        public System.Windows.Visibility CompanyVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Company)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Company)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Entity)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility OfferUnitsVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.OfferUnits)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.OfferUnits)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("OfferUnitLookup")]
    public partial class OfferUnitLookupListView : ViewBase
    {
        public OfferUnitLookupListView()
        {
            InitializeComponent();
        }

        public OfferUnitLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferUnitLookupListViewModel OfferUnitLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OfferUnitLookupListViewModel;
			OfferUnitLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((OfferUnitLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility CostVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Cost)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Cost)].Visibility = value; }
        }


        public System.Windows.Visibility ProductionTermVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.ProductionTerm)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.ProductionTerm)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.DisplayMember)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility OfferVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Offer)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Offer)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Product)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Product)].Visibility = value; }
        }


        public System.Windows.Visibility FacilityVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Facility)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Facility)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentConditionSetVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.PaymentConditionSet)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.PaymentConditionSet)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Entity)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility DependentProductsVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.DependentProducts)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.DependentProducts)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("OrderLookup")]
    public partial class OrderLookupListView : ViewBase
    {
        public OrderLookupListView()
        {
            InitializeComponent();
        }

        public OrderLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, OrderLookupListViewModel OrderLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = OrderLookupListViewModel;
			OrderLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((OrderLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OrderLookup.Number)].Visibility; }
            set { OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OrderLookup.Number)].Visibility = value; }
        }


        public System.Windows.Visibility DateOpenVisibility
        {
            get { return OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OrderLookup.DateOpen)].Visibility; }
            set { OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OrderLookup.DateOpen)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OrderLookup.DisplayMember)].Visibility; }
            set { OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OrderLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OrderLookup.Entity)].Visibility; }
            set { OrderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OrderLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ParameterGroupLookup")]
    public partial class ParameterGroupLookupListView : ViewBase
    {
        public ParameterGroupLookupListView()
        {
            InitializeComponent();
        }

        public ParameterGroupLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterGroupLookupListViewModel ParameterGroupLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterGroupLookupListViewModel;
			ParameterGroupLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ParameterGroupLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterGroupLookup.Name)].Visibility; }
            set { ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterGroupLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterGroupLookup.DisplayMember)].Visibility; }
            set { ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterGroupLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility MeasureVisibility
        {
            get { return ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterGroupLookup.Measure)].Visibility; }
            set { ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterGroupLookup.Measure)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterGroupLookup.Entity)].Visibility; }
            set { ParameterGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterGroupLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ParameterLookup")]
    public partial class ParameterLookupListView : ViewBase
    {
        public ParameterLookupListView()
        {
            InitializeComponent();
        }

        public ParameterLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterLookupListViewModel ParameterLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterLookupListViewModel;
			ParameterLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ParameterLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility ValueVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.Value)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.Value)].Visibility = value; }
        }


        public System.Windows.Visibility IsOriginVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.IsOrigin)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.IsOrigin)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.DisplayMember)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ParameterGroupVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.ParameterGroup)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.ParameterGroup)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.Entity)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ParameterRelationLookup")]
    public partial class ParameterRelationLookupListView : ViewBase
    {
        public ParameterRelationLookupListView()
        {
            InitializeComponent();
        }

        public ParameterRelationLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterRelationLookupListViewModel ParameterRelationLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ParameterRelationLookupListViewModel;
			ParameterRelationLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ParameterRelationLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility ParameterIdVisibility
        {
            get { return ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterRelationLookup.ParameterId)].Visibility; }
            set { ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterRelationLookup.ParameterId)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterRelationLookup.DisplayMember)].Visibility; }
            set { ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterRelationLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterRelationLookup.Entity)].Visibility; }
            set { ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterRelationLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("PaymentActualLookup")]
    public partial class PaymentActualLookupListView : ViewBase
    {
        public PaymentActualLookupListView()
        {
            InitializeComponent();
        }

        public PaymentActualLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentActualLookupListViewModel PaymentActualLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentActualLookupListViewModel;
			PaymentActualLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentActualLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.Date)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility SumVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.Sum)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.Sum)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.Comment)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.Comment)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.DisplayMember)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.Entity)].Visibility; }
            set { PaymentActualLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentActualLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("PaymentConditionLookup")]
    public partial class PaymentConditionLookupListView : ViewBase
    {
        public PaymentConditionLookupListView()
        {
            InitializeComponent();
        }

        public PaymentConditionLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionLookupListViewModel PaymentConditionLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentConditionLookupListViewModel;
			PaymentConditionLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentConditionLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility PartVisibility
        {
            get { return PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.Part)].Visibility; }
            set { PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.Part)].Visibility = value; }
        }


        public System.Windows.Visibility DaysToPointVisibility
        {
            get { return PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.DaysToPoint)].Visibility; }
            set { PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.DaysToPoint)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentConditionPointVisibility
        {
            get { return PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.PaymentConditionPoint)].Visibility; }
            set { PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.PaymentConditionPoint)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.DisplayMember)].Visibility; }
            set { PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.Entity)].Visibility; }
            set { PaymentConditionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("PaymentConditionSetLookup")]
    public partial class PaymentConditionSetLookupListView : ViewBase
    {
        public PaymentConditionSetLookupListView()
        {
            InitializeComponent();
        }

        public PaymentConditionSetLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionSetLookupListViewModel PaymentConditionSetLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentConditionSetLookupListViewModel;
			PaymentConditionSetLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentConditionSetLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return PaymentConditionSetLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionSetLookup.DisplayMember)].Visibility; }
            set { PaymentConditionSetLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionSetLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return PaymentConditionSetLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionSetLookup.Entity)].Visibility; }
            set { PaymentConditionSetLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionSetLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentConditionsVisibility
        {
            get { return PaymentConditionSetLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionSetLookup.PaymentConditions)].Visibility; }
            set { PaymentConditionSetLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentConditionSetLookup.PaymentConditions)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("PaymentDocumentLookup")]
    public partial class PaymentDocumentLookupListView : ViewBase
    {
        public PaymentDocumentLookupListView()
        {
            InitializeComponent();
        }

        public PaymentDocumentLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentDocumentLookupListViewModel PaymentDocumentLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentDocumentLookupListViewModel;
			PaymentDocumentLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentDocumentLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Number)].Visibility; }
            set { PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Number)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Date)].Visibility; }
            set { PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.DisplayMember)].Visibility; }
            set { PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Entity)].Visibility; }
            set { PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("PaymentPlannedLookup")]
    public partial class PaymentPlannedLookupListView : ViewBase
    {
        public PaymentPlannedLookupListView()
        {
            InitializeComponent();
        }

        public PaymentPlannedLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedLookupListViewModel PaymentPlannedLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PaymentPlannedLookupListViewModel;
			PaymentPlannedLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PaymentPlannedLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility SumVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Sum)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Sum)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Date)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility DateCalculatedVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.DateCalculated)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.DateCalculated)].Visibility = value; }
        }


        public System.Windows.Visibility PartVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Part)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Part)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Comment)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Comment)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.DisplayMember)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ConditionVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Condition)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Condition)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Entity)].Visibility; }
            set { PaymentPlannedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentPlannedLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("PersonLookup")]
    public partial class PersonLookupListView : ViewBase
    {
        public PersonLookupListView()
        {
            InitializeComponent();
        }

        public PersonLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, PersonLookupListViewModel PersonLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = PersonLookupListViewModel;
			PersonLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((PersonLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility SurnameVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.Surname)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.Surname)].Visibility = value; }
        }


        public System.Windows.Visibility NameVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.Name)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility PatronymicVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.Patronymic)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.Patronymic)].Visibility = value; }
        }


        public System.Windows.Visibility IsManVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.IsMan)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.IsMan)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.DisplayMember)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.Entity)].Visibility; }
            set { PersonLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PersonLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductBlockIsServiceLookup")]
    public partial class ProductBlockIsServiceLookupListView : ViewBase
    {
        public ProductBlockIsServiceLookupListView()
        {
            InitializeComponent();
        }

        public ProductBlockIsServiceLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockIsServiceLookupListViewModel ProductBlockIsServiceLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductBlockIsServiceLookupListViewModel;
			ProductBlockIsServiceLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductBlockIsServiceLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductBlockIsServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockIsServiceLookup.DisplayMember)].Visibility; }
            set { ProductBlockIsServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockIsServiceLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductBlockIsServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockIsServiceLookup.Entity)].Visibility; }
            set { ProductBlockIsServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockIsServiceLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductBlockLookup")]
    public partial class ProductBlockLookupListView : ViewBase
    {
        public ProductBlockLookupListView()
        {
            InitializeComponent();
        }

        public ProductBlockLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockLookupListViewModel ProductBlockLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductBlockLookupListViewModel;
			ProductBlockLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductBlockLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DesignationVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Designation)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Designation)].Visibility = value; }
        }


        public System.Windows.Visibility DesignationSpecialVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.DesignationSpecial)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.DesignationSpecial)].Visibility = value; }
        }


        public System.Windows.Visibility StructureCostNumberVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.StructureCostNumber)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.StructureCostNumber)].Visibility = value; }
        }


        public System.Windows.Visibility IsServiceVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.IsService)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.IsService)].Visibility = value; }
        }


        public System.Windows.Visibility WeightVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Weight)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Weight)].Visibility = value; }
        }


        public System.Windows.Visibility LastPriceDateVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.LastPriceDate)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.LastPriceDate)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.DisplayMember)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Entity)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductDependentLookup")]
    public partial class ProductDependentLookupListView : ViewBase
    {
        public ProductDependentLookupListView()
        {
            InitializeComponent();
        }

        public ProductDependentLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductDependentLookupListViewModel ProductDependentLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductDependentLookupListViewModel;
			ProductDependentLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductDependentLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility MainProductIdVisibility
        {
            get { return ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.MainProductId)].Visibility; }
            set { ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.MainProductId)].Visibility = value; }
        }


        public System.Windows.Visibility AmountVisibility
        {
            get { return ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.Amount)].Visibility; }
            set { ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.Amount)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.DisplayMember)].Visibility; }
            set { ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.Product)].Visibility; }
            set { ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.Product)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.Entity)].Visibility; }
            set { ProductDependentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDependentLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductDesignationLookup")]
    public partial class ProductDesignationLookupListView : ViewBase
    {
        public ProductDesignationLookupListView()
        {
            InitializeComponent();
        }

        public ProductDesignationLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductDesignationLookupListViewModel ProductDesignationLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductDesignationLookupListViewModel;
			ProductDesignationLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductDesignationLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DesignationVisibility
        {
            get { return ProductDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDesignationLookup.Designation)].Visibility; }
            set { ProductDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDesignationLookup.Designation)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDesignationLookup.DisplayMember)].Visibility; }
            set { ProductDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDesignationLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDesignationLookup.Entity)].Visibility; }
            set { ProductDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDesignationLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductIncludedLookup")]
    public partial class ProductIncludedLookupListView : ViewBase
    {
        public ProductIncludedLookupListView()
        {
            InitializeComponent();
        }

        public ProductIncludedLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductIncludedLookupListViewModel ProductIncludedLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductIncludedLookupListViewModel;
			ProductIncludedLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductIncludedLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility AmountVisibility
        {
            get { return ProductIncludedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductIncludedLookup.Amount)].Visibility; }
            set { ProductIncludedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductIncludedLookup.Amount)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductIncludedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductIncludedLookup.DisplayMember)].Visibility; }
            set { ProductIncludedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductIncludedLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return ProductIncludedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductIncludedLookup.Product)].Visibility; }
            set { ProductIncludedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductIncludedLookup.Product)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductIncludedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductIncludedLookup.Entity)].Visibility; }
            set { ProductIncludedLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductIncludedLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductionTaskLookup")]
    public partial class ProductionTaskLookupListView : ViewBase
    {
        public ProductionTaskLookupListView()
        {
            InitializeComponent();
        }

        public ProductionTaskLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductionTaskLookupListViewModel ProductionTaskLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductionTaskLookupListViewModel;
			ProductionTaskLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductionTaskLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DateTaskVisibility
        {
            get { return ProductionTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductionTaskLookup.DateTask)].Visibility; }
            set { ProductionTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductionTaskLookup.DateTask)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductionTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductionTaskLookup.DisplayMember)].Visibility; }
            set { ProductionTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductionTaskLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductionTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductionTaskLookup.Entity)].Visibility; }
            set { ProductionTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductionTaskLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductLookup")]
    public partial class ProductLookupListView : ViewBase
    {
        public ProductLookupListView()
        {
            InitializeComponent();
        }

        public ProductLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductLookupListViewModel ProductLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductLookupListViewModel;
			ProductLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DesignationVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.Designation)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.Designation)].Visibility = value; }
        }


        public System.Windows.Visibility DesignationSpecialVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.DesignationSpecial)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.DesignationSpecial)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.DisplayMember)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProductTypeVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.ProductType)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.ProductType)].Visibility = value; }
        }


        public System.Windows.Visibility ProductBlockVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.ProductBlock)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.ProductBlock)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.Entity)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductRelationLookup")]
    public partial class ProductRelationLookupListView : ViewBase
    {
        public ProductRelationLookupListView()
        {
            InitializeComponent();
        }

        public ProductRelationLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductRelationLookupListViewModel ProductRelationLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductRelationLookupListViewModel;
			ProductRelationLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductRelationLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility ChildProductsAmountVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.ChildProductsAmount)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.ChildProductsAmount)].Visibility = value; }
        }


        public System.Windows.Visibility IsUniqueVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.IsUnique)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.IsUnique)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.DisplayMember)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.Entity)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductTypeDesignationLookup")]
    public partial class ProductTypeDesignationLookupListView : ViewBase
    {
        public ProductTypeDesignationLookupListView()
        {
            InitializeComponent();
        }

        public ProductTypeDesignationLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductTypeDesignationLookupListViewModel ProductTypeDesignationLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductTypeDesignationLookupListViewModel;
			ProductTypeDesignationLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductTypeDesignationLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductTypeDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeDesignationLookup.DisplayMember)].Visibility; }
            set { ProductTypeDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeDesignationLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProductTypeVisibility
        {
            get { return ProductTypeDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeDesignationLookup.ProductType)].Visibility; }
            set { ProductTypeDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeDesignationLookup.ProductType)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductTypeDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeDesignationLookup.Entity)].Visibility; }
            set { ProductTypeDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeDesignationLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProductTypeLookup")]
    public partial class ProductTypeLookupListView : ViewBase
    {
        public ProductTypeLookupListView()
        {
            InitializeComponent();
        }

        public ProductTypeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductTypeLookupListViewModel ProductTypeLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProductTypeLookupListViewModel;
			ProductTypeLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProductTypeLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ProductTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeLookup.Name)].Visibility; }
            set { ProductTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProductTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeLookup.DisplayMember)].Visibility; }
            set { ProductTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProductTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeLookup.Entity)].Visibility; }
            set { ProductTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProjectLookup")]
    public partial class ProjectLookupListView : ViewBase
    {
        public ProjectLookupListView()
        {
            InitializeComponent();
        }

        public ProjectLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectLookupListViewModel ProjectLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProjectLookupListViewModel;
			ProjectLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProjectLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Name)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility HighProbabilityVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.HighProbability)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.HighProbability)].Visibility = value; }
        }


        public System.Windows.Visibility SumVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Sum)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Sum)].Visibility = value; }
        }


        public System.Windows.Visibility RealizationDateVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.RealizationDate)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.RealizationDate)].Visibility = value; }
        }


        public System.Windows.Visibility TenderDateVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.TenderDate)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.TenderDate)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.DisplayMember)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectTypeVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.ProjectType)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.ProjectType)].Visibility = value; }
        }


        public System.Windows.Visibility ManagerVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Manager)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Manager)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Entity)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility SalesUnitsVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.SalesUnits)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.SalesUnits)].Visibility = value; }
        }


        public System.Windows.Visibility TendersVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Tenders)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Tenders)].Visibility = value; }
        }


        public System.Windows.Visibility OffersVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Offers)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Offers)].Visibility = value; }
        }


        public System.Windows.Visibility NotesVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Notes)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Notes)].Visibility = value; }
        }


        public System.Windows.Visibility FacilitiesVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Facilities)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Facilities)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("ProjectTypeLookup")]
    public partial class ProjectTypeLookupListView : ViewBase
    {
        public ProjectTypeLookupListView()
        {
            InitializeComponent();
        }

        public ProjectTypeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectTypeLookupListViewModel ProjectTypeLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = ProjectTypeLookupListViewModel;
			ProjectTypeLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((ProjectTypeLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return ProjectTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectTypeLookup.Name)].Visibility; }
            set { ProjectTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectTypeLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return ProjectTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectTypeLookup.DisplayMember)].Visibility; }
            set { ProjectTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectTypeLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProjectTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectTypeLookup.Entity)].Visibility; }
            set { ProjectTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectTypeLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("RegionLookup")]
    public partial class RegionLookupListView : ViewBase
    {
        public RegionLookupListView()
        {
            InitializeComponent();
        }

        public RegionLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, RegionLookupListViewModel RegionLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = RegionLookupListViewModel;
			RegionLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((RegionLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.RegionLookup.Name)].Visibility; }
            set { RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.RegionLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.RegionLookup.DisplayMember)].Visibility; }
            set { RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.RegionLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility DistrictVisibility
        {
            get { return RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.RegionLookup.District)].Visibility; }
            set { RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.RegionLookup.District)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.RegionLookup.Entity)].Visibility; }
            set { RegionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.RegionLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("SalesBlockLookup")]
    public partial class SalesBlockLookupListView : ViewBase
    {
        public SalesBlockLookupListView()
        {
            InitializeComponent();
        }

        public SalesBlockLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesBlockLookupListViewModel SalesBlockLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SalesBlockLookupListViewModel;
			SalesBlockLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SalesBlockLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return SalesBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesBlockLookup.DisplayMember)].Visibility; }
            set { SalesBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesBlockLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return SalesBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesBlockLookup.Entity)].Visibility; }
            set { SalesBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesBlockLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("SalesUnitLookup")]
    public partial class SalesUnitLookupListView : ViewBase
    {
        public SalesUnitLookupListView()
        {
            InitializeComponent();
        }

        public SalesUnitLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesUnitLookupListViewModel SalesUnitLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SalesUnitLookupListViewModel;
			SalesUnitLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SalesUnitLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility CostVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Cost)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Cost)].Visibility = value; }
        }


        public System.Windows.Visibility ProductionTermVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ProductionTerm)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ProductionTerm)].Visibility = value; }
        }


        public System.Windows.Visibility DeliveryDateExpectedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DeliveryDateExpected)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DeliveryDateExpected)].Visibility = value; }
        }


        public System.Windows.Visibility RealizationDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.RealizationDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.RealizationDate)].Visibility = value; }
        }


        public System.Windows.Visibility OrderPositionVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.OrderPosition)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.OrderPosition)].Visibility = value; }
        }


        public System.Windows.Visibility SerialNumberVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SerialNumber)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SerialNumber)].Visibility = value; }
        }


        public System.Windows.Visibility AssembleTermVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.AssembleTerm)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.AssembleTerm)].Visibility = value; }
        }


        public System.Windows.Visibility SignalToStartProductionVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SignalToStartProduction)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SignalToStartProduction)].Visibility = value; }
        }


        public System.Windows.Visibility SignalToStartProductionDoneVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SignalToStartProductionDone)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SignalToStartProductionDone)].Visibility = value; }
        }


        public System.Windows.Visibility StartProductionDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.StartProductionDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.StartProductionDate)].Visibility = value; }
        }


        public System.Windows.Visibility PickingDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PickingDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PickingDate)].Visibility = value; }
        }


        public System.Windows.Visibility EndProductionPlanDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.EndProductionPlanDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.EndProductionPlanDate)].Visibility = value; }
        }


        public System.Windows.Visibility EndProductionDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.EndProductionDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.EndProductionDate)].Visibility = value; }
        }


        public System.Windows.Visibility ExpectedDeliveryPeriodVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ExpectedDeliveryPeriod)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ExpectedDeliveryPeriod)].Visibility = value; }
        }


        public System.Windows.Visibility ExpectedDeliveryPeriodCalculatedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ExpectedDeliveryPeriodCalculated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ExpectedDeliveryPeriodCalculated)].Visibility = value; }
        }


        public System.Windows.Visibility ShipmentDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ShipmentDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ShipmentDate)].Visibility = value; }
        }


        public System.Windows.Visibility ShipmentPlanDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ShipmentPlanDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ShipmentPlanDate)].Visibility = value; }
        }


        public System.Windows.Visibility DeliveryDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DeliveryDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DeliveryDate)].Visibility = value; }
        }


        public System.Windows.Visibility IsLoosenVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.IsLoosen)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.IsLoosen)].Visibility = value; }
        }


        public System.Windows.Visibility SumPaidVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SumPaid)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SumPaid)].Visibility = value; }
        }


        public System.Windows.Visibility SumNotPaidVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SumNotPaid)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SumNotPaid)].Visibility = value; }
        }


        public System.Windows.Visibility SumToStartProductionVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SumToStartProduction)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SumToStartProduction)].Visibility = value; }
        }


        public System.Windows.Visibility SumToShippingVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SumToShipping)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.SumToShipping)].Visibility = value; }
        }


        public System.Windows.Visibility OrderInTakeDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.OrderInTakeDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.OrderInTakeDate)].Visibility = value; }
        }


        public System.Windows.Visibility OrderInTakeYearVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.OrderInTakeYear)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.OrderInTakeYear)].Visibility = value; }
        }


        public System.Windows.Visibility OrderInTakeMonthVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.OrderInTakeMonth)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.OrderInTakeMonth)].Visibility = value; }
        }


        public System.Windows.Visibility StartProductionConditionsDoneDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.StartProductionConditionsDoneDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.StartProductionConditionsDoneDate)].Visibility = value; }
        }


        public System.Windows.Visibility ShippingConditionsDoneDateVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ShippingConditionsDoneDate)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ShippingConditionsDoneDate)].Visibility = value; }
        }


        public System.Windows.Visibility StartProductionDateCalculatedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.StartProductionDateCalculated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.StartProductionDateCalculated)].Visibility = value; }
        }


        public System.Windows.Visibility EndProductionDateCalculatedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.EndProductionDateCalculated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.EndProductionDateCalculated)].Visibility = value; }
        }


        public System.Windows.Visibility RealizationDateCalculatedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.RealizationDateCalculated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.RealizationDateCalculated)].Visibility = value; }
        }


        public System.Windows.Visibility ShipmentDateCalculatedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ShipmentDateCalculated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ShipmentDateCalculated)].Visibility = value; }
        }


        public System.Windows.Visibility DeliveryDateCalculatedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DeliveryDateCalculated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DeliveryDateCalculated)].Visibility = value; }
        }


        public System.Windows.Visibility DeliveryPeriodCalculatedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DeliveryPeriodCalculated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DeliveryPeriodCalculated)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DisplayMember)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Product)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Product)].Visibility = value; }
        }


        public System.Windows.Visibility FacilityVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Facility)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Facility)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentConditionSetVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentConditionSet)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentConditionSet)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Project)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Project)].Visibility = value; }
        }


        public System.Windows.Visibility ProducerVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Producer)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Producer)].Visibility = value; }
        }


        public System.Windows.Visibility OrderVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Order)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Order)].Visibility = value; }
        }


        public System.Windows.Visibility SpecificationVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Specification)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Specification)].Visibility = value; }
        }


        public System.Windows.Visibility AddressVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Address)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Address)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Entity)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentsActualVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsActual)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsActual)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentsPlannedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsPlanned)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsPlanned)].Visibility = value; }
        }


        public System.Windows.Visibility ProductsIncludedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ProductsIncluded)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ProductsIncluded)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("SpecificationLookup")]
    public partial class SpecificationLookupListView : ViewBase
    {
        public SpecificationLookupListView()
        {
            InitializeComponent();
        }

        public SpecificationLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SpecificationLookupListViewModel SpecificationLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SpecificationLookupListViewModel;
			SpecificationLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SpecificationLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Number)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Number)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Date)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility VatVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Vat)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Vat)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.DisplayMember)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ContractVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Contract)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Contract)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Entity)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("SumLookup")]
    public partial class SumLookupListView : ViewBase
    {
        public SumLookupListView()
        {
            InitializeComponent();
        }

        public SumLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SumLookupListViewModel SumLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SumLookupListViewModel;
			SumLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SumLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility TypeVisibility
        {
            get { return SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.Type)].Visibility; }
            set { SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.Type)].Visibility = value; }
        }


        public System.Windows.Visibility CurrencyVisibility
        {
            get { return SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.Currency)].Visibility; }
            set { SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.Currency)].Visibility = value; }
        }


        public System.Windows.Visibility ValueVisibility
        {
            get { return SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.Value)].Visibility; }
            set { SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.Value)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.DisplayMember)].Visibility; }
            set { SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.Entity)].Visibility; }
            set { SumLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("SumOnDateLookup")]
    public partial class SumOnDateLookupListView : ViewBase
    {
        public SumOnDateLookupListView()
        {
            InitializeComponent();
        }

        public SumOnDateLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, SumOnDateLookupListViewModel SumOnDateLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = SumOnDateLookupListViewModel;
			SumOnDateLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((SumOnDateLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DateVisibility
        {
            get { return SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumOnDateLookup.Date)].Visibility; }
            set { SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumOnDateLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility SumVisibility
        {
            get { return SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumOnDateLookup.Sum)].Visibility; }
            set { SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumOnDateLookup.Sum)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumOnDateLookup.DisplayMember)].Visibility; }
            set { SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumOnDateLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumOnDateLookup.Entity)].Visibility; }
            set { SumOnDateLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SumOnDateLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TenderLookup")]
    public partial class TenderLookupListView : ViewBase
    {
        public TenderLookupListView()
        {
            InitializeComponent();
        }

        public TenderLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderLookupListViewModel TenderLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderLookupListViewModel;
			TenderLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TenderLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility DateOpenVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.DateOpen)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.DateOpen)].Visibility = value; }
        }


        public System.Windows.Visibility DateCloseVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.DateClose)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.DateClose)].Visibility = value; }
        }


        public System.Windows.Visibility DateNoticeVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.DateNotice)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.DateNotice)].Visibility = value; }
        }


        public System.Windows.Visibility TypeVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Type)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Type)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.DisplayMember)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Project)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Project)].Visibility = value; }
        }


        public System.Windows.Visibility WinnerVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Winner)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Winner)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Entity)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TenderTypeLookup")]
    public partial class TenderTypeLookupListView : ViewBase
    {
        public TenderTypeLookupListView()
        {
            InitializeComponent();
        }

        public TenderTypeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderTypeLookupListViewModel TenderTypeLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TenderTypeLookupListViewModel;
			TenderTypeLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TenderTypeLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderTypeLookup.Name)].Visibility; }
            set { TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderTypeLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility TypeVisibility
        {
            get { return TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderTypeLookup.Type)].Visibility; }
            set { TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderTypeLookup.Type)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderTypeLookup.DisplayMember)].Visibility; }
            set { TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderTypeLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderTypeLookup.Entity)].Visibility; }
            set { TenderTypeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderTypeLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestChildLookup")]
    public partial class TestChildLookupListView : ViewBase
    {
        public TestChildLookupListView()
        {
            InitializeComponent();
        }

        public TestChildLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestChildLookupListViewModel TestChildLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestChildLookupListViewModel;
			TestChildLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestChildLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.Name)].Visibility; }
            set { TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.DisplayMember)].Visibility; }
            set { TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility HusbandVisibility
        {
            get { return TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.Husband)].Visibility; }
            set { TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.Husband)].Visibility = value; }
        }


        public System.Windows.Visibility WifeVisibility
        {
            get { return TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.Wife)].Visibility; }
            set { TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.Wife)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.Entity)].Visibility; }
            set { TestChildLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestChildLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestEntityLookup")]
    public partial class TestEntityLookupListView : ViewBase
    {
        public TestEntityLookupListView()
        {
            InitializeComponent();
        }

        public TestEntityLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestEntityLookupListViewModel TestEntityLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestEntityLookupListViewModel;
			TestEntityLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestEntityLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TestEntityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestEntityLookup.Name)].Visibility; }
            set { TestEntityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestEntityLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TestEntityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestEntityLookup.DisplayMember)].Visibility; }
            set { TestEntityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestEntityLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TestEntityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestEntityLookup.Entity)].Visibility; }
            set { TestEntityLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestEntityLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestFriendAddressLookup")]
    public partial class TestFriendAddressLookupListView : ViewBase
    {
        public TestFriendAddressLookupListView()
        {
            InitializeComponent();
        }

        public TestFriendAddressLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendAddressLookupListViewModel TestFriendAddressLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendAddressLookupListViewModel;
			TestFriendAddressLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestFriendAddressLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility CityVisibility
        {
            get { return TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.City)].Visibility; }
            set { TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.City)].Visibility = value; }
        }


        public System.Windows.Visibility StreetVisibility
        {
            get { return TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.Street)].Visibility; }
            set { TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.Street)].Visibility = value; }
        }


        public System.Windows.Visibility StreetNumberVisibility
        {
            get { return TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.StreetNumber)].Visibility; }
            set { TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.StreetNumber)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.DisplayMember)].Visibility; }
            set { TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.Entity)].Visibility; }
            set { TestFriendAddressLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendAddressLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestFriendEmailLookup")]
    public partial class TestFriendEmailLookupListView : ViewBase
    {
        public TestFriendEmailLookupListView()
        {
            InitializeComponent();
        }

        public TestFriendEmailLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendEmailLookupListViewModel TestFriendEmailLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendEmailLookupListViewModel;
			TestFriendEmailLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestFriendEmailLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility EmailVisibility
        {
            get { return TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendEmailLookup.Email)].Visibility; }
            set { TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendEmailLookup.Email)].Visibility = value; }
        }


        public System.Windows.Visibility CommentVisibility
        {
            get { return TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendEmailLookup.Comment)].Visibility; }
            set { TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendEmailLookup.Comment)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendEmailLookup.DisplayMember)].Visibility; }
            set { TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendEmailLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendEmailLookup.Entity)].Visibility; }
            set { TestFriendEmailLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendEmailLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestFriendGroupLookup")]
    public partial class TestFriendGroupLookupListView : ViewBase
    {
        public TestFriendGroupLookupListView()
        {
            InitializeComponent();
        }

        public TestFriendGroupLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendGroupLookupListViewModel TestFriendGroupLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendGroupLookupListViewModel;
			TestFriendGroupLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestFriendGroupLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TestFriendGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendGroupLookup.Name)].Visibility; }
            set { TestFriendGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendGroupLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TestFriendGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendGroupLookup.DisplayMember)].Visibility; }
            set { TestFriendGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendGroupLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TestFriendGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendGroupLookup.Entity)].Visibility; }
            set { TestFriendGroupLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendGroupLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestFriendLookup")]
    public partial class TestFriendLookupListView : ViewBase
    {
        public TestFriendLookupListView()
        {
            InitializeComponent();
        }

        public TestFriendLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendLookupListViewModel TestFriendLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestFriendLookupListViewModel;
			TestFriendLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestFriendLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility FriendGroupIdVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.FriendGroupId)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.FriendGroupId)].Visibility = value; }
        }


        public System.Windows.Visibility FirstNameVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.FirstName)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.FirstName)].Visibility = value; }
        }


        public System.Windows.Visibility LastNameVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.LastName)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.LastName)].Visibility = value; }
        }


        public System.Windows.Visibility BirthdayVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.Birthday)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.Birthday)].Visibility = value; }
        }


        public System.Windows.Visibility IsDeveloperVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.IsDeveloper)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.IsDeveloper)].Visibility = value; }
        }


        public System.Windows.Visibility IdGetVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.IdGet)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.IdGet)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.DisplayMember)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility TestFriendAddressVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.TestFriendAddress)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.TestFriendAddress)].Visibility = value; }
        }


        public System.Windows.Visibility TestFriendGroupVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.TestFriendGroup)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.TestFriendGroup)].Visibility = value; }
        }


        public System.Windows.Visibility TestFriendEmailGetVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.TestFriendEmailGet)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.TestFriendEmailGet)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.Entity)].Visibility; }
            set { TestFriendLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestFriendLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestHusbandLookup")]
    public partial class TestHusbandLookupListView : ViewBase
    {
        public TestHusbandLookupListView()
        {
            InitializeComponent();
        }

        public TestHusbandLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestHusbandLookupListViewModel TestHusbandLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestHusbandLookupListViewModel;
			TestHusbandLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestHusbandLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestHusbandLookup.Name)].Visibility; }
            set { TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestHusbandLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestHusbandLookup.DisplayMember)].Visibility; }
            set { TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestHusbandLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility WifeVisibility
        {
            get { return TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestHusbandLookup.Wife)].Visibility; }
            set { TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestHusbandLookup.Wife)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestHusbandLookup.Entity)].Visibility; }
            set { TestHusbandLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestHusbandLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("TestWifeLookup")]
    public partial class TestWifeLookupListView : ViewBase
    {
        public TestWifeLookupListView()
        {
            InitializeComponent();
        }

        public TestWifeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, TestWifeLookupListViewModel TestWifeLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = TestWifeLookupListViewModel;
			TestWifeLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((TestWifeLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NVisibility
        {
            get { return TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.N)].Visibility; }
            set { TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.N)].Visibility = value; }
        }


        public System.Windows.Visibility NameVisibility
        {
            get { return TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.Name)].Visibility; }
            set { TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.DisplayMember)].Visibility; }
            set { TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility HusbandVisibility
        {
            get { return TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.Husband)].Visibility; }
            set { TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.Husband)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.Entity)].Visibility; }
            set { TestWifeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TestWifeLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("UserLookup")]
    public partial class UserLookupListView : ViewBase
    {
        public UserLookupListView()
        {
            InitializeComponent();
        }

        public UserLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserLookupListViewModel UserLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserLookupListViewModel;
			UserLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((UserLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility LoginVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Login)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Login)].Visibility = value; }
        }


        public System.Windows.Visibility PasswordVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Password)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Password)].Visibility = value; }
        }


        public System.Windows.Visibility PersonalNumberVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.PersonalNumber)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.PersonalNumber)].Visibility = value; }
        }


        public System.Windows.Visibility RoleCurrentVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.RoleCurrent)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.RoleCurrent)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.DisplayMember)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EmployeeVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Employee)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Employee)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Entity)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD))]
	[DesignationPlural("UserRoleLookup")]
    public partial class UserRoleLookupListView : ViewBase
    {
        public UserRoleLookupListView()
        {
            InitializeComponent();
        }

        public UserRoleLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserRoleLookupListViewModel UserRoleLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserRoleLookupListViewModel;
			UserRoleLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((UserRoleLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserRoleLookup.Name)].Visibility; }
            set { UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserRoleLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility RoleVisibility
        {
            get { return UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserRoleLookup.Role)].Visibility; }
            set { UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserRoleLookup.Role)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserRoleLookup.DisplayMember)].Visibility; }
            set { UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserRoleLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserRoleLookup.Entity)].Visibility; }
            set { UserRoleLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserRoleLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


}
