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



        public static readonly DependencyProperty VisibilityProductCreateNewProductTaskProperty = DependencyProperty.Register("VisibilityProductCreateNewProductTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductCreateNewProductTask
        {
            get { return (Visibility) GetValue(VisibilityProductCreateNewProductTaskProperty); }
            set { SetValue(VisibilityProductCreateNewProductTaskProperty, value); OnPropertyChanged(); }
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


    public partial class ProductBlockIsServiceDetailsView : ViewBase
    {
        public ProductBlockIsServiceDetailsView()
        {
			InitializeComponent();
        }

        public ProductBlockIsServiceDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockIsServiceDetailsViewModel ProductBlockIsServiceDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductBlockIsServiceDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.ProductBlockIsService).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlockIsService.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersProductBlockIsService = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityParametersProductBlockIsServiceProperty = DependencyProperty.Register("VisibilityParametersProductBlockIsService", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParametersProductBlockIsService
        {
            get { return (Visibility) GetValue(VisibilityParametersProductBlockIsServiceProperty); }
            set { SetValue(VisibilityParametersProductBlockIsServiceProperty, value); OnPropertyChanged(); }
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


    public partial class CommonOptionDetailsView : ViewBase
    {
        public CommonOptionDetailsView()
        {
			InitializeComponent();
        }

        public CommonOptionDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CommonOptionDetailsViewModel CommonOptionDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CommonOptionDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.Date)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateCommonOption = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.OurCompanyId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOurCompanyIdCommonOption = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.ActualPriceTerm)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityActualPriceTermCommonOption = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromStartToEndProduction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStandartTermFromStartToEndProductionCommonOption = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromPickToEndProduction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStandartTermFromPickToEndProductionCommonOption = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartPaymentsConditionSetId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStandartPaymentsConditionSetIdCommonOption = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityDateCommonOptionProperty = DependencyProperty.Register("VisibilityDateCommonOption", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateCommonOption
        {
            get { return (Visibility) GetValue(VisibilityDateCommonOptionProperty); }
            set { SetValue(VisibilityDateCommonOptionProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityOurCompanyIdCommonOptionProperty = DependencyProperty.Register("VisibilityOurCompanyIdCommonOption", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOurCompanyIdCommonOption
        {
            get { return (Visibility) GetValue(VisibilityOurCompanyIdCommonOptionProperty); }
            set { SetValue(VisibilityOurCompanyIdCommonOptionProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityActualPriceTermCommonOptionProperty = DependencyProperty.Register("VisibilityActualPriceTermCommonOption", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActualPriceTermCommonOption
        {
            get { return (Visibility) GetValue(VisibilityActualPriceTermCommonOptionProperty); }
            set { SetValue(VisibilityActualPriceTermCommonOptionProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityStandartTermFromStartToEndProductionCommonOptionProperty = DependencyProperty.Register("VisibilityStandartTermFromStartToEndProductionCommonOption", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartTermFromStartToEndProductionCommonOption
        {
            get { return (Visibility) GetValue(VisibilityStandartTermFromStartToEndProductionCommonOptionProperty); }
            set { SetValue(VisibilityStandartTermFromStartToEndProductionCommonOptionProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityStandartTermFromPickToEndProductionCommonOptionProperty = DependencyProperty.Register("VisibilityStandartTermFromPickToEndProductionCommonOption", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartTermFromPickToEndProductionCommonOption
        {
            get { return (Visibility) GetValue(VisibilityStandartTermFromPickToEndProductionCommonOptionProperty); }
            set { SetValue(VisibilityStandartTermFromPickToEndProductionCommonOptionProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityStandartPaymentsConditionSetIdCommonOptionProperty = DependencyProperty.Register("VisibilityStandartPaymentsConditionSetIdCommonOption", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartPaymentsConditionSetIdCommonOption
        {
            get { return (Visibility) GetValue(VisibilityStandartPaymentsConditionSetIdCommonOptionProperty); }
            set { SetValue(VisibilityStandartPaymentsConditionSetIdCommonOptionProperty, value); OnPropertyChanged(); }
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


    public partial class DescribeProductBlockTaskDetailsView : ViewBase
    {
        public DescribeProductBlockTaskDetailsView()
        {
			InitializeComponent();
        }

        public DescribeProductBlockTaskDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DescribeProductBlockTaskDetailsViewModel DescribeProductBlockTaskDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DescribeProductBlockTaskDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.DescribeProductBlockTask).GetProperty(nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.ProductBlock)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductBlockDescribeProductBlockTask = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.DescribeProductBlockTask).GetProperty(nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.Product)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductDescribeProductBlockTask = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityProductBlockDescribeProductBlockTaskProperty = DependencyProperty.Register("VisibilityProductBlockDescribeProductBlockTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductBlockDescribeProductBlockTask
        {
            get { return (Visibility) GetValue(VisibilityProductBlockDescribeProductBlockTaskProperty); }
            set { SetValue(VisibilityProductBlockDescribeProductBlockTaskProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityProductDescribeProductBlockTaskProperty = DependencyProperty.Register("VisibilityProductDescribeProductBlockTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductDescribeProductBlockTask
        {
            get { return (Visibility) GetValue(VisibilityProductDescribeProductBlockTaskProperty); }
            set { SetValue(VisibilityProductDescribeProductBlockTaskProperty, value); OnPropertyChanged(); }
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


            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Offer)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityOfferOfferUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Product)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductOfferUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.ProductsIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductsIncludedOfferUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Facility)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFacilityOfferUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionSetOfferUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductionTermOfferUnit = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityCostOfferUnitProperty = DependencyProperty.Register("VisibilityCostOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityCostOfferUnitProperty); }
            set { SetValue(VisibilityCostOfferUnitProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityOfferOfferUnitProperty = DependencyProperty.Register("VisibilityOfferOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOfferOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityOfferOfferUnitProperty); }
            set { SetValue(VisibilityOfferOfferUnitProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityProductOfferUnitProperty = DependencyProperty.Register("VisibilityProductOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityProductOfferUnitProperty); }
            set { SetValue(VisibilityProductOfferUnitProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityProductsIncludedOfferUnitProperty = DependencyProperty.Register("VisibilityProductsIncludedOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductsIncludedOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityProductsIncludedOfferUnitProperty); }
            set { SetValue(VisibilityProductsIncludedOfferUnitProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityFacilityOfferUnitProperty = DependencyProperty.Register("VisibilityFacilityOfferUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFacilityOfferUnit
        {
            get { return (Visibility) GetValue(VisibilityFacilityOfferUnitProperty); }
            set { SetValue(VisibilityFacilityOfferUnitProperty, value); OnPropertyChanged(); }
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


            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParametersProductBlock = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Prices)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPricesProductBlock = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.StructureCostNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStructureCostNumberProductBlock = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Design)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDesignProductBlock = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.IsService)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsServiceProductBlock = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Weight)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityWeightProductBlock = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.LastPriceDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLastPriceDateProductBlock = Visibility.Collapsed;



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



        public static readonly DependencyProperty VisibilityIsServiceProductBlockProperty = DependencyProperty.Register("VisibilityIsServiceProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsServiceProductBlock
        {
            get { return (Visibility) GetValue(VisibilityIsServiceProductBlockProperty); }
            set { SetValue(VisibilityIsServiceProductBlockProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityWeightProductBlockProperty = DependencyProperty.Register("VisibilityWeightProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityWeightProductBlock
        {
            get { return (Visibility) GetValue(VisibilityWeightProductBlockProperty); }
            set { SetValue(VisibilityWeightProductBlockProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityLastPriceDateProductBlockProperty = DependencyProperty.Register("VisibilityLastPriceDateProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastPriceDateProductBlock
        {
            get { return (Visibility) GetValue(VisibilityLastPriceDateProductBlockProperty); }
            set { SetValue(VisibilityLastPriceDateProductBlockProperty, value); OnPropertyChanged(); }
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


    public partial class ProductionTaskDetailsView : ViewBase
    {
        public ProductionTaskDetailsView()
        {
			InitializeComponent();
        }

        public ProductionTaskDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductionTaskDetailsViewModel ProductionTaskDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductionTaskDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.ProductionTask).GetProperty(nameof(HVTApp.Model.POCOs.ProductionTask.DateTask)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDateTaskProductionTask = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.ProductionTask).GetProperty(nameof(HVTApp.Model.POCOs.ProductionTask.SalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySalesUnitsProductionTask = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityDateTaskProductionTaskProperty = DependencyProperty.Register("VisibilityDateTaskProductionTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateTaskProductionTask
        {
            get { return (Visibility) GetValue(VisibilityDateTaskProductionTaskProperty); }
            set { SetValue(VisibilityDateTaskProductionTaskProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilitySalesUnitsProductionTaskProperty = DependencyProperty.Register("VisibilitySalesUnitsProductionTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySalesUnitsProductionTask
        {
            get { return (Visibility) GetValue(VisibilitySalesUnitsProductionTaskProperty); }
            set { SetValue(VisibilitySalesUnitsProductionTaskProperty, value); OnPropertyChanged(); }
        }


	}


    public partial class SalesBlockDetailsView : ViewBase
    {
        public SalesBlockDetailsView()
        {
			InitializeComponent();
        }

        public SalesBlockDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, SalesBlockDetailsViewModel SalesBlockDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = SalesBlockDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.SalesBlock).GetProperty(nameof(HVTApp.Model.POCOs.SalesBlock.ParentSalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityParentSalesUnitsSalesBlock = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesBlock).GetProperty(nameof(HVTApp.Model.POCOs.SalesBlock.ChildSalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityChildSalesUnitsSalesBlock = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityParentSalesUnitsSalesBlockProperty = DependencyProperty.Register("VisibilityParentSalesUnitsSalesBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentSalesUnitsSalesBlock
        {
            get { return (Visibility) GetValue(VisibilityParentSalesUnitsSalesBlockProperty); }
            set { SetValue(VisibilityParentSalesUnitsSalesBlockProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityChildSalesUnitsSalesBlockProperty = DependencyProperty.Register("VisibilityChildSalesUnitsSalesBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildSalesUnitsSalesBlock
        {
            get { return (Visibility) GetValue(VisibilityChildSalesUnitsSalesBlockProperty); }
            set { SetValue(VisibilityChildSalesUnitsSalesBlockProperty, value); OnPropertyChanged(); }
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


            //attr = typeof(HVTApp.Model.POCOs.DocumentsRegistrationDetails).GetProperty(nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRegistrationNumberDocumentsRegistrationDetails = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.DocumentsRegistrationDetails).GetProperty(nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRegistrationDateDocumentsRegistrationDetails = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityRegistrationNumberDocumentsRegistrationDetailsProperty = DependencyProperty.Register("VisibilityRegistrationNumberDocumentsRegistrationDetails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationNumberDocumentsRegistrationDetails
        {
            get { return (Visibility) GetValue(VisibilityRegistrationNumberDocumentsRegistrationDetailsProperty); }
            set { SetValue(VisibilityRegistrationNumberDocumentsRegistrationDetailsProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityRegistrationDateDocumentsRegistrationDetailsProperty = DependencyProperty.Register("VisibilityRegistrationDateDocumentsRegistrationDetails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationDateDocumentsRegistrationDetails
        {
            get { return (Visibility) GetValue(VisibilityRegistrationDateDocumentsRegistrationDetailsProperty); }
            set { SetValue(VisibilityRegistrationDateDocumentsRegistrationDetailsProperty, value); OnPropertyChanged(); }
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



        }



        public static readonly DependencyProperty VisibilityRequiredParametersParameterRelationProperty = DependencyProperty.Register("VisibilityRequiredParametersParameterRelation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRequiredParametersParameterRelation
        {
            get { return (Visibility) GetValue(VisibilityRequiredParametersParameterRelationProperty); }
            set { SetValue(VisibilityRequiredParametersParameterRelationProperty, value); OnPropertyChanged(); }
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


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Cost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCostSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Product)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ProductsIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductsIncludedSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Facility)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFacilitySalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPaymentConditionSetSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductionTermSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Project)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProjectSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDateExpected)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDeliveryDateExpectedSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Producer)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProducerSalesUnit = Visibility.Collapsed;


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


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ExpectedDeliveryPeriod)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityExpectedDeliveryPeriodSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ExpectedDeliveryPeriodCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityExpectedDeliveryPeriodCalculatedSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Address)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAddressSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShipmentDateSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentPlanDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityShipmentPlanDateSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDate)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDeliveryDateSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AllowEditCost)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAllowEditCostSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AllowEditProduct)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityAllowEditProductSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.IsLoosen)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsLoosenSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.IsPaid)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsPaidSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumPaid)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumPaidSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumNotPaid)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumNotPaidSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumToStartProduction)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumToStartProductionSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumToShipping)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilitySumToShippingSalesUnit = Visibility.Collapsed;


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


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStartProductionDateCalculatedSalesUnit = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEndProductionDateCalculatedSalesUnit = Visibility.Collapsed;


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



        public static readonly DependencyProperty VisibilityCostSalesUnitProperty = DependencyProperty.Register("VisibilityCostSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityCostSalesUnitProperty); }
            set { SetValue(VisibilityCostSalesUnitProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityProductSalesUnitProperty = DependencyProperty.Register("VisibilityProductSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityProductSalesUnitProperty); }
            set { SetValue(VisibilityProductSalesUnitProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityProductsIncludedSalesUnitProperty = DependencyProperty.Register("VisibilityProductsIncludedSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductsIncludedSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityProductsIncludedSalesUnitProperty); }
            set { SetValue(VisibilityProductsIncludedSalesUnitProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityFacilitySalesUnitProperty = DependencyProperty.Register("VisibilityFacilitySalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFacilitySalesUnit
        {
            get { return (Visibility) GetValue(VisibilityFacilitySalesUnitProperty); }
            set { SetValue(VisibilityFacilitySalesUnitProperty, value); OnPropertyChanged(); }
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



        public static readonly DependencyProperty VisibilityAddressSalesUnitProperty = DependencyProperty.Register("VisibilityAddressSalesUnit", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddressSalesUnit
        {
            get { return (Visibility) GetValue(VisibilityAddressSalesUnitProperty); }
            set { SetValue(VisibilityAddressSalesUnitProperty, value); OnPropertyChanged(); }
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


    public partial class TestFriendAddressDetailsView : ViewBase
    {
        public TestFriendAddressDetailsView()
        {
			InitializeComponent();
        }

        public TestFriendAddressDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendAddressDetailsViewModel TestFriendAddressDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TestFriendAddressDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.TestFriendAddress).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendAddress.City)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCityTestFriendAddress = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriendAddress).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendAddress.Street)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStreetTestFriendAddress = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriendAddress).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendAddress.StreetNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityStreetNumberTestFriendAddress = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityCityTestFriendAddressProperty = DependencyProperty.Register("VisibilityCityTestFriendAddress", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCityTestFriendAddress
        {
            get { return (Visibility) GetValue(VisibilityCityTestFriendAddressProperty); }
            set { SetValue(VisibilityCityTestFriendAddressProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityStreetTestFriendAddressProperty = DependencyProperty.Register("VisibilityStreetTestFriendAddress", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStreetTestFriendAddress
        {
            get { return (Visibility) GetValue(VisibilityStreetTestFriendAddressProperty); }
            set { SetValue(VisibilityStreetTestFriendAddressProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityStreetNumberTestFriendAddressProperty = DependencyProperty.Register("VisibilityStreetNumberTestFriendAddress", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStreetNumberTestFriendAddress
        {
            get { return (Visibility) GetValue(VisibilityStreetNumberTestFriendAddressProperty); }
            set { SetValue(VisibilityStreetNumberTestFriendAddressProperty, value); OnPropertyChanged(); }
        }


	}


    public partial class TestFriendDetailsView : ViewBase
    {
        public TestFriendDetailsView()
        {
			InitializeComponent();
        }

        public TestFriendDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendDetailsViewModel TestFriendDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TestFriendDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.FriendGroupId)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFriendGroupIdTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.FirstName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFirstNameTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.LastName)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityLastNameTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.Birthday)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityBirthdayTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.IsDeveloper)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIsDeveloperTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendAddress)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTestFriendAddressTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendGroup)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTestFriendGroupTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.Emails)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEmailsTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.IdGet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityIdGetTestFriend = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendEmailGet)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityTestFriendEmailGetTestFriend = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityFriendGroupIdTestFriendProperty = DependencyProperty.Register("VisibilityFriendGroupIdTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFriendGroupIdTestFriend
        {
            get { return (Visibility) GetValue(VisibilityFriendGroupIdTestFriendProperty); }
            set { SetValue(VisibilityFriendGroupIdTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityFirstNameTestFriendProperty = DependencyProperty.Register("VisibilityFirstNameTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFirstNameTestFriend
        {
            get { return (Visibility) GetValue(VisibilityFirstNameTestFriendProperty); }
            set { SetValue(VisibilityFirstNameTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityLastNameTestFriendProperty = DependencyProperty.Register("VisibilityLastNameTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastNameTestFriend
        {
            get { return (Visibility) GetValue(VisibilityLastNameTestFriendProperty); }
            set { SetValue(VisibilityLastNameTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityBirthdayTestFriendProperty = DependencyProperty.Register("VisibilityBirthdayTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBirthdayTestFriend
        {
            get { return (Visibility) GetValue(VisibilityBirthdayTestFriendProperty); }
            set { SetValue(VisibilityBirthdayTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityIsDeveloperTestFriendProperty = DependencyProperty.Register("VisibilityIsDeveloperTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsDeveloperTestFriend
        {
            get { return (Visibility) GetValue(VisibilityIsDeveloperTestFriendProperty); }
            set { SetValue(VisibilityIsDeveloperTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityTestFriendAddressTestFriendProperty = DependencyProperty.Register("VisibilityTestFriendAddressTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTestFriendAddressTestFriend
        {
            get { return (Visibility) GetValue(VisibilityTestFriendAddressTestFriendProperty); }
            set { SetValue(VisibilityTestFriendAddressTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityTestFriendGroupTestFriendProperty = DependencyProperty.Register("VisibilityTestFriendGroupTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTestFriendGroupTestFriend
        {
            get { return (Visibility) GetValue(VisibilityTestFriendGroupTestFriendProperty); }
            set { SetValue(VisibilityTestFriendGroupTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityEmailsTestFriendProperty = DependencyProperty.Register("VisibilityEmailsTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEmailsTestFriend
        {
            get { return (Visibility) GetValue(VisibilityEmailsTestFriendProperty); }
            set { SetValue(VisibilityEmailsTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityIdGetTestFriendProperty = DependencyProperty.Register("VisibilityIdGetTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIdGetTestFriend
        {
            get { return (Visibility) GetValue(VisibilityIdGetTestFriendProperty); }
            set { SetValue(VisibilityIdGetTestFriendProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityTestFriendEmailGetTestFriendProperty = DependencyProperty.Register("VisibilityTestFriendEmailGetTestFriend", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTestFriendEmailGetTestFriend
        {
            get { return (Visibility) GetValue(VisibilityTestFriendEmailGetTestFriendProperty); }
            set { SetValue(VisibilityTestFriendEmailGetTestFriendProperty, value); OnPropertyChanged(); }
        }


	}


    public partial class TestFriendEmailDetailsView : ViewBase
    {
        public TestFriendEmailDetailsView()
        {
			InitializeComponent();
        }

        public TestFriendEmailDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendEmailDetailsViewModel TestFriendEmailDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TestFriendEmailDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.TestFriendEmail).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendEmail.Email)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEmailTestFriendEmail = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriendEmail).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendEmail.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCommentTestFriendEmail = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityEmailTestFriendEmailProperty = DependencyProperty.Register("VisibilityEmailTestFriendEmail", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEmailTestFriendEmail
        {
            get { return (Visibility) GetValue(VisibilityEmailTestFriendEmailProperty); }
            set { SetValue(VisibilityEmailTestFriendEmailProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityCommentTestFriendEmailProperty = DependencyProperty.Register("VisibilityCommentTestFriendEmail", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCommentTestFriendEmail
        {
            get { return (Visibility) GetValue(VisibilityCommentTestFriendEmailProperty); }
            set { SetValue(VisibilityCommentTestFriendEmailProperty, value); OnPropertyChanged(); }
        }


	}


    public partial class TestFriendGroupDetailsView : ViewBase
    {
        public TestFriendGroupDetailsView()
        {
			InitializeComponent();
        }

        public TestFriendGroupDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestFriendGroupDetailsViewModel TestFriendGroupDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TestFriendGroupDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.TestFriendGroup).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendGroup.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameTestFriendGroup = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestFriendGroup).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendGroup.FriendTests)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityFriendTestsTestFriendGroup = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityNameTestFriendGroupProperty = DependencyProperty.Register("VisibilityNameTestFriendGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameTestFriendGroup
        {
            get { return (Visibility) GetValue(VisibilityNameTestFriendGroupProperty); }
            set { SetValue(VisibilityNameTestFriendGroupProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityFriendTestsTestFriendGroupProperty = DependencyProperty.Register("VisibilityFriendTestsTestFriendGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFriendTestsTestFriendGroup
        {
            get { return (Visibility) GetValue(VisibilityFriendTestsTestFriendGroupProperty); }
            set { SetValue(VisibilityFriendTestsTestFriendGroupProperty, value); OnPropertyChanged(); }
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


            //attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Code)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCodeDocument = Visibility.Collapsed;


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



        }



        public static readonly DependencyProperty VisibilityNumberDocumentProperty = DependencyProperty.Register("VisibilityNumberDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumberDocument
        {
            get { return (Visibility) GetValue(VisibilityNumberDocumentProperty); }
            set { SetValue(VisibilityNumberDocumentProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityCodeDocumentProperty = DependencyProperty.Register("VisibilityCodeDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCodeDocument
        {
            get { return (Visibility) GetValue(VisibilityCodeDocumentProperty); }
            set { SetValue(VisibilityCodeDocumentProperty, value); OnPropertyChanged(); }
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


    public partial class TestEntityDetailsView : ViewBase
    {
        public TestEntityDetailsView()
        {
			InitializeComponent();
        }

        public TestEntityDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestEntityDetailsViewModel TestEntityDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TestEntityDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.TestEntity).GetProperty(nameof(HVTApp.Model.POCOs.TestEntity.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameTestEntity = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityNameTestEntityProperty = DependencyProperty.Register("VisibilityNameTestEntity", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameTestEntity
        {
            get { return (Visibility) GetValue(VisibilityNameTestEntityProperty); }
            set { SetValue(VisibilityNameTestEntityProperty, value); OnPropertyChanged(); }
        }


	}


    public partial class TestHusbandDetailsView : ViewBase
    {
        public TestHusbandDetailsView()
        {
			InitializeComponent();
        }

        public TestHusbandDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestHusbandDetailsViewModel TestHusbandDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TestHusbandDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.TestHusband).GetProperty(nameof(HVTApp.Model.POCOs.TestHusband.Wife)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityWifeTestHusband = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestHusband).GetProperty(nameof(HVTApp.Model.POCOs.TestHusband.Children)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityChildrenTestHusband = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestHusband).GetProperty(nameof(HVTApp.Model.POCOs.TestHusband.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameTestHusband = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityWifeTestHusbandProperty = DependencyProperty.Register("VisibilityWifeTestHusband", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityWifeTestHusband
        {
            get { return (Visibility) GetValue(VisibilityWifeTestHusbandProperty); }
            set { SetValue(VisibilityWifeTestHusbandProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityChildrenTestHusbandProperty = DependencyProperty.Register("VisibilityChildrenTestHusband", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildrenTestHusband
        {
            get { return (Visibility) GetValue(VisibilityChildrenTestHusbandProperty); }
            set { SetValue(VisibilityChildrenTestHusbandProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityNameTestHusbandProperty = DependencyProperty.Register("VisibilityNameTestHusband", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameTestHusband
        {
            get { return (Visibility) GetValue(VisibilityNameTestHusbandProperty); }
            set { SetValue(VisibilityNameTestHusbandProperty, value); OnPropertyChanged(); }
        }


	}


    public partial class TestWifeDetailsView : ViewBase
    {
        public TestWifeDetailsView()
        {
			InitializeComponent();
        }

        public TestWifeDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestWifeDetailsViewModel TestWifeDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TestWifeDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.TestWife).GetProperty(nameof(HVTApp.Model.POCOs.TestWife.N)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNTestWife = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestWife).GetProperty(nameof(HVTApp.Model.POCOs.TestWife.Husband)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHusbandTestWife = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestWife).GetProperty(nameof(HVTApp.Model.POCOs.TestWife.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameTestWife = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityNTestWifeProperty = DependencyProperty.Register("VisibilityNTestWife", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNTestWife
        {
            get { return (Visibility) GetValue(VisibilityNTestWifeProperty); }
            set { SetValue(VisibilityNTestWifeProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityHusbandTestWifeProperty = DependencyProperty.Register("VisibilityHusbandTestWife", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHusbandTestWife
        {
            get { return (Visibility) GetValue(VisibilityHusbandTestWifeProperty); }
            set { SetValue(VisibilityHusbandTestWifeProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityNameTestWifeProperty = DependencyProperty.Register("VisibilityNameTestWife", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameTestWife
        {
            get { return (Visibility) GetValue(VisibilityNameTestWifeProperty); }
            set { SetValue(VisibilityNameTestWifeProperty, value); OnPropertyChanged(); }
        }


	}


    public partial class TestChildDetailsView : ViewBase
    {
        public TestChildDetailsView()
        {
			InitializeComponent();
        }

        public TestChildDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, TestChildDetailsViewModel TestChildDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = TestChildDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            //NotUpdateAttribute attr;


            //attr = typeof(HVTApp.Model.POCOs.TestChild).GetProperty(nameof(HVTApp.Model.POCOs.TestChild.Husband)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHusbandTestChild = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestChild).GetProperty(nameof(HVTApp.Model.POCOs.TestChild.Wife)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityWifeTestChild = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.TestChild).GetProperty(nameof(HVTApp.Model.POCOs.TestChild.Name)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityNameTestChild = Visibility.Collapsed;



        }



        public static readonly DependencyProperty VisibilityHusbandTestChildProperty = DependencyProperty.Register("VisibilityHusbandTestChild", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHusbandTestChild
        {
            get { return (Visibility) GetValue(VisibilityHusbandTestChildProperty); }
            set { SetValue(VisibilityHusbandTestChildProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityWifeTestChildProperty = DependencyProperty.Register("VisibilityWifeTestChild", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityWifeTestChild
        {
            get { return (Visibility) GetValue(VisibilityWifeTestChildProperty); }
            set { SetValue(VisibilityWifeTestChildProperty, value); OnPropertyChanged(); }
        }



        public static readonly DependencyProperty VisibilityNameTestChildProperty = DependencyProperty.Register("VisibilityNameTestChild", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNameTestChild
        {
            get { return (Visibility) GetValue(VisibilityNameTestChildProperty); }
            set { SetValue(VisibilityNameTestChildProperty, value); OnPropertyChanged(); }
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


            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.ProductBlock)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityProductBlockProduct = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.DependentProducts)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityDependentProductsProduct = Visibility.Collapsed;



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


            //attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Code)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityCodeOffer = Visibility.Collapsed;


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



        public static readonly DependencyProperty VisibilityCodeOfferProperty = DependencyProperty.Register("VisibilityCodeOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCodeOffer
        {
            get { return (Visibility) GetValue(VisibilityCodeOfferProperty); }
            set { SetValue(VisibilityCodeOfferProperty, value); OnPropertyChanged(); }
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


            //attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.HighProbability)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityHighProbabilityProject = Visibility.Collapsed;


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



        public static readonly DependencyProperty VisibilityHighProbabilityProjectProperty = DependencyProperty.Register("VisibilityHighProbabilityProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHighProbabilityProject
        {
            get { return (Visibility) GetValue(VisibilityHighProbabilityProjectProperty); }
            set { SetValue(VisibilityHighProbabilityProjectProperty, value); OnPropertyChanged(); }
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


            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.PersonalNumber)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityPersonalNumberUser = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.RoleCurrent)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRoleCurrentUser = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Roles)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityRolesUser = Visibility.Collapsed;


            //attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Employee)).GetCustomAttribute<NotUpdateAttribute>();
            //if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
            //    VisibilityEmployeeUser = Visibility.Collapsed;



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



        public static readonly DependencyProperty VisibilityPersonalNumberUserProperty = DependencyProperty.Register("VisibilityPersonalNumberUser", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPersonalNumberUser
        {
            get { return (Visibility) GetValue(VisibilityPersonalNumberUserProperty); }
            set { SetValue(VisibilityPersonalNumberUserProperty, value); OnPropertyChanged(); }
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


	}


}
