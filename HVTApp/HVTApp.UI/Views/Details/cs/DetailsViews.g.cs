using HVTApp.Infrastructure;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;
using HVTApp.UI.ViewModels;
using System.Windows;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using System.Reflection;
using System.Linq;

namespace HVTApp.UI.Views
{
    public partial class CountryUnionDetailsView : ViewBase
    {
        public CountryUnionDetailsView()
        {
			InitializeComponent();
        }

        public CountryUnionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CountryUnionDetailsViewModel CountryUnionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CountryUnionDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.CountryUnion).GetProperty(nameof(HVTApp.Model.POCOs.CountryUnion.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameCountryUnion = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CountryUnion).GetProperty(nameof(HVTApp.Model.POCOs.CountryUnion.Countries)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCountriesCountryUnion = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameCountryUnionProperty = DependencyProperty.Register("VisibilityNameCountryUnion", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameCountryUnion
        {
            get { return (Visibility) GetValue(VisibilityNameCountryUnionProperty); }
            set { SetValue(VisibilityNameCountryUnionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCountriesCountryUnionProperty = DependencyProperty.Register("VisibilityCountriesCountryUnion", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCountriesCountryUnion
        {
            get { return (Visibility) GetValue(VisibilityCountriesCountryUnionProperty); }
            set { SetValue(VisibilityCountriesCountryUnionProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class BankGuaranteeDetailsView : ViewBase
    {
        public BankGuaranteeDetailsView()
        {
			InitializeComponent();
        }

        public BankGuaranteeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, BankGuaranteeDetailsViewModel BankGuaranteeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = BankGuaranteeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.BankGuarantee).GetProperty(nameof(HVTApp.Model.POCOs.BankGuarantee.BankGuaranteeType)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityBankGuaranteeTypeBankGuarantee = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BankGuarantee).GetProperty(nameof(HVTApp.Model.POCOs.BankGuarantee.Percent)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPercentBankGuarantee = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BankGuarantee).GetProperty(nameof(HVTApp.Model.POCOs.BankGuarantee.Days)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDaysBankGuarantee = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityBankGuaranteeTypeBankGuaranteeProperty = DependencyProperty.Register("VisibilityBankGuaranteeTypeBankGuarantee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBankGuaranteeTypeBankGuarantee
        {
            get { return (Visibility) GetValue(VisibilityBankGuaranteeTypeBankGuaranteeProperty); }
            set { SetValue(VisibilityBankGuaranteeTypeBankGuaranteeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPercentBankGuaranteeProperty = DependencyProperty.Register("VisibilityPercentBankGuarantee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPercentBankGuarantee
        {
            get { return (Visibility) GetValue(VisibilityPercentBankGuaranteeProperty); }
            set { SetValue(VisibilityPercentBankGuaranteeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDaysBankGuaranteeProperty = DependencyProperty.Register("VisibilityDaysBankGuarantee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDaysBankGuarantee
        {
            get { return (Visibility) GetValue(VisibilityDaysBankGuaranteeProperty); }
            set { SetValue(VisibilityDaysBankGuaranteeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class BankGuaranteeTypeDetailsView : ViewBase
    {
        public BankGuaranteeTypeDetailsView()
        {
			InitializeComponent();
        }

        public BankGuaranteeTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, BankGuaranteeTypeDetailsViewModel BankGuaranteeTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = BankGuaranteeTypeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.BankGuaranteeType).GetProperty(nameof(HVTApp.Model.POCOs.BankGuaranteeType.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameBankGuaranteeType = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameBankGuaranteeTypeProperty = DependencyProperty.Register("VisibilityNameBankGuaranteeType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameBankGuaranteeType
        {
            get { return (Visibility) GetValue(VisibilityNameBankGuaranteeTypeProperty); }
            set { SetValue(VisibilityNameBankGuaranteeTypeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class BudgetDetailsView : ViewBase
    {
        public BudgetDetailsView()
        {
			InitializeComponent();
        }

        public BudgetDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, BudgetDetailsViewModel BudgetDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = BudgetDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Budget).GetProperty(nameof(HVTApp.Model.POCOs.Budget.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateBudget = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Budget).GetProperty(nameof(HVTApp.Model.POCOs.Budget.DateStart)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateStartBudget = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Budget).GetProperty(nameof(HVTApp.Model.POCOs.Budget.DateFinish)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateFinishBudget = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Budget).GetProperty(nameof(HVTApp.Model.POCOs.Budget.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameBudget = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Budget).GetProperty(nameof(HVTApp.Model.POCOs.Budget.Units)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityUnitsBudget = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateBudgetProperty = DependencyProperty.Register("VisibilityDateBudget", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateBudget
        {
            get { return (Visibility) GetValue(VisibilityDateBudgetProperty); }
            set { SetValue(VisibilityDateBudgetProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateStartBudgetProperty = DependencyProperty.Register("VisibilityDateStartBudget", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateStartBudget
        {
            get { return (Visibility) GetValue(VisibilityDateStartBudgetProperty); }
            set { SetValue(VisibilityDateStartBudgetProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateFinishBudgetProperty = DependencyProperty.Register("VisibilityDateFinishBudget", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateFinishBudget
        {
            get { return (Visibility) GetValue(VisibilityDateFinishBudgetProperty); }
            set { SetValue(VisibilityDateFinishBudgetProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNameBudgetProperty = DependencyProperty.Register("VisibilityNameBudget", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameBudget
        {
            get { return (Visibility) GetValue(VisibilityNameBudgetProperty); }
            set { SetValue(VisibilityNameBudgetProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityUnitsBudgetProperty = DependencyProperty.Register("VisibilityUnitsBudget", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityUnitsBudget
        {
            get { return (Visibility) GetValue(VisibilityUnitsBudgetProperty); }
            set { SetValue(VisibilityUnitsBudgetProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class BudgetUnitDetailsView : ViewBase
    {
        public BudgetUnitDetailsView()
        {
			InitializeComponent();
        }

        public BudgetUnitDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, BudgetUnitDetailsViewModel BudgetUnitDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = BudgetUnitDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.SalesUnit)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySalesUnitBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.OrderInTakeDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderInTakeDateBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.RealizationDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRealizationDateBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.OrderInTakeDateByManager)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderInTakeDateByManagerBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.RealizationDateByManager)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRealizationDateByManagerBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.Cost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.CostByManager)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostByManagerBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionSetBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.PaymentConditionSetByManager)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionSetByManagerBudgetUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BudgetUnit).GetProperty(nameof(HVTApp.Model.POCOs.BudgetUnit.IsRemoved)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsRemovedBudgetUnit = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilitySalesUnitBudgetUnitProperty = DependencyProperty.Register("VisibilitySalesUnitBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySalesUnitBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilitySalesUnitBudgetUnitProperty); }
            set { SetValue(VisibilitySalesUnitBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderInTakeDateBudgetUnitProperty = DependencyProperty.Register("VisibilityOrderInTakeDateBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeDateBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderInTakeDateBudgetUnitProperty); }
            set { SetValue(VisibilityOrderInTakeDateBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRealizationDateBudgetUnitProperty = DependencyProperty.Register("VisibilityRealizationDateBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRealizationDateBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityRealizationDateBudgetUnitProperty); }
            set { SetValue(VisibilityRealizationDateBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderInTakeDateByManagerBudgetUnitProperty = DependencyProperty.Register("VisibilityOrderInTakeDateByManagerBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeDateByManagerBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderInTakeDateByManagerBudgetUnitProperty); }
            set { SetValue(VisibilityOrderInTakeDateByManagerBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRealizationDateByManagerBudgetUnitProperty = DependencyProperty.Register("VisibilityRealizationDateByManagerBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRealizationDateByManagerBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityRealizationDateByManagerBudgetUnitProperty); }
            set { SetValue(VisibilityRealizationDateByManagerBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCostBudgetUnitProperty = DependencyProperty.Register("VisibilityCostBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityCostBudgetUnitProperty); }
            set { SetValue(VisibilityCostBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCostByManagerBudgetUnitProperty = DependencyProperty.Register("VisibilityCostByManagerBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostByManagerBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityCostByManagerBudgetUnitProperty); }
            set { SetValue(VisibilityCostByManagerBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentConditionSetBudgetUnitProperty = DependencyProperty.Register("VisibilityPaymentConditionSetBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionSetBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionSetBudgetUnitProperty); }
            set { SetValue(VisibilityPaymentConditionSetBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentConditionSetByManagerBudgetUnitProperty = DependencyProperty.Register("VisibilityPaymentConditionSetByManagerBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionSetByManagerBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionSetByManagerBudgetUnitProperty); }
            set { SetValue(VisibilityPaymentConditionSetByManagerBudgetUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsRemovedBudgetUnitProperty = DependencyProperty.Register("VisibilityIsRemovedBudgetUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsRemovedBudgetUnit
        {
            get { return (Visibility) GetValue(VisibilityIsRemovedBudgetUnitProperty); }
            set { SetValue(VisibilityIsRemovedBudgetUnitProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ConstructorParametersListDetailsView : ViewBase
    {
        public ConstructorParametersListDetailsView()
        {
			InitializeComponent();
        }

        public ConstructorParametersListDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ConstructorParametersListDetailsViewModel ConstructorParametersListDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ConstructorParametersListDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ConstructorParametersList).GetProperty(nameof(HVTApp.Model.POCOs.ConstructorParametersList.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameConstructorParametersList = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ConstructorParametersList).GetProperty(nameof(HVTApp.Model.POCOs.ConstructorParametersList.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersConstructorParametersList = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameConstructorParametersListProperty = DependencyProperty.Register("VisibilityNameConstructorParametersList", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameConstructorParametersList
        {
            get { return (Visibility) GetValue(VisibilityNameConstructorParametersListProperty); }
            set { SetValue(VisibilityNameConstructorParametersListProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParametersConstructorParametersListProperty = DependencyProperty.Register("VisibilityParametersConstructorParametersList", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersConstructorParametersList
        {
            get { return (Visibility) GetValue(VisibilityParametersConstructorParametersListProperty); }
            set { SetValue(VisibilityParametersConstructorParametersListProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ConstructorsParametersDetailsView : ViewBase
    {
        public ConstructorsParametersDetailsView()
        {
			InitializeComponent();
        }

        public ConstructorsParametersDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ConstructorsParametersDetailsViewModel ConstructorsParametersDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ConstructorsParametersDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ConstructorsParameters).GetProperty(nameof(HVTApp.Model.POCOs.ConstructorsParameters.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameConstructorsParameters = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ConstructorsParameters).GetProperty(nameof(HVTApp.Model.POCOs.ConstructorsParameters.Constructors)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityConstructorsConstructorsParameters = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ConstructorsParameters).GetProperty(nameof(HVTApp.Model.POCOs.ConstructorsParameters.PatametersLists)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPatametersListsConstructorsParameters = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameConstructorsParametersProperty = DependencyProperty.Register("VisibilityNameConstructorsParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameConstructorsParameters
        {
            get { return (Visibility) GetValue(VisibilityNameConstructorsParametersProperty); }
            set { SetValue(VisibilityNameConstructorsParametersProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityConstructorsConstructorsParametersProperty = DependencyProperty.Register("VisibilityConstructorsConstructorsParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityConstructorsConstructorsParameters
        {
            get { return (Visibility) GetValue(VisibilityConstructorsConstructorsParametersProperty); }
            set { SetValue(VisibilityConstructorsConstructorsParametersProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPatametersListsConstructorsParametersProperty = DependencyProperty.Register("VisibilityPatametersListsConstructorsParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPatametersListsConstructorsParameters
        {
            get { return (Visibility) GetValue(VisibilityPatametersListsConstructorsParametersProperty); }
            set { SetValue(VisibilityPatametersListsConstructorsParametersProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class CostsPercentsDetailsView : ViewBase
    {
        public CostsPercentsDetailsView()
        {
			InitializeComponent();
        }

        public CostsPercentsDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CostsPercentsDetailsViewModel CostsPercentsDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CostsPercentsDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.CostsPercents).GetProperty(nameof(HVTApp.Model.POCOs.CostsPercents.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateCostsPercents = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CostsPercents).GetProperty(nameof(HVTApp.Model.POCOs.CostsPercents.ManagmentCosts)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityManagmentCostsCostsPercents = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CostsPercents).GetProperty(nameof(HVTApp.Model.POCOs.CostsPercents.EconomicCosts)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEconomicCostsCostsPercents = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CostsPercents).GetProperty(nameof(HVTApp.Model.POCOs.CostsPercents.CommercialCosts)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommercialCostsCostsPercents = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateCostsPercentsProperty = DependencyProperty.Register("VisibilityDateCostsPercents", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateCostsPercents
        {
            get { return (Visibility) GetValue(VisibilityDateCostsPercentsProperty); }
            set { SetValue(VisibilityDateCostsPercentsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityManagmentCostsCostsPercentsProperty = DependencyProperty.Register("VisibilityManagmentCostsCostsPercents", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityManagmentCostsCostsPercents
        {
            get { return (Visibility) GetValue(VisibilityManagmentCostsCostsPercentsProperty); }
            set { SetValue(VisibilityManagmentCostsCostsPercentsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityEconomicCostsCostsPercentsProperty = DependencyProperty.Register("VisibilityEconomicCostsCostsPercents", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEconomicCostsCostsPercents
        {
            get { return (Visibility) GetValue(VisibilityEconomicCostsCostsPercentsProperty); }
            set { SetValue(VisibilityEconomicCostsCostsPercentsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommercialCostsCostsPercentsProperty = DependencyProperty.Register("VisibilityCommercialCostsCostsPercents", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommercialCostsCostsPercents
        {
            get { return (Visibility) GetValue(VisibilityCommercialCostsCostsPercentsProperty); }
            set { SetValue(VisibilityCommercialCostsCostsPercentsProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class CreateNewProductTaskDetailsView : ViewBase
    {
        public CreateNewProductTaskDetailsView()
        {
			InitializeComponent();
        }

        public CreateNewProductTaskDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CreateNewProductTaskDetailsViewModel CreateNewProductTaskDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CreateNewProductTaskDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.Designation)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesignationCreateNewProductTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.StructureCostNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStructureCostNumberCreateNewProductTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentCreateNewProductTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.Product)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductCreateNewProductTask = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDesignationCreateNewProductTaskProperty = DependencyProperty.Register("VisibilityDesignationCreateNewProductTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignationCreateNewProductTask
        {
            get { return (Visibility) GetValue(VisibilityDesignationCreateNewProductTaskProperty); }
            set { SetValue(VisibilityDesignationCreateNewProductTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStructureCostNumberCreateNewProductTaskProperty = DependencyProperty.Register("VisibilityStructureCostNumberCreateNewProductTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStructureCostNumberCreateNewProductTask
        {
            get { return (Visibility) GetValue(VisibilityStructureCostNumberCreateNewProductTaskProperty); }
            set { SetValue(VisibilityStructureCostNumberCreateNewProductTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentCreateNewProductTaskProperty = DependencyProperty.Register("VisibilityCommentCreateNewProductTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentCreateNewProductTask
        {
            get { return (Visibility) GetValue(VisibilityCommentCreateNewProductTaskProperty); }
            set { SetValue(VisibilityCommentCreateNewProductTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductCreateNewProductTaskProperty = DependencyProperty.Register("VisibilityProductCreateNewProductTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductCreateNewProductTask
        {
            get { return (Visibility) GetValue(VisibilityProductCreateNewProductTaskProperty); }
            set { SetValue(VisibilityProductCreateNewProductTaskProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class DirectumTaskDetailsView : ViewBase
    {
        public DirectumTaskDetailsView()
        {
			InitializeComponent();
        }

        public DirectumTaskDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DirectumTaskDetailsViewModel DirectumTaskDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DirectumTaskDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.Group)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityGroupDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.Performer)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPerformerDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.StartPerformer)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartPerformerDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.FinishPlan)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFinishPlanDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.FinishPerformer)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFinishPerformerDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.FinishAuthor)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFinishAuthorDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.Messages)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMessagesDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.ParentTask)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParentTaskDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.PreviousTask)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPreviousTaskDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.Childs)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityChildsDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.Parallel)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParallelDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.Next)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNextDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.StartResult)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartResultDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.Status)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStatusDirectumTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTask).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTask.IsActual)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsActualDirectumTask = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityGroupDirectumTaskProperty = DependencyProperty.Register("VisibilityGroupDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityGroupDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityGroupDirectumTaskProperty); }
            set { SetValue(VisibilityGroupDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPerformerDirectumTaskProperty = DependencyProperty.Register("VisibilityPerformerDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPerformerDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityPerformerDirectumTaskProperty); }
            set { SetValue(VisibilityPerformerDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStartPerformerDirectumTaskProperty = DependencyProperty.Register("VisibilityStartPerformerDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartPerformerDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityStartPerformerDirectumTaskProperty); }
            set { SetValue(VisibilityStartPerformerDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFinishPlanDirectumTaskProperty = DependencyProperty.Register("VisibilityFinishPlanDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFinishPlanDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityFinishPlanDirectumTaskProperty); }
            set { SetValue(VisibilityFinishPlanDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFinishPerformerDirectumTaskProperty = DependencyProperty.Register("VisibilityFinishPerformerDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFinishPerformerDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityFinishPerformerDirectumTaskProperty); }
            set { SetValue(VisibilityFinishPerformerDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFinishAuthorDirectumTaskProperty = DependencyProperty.Register("VisibilityFinishAuthorDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFinishAuthorDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityFinishAuthorDirectumTaskProperty); }
            set { SetValue(VisibilityFinishAuthorDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityMessagesDirectumTaskProperty = DependencyProperty.Register("VisibilityMessagesDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMessagesDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityMessagesDirectumTaskProperty); }
            set { SetValue(VisibilityMessagesDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParentTaskDirectumTaskProperty = DependencyProperty.Register("VisibilityParentTaskDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentTaskDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityParentTaskDirectumTaskProperty); }
            set { SetValue(VisibilityParentTaskDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPreviousTaskDirectumTaskProperty = DependencyProperty.Register("VisibilityPreviousTaskDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPreviousTaskDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityPreviousTaskDirectumTaskProperty); }
            set { SetValue(VisibilityPreviousTaskDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityChildsDirectumTaskProperty = DependencyProperty.Register("VisibilityChildsDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildsDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityChildsDirectumTaskProperty); }
            set { SetValue(VisibilityChildsDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParallelDirectumTaskProperty = DependencyProperty.Register("VisibilityParallelDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParallelDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityParallelDirectumTaskProperty); }
            set { SetValue(VisibilityParallelDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNextDirectumTaskProperty = DependencyProperty.Register("VisibilityNextDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNextDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityNextDirectumTaskProperty); }
            set { SetValue(VisibilityNextDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStartResultDirectumTaskProperty = DependencyProperty.Register("VisibilityStartResultDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartResultDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityStartResultDirectumTaskProperty); }
            set { SetValue(VisibilityStartResultDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStatusDirectumTaskProperty = DependencyProperty.Register("VisibilityStatusDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStatusDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityStatusDirectumTaskProperty); }
            set { SetValue(VisibilityStatusDirectumTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsActualDirectumTaskProperty = DependencyProperty.Register("VisibilityIsActualDirectumTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsActualDirectumTask
        {
            get { return (Visibility) GetValue(VisibilityIsActualDirectumTaskProperty); }
            set { SetValue(VisibilityIsActualDirectumTaskProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class DirectumTaskGroupDetailsView : ViewBase
    {
        public DirectumTaskGroupDetailsView()
        {
			InitializeComponent();
        }

        public DirectumTaskGroupDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DirectumTaskGroupDetailsViewModel DirectumTaskGroupDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DirectumTaskGroupDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroup).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroup.Title)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTitleDirectumTaskGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroup).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroup.Author)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAuthorDirectumTaskGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroup).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroup.StartAuthor)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartAuthorDirectumTaskGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroup).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroup.IsStoped)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsStopedDirectumTaskGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroup).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroup.Observers)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityObserversDirectumTaskGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroup).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroup.Priority)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriorityDirectumTaskGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroup).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroup.Message)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMessageDirectumTaskGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroup).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroup.Files)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFilesDirectumTaskGroup = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityTitleDirectumTaskGroupProperty = DependencyProperty.Register("VisibilityTitleDirectumTaskGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTitleDirectumTaskGroup
        {
            get { return (Visibility) GetValue(VisibilityTitleDirectumTaskGroupProperty); }
            set { SetValue(VisibilityTitleDirectumTaskGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAuthorDirectumTaskGroupProperty = DependencyProperty.Register("VisibilityAuthorDirectumTaskGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAuthorDirectumTaskGroup
        {
            get { return (Visibility) GetValue(VisibilityAuthorDirectumTaskGroupProperty); }
            set { SetValue(VisibilityAuthorDirectumTaskGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStartAuthorDirectumTaskGroupProperty = DependencyProperty.Register("VisibilityStartAuthorDirectumTaskGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartAuthorDirectumTaskGroup
        {
            get { return (Visibility) GetValue(VisibilityStartAuthorDirectumTaskGroupProperty); }
            set { SetValue(VisibilityStartAuthorDirectumTaskGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsStopedDirectumTaskGroupProperty = DependencyProperty.Register("VisibilityIsStopedDirectumTaskGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsStopedDirectumTaskGroup
        {
            get { return (Visibility) GetValue(VisibilityIsStopedDirectumTaskGroupProperty); }
            set { SetValue(VisibilityIsStopedDirectumTaskGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityObserversDirectumTaskGroupProperty = DependencyProperty.Register("VisibilityObserversDirectumTaskGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityObserversDirectumTaskGroup
        {
            get { return (Visibility) GetValue(VisibilityObserversDirectumTaskGroupProperty); }
            set { SetValue(VisibilityObserversDirectumTaskGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPriorityDirectumTaskGroupProperty = DependencyProperty.Register("VisibilityPriorityDirectumTaskGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriorityDirectumTaskGroup
        {
            get { return (Visibility) GetValue(VisibilityPriorityDirectumTaskGroupProperty); }
            set { SetValue(VisibilityPriorityDirectumTaskGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityMessageDirectumTaskGroupProperty = DependencyProperty.Register("VisibilityMessageDirectumTaskGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMessageDirectumTaskGroup
        {
            get { return (Visibility) GetValue(VisibilityMessageDirectumTaskGroupProperty); }
            set { SetValue(VisibilityMessageDirectumTaskGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFilesDirectumTaskGroupProperty = DependencyProperty.Register("VisibilityFilesDirectumTaskGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFilesDirectumTaskGroup
        {
            get { return (Visibility) GetValue(VisibilityFilesDirectumTaskGroupProperty); }
            set { SetValue(VisibilityFilesDirectumTaskGroupProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class DirectumTaskGroupFileDetailsView : ViewBase
    {
        public DirectumTaskGroupFileDetailsView()
        {
			InitializeComponent();
        }

        public DirectumTaskGroupFileDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DirectumTaskGroupFileDetailsViewModel DirectumTaskGroupFileDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DirectumTaskGroupFileDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroupFile).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroupFile.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameDirectumTaskGroupFile = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroupFile).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroupFile.LoadMoment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLoadMomentDirectumTaskGroupFile = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroupFile).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroupFile.Author)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAuthorDirectumTaskGroupFile = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskGroupFile).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskGroupFile.DirectumTaskGroupId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDirectumTaskGroupIdDirectumTaskGroupFile = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameDirectumTaskGroupFileProperty = DependencyProperty.Register("VisibilityNameDirectumTaskGroupFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameDirectumTaskGroupFile
        {
            get { return (Visibility) GetValue(VisibilityNameDirectumTaskGroupFileProperty); }
            set { SetValue(VisibilityNameDirectumTaskGroupFileProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLoadMomentDirectumTaskGroupFileProperty = DependencyProperty.Register("VisibilityLoadMomentDirectumTaskGroupFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLoadMomentDirectumTaskGroupFile
        {
            get { return (Visibility) GetValue(VisibilityLoadMomentDirectumTaskGroupFileProperty); }
            set { SetValue(VisibilityLoadMomentDirectumTaskGroupFileProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAuthorDirectumTaskGroupFileProperty = DependencyProperty.Register("VisibilityAuthorDirectumTaskGroupFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAuthorDirectumTaskGroupFile
        {
            get { return (Visibility) GetValue(VisibilityAuthorDirectumTaskGroupFileProperty); }
            set { SetValue(VisibilityAuthorDirectumTaskGroupFileProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDirectumTaskGroupIdDirectumTaskGroupFileProperty = DependencyProperty.Register("VisibilityDirectumTaskGroupIdDirectumTaskGroupFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDirectumTaskGroupIdDirectumTaskGroupFile
        {
            get { return (Visibility) GetValue(VisibilityDirectumTaskGroupIdDirectumTaskGroupFileProperty); }
            set { SetValue(VisibilityDirectumTaskGroupIdDirectumTaskGroupFileProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class DirectumTaskMessageDetailsView : ViewBase
    {
        public DirectumTaskMessageDetailsView()
        {
			InitializeComponent();
        }

        public DirectumTaskMessageDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DirectumTaskMessageDetailsViewModel DirectumTaskMessageDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DirectumTaskMessageDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskMessage).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskMessage.Moment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMomentDirectumTaskMessage = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskMessage).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskMessage.Author)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAuthorDirectumTaskMessage = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DirectumTaskMessage).GetProperty(nameof(HVTApp.Model.POCOs.DirectumTaskMessage.Message)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMessageDirectumTaskMessage = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityMomentDirectumTaskMessageProperty = DependencyProperty.Register("VisibilityMomentDirectumTaskMessage", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMomentDirectumTaskMessage
        {
            get { return (Visibility) GetValue(VisibilityMomentDirectumTaskMessageProperty); }
            set { SetValue(VisibilityMomentDirectumTaskMessageProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAuthorDirectumTaskMessageProperty = DependencyProperty.Register("VisibilityAuthorDirectumTaskMessage", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAuthorDirectumTaskMessage
        {
            get { return (Visibility) GetValue(VisibilityAuthorDirectumTaskMessageProperty); }
            set { SetValue(VisibilityAuthorDirectumTaskMessageProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityMessageDirectumTaskMessageProperty = DependencyProperty.Register("VisibilityMessageDirectumTaskMessage", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMessageDirectumTaskMessage
        {
            get { return (Visibility) GetValue(VisibilityMessageDirectumTaskMessageProperty); }
            set { SetValue(VisibilityMessageDirectumTaskMessageProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class DocumentNumberDetailsView : ViewBase
    {
        public DocumentNumberDetailsView()
        {
			InitializeComponent();
        }

        public DocumentNumberDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentNumberDetailsViewModel DocumentNumberDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DocumentNumberDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.DocumentNumber).GetProperty(nameof(HVTApp.Model.POCOs.DocumentNumber.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberDocumentNumber = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNumberDocumentNumberProperty = DependencyProperty.Register("VisibilityNumberDocumentNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberDocumentNumber
        {
            get { return (Visibility) GetValue(VisibilityNumberDocumentNumberProperty); }
            set { SetValue(VisibilityNumberDocumentNumberProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class EventServiceUnitDetailsView : ViewBase
    {
        public EventServiceUnitDetailsView()
        {
			InitializeComponent();
        }

        public EventServiceUnitDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, EventServiceUnitDetailsViewModel EventServiceUnitDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = EventServiceUnitDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.EventServiceUnit).GetProperty(nameof(HVTApp.Model.POCOs.EventServiceUnit.User)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityUserEventServiceUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.EventServiceUnit).GetProperty(nameof(HVTApp.Model.POCOs.EventServiceUnit.TargetEntityId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTargetEntityIdEventServiceUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.EventServiceUnit).GetProperty(nameof(HVTApp.Model.POCOs.EventServiceUnit.EventServiceActionType)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEventServiceActionTypeEventServiceUnit = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityUserEventServiceUnitProperty = DependencyProperty.Register("VisibilityUserEventServiceUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityUserEventServiceUnit
        {
            get { return (Visibility) GetValue(VisibilityUserEventServiceUnitProperty); }
            set { SetValue(VisibilityUserEventServiceUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTargetEntityIdEventServiceUnitProperty = DependencyProperty.Register("VisibilityTargetEntityIdEventServiceUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTargetEntityIdEventServiceUnit
        {
            get { return (Visibility) GetValue(VisibilityTargetEntityIdEventServiceUnitProperty); }
            set { SetValue(VisibilityTargetEntityIdEventServiceUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityEventServiceActionTypeEventServiceUnitProperty = DependencyProperty.Register("VisibilityEventServiceActionTypeEventServiceUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEventServiceActionTypeEventServiceUnit
        {
            get { return (Visibility) GetValue(VisibilityEventServiceActionTypeEventServiceUnitProperty); }
            set { SetValue(VisibilityEventServiceActionTypeEventServiceUnitProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class IncomingRequestDetailsView : ViewBase
    {
        public IncomingRequestDetailsView()
        {
			InitializeComponent();
        }

        public IncomingRequestDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, IncomingRequestDetailsViewModel IncomingRequestDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = IncomingRequestDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.IncomingRequest).GetProperty(nameof(HVTApp.Model.POCOs.IncomingRequest.Document)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDocumentIncomingRequest = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.IncomingRequest).GetProperty(nameof(HVTApp.Model.POCOs.IncomingRequest.Performers)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPerformersIncomingRequest = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.IncomingRequest).GetProperty(nameof(HVTApp.Model.POCOs.IncomingRequest.IsDone)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsDoneIncomingRequest = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.IncomingRequest).GetProperty(nameof(HVTApp.Model.POCOs.IncomingRequest.IsActual)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsActualIncomingRequest = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.IncomingRequest).GetProperty(nameof(HVTApp.Model.POCOs.IncomingRequest.InstructionDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityInstructionDateIncomingRequest = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.IncomingRequest).GetProperty(nameof(HVTApp.Model.POCOs.IncomingRequest.DoneDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDoneDateIncomingRequest = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDocumentIncomingRequestProperty = DependencyProperty.Register("VisibilityDocumentIncomingRequest", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDocumentIncomingRequest
        {
            get { return (Visibility) GetValue(VisibilityDocumentIncomingRequestProperty); }
            set { SetValue(VisibilityDocumentIncomingRequestProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPerformersIncomingRequestProperty = DependencyProperty.Register("VisibilityPerformersIncomingRequest", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPerformersIncomingRequest
        {
            get { return (Visibility) GetValue(VisibilityPerformersIncomingRequestProperty); }
            set { SetValue(VisibilityPerformersIncomingRequestProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsDoneIncomingRequestProperty = DependencyProperty.Register("VisibilityIsDoneIncomingRequest", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsDoneIncomingRequest
        {
            get { return (Visibility) GetValue(VisibilityIsDoneIncomingRequestProperty); }
            set { SetValue(VisibilityIsDoneIncomingRequestProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsActualIncomingRequestProperty = DependencyProperty.Register("VisibilityIsActualIncomingRequest", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsActualIncomingRequest
        {
            get { return (Visibility) GetValue(VisibilityIsActualIncomingRequestProperty); }
            set { SetValue(VisibilityIsActualIncomingRequestProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityInstructionDateIncomingRequestProperty = DependencyProperty.Register("VisibilityInstructionDateIncomingRequest", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityInstructionDateIncomingRequest
        {
            get { return (Visibility) GetValue(VisibilityInstructionDateIncomingRequestProperty); }
            set { SetValue(VisibilityInstructionDateIncomingRequestProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDoneDateIncomingRequestProperty = DependencyProperty.Register("VisibilityDoneDateIncomingRequest", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDoneDateIncomingRequest
        {
            get { return (Visibility) GetValue(VisibilityDoneDateIncomingRequestProperty); }
            set { SetValue(VisibilityDoneDateIncomingRequestProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class LaborHourCostDetailsView : ViewBase
    {
        public LaborHourCostDetailsView()
        {
			InitializeComponent();
        }

        public LaborHourCostDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, LaborHourCostDetailsViewModel LaborHourCostDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = LaborHourCostDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.LaborHourCost).GetProperty(nameof(HVTApp.Model.POCOs.LaborHourCost.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateLaborHourCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.LaborHourCost).GetProperty(nameof(HVTApp.Model.POCOs.LaborHourCost.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumLaborHourCost = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateLaborHourCostProperty = DependencyProperty.Register("VisibilityDateLaborHourCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateLaborHourCost
        {
            get { return (Visibility) GetValue(VisibilityDateLaborHourCostProperty); }
            set { SetValue(VisibilityDateLaborHourCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySumLaborHourCostProperty = DependencyProperty.Register("VisibilitySumLaborHourCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumLaborHourCost
        {
            get { return (Visibility) GetValue(VisibilitySumLaborHourCostProperty); }
            set { SetValue(VisibilitySumLaborHourCostProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class LaborHoursDetailsView : ViewBase
    {
        public LaborHoursDetailsView()
        {
			InitializeComponent();
        }

        public LaborHoursDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, LaborHoursDetailsViewModel LaborHoursDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = LaborHoursDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.LaborHours).GetProperty(nameof(HVTApp.Model.POCOs.LaborHours.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersLaborHours = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.LaborHours).GetProperty(nameof(HVTApp.Model.POCOs.LaborHours.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAmountLaborHours = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.LaborHours).GetProperty(nameof(HVTApp.Model.POCOs.LaborHours.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentLaborHours = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityParametersLaborHoursProperty = DependencyProperty.Register("VisibilityParametersLaborHours", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersLaborHours
        {
            get { return (Visibility) GetValue(VisibilityParametersLaborHoursProperty); }
            set { SetValue(VisibilityParametersLaborHoursProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAmountLaborHoursProperty = DependencyProperty.Register("VisibilityAmountLaborHours", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmountLaborHours
        {
            get { return (Visibility) GetValue(VisibilityAmountLaborHoursProperty); }
            set { SetValue(VisibilityAmountLaborHoursProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentLaborHoursProperty = DependencyProperty.Register("VisibilityCommentLaborHours", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentLaborHours
        {
            get { return (Visibility) GetValue(VisibilityCommentLaborHoursProperty); }
            set { SetValue(VisibilityCommentLaborHoursProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class LogUnitDetailsView : ViewBase
    {
        public LogUnitDetailsView()
        {
			InitializeComponent();
        }

        public LogUnitDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, LogUnitDetailsViewModel LogUnitDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = LogUnitDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.LogUnit).GetProperty(nameof(HVTApp.Model.POCOs.LogUnit.Moment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMomentLogUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.LogUnit).GetProperty(nameof(HVTApp.Model.POCOs.LogUnit.Author)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAuthorLogUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.LogUnit).GetProperty(nameof(HVTApp.Model.POCOs.LogUnit.Head)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHeadLogUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.LogUnit).GetProperty(nameof(HVTApp.Model.POCOs.LogUnit.Message)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMessageLogUnit = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityMomentLogUnitProperty = DependencyProperty.Register("VisibilityMomentLogUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMomentLogUnit
        {
            get { return (Visibility) GetValue(VisibilityMomentLogUnitProperty); }
            set { SetValue(VisibilityMomentLogUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAuthorLogUnitProperty = DependencyProperty.Register("VisibilityAuthorLogUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAuthorLogUnit
        {
            get { return (Visibility) GetValue(VisibilityAuthorLogUnitProperty); }
            set { SetValue(VisibilityAuthorLogUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityHeadLogUnitProperty = DependencyProperty.Register("VisibilityHeadLogUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHeadLogUnit
        {
            get { return (Visibility) GetValue(VisibilityHeadLogUnitProperty); }
            set { SetValue(VisibilityHeadLogUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityMessageLogUnitProperty = DependencyProperty.Register("VisibilityMessageLogUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMessageLogUnit
        {
            get { return (Visibility) GetValue(VisibilityMessageLogUnitProperty); }
            set { SetValue(VisibilityMessageLogUnitProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class LosingReasonDetailsView : ViewBase
    {
        public LosingReasonDetailsView()
        {
			InitializeComponent();
        }

        public LosingReasonDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, LosingReasonDetailsViewModel LosingReasonDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = LosingReasonDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.LosingReason).GetProperty(nameof(HVTApp.Model.POCOs.LosingReason.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameLosingReason = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameLosingReasonProperty = DependencyProperty.Register("VisibilityNameLosingReason", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameLosingReason
        {
            get { return (Visibility) GetValue(VisibilityNameLosingReasonProperty); }
            set { SetValue(VisibilityNameLosingReasonProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class MarketFieldDetailsView : ViewBase
    {
        public MarketFieldDetailsView()
        {
			InitializeComponent();
        }

        public MarketFieldDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, MarketFieldDetailsViewModel MarketFieldDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = MarketFieldDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.MarketField).GetProperty(nameof(HVTApp.Model.POCOs.MarketField.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameMarketField = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.MarketField).GetProperty(nameof(HVTApp.Model.POCOs.MarketField.ActivityFields)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityActivityFieldsMarketField = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameMarketFieldProperty = DependencyProperty.Register("VisibilityNameMarketField", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameMarketField
        {
            get { return (Visibility) GetValue(VisibilityNameMarketFieldProperty); }
            set { SetValue(VisibilityNameMarketFieldProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityActivityFieldsMarketFieldProperty = DependencyProperty.Register("VisibilityActivityFieldsMarketField", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActivityFieldsMarketField
        {
            get { return (Visibility) GetValue(VisibilityActivityFieldsMarketFieldProperty); }
            set { SetValue(VisibilityActivityFieldsMarketFieldProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PaymentActualDetailsView : ViewBase
    {
        public PaymentActualDetailsView()
        {
			InitializeComponent();
        }

        public PaymentActualDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentActualDetailsViewModel PaymentActualDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PaymentActualDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PaymentActual).GetProperty(nameof(HVTApp.Model.POCOs.PaymentActual.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDatePaymentActual = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentActual).GetProperty(nameof(HVTApp.Model.POCOs.PaymentActual.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumPaymentActual = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentActual).GetProperty(nameof(HVTApp.Model.POCOs.PaymentActual.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentPaymentActual = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDatePaymentActualProperty = DependencyProperty.Register("VisibilityDatePaymentActual", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDatePaymentActual
        {
            get { return (Visibility) GetValue(VisibilityDatePaymentActualProperty); }
            set { SetValue(VisibilityDatePaymentActualProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySumPaymentActualProperty = DependencyProperty.Register("VisibilitySumPaymentActual", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumPaymentActual
        {
            get { return (Visibility) GetValue(VisibilitySumPaymentActualProperty); }
            set { SetValue(VisibilitySumPaymentActualProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentPaymentActualProperty = DependencyProperty.Register("VisibilityCommentPaymentActual", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentPaymentActual
        {
            get { return (Visibility) GetValue(VisibilityCommentPaymentActualProperty); }
            set { SetValue(VisibilityCommentPaymentActualProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PaymentConditionPointDetailsView : ViewBase
    {
        public PaymentConditionPointDetailsView()
        {
			InitializeComponent();
        }

        public PaymentConditionPointDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionPointDetailsViewModel PaymentConditionPointDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PaymentConditionPointDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PaymentConditionPoint).GetProperty(nameof(HVTApp.Model.POCOs.PaymentConditionPoint.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNamePaymentConditionPoint = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentConditionPoint).GetProperty(nameof(HVTApp.Model.POCOs.PaymentConditionPoint.PaymentConditionPointEnum)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionPointEnumPaymentConditionPoint = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNamePaymentConditionPointProperty = DependencyProperty.Register("VisibilityNamePaymentConditionPoint", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNamePaymentConditionPoint
        {
            get { return (Visibility) GetValue(VisibilityNamePaymentConditionPointProperty); }
            set { SetValue(VisibilityNamePaymentConditionPointProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentConditionPointEnumPaymentConditionPointProperty = DependencyProperty.Register("VisibilityPaymentConditionPointEnumPaymentConditionPoint", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionPointEnumPaymentConditionPoint
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionPointEnumPaymentConditionPointProperty); }
            set { SetValue(VisibilityPaymentConditionPointEnumPaymentConditionPointProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PaymentPlannedDetailsView : ViewBase
    {
        public PaymentPlannedDetailsView()
        {
			InitializeComponent();
        }

        public PaymentPlannedDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentPlannedDetailsViewModel PaymentPlannedDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PaymentPlannedDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDatePaymentPlanned = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.DateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateCalculatedPaymentPlanned = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Part)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPartPaymentPlanned = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentPaymentPlanned = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Condition)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityConditionPaymentPlanned = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDatePaymentPlannedProperty = DependencyProperty.Register("VisibilityDatePaymentPlanned", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDatePaymentPlanned
        {
            get { return (Visibility) GetValue(VisibilityDatePaymentPlannedProperty); }
            set { SetValue(VisibilityDatePaymentPlannedProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateCalculatedPaymentPlannedProperty = DependencyProperty.Register("VisibilityDateCalculatedPaymentPlanned", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateCalculatedPaymentPlanned
        {
            get { return (Visibility) GetValue(VisibilityDateCalculatedPaymentPlannedProperty); }
            set { SetValue(VisibilityDateCalculatedPaymentPlannedProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPartPaymentPlannedProperty = DependencyProperty.Register("VisibilityPartPaymentPlanned", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPartPaymentPlanned
        {
            get { return (Visibility) GetValue(VisibilityPartPaymentPlannedProperty); }
            set { SetValue(VisibilityPartPaymentPlannedProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentPaymentPlannedProperty = DependencyProperty.Register("VisibilityCommentPaymentPlanned", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentPaymentPlanned
        {
            get { return (Visibility) GetValue(VisibilityCommentPaymentPlannedProperty); }
            set { SetValue(VisibilityCommentPaymentPlannedProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityConditionPaymentPlannedProperty = DependencyProperty.Register("VisibilityConditionPaymentPlanned", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityConditionPaymentPlanned
        {
            get { return (Visibility) GetValue(VisibilityConditionPaymentPlannedProperty); }
            set { SetValue(VisibilityConditionPaymentPlannedProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PenaltyDetailsView : ViewBase
    {
        public PenaltyDetailsView()
        {
			InitializeComponent();
        }

        public PenaltyDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PenaltyDetailsViewModel PenaltyDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PenaltyDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Penalty).GetProperty(nameof(HVTApp.Model.POCOs.Penalty.PercentPerDay)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPercentPerDayPenalty = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Penalty).GetProperty(nameof(HVTApp.Model.POCOs.Penalty.PercentLimit)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPercentLimitPenalty = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Penalty).GetProperty(nameof(HVTApp.Model.POCOs.Penalty.PenaltyPaid)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPenaltyPaidPenalty = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityPercentPerDayPenaltyProperty = DependencyProperty.Register("VisibilityPercentPerDayPenalty", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPercentPerDayPenalty
        {
            get { return (Visibility) GetValue(VisibilityPercentPerDayPenaltyProperty); }
            set { SetValue(VisibilityPercentPerDayPenaltyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPercentLimitPenaltyProperty = DependencyProperty.Register("VisibilityPercentLimitPenalty", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPercentLimitPenalty
        {
            get { return (Visibility) GetValue(VisibilityPercentLimitPenaltyProperty); }
            set { SetValue(VisibilityPercentLimitPenaltyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPenaltyPaidPenaltyProperty = DependencyProperty.Register("VisibilityPenaltyPaidPenalty", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPenaltyPaidPenalty
        {
            get { return (Visibility) GetValue(VisibilityPenaltyPaidPenaltyProperty); }
            set { SetValue(VisibilityPenaltyPaidPenaltyProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PriceCalculationDetailsView : ViewBase
    {
        public PriceCalculationDetailsView()
        {
			InitializeComponent();
        }

        public PriceCalculationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PriceCalculationDetailsViewModel PriceCalculationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PriceCalculationDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.PriceCalculationItems)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriceCalculationItemsPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.History)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHistoryPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.LastHistoryItem)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLastHistoryItemPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.IsStarted)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsStartedPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.IsFinished)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsFinishedPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.TaskOpenMoment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTaskOpenMomentPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.TaskCloseMoment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTaskCloseMomentPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.IsNeedExcelFile)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsNeedExcelFilePriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNamePriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.Files)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFilesPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.Initiator)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityInitiatorPriceCalculation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculation).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculation.FrontManager)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFrontManagerPriceCalculation = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityPriceCalculationItemsPriceCalculationProperty = DependencyProperty.Register("VisibilityPriceCalculationItemsPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriceCalculationItemsPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityPriceCalculationItemsPriceCalculationProperty); }
            set { SetValue(VisibilityPriceCalculationItemsPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityHistoryPriceCalculationProperty = DependencyProperty.Register("VisibilityHistoryPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHistoryPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityHistoryPriceCalculationProperty); }
            set { SetValue(VisibilityHistoryPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLastHistoryItemPriceCalculationProperty = DependencyProperty.Register("VisibilityLastHistoryItemPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastHistoryItemPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityLastHistoryItemPriceCalculationProperty); }
            set { SetValue(VisibilityLastHistoryItemPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsStartedPriceCalculationProperty = DependencyProperty.Register("VisibilityIsStartedPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsStartedPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityIsStartedPriceCalculationProperty); }
            set { SetValue(VisibilityIsStartedPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsFinishedPriceCalculationProperty = DependencyProperty.Register("VisibilityIsFinishedPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsFinishedPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityIsFinishedPriceCalculationProperty); }
            set { SetValue(VisibilityIsFinishedPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTaskOpenMomentPriceCalculationProperty = DependencyProperty.Register("VisibilityTaskOpenMomentPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTaskOpenMomentPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityTaskOpenMomentPriceCalculationProperty); }
            set { SetValue(VisibilityTaskOpenMomentPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTaskCloseMomentPriceCalculationProperty = DependencyProperty.Register("VisibilityTaskCloseMomentPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTaskCloseMomentPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityTaskCloseMomentPriceCalculationProperty); }
            set { SetValue(VisibilityTaskCloseMomentPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsNeedExcelFilePriceCalculationProperty = DependencyProperty.Register("VisibilityIsNeedExcelFilePriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsNeedExcelFilePriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityIsNeedExcelFilePriceCalculationProperty); }
            set { SetValue(VisibilityIsNeedExcelFilePriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNamePriceCalculationProperty = DependencyProperty.Register("VisibilityNamePriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNamePriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityNamePriceCalculationProperty); }
            set { SetValue(VisibilityNamePriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFilesPriceCalculationProperty = DependencyProperty.Register("VisibilityFilesPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFilesPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityFilesPriceCalculationProperty); }
            set { SetValue(VisibilityFilesPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityInitiatorPriceCalculationProperty = DependencyProperty.Register("VisibilityInitiatorPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityInitiatorPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityInitiatorPriceCalculationProperty); }
            set { SetValue(VisibilityInitiatorPriceCalculationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFrontManagerPriceCalculationProperty = DependencyProperty.Register("VisibilityFrontManagerPriceCalculation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFrontManagerPriceCalculation
        {
            get { return (Visibility) GetValue(VisibilityFrontManagerPriceCalculationProperty); }
            set { SetValue(VisibilityFrontManagerPriceCalculationProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PriceCalculationFileDetailsView : ViewBase
    {
        public PriceCalculationFileDetailsView()
        {
			InitializeComponent();
        }

        public PriceCalculationFileDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PriceCalculationFileDetailsViewModel PriceCalculationFileDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PriceCalculationFileDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationFile).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationFile.CreationMoment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCreationMomentPriceCalculationFile = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationFile).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationFile.CalculationId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCalculationIdPriceCalculationFile = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityCreationMomentPriceCalculationFileProperty = DependencyProperty.Register("VisibilityCreationMomentPriceCalculationFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCreationMomentPriceCalculationFile
        {
            get { return (Visibility) GetValue(VisibilityCreationMomentPriceCalculationFileProperty); }
            set { SetValue(VisibilityCreationMomentPriceCalculationFileProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCalculationIdPriceCalculationFileProperty = DependencyProperty.Register("VisibilityCalculationIdPriceCalculationFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCalculationIdPriceCalculationFile
        {
            get { return (Visibility) GetValue(VisibilityCalculationIdPriceCalculationFileProperty); }
            set { SetValue(VisibilityCalculationIdPriceCalculationFileProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PriceCalculationHistoryItemDetailsView : ViewBase
    {
        public PriceCalculationHistoryItemDetailsView()
        {
			InitializeComponent();
        }

        public PriceCalculationHistoryItemDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PriceCalculationHistoryItemDetailsViewModel PriceCalculationHistoryItemDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PriceCalculationHistoryItemDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationHistoryItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationHistoryItem.PriceCalculationId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriceCalculationIdPriceCalculationHistoryItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationHistoryItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationHistoryItem.Moment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMomentPriceCalculationHistoryItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationHistoryItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationHistoryItem.Type)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTypePriceCalculationHistoryItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationHistoryItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationHistoryItem.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentPriceCalculationHistoryItem = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityPriceCalculationIdPriceCalculationHistoryItemProperty = DependencyProperty.Register("VisibilityPriceCalculationIdPriceCalculationHistoryItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriceCalculationIdPriceCalculationHistoryItem
        {
            get { return (Visibility) GetValue(VisibilityPriceCalculationIdPriceCalculationHistoryItemProperty); }
            set { SetValue(VisibilityPriceCalculationIdPriceCalculationHistoryItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityMomentPriceCalculationHistoryItemProperty = DependencyProperty.Register("VisibilityMomentPriceCalculationHistoryItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMomentPriceCalculationHistoryItem
        {
            get { return (Visibility) GetValue(VisibilityMomentPriceCalculationHistoryItemProperty); }
            set { SetValue(VisibilityMomentPriceCalculationHistoryItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTypePriceCalculationHistoryItemProperty = DependencyProperty.Register("VisibilityTypePriceCalculationHistoryItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTypePriceCalculationHistoryItem
        {
            get { return (Visibility) GetValue(VisibilityTypePriceCalculationHistoryItemProperty); }
            set { SetValue(VisibilityTypePriceCalculationHistoryItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentPriceCalculationHistoryItemProperty = DependencyProperty.Register("VisibilityCommentPriceCalculationHistoryItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentPriceCalculationHistoryItem
        {
            get { return (Visibility) GetValue(VisibilityCommentPriceCalculationHistoryItemProperty); }
            set { SetValue(VisibilityCommentPriceCalculationHistoryItemProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PriceCalculationItemDetailsView : ViewBase
    {
        public PriceCalculationItemDetailsView()
        {
			InitializeComponent();
        }

        public PriceCalculationItemDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PriceCalculationItemDetailsViewModel PriceCalculationItemDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PriceCalculationItemDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationItem.PriceCalculationId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriceCalculationIdPriceCalculationItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationItem.SalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySalesUnitsPriceCalculationItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationItem.StructureCosts)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStructureCostsPriceCalculationItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationItem.OrderInTakeDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderInTakeDatePriceCalculationItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationItem.RealizationDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRealizationDatePriceCalculationItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationItem.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionSetPriceCalculationItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationItem.HasPrice)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHasPricePriceCalculationItem = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PriceCalculationItem).GetProperty(nameof(HVTApp.Model.POCOs.PriceCalculationItem.Price)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPricePriceCalculationItem = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityPriceCalculationIdPriceCalculationItemProperty = DependencyProperty.Register("VisibilityPriceCalculationIdPriceCalculationItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriceCalculationIdPriceCalculationItem
        {
            get { return (Visibility) GetValue(VisibilityPriceCalculationIdPriceCalculationItemProperty); }
            set { SetValue(VisibilityPriceCalculationIdPriceCalculationItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySalesUnitsPriceCalculationItemProperty = DependencyProperty.Register("VisibilitySalesUnitsPriceCalculationItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySalesUnitsPriceCalculationItem
        {
            get { return (Visibility) GetValue(VisibilitySalesUnitsPriceCalculationItemProperty); }
            set { SetValue(VisibilitySalesUnitsPriceCalculationItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStructureCostsPriceCalculationItemProperty = DependencyProperty.Register("VisibilityStructureCostsPriceCalculationItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStructureCostsPriceCalculationItem
        {
            get { return (Visibility) GetValue(VisibilityStructureCostsPriceCalculationItemProperty); }
            set { SetValue(VisibilityStructureCostsPriceCalculationItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderInTakeDatePriceCalculationItemProperty = DependencyProperty.Register("VisibilityOrderInTakeDatePriceCalculationItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeDatePriceCalculationItem
        {
            get { return (Visibility) GetValue(VisibilityOrderInTakeDatePriceCalculationItemProperty); }
            set { SetValue(VisibilityOrderInTakeDatePriceCalculationItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRealizationDatePriceCalculationItemProperty = DependencyProperty.Register("VisibilityRealizationDatePriceCalculationItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRealizationDatePriceCalculationItem
        {
            get { return (Visibility) GetValue(VisibilityRealizationDatePriceCalculationItemProperty); }
            set { SetValue(VisibilityRealizationDatePriceCalculationItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentConditionSetPriceCalculationItemProperty = DependencyProperty.Register("VisibilityPaymentConditionSetPriceCalculationItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionSetPriceCalculationItem
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionSetPriceCalculationItemProperty); }
            set { SetValue(VisibilityPaymentConditionSetPriceCalculationItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityHasPricePriceCalculationItemProperty = DependencyProperty.Register("VisibilityHasPricePriceCalculationItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHasPricePriceCalculationItem
        {
            get { return (Visibility) GetValue(VisibilityHasPricePriceCalculationItemProperty); }
            set { SetValue(VisibilityHasPricePriceCalculationItemProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPricePriceCalculationItemProperty = DependencyProperty.Register("VisibilityPricePriceCalculationItem", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPricePriceCalculationItem
        {
            get { return (Visibility) GetValue(VisibilityPricePriceCalculationItemProperty); }
            set { SetValue(VisibilityPricePriceCalculationItemProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductCategoryDetailsView : ViewBase
    {
        public ProductCategoryDetailsView()
        {
			InitializeComponent();
        }

        public ProductCategoryDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductCategoryDetailsViewModel ProductCategoryDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductCategoryDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductCategory).GetProperty(nameof(HVTApp.Model.POCOs.ProductCategory.NameFull)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameFullProductCategory = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductCategory).GetProperty(nameof(HVTApp.Model.POCOs.ProductCategory.NameShort)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameShortProductCategory = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductCategory).GetProperty(nameof(HVTApp.Model.POCOs.ProductCategory.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersProductCategory = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameFullProductCategoryProperty = DependencyProperty.Register("VisibilityNameFullProductCategory", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameFullProductCategory
        {
            get { return (Visibility) GetValue(VisibilityNameFullProductCategoryProperty); }
            set { SetValue(VisibilityNameFullProductCategoryProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNameShortProductCategoryProperty = DependencyProperty.Register("VisibilityNameShortProductCategory", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameShortProductCategory
        {
            get { return (Visibility) GetValue(VisibilityNameShortProductCategoryProperty); }
            set { SetValue(VisibilityNameShortProductCategoryProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParametersProductCategoryProperty = DependencyProperty.Register("VisibilityParametersProductCategory", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersProductCategory
        {
            get { return (Visibility) GetValue(VisibilityParametersProductCategoryProperty); }
            set { SetValue(VisibilityParametersProductCategoryProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductCategoryPriceAndCostDetailsView : ViewBase
    {
        public ProductCategoryPriceAndCostDetailsView()
        {
			InitializeComponent();
        }

        public ProductCategoryPriceAndCostDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductCategoryPriceAndCostDetailsViewModel ProductCategoryPriceAndCostDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductCategoryPriceAndCostDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductCategoryPriceAndCost).GetProperty(nameof(HVTApp.Model.POCOs.ProductCategoryPriceAndCost.Category)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCategoryProductCategoryPriceAndCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductCategoryPriceAndCost).GetProperty(nameof(HVTApp.Model.POCOs.ProductCategoryPriceAndCost.Cost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostProductCategoryPriceAndCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductCategoryPriceAndCost).GetProperty(nameof(HVTApp.Model.POCOs.ProductCategoryPriceAndCost.Price)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriceProductCategoryPriceAndCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductCategoryPriceAndCost).GetProperty(nameof(HVTApp.Model.POCOs.ProductCategoryPriceAndCost.StructureCost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStructureCostProductCategoryPriceAndCost = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityCategoryProductCategoryPriceAndCostProperty = DependencyProperty.Register("VisibilityCategoryProductCategoryPriceAndCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCategoryProductCategoryPriceAndCost
        {
            get { return (Visibility) GetValue(VisibilityCategoryProductCategoryPriceAndCostProperty); }
            set { SetValue(VisibilityCategoryProductCategoryPriceAndCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCostProductCategoryPriceAndCostProperty = DependencyProperty.Register("VisibilityCostProductCategoryPriceAndCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostProductCategoryPriceAndCost
        {
            get { return (Visibility) GetValue(VisibilityCostProductCategoryPriceAndCostProperty); }
            set { SetValue(VisibilityCostProductCategoryPriceAndCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPriceProductCategoryPriceAndCostProperty = DependencyProperty.Register("VisibilityPriceProductCategoryPriceAndCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriceProductCategoryPriceAndCost
        {
            get { return (Visibility) GetValue(VisibilityPriceProductCategoryPriceAndCostProperty); }
            set { SetValue(VisibilityPriceProductCategoryPriceAndCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStructureCostProductCategoryPriceAndCostProperty = DependencyProperty.Register("VisibilityStructureCostProductCategoryPriceAndCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStructureCostProductCategoryPriceAndCost
        {
            get { return (Visibility) GetValue(VisibilityStructureCostProductCategoryPriceAndCostProperty); }
            set { SetValue(VisibilityStructureCostProductCategoryPriceAndCostProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductIncludedDetailsView : ViewBase
    {
        public ProductIncludedDetailsView()
        {
			InitializeComponent();
        }

        public ProductIncludedDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductIncludedDetailsViewModel ProductIncludedDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductIncludedDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductIncluded).GetProperty(nameof(HVTApp.Model.POCOs.ProductIncluded.Product)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductProductIncluded = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductIncluded).GetProperty(nameof(HVTApp.Model.POCOs.ProductIncluded.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAmountProductIncluded = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductIncluded).GetProperty(nameof(HVTApp.Model.POCOs.ProductIncluded.CustomFixedPrice)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCustomFixedPriceProductIncluded = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductIncluded).GetProperty(nameof(HVTApp.Model.POCOs.ProductIncluded.ParentsCount)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParentsCountProductIncluded = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductIncluded).GetProperty(nameof(HVTApp.Model.POCOs.ProductIncluded.AmountOnUnit)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAmountOnUnitProductIncluded = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityProductProductIncludedProperty = DependencyProperty.Register("VisibilityProductProductIncluded", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductProductIncluded
        {
            get { return (Visibility) GetValue(VisibilityProductProductIncludedProperty); }
            set { SetValue(VisibilityProductProductIncludedProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAmountProductIncludedProperty = DependencyProperty.Register("VisibilityAmountProductIncluded", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmountProductIncluded
        {
            get { return (Visibility) GetValue(VisibilityAmountProductIncludedProperty); }
            set { SetValue(VisibilityAmountProductIncludedProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCustomFixedPriceProductIncludedProperty = DependencyProperty.Register("VisibilityCustomFixedPriceProductIncluded", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCustomFixedPriceProductIncluded
        {
            get { return (Visibility) GetValue(VisibilityCustomFixedPriceProductIncludedProperty); }
            set { SetValue(VisibilityCustomFixedPriceProductIncludedProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParentsCountProductIncludedProperty = DependencyProperty.Register("VisibilityParentsCountProductIncluded", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentsCountProductIncluded
        {
            get { return (Visibility) GetValue(VisibilityParentsCountProductIncludedProperty); }
            set { SetValue(VisibilityParentsCountProductIncludedProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAmountOnUnitProductIncludedProperty = DependencyProperty.Register("VisibilityAmountOnUnitProductIncluded", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmountOnUnitProductIncluded
        {
            get { return (Visibility) GetValue(VisibilityAmountOnUnitProductIncludedProperty); }
            set { SetValue(VisibilityAmountOnUnitProductIncludedProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductDesignationDetailsView : ViewBase
    {
        public ProductDesignationDetailsView()
        {
			InitializeComponent();
        }

        public ProductDesignationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductDesignationDetailsViewModel ProductDesignationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductDesignationDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductDesignation.Designation)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesignationProductDesignation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductDesignation.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersProductDesignation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductDesignation.Parents)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParentsProductDesignation = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDesignationProductDesignationProperty = DependencyProperty.Register("VisibilityDesignationProductDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignationProductDesignation
        {
            get { return (Visibility) GetValue(VisibilityDesignationProductDesignationProperty); }
            set { SetValue(VisibilityDesignationProductDesignationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParametersProductDesignationProperty = DependencyProperty.Register("VisibilityParametersProductDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersProductDesignation
        {
            get { return (Visibility) GetValue(VisibilityParametersProductDesignationProperty); }
            set { SetValue(VisibilityParametersProductDesignationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParentsProductDesignationProperty = DependencyProperty.Register("VisibilityParentsProductDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentsProductDesignation
        {
            get { return (Visibility) GetValue(VisibilityParentsProductDesignationProperty); }
            set { SetValue(VisibilityParentsProductDesignationProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductTypeDetailsView : ViewBase
    {
        public ProductTypeDetailsView()
        {
			InitializeComponent();
        }

        public ProductTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductTypeDetailsViewModel ProductTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductTypeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductType).GetProperty(nameof(HVTApp.Model.POCOs.ProductType.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameProductType = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameProductTypeProperty = DependencyProperty.Register("VisibilityNameProductType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameProductType
        {
            get { return (Visibility) GetValue(VisibilityNameProductTypeProperty); }
            set { SetValue(VisibilityNameProductTypeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductTypeDesignationDetailsView : ViewBase
    {
        public ProductTypeDesignationDetailsView()
        {
			InitializeComponent();
        }

        public ProductTypeDesignationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductTypeDesignationDetailsViewModel ProductTypeDesignationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductTypeDesignationDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductTypeDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductTypeDesignation.ProductType)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductTypeProductTypeDesignation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductTypeDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductTypeDesignation.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersProductTypeDesignation = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityProductTypeProductTypeDesignationProperty = DependencyProperty.Register("VisibilityProductTypeProductTypeDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductTypeProductTypeDesignation
        {
            get { return (Visibility) GetValue(VisibilityProductTypeProductTypeDesignationProperty); }
            set { SetValue(VisibilityProductTypeProductTypeDesignationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParametersProductTypeDesignationProperty = DependencyProperty.Register("VisibilityParametersProductTypeDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersProductTypeDesignation
        {
            get { return (Visibility) GetValue(VisibilityParametersProductTypeDesignationProperty); }
            set { SetValue(VisibilityParametersProductTypeDesignationProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProjectTypeDetailsView : ViewBase
    {
        public ProjectTypeDetailsView()
        {
			InitializeComponent();
        }

        public ProjectTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectTypeDetailsViewModel ProjectTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProjectTypeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProjectType).GetProperty(nameof(HVTApp.Model.POCOs.ProjectType.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameProjectType = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameProjectTypeProperty = DependencyProperty.Register("VisibilityNameProjectType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameProjectType
        {
            get { return (Visibility) GetValue(VisibilityNameProjectTypeProperty); }
            set { SetValue(VisibilityNameProjectTypeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class StandartMarginalIncomeDetailsView : ViewBase
    {
        public StandartMarginalIncomeDetailsView()
        {
			InitializeComponent();
        }

        public StandartMarginalIncomeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, StandartMarginalIncomeDetailsViewModel StandartMarginalIncomeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = StandartMarginalIncomeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.StandartMarginalIncome).GetProperty(nameof(HVTApp.Model.POCOs.StandartMarginalIncome.MarginalIncome)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMarginalIncomeStandartMarginalIncome = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StandartMarginalIncome).GetProperty(nameof(HVTApp.Model.POCOs.StandartMarginalIncome.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersStandartMarginalIncome = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityMarginalIncomeStandartMarginalIncomeProperty = DependencyProperty.Register("VisibilityMarginalIncomeStandartMarginalIncome", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMarginalIncomeStandartMarginalIncome
        {
            get { return (Visibility) GetValue(VisibilityMarginalIncomeStandartMarginalIncomeProperty); }
            set { SetValue(VisibilityMarginalIncomeStandartMarginalIncomeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParametersStandartMarginalIncomeProperty = DependencyProperty.Register("VisibilityParametersStandartMarginalIncome", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersStandartMarginalIncome
        {
            get { return (Visibility) GetValue(VisibilityParametersStandartMarginalIncomeProperty); }
            set { SetValue(VisibilityParametersStandartMarginalIncomeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class StandartProductionTermDetailsView : ViewBase
    {
        public StandartProductionTermDetailsView()
        {
			InitializeComponent();
        }

        public StandartProductionTermDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, StandartProductionTermDetailsViewModel StandartProductionTermDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = StandartProductionTermDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.StandartProductionTerm).GetProperty(nameof(HVTApp.Model.POCOs.StandartProductionTerm.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductionTermStandartProductionTerm = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StandartProductionTerm).GetProperty(nameof(HVTApp.Model.POCOs.StandartProductionTerm.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersStandartProductionTerm = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityProductionTermStandartProductionTermProperty = DependencyProperty.Register("VisibilityProductionTermStandartProductionTerm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductionTermStandartProductionTerm
        {
            get { return (Visibility) GetValue(VisibilityProductionTermStandartProductionTermProperty); }
            set { SetValue(VisibilityProductionTermStandartProductionTermProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParametersStandartProductionTermProperty = DependencyProperty.Register("VisibilityParametersStandartProductionTerm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersStandartProductionTerm
        {
            get { return (Visibility) GetValue(VisibilityParametersStandartProductionTermProperty); }
            set { SetValue(VisibilityParametersStandartProductionTermProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class StructureCostDetailsView : ViewBase
    {
        public StructureCostDetailsView()
        {
			InitializeComponent();
        }

        public StructureCostDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, StructureCostDetailsViewModel StructureCostDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = StructureCostDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.StructureCost).GetProperty(nameof(HVTApp.Model.POCOs.StructureCost.PriceCalculationItemId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriceCalculationItemIdStructureCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StructureCost).GetProperty(nameof(HVTApp.Model.POCOs.StructureCost.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberStructureCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StructureCost).GetProperty(nameof(HVTApp.Model.POCOs.StructureCost.AmountNumerator)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAmountNumeratorStructureCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StructureCost).GetProperty(nameof(HVTApp.Model.POCOs.StructureCost.AmountDenomerator)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAmountDenomeratorStructureCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StructureCost).GetProperty(nameof(HVTApp.Model.POCOs.StructureCost.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAmountStructureCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StructureCost).GetProperty(nameof(HVTApp.Model.POCOs.StructureCost.UnitPrice)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityUnitPriceStructureCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StructureCost).GetProperty(nameof(HVTApp.Model.POCOs.StructureCost.Total)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTotalStructureCost = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.StructureCost).GetProperty(nameof(HVTApp.Model.POCOs.StructureCost.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentStructureCost = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityPriceCalculationItemIdStructureCostProperty = DependencyProperty.Register("VisibilityPriceCalculationItemIdStructureCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriceCalculationItemIdStructureCost
        {
            get { return (Visibility) GetValue(VisibilityPriceCalculationItemIdStructureCostProperty); }
            set { SetValue(VisibilityPriceCalculationItemIdStructureCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNumberStructureCostProperty = DependencyProperty.Register("VisibilityNumberStructureCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberStructureCost
        {
            get { return (Visibility) GetValue(VisibilityNumberStructureCostProperty); }
            set { SetValue(VisibilityNumberStructureCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAmountNumeratorStructureCostProperty = DependencyProperty.Register("VisibilityAmountNumeratorStructureCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmountNumeratorStructureCost
        {
            get { return (Visibility) GetValue(VisibilityAmountNumeratorStructureCostProperty); }
            set { SetValue(VisibilityAmountNumeratorStructureCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAmountDenomeratorStructureCostProperty = DependencyProperty.Register("VisibilityAmountDenomeratorStructureCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmountDenomeratorStructureCost
        {
            get { return (Visibility) GetValue(VisibilityAmountDenomeratorStructureCostProperty); }
            set { SetValue(VisibilityAmountDenomeratorStructureCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAmountStructureCostProperty = DependencyProperty.Register("VisibilityAmountStructureCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmountStructureCost
        {
            get { return (Visibility) GetValue(VisibilityAmountStructureCostProperty); }
            set { SetValue(VisibilityAmountStructureCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityUnitPriceStructureCostProperty = DependencyProperty.Register("VisibilityUnitPriceStructureCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityUnitPriceStructureCost
        {
            get { return (Visibility) GetValue(VisibilityUnitPriceStructureCostProperty); }
            set { SetValue(VisibilityUnitPriceStructureCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTotalStructureCostProperty = DependencyProperty.Register("VisibilityTotalStructureCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTotalStructureCost
        {
            get { return (Visibility) GetValue(VisibilityTotalStructureCostProperty); }
            set { SetValue(VisibilityTotalStructureCostProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentStructureCostProperty = DependencyProperty.Register("VisibilityCommentStructureCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentStructureCost
        {
            get { return (Visibility) GetValue(VisibilityCommentStructureCostProperty); }
            set { SetValue(VisibilityCommentStructureCostProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class SupervisionDetailsView : ViewBase
    {
        public SupervisionDetailsView()
        {
			InitializeComponent();
        }

        public SupervisionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SupervisionDetailsViewModel SupervisionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = SupervisionDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Supervision).GetProperty(nameof(HVTApp.Model.POCOs.Supervision.SalesUnit)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySalesUnitSupervision = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Supervision).GetProperty(nameof(HVTApp.Model.POCOs.Supervision.SupervisionUnit)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySupervisionUnitSupervision = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Supervision).GetProperty(nameof(HVTApp.Model.POCOs.Supervision.DateFinish)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateFinishSupervision = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Supervision).GetProperty(nameof(HVTApp.Model.POCOs.Supervision.DateRequired)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateRequiredSupervision = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Supervision).GetProperty(nameof(HVTApp.Model.POCOs.Supervision.ClientOrderNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityClientOrderNumberSupervision = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Supervision).GetProperty(nameof(HVTApp.Model.POCOs.Supervision.ServiceOrderNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityServiceOrderNumberSupervision = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilitySalesUnitSupervisionProperty = DependencyProperty.Register("VisibilitySalesUnitSupervision", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySalesUnitSupervision
        {
            get { return (Visibility) GetValue(VisibilitySalesUnitSupervisionProperty); }
            set { SetValue(VisibilitySalesUnitSupervisionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySupervisionUnitSupervisionProperty = DependencyProperty.Register("VisibilitySupervisionUnitSupervision", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySupervisionUnitSupervision
        {
            get { return (Visibility) GetValue(VisibilitySupervisionUnitSupervisionProperty); }
            set { SetValue(VisibilitySupervisionUnitSupervisionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateFinishSupervisionProperty = DependencyProperty.Register("VisibilityDateFinishSupervision", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateFinishSupervision
        {
            get { return (Visibility) GetValue(VisibilityDateFinishSupervisionProperty); }
            set { SetValue(VisibilityDateFinishSupervisionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateRequiredSupervisionProperty = DependencyProperty.Register("VisibilityDateRequiredSupervision", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateRequiredSupervision
        {
            get { return (Visibility) GetValue(VisibilityDateRequiredSupervisionProperty); }
            set { SetValue(VisibilityDateRequiredSupervisionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityClientOrderNumberSupervisionProperty = DependencyProperty.Register("VisibilityClientOrderNumberSupervision", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityClientOrderNumberSupervision
        {
            get { return (Visibility) GetValue(VisibilityClientOrderNumberSupervisionProperty); }
            set { SetValue(VisibilityClientOrderNumberSupervisionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityServiceOrderNumberSupervisionProperty = DependencyProperty.Register("VisibilityServiceOrderNumberSupervision", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityServiceOrderNumberSupervision
        {
            get { return (Visibility) GetValue(VisibilityServiceOrderNumberSupervisionProperty); }
            set { SetValue(VisibilityServiceOrderNumberSupervisionProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class AnswerFileTceDetailsView : ViewBase
    {
        public AnswerFileTceDetailsView()
        {
			InitializeComponent();
        }

        public AnswerFileTceDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, AnswerFileTceDetailsViewModel AnswerFileTceDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = AnswerFileTceDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.AnswerFileTce).GetProperty(nameof(HVTApp.Model.POCOs.AnswerFileTce.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateAnswerFileTce = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.AnswerFileTce).GetProperty(nameof(HVTApp.Model.POCOs.AnswerFileTce.TechnicalRequrementsTaskId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTechnicalRequrementsTaskIdAnswerFileTce = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.AnswerFileTce).GetProperty(nameof(HVTApp.Model.POCOs.AnswerFileTce.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameAnswerFileTce = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.AnswerFileTce).GetProperty(nameof(HVTApp.Model.POCOs.AnswerFileTce.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentAnswerFileTce = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.AnswerFileTce).GetProperty(nameof(HVTApp.Model.POCOs.AnswerFileTce.IsActual)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsActualAnswerFileTce = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateAnswerFileTceProperty = DependencyProperty.Register("VisibilityDateAnswerFileTce", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateAnswerFileTce
        {
            get { return (Visibility) GetValue(VisibilityDateAnswerFileTceProperty); }
            set { SetValue(VisibilityDateAnswerFileTceProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTechnicalRequrementsTaskIdAnswerFileTceProperty = DependencyProperty.Register("VisibilityTechnicalRequrementsTaskIdAnswerFileTce", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTechnicalRequrementsTaskIdAnswerFileTce
        {
            get { return (Visibility) GetValue(VisibilityTechnicalRequrementsTaskIdAnswerFileTceProperty); }
            set { SetValue(VisibilityTechnicalRequrementsTaskIdAnswerFileTceProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNameAnswerFileTceProperty = DependencyProperty.Register("VisibilityNameAnswerFileTce", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameAnswerFileTce
        {
            get { return (Visibility) GetValue(VisibilityNameAnswerFileTceProperty); }
            set { SetValue(VisibilityNameAnswerFileTceProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentAnswerFileTceProperty = DependencyProperty.Register("VisibilityCommentAnswerFileTce", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentAnswerFileTce
        {
            get { return (Visibility) GetValue(VisibilityCommentAnswerFileTceProperty); }
            set { SetValue(VisibilityCommentAnswerFileTceProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsActualAnswerFileTceProperty = DependencyProperty.Register("VisibilityIsActualAnswerFileTce", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsActualAnswerFileTce
        {
            get { return (Visibility) GetValue(VisibilityIsActualAnswerFileTceProperty); }
            set { SetValue(VisibilityIsActualAnswerFileTceProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ShippingCostFileDetailsView : ViewBase
    {
        public ShippingCostFileDetailsView()
        {
			InitializeComponent();
        }

        public ShippingCostFileDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ShippingCostFileDetailsViewModel ShippingCostFileDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ShippingCostFileDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ShippingCostFile).GetProperty(nameof(HVTApp.Model.POCOs.ShippingCostFile.TechnicalRequrementsTaskId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTechnicalRequrementsTaskIdShippingCostFile = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ShippingCostFile).GetProperty(nameof(HVTApp.Model.POCOs.ShippingCostFile.Moment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMomentShippingCostFile = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityTechnicalRequrementsTaskIdShippingCostFileProperty = DependencyProperty.Register("VisibilityTechnicalRequrementsTaskIdShippingCostFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTechnicalRequrementsTaskIdShippingCostFile
        {
            get { return (Visibility) GetValue(VisibilityTechnicalRequrementsTaskIdShippingCostFileProperty); }
            set { SetValue(VisibilityTechnicalRequrementsTaskIdShippingCostFileProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityMomentShippingCostFileProperty = DependencyProperty.Register("VisibilityMomentShippingCostFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMomentShippingCostFile
        {
            get { return (Visibility) GetValue(VisibilityMomentShippingCostFileProperty); }
            set { SetValue(VisibilityMomentShippingCostFileProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class TechnicalRequrementsDetailsView : ViewBase
    {
        public TechnicalRequrementsDetailsView()
        {
			InitializeComponent();
        }

        public TechnicalRequrementsDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TechnicalRequrementsDetailsViewModel TechnicalRequrementsDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TechnicalRequrementsDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrements).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrements.SalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySalesUnitsTechnicalRequrements = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrements).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrements.OrderInTakeDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderInTakeDateTechnicalRequrements = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrements).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrements.RealizationDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRealizationDateTechnicalRequrements = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrements).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrements.Files)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFilesTechnicalRequrements = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrements).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrements.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentTechnicalRequrements = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrements).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrements.IsActual)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsActualTechnicalRequrements = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilitySalesUnitsTechnicalRequrementsProperty = DependencyProperty.Register("VisibilitySalesUnitsTechnicalRequrements", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySalesUnitsTechnicalRequrements
        {
            get { return (Visibility) GetValue(VisibilitySalesUnitsTechnicalRequrementsProperty); }
            set { SetValue(VisibilitySalesUnitsTechnicalRequrementsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderInTakeDateTechnicalRequrementsProperty = DependencyProperty.Register("VisibilityOrderInTakeDateTechnicalRequrements", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeDateTechnicalRequrements
        {
            get { return (Visibility) GetValue(VisibilityOrderInTakeDateTechnicalRequrementsProperty); }
            set { SetValue(VisibilityOrderInTakeDateTechnicalRequrementsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRealizationDateTechnicalRequrementsProperty = DependencyProperty.Register("VisibilityRealizationDateTechnicalRequrements", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRealizationDateTechnicalRequrements
        {
            get { return (Visibility) GetValue(VisibilityRealizationDateTechnicalRequrementsProperty); }
            set { SetValue(VisibilityRealizationDateTechnicalRequrementsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFilesTechnicalRequrementsProperty = DependencyProperty.Register("VisibilityFilesTechnicalRequrements", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFilesTechnicalRequrements
        {
            get { return (Visibility) GetValue(VisibilityFilesTechnicalRequrementsProperty); }
            set { SetValue(VisibilityFilesTechnicalRequrementsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentTechnicalRequrementsProperty = DependencyProperty.Register("VisibilityCommentTechnicalRequrements", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentTechnicalRequrements
        {
            get { return (Visibility) GetValue(VisibilityCommentTechnicalRequrementsProperty); }
            set { SetValue(VisibilityCommentTechnicalRequrementsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsActualTechnicalRequrementsProperty = DependencyProperty.Register("VisibilityIsActualTechnicalRequrements", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsActualTechnicalRequrements
        {
            get { return (Visibility) GetValue(VisibilityIsActualTechnicalRequrementsProperty); }
            set { SetValue(VisibilityIsActualTechnicalRequrementsProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class TechnicalRequrementsFileDetailsView : ViewBase
    {
        public TechnicalRequrementsFileDetailsView()
        {
			InitializeComponent();
        }

        public TechnicalRequrementsFileDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TechnicalRequrementsFileDetailsViewModel TechnicalRequrementsFileDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TechnicalRequrementsFileDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsFile).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsFile.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateTechnicalRequrementsFile = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsFile).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsFile.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameTechnicalRequrementsFile = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsFile).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsFile.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentTechnicalRequrementsFile = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsFile).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsFile.IsActual)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsActualTechnicalRequrementsFile = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateTechnicalRequrementsFileProperty = DependencyProperty.Register("VisibilityDateTechnicalRequrementsFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateTechnicalRequrementsFile
        {
            get { return (Visibility) GetValue(VisibilityDateTechnicalRequrementsFileProperty); }
            set { SetValue(VisibilityDateTechnicalRequrementsFileProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNameTechnicalRequrementsFileProperty = DependencyProperty.Register("VisibilityNameTechnicalRequrementsFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameTechnicalRequrementsFile
        {
            get { return (Visibility) GetValue(VisibilityNameTechnicalRequrementsFileProperty); }
            set { SetValue(VisibilityNameTechnicalRequrementsFileProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentTechnicalRequrementsFileProperty = DependencyProperty.Register("VisibilityCommentTechnicalRequrementsFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentTechnicalRequrementsFile
        {
            get { return (Visibility) GetValue(VisibilityCommentTechnicalRequrementsFileProperty); }
            set { SetValue(VisibilityCommentTechnicalRequrementsFileProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsActualTechnicalRequrementsFileProperty = DependencyProperty.Register("VisibilityIsActualTechnicalRequrementsFile", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsActualTechnicalRequrementsFile
        {
            get { return (Visibility) GetValue(VisibilityIsActualTechnicalRequrementsFileProperty); }
            set { SetValue(VisibilityIsActualTechnicalRequrementsFileProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class TechnicalRequrementsTaskDetailsView : ViewBase
    {
        public TechnicalRequrementsTaskDetailsView()
        {
			InitializeComponent();
        }

        public TechnicalRequrementsTaskDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TechnicalRequrementsTaskDetailsViewModel TechnicalRequrementsTaskDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TechnicalRequrementsTaskDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.Requrements)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRequrementsTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.TceNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTceNumberTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.BackManager)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityBackManagerTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.LastOpenBackManagerMoment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLastOpenBackManagerMomentTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.LastOpenFrontManagerMoment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLastOpenFrontManagerMomentTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.LogisticsCalculationRequired)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLogisticsCalculationRequiredTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.ExcelFileIsRequired)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityExcelFileIsRequiredTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.PriceCalculations)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriceCalculationsTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.AnswerFiles)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAnswerFilesTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.ShippingCostFiles)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShippingCostFilesTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.HistoryElements)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHistoryElementsTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.DesiredFinishDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesiredFinishDateTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.FrontManager)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFrontManagerTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.Start)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.Finish)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFinishTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.LastHistoryElement)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLastHistoryElementTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.IsStarted)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsStartedTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.IsFinished)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsFinishedTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.IsRejected)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsRejectedTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.IsStopped)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsStoppedTechnicalRequrementsTask = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTask).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTask.IsAccepted)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsAcceptedTechnicalRequrementsTask = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityRequrementsTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityRequrementsTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRequrementsTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityRequrementsTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityRequrementsTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTceNumberTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityTceNumberTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTceNumberTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityTceNumberTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityTceNumberTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityBackManagerTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityBackManagerTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBackManagerTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityBackManagerTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityBackManagerTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLastOpenBackManagerMomentTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityLastOpenBackManagerMomentTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastOpenBackManagerMomentTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityLastOpenBackManagerMomentTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityLastOpenBackManagerMomentTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLastOpenFrontManagerMomentTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityLastOpenFrontManagerMomentTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastOpenFrontManagerMomentTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityLastOpenFrontManagerMomentTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityLastOpenFrontManagerMomentTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLogisticsCalculationRequiredTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityLogisticsCalculationRequiredTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLogisticsCalculationRequiredTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityLogisticsCalculationRequiredTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityLogisticsCalculationRequiredTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityExcelFileIsRequiredTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityExcelFileIsRequiredTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityExcelFileIsRequiredTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityExcelFileIsRequiredTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityExcelFileIsRequiredTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPriceCalculationsTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityPriceCalculationsTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriceCalculationsTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityPriceCalculationsTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityPriceCalculationsTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAnswerFilesTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityAnswerFilesTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAnswerFilesTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityAnswerFilesTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityAnswerFilesTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShippingCostFilesTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityShippingCostFilesTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShippingCostFilesTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityShippingCostFilesTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityShippingCostFilesTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityHistoryElementsTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityHistoryElementsTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHistoryElementsTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityHistoryElementsTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityHistoryElementsTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDesiredFinishDateTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityDesiredFinishDateTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesiredFinishDateTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityDesiredFinishDateTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityDesiredFinishDateTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFrontManagerTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityFrontManagerTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFrontManagerTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityFrontManagerTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityFrontManagerTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStartTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityStartTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityStartTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityStartTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFinishTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityFinishTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFinishTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityFinishTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityFinishTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLastHistoryElementTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityLastHistoryElementTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastHistoryElementTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityLastHistoryElementTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityLastHistoryElementTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsStartedTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityIsStartedTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsStartedTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityIsStartedTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityIsStartedTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsFinishedTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityIsFinishedTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsFinishedTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityIsFinishedTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityIsFinishedTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsRejectedTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityIsRejectedTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsRejectedTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityIsRejectedTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityIsRejectedTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsStoppedTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityIsStoppedTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsStoppedTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityIsStoppedTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityIsStoppedTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsAcceptedTechnicalRequrementsTaskProperty = DependencyProperty.Register("VisibilityIsAcceptedTechnicalRequrementsTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsAcceptedTechnicalRequrementsTask
        {
            get { return (Visibility) GetValue(VisibilityIsAcceptedTechnicalRequrementsTaskProperty); }
            set { SetValue(VisibilityIsAcceptedTechnicalRequrementsTaskProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class TechnicalRequrementsTaskHistoryElementDetailsView : ViewBase
    {
        public TechnicalRequrementsTaskHistoryElementDetailsView()
        {
			InitializeComponent();
        }

        public TechnicalRequrementsTaskHistoryElementDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TechnicalRequrementsTaskHistoryElementDetailsViewModel TechnicalRequrementsTaskHistoryElementDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TechnicalRequrementsTaskHistoryElementDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement.TechnicalRequrementsTaskId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTechnicalRequrementsTaskIdTechnicalRequrementsTaskHistoryElement = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement.Moment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMomentTechnicalRequrementsTaskHistoryElement = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement.Type)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTypeTechnicalRequrementsTaskHistoryElement = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement).GetProperty(nameof(HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentTechnicalRequrementsTaskHistoryElement = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityTechnicalRequrementsTaskIdTechnicalRequrementsTaskHistoryElementProperty = DependencyProperty.Register("VisibilityTechnicalRequrementsTaskIdTechnicalRequrementsTaskHistoryElement", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTechnicalRequrementsTaskIdTechnicalRequrementsTaskHistoryElement
        {
            get { return (Visibility) GetValue(VisibilityTechnicalRequrementsTaskIdTechnicalRequrementsTaskHistoryElementProperty); }
            set { SetValue(VisibilityTechnicalRequrementsTaskIdTechnicalRequrementsTaskHistoryElementProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityMomentTechnicalRequrementsTaskHistoryElementProperty = DependencyProperty.Register("VisibilityMomentTechnicalRequrementsTaskHistoryElement", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMomentTechnicalRequrementsTaskHistoryElement
        {
            get { return (Visibility) GetValue(VisibilityMomentTechnicalRequrementsTaskHistoryElementProperty); }
            set { SetValue(VisibilityMomentTechnicalRequrementsTaskHistoryElementProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTypeTechnicalRequrementsTaskHistoryElementProperty = DependencyProperty.Register("VisibilityTypeTechnicalRequrementsTaskHistoryElement", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTypeTechnicalRequrementsTaskHistoryElement
        {
            get { return (Visibility) GetValue(VisibilityTypeTechnicalRequrementsTaskHistoryElementProperty); }
            set { SetValue(VisibilityTypeTechnicalRequrementsTaskHistoryElementProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentTechnicalRequrementsTaskHistoryElementProperty = DependencyProperty.Register("VisibilityCommentTechnicalRequrementsTaskHistoryElement", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentTechnicalRequrementsTaskHistoryElement
        {
            get { return (Visibility) GetValue(VisibilityCommentTechnicalRequrementsTaskHistoryElementProperty); }
            set { SetValue(VisibilityCommentTechnicalRequrementsTaskHistoryElementProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class UserGroupDetailsView : ViewBase
    {
        public UserGroupDetailsView()
        {
			InitializeComponent();
        }

        public UserGroupDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, UserGroupDetailsViewModel UserGroupDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = UserGroupDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.UserGroup).GetProperty(nameof(HVTApp.Model.POCOs.UserGroup.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameUserGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.UserGroup).GetProperty(nameof(HVTApp.Model.POCOs.UserGroup.Users)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityUsersUserGroup = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameUserGroupProperty = DependencyProperty.Register("VisibilityNameUserGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameUserGroup
        {
            get { return (Visibility) GetValue(VisibilityNameUserGroupProperty); }
            set { SetValue(VisibilityNameUserGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityUsersUserGroupProperty = DependencyProperty.Register("VisibilityUsersUserGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityUsersUserGroup
        {
            get { return (Visibility) GetValue(VisibilityUsersUserGroupProperty); }
            set { SetValue(VisibilityUsersUserGroupProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class GlobalPropertiesDetailsView : ViewBase
    {
        public GlobalPropertiesDetailsView()
        {
			InitializeComponent();
        }

        public GlobalPropertiesDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, GlobalPropertiesDetailsViewModel GlobalPropertiesDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = GlobalPropertiesDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.OurCompany)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOurCompanyGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.ActualPriceTerm)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityActualPriceTermGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.StandartTermFromStartToEndProduction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStandartTermFromStartToEndProductionGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.StandartTermFromPickToEndProduction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStandartTermFromPickToEndProductionGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.StandartPaymentsConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStandartPaymentsConditionSetGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityVatGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.NewProductParameter)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNewProductParameterGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.NewProductParameterGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNewProductParameterGroupGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.ServiceParameter)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityServiceParameterGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.SupervisionParameter)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySupervisionParameterGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.VoltageGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityVoltageGroupGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.IsolationMaterialGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsolationMaterialGroupGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.IsolationColorGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsolationColorGroupGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.IsolationDpuGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsolationDpuGroupGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.ComplectDesignationGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityComplectDesignationGroupGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.ComplectsParameter)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityComplectsParameterGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.ComplectsGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityComplectsGroupGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.DefaultProjectType)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDefaultProjectTypeGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.RecipientSupervisionLetterEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRecipientSupervisionLetterEmployeeGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.SenderOfferEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySenderOfferEmployeeGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.HvtProducersActivityField)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHvtProducersActivityFieldGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionSetGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.IncomingRequestsPath)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIncomingRequestsPathGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.DirectumAttachmentsPath)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDirectumAttachmentsPathGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.TechnicalRequrementsFilesPath)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTechnicalRequrementsFilesPathGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.TechnicalRequrementsFilesAnswersPath)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTechnicalRequrementsFilesAnswersPathGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.ShippingCostFilesPath)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShippingCostFilesPathGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.PriceCalculationsFilesPath)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriceCalculationsFilesPathGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.LogsPath)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLogsPathGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.Developer)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDeveloperGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.LastDeveloperVizit)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLastDeveloperVizitGlobalProperties = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.GlobalProperties).GetProperty(nameof(HVTApp.Model.POCOs.GlobalProperties.ProductIncludedDefault)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductIncludedDefaultGlobalProperties = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateGlobalPropertiesProperty = DependencyProperty.Register("VisibilityDateGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityDateGlobalPropertiesProperty); }
            set { SetValue(VisibilityDateGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOurCompanyGlobalPropertiesProperty = DependencyProperty.Register("VisibilityOurCompanyGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOurCompanyGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityOurCompanyGlobalPropertiesProperty); }
            set { SetValue(VisibilityOurCompanyGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityActualPriceTermGlobalPropertiesProperty = DependencyProperty.Register("VisibilityActualPriceTermGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActualPriceTermGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityActualPriceTermGlobalPropertiesProperty); }
            set { SetValue(VisibilityActualPriceTermGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStandartTermFromStartToEndProductionGlobalPropertiesProperty = DependencyProperty.Register("VisibilityStandartTermFromStartToEndProductionGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartTermFromStartToEndProductionGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityStandartTermFromStartToEndProductionGlobalPropertiesProperty); }
            set { SetValue(VisibilityStandartTermFromStartToEndProductionGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStandartTermFromPickToEndProductionGlobalPropertiesProperty = DependencyProperty.Register("VisibilityStandartTermFromPickToEndProductionGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartTermFromPickToEndProductionGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityStandartTermFromPickToEndProductionGlobalPropertiesProperty); }
            set { SetValue(VisibilityStandartTermFromPickToEndProductionGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStandartPaymentsConditionSetGlobalPropertiesProperty = DependencyProperty.Register("VisibilityStandartPaymentsConditionSetGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartPaymentsConditionSetGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityStandartPaymentsConditionSetGlobalPropertiesProperty); }
            set { SetValue(VisibilityStandartPaymentsConditionSetGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityVatGlobalPropertiesProperty = DependencyProperty.Register("VisibilityVatGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityVatGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityVatGlobalPropertiesProperty); }
            set { SetValue(VisibilityVatGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNewProductParameterGlobalPropertiesProperty = DependencyProperty.Register("VisibilityNewProductParameterGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNewProductParameterGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityNewProductParameterGlobalPropertiesProperty); }
            set { SetValue(VisibilityNewProductParameterGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNewProductParameterGroupGlobalPropertiesProperty = DependencyProperty.Register("VisibilityNewProductParameterGroupGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNewProductParameterGroupGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityNewProductParameterGroupGlobalPropertiesProperty); }
            set { SetValue(VisibilityNewProductParameterGroupGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityServiceParameterGlobalPropertiesProperty = DependencyProperty.Register("VisibilityServiceParameterGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityServiceParameterGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityServiceParameterGlobalPropertiesProperty); }
            set { SetValue(VisibilityServiceParameterGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySupervisionParameterGlobalPropertiesProperty = DependencyProperty.Register("VisibilitySupervisionParameterGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySupervisionParameterGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilitySupervisionParameterGlobalPropertiesProperty); }
            set { SetValue(VisibilitySupervisionParameterGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityVoltageGroupGlobalPropertiesProperty = DependencyProperty.Register("VisibilityVoltageGroupGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityVoltageGroupGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityVoltageGroupGlobalPropertiesProperty); }
            set { SetValue(VisibilityVoltageGroupGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsolationMaterialGroupGlobalPropertiesProperty = DependencyProperty.Register("VisibilityIsolationMaterialGroupGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsolationMaterialGroupGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityIsolationMaterialGroupGlobalPropertiesProperty); }
            set { SetValue(VisibilityIsolationMaterialGroupGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsolationColorGroupGlobalPropertiesProperty = DependencyProperty.Register("VisibilityIsolationColorGroupGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsolationColorGroupGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityIsolationColorGroupGlobalPropertiesProperty); }
            set { SetValue(VisibilityIsolationColorGroupGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsolationDpuGroupGlobalPropertiesProperty = DependencyProperty.Register("VisibilityIsolationDpuGroupGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsolationDpuGroupGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityIsolationDpuGroupGlobalPropertiesProperty); }
            set { SetValue(VisibilityIsolationDpuGroupGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityComplectDesignationGroupGlobalPropertiesProperty = DependencyProperty.Register("VisibilityComplectDesignationGroupGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityComplectDesignationGroupGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityComplectDesignationGroupGlobalPropertiesProperty); }
            set { SetValue(VisibilityComplectDesignationGroupGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityComplectsParameterGlobalPropertiesProperty = DependencyProperty.Register("VisibilityComplectsParameterGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityComplectsParameterGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityComplectsParameterGlobalPropertiesProperty); }
            set { SetValue(VisibilityComplectsParameterGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityComplectsGroupGlobalPropertiesProperty = DependencyProperty.Register("VisibilityComplectsGroupGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityComplectsGroupGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityComplectsGroupGlobalPropertiesProperty); }
            set { SetValue(VisibilityComplectsGroupGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDefaultProjectTypeGlobalPropertiesProperty = DependencyProperty.Register("VisibilityDefaultProjectTypeGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDefaultProjectTypeGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityDefaultProjectTypeGlobalPropertiesProperty); }
            set { SetValue(VisibilityDefaultProjectTypeGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRecipientSupervisionLetterEmployeeGlobalPropertiesProperty = DependencyProperty.Register("VisibilityRecipientSupervisionLetterEmployeeGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientSupervisionLetterEmployeeGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityRecipientSupervisionLetterEmployeeGlobalPropertiesProperty); }
            set { SetValue(VisibilityRecipientSupervisionLetterEmployeeGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySenderOfferEmployeeGlobalPropertiesProperty = DependencyProperty.Register("VisibilitySenderOfferEmployeeGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderOfferEmployeeGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilitySenderOfferEmployeeGlobalPropertiesProperty); }
            set { SetValue(VisibilitySenderOfferEmployeeGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityHvtProducersActivityFieldGlobalPropertiesProperty = DependencyProperty.Register("VisibilityHvtProducersActivityFieldGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHvtProducersActivityFieldGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityHvtProducersActivityFieldGlobalPropertiesProperty); }
            set { SetValue(VisibilityHvtProducersActivityFieldGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentConditionSetGlobalPropertiesProperty = DependencyProperty.Register("VisibilityPaymentConditionSetGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionSetGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionSetGlobalPropertiesProperty); }
            set { SetValue(VisibilityPaymentConditionSetGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIncomingRequestsPathGlobalPropertiesProperty = DependencyProperty.Register("VisibilityIncomingRequestsPathGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIncomingRequestsPathGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityIncomingRequestsPathGlobalPropertiesProperty); }
            set { SetValue(VisibilityIncomingRequestsPathGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDirectumAttachmentsPathGlobalPropertiesProperty = DependencyProperty.Register("VisibilityDirectumAttachmentsPathGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDirectumAttachmentsPathGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityDirectumAttachmentsPathGlobalPropertiesProperty); }
            set { SetValue(VisibilityDirectumAttachmentsPathGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTechnicalRequrementsFilesPathGlobalPropertiesProperty = DependencyProperty.Register("VisibilityTechnicalRequrementsFilesPathGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTechnicalRequrementsFilesPathGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityTechnicalRequrementsFilesPathGlobalPropertiesProperty); }
            set { SetValue(VisibilityTechnicalRequrementsFilesPathGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTechnicalRequrementsFilesAnswersPathGlobalPropertiesProperty = DependencyProperty.Register("VisibilityTechnicalRequrementsFilesAnswersPathGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTechnicalRequrementsFilesAnswersPathGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityTechnicalRequrementsFilesAnswersPathGlobalPropertiesProperty); }
            set { SetValue(VisibilityTechnicalRequrementsFilesAnswersPathGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShippingCostFilesPathGlobalPropertiesProperty = DependencyProperty.Register("VisibilityShippingCostFilesPathGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShippingCostFilesPathGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityShippingCostFilesPathGlobalPropertiesProperty); }
            set { SetValue(VisibilityShippingCostFilesPathGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPriceCalculationsFilesPathGlobalPropertiesProperty = DependencyProperty.Register("VisibilityPriceCalculationsFilesPathGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriceCalculationsFilesPathGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityPriceCalculationsFilesPathGlobalPropertiesProperty); }
            set { SetValue(VisibilityPriceCalculationsFilesPathGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLogsPathGlobalPropertiesProperty = DependencyProperty.Register("VisibilityLogsPathGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLogsPathGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityLogsPathGlobalPropertiesProperty); }
            set { SetValue(VisibilityLogsPathGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDeveloperGlobalPropertiesProperty = DependencyProperty.Register("VisibilityDeveloperGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeveloperGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityDeveloperGlobalPropertiesProperty); }
            set { SetValue(VisibilityDeveloperGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLastDeveloperVizitGlobalPropertiesProperty = DependencyProperty.Register("VisibilityLastDeveloperVizitGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastDeveloperVizitGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityLastDeveloperVizitGlobalPropertiesProperty); }
            set { SetValue(VisibilityLastDeveloperVizitGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductIncludedDefaultGlobalPropertiesProperty = DependencyProperty.Register("VisibilityProductIncludedDefaultGlobalProperties", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductIncludedDefaultGlobalProperties
        {
            get { return (Visibility) GetValue(VisibilityProductIncludedDefaultGlobalPropertiesProperty); }
            set { SetValue(VisibilityProductIncludedDefaultGlobalPropertiesProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class AddressDetailsView : ViewBase
    {
        public AddressDetailsView()
        {
			InitializeComponent();
        }

        public AddressDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, AddressDetailsViewModel AddressDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = AddressDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Address).GetProperty(nameof(HVTApp.Model.POCOs.Address.Description)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDescriptionAddress = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Address).GetProperty(nameof(HVTApp.Model.POCOs.Address.Locality)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLocalityAddress = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDescriptionAddressProperty = DependencyProperty.Register("VisibilityDescriptionAddress", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDescriptionAddress
        {
            get { return (Visibility) GetValue(VisibilityDescriptionAddressProperty); }
            set { SetValue(VisibilityDescriptionAddressProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLocalityAddressProperty = DependencyProperty.Register("VisibilityLocalityAddress", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLocalityAddress
        {
            get { return (Visibility) GetValue(VisibilityLocalityAddressProperty); }
            set { SetValue(VisibilityLocalityAddressProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class CountryDetailsView : ViewBase
    {
        public CountryDetailsView()
        {
			InitializeComponent();
        }

        public CountryDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CountryDetailsViewModel CountryDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CountryDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Country).GetProperty(nameof(HVTApp.Model.POCOs.Country.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameCountry = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameCountryProperty = DependencyProperty.Register("VisibilityNameCountry", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameCountry
        {
            get { return (Visibility) GetValue(VisibilityNameCountryProperty); }
            set { SetValue(VisibilityNameCountryProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class DistrictDetailsView : ViewBase
    {
        public DistrictDetailsView()
        {
			InitializeComponent();
        }

        public DistrictDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DistrictDetailsViewModel DistrictDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DistrictDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.District).GetProperty(nameof(HVTApp.Model.POCOs.District.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameDistrict = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.District).GetProperty(nameof(HVTApp.Model.POCOs.District.Country)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCountryDistrict = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameDistrictProperty = DependencyProperty.Register("VisibilityNameDistrict", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameDistrict
        {
            get { return (Visibility) GetValue(VisibilityNameDistrictProperty); }
            set { SetValue(VisibilityNameDistrictProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCountryDistrictProperty = DependencyProperty.Register("VisibilityCountryDistrict", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCountryDistrict
        {
            get { return (Visibility) GetValue(VisibilityCountryDistrictProperty); }
            set { SetValue(VisibilityCountryDistrictProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class LocalityDetailsView : ViewBase
    {
        public LocalityDetailsView()
        {
			InitializeComponent();
        }

        public LocalityDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityDetailsViewModel LocalityDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = LocalityDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameLocality = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.LocalityType)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLocalityTypeLocality = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.Region)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRegionLocality = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsCountryCapital)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsCountryCapitalLocality = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsDistrictCapital)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsDistrictCapitalLocality = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsRegionCapital)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsRegionCapitalLocality = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.DistanceToEkb)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDistanceToEkbLocality = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameLocalityProperty = DependencyProperty.Register("VisibilityNameLocality", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameLocality
        {
            get { return (Visibility) GetValue(VisibilityNameLocalityProperty); }
            set { SetValue(VisibilityNameLocalityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLocalityTypeLocalityProperty = DependencyProperty.Register("VisibilityLocalityTypeLocality", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLocalityTypeLocality
        {
            get { return (Visibility) GetValue(VisibilityLocalityTypeLocalityProperty); }
            set { SetValue(VisibilityLocalityTypeLocalityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRegionLocalityProperty = DependencyProperty.Register("VisibilityRegionLocality", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegionLocality
        {
            get { return (Visibility) GetValue(VisibilityRegionLocalityProperty); }
            set { SetValue(VisibilityRegionLocalityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsCountryCapitalLocalityProperty = DependencyProperty.Register("VisibilityIsCountryCapitalLocality", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsCountryCapitalLocality
        {
            get { return (Visibility) GetValue(VisibilityIsCountryCapitalLocalityProperty); }
            set { SetValue(VisibilityIsCountryCapitalLocalityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsDistrictCapitalLocalityProperty = DependencyProperty.Register("VisibilityIsDistrictCapitalLocality", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsDistrictCapitalLocality
        {
            get { return (Visibility) GetValue(VisibilityIsDistrictCapitalLocalityProperty); }
            set { SetValue(VisibilityIsDistrictCapitalLocalityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsRegionCapitalLocalityProperty = DependencyProperty.Register("VisibilityIsRegionCapitalLocality", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsRegionCapitalLocality
        {
            get { return (Visibility) GetValue(VisibilityIsRegionCapitalLocalityProperty); }
            set { SetValue(VisibilityIsRegionCapitalLocalityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDistanceToEkbLocalityProperty = DependencyProperty.Register("VisibilityDistanceToEkbLocality", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDistanceToEkbLocality
        {
            get { return (Visibility) GetValue(VisibilityDistanceToEkbLocalityProperty); }
            set { SetValue(VisibilityDistanceToEkbLocalityProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class LocalityTypeDetailsView : ViewBase
    {
        public LocalityTypeDetailsView()
        {
			InitializeComponent();
        }

        public LocalityTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, LocalityTypeDetailsViewModel LocalityTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = LocalityTypeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.LocalityType).GetProperty(nameof(HVTApp.Model.POCOs.LocalityType.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFullNameLocalityType = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.LocalityType).GetProperty(nameof(HVTApp.Model.POCOs.LocalityType.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShortNameLocalityType = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityFullNameLocalityTypeProperty = DependencyProperty.Register("VisibilityFullNameLocalityType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullNameLocalityType
        {
            get { return (Visibility) GetValue(VisibilityFullNameLocalityTypeProperty); }
            set { SetValue(VisibilityFullNameLocalityTypeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShortNameLocalityTypeProperty = DependencyProperty.Register("VisibilityShortNameLocalityType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortNameLocalityType
        {
            get { return (Visibility) GetValue(VisibilityShortNameLocalityTypeProperty); }
            set { SetValue(VisibilityShortNameLocalityTypeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class RegionDetailsView : ViewBase
    {
        public RegionDetailsView()
        {
			InitializeComponent();
        }

        public RegionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, RegionDetailsViewModel RegionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = RegionDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Region).GetProperty(nameof(HVTApp.Model.POCOs.Region.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameRegion = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Region).GetProperty(nameof(HVTApp.Model.POCOs.Region.District)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDistrictRegion = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameRegionProperty = DependencyProperty.Register("VisibilityNameRegion", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameRegion
        {
            get { return (Visibility) GetValue(VisibilityNameRegionProperty); }
            set { SetValue(VisibilityNameRegionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDistrictRegionProperty = DependencyProperty.Register("VisibilityDistrictRegion", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDistrictRegion
        {
            get { return (Visibility) GetValue(VisibilityDistrictRegionProperty); }
            set { SetValue(VisibilityDistrictRegionProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class SumDetailsView : ViewBase
    {
        public SumDetailsView()
        {
			InitializeComponent();
        }

        public SumDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SumDetailsViewModel SumDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = SumDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Sum).GetProperty(nameof(HVTApp.Model.POCOs.Sum.Type)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTypeSum = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Sum).GetProperty(nameof(HVTApp.Model.POCOs.Sum.Currency)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCurrencySum = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Sum).GetProperty(nameof(HVTApp.Model.POCOs.Sum.Value)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityValueSum = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityTypeSumProperty = DependencyProperty.Register("VisibilityTypeSum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTypeSum
        {
            get { return (Visibility) GetValue(VisibilityTypeSumProperty); }
            set { SetValue(VisibilityTypeSumProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCurrencySumProperty = DependencyProperty.Register("VisibilityCurrencySum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCurrencySum
        {
            get { return (Visibility) GetValue(VisibilityCurrencySumProperty); }
            set { SetValue(VisibilityCurrencySumProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityValueSumProperty = DependencyProperty.Register("VisibilityValueSum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityValueSum
        {
            get { return (Visibility) GetValue(VisibilityValueSumProperty); }
            set { SetValue(VisibilityValueSumProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class CurrencyExchangeRateDetailsView : ViewBase
    {
        public CurrencyExchangeRateDetailsView()
        {
			InitializeComponent();
        }

        public CurrencyExchangeRateDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CurrencyExchangeRateDetailsViewModel CurrencyExchangeRateDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CurrencyExchangeRateDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateCurrencyExchangeRate = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.FirstCurrency)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFirstCurrencyCurrencyExchangeRate = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.SecondCurrency)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySecondCurrencyCurrencyExchangeRate = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.ExchangeRate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityExchangeRateCurrencyExchangeRate = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateCurrencyExchangeRateProperty = DependencyProperty.Register("VisibilityDateCurrencyExchangeRate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateCurrencyExchangeRate
        {
            get { return (Visibility) GetValue(VisibilityDateCurrencyExchangeRateProperty); }
            set { SetValue(VisibilityDateCurrencyExchangeRateProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFirstCurrencyCurrencyExchangeRateProperty = DependencyProperty.Register("VisibilityFirstCurrencyCurrencyExchangeRate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFirstCurrencyCurrencyExchangeRate
        {
            get { return (Visibility) GetValue(VisibilityFirstCurrencyCurrencyExchangeRateProperty); }
            set { SetValue(VisibilityFirstCurrencyCurrencyExchangeRateProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySecondCurrencyCurrencyExchangeRateProperty = DependencyProperty.Register("VisibilitySecondCurrencyCurrencyExchangeRate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySecondCurrencyCurrencyExchangeRate
        {
            get { return (Visibility) GetValue(VisibilitySecondCurrencyCurrencyExchangeRateProperty); }
            set { SetValue(VisibilitySecondCurrencyCurrencyExchangeRateProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityExchangeRateCurrencyExchangeRateProperty = DependencyProperty.Register("VisibilityExchangeRateCurrencyExchangeRate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityExchangeRateCurrencyExchangeRate
        {
            get { return (Visibility) GetValue(VisibilityExchangeRateCurrencyExchangeRateProperty); }
            set { SetValue(VisibilityExchangeRateCurrencyExchangeRateProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class NoteDetailsView : ViewBase
    {
        public NoteDetailsView()
        {
			InitializeComponent();
        }

        public NoteDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, NoteDetailsViewModel NoteDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = NoteDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Note).GetProperty(nameof(HVTApp.Model.POCOs.Note.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateNote = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Note).GetProperty(nameof(HVTApp.Model.POCOs.Note.Text)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTextNote = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Note).GetProperty(nameof(HVTApp.Model.POCOs.Note.IsImportant)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsImportantNote = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateNoteProperty = DependencyProperty.Register("VisibilityDateNote", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateNote
        {
            get { return (Visibility) GetValue(VisibilityDateNoteProperty); }
            set { SetValue(VisibilityDateNoteProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTextNoteProperty = DependencyProperty.Register("VisibilityTextNote", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTextNote
        {
            get { return (Visibility) GetValue(VisibilityTextNoteProperty); }
            set { SetValue(VisibilityTextNoteProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsImportantNoteProperty = DependencyProperty.Register("VisibilityIsImportantNote", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsImportantNote
        {
            get { return (Visibility) GetValue(VisibilityIsImportantNoteProperty); }
            set { SetValue(VisibilityIsImportantNoteProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class OfferUnitDetailsView : ViewBase
    {
        public OfferUnitDetailsView()
        {
			InitializeComponent();
        }

        public OfferUnitDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferUnitDetailsViewModel OfferUnitDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = OfferUnitDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Cost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.CostDelivery)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostDeliveryOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.CostDeliveryIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostDeliveryIncludedOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Offer)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOfferOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Facility)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFacilityOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Product)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionSetOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductionTermOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.ProductsIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductsIncludedOfferUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentOfferUnit = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityCostOfferUnitProperty = DependencyProperty.Register("VisibilityCostOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityCostOfferUnitProperty); }
            set { SetValue(VisibilityCostOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCostDeliveryOfferUnitProperty = DependencyProperty.Register("VisibilityCostDeliveryOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostDeliveryOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityCostDeliveryOfferUnitProperty); }
            set { SetValue(VisibilityCostDeliveryOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCostDeliveryIncludedOfferUnitProperty = DependencyProperty.Register("VisibilityCostDeliveryIncludedOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostDeliveryIncludedOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityCostDeliveryIncludedOfferUnitProperty); }
            set { SetValue(VisibilityCostDeliveryIncludedOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOfferOfferUnitProperty = DependencyProperty.Register("VisibilityOfferOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOfferOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityOfferOfferUnitProperty); }
            set { SetValue(VisibilityOfferOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFacilityOfferUnitProperty = DependencyProperty.Register("VisibilityFacilityOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFacilityOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityFacilityOfferUnitProperty); }
            set { SetValue(VisibilityFacilityOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductOfferUnitProperty = DependencyProperty.Register("VisibilityProductOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityProductOfferUnitProperty); }
            set { SetValue(VisibilityProductOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentConditionSetOfferUnitProperty = DependencyProperty.Register("VisibilityPaymentConditionSetOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionSetOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionSetOfferUnitProperty); }
            set { SetValue(VisibilityPaymentConditionSetOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductionTermOfferUnitProperty = DependencyProperty.Register("VisibilityProductionTermOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductionTermOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityProductionTermOfferUnitProperty); }
            set { SetValue(VisibilityProductionTermOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductsIncludedOfferUnitProperty = DependencyProperty.Register("VisibilityProductsIncludedOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductsIncludedOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityProductsIncludedOfferUnitProperty); }
            set { SetValue(VisibilityProductsIncludedOfferUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentOfferUnitProperty = DependencyProperty.Register("VisibilityCommentOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityCommentOfferUnitProperty); }
            set { SetValue(VisibilityCommentOfferUnitProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PaymentConditionSetDetailsView : ViewBase
    {
        public PaymentConditionSetDetailsView()
        {
			InitializeComponent();
        }

        public PaymentConditionSetDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionSetDetailsViewModel PaymentConditionSetDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PaymentConditionSetDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PaymentConditionSet).GetProperty(nameof(HVTApp.Model.POCOs.PaymentConditionSet.PaymentConditions)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionsPaymentConditionSet = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityPaymentConditionsPaymentConditionSetProperty = DependencyProperty.Register("VisibilityPaymentConditionsPaymentConditionSet", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionsPaymentConditionSet
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionsPaymentConditionSetProperty); }
            set { SetValue(VisibilityPaymentConditionsPaymentConditionSetProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductBlockDetailsView : ViewBase
    {
        public ProductBlockDetailsView()
        {
			InitializeComponent();
        }

        public ProductBlockDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockDetailsViewModel ProductBlockDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductBlockDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Designation)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesignationProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.DesignationSpecial)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesignationSpecialProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.ProductType)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductTypeProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Prices)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPricesProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.FixedCosts)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFixedCostsProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.StructureCostNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStructureCostNumberProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Design)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesignProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Weight)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityWeightProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.HasPrice)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHasPriceProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.LastPriceDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLastPriceDateProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.HasFixedPrice)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHasFixedPriceProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.IsNew)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsNewProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.IsService)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsServiceProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.IsSupervision)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsSupervisionProductBlock = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.IsDelivery)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsDeliveryProductBlock = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDesignationProductBlockProperty = DependencyProperty.Register("VisibilityDesignationProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignationProductBlock
        {
            get { return (Visibility) GetValue(VisibilityDesignationProductBlockProperty); }
            set { SetValue(VisibilityDesignationProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDesignationSpecialProductBlockProperty = DependencyProperty.Register("VisibilityDesignationSpecialProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignationSpecialProductBlock
        {
            get { return (Visibility) GetValue(VisibilityDesignationSpecialProductBlockProperty); }
            set { SetValue(VisibilityDesignationSpecialProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductTypeProductBlockProperty = DependencyProperty.Register("VisibilityProductTypeProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductTypeProductBlock
        {
            get { return (Visibility) GetValue(VisibilityProductTypeProductBlockProperty); }
            set { SetValue(VisibilityProductTypeProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParametersProductBlockProperty = DependencyProperty.Register("VisibilityParametersProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersProductBlock
        {
            get { return (Visibility) GetValue(VisibilityParametersProductBlockProperty); }
            set { SetValue(VisibilityParametersProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPricesProductBlockProperty = DependencyProperty.Register("VisibilityPricesProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPricesProductBlock
        {
            get { return (Visibility) GetValue(VisibilityPricesProductBlockProperty); }
            set { SetValue(VisibilityPricesProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFixedCostsProductBlockProperty = DependencyProperty.Register("VisibilityFixedCostsProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFixedCostsProductBlock
        {
            get { return (Visibility) GetValue(VisibilityFixedCostsProductBlockProperty); }
            set { SetValue(VisibilityFixedCostsProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStructureCostNumberProductBlockProperty = DependencyProperty.Register("VisibilityStructureCostNumberProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStructureCostNumberProductBlock
        {
            get { return (Visibility) GetValue(VisibilityStructureCostNumberProductBlockProperty); }
            set { SetValue(VisibilityStructureCostNumberProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDesignProductBlockProperty = DependencyProperty.Register("VisibilityDesignProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignProductBlock
        {
            get { return (Visibility) GetValue(VisibilityDesignProductBlockProperty); }
            set { SetValue(VisibilityDesignProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityWeightProductBlockProperty = DependencyProperty.Register("VisibilityWeightProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityWeightProductBlock
        {
            get { return (Visibility) GetValue(VisibilityWeightProductBlockProperty); }
            set { SetValue(VisibilityWeightProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityHasPriceProductBlockProperty = DependencyProperty.Register("VisibilityHasPriceProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHasPriceProductBlock
        {
            get { return (Visibility) GetValue(VisibilityHasPriceProductBlockProperty); }
            set { SetValue(VisibilityHasPriceProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLastPriceDateProductBlockProperty = DependencyProperty.Register("VisibilityLastPriceDateProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastPriceDateProductBlock
        {
            get { return (Visibility) GetValue(VisibilityLastPriceDateProductBlockProperty); }
            set { SetValue(VisibilityLastPriceDateProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityHasFixedPriceProductBlockProperty = DependencyProperty.Register("VisibilityHasFixedPriceProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHasFixedPriceProductBlock
        {
            get { return (Visibility) GetValue(VisibilityHasFixedPriceProductBlockProperty); }
            set { SetValue(VisibilityHasFixedPriceProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsNewProductBlockProperty = DependencyProperty.Register("VisibilityIsNewProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsNewProductBlock
        {
            get { return (Visibility) GetValue(VisibilityIsNewProductBlockProperty); }
            set { SetValue(VisibilityIsNewProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsServiceProductBlockProperty = DependencyProperty.Register("VisibilityIsServiceProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsServiceProductBlock
        {
            get { return (Visibility) GetValue(VisibilityIsServiceProductBlockProperty); }
            set { SetValue(VisibilityIsServiceProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsSupervisionProductBlockProperty = DependencyProperty.Register("VisibilityIsSupervisionProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsSupervisionProductBlock
        {
            get { return (Visibility) GetValue(VisibilityIsSupervisionProductBlockProperty); }
            set { SetValue(VisibilityIsSupervisionProductBlockProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsDeliveryProductBlockProperty = DependencyProperty.Register("VisibilityIsDeliveryProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsDeliveryProductBlock
        {
            get { return (Visibility) GetValue(VisibilityIsDeliveryProductBlockProperty); }
            set { SetValue(VisibilityIsDeliveryProductBlockProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductDependentDetailsView : ViewBase
    {
        public ProductDependentDetailsView()
        {
			InitializeComponent();
        }

        public ProductDependentDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductDependentDetailsViewModel ProductDependentDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductDependentDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductDependent).GetProperty(nameof(HVTApp.Model.POCOs.ProductDependent.MainProductId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMainProductIdProductDependent = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductDependent).GetProperty(nameof(HVTApp.Model.POCOs.ProductDependent.Product)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductProductDependent = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductDependent).GetProperty(nameof(HVTApp.Model.POCOs.ProductDependent.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAmountProductDependent = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityMainProductIdProductDependentProperty = DependencyProperty.Register("VisibilityMainProductIdProductDependent", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMainProductIdProductDependent
        {
            get { return (Visibility) GetValue(VisibilityMainProductIdProductDependentProperty); }
            set { SetValue(VisibilityMainProductIdProductDependentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductProductDependentProperty = DependencyProperty.Register("VisibilityProductProductDependent", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductProductDependent
        {
            get { return (Visibility) GetValue(VisibilityProductProductDependentProperty); }
            set { SetValue(VisibilityProductProductDependentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAmountProductDependentProperty = DependencyProperty.Register("VisibilityAmountProductDependent", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmountProductDependent
        {
            get { return (Visibility) GetValue(VisibilityAmountProductDependentProperty); }
            set { SetValue(VisibilityAmountProductDependentProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class BankDetailsDetailsView : ViewBase
    {
        public BankDetailsDetailsView()
        {
			InitializeComponent();
        }

        public BankDetailsDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, BankDetailsDetailsViewModel BankDetailsDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = BankDetailsDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.BankName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityBankNameBankDetails = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.BankIdentificationCode)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityBankIdentificationCodeBankDetails = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.CorrespondentAccount)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCorrespondentAccountBankDetails = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.CheckingAccount)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCheckingAccountBankDetails = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityBankNameBankDetailsProperty = DependencyProperty.Register("VisibilityBankNameBankDetails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBankNameBankDetails
        {
            get { return (Visibility) GetValue(VisibilityBankNameBankDetailsProperty); }
            set { SetValue(VisibilityBankNameBankDetailsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityBankIdentificationCodeBankDetailsProperty = DependencyProperty.Register("VisibilityBankIdentificationCodeBankDetails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBankIdentificationCodeBankDetails
        {
            get { return (Visibility) GetValue(VisibilityBankIdentificationCodeBankDetailsProperty); }
            set { SetValue(VisibilityBankIdentificationCodeBankDetailsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCorrespondentAccountBankDetailsProperty = DependencyProperty.Register("VisibilityCorrespondentAccountBankDetails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCorrespondentAccountBankDetails
        {
            get { return (Visibility) GetValue(VisibilityCorrespondentAccountBankDetailsProperty); }
            set { SetValue(VisibilityCorrespondentAccountBankDetailsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCheckingAccountBankDetailsProperty = DependencyProperty.Register("VisibilityCheckingAccountBankDetails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCheckingAccountBankDetails
        {
            get { return (Visibility) GetValue(VisibilityCheckingAccountBankDetailsProperty); }
            set { SetValue(VisibilityCheckingAccountBankDetailsProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class CompanyDetailsView : ViewBase
    {
        public CompanyDetailsView()
        {
			InitializeComponent();
        }

        public CompanyDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyDetailsViewModel CompanyDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CompanyDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFullNameCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShortNameCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Inn)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityInnCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Kpp)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityKppCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Form)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFormCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ParentCompany)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParentCompanyCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.AddressLegal)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAddressLegalCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.AddressPost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAddressPostCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.BankDetailsList)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityBankDetailsListCompany = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ActivityFilds)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityActivityFildsCompany = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityFullNameCompanyProperty = DependencyProperty.Register("VisibilityFullNameCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullNameCompany
        {
            get { return (Visibility) GetValue(VisibilityFullNameCompanyProperty); }
            set { SetValue(VisibilityFullNameCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShortNameCompanyProperty = DependencyProperty.Register("VisibilityShortNameCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortNameCompany
        {
            get { return (Visibility) GetValue(VisibilityShortNameCompanyProperty); }
            set { SetValue(VisibilityShortNameCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityInnCompanyProperty = DependencyProperty.Register("VisibilityInnCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityInnCompany
        {
            get { return (Visibility) GetValue(VisibilityInnCompanyProperty); }
            set { SetValue(VisibilityInnCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityKppCompanyProperty = DependencyProperty.Register("VisibilityKppCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityKppCompany
        {
            get { return (Visibility) GetValue(VisibilityKppCompanyProperty); }
            set { SetValue(VisibilityKppCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityFormCompanyProperty = DependencyProperty.Register("VisibilityFormCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFormCompany
        {
            get { return (Visibility) GetValue(VisibilityFormCompanyProperty); }
            set { SetValue(VisibilityFormCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParentCompanyCompanyProperty = DependencyProperty.Register("VisibilityParentCompanyCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentCompanyCompany
        {
            get { return (Visibility) GetValue(VisibilityParentCompanyCompanyProperty); }
            set { SetValue(VisibilityParentCompanyCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAddressLegalCompanyProperty = DependencyProperty.Register("VisibilityAddressLegalCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddressLegalCompany
        {
            get { return (Visibility) GetValue(VisibilityAddressLegalCompanyProperty); }
            set { SetValue(VisibilityAddressLegalCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAddressPostCompanyProperty = DependencyProperty.Register("VisibilityAddressPostCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddressPostCompany
        {
            get { return (Visibility) GetValue(VisibilityAddressPostCompanyProperty); }
            set { SetValue(VisibilityAddressPostCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityBankDetailsListCompanyProperty = DependencyProperty.Register("VisibilityBankDetailsListCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBankDetailsListCompany
        {
            get { return (Visibility) GetValue(VisibilityBankDetailsListCompanyProperty); }
            set { SetValue(VisibilityBankDetailsListCompanyProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityActivityFildsCompanyProperty = DependencyProperty.Register("VisibilityActivityFildsCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActivityFildsCompany
        {
            get { return (Visibility) GetValue(VisibilityActivityFildsCompanyProperty); }
            set { SetValue(VisibilityActivityFildsCompanyProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class CompanyFormDetailsView : ViewBase
    {
        public CompanyFormDetailsView()
        {
			InitializeComponent();
        }

        public CompanyFormDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CompanyFormDetailsViewModel CompanyFormDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CompanyFormDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.CompanyForm).GetProperty(nameof(HVTApp.Model.POCOs.CompanyForm.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFullNameCompanyForm = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.CompanyForm).GetProperty(nameof(HVTApp.Model.POCOs.CompanyForm.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShortNameCompanyForm = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityFullNameCompanyFormProperty = DependencyProperty.Register("VisibilityFullNameCompanyForm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullNameCompanyForm
        {
            get { return (Visibility) GetValue(VisibilityFullNameCompanyFormProperty); }
            set { SetValue(VisibilityFullNameCompanyFormProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShortNameCompanyFormProperty = DependencyProperty.Register("VisibilityShortNameCompanyForm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortNameCompanyForm
        {
            get { return (Visibility) GetValue(VisibilityShortNameCompanyFormProperty); }
            set { SetValue(VisibilityShortNameCompanyFormProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class DocumentsRegistrationDetailsDetailsView : ViewBase
    {
        public DocumentsRegistrationDetailsDetailsView()
        {
			InitializeComponent();
        }

        public DocumentsRegistrationDetailsDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentsRegistrationDetailsDetailsViewModel DocumentsRegistrationDetailsDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DocumentsRegistrationDetailsDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.DocumentsRegistrationDetails).GetProperty(nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateDocumentsRegistrationDetails = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.DocumentsRegistrationDetails).GetProperty(nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberDocumentsRegistrationDetails = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateDocumentsRegistrationDetailsProperty = DependencyProperty.Register("VisibilityDateDocumentsRegistrationDetails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateDocumentsRegistrationDetails
        {
            get { return (Visibility) GetValue(VisibilityDateDocumentsRegistrationDetailsProperty); }
            set { SetValue(VisibilityDateDocumentsRegistrationDetailsProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNumberDocumentsRegistrationDetailsProperty = DependencyProperty.Register("VisibilityNumberDocumentsRegistrationDetails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberDocumentsRegistrationDetails
        {
            get { return (Visibility) GetValue(VisibilityNumberDocumentsRegistrationDetailsProperty); }
            set { SetValue(VisibilityNumberDocumentsRegistrationDetailsProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class EmployeesPositionDetailsView : ViewBase
    {
        public EmployeesPositionDetailsView()
        {
			InitializeComponent();
        }

        public EmployeesPositionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeesPositionDetailsViewModel EmployeesPositionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = EmployeesPositionDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.EmployeesPosition).GetProperty(nameof(HVTApp.Model.POCOs.EmployeesPosition.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameEmployeesPosition = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameEmployeesPositionProperty = DependencyProperty.Register("VisibilityNameEmployeesPosition", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameEmployeesPosition
        {
            get { return (Visibility) GetValue(VisibilityNameEmployeesPositionProperty); }
            set { SetValue(VisibilityNameEmployeesPositionProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class FacilityTypeDetailsView : ViewBase
    {
        public FacilityTypeDetailsView()
        {
			InitializeComponent();
        }

        public FacilityTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityTypeDetailsViewModel FacilityTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = FacilityTypeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.FacilityType).GetProperty(nameof(HVTApp.Model.POCOs.FacilityType.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFullNameFacilityType = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.FacilityType).GetProperty(nameof(HVTApp.Model.POCOs.FacilityType.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShortNameFacilityType = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityFullNameFacilityTypeProperty = DependencyProperty.Register("VisibilityFullNameFacilityType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullNameFacilityType
        {
            get { return (Visibility) GetValue(VisibilityFullNameFacilityTypeProperty); }
            set { SetValue(VisibilityFullNameFacilityTypeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShortNameFacilityTypeProperty = DependencyProperty.Register("VisibilityShortNameFacilityType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortNameFacilityType
        {
            get { return (Visibility) GetValue(VisibilityShortNameFacilityTypeProperty); }
            set { SetValue(VisibilityShortNameFacilityTypeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ActivityFieldDetailsView : ViewBase
    {
        public ActivityFieldDetailsView()
        {
			InitializeComponent();
        }

        public ActivityFieldDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ActivityFieldDetailsViewModel ActivityFieldDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ActivityFieldDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ActivityField).GetProperty(nameof(HVTApp.Model.POCOs.ActivityField.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameActivityField = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ActivityField).GetProperty(nameof(HVTApp.Model.POCOs.ActivityField.ActivityFieldEnum)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityActivityFieldEnumActivityField = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameActivityFieldProperty = DependencyProperty.Register("VisibilityNameActivityField", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameActivityField
        {
            get { return (Visibility) GetValue(VisibilityNameActivityFieldProperty); }
            set { SetValue(VisibilityNameActivityFieldProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityActivityFieldEnumActivityFieldProperty = DependencyProperty.Register("VisibilityActivityFieldEnumActivityField", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActivityFieldEnumActivityField
        {
            get { return (Visibility) GetValue(VisibilityActivityFieldEnumActivityFieldProperty); }
            set { SetValue(VisibilityActivityFieldEnumActivityFieldProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ContractDetailsView : ViewBase
    {
        public ContractDetailsView()
        {
			InitializeComponent();
        }

        public ContractDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ContractDetailsViewModel ContractDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ContractDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Contract).GetProperty(nameof(HVTApp.Model.POCOs.Contract.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberContract = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Contract).GetProperty(nameof(HVTApp.Model.POCOs.Contract.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateContract = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Contract).GetProperty(nameof(HVTApp.Model.POCOs.Contract.Contragent)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityContragentContract = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNumberContractProperty = DependencyProperty.Register("VisibilityNumberContract", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberContract
        {
            get { return (Visibility) GetValue(VisibilityNumberContractProperty); }
            set { SetValue(VisibilityNumberContractProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateContractProperty = DependencyProperty.Register("VisibilityDateContract", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateContract
        {
            get { return (Visibility) GetValue(VisibilityDateContractProperty); }
            set { SetValue(VisibilityDateContractProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityContragentContractProperty = DependencyProperty.Register("VisibilityContragentContract", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityContragentContract
        {
            get { return (Visibility) GetValue(VisibilityContragentContractProperty); }
            set { SetValue(VisibilityContragentContractProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class MeasureDetailsView : ViewBase
    {
        public MeasureDetailsView()
        {
			InitializeComponent();
        }

        public MeasureDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, MeasureDetailsViewModel MeasureDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = MeasureDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Measure).GetProperty(nameof(HVTApp.Model.POCOs.Measure.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFullNameMeasure = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Measure).GetProperty(nameof(HVTApp.Model.POCOs.Measure.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShortNameMeasure = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityFullNameMeasureProperty = DependencyProperty.Register("VisibilityFullNameMeasure", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullNameMeasure
        {
            get { return (Visibility) GetValue(VisibilityFullNameMeasureProperty); }
            set { SetValue(VisibilityFullNameMeasureProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShortNameMeasureProperty = DependencyProperty.Register("VisibilityShortNameMeasure", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortNameMeasure
        {
            get { return (Visibility) GetValue(VisibilityShortNameMeasureProperty); }
            set { SetValue(VisibilityShortNameMeasureProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ParameterDetailsView : ViewBase
    {
        public ParameterDetailsView()
        {
			InitializeComponent();
        }

        public ParameterDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterDetailsViewModel ParameterDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ParameterDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.ParameterGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParameterGroupParameter = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.Value)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityValueParameter = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.ParameterRelations)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParameterRelationsParameter = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.Rang)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRangParameter = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentParameter = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.IsOrigin)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsOriginParameter = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityParameterGroupParameterProperty = DependencyProperty.Register("VisibilityParameterGroupParameter", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameterGroupParameter
        {
            get { return (Visibility) GetValue(VisibilityParameterGroupParameterProperty); }
            set { SetValue(VisibilityParameterGroupParameterProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityValueParameterProperty = DependencyProperty.Register("VisibilityValueParameter", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityValueParameter
        {
            get { return (Visibility) GetValue(VisibilityValueParameterProperty); }
            set { SetValue(VisibilityValueParameterProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParameterRelationsParameterProperty = DependencyProperty.Register("VisibilityParameterRelationsParameter", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameterRelationsParameter
        {
            get { return (Visibility) GetValue(VisibilityParameterRelationsParameterProperty); }
            set { SetValue(VisibilityParameterRelationsParameterProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRangParameterProperty = DependencyProperty.Register("VisibilityRangParameter", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRangParameter
        {
            get { return (Visibility) GetValue(VisibilityRangParameterProperty); }
            set { SetValue(VisibilityRangParameterProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentParameterProperty = DependencyProperty.Register("VisibilityCommentParameter", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentParameter
        {
            get { return (Visibility) GetValue(VisibilityCommentParameterProperty); }
            set { SetValue(VisibilityCommentParameterProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsOriginParameterProperty = DependencyProperty.Register("VisibilityIsOriginParameter", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsOriginParameter
        {
            get { return (Visibility) GetValue(VisibilityIsOriginParameterProperty); }
            set { SetValue(VisibilityIsOriginParameterProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ParameterGroupDetailsView : ViewBase
    {
        public ParameterGroupDetailsView()
        {
			InitializeComponent();
        }

        public ParameterGroupDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterGroupDetailsViewModel ParameterGroupDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ParameterGroupDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ParameterGroup).GetProperty(nameof(HVTApp.Model.POCOs.ParameterGroup.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameParameterGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ParameterGroup).GetProperty(nameof(HVTApp.Model.POCOs.ParameterGroup.Measure)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityMeasureParameterGroup = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ParameterGroup).GetProperty(nameof(HVTApp.Model.POCOs.ParameterGroup.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentParameterGroup = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameParameterGroupProperty = DependencyProperty.Register("VisibilityNameParameterGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameParameterGroup
        {
            get { return (Visibility) GetValue(VisibilityNameParameterGroupProperty); }
            set { SetValue(VisibilityNameParameterGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityMeasureParameterGroupProperty = DependencyProperty.Register("VisibilityMeasureParameterGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMeasureParameterGroup
        {
            get { return (Visibility) GetValue(VisibilityMeasureParameterGroupProperty); }
            set { SetValue(VisibilityMeasureParameterGroupProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentParameterGroupProperty = DependencyProperty.Register("VisibilityCommentParameterGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentParameterGroup
        {
            get { return (Visibility) GetValue(VisibilityCommentParameterGroupProperty); }
            set { SetValue(VisibilityCommentParameterGroupProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductRelationDetailsView : ViewBase
    {
        public ProductRelationDetailsView()
        {
			InitializeComponent();
        }

        public ProductRelationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductRelationDetailsViewModel ProductRelationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductRelationDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameProductRelation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.ParentProductParameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParentProductParametersProductRelation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.ChildProductParameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityChildProductParametersProductRelation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.ChildProductsAmount)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityChildProductsAmountProductRelation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.IsUnique)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsUniqueProductRelation = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameProductRelationProperty = DependencyProperty.Register("VisibilityNameProductRelation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameProductRelation
        {
            get { return (Visibility) GetValue(VisibilityNameProductRelationProperty); }
            set { SetValue(VisibilityNameProductRelationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParentProductParametersProductRelationProperty = DependencyProperty.Register("VisibilityParentProductParametersProductRelation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentProductParametersProductRelation
        {
            get { return (Visibility) GetValue(VisibilityParentProductParametersProductRelationProperty); }
            set { SetValue(VisibilityParentProductParametersProductRelationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityChildProductParametersProductRelationProperty = DependencyProperty.Register("VisibilityChildProductParametersProductRelation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildProductParametersProductRelation
        {
            get { return (Visibility) GetValue(VisibilityChildProductParametersProductRelationProperty); }
            set { SetValue(VisibilityChildProductParametersProductRelationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityChildProductsAmountProductRelationProperty = DependencyProperty.Register("VisibilityChildProductsAmountProductRelation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildProductsAmountProductRelation
        {
            get { return (Visibility) GetValue(VisibilityChildProductsAmountProductRelationProperty); }
            set { SetValue(VisibilityChildProductsAmountProductRelationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsUniqueProductRelationProperty = DependencyProperty.Register("VisibilityIsUniqueProductRelation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsUniqueProductRelation
        {
            get { return (Visibility) GetValue(VisibilityIsUniqueProductRelationProperty); }
            set { SetValue(VisibilityIsUniqueProductRelationProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PersonDetailsView : ViewBase
    {
        public PersonDetailsView()
        {
			InitializeComponent();
        }

        public PersonDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PersonDetailsViewModel PersonDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PersonDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.Surname)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySurnamePerson = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNamePerson = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.Patronymic)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPatronymicPerson = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.IsMan)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsManPerson = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilitySurnamePersonProperty = DependencyProperty.Register("VisibilitySurnamePerson", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySurnamePerson
        {
            get { return (Visibility) GetValue(VisibilitySurnamePersonProperty); }
            set { SetValue(VisibilitySurnamePersonProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNamePersonProperty = DependencyProperty.Register("VisibilityNamePerson", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNamePerson
        {
            get { return (Visibility) GetValue(VisibilityNamePersonProperty); }
            set { SetValue(VisibilityNamePersonProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPatronymicPersonProperty = DependencyProperty.Register("VisibilityPatronymicPerson", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPatronymicPerson
        {
            get { return (Visibility) GetValue(VisibilityPatronymicPersonProperty); }
            set { SetValue(VisibilityPatronymicPersonProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsManPersonProperty = DependencyProperty.Register("VisibilityIsManPerson", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsManPerson
        {
            get { return (Visibility) GetValue(VisibilityIsManPersonProperty); }
            set { SetValue(VisibilityIsManPersonProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ParameterRelationDetailsView : ViewBase
    {
        public ParameterRelationDetailsView()
        {
			InitializeComponent();
        }

        public ParameterRelationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ParameterRelationDetailsViewModel ParameterRelationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ParameterRelationDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.ParameterRelation).GetProperty(nameof(HVTApp.Model.POCOs.ParameterRelation.RequiredParameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRequiredParametersParameterRelation = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.ParameterRelation).GetProperty(nameof(HVTApp.Model.POCOs.ParameterRelation.ParameterId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParameterIdParameterRelation = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityRequiredParametersParameterRelationProperty = DependencyProperty.Register("VisibilityRequiredParametersParameterRelation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRequiredParametersParameterRelation
        {
            get { return (Visibility) GetValue(VisibilityRequiredParametersParameterRelationProperty); }
            set { SetValue(VisibilityRequiredParametersParameterRelationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParameterIdParameterRelationProperty = DependencyProperty.Register("VisibilityParameterIdParameterRelation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameterIdParameterRelation
        {
            get { return (Visibility) GetValue(VisibilityParameterIdParameterRelationProperty); }
            set { SetValue(VisibilityParameterIdParameterRelationProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class SalesUnitDetailsView : ViewBase
    {
        public SalesUnitDetailsView()
        {
			InitializeComponent();
        }

        public SalesUnitDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesUnitDetailsViewModel SalesUnitDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = SalesUnitDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Facility)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFacilitySalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Product)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Cost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Price)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPriceSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.LaborHours)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLaborHoursSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ProductsIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductsIncludedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionSetSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductionTermSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Project)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProjectSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDateExpected)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDeliveryDateExpectedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Producer)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProducerSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.LosingReasons)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLosingReasonsSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.RealizationDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRealizationDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Order)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderPosition)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderPositionSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SerialNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySerialNumberSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AssembleTerm)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAssembleTermSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SignalToStartProduction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySignalToStartProductionSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SignalToStartProductionDone)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySignalToStartProductionDoneSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartProductionDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PickingDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPickingDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionPlanDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEndProductionPlanDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEndProductionDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Specification)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySpecificationSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsActual)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentsActualSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsPlanned)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentsPlannedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Penalty)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPenaltySalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.CostDelivery)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostDeliverySalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.CostDeliveryIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostDeliveryIncludedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ExpectedDeliveryPeriod)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityExpectedDeliveryPeriodSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ExpectedDeliveryPeriodCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityExpectedDeliveryPeriodCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AddressDelivery)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAddressDeliverySalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShipmentDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentPlanDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShipmentPlanDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDeliveryDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.IsRemoved)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsRemovedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.BankGuarantees)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityBankGuaranteesSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AllowEditCost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAllowEditCostSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AllowEditProduct)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAllowEditProductSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.IsLoosen)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsLoosenSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.IsWon)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsWonSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.IsDone)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsDoneSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ActualPriceCalculationItemId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityActualPriceCalculationItemIdSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderIsTaken)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderIsTakenSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderIsRealized)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderIsRealizedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AllowTotalRemove)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAllowTotalRemoveSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AddressDeliveryCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAddressDeliveryCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.IsPaid)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsPaidSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumPaid)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumPaidSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumNotPaid)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumNotPaidSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityVatSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumNotPaidWithVat)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumNotPaidWithVatSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumToStartProduction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumToStartProductionSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumToShipping)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumToShippingSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderInTakeDateInjected)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderInTakeDateInjectedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderInTakeDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderInTakeDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderInTakeYear)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderInTakeYearSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderInTakeMonth)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOrderInTakeMonthSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionConditionsDoneDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartProductionConditionsDoneDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShippingConditionsDoneDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShippingConditionsDoneDateSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDateInjected)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartProductionDateInjectedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartProductionDateCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEndProductionDateCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDateByContractCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEndProductionDateByContractCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.RealizationDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRealizationDateCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShipmentDateCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDeliveryDateCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryPeriodCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDeliveryPeriodCalculatedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsPlannedActual)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentsPlannedActualSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsPlannedGenerated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentsPlannedGeneratedSalesUnit = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsPlannedCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentsPlannedCalculatedSalesUnit = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityFacilitySalesUnitProperty = DependencyProperty.Register("VisibilityFacilitySalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFacilitySalesUnit
        {
            get { return (Visibility) GetValue(VisibilityFacilitySalesUnitProperty); }
            set { SetValue(VisibilityFacilitySalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductSalesUnitProperty = DependencyProperty.Register("VisibilityProductSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityProductSalesUnitProperty); }
            set { SetValue(VisibilityProductSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCostSalesUnitProperty = DependencyProperty.Register("VisibilityCostSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityCostSalesUnitProperty); }
            set { SetValue(VisibilityCostSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPriceSalesUnitProperty = DependencyProperty.Register("VisibilityPriceSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPriceSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPriceSalesUnitProperty); }
            set { SetValue(VisibilityPriceSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLaborHoursSalesUnitProperty = DependencyProperty.Register("VisibilityLaborHoursSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLaborHoursSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityLaborHoursSalesUnitProperty); }
            set { SetValue(VisibilityLaborHoursSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductsIncludedSalesUnitProperty = DependencyProperty.Register("VisibilityProductsIncludedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductsIncludedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityProductsIncludedSalesUnitProperty); }
            set { SetValue(VisibilityProductsIncludedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentConditionSetSalesUnitProperty = DependencyProperty.Register("VisibilityPaymentConditionSetSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionSetSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionSetSalesUnitProperty); }
            set { SetValue(VisibilityPaymentConditionSetSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductionTermSalesUnitProperty = DependencyProperty.Register("VisibilityProductionTermSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductionTermSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityProductionTermSalesUnitProperty); }
            set { SetValue(VisibilityProductionTermSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentSalesUnitProperty = DependencyProperty.Register("VisibilityCommentSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityCommentSalesUnitProperty); }
            set { SetValue(VisibilityCommentSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProjectSalesUnitProperty = DependencyProperty.Register("VisibilityProjectSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProjectSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityProjectSalesUnitProperty); }
            set { SetValue(VisibilityProjectSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDeliveryDateExpectedSalesUnitProperty = DependencyProperty.Register("VisibilityDeliveryDateExpectedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeliveryDateExpectedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityDeliveryDateExpectedSalesUnitProperty); }
            set { SetValue(VisibilityDeliveryDateExpectedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProducerSalesUnitProperty = DependencyProperty.Register("VisibilityProducerSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProducerSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityProducerSalesUnitProperty); }
            set { SetValue(VisibilityProducerSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityLosingReasonsSalesUnitProperty = DependencyProperty.Register("VisibilityLosingReasonsSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLosingReasonsSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityLosingReasonsSalesUnitProperty); }
            set { SetValue(VisibilityLosingReasonsSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRealizationDateSalesUnitProperty = DependencyProperty.Register("VisibilityRealizationDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRealizationDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityRealizationDateSalesUnitProperty); }
            set { SetValue(VisibilityRealizationDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderSalesUnitProperty = DependencyProperty.Register("VisibilityOrderSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderSalesUnitProperty); }
            set { SetValue(VisibilityOrderSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderPositionSalesUnitProperty = DependencyProperty.Register("VisibilityOrderPositionSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderPositionSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderPositionSalesUnitProperty); }
            set { SetValue(VisibilityOrderPositionSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySerialNumberSalesUnitProperty = DependencyProperty.Register("VisibilitySerialNumberSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySerialNumberSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySerialNumberSalesUnitProperty); }
            set { SetValue(VisibilitySerialNumberSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAssembleTermSalesUnitProperty = DependencyProperty.Register("VisibilityAssembleTermSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAssembleTermSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityAssembleTermSalesUnitProperty); }
            set { SetValue(VisibilityAssembleTermSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySignalToStartProductionSalesUnitProperty = DependencyProperty.Register("VisibilitySignalToStartProductionSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySignalToStartProductionSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySignalToStartProductionSalesUnitProperty); }
            set { SetValue(VisibilitySignalToStartProductionSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySignalToStartProductionDoneSalesUnitProperty = DependencyProperty.Register("VisibilitySignalToStartProductionDoneSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySignalToStartProductionDoneSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySignalToStartProductionDoneSalesUnitProperty); }
            set { SetValue(VisibilitySignalToStartProductionDoneSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStartProductionDateSalesUnitProperty = DependencyProperty.Register("VisibilityStartProductionDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartProductionDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityStartProductionDateSalesUnitProperty); }
            set { SetValue(VisibilityStartProductionDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPickingDateSalesUnitProperty = DependencyProperty.Register("VisibilityPickingDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPickingDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPickingDateSalesUnitProperty); }
            set { SetValue(VisibilityPickingDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityEndProductionPlanDateSalesUnitProperty = DependencyProperty.Register("VisibilityEndProductionPlanDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEndProductionPlanDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityEndProductionPlanDateSalesUnitProperty); }
            set { SetValue(VisibilityEndProductionPlanDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityEndProductionDateSalesUnitProperty = DependencyProperty.Register("VisibilityEndProductionDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEndProductionDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityEndProductionDateSalesUnitProperty); }
            set { SetValue(VisibilityEndProductionDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySpecificationSalesUnitProperty = DependencyProperty.Register("VisibilitySpecificationSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySpecificationSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySpecificationSalesUnitProperty); }
            set { SetValue(VisibilitySpecificationSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentsActualSalesUnitProperty = DependencyProperty.Register("VisibilityPaymentsActualSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsActualSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentsActualSalesUnitProperty); }
            set { SetValue(VisibilityPaymentsActualSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentsPlannedSalesUnitProperty = DependencyProperty.Register("VisibilityPaymentsPlannedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsPlannedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentsPlannedSalesUnitProperty); }
            set { SetValue(VisibilityPaymentsPlannedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPenaltySalesUnitProperty = DependencyProperty.Register("VisibilityPenaltySalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPenaltySalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPenaltySalesUnitProperty); }
            set { SetValue(VisibilityPenaltySalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCostDeliverySalesUnitProperty = DependencyProperty.Register("VisibilityCostDeliverySalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostDeliverySalesUnit
        {
            get { return (Visibility) GetValue(VisibilityCostDeliverySalesUnitProperty); }
            set { SetValue(VisibilityCostDeliverySalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCostDeliveryIncludedSalesUnitProperty = DependencyProperty.Register("VisibilityCostDeliveryIncludedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostDeliveryIncludedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityCostDeliveryIncludedSalesUnitProperty); }
            set { SetValue(VisibilityCostDeliveryIncludedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityExpectedDeliveryPeriodSalesUnitProperty = DependencyProperty.Register("VisibilityExpectedDeliveryPeriodSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityExpectedDeliveryPeriodSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityExpectedDeliveryPeriodSalesUnitProperty); }
            set { SetValue(VisibilityExpectedDeliveryPeriodSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityExpectedDeliveryPeriodCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityExpectedDeliveryPeriodCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityExpectedDeliveryPeriodCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityExpectedDeliveryPeriodCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityExpectedDeliveryPeriodCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAddressDeliverySalesUnitProperty = DependencyProperty.Register("VisibilityAddressDeliverySalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddressDeliverySalesUnit
        {
            get { return (Visibility) GetValue(VisibilityAddressDeliverySalesUnitProperty); }
            set { SetValue(VisibilityAddressDeliverySalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShipmentDateSalesUnitProperty = DependencyProperty.Register("VisibilityShipmentDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShipmentDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityShipmentDateSalesUnitProperty); }
            set { SetValue(VisibilityShipmentDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShipmentPlanDateSalesUnitProperty = DependencyProperty.Register("VisibilityShipmentPlanDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShipmentPlanDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityShipmentPlanDateSalesUnitProperty); }
            set { SetValue(VisibilityShipmentPlanDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDeliveryDateSalesUnitProperty = DependencyProperty.Register("VisibilityDeliveryDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeliveryDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityDeliveryDateSalesUnitProperty); }
            set { SetValue(VisibilityDeliveryDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsRemovedSalesUnitProperty = DependencyProperty.Register("VisibilityIsRemovedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsRemovedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityIsRemovedSalesUnitProperty); }
            set { SetValue(VisibilityIsRemovedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityBankGuaranteesSalesUnitProperty = DependencyProperty.Register("VisibilityBankGuaranteesSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBankGuaranteesSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityBankGuaranteesSalesUnitProperty); }
            set { SetValue(VisibilityBankGuaranteesSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAllowEditCostSalesUnitProperty = DependencyProperty.Register("VisibilityAllowEditCostSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAllowEditCostSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityAllowEditCostSalesUnitProperty); }
            set { SetValue(VisibilityAllowEditCostSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAllowEditProductSalesUnitProperty = DependencyProperty.Register("VisibilityAllowEditProductSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAllowEditProductSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityAllowEditProductSalesUnitProperty); }
            set { SetValue(VisibilityAllowEditProductSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsLoosenSalesUnitProperty = DependencyProperty.Register("VisibilityIsLoosenSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsLoosenSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityIsLoosenSalesUnitProperty); }
            set { SetValue(VisibilityIsLoosenSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsWonSalesUnitProperty = DependencyProperty.Register("VisibilityIsWonSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsWonSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityIsWonSalesUnitProperty); }
            set { SetValue(VisibilityIsWonSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsDoneSalesUnitProperty = DependencyProperty.Register("VisibilityIsDoneSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsDoneSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityIsDoneSalesUnitProperty); }
            set { SetValue(VisibilityIsDoneSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityActualPriceCalculationItemIdSalesUnitProperty = DependencyProperty.Register("VisibilityActualPriceCalculationItemIdSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActualPriceCalculationItemIdSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityActualPriceCalculationItemIdSalesUnitProperty); }
            set { SetValue(VisibilityActualPriceCalculationItemIdSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderIsTakenSalesUnitProperty = DependencyProperty.Register("VisibilityOrderIsTakenSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderIsTakenSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderIsTakenSalesUnitProperty); }
            set { SetValue(VisibilityOrderIsTakenSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderIsRealizedSalesUnitProperty = DependencyProperty.Register("VisibilityOrderIsRealizedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderIsRealizedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderIsRealizedSalesUnitProperty); }
            set { SetValue(VisibilityOrderIsRealizedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAllowTotalRemoveSalesUnitProperty = DependencyProperty.Register("VisibilityAllowTotalRemoveSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAllowTotalRemoveSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityAllowTotalRemoveSalesUnitProperty); }
            set { SetValue(VisibilityAllowTotalRemoveSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAddressDeliveryCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityAddressDeliveryCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddressDeliveryCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityAddressDeliveryCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityAddressDeliveryCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsPaidSalesUnitProperty = DependencyProperty.Register("VisibilityIsPaidSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsPaidSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityIsPaidSalesUnitProperty); }
            set { SetValue(VisibilityIsPaidSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySumPaidSalesUnitProperty = DependencyProperty.Register("VisibilitySumPaidSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumPaidSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySumPaidSalesUnitProperty); }
            set { SetValue(VisibilitySumPaidSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySumNotPaidSalesUnitProperty = DependencyProperty.Register("VisibilitySumNotPaidSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumNotPaidSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySumNotPaidSalesUnitProperty); }
            set { SetValue(VisibilitySumNotPaidSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityVatSalesUnitProperty = DependencyProperty.Register("VisibilityVatSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityVatSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityVatSalesUnitProperty); }
            set { SetValue(VisibilityVatSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySumNotPaidWithVatSalesUnitProperty = DependencyProperty.Register("VisibilitySumNotPaidWithVatSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumNotPaidWithVatSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySumNotPaidWithVatSalesUnitProperty); }
            set { SetValue(VisibilitySumNotPaidWithVatSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySumToStartProductionSalesUnitProperty = DependencyProperty.Register("VisibilitySumToStartProductionSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumToStartProductionSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySumToStartProductionSalesUnitProperty); }
            set { SetValue(VisibilitySumToStartProductionSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySumToShippingSalesUnitProperty = DependencyProperty.Register("VisibilitySumToShippingSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumToShippingSalesUnit
        {
            get { return (Visibility) GetValue(VisibilitySumToShippingSalesUnitProperty); }
            set { SetValue(VisibilitySumToShippingSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderInTakeDateInjectedSalesUnitProperty = DependencyProperty.Register("VisibilityOrderInTakeDateInjectedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeDateInjectedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderInTakeDateInjectedSalesUnitProperty); }
            set { SetValue(VisibilityOrderInTakeDateInjectedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderInTakeDateSalesUnitProperty = DependencyProperty.Register("VisibilityOrderInTakeDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderInTakeDateSalesUnitProperty); }
            set { SetValue(VisibilityOrderInTakeDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderInTakeYearSalesUnitProperty = DependencyProperty.Register("VisibilityOrderInTakeYearSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeYearSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderInTakeYearSalesUnitProperty); }
            set { SetValue(VisibilityOrderInTakeYearSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOrderInTakeMonthSalesUnitProperty = DependencyProperty.Register("VisibilityOrderInTakeMonthSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeMonthSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityOrderInTakeMonthSalesUnitProperty); }
            set { SetValue(VisibilityOrderInTakeMonthSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStartProductionConditionsDoneDateSalesUnitProperty = DependencyProperty.Register("VisibilityStartProductionConditionsDoneDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartProductionConditionsDoneDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityStartProductionConditionsDoneDateSalesUnitProperty); }
            set { SetValue(VisibilityStartProductionConditionsDoneDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShippingConditionsDoneDateSalesUnitProperty = DependencyProperty.Register("VisibilityShippingConditionsDoneDateSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShippingConditionsDoneDateSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityShippingConditionsDoneDateSalesUnitProperty); }
            set { SetValue(VisibilityShippingConditionsDoneDateSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStartProductionDateInjectedSalesUnitProperty = DependencyProperty.Register("VisibilityStartProductionDateInjectedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartProductionDateInjectedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityStartProductionDateInjectedSalesUnitProperty); }
            set { SetValue(VisibilityStartProductionDateInjectedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityStartProductionDateCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityStartProductionDateCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartProductionDateCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityStartProductionDateCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityStartProductionDateCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityEndProductionDateCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityEndProductionDateCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEndProductionDateCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityEndProductionDateCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityEndProductionDateCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityEndProductionDateByContractCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityEndProductionDateByContractCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEndProductionDateByContractCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityEndProductionDateByContractCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityEndProductionDateByContractCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRealizationDateCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityRealizationDateCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRealizationDateCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityRealizationDateCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityRealizationDateCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityShipmentDateCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityShipmentDateCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShipmentDateCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityShipmentDateCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityShipmentDateCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDeliveryDateCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityDeliveryDateCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeliveryDateCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityDeliveryDateCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityDeliveryDateCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDeliveryPeriodCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityDeliveryPeriodCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeliveryPeriodCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityDeliveryPeriodCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityDeliveryPeriodCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentsPlannedActualSalesUnitProperty = DependencyProperty.Register("VisibilityPaymentsPlannedActualSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsPlannedActualSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentsPlannedActualSalesUnitProperty); }
            set { SetValue(VisibilityPaymentsPlannedActualSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentsPlannedGeneratedSalesUnitProperty = DependencyProperty.Register("VisibilityPaymentsPlannedGeneratedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsPlannedGeneratedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentsPlannedGeneratedSalesUnitProperty); }
            set { SetValue(VisibilityPaymentsPlannedGeneratedSalesUnitProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentsPlannedCalculatedSalesUnitProperty = DependencyProperty.Register("VisibilityPaymentsPlannedCalculatedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsPlannedCalculatedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityPaymentsPlannedCalculatedSalesUnitProperty); }
            set { SetValue(VisibilityPaymentsPlannedCalculatedSalesUnitProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class DocumentDetailsView : ViewBase
    {
        public DocumentDetailsView()
        {
			InitializeComponent();
        }

        public DocumentDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentDetailsViewModel DocumentDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DocumentDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RegNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRegNumberDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RequestDocument)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRequestDocumentDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Author)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAuthorDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.SenderId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySenderIdDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.SenderEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySenderEmployeeDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RecipientId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRecipientIdDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RecipientEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRecipientEmployeeDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.CopyToRecipients)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCopyToRecipientsDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfRecipient)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRegistrationDetailsOfRecipientDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.TceNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTceNumberDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Direction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDirectionDocument = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNumberDocumentProperty = DependencyProperty.Register("VisibilityNumberDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberDocument
        {
            get { return (Visibility) GetValue(VisibilityNumberDocumentProperty); }
            set { SetValue(VisibilityNumberDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRegNumberDocumentProperty = DependencyProperty.Register("VisibilityRegNumberDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegNumberDocument
        {
            get { return (Visibility) GetValue(VisibilityRegNumberDocumentProperty); }
            set { SetValue(VisibilityRegNumberDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateDocumentProperty = DependencyProperty.Register("VisibilityDateDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateDocument
        {
            get { return (Visibility) GetValue(VisibilityDateDocumentProperty); }
            set { SetValue(VisibilityDateDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRequestDocumentDocumentProperty = DependencyProperty.Register("VisibilityRequestDocumentDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRequestDocumentDocument
        {
            get { return (Visibility) GetValue(VisibilityRequestDocumentDocumentProperty); }
            set { SetValue(VisibilityRequestDocumentDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAuthorDocumentProperty = DependencyProperty.Register("VisibilityAuthorDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAuthorDocument
        {
            get { return (Visibility) GetValue(VisibilityAuthorDocumentProperty); }
            set { SetValue(VisibilityAuthorDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySenderIdDocumentProperty = DependencyProperty.Register("VisibilitySenderIdDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderIdDocument
        {
            get { return (Visibility) GetValue(VisibilitySenderIdDocumentProperty); }
            set { SetValue(VisibilitySenderIdDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySenderEmployeeDocumentProperty = DependencyProperty.Register("VisibilitySenderEmployeeDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderEmployeeDocument
        {
            get { return (Visibility) GetValue(VisibilitySenderEmployeeDocumentProperty); }
            set { SetValue(VisibilitySenderEmployeeDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRecipientIdDocumentProperty = DependencyProperty.Register("VisibilityRecipientIdDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientIdDocument
        {
            get { return (Visibility) GetValue(VisibilityRecipientIdDocumentProperty); }
            set { SetValue(VisibilityRecipientIdDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRecipientEmployeeDocumentProperty = DependencyProperty.Register("VisibilityRecipientEmployeeDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientEmployeeDocument
        {
            get { return (Visibility) GetValue(VisibilityRecipientEmployeeDocumentProperty); }
            set { SetValue(VisibilityRecipientEmployeeDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCopyToRecipientsDocumentProperty = DependencyProperty.Register("VisibilityCopyToRecipientsDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCopyToRecipientsDocument
        {
            get { return (Visibility) GetValue(VisibilityCopyToRecipientsDocumentProperty); }
            set { SetValue(VisibilityCopyToRecipientsDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRegistrationDetailsOfRecipientDocumentProperty = DependencyProperty.Register("VisibilityRegistrationDetailsOfRecipientDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationDetailsOfRecipientDocument
        {
            get { return (Visibility) GetValue(VisibilityRegistrationDetailsOfRecipientDocumentProperty); }
            set { SetValue(VisibilityRegistrationDetailsOfRecipientDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentDocumentProperty = DependencyProperty.Register("VisibilityCommentDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentDocument
        {
            get { return (Visibility) GetValue(VisibilityCommentDocumentProperty); }
            set { SetValue(VisibilityCommentDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTceNumberDocumentProperty = DependencyProperty.Register("VisibilityTceNumberDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTceNumberDocument
        {
            get { return (Visibility) GetValue(VisibilityTceNumberDocumentProperty); }
            set { SetValue(VisibilityTceNumberDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDirectionDocumentProperty = DependencyProperty.Register("VisibilityDirectionDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDirectionDocument
        {
            get { return (Visibility) GetValue(VisibilityDirectionDocumentProperty); }
            set { SetValue(VisibilityDirectionDocumentProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class SumOnDateDetailsView : ViewBase
    {
        public SumOnDateDetailsView()
        {
			InitializeComponent();
        }

        public SumOnDateDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SumOnDateDetailsViewModel SumOnDateDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = SumOnDateDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.SumOnDate).GetProperty(nameof(HVTApp.Model.POCOs.SumOnDate.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateSumOnDate = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.SumOnDate).GetProperty(nameof(HVTApp.Model.POCOs.SumOnDate.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumSumOnDate = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDateSumOnDateProperty = DependencyProperty.Register("VisibilityDateSumOnDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateSumOnDate
        {
            get { return (Visibility) GetValue(VisibilityDateSumOnDateProperty); }
            set { SetValue(VisibilityDateSumOnDateProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySumSumOnDateProperty = DependencyProperty.Register("VisibilitySumSumOnDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumSumOnDate
        {
            get { return (Visibility) GetValue(VisibilitySumSumOnDateProperty); }
            set { SetValue(VisibilitySumSumOnDateProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProductDetailsView : ViewBase
    {
        public ProductDetailsView()
        {
			InitializeComponent();
        }

        public ProductDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductDetailsViewModel ProductDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.Designation)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesignationProduct = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.DesignationSpecial)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesignationSpecialProduct = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.ProductType)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductTypeProduct = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.Category)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCategoryProduct = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.ProductBlock)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductBlockProduct = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.DependentProducts)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDependentProductsProduct = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentProduct = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.HasBlockWithFixedCost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHasBlockWithFixedCostProduct = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityDesignationProductProperty = DependencyProperty.Register("VisibilityDesignationProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignationProduct
        {
            get { return (Visibility) GetValue(VisibilityDesignationProductProperty); }
            set { SetValue(VisibilityDesignationProductProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDesignationSpecialProductProperty = DependencyProperty.Register("VisibilityDesignationSpecialProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignationSpecialProduct
        {
            get { return (Visibility) GetValue(VisibilityDesignationSpecialProductProperty); }
            set { SetValue(VisibilityDesignationSpecialProductProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductTypeProductProperty = DependencyProperty.Register("VisibilityProductTypeProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductTypeProduct
        {
            get { return (Visibility) GetValue(VisibilityProductTypeProductProperty); }
            set { SetValue(VisibilityProductTypeProductProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCategoryProductProperty = DependencyProperty.Register("VisibilityCategoryProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCategoryProduct
        {
            get { return (Visibility) GetValue(VisibilityCategoryProductProperty); }
            set { SetValue(VisibilityCategoryProductProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProductBlockProductProperty = DependencyProperty.Register("VisibilityProductBlockProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductBlockProduct
        {
            get { return (Visibility) GetValue(VisibilityProductBlockProductProperty); }
            set { SetValue(VisibilityProductBlockProductProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDependentProductsProductProperty = DependencyProperty.Register("VisibilityDependentProductsProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDependentProductsProduct
        {
            get { return (Visibility) GetValue(VisibilityDependentProductsProductProperty); }
            set { SetValue(VisibilityDependentProductsProductProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentProductProperty = DependencyProperty.Register("VisibilityCommentProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentProduct
        {
            get { return (Visibility) GetValue(VisibilityCommentProductProperty); }
            set { SetValue(VisibilityCommentProductProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityHasBlockWithFixedCostProductProperty = DependencyProperty.Register("VisibilityHasBlockWithFixedCostProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHasBlockWithFixedCostProduct
        {
            get { return (Visibility) GetValue(VisibilityHasBlockWithFixedCostProductProperty); }
            set { SetValue(VisibilityHasBlockWithFixedCostProductProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class OfferDetailsView : ViewBase
    {
        public OfferDetailsView()
        {
			InitializeComponent();
        }

        public OfferDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, OfferDetailsViewModel OfferDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = OfferDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Project)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProjectOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.ValidityDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityValidityDateOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityVatOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RegNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRegNumberOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RequestDocument)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRequestDocumentOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Author)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAuthorOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.SenderId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySenderIdOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.SenderEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySenderEmployeeOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RecipientId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRecipientIdOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RecipientEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRecipientEmployeeOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.CopyToRecipients)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCopyToRecipientsOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfRecipient)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRegistrationDetailsOfRecipientOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.TceNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTceNumberOffer = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Direction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDirectionOffer = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityProjectOfferProperty = DependencyProperty.Register("VisibilityProjectOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProjectOffer
        {
            get { return (Visibility) GetValue(VisibilityProjectOfferProperty); }
            set { SetValue(VisibilityProjectOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityValidityDateOfferProperty = DependencyProperty.Register("VisibilityValidityDateOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityValidityDateOffer
        {
            get { return (Visibility) GetValue(VisibilityValidityDateOfferProperty); }
            set { SetValue(VisibilityValidityDateOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityVatOfferProperty = DependencyProperty.Register("VisibilityVatOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityVatOffer
        {
            get { return (Visibility) GetValue(VisibilityVatOfferProperty); }
            set { SetValue(VisibilityVatOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNumberOfferProperty = DependencyProperty.Register("VisibilityNumberOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberOffer
        {
            get { return (Visibility) GetValue(VisibilityNumberOfferProperty); }
            set { SetValue(VisibilityNumberOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRegNumberOfferProperty = DependencyProperty.Register("VisibilityRegNumberOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegNumberOffer
        {
            get { return (Visibility) GetValue(VisibilityRegNumberOfferProperty); }
            set { SetValue(VisibilityRegNumberOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateOfferProperty = DependencyProperty.Register("VisibilityDateOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateOffer
        {
            get { return (Visibility) GetValue(VisibilityDateOfferProperty); }
            set { SetValue(VisibilityDateOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRequestDocumentOfferProperty = DependencyProperty.Register("VisibilityRequestDocumentOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRequestDocumentOffer
        {
            get { return (Visibility) GetValue(VisibilityRequestDocumentOfferProperty); }
            set { SetValue(VisibilityRequestDocumentOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAuthorOfferProperty = DependencyProperty.Register("VisibilityAuthorOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAuthorOffer
        {
            get { return (Visibility) GetValue(VisibilityAuthorOfferProperty); }
            set { SetValue(VisibilityAuthorOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySenderIdOfferProperty = DependencyProperty.Register("VisibilitySenderIdOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderIdOffer
        {
            get { return (Visibility) GetValue(VisibilitySenderIdOfferProperty); }
            set { SetValue(VisibilitySenderIdOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySenderEmployeeOfferProperty = DependencyProperty.Register("VisibilitySenderEmployeeOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderEmployeeOffer
        {
            get { return (Visibility) GetValue(VisibilitySenderEmployeeOfferProperty); }
            set { SetValue(VisibilitySenderEmployeeOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRecipientIdOfferProperty = DependencyProperty.Register("VisibilityRecipientIdOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientIdOffer
        {
            get { return (Visibility) GetValue(VisibilityRecipientIdOfferProperty); }
            set { SetValue(VisibilityRecipientIdOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRecipientEmployeeOfferProperty = DependencyProperty.Register("VisibilityRecipientEmployeeOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientEmployeeOffer
        {
            get { return (Visibility) GetValue(VisibilityRecipientEmployeeOfferProperty); }
            set { SetValue(VisibilityRecipientEmployeeOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCopyToRecipientsOfferProperty = DependencyProperty.Register("VisibilityCopyToRecipientsOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCopyToRecipientsOffer
        {
            get { return (Visibility) GetValue(VisibilityCopyToRecipientsOfferProperty); }
            set { SetValue(VisibilityCopyToRecipientsOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRegistrationDetailsOfRecipientOfferProperty = DependencyProperty.Register("VisibilityRegistrationDetailsOfRecipientOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationDetailsOfRecipientOffer
        {
            get { return (Visibility) GetValue(VisibilityRegistrationDetailsOfRecipientOfferProperty); }
            set { SetValue(VisibilityRegistrationDetailsOfRecipientOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCommentOfferProperty = DependencyProperty.Register("VisibilityCommentOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentOffer
        {
            get { return (Visibility) GetValue(VisibilityCommentOfferProperty); }
            set { SetValue(VisibilityCommentOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTceNumberOfferProperty = DependencyProperty.Register("VisibilityTceNumberOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTceNumberOffer
        {
            get { return (Visibility) GetValue(VisibilityTceNumberOfferProperty); }
            set { SetValue(VisibilityTceNumberOfferProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDirectionOfferProperty = DependencyProperty.Register("VisibilityDirectionOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDirectionOffer
        {
            get { return (Visibility) GetValue(VisibilityDirectionOfferProperty); }
            set { SetValue(VisibilityDirectionOfferProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class EmployeeDetailsView : ViewBase
    {
        public EmployeeDetailsView()
        {
			InitializeComponent();
        }

        public EmployeeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, EmployeeDetailsViewModel EmployeeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = EmployeeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Person)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPersonEmployee = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.PersonalNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPersonalNumberEmployee = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.PhoneNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPhoneNumberEmployee = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Email)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEmailEmployee = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Company)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCompanyEmployee = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Position)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPositionEmployee = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityPersonEmployeeProperty = DependencyProperty.Register("VisibilityPersonEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPersonEmployee
        {
            get { return (Visibility) GetValue(VisibilityPersonEmployeeProperty); }
            set { SetValue(VisibilityPersonEmployeeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPersonalNumberEmployeeProperty = DependencyProperty.Register("VisibilityPersonalNumberEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPersonalNumberEmployee
        {
            get { return (Visibility) GetValue(VisibilityPersonalNumberEmployeeProperty); }
            set { SetValue(VisibilityPersonalNumberEmployeeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPhoneNumberEmployeeProperty = DependencyProperty.Register("VisibilityPhoneNumberEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPhoneNumberEmployee
        {
            get { return (Visibility) GetValue(VisibilityPhoneNumberEmployeeProperty); }
            set { SetValue(VisibilityPhoneNumberEmployeeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityEmailEmployeeProperty = DependencyProperty.Register("VisibilityEmailEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEmailEmployee
        {
            get { return (Visibility) GetValue(VisibilityEmailEmployeeProperty); }
            set { SetValue(VisibilityEmailEmployeeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityCompanyEmployeeProperty = DependencyProperty.Register("VisibilityCompanyEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCompanyEmployee
        {
            get { return (Visibility) GetValue(VisibilityCompanyEmployeeProperty); }
            set { SetValue(VisibilityCompanyEmployeeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPositionEmployeeProperty = DependencyProperty.Register("VisibilityPositionEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPositionEmployee
        {
            get { return (Visibility) GetValue(VisibilityPositionEmployeeProperty); }
            set { SetValue(VisibilityPositionEmployeeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class OrderDetailsView : ViewBase
    {
        public OrderDetailsView()
        {
			InitializeComponent();
        }

        public OrderDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, OrderDetailsViewModel OrderDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = OrderDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Order).GetProperty(nameof(HVTApp.Model.POCOs.Order.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberOrder = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Order).GetProperty(nameof(HVTApp.Model.POCOs.Order.DateOpen)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateOpenOrder = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNumberOrderProperty = DependencyProperty.Register("VisibilityNumberOrder", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberOrder
        {
            get { return (Visibility) GetValue(VisibilityNumberOrderProperty); }
            set { SetValue(VisibilityNumberOrderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateOpenOrderProperty = DependencyProperty.Register("VisibilityDateOpenOrder", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateOpenOrder
        {
            get { return (Visibility) GetValue(VisibilityDateOpenOrderProperty); }
            set { SetValue(VisibilityDateOpenOrderProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PaymentConditionDetailsView : ViewBase
    {
        public PaymentConditionDetailsView()
        {
			InitializeComponent();
        }

        public PaymentConditionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionDetailsViewModel PaymentConditionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PaymentConditionDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PaymentCondition).GetProperty(nameof(HVTApp.Model.POCOs.PaymentCondition.Part)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPartPaymentCondition = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentCondition).GetProperty(nameof(HVTApp.Model.POCOs.PaymentCondition.DaysToPoint)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDaysToPointPaymentCondition = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentCondition).GetProperty(nameof(HVTApp.Model.POCOs.PaymentCondition.PaymentConditionPoint)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionPointPaymentCondition = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityPartPaymentConditionProperty = DependencyProperty.Register("VisibilityPartPaymentCondition", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPartPaymentCondition
        {
            get { return (Visibility) GetValue(VisibilityPartPaymentConditionProperty); }
            set { SetValue(VisibilityPartPaymentConditionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDaysToPointPaymentConditionProperty = DependencyProperty.Register("VisibilityDaysToPointPaymentCondition", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDaysToPointPaymentCondition
        {
            get { return (Visibility) GetValue(VisibilityDaysToPointPaymentConditionProperty); }
            set { SetValue(VisibilityDaysToPointPaymentConditionProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentConditionPointPaymentConditionProperty = DependencyProperty.Register("VisibilityPaymentConditionPointPaymentCondition", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionPointPaymentCondition
        {
            get { return (Visibility) GetValue(VisibilityPaymentConditionPointPaymentConditionProperty); }
            set { SetValue(VisibilityPaymentConditionPointPaymentConditionProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class PaymentDocumentDetailsView : ViewBase
    {
        public PaymentDocumentDetailsView()
        {
			InitializeComponent();
        }

        public PaymentDocumentDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentDocumentDetailsViewModel PaymentDocumentDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = PaymentDocumentDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberPaymentDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDatePaymentDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Payments)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentsPaymentDocument = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityVatPaymentDocument = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNumberPaymentDocumentProperty = DependencyProperty.Register("VisibilityNumberPaymentDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberPaymentDocument
        {
            get { return (Visibility) GetValue(VisibilityNumberPaymentDocumentProperty); }
            set { SetValue(VisibilityNumberPaymentDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDatePaymentDocumentProperty = DependencyProperty.Register("VisibilityDatePaymentDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDatePaymentDocument
        {
            get { return (Visibility) GetValue(VisibilityDatePaymentDocumentProperty); }
            set { SetValue(VisibilityDatePaymentDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPaymentsPaymentDocumentProperty = DependencyProperty.Register("VisibilityPaymentsPaymentDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsPaymentDocument
        {
            get { return (Visibility) GetValue(VisibilityPaymentsPaymentDocumentProperty); }
            set { SetValue(VisibilityPaymentsPaymentDocumentProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityVatPaymentDocumentProperty = DependencyProperty.Register("VisibilityVatPaymentDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityVatPaymentDocument
        {
            get { return (Visibility) GetValue(VisibilityVatPaymentDocumentProperty); }
            set { SetValue(VisibilityVatPaymentDocumentProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class FacilityDetailsView : ViewBase
    {
        public FacilityDetailsView()
        {
			InitializeComponent();
        }

        public FacilityDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, FacilityDetailsViewModel FacilityDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = FacilityDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameFacility = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.Type)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTypeFacility = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.OwnerCompany)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOwnerCompanyFacility = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.Address)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAddressFacility = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameFacilityProperty = DependencyProperty.Register("VisibilityNameFacility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameFacility
        {
            get { return (Visibility) GetValue(VisibilityNameFacilityProperty); }
            set { SetValue(VisibilityNameFacilityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTypeFacilityProperty = DependencyProperty.Register("VisibilityTypeFacility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTypeFacility
        {
            get { return (Visibility) GetValue(VisibilityTypeFacilityProperty); }
            set { SetValue(VisibilityTypeFacilityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityOwnerCompanyFacilityProperty = DependencyProperty.Register("VisibilityOwnerCompanyFacility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOwnerCompanyFacility
        {
            get { return (Visibility) GetValue(VisibilityOwnerCompanyFacilityProperty); }
            set { SetValue(VisibilityOwnerCompanyFacilityProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityAddressFacilityProperty = DependencyProperty.Register("VisibilityAddressFacility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddressFacility
        {
            get { return (Visibility) GetValue(VisibilityAddressFacilityProperty); }
            set { SetValue(VisibilityAddressFacilityProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class ProjectDetailsView : ViewBase
    {
        public ProjectDetailsView()
        {
			InitializeComponent();
        }

        public ProjectDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectDetailsViewModel ProjectDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProjectDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameProject = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.ProjectType)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProjectTypeProject = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.InWork)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityInWorkProject = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.ForReport)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityForReportProject = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.Manager)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityManagerProject = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.Notes)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNotesProject = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameProjectProperty = DependencyProperty.Register("VisibilityNameProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameProject
        {
            get { return (Visibility) GetValue(VisibilityNameProjectProperty); }
            set { SetValue(VisibilityNameProjectProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProjectTypeProjectProperty = DependencyProperty.Register("VisibilityProjectTypeProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProjectTypeProject
        {
            get { return (Visibility) GetValue(VisibilityProjectTypeProjectProperty); }
            set { SetValue(VisibilityProjectTypeProjectProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityInWorkProjectProperty = DependencyProperty.Register("VisibilityInWorkProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityInWorkProject
        {
            get { return (Visibility) GetValue(VisibilityInWorkProjectProperty); }
            set { SetValue(VisibilityInWorkProjectProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityForReportProjectProperty = DependencyProperty.Register("VisibilityForReportProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityForReportProject
        {
            get { return (Visibility) GetValue(VisibilityForReportProjectProperty); }
            set { SetValue(VisibilityForReportProjectProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityManagerProjectProperty = DependencyProperty.Register("VisibilityManagerProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityManagerProject
        {
            get { return (Visibility) GetValue(VisibilityManagerProjectProperty); }
            set { SetValue(VisibilityManagerProjectProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityNotesProjectProperty = DependencyProperty.Register("VisibilityNotesProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNotesProject
        {
            get { return (Visibility) GetValue(VisibilityNotesProjectProperty); }
            set { SetValue(VisibilityNotesProjectProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class UserRoleDetailsView : ViewBase
    {
        public UserRoleDetailsView()
        {
			InitializeComponent();
        }

        public UserRoleDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, UserRoleDetailsViewModel UserRoleDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = UserRoleDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.UserRole).GetProperty(nameof(HVTApp.Model.POCOs.UserRole.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameUserRole = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.UserRole).GetProperty(nameof(HVTApp.Model.POCOs.UserRole.Role)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRoleUserRole = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameUserRoleProperty = DependencyProperty.Register("VisibilityNameUserRole", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameUserRole
        {
            get { return (Visibility) GetValue(VisibilityNameUserRoleProperty); }
            set { SetValue(VisibilityNameUserRoleProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRoleUserRoleProperty = DependencyProperty.Register("VisibilityRoleUserRole", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRoleUserRole
        {
            get { return (Visibility) GetValue(VisibilityRoleUserRoleProperty); }
            set { SetValue(VisibilityRoleUserRoleProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class SpecificationDetailsView : ViewBase
    {
        public SpecificationDetailsView()
        {
			InitializeComponent();
        }

        public SpecificationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SpecificationDetailsViewModel SpecificationDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = SpecificationDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Number)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNumberSpecification = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateSpecification = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.SignDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySignDateSpecification = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityVatSpecification = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Contract)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityContractSpecification = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNumberSpecificationProperty = DependencyProperty.Register("VisibilityNumberSpecification", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberSpecification
        {
            get { return (Visibility) GetValue(VisibilityNumberSpecificationProperty); }
            set { SetValue(VisibilityNumberSpecificationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateSpecificationProperty = DependencyProperty.Register("VisibilityDateSpecification", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateSpecification
        {
            get { return (Visibility) GetValue(VisibilityDateSpecificationProperty); }
            set { SetValue(VisibilityDateSpecificationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilitySignDateSpecificationProperty = DependencyProperty.Register("VisibilitySignDateSpecification", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySignDateSpecification
        {
            get { return (Visibility) GetValue(VisibilitySignDateSpecificationProperty); }
            set { SetValue(VisibilitySignDateSpecificationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityVatSpecificationProperty = DependencyProperty.Register("VisibilityVatSpecification", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityVatSpecification
        {
            get { return (Visibility) GetValue(VisibilityVatSpecificationProperty); }
            set { SetValue(VisibilityVatSpecificationProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityContractSpecificationProperty = DependencyProperty.Register("VisibilityContractSpecification", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityContractSpecification
        {
            get { return (Visibility) GetValue(VisibilityContractSpecificationProperty); }
            set { SetValue(VisibilityContractSpecificationProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class TenderDetailsView : ViewBase
    {
        public TenderDetailsView()
        {
			InitializeComponent();
        }

        public TenderDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderDetailsViewModel TenderDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TenderDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Link)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLinkTender = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Project)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProjectTender = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Types)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTypesTender = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateOpen)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateOpenTender = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateClose)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateCloseTender = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateNotice)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateNoticeTender = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Participants)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParticipantsTender = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Winner)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityWinnerTender = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DidNotTakePlace)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDidNotTakePlaceTender = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityLinkTenderProperty = DependencyProperty.Register("VisibilityLinkTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLinkTender
        {
            get { return (Visibility) GetValue(VisibilityLinkTenderProperty); }
            set { SetValue(VisibilityLinkTenderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityProjectTenderProperty = DependencyProperty.Register("VisibilityProjectTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProjectTender
        {
            get { return (Visibility) GetValue(VisibilityProjectTenderProperty); }
            set { SetValue(VisibilityProjectTenderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTypesTenderProperty = DependencyProperty.Register("VisibilityTypesTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTypesTender
        {
            get { return (Visibility) GetValue(VisibilityTypesTenderProperty); }
            set { SetValue(VisibilityTypesTenderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateOpenTenderProperty = DependencyProperty.Register("VisibilityDateOpenTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateOpenTender
        {
            get { return (Visibility) GetValue(VisibilityDateOpenTenderProperty); }
            set { SetValue(VisibilityDateOpenTenderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateCloseTenderProperty = DependencyProperty.Register("VisibilityDateCloseTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateCloseTender
        {
            get { return (Visibility) GetValue(VisibilityDateCloseTenderProperty); }
            set { SetValue(VisibilityDateCloseTenderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDateNoticeTenderProperty = DependencyProperty.Register("VisibilityDateNoticeTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateNoticeTender
        {
            get { return (Visibility) GetValue(VisibilityDateNoticeTenderProperty); }
            set { SetValue(VisibilityDateNoticeTenderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityParticipantsTenderProperty = DependencyProperty.Register("VisibilityParticipantsTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParticipantsTender
        {
            get { return (Visibility) GetValue(VisibilityParticipantsTenderProperty); }
            set { SetValue(VisibilityParticipantsTenderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityWinnerTenderProperty = DependencyProperty.Register("VisibilityWinnerTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityWinnerTender
        {
            get { return (Visibility) GetValue(VisibilityWinnerTenderProperty); }
            set { SetValue(VisibilityWinnerTenderProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityDidNotTakePlaceTenderProperty = DependencyProperty.Register("VisibilityDidNotTakePlaceTender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDidNotTakePlaceTender
        {
            get { return (Visibility) GetValue(VisibilityDidNotTakePlaceTenderProperty); }
            set { SetValue(VisibilityDidNotTakePlaceTenderProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class TenderTypeDetailsView : ViewBase
    {
        public TenderTypeDetailsView()
        {
			InitializeComponent();
        }

        public TenderTypeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TenderTypeDetailsViewModel TenderTypeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TenderTypeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.TenderType).GetProperty(nameof(HVTApp.Model.POCOs.TenderType.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameTenderType = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.TenderType).GetProperty(nameof(HVTApp.Model.POCOs.TenderType.Type)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTypeTenderType = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityNameTenderTypeProperty = DependencyProperty.Register("VisibilityNameTenderType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameTenderType
        {
            get { return (Visibility) GetValue(VisibilityNameTenderTypeProperty); }
            set { SetValue(VisibilityNameTenderTypeProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityTypeTenderTypeProperty = DependencyProperty.Register("VisibilityTypeTenderType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTypeTenderType
        {
            get { return (Visibility) GetValue(VisibilityTypeTenderTypeProperty); }
            set { SetValue(VisibilityTypeTenderTypeProperty, value); OnPropertyChanged(); }
        }

	}

    public partial class UserDetailsView : ViewBase
    {
        public UserDetailsView()
        {
			InitializeComponent();
        }

        public UserDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, UserDetailsViewModel UserDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = UserDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;

            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Login)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLoginUser = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Password)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPasswordUser = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.RoleCurrent)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRoleCurrentUser = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Roles)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRolesUser = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Employee)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEmployeeUser = Visibility.Collapsed;

            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.IsActual)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsActualUser = Visibility.Collapsed;


        }


        public static readonly DependencyProperty VisibilityLoginUserProperty = DependencyProperty.Register("VisibilityLoginUser", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLoginUser
        {
            get { return (Visibility) GetValue(VisibilityLoginUserProperty); }
            set { SetValue(VisibilityLoginUserProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityPasswordUserProperty = DependencyProperty.Register("VisibilityPasswordUser", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPasswordUser
        {
            get { return (Visibility) GetValue(VisibilityPasswordUserProperty); }
            set { SetValue(VisibilityPasswordUserProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRoleCurrentUserProperty = DependencyProperty.Register("VisibilityRoleCurrentUser", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRoleCurrentUser
        {
            get { return (Visibility) GetValue(VisibilityRoleCurrentUserProperty); }
            set { SetValue(VisibilityRoleCurrentUserProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityRolesUserProperty = DependencyProperty.Register("VisibilityRolesUser", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRolesUser
        {
            get { return (Visibility) GetValue(VisibilityRolesUserProperty); }
            set { SetValue(VisibilityRolesUserProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityEmployeeUserProperty = DependencyProperty.Register("VisibilityEmployeeUser", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEmployeeUser
        {
            get { return (Visibility) GetValue(VisibilityEmployeeUserProperty); }
            set { SetValue(VisibilityEmployeeUserProperty, value); OnPropertyChanged(); }
        }


        public static readonly DependencyProperty VisibilityIsActualUserProperty = DependencyProperty.Register("VisibilityIsActualUser", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsActualUser
        {
            get { return (Visibility) GetValue(VisibilityIsActualUserProperty); }
            set { SetValue(VisibilityIsActualUserProperty, value); OnPropertyChanged(); }
        }

	}

}
