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

    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Сфера деятельности")]
	[DesignationPlural("ActivityFieldLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Адрес")]
	[DesignationPlural("AddressLookup")]
	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Банковские реквизиты")]
	[DesignationPlural("BankDetailsLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Общие настройки")]
	[DesignationPlural("CommonOptionLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility VatVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.Vat)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.Vat)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.DisplayMember)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility OurCompanyVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.OurCompany)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.OurCompany)].Visibility = value; }
        }


        public System.Windows.Visibility StandartPaymentsConditionSetVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.StandartPaymentsConditionSet)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.StandartPaymentsConditionSet)].Visibility = value; }
        }


        public System.Windows.Visibility NewProductParameterVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.NewProductParameter)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.NewProductParameter)].Visibility = value; }
        }


        public System.Windows.Visibility NewProductParameterGroupVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.NewProductParameterGroup)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.NewProductParameterGroup)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.Entity)].Visibility; }
            set { CommonOptionLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CommonOptionLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Организационная форма")]
	[DesignationPlural("CompanyFormLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Компания")]
	[DesignationPlural("CompanyLookup")]
	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.DataBaseFiller)]
[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility BankDetailsListVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.BankDetailsList)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.BankDetailsList)].Visibility = value; }
        }


        public System.Windows.Visibility ActivityFildsVisibility
        {
            get { return CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.ActivityFilds)].Visibility; }
            set { CompanyLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CompanyLookup.ActivityFilds)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Контракт")]
	[DesignationPlural("ContractLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Страна")]
	[DesignationPlural("CountryLookup")]
	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Задание на создание нового продукта")]
	[DesignationPlural("CreateNewProductTaskLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility CommentVisibility
        {
            get { return CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.Comment)].Visibility; }
            set { CreateNewProductTaskLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.CreateNewProductTaskLookup.Comment)].Visibility = value; }
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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Курс обмена валют")]
	[DesignationPlural("CurrencyExchangeRateLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Округ")]
	[DesignationPlural("DistrictLookup")]
	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Документ")]
	[DesignationPlural("DocumentLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility CodeVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Code)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Code)].Visibility = value; }
        }


        public System.Windows.Visibility RegNumberVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RegNumber)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.RegNumber)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Date)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Date)].Visibility = value; }
        }


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


        public System.Windows.Visibility NumberVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Number)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.Number)].Visibility = value; }
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


        public System.Windows.Visibility CopyToRecipientsVisibility
        {
            get { return DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.CopyToRecipients)].Visibility; }
            set { DocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentLookup.CopyToRecipients)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Номер документа")]
	[DesignationPlural("DocumentNumberLookup")]
	[AllowEditAttribute(Role.Admin)]

    public partial class DocumentNumberLookupListView : ViewBase
    {
        public DocumentNumberLookupListView()
        {
            InitializeComponent();
        }

        public DocumentNumberLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentNumberLookupListViewModel DocumentNumberLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = DocumentNumberLookupListViewModel;
			DocumentNumberLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((DocumentNumberLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NumberVisibility
        {
            get { return DocumentNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentNumberLookup.Number)].Visibility; }
            set { DocumentNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentNumberLookup.Number)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return DocumentNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentNumberLookup.DisplayMember)].Visibility; }
            set { DocumentNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentNumberLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return DocumentNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentNumberLookup.Entity)].Visibility; }
            set { DocumentNumberLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentNumberLookup.Entity)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Регистрационные данные")]
	[DesignationPlural("DocumentsRegistrationDetailsLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility DateVisibility
        {
            get { return DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.Date)].Visibility; }
            set { DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.Date)].Visibility = value; }
        }


        public System.Windows.Visibility NumberVisibility
        {
            get { return DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.Number)].Visibility; }
            set { DocumentsRegistrationDetailsLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.DocumentsRegistrationDetailsLookup.Number)].Visibility = value; }
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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Сотрудник")]
	[DesignationPlural("EmployeeLookup")]
	[AllowEditAttribute(Role.DataBaseFiller)]
