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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.Designation)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDesignation = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.StructureCostNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStructureCostNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProduct = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DesignationVisibilityProperty = DependencyProperty.Register("VisibilityDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignation
        {
            get { return (Visibility) GetValue(DesignationVisibilityProperty); }
            set { SetValue(DesignationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StructureCostNumberVisibilityProperty = DependencyProperty.Register("VisibilityStructureCostNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStructureCostNumber
        {
            get { return (Visibility) GetValue(StructureCostNumberVisibilityProperty); }
            set { SetValue(StructureCostNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("VisibilityProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProduct
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }


	}


    public partial class DocumentIncomingNumberDetailsView : ViewBase
    {
        public DocumentIncomingNumberDetailsView()
        {
			InitializeComponent();
        }

        public DocumentIncomingNumberDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentIncomingNumberDetailsViewModel DocumentIncomingNumberDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DocumentIncomingNumberDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.DocumentIncomingNumber).GetProperty(nameof(HVTApp.Model.POCOs.DocumentIncomingNumber.Num)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityNum = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumVisibilityProperty = DependencyProperty.Register("VisibilityNum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNum
        {
            get { return (Visibility) GetValue(NumVisibilityProperty); }
            set { SetValue(NumVisibilityProperty, value); }
        }


	}


    public partial class DocumentOutgoingNumberDetailsView : ViewBase
    {
        public DocumentOutgoingNumberDetailsView()
        {
			InitializeComponent();
        }

        public DocumentOutgoingNumberDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, DocumentOutgoingNumberDetailsViewModel DocumentOutgoingNumberDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = DocumentOutgoingNumberDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.DocumentOutgoingNumber).GetProperty(nameof(HVTApp.Model.POCOs.DocumentOutgoingNumber.Num)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityNum = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumVisibilityProperty = DependencyProperty.Register("VisibilityNum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNum
        {
            get { return (Visibility) GetValue(NumVisibilityProperty); }
            set { SetValue(NumVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.PaymentActual).GetProperty(nameof(HVTApp.Model.POCOs.PaymentActual.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentActual).GetProperty(nameof(HVTApp.Model.POCOs.PaymentActual.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySum = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentActual).GetProperty(nameof(HVTApp.Model.POCOs.PaymentActual.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityComment = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumVisibilityProperty = DependencyProperty.Register("VisibilitySum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySum
        {
            get { return (Visibility) GetValue(SumVisibilityProperty); }
            set { SetValue(SumVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("VisibilityComment", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityComment
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.DateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDateCalculated = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Part)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPart = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityComment = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Condition)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCondition = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateCalculatedVisibilityProperty = DependencyProperty.Register("VisibilityDateCalculated", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateCalculated
        {
            get { return (Visibility) GetValue(DateCalculatedVisibilityProperty); }
            set { SetValue(DateCalculatedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PartVisibilityProperty = DependencyProperty.Register("VisibilityPart", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPart
        {
            get { return (Visibility) GetValue(PartVisibilityProperty); }
            set { SetValue(PartVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("VisibilityComment", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityComment
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ConditionVisibilityProperty = DependencyProperty.Register("VisibilityCondition", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCondition
        {
            get { return (Visibility) GetValue(ConditionVisibilityProperty); }
            set { SetValue(ConditionVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductBlockIsService).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlockIsService.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParameters = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParametersVisibilityProperty = DependencyProperty.Register("VisibilityParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameters
        {
            get { return (Visibility) GetValue(ParametersVisibilityProperty); }
            set { SetValue(ParametersVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductIncluded).GetProperty(nameof(HVTApp.Model.POCOs.ProductIncluded.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProduct = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductIncluded).GetProperty(nameof(HVTApp.Model.POCOs.ProductIncluded.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAmount = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("VisibilityProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProduct
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AmountVisibilityProperty = DependencyProperty.Register("VisibilityAmount", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmount
        {
            get { return (Visibility) GetValue(AmountVisibilityProperty); }
            set { SetValue(AmountVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductDesignation.Designation)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDesignation = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductDesignation.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParameters = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DesignationVisibilityProperty = DependencyProperty.Register("VisibilityDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignation
        {
            get { return (Visibility) GetValue(DesignationVisibilityProperty); }
            set { SetValue(DesignationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParametersVisibilityProperty = DependencyProperty.Register("VisibilityParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameters
        {
            get { return (Visibility) GetValue(ParametersVisibilityProperty); }
            set { SetValue(ParametersVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductType).GetProperty(nameof(HVTApp.Model.POCOs.ProductType.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductTypeDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductTypeDesignation.ProductType)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductType = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductTypeDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductTypeDesignation.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParameters = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProductTypeVisibilityProperty = DependencyProperty.Register("VisibilityProductType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductType
        {
            get { return (Visibility) GetValue(ProductTypeVisibilityProperty); }
            set { SetValue(ProductTypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParametersVisibilityProperty = DependencyProperty.Register("VisibilityParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameters
        {
            get { return (Visibility) GetValue(ParametersVisibilityProperty); }
            set { SetValue(ParametersVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProjectType).GetProperty(nameof(HVTApp.Model.POCOs.ProjectType.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.OurCompanyId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOurCompanyId = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.ActualPriceTerm)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityActualPriceTerm = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromStartToEndProduction)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStandartTermFromStartToEndProduction = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromPickToEndProduction)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStandartTermFromPickToEndProduction = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartPaymentsConditionSetId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStandartPaymentsConditionSetId = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OurCompanyIdVisibilityProperty = DependencyProperty.Register("VisibilityOurCompanyId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOurCompanyId
        {
            get { return (Visibility) GetValue(OurCompanyIdVisibilityProperty); }
            set { SetValue(OurCompanyIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ActualPriceTermVisibilityProperty = DependencyProperty.Register("VisibilityActualPriceTerm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActualPriceTerm
        {
            get { return (Visibility) GetValue(ActualPriceTermVisibilityProperty); }
            set { SetValue(ActualPriceTermVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StandartTermFromStartToEndProductionVisibilityProperty = DependencyProperty.Register("VisibilityStandartTermFromStartToEndProduction", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartTermFromStartToEndProduction
        {
            get { return (Visibility) GetValue(StandartTermFromStartToEndProductionVisibilityProperty); }
            set { SetValue(StandartTermFromStartToEndProductionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StandartTermFromPickToEndProductionVisibilityProperty = DependencyProperty.Register("VisibilityStandartTermFromPickToEndProduction", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartTermFromPickToEndProduction
        {
            get { return (Visibility) GetValue(StandartTermFromPickToEndProductionVisibilityProperty); }
            set { SetValue(StandartTermFromPickToEndProductionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StandartPaymentsConditionSetIdVisibilityProperty = DependencyProperty.Register("VisibilityStandartPaymentsConditionSetId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartPaymentsConditionSetId
        {
            get { return (Visibility) GetValue(StandartPaymentsConditionSetIdVisibilityProperty); }
            set { SetValue(StandartPaymentsConditionSetIdVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Address).GetProperty(nameof(HVTApp.Model.POCOs.Address.Description)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDescription = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Address).GetProperty(nameof(HVTApp.Model.POCOs.Address.Locality)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityLocality = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DescriptionVisibilityProperty = DependencyProperty.Register("VisibilityDescription", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDescription
        {
            get { return (Visibility) GetValue(DescriptionVisibilityProperty); }
            set { SetValue(DescriptionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty LocalityVisibilityProperty = DependencyProperty.Register("VisibilityLocality", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLocality
        {
            get { return (Visibility) GetValue(LocalityVisibilityProperty); }
            set { SetValue(LocalityVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Country).GetProperty(nameof(HVTApp.Model.POCOs.Country.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.District).GetProperty(nameof(HVTApp.Model.POCOs.District.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.District).GetProperty(nameof(HVTApp.Model.POCOs.District.Country)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCountry = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CountryVisibilityProperty = DependencyProperty.Register("VisibilityCountry", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCountry
        {
            get { return (Visibility) GetValue(CountryVisibilityProperty); }
            set { SetValue(CountryVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.LocalityType)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityLocalityType = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.Region)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRegion = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsCountryCapital)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsCountryCapital = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsDistrictCapital)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsDistrictCapital = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsRegionCapital)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsRegionCapital = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.StandartDeliveryPeriod)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStandartDeliveryPeriod = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.DistanceToEkb)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDistanceToEkb = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty LocalityTypeVisibilityProperty = DependencyProperty.Register("VisibilityLocalityType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLocalityType
        {
            get { return (Visibility) GetValue(LocalityTypeVisibilityProperty); }
            set { SetValue(LocalityTypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegionVisibilityProperty = DependencyProperty.Register("VisibilityRegion", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegion
        {
            get { return (Visibility) GetValue(RegionVisibilityProperty); }
            set { SetValue(RegionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsCountryCapitalVisibilityProperty = DependencyProperty.Register("VisibilityIsCountryCapital", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsCountryCapital
        {
            get { return (Visibility) GetValue(IsCountryCapitalVisibilityProperty); }
            set { SetValue(IsCountryCapitalVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsDistrictCapitalVisibilityProperty = DependencyProperty.Register("VisibilityIsDistrictCapital", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsDistrictCapital
        {
            get { return (Visibility) GetValue(IsDistrictCapitalVisibilityProperty); }
            set { SetValue(IsDistrictCapitalVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsRegionCapitalVisibilityProperty = DependencyProperty.Register("VisibilityIsRegionCapital", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsRegionCapital
        {
            get { return (Visibility) GetValue(IsRegionCapitalVisibilityProperty); }
            set { SetValue(IsRegionCapitalVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StandartDeliveryPeriodVisibilityProperty = DependencyProperty.Register("VisibilityStandartDeliveryPeriod", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStandartDeliveryPeriod
        {
            get { return (Visibility) GetValue(StandartDeliveryPeriodVisibilityProperty); }
            set { SetValue(StandartDeliveryPeriodVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DistanceToEkbVisibilityProperty = DependencyProperty.Register("VisibilityDistanceToEkb", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDistanceToEkb
        {
            get { return (Visibility) GetValue(DistanceToEkbVisibilityProperty); }
            set { SetValue(DistanceToEkbVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.LocalityType).GetProperty(nameof(HVTApp.Model.POCOs.LocalityType.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFullName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.LocalityType).GetProperty(nameof(HVTApp.Model.POCOs.LocalityType.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShortName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("VisibilityFullName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullName
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("VisibilityShortName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortName
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Region).GetProperty(nameof(HVTApp.Model.POCOs.Region.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Region).GetProperty(nameof(HVTApp.Model.POCOs.Region.District)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDistrict = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DistrictVisibilityProperty = DependencyProperty.Register("VisibilityDistrict", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDistrict
        {
            get { return (Visibility) GetValue(DistrictVisibilityProperty); }
            set { SetValue(DistrictVisibilityProperty, value); }
        }


	}


    public partial class CalculatePriceTaskDetailsView : ViewBase
    {
        public CalculatePriceTaskDetailsView()
        {
			InitializeComponent();
        }

        public CalculatePriceTaskDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, CalculatePriceTaskDetailsViewModel CalculatePriceTaskDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = CalculatePriceTaskDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Status)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStatus = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySum = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.ProductBlock)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductBlock = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Projects)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProjects = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Offers)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOffers = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Specifications)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySpecifications = Visibility.Collapsed;



        }



        public static readonly DependencyProperty StatusVisibilityProperty = DependencyProperty.Register("VisibilityStatus", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStatus
        {
            get { return (Visibility) GetValue(StatusVisibilityProperty); }
            set { SetValue(StatusVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumVisibilityProperty = DependencyProperty.Register("VisibilitySum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySum
        {
            get { return (Visibility) GetValue(SumVisibilityProperty); }
            set { SetValue(SumVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductBlockVisibilityProperty = DependencyProperty.Register("VisibilityProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductBlock
        {
            get { return (Visibility) GetValue(ProductBlockVisibilityProperty); }
            set { SetValue(ProductBlockVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProjectsVisibilityProperty = DependencyProperty.Register("VisibilityProjects", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProjects
        {
            get { return (Visibility) GetValue(ProjectsVisibilityProperty); }
            set { SetValue(ProjectsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OffersVisibilityProperty = DependencyProperty.Register("VisibilityOffers", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOffers
        {
            get { return (Visibility) GetValue(OffersVisibilityProperty); }
            set { SetValue(OffersVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SpecificationsVisibilityProperty = DependencyProperty.Register("VisibilitySpecifications", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySpecifications
        {
            get { return (Visibility) GetValue(SpecificationsVisibilityProperty); }
            set { SetValue(SpecificationsVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Sum).GetProperty(nameof(HVTApp.Model.POCOs.Sum.Type)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityType = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Sum).GetProperty(nameof(HVTApp.Model.POCOs.Sum.Currency)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCurrency = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Sum).GetProperty(nameof(HVTApp.Model.POCOs.Sum.Value)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityValue = Visibility.Collapsed;



        }



        public static readonly DependencyProperty TypeVisibilityProperty = DependencyProperty.Register("VisibilityType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityType
        {
            get { return (Visibility) GetValue(TypeVisibilityProperty); }
            set { SetValue(TypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CurrencyVisibilityProperty = DependencyProperty.Register("VisibilityCurrency", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCurrency
        {
            get { return (Visibility) GetValue(CurrencyVisibilityProperty); }
            set { SetValue(CurrencyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ValueVisibilityProperty = DependencyProperty.Register("VisibilityValue", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityValue
        {
            get { return (Visibility) GetValue(ValueVisibilityProperty); }
            set { SetValue(ValueVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.FirstCurrency)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFirstCurrency = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.SecondCurrency)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySecondCurrency = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.ExchangeRate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityExchangeRate = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FirstCurrencyVisibilityProperty = DependencyProperty.Register("VisibilityFirstCurrency", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFirstCurrency
        {
            get { return (Visibility) GetValue(FirstCurrencyVisibilityProperty); }
            set { SetValue(FirstCurrencyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SecondCurrencyVisibilityProperty = DependencyProperty.Register("VisibilitySecondCurrency", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySecondCurrency
        {
            get { return (Visibility) GetValue(SecondCurrencyVisibilityProperty); }
            set { SetValue(SecondCurrencyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ExchangeRateVisibilityProperty = DependencyProperty.Register("VisibilityExchangeRate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityExchangeRate
        {
            get { return (Visibility) GetValue(ExchangeRateVisibilityProperty); }
            set { SetValue(ExchangeRateVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.DescribeProductBlockTask).GetProperty(nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.ProductBlock)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductBlock = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.DescribeProductBlockTask).GetProperty(nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProduct = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProductBlockVisibilityProperty = DependencyProperty.Register("VisibilityProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductBlock
        {
            get { return (Visibility) GetValue(ProductBlockVisibilityProperty); }
            set { SetValue(ProductBlockVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("VisibilityProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProduct
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Note).GetProperty(nameof(HVTApp.Model.POCOs.Note.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Note).GetProperty(nameof(HVTApp.Model.POCOs.Note.Text)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityText = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Note).GetProperty(nameof(HVTApp.Model.POCOs.Note.IsImportant)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsImportant = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register("VisibilityText", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityText
        {
            get { return (Visibility) GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsImportantVisibilityProperty = DependencyProperty.Register("VisibilityIsImportant", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsImportant
        {
            get { return (Visibility) GetValue(IsImportantVisibilityProperty); }
            set { SetValue(IsImportantVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Cost)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCost = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Offer)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOffer = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProduct = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.ProductsIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductsIncluded = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Facility)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFacility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPaymentConditionSet = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductionTerm = Visibility.Collapsed;



        }



        public static readonly DependencyProperty CostVisibilityProperty = DependencyProperty.Register("VisibilityCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCost
        {
            get { return (Visibility) GetValue(CostVisibilityProperty); }
            set { SetValue(CostVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OfferVisibilityProperty = DependencyProperty.Register("VisibilityOffer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOffer
        {
            get { return (Visibility) GetValue(OfferVisibilityProperty); }
            set { SetValue(OfferVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("VisibilityProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProduct
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductsIncludedVisibilityProperty = DependencyProperty.Register("VisibilityProductsIncluded", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductsIncluded
        {
            get { return (Visibility) GetValue(ProductsIncludedVisibilityProperty); }
            set { SetValue(ProductsIncludedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FacilityVisibilityProperty = DependencyProperty.Register("VisibilityFacility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFacility
        {
            get { return (Visibility) GetValue(FacilityVisibilityProperty); }
            set { SetValue(FacilityVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentConditionSetVisibilityProperty = DependencyProperty.Register("VisibilityPaymentConditionSet", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionSet
        {
            get { return (Visibility) GetValue(PaymentConditionSetVisibilityProperty); }
            set { SetValue(PaymentConditionSetVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductionTermVisibilityProperty = DependencyProperty.Register("VisibilityProductionTerm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductionTerm
        {
            get { return (Visibility) GetValue(ProductionTermVisibilityProperty); }
            set { SetValue(ProductionTermVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.PaymentConditionSet).GetProperty(nameof(HVTApp.Model.POCOs.PaymentConditionSet.PaymentConditions)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPaymentConditions = Visibility.Collapsed;



        }



        public static readonly DependencyProperty PaymentConditionsVisibilityProperty = DependencyProperty.Register("VisibilityPaymentConditions", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditions
        {
            get { return (Visibility) GetValue(PaymentConditionsVisibilityProperty); }
            set { SetValue(PaymentConditionsVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Designation)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDesignation = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.DesignationSpecial)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDesignationSpecial = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParameters = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Prices)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPrices = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.StructureCostNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStructureCostNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.IsService)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsService = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.LastPriceDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityLastPriceDate = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DesignationVisibilityProperty = DependencyProperty.Register("VisibilityDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignation
        {
            get { return (Visibility) GetValue(DesignationVisibilityProperty); }
            set { SetValue(DesignationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DesignationSpecialVisibilityProperty = DependencyProperty.Register("VisibilityDesignationSpecial", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignationSpecial
        {
            get { return (Visibility) GetValue(DesignationSpecialVisibilityProperty); }
            set { SetValue(DesignationSpecialVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParametersVisibilityProperty = DependencyProperty.Register("VisibilityParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameters
        {
            get { return (Visibility) GetValue(ParametersVisibilityProperty); }
            set { SetValue(ParametersVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PricesVisibilityProperty = DependencyProperty.Register("VisibilityPrices", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPrices
        {
            get { return (Visibility) GetValue(PricesVisibilityProperty); }
            set { SetValue(PricesVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StructureCostNumberVisibilityProperty = DependencyProperty.Register("VisibilityStructureCostNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStructureCostNumber
        {
            get { return (Visibility) GetValue(StructureCostNumberVisibilityProperty); }
            set { SetValue(StructureCostNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsServiceVisibilityProperty = DependencyProperty.Register("VisibilityIsService", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsService
        {
            get { return (Visibility) GetValue(IsServiceVisibilityProperty); }
            set { SetValue(IsServiceVisibilityProperty, value); }
        }



        public static readonly DependencyProperty LastPriceDateVisibilityProperty = DependencyProperty.Register("VisibilityLastPriceDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastPriceDate
        {
            get { return (Visibility) GetValue(LastPriceDateVisibilityProperty); }
            set { SetValue(LastPriceDateVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductDependent).GetProperty(nameof(HVTApp.Model.POCOs.ProductDependent.MainProductId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityMainProductId = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductDependent).GetProperty(nameof(HVTApp.Model.POCOs.ProductDependent.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProduct = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductDependent).GetProperty(nameof(HVTApp.Model.POCOs.ProductDependent.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAmount = Visibility.Collapsed;



        }



        public static readonly DependencyProperty MainProductIdVisibilityProperty = DependencyProperty.Register("VisibilityMainProductId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMainProductId
        {
            get { return (Visibility) GetValue(MainProductIdVisibilityProperty); }
            set { SetValue(MainProductIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("VisibilityProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProduct
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AmountVisibilityProperty = DependencyProperty.Register("VisibilityAmount", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAmount
        {
            get { return (Visibility) GetValue(AmountVisibilityProperty); }
            set { SetValue(AmountVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductionTask).GetProperty(nameof(HVTApp.Model.POCOs.ProductionTask.DateTask)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDateTask = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductionTask).GetProperty(nameof(HVTApp.Model.POCOs.ProductionTask.SalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySalesUnits = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateTaskVisibilityProperty = DependencyProperty.Register("VisibilityDateTask", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateTask
        {
            get { return (Visibility) GetValue(DateTaskVisibilityProperty); }
            set { SetValue(DateTaskVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SalesUnitsVisibilityProperty = DependencyProperty.Register("VisibilitySalesUnits", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySalesUnits
        {
            get { return (Visibility) GetValue(SalesUnitsVisibilityProperty); }
            set { SetValue(SalesUnitsVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.SalesBlock).GetProperty(nameof(HVTApp.Model.POCOs.SalesBlock.ParentSalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParentSalesUnits = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesBlock).GetProperty(nameof(HVTApp.Model.POCOs.SalesBlock.ChildSalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityChildSalesUnits = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParentSalesUnitsVisibilityProperty = DependencyProperty.Register("VisibilityParentSalesUnits", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentSalesUnits
        {
            get { return (Visibility) GetValue(ParentSalesUnitsVisibilityProperty); }
            set { SetValue(ParentSalesUnitsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ChildSalesUnitsVisibilityProperty = DependencyProperty.Register("VisibilityChildSalesUnits", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildSalesUnits
        {
            get { return (Visibility) GetValue(ChildSalesUnitsVisibilityProperty); }
            set { SetValue(ChildSalesUnitsVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.BankName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityBankName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.BankIdentificationCode)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityBankIdentificationCode = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.CorrespondentAccount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCorrespondentAccount = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.CheckingAccount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCheckingAccount = Visibility.Collapsed;



        }



        public static readonly DependencyProperty BankNameVisibilityProperty = DependencyProperty.Register("VisibilityBankName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBankName
        {
            get { return (Visibility) GetValue(BankNameVisibilityProperty); }
            set { SetValue(BankNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty BankIdentificationCodeVisibilityProperty = DependencyProperty.Register("VisibilityBankIdentificationCode", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBankIdentificationCode
        {
            get { return (Visibility) GetValue(BankIdentificationCodeVisibilityProperty); }
            set { SetValue(BankIdentificationCodeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CorrespondentAccountVisibilityProperty = DependencyProperty.Register("VisibilityCorrespondentAccount", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCorrespondentAccount
        {
            get { return (Visibility) GetValue(CorrespondentAccountVisibilityProperty); }
            set { SetValue(CorrespondentAccountVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CheckingAccountVisibilityProperty = DependencyProperty.Register("VisibilityCheckingAccount", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCheckingAccount
        {
            get { return (Visibility) GetValue(CheckingAccountVisibilityProperty); }
            set { SetValue(CheckingAccountVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFullName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShortName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Inn)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityInn = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Kpp)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityKpp = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Form)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityForm = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ParentCompany)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParentCompany = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.AddressLegal)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAddressLegal = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.AddressPost)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAddressPost = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.BankDetailsList)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityBankDetailsList = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ActivityFilds)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityActivityFilds = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("VisibilityFullName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullName
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("VisibilityShortName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortName
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty InnVisibilityProperty = DependencyProperty.Register("VisibilityInn", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityInn
        {
            get { return (Visibility) GetValue(InnVisibilityProperty); }
            set { SetValue(InnVisibilityProperty, value); }
        }



        public static readonly DependencyProperty KppVisibilityProperty = DependencyProperty.Register("VisibilityKpp", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityKpp
        {
            get { return (Visibility) GetValue(KppVisibilityProperty); }
            set { SetValue(KppVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FormVisibilityProperty = DependencyProperty.Register("VisibilityForm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityForm
        {
            get { return (Visibility) GetValue(FormVisibilityProperty); }
            set { SetValue(FormVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParentCompanyVisibilityProperty = DependencyProperty.Register("VisibilityParentCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentCompany
        {
            get { return (Visibility) GetValue(ParentCompanyVisibilityProperty); }
            set { SetValue(ParentCompanyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AddressLegalVisibilityProperty = DependencyProperty.Register("VisibilityAddressLegal", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddressLegal
        {
            get { return (Visibility) GetValue(AddressLegalVisibilityProperty); }
            set { SetValue(AddressLegalVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AddressPostVisibilityProperty = DependencyProperty.Register("VisibilityAddressPost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddressPost
        {
            get { return (Visibility) GetValue(AddressPostVisibilityProperty); }
            set { SetValue(AddressPostVisibilityProperty, value); }
        }



        public static readonly DependencyProperty BankDetailsListVisibilityProperty = DependencyProperty.Register("VisibilityBankDetailsList", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBankDetailsList
        {
            get { return (Visibility) GetValue(BankDetailsListVisibilityProperty); }
            set { SetValue(BankDetailsListVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ActivityFildsVisibilityProperty = DependencyProperty.Register("VisibilityActivityFilds", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActivityFilds
        {
            get { return (Visibility) GetValue(ActivityFildsVisibilityProperty); }
            set { SetValue(ActivityFildsVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.CompanyForm).GetProperty(nameof(HVTApp.Model.POCOs.CompanyForm.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFullName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CompanyForm).GetProperty(nameof(HVTApp.Model.POCOs.CompanyForm.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShortName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("VisibilityFullName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullName
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("VisibilityShortName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortName
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.DocumentsRegistrationDetails).GetProperty(nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRegistrationNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.DocumentsRegistrationDetails).GetProperty(nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRegistrationDate = Visibility.Collapsed;



        }



        public static readonly DependencyProperty RegistrationNumberVisibilityProperty = DependencyProperty.Register("VisibilityRegistrationNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationNumber
        {
            get { return (Visibility) GetValue(RegistrationNumberVisibilityProperty); }
            set { SetValue(RegistrationNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDateVisibilityProperty = DependencyProperty.Register("VisibilityRegistrationDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationDate
        {
            get { return (Visibility) GetValue(RegistrationDateVisibilityProperty); }
            set { SetValue(RegistrationDateVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.EmployeesPosition).GetProperty(nameof(HVTApp.Model.POCOs.EmployeesPosition.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.FacilityType).GetProperty(nameof(HVTApp.Model.POCOs.FacilityType.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFullName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.FacilityType).GetProperty(nameof(HVTApp.Model.POCOs.FacilityType.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShortName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("VisibilityFullName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullName
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("VisibilityShortName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortName
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ActivityField).GetProperty(nameof(HVTApp.Model.POCOs.ActivityField.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ActivityField).GetProperty(nameof(HVTApp.Model.POCOs.ActivityField.ActivityFieldEnum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityActivityFieldEnum = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ActivityFieldEnumVisibilityProperty = DependencyProperty.Register("VisibilityActivityFieldEnum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityActivityFieldEnum
        {
            get { return (Visibility) GetValue(ActivityFieldEnumVisibilityProperty); }
            set { SetValue(ActivityFieldEnumVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Contract).GetProperty(nameof(HVTApp.Model.POCOs.Contract.Number)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Contract).GetProperty(nameof(HVTApp.Model.POCOs.Contract.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Contract).GetProperty(nameof(HVTApp.Model.POCOs.Contract.Contragent)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityContragent = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumberVisibilityProperty = DependencyProperty.Register("VisibilityNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumber
        {
            get { return (Visibility) GetValue(NumberVisibilityProperty); }
            set { SetValue(NumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ContragentVisibilityProperty = DependencyProperty.Register("VisibilityContragent", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityContragent
        {
            get { return (Visibility) GetValue(ContragentVisibilityProperty); }
            set { SetValue(ContragentVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Measure).GetProperty(nameof(HVTApp.Model.POCOs.Measure.FullName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFullName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Measure).GetProperty(nameof(HVTApp.Model.POCOs.Measure.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShortName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("VisibilityFullName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFullName
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("VisibilityShortName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShortName
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.ParameterGroup)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParameterGroup = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.Value)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityValue = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.ParameterRelations)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParameterRelations = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.IsOrigin)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsOrigin = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParameterGroupVisibilityProperty = DependencyProperty.Register("VisibilityParameterGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameterGroup
        {
            get { return (Visibility) GetValue(ParameterGroupVisibilityProperty); }
            set { SetValue(ParameterGroupVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ValueVisibilityProperty = DependencyProperty.Register("VisibilityValue", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityValue
        {
            get { return (Visibility) GetValue(ValueVisibilityProperty); }
            set { SetValue(ValueVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParameterRelationsVisibilityProperty = DependencyProperty.Register("VisibilityParameterRelations", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameterRelations
        {
            get { return (Visibility) GetValue(ParameterRelationsVisibilityProperty); }
            set { SetValue(ParameterRelationsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsOriginVisibilityProperty = DependencyProperty.Register("VisibilityIsOrigin", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsOrigin
        {
            get { return (Visibility) GetValue(IsOriginVisibilityProperty); }
            set { SetValue(IsOriginVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ParameterGroup).GetProperty(nameof(HVTApp.Model.POCOs.ParameterGroup.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ParameterGroup).GetProperty(nameof(HVTApp.Model.POCOs.ParameterGroup.Measure)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityMeasure = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty MeasureVisibilityProperty = DependencyProperty.Register("VisibilityMeasure", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityMeasure
        {
            get { return (Visibility) GetValue(MeasureVisibilityProperty); }
            set { SetValue(MeasureVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.ParentProductParameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParentProductParameters = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.ChildProductParameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityChildProductParameters = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.ChildProductsAmount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityChildProductsAmount = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.IsUnique)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsUnique = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParentProductParametersVisibilityProperty = DependencyProperty.Register("VisibilityParentProductParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParentProductParameters
        {
            get { return (Visibility) GetValue(ParentProductParametersVisibilityProperty); }
            set { SetValue(ParentProductParametersVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ChildProductParametersVisibilityProperty = DependencyProperty.Register("VisibilityChildProductParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildProductParameters
        {
            get { return (Visibility) GetValue(ChildProductParametersVisibilityProperty); }
            set { SetValue(ChildProductParametersVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ChildProductsAmountVisibilityProperty = DependencyProperty.Register("VisibilityChildProductsAmount", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildProductsAmount
        {
            get { return (Visibility) GetValue(ChildProductsAmountVisibilityProperty); }
            set { SetValue(ChildProductsAmountVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsUniqueVisibilityProperty = DependencyProperty.Register("VisibilityIsUnique", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsUnique
        {
            get { return (Visibility) GetValue(IsUniqueVisibilityProperty); }
            set { SetValue(IsUniqueVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.Surname)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySurname = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.Patronymic)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPatronymic = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.IsMan)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsMan = Visibility.Collapsed;



        }



        public static readonly DependencyProperty SurnameVisibilityProperty = DependencyProperty.Register("VisibilitySurname", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySurname
        {
            get { return (Visibility) GetValue(SurnameVisibilityProperty); }
            set { SetValue(SurnameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PatronymicVisibilityProperty = DependencyProperty.Register("VisibilityPatronymic", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPatronymic
        {
            get { return (Visibility) GetValue(PatronymicVisibilityProperty); }
            set { SetValue(PatronymicVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsManVisibilityProperty = DependencyProperty.Register("VisibilityIsMan", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsMan
        {
            get { return (Visibility) GetValue(IsManVisibilityProperty); }
            set { SetValue(IsManVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ParameterRelation).GetProperty(nameof(HVTApp.Model.POCOs.ParameterRelation.ParameterId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParameterId = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ParameterRelation).GetProperty(nameof(HVTApp.Model.POCOs.ParameterRelation.RequiredParameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRequiredParameters = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParameterIdVisibilityProperty = DependencyProperty.Register("VisibilityParameterId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParameterId
        {
            get { return (Visibility) GetValue(ParameterIdVisibilityProperty); }
            set { SetValue(ParameterIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RequiredParametersVisibilityProperty = DependencyProperty.Register("VisibilityRequiredParameters", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRequiredParameters
        {
            get { return (Visibility) GetValue(RequiredParametersVisibilityProperty); }
            set { SetValue(RequiredParametersVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Cost)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCost = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProduct = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ProductsIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductsIncluded = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Facility)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFacility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPaymentConditionSet = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductionTerm = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Project)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProject = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDateExpected)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDeliveryDateExpected = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Producer)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProducer = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.RealizationDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRealizationDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Order)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOrder = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderPosition)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOrderPosition = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SerialNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySerialNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AssembleTerm)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAssembleTerm = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SignalToStartProduction)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySignalToStartProduction = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SignalToStartProductionDone)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySignalToStartProductionDone = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStartProductionDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PickingDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPickingDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionPlanDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityEndProductionPlanDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityEndProductionDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Specification)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySpecification = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsActual)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPaymentsActual = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsPlanned)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPaymentsPlanned = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ExpectedDeliveryPeriod)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityExpectedDeliveryPeriod = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Address)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAddress = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.CostOfShipment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCostOfShipment = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShipmentDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentPlanDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShipmentPlanDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDeliveryDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.IsLoosen)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsLoosen = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumPaid)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySumPaid = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumNotPaid)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySumNotPaid = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumToStartProduction)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySumToStartProduction = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SumToShipping)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySumToShipping = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderInTakeDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOrderInTakeDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderInTakeYear)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOrderInTakeYear = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderInTakeMonth)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOrderInTakeMonth = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionConditionsDoneDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStartProductionConditionsDoneDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShippingConditionsDoneDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShippingConditionsDoneDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStartProductionDateCalculated = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityEndProductionDateCalculated = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.RealizationDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRealizationDateCalculated = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityShipmentDateCalculated = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDateCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDeliveryDateCalculated = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryPeriodCalculated)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDeliveryPeriodCalculated = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsPlannedActual)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPaymentsPlannedActual = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsPlannedGenerated)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPaymentsPlannedGenerated = Visibility.Collapsed;



        }



        public static readonly DependencyProperty CostVisibilityProperty = DependencyProperty.Register("VisibilityCost", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCost
        {
            get { return (Visibility) GetValue(CostVisibilityProperty); }
            set { SetValue(CostVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("VisibilityProduct", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProduct
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductsIncludedVisibilityProperty = DependencyProperty.Register("VisibilityProductsIncluded", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductsIncluded
        {
            get { return (Visibility) GetValue(ProductsIncludedVisibilityProperty); }
            set { SetValue(ProductsIncludedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FacilityVisibilityProperty = DependencyProperty.Register("VisibilityFacility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFacility
        {
            get { return (Visibility) GetValue(FacilityVisibilityProperty); }
            set { SetValue(FacilityVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentConditionSetVisibilityProperty = DependencyProperty.Register("VisibilityPaymentConditionSet", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionSet
        {
            get { return (Visibility) GetValue(PaymentConditionSetVisibilityProperty); }
            set { SetValue(PaymentConditionSetVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductionTermVisibilityProperty = DependencyProperty.Register("VisibilityProductionTerm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductionTerm
        {
            get { return (Visibility) GetValue(ProductionTermVisibilityProperty); }
            set { SetValue(ProductionTermVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProjectVisibilityProperty = DependencyProperty.Register("VisibilityProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProject
        {
            get { return (Visibility) GetValue(ProjectVisibilityProperty); }
            set { SetValue(ProjectVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DeliveryDateExpectedVisibilityProperty = DependencyProperty.Register("VisibilityDeliveryDateExpected", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeliveryDateExpected
        {
            get { return (Visibility) GetValue(DeliveryDateExpectedVisibilityProperty); }
            set { SetValue(DeliveryDateExpectedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProducerVisibilityProperty = DependencyProperty.Register("VisibilityProducer", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProducer
        {
            get { return (Visibility) GetValue(ProducerVisibilityProperty); }
            set { SetValue(ProducerVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RealizationDateVisibilityProperty = DependencyProperty.Register("VisibilityRealizationDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRealizationDate
        {
            get { return (Visibility) GetValue(RealizationDateVisibilityProperty); }
            set { SetValue(RealizationDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OrderVisibilityProperty = DependencyProperty.Register("VisibilityOrder", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrder
        {
            get { return (Visibility) GetValue(OrderVisibilityProperty); }
            set { SetValue(OrderVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OrderPositionVisibilityProperty = DependencyProperty.Register("VisibilityOrderPosition", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderPosition
        {
            get { return (Visibility) GetValue(OrderPositionVisibilityProperty); }
            set { SetValue(OrderPositionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SerialNumberVisibilityProperty = DependencyProperty.Register("VisibilitySerialNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySerialNumber
        {
            get { return (Visibility) GetValue(SerialNumberVisibilityProperty); }
            set { SetValue(SerialNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AssembleTermVisibilityProperty = DependencyProperty.Register("VisibilityAssembleTerm", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAssembleTerm
        {
            get { return (Visibility) GetValue(AssembleTermVisibilityProperty); }
            set { SetValue(AssembleTermVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SignalToStartProductionVisibilityProperty = DependencyProperty.Register("VisibilitySignalToStartProduction", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySignalToStartProduction
        {
            get { return (Visibility) GetValue(SignalToStartProductionVisibilityProperty); }
            set { SetValue(SignalToStartProductionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SignalToStartProductionDoneVisibilityProperty = DependencyProperty.Register("VisibilitySignalToStartProductionDone", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySignalToStartProductionDone
        {
            get { return (Visibility) GetValue(SignalToStartProductionDoneVisibilityProperty); }
            set { SetValue(SignalToStartProductionDoneVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StartProductionDateVisibilityProperty = DependencyProperty.Register("VisibilityStartProductionDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartProductionDate
        {
            get { return (Visibility) GetValue(StartProductionDateVisibilityProperty); }
            set { SetValue(StartProductionDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PickingDateVisibilityProperty = DependencyProperty.Register("VisibilityPickingDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPickingDate
        {
            get { return (Visibility) GetValue(PickingDateVisibilityProperty); }
            set { SetValue(PickingDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EndProductionPlanDateVisibilityProperty = DependencyProperty.Register("VisibilityEndProductionPlanDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEndProductionPlanDate
        {
            get { return (Visibility) GetValue(EndProductionPlanDateVisibilityProperty); }
            set { SetValue(EndProductionPlanDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EndProductionDateVisibilityProperty = DependencyProperty.Register("VisibilityEndProductionDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEndProductionDate
        {
            get { return (Visibility) GetValue(EndProductionDateVisibilityProperty); }
            set { SetValue(EndProductionDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SpecificationVisibilityProperty = DependencyProperty.Register("VisibilitySpecification", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySpecification
        {
            get { return (Visibility) GetValue(SpecificationVisibilityProperty); }
            set { SetValue(SpecificationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentsActualVisibilityProperty = DependencyProperty.Register("VisibilityPaymentsActual", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsActual
        {
            get { return (Visibility) GetValue(PaymentsActualVisibilityProperty); }
            set { SetValue(PaymentsActualVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentsPlannedVisibilityProperty = DependencyProperty.Register("VisibilityPaymentsPlanned", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsPlanned
        {
            get { return (Visibility) GetValue(PaymentsPlannedVisibilityProperty); }
            set { SetValue(PaymentsPlannedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ExpectedDeliveryPeriodVisibilityProperty = DependencyProperty.Register("VisibilityExpectedDeliveryPeriod", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityExpectedDeliveryPeriod
        {
            get { return (Visibility) GetValue(ExpectedDeliveryPeriodVisibilityProperty); }
            set { SetValue(ExpectedDeliveryPeriodVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AddressVisibilityProperty = DependencyProperty.Register("VisibilityAddress", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddress
        {
            get { return (Visibility) GetValue(AddressVisibilityProperty); }
            set { SetValue(AddressVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CostOfShipmentVisibilityProperty = DependencyProperty.Register("VisibilityCostOfShipment", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCostOfShipment
        {
            get { return (Visibility) GetValue(CostOfShipmentVisibilityProperty); }
            set { SetValue(CostOfShipmentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShipmentDateVisibilityProperty = DependencyProperty.Register("VisibilityShipmentDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShipmentDate
        {
            get { return (Visibility) GetValue(ShipmentDateVisibilityProperty); }
            set { SetValue(ShipmentDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShipmentPlanDateVisibilityProperty = DependencyProperty.Register("VisibilityShipmentPlanDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShipmentPlanDate
        {
            get { return (Visibility) GetValue(ShipmentPlanDateVisibilityProperty); }
            set { SetValue(ShipmentPlanDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DeliveryDateVisibilityProperty = DependencyProperty.Register("VisibilityDeliveryDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeliveryDate
        {
            get { return (Visibility) GetValue(DeliveryDateVisibilityProperty); }
            set { SetValue(DeliveryDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsLoosenVisibilityProperty = DependencyProperty.Register("VisibilityIsLoosen", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsLoosen
        {
            get { return (Visibility) GetValue(IsLoosenVisibilityProperty); }
            set { SetValue(IsLoosenVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumPaidVisibilityProperty = DependencyProperty.Register("VisibilitySumPaid", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumPaid
        {
            get { return (Visibility) GetValue(SumPaidVisibilityProperty); }
            set { SetValue(SumPaidVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumNotPaidVisibilityProperty = DependencyProperty.Register("VisibilitySumNotPaid", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumNotPaid
        {
            get { return (Visibility) GetValue(SumNotPaidVisibilityProperty); }
            set { SetValue(SumNotPaidVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumToStartProductionVisibilityProperty = DependencyProperty.Register("VisibilitySumToStartProduction", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumToStartProduction
        {
            get { return (Visibility) GetValue(SumToStartProductionVisibilityProperty); }
            set { SetValue(SumToStartProductionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumToShippingVisibilityProperty = DependencyProperty.Register("VisibilitySumToShipping", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySumToShipping
        {
            get { return (Visibility) GetValue(SumToShippingVisibilityProperty); }
            set { SetValue(SumToShippingVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OrderInTakeDateVisibilityProperty = DependencyProperty.Register("VisibilityOrderInTakeDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeDate
        {
            get { return (Visibility) GetValue(OrderInTakeDateVisibilityProperty); }
            set { SetValue(OrderInTakeDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OrderInTakeYearVisibilityProperty = DependencyProperty.Register("VisibilityOrderInTakeYear", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeYear
        {
            get { return (Visibility) GetValue(OrderInTakeYearVisibilityProperty); }
            set { SetValue(OrderInTakeYearVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OrderInTakeMonthVisibilityProperty = DependencyProperty.Register("VisibilityOrderInTakeMonth", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOrderInTakeMonth
        {
            get { return (Visibility) GetValue(OrderInTakeMonthVisibilityProperty); }
            set { SetValue(OrderInTakeMonthVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StartProductionConditionsDoneDateVisibilityProperty = DependencyProperty.Register("VisibilityStartProductionConditionsDoneDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartProductionConditionsDoneDate
        {
            get { return (Visibility) GetValue(StartProductionConditionsDoneDateVisibilityProperty); }
            set { SetValue(StartProductionConditionsDoneDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShippingConditionsDoneDateVisibilityProperty = DependencyProperty.Register("VisibilityShippingConditionsDoneDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShippingConditionsDoneDate
        {
            get { return (Visibility) GetValue(ShippingConditionsDoneDateVisibilityProperty); }
            set { SetValue(ShippingConditionsDoneDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StartProductionDateCalculatedVisibilityProperty = DependencyProperty.Register("VisibilityStartProductionDateCalculated", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStartProductionDateCalculated
        {
            get { return (Visibility) GetValue(StartProductionDateCalculatedVisibilityProperty); }
            set { SetValue(StartProductionDateCalculatedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EndProductionDateCalculatedVisibilityProperty = DependencyProperty.Register("VisibilityEndProductionDateCalculated", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEndProductionDateCalculated
        {
            get { return (Visibility) GetValue(EndProductionDateCalculatedVisibilityProperty); }
            set { SetValue(EndProductionDateCalculatedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RealizationDateCalculatedVisibilityProperty = DependencyProperty.Register("VisibilityRealizationDateCalculated", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRealizationDateCalculated
        {
            get { return (Visibility) GetValue(RealizationDateCalculatedVisibilityProperty); }
            set { SetValue(RealizationDateCalculatedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShipmentDateCalculatedVisibilityProperty = DependencyProperty.Register("VisibilityShipmentDateCalculated", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityShipmentDateCalculated
        {
            get { return (Visibility) GetValue(ShipmentDateCalculatedVisibilityProperty); }
            set { SetValue(ShipmentDateCalculatedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DeliveryDateCalculatedVisibilityProperty = DependencyProperty.Register("VisibilityDeliveryDateCalculated", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeliveryDateCalculated
        {
            get { return (Visibility) GetValue(DeliveryDateCalculatedVisibilityProperty); }
            set { SetValue(DeliveryDateCalculatedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DeliveryPeriodCalculatedVisibilityProperty = DependencyProperty.Register("VisibilityDeliveryPeriodCalculated", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDeliveryPeriodCalculated
        {
            get { return (Visibility) GetValue(DeliveryPeriodCalculatedVisibilityProperty); }
            set { SetValue(DeliveryPeriodCalculatedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentsPlannedActualVisibilityProperty = DependencyProperty.Register("VisibilityPaymentsPlannedActual", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsPlannedActual
        {
            get { return (Visibility) GetValue(PaymentsPlannedActualVisibilityProperty); }
            set { SetValue(PaymentsPlannedActualVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentsPlannedGeneratedVisibilityProperty = DependencyProperty.Register("VisibilityPaymentsPlannedGenerated", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentsPlannedGenerated
        {
            get { return (Visibility) GetValue(PaymentsPlannedGeneratedVisibilityProperty); }
            set { SetValue(PaymentsPlannedGeneratedVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TestFriendAddress).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendAddress.City)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCity = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriendAddress).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendAddress.Street)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStreet = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriendAddress).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendAddress.StreetNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityStreetNumber = Visibility.Collapsed;



        }



        public static readonly DependencyProperty CityVisibilityProperty = DependencyProperty.Register("VisibilityCity", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCity
        {
            get { return (Visibility) GetValue(CityVisibilityProperty); }
            set { SetValue(CityVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StreetVisibilityProperty = DependencyProperty.Register("VisibilityStreet", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStreet
        {
            get { return (Visibility) GetValue(StreetVisibilityProperty); }
            set { SetValue(StreetVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StreetNumberVisibilityProperty = DependencyProperty.Register("VisibilityStreetNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityStreetNumber
        {
            get { return (Visibility) GetValue(StreetNumberVisibilityProperty); }
            set { SetValue(StreetNumberVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.FriendGroupId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFriendGroupId = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.FirstName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFirstName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.LastName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityLastName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.Birthday)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityBirthday = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.IsDeveloper)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIsDeveloper = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendAddress)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityTestFriendAddress = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendGroup)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityTestFriendGroup = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.Emails)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityEmails = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.IdGet)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityIdGet = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendEmailGet)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityTestFriendEmailGet = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FriendGroupIdVisibilityProperty = DependencyProperty.Register("VisibilityFriendGroupId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFriendGroupId
        {
            get { return (Visibility) GetValue(FriendGroupIdVisibilityProperty); }
            set { SetValue(FriendGroupIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FirstNameVisibilityProperty = DependencyProperty.Register("VisibilityFirstName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFirstName
        {
            get { return (Visibility) GetValue(FirstNameVisibilityProperty); }
            set { SetValue(FirstNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty LastNameVisibilityProperty = DependencyProperty.Register("VisibilityLastName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLastName
        {
            get { return (Visibility) GetValue(LastNameVisibilityProperty); }
            set { SetValue(LastNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty BirthdayVisibilityProperty = DependencyProperty.Register("VisibilityBirthday", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityBirthday
        {
            get { return (Visibility) GetValue(BirthdayVisibilityProperty); }
            set { SetValue(BirthdayVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsDeveloperVisibilityProperty = DependencyProperty.Register("VisibilityIsDeveloper", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIsDeveloper
        {
            get { return (Visibility) GetValue(IsDeveloperVisibilityProperty); }
            set { SetValue(IsDeveloperVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TestFriendAddressVisibilityProperty = DependencyProperty.Register("VisibilityTestFriendAddress", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTestFriendAddress
        {
            get { return (Visibility) GetValue(TestFriendAddressVisibilityProperty); }
            set { SetValue(TestFriendAddressVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TestFriendGroupVisibilityProperty = DependencyProperty.Register("VisibilityTestFriendGroup", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTestFriendGroup
        {
            get { return (Visibility) GetValue(TestFriendGroupVisibilityProperty); }
            set { SetValue(TestFriendGroupVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EmailsVisibilityProperty = DependencyProperty.Register("VisibilityEmails", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEmails
        {
            get { return (Visibility) GetValue(EmailsVisibilityProperty); }
            set { SetValue(EmailsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IdGetVisibilityProperty = DependencyProperty.Register("VisibilityIdGet", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityIdGet
        {
            get { return (Visibility) GetValue(IdGetVisibilityProperty); }
            set { SetValue(IdGetVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TestFriendEmailGetVisibilityProperty = DependencyProperty.Register("VisibilityTestFriendEmailGet", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTestFriendEmailGet
        {
            get { return (Visibility) GetValue(TestFriendEmailGetVisibilityProperty); }
            set { SetValue(TestFriendEmailGetVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TestFriendEmail).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendEmail.Email)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityEmail = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriendEmail).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendEmail.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityComment = Visibility.Collapsed;



        }



        public static readonly DependencyProperty EmailVisibilityProperty = DependencyProperty.Register("VisibilityEmail", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEmail
        {
            get { return (Visibility) GetValue(EmailVisibilityProperty); }
            set { SetValue(EmailVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("VisibilityComment", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityComment
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TestFriendGroup).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendGroup.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriendGroup).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendGroup.FriendTests)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityFriendTests = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FriendTestsVisibilityProperty = DependencyProperty.Register("VisibilityFriendTests", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityFriendTests
        {
            get { return (Visibility) GetValue(FriendTestsVisibilityProperty); }
            set { SetValue(FriendTestsVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RequestDocument)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRequestDocument = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Author)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAuthor = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.SenderId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySenderId = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.SenderEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySenderEmployee = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RecipientId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRecipientId = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RecipientEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRecipientEmployee = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.CopyToRecipients)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCopyToRecipients = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfSender)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRegistrationDetailsOfSender = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfRecipient)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRegistrationDetailsOfRecipient = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityComment = Visibility.Collapsed;



        }



        public static readonly DependencyProperty RequestDocumentVisibilityProperty = DependencyProperty.Register("VisibilityRequestDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRequestDocument
        {
            get { return (Visibility) GetValue(RequestDocumentVisibilityProperty); }
            set { SetValue(RequestDocumentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AuthorVisibilityProperty = DependencyProperty.Register("VisibilityAuthor", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAuthor
        {
            get { return (Visibility) GetValue(AuthorVisibilityProperty); }
            set { SetValue(AuthorVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SenderIdVisibilityProperty = DependencyProperty.Register("VisibilitySenderId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderId
        {
            get { return (Visibility) GetValue(SenderIdVisibilityProperty); }
            set { SetValue(SenderIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SenderEmployeeVisibilityProperty = DependencyProperty.Register("VisibilitySenderEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderEmployee
        {
            get { return (Visibility) GetValue(SenderEmployeeVisibilityProperty); }
            set { SetValue(SenderEmployeeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RecipientIdVisibilityProperty = DependencyProperty.Register("VisibilityRecipientId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientId
        {
            get { return (Visibility) GetValue(RecipientIdVisibilityProperty); }
            set { SetValue(RecipientIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RecipientEmployeeVisibilityProperty = DependencyProperty.Register("VisibilityRecipientEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientEmployee
        {
            get { return (Visibility) GetValue(RecipientEmployeeVisibilityProperty); }
            set { SetValue(RecipientEmployeeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CopyToRecipientsVisibilityProperty = DependencyProperty.Register("VisibilityCopyToRecipients", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCopyToRecipients
        {
            get { return (Visibility) GetValue(CopyToRecipientsVisibilityProperty); }
            set { SetValue(CopyToRecipientsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDetailsOfSenderVisibilityProperty = DependencyProperty.Register("VisibilityRegistrationDetailsOfSender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationDetailsOfSender
        {
            get { return (Visibility) GetValue(RegistrationDetailsOfSenderVisibilityProperty); }
            set { SetValue(RegistrationDetailsOfSenderVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDetailsOfRecipientVisibilityProperty = DependencyProperty.Register("VisibilityRegistrationDetailsOfRecipient", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationDetailsOfRecipient
        {
            get { return (Visibility) GetValue(RegistrationDetailsOfRecipientVisibilityProperty); }
            set { SetValue(RegistrationDetailsOfRecipientVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("VisibilityComment", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityComment
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TestEntity).GetProperty(nameof(HVTApp.Model.POCOs.TestEntity.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TestHusband).GetProperty(nameof(HVTApp.Model.POCOs.TestHusband.Wife)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityWife = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestHusband).GetProperty(nameof(HVTApp.Model.POCOs.TestHusband.Children)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityChildren = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestHusband).GetProperty(nameof(HVTApp.Model.POCOs.TestHusband.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty WifeVisibilityProperty = DependencyProperty.Register("VisibilityWife", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityWife
        {
            get { return (Visibility) GetValue(WifeVisibilityProperty); }
            set { SetValue(WifeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ChildrenVisibilityProperty = DependencyProperty.Register("VisibilityChildren", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityChildren
        {
            get { return (Visibility) GetValue(ChildrenVisibilityProperty); }
            set { SetValue(ChildrenVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TestWife).GetProperty(nameof(HVTApp.Model.POCOs.TestWife.N)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityN = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestWife).GetProperty(nameof(HVTApp.Model.POCOs.TestWife.Husband)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityHusband = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestWife).GetProperty(nameof(HVTApp.Model.POCOs.TestWife.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NVisibilityProperty = DependencyProperty.Register("VisibilityN", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityN
        {
            get { return (Visibility) GetValue(NVisibilityProperty); }
            set { SetValue(NVisibilityProperty, value); }
        }



        public static readonly DependencyProperty HusbandVisibilityProperty = DependencyProperty.Register("VisibilityHusband", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHusband
        {
            get { return (Visibility) GetValue(HusbandVisibilityProperty); }
            set { SetValue(HusbandVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TestChild).GetProperty(nameof(HVTApp.Model.POCOs.TestChild.Husband)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityHusband = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestChild).GetProperty(nameof(HVTApp.Model.POCOs.TestChild.Wife)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityWife = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestChild).GetProperty(nameof(HVTApp.Model.POCOs.TestChild.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;



        }



        public static readonly DependencyProperty HusbandVisibilityProperty = DependencyProperty.Register("VisibilityHusband", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHusband
        {
            get { return (Visibility) GetValue(HusbandVisibilityProperty); }
            set { SetValue(HusbandVisibilityProperty, value); }
        }



        public static readonly DependencyProperty WifeVisibilityProperty = DependencyProperty.Register("VisibilityWife", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityWife
        {
            get { return (Visibility) GetValue(WifeVisibilityProperty); }
            set { SetValue(WifeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.SumOnDate).GetProperty(nameof(HVTApp.Model.POCOs.SumOnDate.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SumOnDate).GetProperty(nameof(HVTApp.Model.POCOs.SumOnDate.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySum = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumVisibilityProperty = DependencyProperty.Register("VisibilitySum", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySum
        {
            get { return (Visibility) GetValue(SumVisibilityProperty); }
            set { SetValue(SumVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.Designation)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDesignation = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.DesignationSpecial)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDesignationSpecial = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.ProductType)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductType = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.ProductBlock)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProductBlock = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.DependentProducts)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDependentProducts = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DesignationVisibilityProperty = DependencyProperty.Register("VisibilityDesignation", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignation
        {
            get { return (Visibility) GetValue(DesignationVisibilityProperty); }
            set { SetValue(DesignationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DesignationSpecialVisibilityProperty = DependencyProperty.Register("VisibilityDesignationSpecial", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDesignationSpecial
        {
            get { return (Visibility) GetValue(DesignationSpecialVisibilityProperty); }
            set { SetValue(DesignationSpecialVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductTypeVisibilityProperty = DependencyProperty.Register("VisibilityProductType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductType
        {
            get { return (Visibility) GetValue(ProductTypeVisibilityProperty); }
            set { SetValue(ProductTypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductBlockVisibilityProperty = DependencyProperty.Register("VisibilityProductBlock", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProductBlock
        {
            get { return (Visibility) GetValue(ProductBlockVisibilityProperty); }
            set { SetValue(ProductBlockVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DependentProductsVisibilityProperty = DependencyProperty.Register("VisibilityDependentProducts", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDependentProducts
        {
            get { return (Visibility) GetValue(DependentProductsVisibilityProperty); }
            set { SetValue(DependentProductsVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Project)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProject = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.ValidityDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityValidityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityVat = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RequestDocument)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRequestDocument = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Author)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAuthor = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.SenderId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySenderId = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.SenderEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilitySenderEmployee = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RecipientId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRecipientId = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RecipientEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRecipientEmployee = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.CopyToRecipients)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCopyToRecipients = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfSender)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRegistrationDetailsOfSender = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfRecipient)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRegistrationDetailsOfRecipient = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityComment = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProjectVisibilityProperty = DependencyProperty.Register("VisibilityProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProject
        {
            get { return (Visibility) GetValue(ProjectVisibilityProperty); }
            set { SetValue(ProjectVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ValidityDateVisibilityProperty = DependencyProperty.Register("VisibilityValidityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityValidityDate
        {
            get { return (Visibility) GetValue(ValidityDateVisibilityProperty); }
            set { SetValue(ValidityDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty VatVisibilityProperty = DependencyProperty.Register("VisibilityVat", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityVat
        {
            get { return (Visibility) GetValue(VatVisibilityProperty); }
            set { SetValue(VatVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RequestDocumentVisibilityProperty = DependencyProperty.Register("VisibilityRequestDocument", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRequestDocument
        {
            get { return (Visibility) GetValue(RequestDocumentVisibilityProperty); }
            set { SetValue(RequestDocumentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AuthorVisibilityProperty = DependencyProperty.Register("VisibilityAuthor", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAuthor
        {
            get { return (Visibility) GetValue(AuthorVisibilityProperty); }
            set { SetValue(AuthorVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SenderIdVisibilityProperty = DependencyProperty.Register("VisibilitySenderId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderId
        {
            get { return (Visibility) GetValue(SenderIdVisibilityProperty); }
            set { SetValue(SenderIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SenderEmployeeVisibilityProperty = DependencyProperty.Register("VisibilitySenderEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilitySenderEmployee
        {
            get { return (Visibility) GetValue(SenderEmployeeVisibilityProperty); }
            set { SetValue(SenderEmployeeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RecipientIdVisibilityProperty = DependencyProperty.Register("VisibilityRecipientId", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientId
        {
            get { return (Visibility) GetValue(RecipientIdVisibilityProperty); }
            set { SetValue(RecipientIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RecipientEmployeeVisibilityProperty = DependencyProperty.Register("VisibilityRecipientEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRecipientEmployee
        {
            get { return (Visibility) GetValue(RecipientEmployeeVisibilityProperty); }
            set { SetValue(RecipientEmployeeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CopyToRecipientsVisibilityProperty = DependencyProperty.Register("VisibilityCopyToRecipients", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCopyToRecipients
        {
            get { return (Visibility) GetValue(CopyToRecipientsVisibilityProperty); }
            set { SetValue(CopyToRecipientsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDetailsOfSenderVisibilityProperty = DependencyProperty.Register("VisibilityRegistrationDetailsOfSender", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationDetailsOfSender
        {
            get { return (Visibility) GetValue(RegistrationDetailsOfSenderVisibilityProperty); }
            set { SetValue(RegistrationDetailsOfSenderVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDetailsOfRecipientVisibilityProperty = DependencyProperty.Register("VisibilityRegistrationDetailsOfRecipient", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRegistrationDetailsOfRecipient
        {
            get { return (Visibility) GetValue(RegistrationDetailsOfRecipientVisibilityProperty); }
            set { SetValue(RegistrationDetailsOfRecipientVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("VisibilityComment", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityComment
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Person)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPerson = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.PhoneNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPhoneNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Email)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityEmail = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Company)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityCompany = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Position)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPosition = Visibility.Collapsed;



        }



        public static readonly DependencyProperty PersonVisibilityProperty = DependencyProperty.Register("VisibilityPerson", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPerson
        {
            get { return (Visibility) GetValue(PersonVisibilityProperty); }
            set { SetValue(PersonVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PhoneNumberVisibilityProperty = DependencyProperty.Register("VisibilityPhoneNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPhoneNumber
        {
            get { return (Visibility) GetValue(PhoneNumberVisibilityProperty); }
            set { SetValue(PhoneNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EmailVisibilityProperty = DependencyProperty.Register("VisibilityEmail", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEmail
        {
            get { return (Visibility) GetValue(EmailVisibilityProperty); }
            set { SetValue(EmailVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CompanyVisibilityProperty = DependencyProperty.Register("VisibilityCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityCompany
        {
            get { return (Visibility) GetValue(CompanyVisibilityProperty); }
            set { SetValue(CompanyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PositionVisibilityProperty = DependencyProperty.Register("VisibilityPosition", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPosition
        {
            get { return (Visibility) GetValue(PositionVisibilityProperty); }
            set { SetValue(PositionVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Order).GetProperty(nameof(HVTApp.Model.POCOs.Order.Number)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Order).GetProperty(nameof(HVTApp.Model.POCOs.Order.OpenOrderDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOpenOrderDate = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumberVisibilityProperty = DependencyProperty.Register("VisibilityNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumber
        {
            get { return (Visibility) GetValue(NumberVisibilityProperty); }
            set { SetValue(NumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OpenOrderDateVisibilityProperty = DependencyProperty.Register("VisibilityOpenOrderDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOpenOrderDate
        {
            get { return (Visibility) GetValue(OpenOrderDateVisibilityProperty); }
            set { SetValue(OpenOrderDateVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.PaymentCondition).GetProperty(nameof(HVTApp.Model.POCOs.PaymentCondition.Part)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPart = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentCondition).GetProperty(nameof(HVTApp.Model.POCOs.PaymentCondition.DaysToPoint)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDaysToPoint = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentCondition).GetProperty(nameof(HVTApp.Model.POCOs.PaymentCondition.PaymentConditionPoint)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPaymentConditionPoint = Visibility.Collapsed;



        }



        public static readonly DependencyProperty PartVisibilityProperty = DependencyProperty.Register("VisibilityPart", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPart
        {
            get { return (Visibility) GetValue(PartVisibilityProperty); }
            set { SetValue(PartVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DaysToPointVisibilityProperty = DependencyProperty.Register("VisibilityDaysToPoint", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDaysToPoint
        {
            get { return (Visibility) GetValue(DaysToPointVisibilityProperty); }
            set { SetValue(DaysToPointVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentConditionPointVisibilityProperty = DependencyProperty.Register("VisibilityPaymentConditionPoint", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPaymentConditionPoint
        {
            get { return (Visibility) GetValue(PaymentConditionPointVisibilityProperty); }
            set { SetValue(PaymentConditionPointVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Number)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Payments)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPayments = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumberVisibilityProperty = DependencyProperty.Register("VisibilityNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumber
        {
            get { return (Visibility) GetValue(NumberVisibilityProperty); }
            set { SetValue(NumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentsVisibilityProperty = DependencyProperty.Register("VisibilityPayments", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPayments
        {
            get { return (Visibility) GetValue(PaymentsVisibilityProperty); }
            set { SetValue(PaymentsVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.Type)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityType = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.OwnerCompany)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityOwnerCompany = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.Address)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityAddress = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TypeVisibilityProperty = DependencyProperty.Register("VisibilityType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityType
        {
            get { return (Visibility) GetValue(TypeVisibilityProperty); }
            set { SetValue(TypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OwnerCompanyVisibilityProperty = DependencyProperty.Register("VisibilityOwnerCompany", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityOwnerCompany
        {
            get { return (Visibility) GetValue(OwnerCompanyVisibilityProperty); }
            set { SetValue(OwnerCompanyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AddressVisibilityProperty = DependencyProperty.Register("VisibilityAddress", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityAddress
        {
            get { return (Visibility) GetValue(AddressVisibilityProperty); }
            set { SetValue(AddressVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.ProjectType)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProjectType = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.HighProbability)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityHighProbability = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.Manager)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityManager = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.Notes)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityNotes = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProjectTypeVisibilityProperty = DependencyProperty.Register("VisibilityProjectType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProjectType
        {
            get { return (Visibility) GetValue(ProjectTypeVisibilityProperty); }
            set { SetValue(ProjectTypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty HighProbabilityVisibilityProperty = DependencyProperty.Register("VisibilityHighProbability", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityHighProbability
        {
            get { return (Visibility) GetValue(HighProbabilityVisibilityProperty); }
            set { SetValue(HighProbabilityVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ManagerVisibilityProperty = DependencyProperty.Register("VisibilityManager", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityManager
        {
            get { return (Visibility) GetValue(ManagerVisibilityProperty); }
            set { SetValue(ManagerVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NotesVisibilityProperty = DependencyProperty.Register("VisibilityNotes", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNotes
        {
            get { return (Visibility) GetValue(NotesVisibilityProperty); }
            set { SetValue(NotesVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.UserRole).GetProperty(nameof(HVTApp.Model.POCOs.UserRole.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.UserRole).GetProperty(nameof(HVTApp.Model.POCOs.UserRole.Role)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRole = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RoleVisibilityProperty = DependencyProperty.Register("VisibilityRole", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRole
        {
            get { return (Visibility) GetValue(RoleVisibilityProperty); }
            set { SetValue(RoleVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Number)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDate = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityVat = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Contract)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityContract = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumberVisibilityProperty = DependencyProperty.Register("VisibilityNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityNumber
        {
            get { return (Visibility) GetValue(NumberVisibilityProperty); }
            set { SetValue(NumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("VisibilityDate", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDate
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty VatVisibilityProperty = DependencyProperty.Register("VisibilityVat", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityVat
        {
            get { return (Visibility) GetValue(VatVisibilityProperty); }
            set { SetValue(VatVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ContractVisibilityProperty = DependencyProperty.Register("VisibilityContract", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityContract
        {
            get { return (Visibility) GetValue(ContractVisibilityProperty); }
            set { SetValue(ContractVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Project)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityProject = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Types)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityTypes = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateOpen)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDateOpen = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateClose)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDateClose = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateNotice)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityDateNotice = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Participants)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityParticipants = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Winner)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityWinner = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProjectVisibilityProperty = DependencyProperty.Register("VisibilityProject", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityProject
        {
            get { return (Visibility) GetValue(ProjectVisibilityProperty); }
            set { SetValue(ProjectVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TypesVisibilityProperty = DependencyProperty.Register("VisibilityTypes", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityTypes
        {
            get { return (Visibility) GetValue(TypesVisibilityProperty); }
            set { SetValue(TypesVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateOpenVisibilityProperty = DependencyProperty.Register("VisibilityDateOpen", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateOpen
        {
            get { return (Visibility) GetValue(DateOpenVisibilityProperty); }
            set { SetValue(DateOpenVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateCloseVisibilityProperty = DependencyProperty.Register("VisibilityDateClose", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateClose
        {
            get { return (Visibility) GetValue(DateCloseVisibilityProperty); }
            set { SetValue(DateCloseVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateNoticeVisibilityProperty = DependencyProperty.Register("VisibilityDateNotice", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityDateNotice
        {
            get { return (Visibility) GetValue(DateNoticeVisibilityProperty); }
            set { SetValue(DateNoticeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParticipantsVisibilityProperty = DependencyProperty.Register("VisibilityParticipants", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityParticipants
        {
            get { return (Visibility) GetValue(ParticipantsVisibilityProperty); }
            set { SetValue(ParticipantsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty WinnerVisibilityProperty = DependencyProperty.Register("VisibilityWinner", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityWinner
        {
            get { return (Visibility) GetValue(WinnerVisibilityProperty); }
            set { SetValue(WinnerVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.TenderType).GetProperty(nameof(HVTApp.Model.POCOs.TenderType.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityName = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TenderType).GetProperty(nameof(HVTApp.Model.POCOs.TenderType.Type)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityType = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("VisibilityName", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityName
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TypeVisibilityProperty = DependencyProperty.Register("VisibilityType", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityType
        {
            get { return (Visibility) GetValue(TypeVisibilityProperty); }
            set { SetValue(TypeVisibilityProperty, value); }
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
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Login)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityLogin = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Password)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPassword = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.PersonalNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityPersonalNumber = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.RoleCurrent)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRoleCurrent = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Roles)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityRoles = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Employee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VisibilityEmployee = Visibility.Collapsed;



        }



        public static readonly DependencyProperty LoginVisibilityProperty = DependencyProperty.Register("VisibilityLogin", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityLogin
        {
            get { return (Visibility) GetValue(LoginVisibilityProperty); }
            set { SetValue(LoginVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PasswordVisibilityProperty = DependencyProperty.Register("VisibilityPassword", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPassword
        {
            get { return (Visibility) GetValue(PasswordVisibilityProperty); }
            set { SetValue(PasswordVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PersonalNumberVisibilityProperty = DependencyProperty.Register("VisibilityPersonalNumber", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityPersonalNumber
        {
            get { return (Visibility) GetValue(PersonalNumberVisibilityProperty); }
            set { SetValue(PersonalNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RoleCurrentVisibilityProperty = DependencyProperty.Register("VisibilityRoleCurrent", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRoleCurrent
        {
            get { return (Visibility) GetValue(RoleCurrentVisibilityProperty); }
            set { SetValue(RoleCurrentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RolesVisibilityProperty = DependencyProperty.Register("VisibilityRoles", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityRoles
        {
            get { return (Visibility) GetValue(RolesVisibilityProperty); }
            set { SetValue(RolesVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EmployeeVisibilityProperty = DependencyProperty.Register("VisibilityEmployee", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VisibilityEmployee
        {
            get { return (Visibility) GetValue(EmployeeVisibilityProperty); }
            set { SetValue(EmployeeVisibilityProperty, value); }
        }


	}


}