[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Должность")]
	[DesignationPlural("EmployeesPositionLookup")]
	[AllowEditAttribute(Role.DataBaseFiller)]
[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Economist)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Объект")]
	[DesignationPlural("FacilityLookup")]
	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Тип объекта")]
	[DesignationPlural("FacilityTypeLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Населенный пункт")]
	[DesignationPlural("LocalityLookup")]
	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Тип населенного пункта")]
	[DesignationPlural("LocalityTypeLookup")]
	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Область рынка")]
	[DesignationPlural("MarketFieldLookup")]
	[AllowEditAttribute(Role.Admin)]

    public partial class MarketFieldLookupListView : ViewBase
    {
        public MarketFieldLookupListView()
        {
            InitializeComponent();
        }

        public MarketFieldLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, MarketFieldLookupListViewModel MarketFieldLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = MarketFieldLookupListViewModel;
			MarketFieldLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((MarketFieldLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility NameVisibility
        {
            get { return MarketFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MarketFieldLookup.Name)].Visibility; }
            set { MarketFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MarketFieldLookup.Name)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return MarketFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MarketFieldLookup.DisplayMember)].Visibility; }
            set { MarketFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MarketFieldLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return MarketFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MarketFieldLookup.Entity)].Visibility; }
            set { MarketFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MarketFieldLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility ActivityFieldsVisibility
        {
            get { return MarketFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MarketFieldLookup.ActivityFields)].Visibility; }
            set { MarketFieldLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.MarketFieldLookup.ActivityFields)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Единица измерения")]
	[DesignationPlural("MeasureLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Заметка")]
	[DesignationPlural("NoteLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Предложение")]
	[DesignationPlural("OfferLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility CodeVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Code)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Code)].Visibility = value; }
        }


        public System.Windows.Visibility RegNumberVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RegNumber)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.RegNumber)].Visibility = value; }
        }


        public System.Windows.Visibility DateVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Date)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Date)].Visibility = value; }
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


        public System.Windows.Visibility NumberVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Number)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.Number)].Visibility = value; }
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


        public System.Windows.Visibility CopyToRecipientsVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.CopyToRecipients)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.CopyToRecipients)].Visibility = value; }
        }


        public System.Windows.Visibility OfferUnitsVisibility
        {
            get { return OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.OfferUnits)].Visibility; }
            set { OfferLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferLookup.OfferUnits)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Единица ТКП")]
	[DesignationPlural("OfferUnitLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility FacilityVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Facility)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Facility)].Visibility = value; }
        }


        public System.Windows.Visibility ProductVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Product)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.Product)].Visibility = value; }
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


        public System.Windows.Visibility ProductsIncludedVisibility
        {
            get { return OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.ProductsIncluded)].Visibility; }
            set { OfferUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.OfferUnitLookup.ProductsIncluded)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Заводской заказ")]
	[DesignationPlural("OrderLookup")]
	[AllowEditAttribute(Role.PlanMaker)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Группа параметров")]
	[DesignationPlural("ParameterGroupLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Параметр")]
	[DesignationPlural("ParameterLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility ParameterRelationsVisibility
        {
            get { return ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.ParameterRelations)].Visibility; }
            set { ParameterLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterLookup.ParameterRelations)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Ограничение использования параметра")]
	[DesignationPlural("ParameterRelationLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility RequiredParametersVisibility
        {
            get { return ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterRelationLookup.RequiredParameters)].Visibility; }
            set { ParameterRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ParameterRelationLookup.RequiredParameters)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Платеж совершенный")]
	[DesignationPlural("PaymentActualLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Условие платежа")]
	[DesignationPlural("PaymentConditionLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Условия оплаты")]
	[DesignationPlural("PaymentConditionSetLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Платежный документ")]
	[DesignationPlural("PaymentDocumentLookup")]
	[AllowEditAttribute(Role.Economist)]
[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility VatVisibility
        {
            get { return PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Vat)].Visibility; }
            set { PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Vat)].Visibility = value; }
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


        public System.Windows.Visibility PaymentsVisibility
        {
            get { return PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Payments)].Visibility; }
            set { PaymentDocumentLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.PaymentDocumentLookup.Payments)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Платеж плановый")]
	[DesignationPlural("PaymentPlannedLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Персона")]
	[DesignationPlural("PersonLookup")]
	[AllowEditAttribute(Role.DataBaseFiller)]
[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Economist)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Признаки услуги")]
	[DesignationPlural("ProductBlockIsServiceLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility ParametersVisibility
        {
            get { return ProductBlockIsServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockIsServiceLookup.Parameters)].Visibility; }
            set { ProductBlockIsServiceLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockIsServiceLookup.Parameters)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Блок")]
	[DesignationPlural("ProductBlockLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility DesignVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Design)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Design)].Visibility = value; }
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


        public System.Windows.Visibility ParametersVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Parameters)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Parameters)].Visibility = value; }
        }


        public System.Windows.Visibility PricesVisibility
        {
            get { return ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Prices)].Visibility; }
            set { ProductBlockLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductBlockLookup.Prices)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Зависимое оборудование")]
	[DesignationPlural("ProductDependentLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Обозначение продукта")]
	[DesignationPlural("ProductDesignationLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility ParametersVisibility
        {
            get { return ProductDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDesignationLookup.Parameters)].Visibility; }
            set { ProductDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductDesignationLookup.Parameters)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Включенное в стоимость оборудование")]
	[DesignationPlural("ProductIncludedLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Продукт")]
	[DesignationPlural("ProductLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility DependentProductsVisibility
        {
            get { return ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.DependentProducts)].Visibility; }
            set { ProductLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductLookup.DependentProducts)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Связи продуктов")]
	[DesignationPlural("ProductRelationLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility NameVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.Name)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.Name)].Visibility = value; }
        }


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


        public System.Windows.Visibility ParentProductParametersVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.ParentProductParameters)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.ParentProductParameters)].Visibility = value; }
        }


        public System.Windows.Visibility ChildProductParametersVisibility
        {
            get { return ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.ChildProductParameters)].Visibility; }
            set { ProductRelationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductRelationLookup.ChildProductParameters)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Обозначение типа продукта")]
	[DesignationPlural("ProductTypeDesignationLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility ParametersVisibility
        {
            get { return ProductTypeDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeDesignationLookup.Parameters)].Visibility; }
            set { ProductTypeDesignationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProductTypeDesignationLookup.Parameters)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Тип продукта")]
	[DesignationPlural("ProductTypeLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Проект")]
	[DesignationPlural("ProjectLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility InWorkVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.InWork)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.InWork)].Visibility = value; }
        }


        public System.Windows.Visibility ForReportVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.ForReport)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.ForReport)].Visibility = value; }
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


        public System.Windows.Visibility BuilderVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Builder)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Builder)].Visibility = value; }
        }


        public System.Windows.Visibility ProjectMakerVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.ProjectMaker)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.ProjectMaker)].Visibility = value; }
        }


        public System.Windows.Visibility SypplierVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Sypplier)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Sypplier)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Entity)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility NotesVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Notes)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Notes)].Visibility = value; }
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


        public System.Windows.Visibility FacilitiesVisibility
        {
            get { return ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Facilities)].Visibility; }
            set { ProjectLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.ProjectLookup.Facilities)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Тип проекта")]
	[DesignationPlural("ProjectTypeLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Регион")]
	[DesignationPlural("RegionLookup")]
	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Единица продаж")]
	[DesignationPlural("SalesUnitLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility AllowEditCostVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.AllowEditCost)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.AllowEditCost)].Visibility = value; }
        }


        public System.Windows.Visibility AllowEditProductVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.AllowEditProduct)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.AllowEditProduct)].Visibility = value; }
        }


        public System.Windows.Visibility IsLoosenVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.IsLoosen)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.IsLoosen)].Visibility = value; }
        }


        public System.Windows.Visibility IsDoneVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.IsDone)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.IsDone)].Visibility = value; }
        }


        public System.Windows.Visibility IsPaidVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.IsPaid)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.IsPaid)].Visibility = value; }
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


        public System.Windows.Visibility ProductsIncludedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ProductsIncluded)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.ProductsIncluded)].Visibility = value; }
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


        public System.Windows.Visibility PaymentsPlannedActualVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsPlannedActual)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsPlannedActual)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentsPlannedGeneratedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsPlannedGenerated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsPlannedGenerated)].Visibility = value; }
        }


        public System.Windows.Visibility PaymentsPlannedCalculatedVisibility
        {
            get { return SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsPlannedCalculated)].Visibility; }
            set { SalesUnitLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SalesUnitLookup.PaymentsPlannedCalculated)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Спецификация")]
	[DesignationPlural("SpecificationLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility UnitsVisibility
        {
            get { return SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Units)].Visibility; }
            set { SpecificationLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.SpecificationLookup.Units)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Стандартный маржинальный доход")]
	[DesignationPlural("StandartMarginalIncomeLookup")]
	[AllowEditAttribute(Role.Admin)]

    public partial class StandartMarginalIncomeLookupListView : ViewBase
    {
        public StandartMarginalIncomeLookupListView()
        {
            InitializeComponent();
        }

        public StandartMarginalIncomeLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, StandartMarginalIncomeLookupListViewModel StandartMarginalIncomeLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = StandartMarginalIncomeLookupListViewModel;
			StandartMarginalIncomeLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((StandartMarginalIncomeLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility MarginalIncomeVisibility
        {
            get { return StandartMarginalIncomeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartMarginalIncomeLookup.MarginalIncome)].Visibility; }
            set { StandartMarginalIncomeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartMarginalIncomeLookup.MarginalIncome)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return StandartMarginalIncomeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartMarginalIncomeLookup.DisplayMember)].Visibility; }
            set { StandartMarginalIncomeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartMarginalIncomeLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return StandartMarginalIncomeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartMarginalIncomeLookup.Entity)].Visibility; }
            set { StandartMarginalIncomeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartMarginalIncomeLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility ParametersVisibility
        {
            get { return StandartMarginalIncomeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartMarginalIncomeLookup.Parameters)].Visibility; }
            set { StandartMarginalIncomeLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartMarginalIncomeLookup.Parameters)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Стандартный срок производства")]
	[DesignationPlural("StandartProductionTermLookup")]
	[AllowEditAttribute(Role.Admin)]

    public partial class StandartProductionTermLookupListView : ViewBase
    {
        public StandartProductionTermLookupListView()
        {
            InitializeComponent();
        }

        public StandartProductionTermLookupListView(IRegionManager regionManager, IEventAggregator eventAggregator, StandartProductionTermLookupListViewModel StandartProductionTermLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = StandartProductionTermLookupListViewModel;
			StandartProductionTermLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }
		        
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
			await ((StandartProductionTermLookupListViewModel)DataContext).LoadAsync();;
        }

		#region VisibilityProps


        public System.Windows.Visibility ProductionTermVisibility
        {
            get { return StandartProductionTermLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartProductionTermLookup.ProductionTerm)].Visibility; }
            set { StandartProductionTermLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartProductionTermLookup.ProductionTerm)].Visibility = value; }
        }


        public System.Windows.Visibility DisplayMemberVisibility
        {
            get { return StandartProductionTermLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartProductionTermLookup.DisplayMember)].Visibility; }
            set { StandartProductionTermLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartProductionTermLookup.DisplayMember)].Visibility = value; }
        }


        public System.Windows.Visibility EntityVisibility
        {
            get { return StandartProductionTermLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartProductionTermLookup.Entity)].Visibility; }
            set { StandartProductionTermLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartProductionTermLookup.Entity)].Visibility = value; }
        }


        public System.Windows.Visibility ParametersVisibility
        {
            get { return StandartProductionTermLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartProductionTermLookup.Parameters)].Visibility; }
            set { StandartProductionTermLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.StandartProductionTermLookup.Parameters)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Сумма (фэйк)")]
	[DesignationPlural("SumLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Сумма на дату")]
	[DesignationPlural("SumOnDateLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Конкурс")]
	[DesignationPlural("TenderLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility TypesVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Types)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Types)].Visibility = value; }
        }


        public System.Windows.Visibility ParticipantsVisibility
        {
            get { return TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Participants)].Visibility; }
            set { TenderLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.TenderLookup.Participants)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Тип тендера")]
	[DesignationPlural("TenderTypeLookup")]
	[AllowEditAttribute(Role.Admin)]

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


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Пользователь")]
	[DesignationPlural("UserLookup")]
	[AllowEditAttribute(Role.Admin)]

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


        public System.Windows.Visibility RolesVisibility
        {
            get { return UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Roles)].Visibility; }
            set { UserLookupListGrid.FieldLayouts[0].Fields[nameof(HVTApp.UI.Lookup.UserLookup.Roles)].Visibility = value; }
        }



		#endregion
    }


    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh))]
	[Designation("Роль пользователя")]
	[DesignationPlural("UserRoleLookup")]
	[AllowEditAttribute(Role.Admin)]

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
