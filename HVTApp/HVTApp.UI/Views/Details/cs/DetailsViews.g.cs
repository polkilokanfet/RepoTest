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
                DesignationVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.StructureCostNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StructureCostNumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CreateNewProductTask).GetProperty(nameof(HVTApp.Model.POCOs.CreateNewProductTask.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DesignationVisibilityProperty = DependencyProperty.Register("DesignationVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DesignationVisibility
        {
            get { return (Visibility) GetValue(DesignationVisibilityProperty); }
            set { SetValue(DesignationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StructureCostNumberVisibilityProperty = DependencyProperty.Register("StructureCostNumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StructureCostNumberVisibility
        {
            get { return (Visibility) GetValue(StructureCostNumberVisibilityProperty); }
            set { SetValue(StructureCostNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("ProductVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductVisibility
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }


	}


    public partial class PaymentActualDetailsView : ViewBase
    {
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
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentActual).GetProperty(nameof(HVTApp.Model.POCOs.PaymentActual.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SumVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentActual).GetProperty(nameof(HVTApp.Model.POCOs.PaymentActual.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CommentVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumVisibilityProperty = DependencyProperty.Register("SumVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SumVisibility
        {
            get { return (Visibility) GetValue(SumVisibilityProperty); }
            set { SetValue(SumVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("CommentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CommentVisibility
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
        }


	}


    public partial class PaymentPlannedDetailsView : ViewBase
    {
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
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SumVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CommentVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentPlanned).GetProperty(nameof(HVTApp.Model.POCOs.PaymentPlanned.Condition)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ConditionVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumVisibilityProperty = DependencyProperty.Register("SumVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SumVisibility
        {
            get { return (Visibility) GetValue(SumVisibilityProperty); }
            set { SetValue(SumVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("CommentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CommentVisibility
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ConditionVisibilityProperty = DependencyProperty.Register("ConditionVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ConditionVisibility
        {
            get { return (Visibility) GetValue(ConditionVisibilityProperty); }
            set { SetValue(ConditionVisibilityProperty, value); }
        }


	}


    public partial class ProductBlockIsServiceDetailsView : ViewBase
    {
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
                ParametersVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParametersVisibilityProperty = DependencyProperty.Register("ParametersVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParametersVisibility
        {
            get { return (Visibility) GetValue(ParametersVisibilityProperty); }
            set { SetValue(ParametersVisibilityProperty, value); }
        }


	}


    public partial class ProductIncludedDetailsView : ViewBase
    {
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
                ProductVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductIncluded).GetProperty(nameof(HVTApp.Model.POCOs.ProductIncluded.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AmountVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("ProductVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductVisibility
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AmountVisibilityProperty = DependencyProperty.Register("AmountVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AmountVisibility
        {
            get { return (Visibility) GetValue(AmountVisibilityProperty); }
            set { SetValue(AmountVisibilityProperty, value); }
        }


	}


    public partial class ProductDesignationDetailsView : ViewBase
    {
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
                DesignationVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductDesignation.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ParametersVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DesignationVisibilityProperty = DependencyProperty.Register("DesignationVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DesignationVisibility
        {
            get { return (Visibility) GetValue(DesignationVisibilityProperty); }
            set { SetValue(DesignationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParametersVisibilityProperty = DependencyProperty.Register("ParametersVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParametersVisibility
        {
            get { return (Visibility) GetValue(ParametersVisibilityProperty); }
            set { SetValue(ParametersVisibilityProperty, value); }
        }


	}


    public partial class ProductTypeDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }


	}


    public partial class ProductTypeDesignationDetailsView : ViewBase
    {
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
                ProductTypeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductTypeDesignation).GetProperty(nameof(HVTApp.Model.POCOs.ProductTypeDesignation.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ParametersVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProductTypeVisibilityProperty = DependencyProperty.Register("ProductTypeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductTypeVisibility
        {
            get { return (Visibility) GetValue(ProductTypeVisibilityProperty); }
            set { SetValue(ProductTypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParametersVisibilityProperty = DependencyProperty.Register("ParametersVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParametersVisibility
        {
            get { return (Visibility) GetValue(ParametersVisibilityProperty); }
            set { SetValue(ParametersVisibilityProperty, value); }
        }


	}


    public partial class ProjectTypeDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }


	}


    public partial class CommonOptionDetailsView : ViewBase
    {
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
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.OurCompanyId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                OurCompanyIdVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.ActualPriceTerm)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ActualPriceTermVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromStartToEndProduction)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StandartTermFromStartToEndProductionVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartTermFromPickToEndProduction)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StandartTermFromPickToEndProductionVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CommonOption).GetProperty(nameof(HVTApp.Model.POCOs.CommonOption.StandartPaymentsConditionSetId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StandartPaymentsConditionSetIdVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OurCompanyIdVisibilityProperty = DependencyProperty.Register("OurCompanyIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility OurCompanyIdVisibility
        {
            get { return (Visibility) GetValue(OurCompanyIdVisibilityProperty); }
            set { SetValue(OurCompanyIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ActualPriceTermVisibilityProperty = DependencyProperty.Register("ActualPriceTermVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ActualPriceTermVisibility
        {
            get { return (Visibility) GetValue(ActualPriceTermVisibilityProperty); }
            set { SetValue(ActualPriceTermVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StandartTermFromStartToEndProductionVisibilityProperty = DependencyProperty.Register("StandartTermFromStartToEndProductionVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StandartTermFromStartToEndProductionVisibility
        {
            get { return (Visibility) GetValue(StandartTermFromStartToEndProductionVisibilityProperty); }
            set { SetValue(StandartTermFromStartToEndProductionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StandartTermFromPickToEndProductionVisibilityProperty = DependencyProperty.Register("StandartTermFromPickToEndProductionVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StandartTermFromPickToEndProductionVisibility
        {
            get { return (Visibility) GetValue(StandartTermFromPickToEndProductionVisibilityProperty); }
            set { SetValue(StandartTermFromPickToEndProductionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StandartPaymentsConditionSetIdVisibilityProperty = DependencyProperty.Register("StandartPaymentsConditionSetIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StandartPaymentsConditionSetIdVisibility
        {
            get { return (Visibility) GetValue(StandartPaymentsConditionSetIdVisibilityProperty); }
            set { SetValue(StandartPaymentsConditionSetIdVisibilityProperty, value); }
        }


	}


    public partial class AddressDetailsView : ViewBase
    {
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
                DescriptionVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Address).GetProperty(nameof(HVTApp.Model.POCOs.Address.Locality)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                LocalityVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DescriptionVisibilityProperty = DependencyProperty.Register("DescriptionVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DescriptionVisibility
        {
            get { return (Visibility) GetValue(DescriptionVisibilityProperty); }
            set { SetValue(DescriptionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty LocalityVisibilityProperty = DependencyProperty.Register("LocalityVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility LocalityVisibility
        {
            get { return (Visibility) GetValue(LocalityVisibilityProperty); }
            set { SetValue(LocalityVisibilityProperty, value); }
        }


	}


    public partial class CountryDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }


	}


    public partial class DistrictDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.District).GetProperty(nameof(HVTApp.Model.POCOs.District.Country)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CountryVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CountryVisibilityProperty = DependencyProperty.Register("CountryVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CountryVisibility
        {
            get { return (Visibility) GetValue(CountryVisibilityProperty); }
            set { SetValue(CountryVisibilityProperty, value); }
        }


	}


    public partial class LocalityDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.LocalityType)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                LocalityTypeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.Region)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RegionVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsCountryCapital)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsCountryCapitalVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsDistrictCapital)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsDistrictCapitalVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.IsRegionCapital)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsRegionCapitalVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.StandartDeliveryPeriod)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StandartDeliveryPeriodVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Locality).GetProperty(nameof(HVTApp.Model.POCOs.Locality.DistanceToEkb)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DistanceToEkbVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty LocalityTypeVisibilityProperty = DependencyProperty.Register("LocalityTypeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility LocalityTypeVisibility
        {
            get { return (Visibility) GetValue(LocalityTypeVisibilityProperty); }
            set { SetValue(LocalityTypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegionVisibilityProperty = DependencyProperty.Register("RegionVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RegionVisibility
        {
            get { return (Visibility) GetValue(RegionVisibilityProperty); }
            set { SetValue(RegionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsCountryCapitalVisibilityProperty = DependencyProperty.Register("IsCountryCapitalVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsCountryCapitalVisibility
        {
            get { return (Visibility) GetValue(IsCountryCapitalVisibilityProperty); }
            set { SetValue(IsCountryCapitalVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsDistrictCapitalVisibilityProperty = DependencyProperty.Register("IsDistrictCapitalVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsDistrictCapitalVisibility
        {
            get { return (Visibility) GetValue(IsDistrictCapitalVisibilityProperty); }
            set { SetValue(IsDistrictCapitalVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsRegionCapitalVisibilityProperty = DependencyProperty.Register("IsRegionCapitalVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsRegionCapitalVisibility
        {
            get { return (Visibility) GetValue(IsRegionCapitalVisibilityProperty); }
            set { SetValue(IsRegionCapitalVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StandartDeliveryPeriodVisibilityProperty = DependencyProperty.Register("StandartDeliveryPeriodVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StandartDeliveryPeriodVisibility
        {
            get { return (Visibility) GetValue(StandartDeliveryPeriodVisibilityProperty); }
            set { SetValue(StandartDeliveryPeriodVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DistanceToEkbVisibilityProperty = DependencyProperty.Register("DistanceToEkbVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DistanceToEkbVisibility
        {
            get { return (Visibility) GetValue(DistanceToEkbVisibilityProperty); }
            set { SetValue(DistanceToEkbVisibilityProperty, value); }
        }


	}


    public partial class LocalityTypeDetailsView : ViewBase
    {
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
                FullNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.LocalityType).GetProperty(nameof(HVTApp.Model.POCOs.LocalityType.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ShortNameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("FullNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FullNameVisibility
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("ShortNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ShortNameVisibility
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
        }


	}


    public partial class RegionDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Region).GetProperty(nameof(HVTApp.Model.POCOs.Region.District)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DistrictVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DistrictVisibilityProperty = DependencyProperty.Register("DistrictVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DistrictVisibility
        {
            get { return (Visibility) GetValue(DistrictVisibilityProperty); }
            set { SetValue(DistrictVisibilityProperty, value); }
        }


	}


    public partial class CalculatePriceTaskDetailsView : ViewBase
    {
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
                StatusVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SumVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.ProductBlock)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductBlockVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Projects)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProjectsVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Offers)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                OffersVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CalculatePriceTask).GetProperty(nameof(HVTApp.Model.POCOs.CalculatePriceTask.Specifications)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SpecificationsVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty StatusVisibilityProperty = DependencyProperty.Register("StatusVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StatusVisibility
        {
            get { return (Visibility) GetValue(StatusVisibilityProperty); }
            set { SetValue(StatusVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumVisibilityProperty = DependencyProperty.Register("SumVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SumVisibility
        {
            get { return (Visibility) GetValue(SumVisibilityProperty); }
            set { SetValue(SumVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductBlockVisibilityProperty = DependencyProperty.Register("ProductBlockVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductBlockVisibility
        {
            get { return (Visibility) GetValue(ProductBlockVisibilityProperty); }
            set { SetValue(ProductBlockVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProjectsVisibilityProperty = DependencyProperty.Register("ProjectsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProjectsVisibility
        {
            get { return (Visibility) GetValue(ProjectsVisibilityProperty); }
            set { SetValue(ProjectsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OffersVisibilityProperty = DependencyProperty.Register("OffersVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility OffersVisibility
        {
            get { return (Visibility) GetValue(OffersVisibilityProperty); }
            set { SetValue(OffersVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SpecificationsVisibilityProperty = DependencyProperty.Register("SpecificationsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SpecificationsVisibility
        {
            get { return (Visibility) GetValue(SpecificationsVisibilityProperty); }
            set { SetValue(SpecificationsVisibilityProperty, value); }
        }


	}


    public partial class SumDetailsView : ViewBase
    {
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
                TypeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Sum).GetProperty(nameof(HVTApp.Model.POCOs.Sum.Currency)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CurrencyVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Sum).GetProperty(nameof(HVTApp.Model.POCOs.Sum.Value)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ValueVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty TypeVisibilityProperty = DependencyProperty.Register("TypeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility TypeVisibility
        {
            get { return (Visibility) GetValue(TypeVisibilityProperty); }
            set { SetValue(TypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CurrencyVisibilityProperty = DependencyProperty.Register("CurrencyVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CurrencyVisibility
        {
            get { return (Visibility) GetValue(CurrencyVisibilityProperty); }
            set { SetValue(CurrencyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ValueVisibilityProperty = DependencyProperty.Register("ValueVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ValueVisibility
        {
            get { return (Visibility) GetValue(ValueVisibilityProperty); }
            set { SetValue(ValueVisibilityProperty, value); }
        }


	}


    public partial class CurrencyExchangeRateDetailsView : ViewBase
    {
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
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.FirstCurrency)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                FirstCurrencyVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.SecondCurrency)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SecondCurrencyVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CurrencyExchangeRate).GetProperty(nameof(HVTApp.Model.POCOs.CurrencyExchangeRate.ExchangeRate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ExchangeRateVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FirstCurrencyVisibilityProperty = DependencyProperty.Register("FirstCurrencyVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FirstCurrencyVisibility
        {
            get { return (Visibility) GetValue(FirstCurrencyVisibilityProperty); }
            set { SetValue(FirstCurrencyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SecondCurrencyVisibilityProperty = DependencyProperty.Register("SecondCurrencyVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SecondCurrencyVisibility
        {
            get { return (Visibility) GetValue(SecondCurrencyVisibilityProperty); }
            set { SetValue(SecondCurrencyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ExchangeRateVisibilityProperty = DependencyProperty.Register("ExchangeRateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ExchangeRateVisibility
        {
            get { return (Visibility) GetValue(ExchangeRateVisibilityProperty); }
            set { SetValue(ExchangeRateVisibilityProperty, value); }
        }


	}


    public partial class DescribeProductBlockTaskDetailsView : ViewBase
    {
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
                ProductBlockVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.DescribeProductBlockTask).GetProperty(nameof(HVTApp.Model.POCOs.DescribeProductBlockTask.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProductBlockVisibilityProperty = DependencyProperty.Register("ProductBlockVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductBlockVisibility
        {
            get { return (Visibility) GetValue(ProductBlockVisibilityProperty); }
            set { SetValue(ProductBlockVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("ProductVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductVisibility
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }


	}


    public partial class NoteDetailsView : ViewBase
    {
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
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Note).GetProperty(nameof(HVTApp.Model.POCOs.Note.Text)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                TextVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Note).GetProperty(nameof(HVTApp.Model.POCOs.Note.IsImportant)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsImportantVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility TextVisibility
        {
            get { return (Visibility) GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsImportantVisibilityProperty = DependencyProperty.Register("IsImportantVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsImportantVisibility
        {
            get { return (Visibility) GetValue(IsImportantVisibilityProperty); }
            set { SetValue(IsImportantVisibilityProperty, value); }
        }


	}


    public partial class OfferUnitDetailsView : ViewBase
    {
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
                CostVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Offer)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                OfferVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.ProductsIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductsIncludedVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Services)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ServicesVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.Facility)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                FacilityVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PaymentConditionSetVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.OfferUnit).GetProperty(nameof(HVTApp.Model.POCOs.OfferUnit.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductionTermVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty CostVisibilityProperty = DependencyProperty.Register("CostVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CostVisibility
        {
            get { return (Visibility) GetValue(CostVisibilityProperty); }
            set { SetValue(CostVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OfferVisibilityProperty = DependencyProperty.Register("OfferVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility OfferVisibility
        {
            get { return (Visibility) GetValue(OfferVisibilityProperty); }
            set { SetValue(OfferVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("ProductVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductVisibility
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductsIncludedVisibilityProperty = DependencyProperty.Register("ProductsIncludedVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductsIncludedVisibility
        {
            get { return (Visibility) GetValue(ProductsIncludedVisibilityProperty); }
            set { SetValue(ProductsIncludedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ServicesVisibilityProperty = DependencyProperty.Register("ServicesVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ServicesVisibility
        {
            get { return (Visibility) GetValue(ServicesVisibilityProperty); }
            set { SetValue(ServicesVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FacilityVisibilityProperty = DependencyProperty.Register("FacilityVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FacilityVisibility
        {
            get { return (Visibility) GetValue(FacilityVisibilityProperty); }
            set { SetValue(FacilityVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentConditionSetVisibilityProperty = DependencyProperty.Register("PaymentConditionSetVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PaymentConditionSetVisibility
        {
            get { return (Visibility) GetValue(PaymentConditionSetVisibilityProperty); }
            set { SetValue(PaymentConditionSetVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductionTermVisibilityProperty = DependencyProperty.Register("ProductionTermVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductionTermVisibility
        {
            get { return (Visibility) GetValue(ProductionTermVisibilityProperty); }
            set { SetValue(ProductionTermVisibilityProperty, value); }
        }


	}


    public partial class PaymentConditionSetDetailsView : ViewBase
    {
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
                PaymentConditionsVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty PaymentConditionsVisibilityProperty = DependencyProperty.Register("PaymentConditionsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PaymentConditionsVisibility
        {
            get { return (Visibility) GetValue(PaymentConditionsVisibilityProperty); }
            set { SetValue(PaymentConditionsVisibilityProperty, value); }
        }


	}


    public partial class ProductBlockDetailsView : ViewBase
    {
        public ProductBlockDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ProductBlockDetailsViewModel ProductBlockDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ProductBlockDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Parameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ParametersVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.Prices)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PricesVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.StructureCostNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StructureCostNumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductBlock).GetProperty(nameof(HVTApp.Model.POCOs.ProductBlock.IsService)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsServiceVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParametersVisibilityProperty = DependencyProperty.Register("ParametersVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParametersVisibility
        {
            get { return (Visibility) GetValue(ParametersVisibilityProperty); }
            set { SetValue(ParametersVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PricesVisibilityProperty = DependencyProperty.Register("PricesVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PricesVisibility
        {
            get { return (Visibility) GetValue(PricesVisibilityProperty); }
            set { SetValue(PricesVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StructureCostNumberVisibilityProperty = DependencyProperty.Register("StructureCostNumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StructureCostNumberVisibility
        {
            get { return (Visibility) GetValue(StructureCostNumberVisibilityProperty); }
            set { SetValue(StructureCostNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsServiceVisibilityProperty = DependencyProperty.Register("IsServiceVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsServiceVisibility
        {
            get { return (Visibility) GetValue(IsServiceVisibilityProperty); }
            set { SetValue(IsServiceVisibilityProperty, value); }
        }


	}


    public partial class ProductDependentDetailsView : ViewBase
    {
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
                MainProductIdVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductDependent).GetProperty(nameof(HVTApp.Model.POCOs.ProductDependent.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductDependent).GetProperty(nameof(HVTApp.Model.POCOs.ProductDependent.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AmountVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty MainProductIdVisibilityProperty = DependencyProperty.Register("MainProductIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility MainProductIdVisibility
        {
            get { return (Visibility) GetValue(MainProductIdVisibilityProperty); }
            set { SetValue(MainProductIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("ProductVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductVisibility
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AmountVisibilityProperty = DependencyProperty.Register("AmountVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AmountVisibility
        {
            get { return (Visibility) GetValue(AmountVisibilityProperty); }
            set { SetValue(AmountVisibilityProperty, value); }
        }


	}


    public partial class ProductionTaskDetailsView : ViewBase
    {
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
                DateTaskVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductionTask).GetProperty(nameof(HVTApp.Model.POCOs.ProductionTask.SalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SalesUnitsVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateTaskVisibilityProperty = DependencyProperty.Register("DateTaskVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateTaskVisibility
        {
            get { return (Visibility) GetValue(DateTaskVisibilityProperty); }
            set { SetValue(DateTaskVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SalesUnitsVisibilityProperty = DependencyProperty.Register("SalesUnitsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SalesUnitsVisibility
        {
            get { return (Visibility) GetValue(SalesUnitsVisibilityProperty); }
            set { SetValue(SalesUnitsVisibilityProperty, value); }
        }


	}


    public partial class SalesBlockDetailsView : ViewBase
    {
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
                ParentSalesUnitsVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesBlock).GetProperty(nameof(HVTApp.Model.POCOs.SalesBlock.ChildSalesUnits)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ChildSalesUnitsVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParentSalesUnitsVisibilityProperty = DependencyProperty.Register("ParentSalesUnitsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParentSalesUnitsVisibility
        {
            get { return (Visibility) GetValue(ParentSalesUnitsVisibilityProperty); }
            set { SetValue(ParentSalesUnitsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ChildSalesUnitsVisibilityProperty = DependencyProperty.Register("ChildSalesUnitsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ChildSalesUnitsVisibility
        {
            get { return (Visibility) GetValue(ChildSalesUnitsVisibilityProperty); }
            set { SetValue(ChildSalesUnitsVisibilityProperty, value); }
        }


	}


    public partial class BankDetailsDetailsView : ViewBase
    {
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
                BankNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.BankIdentificationCode)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                BankIdentificationCodeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.CorrespondentAccount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CorrespondentAccountVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.BankDetails).GetProperty(nameof(HVTApp.Model.POCOs.BankDetails.CheckingAccount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CheckingAccountVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty BankNameVisibilityProperty = DependencyProperty.Register("BankNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility BankNameVisibility
        {
            get { return (Visibility) GetValue(BankNameVisibilityProperty); }
            set { SetValue(BankNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty BankIdentificationCodeVisibilityProperty = DependencyProperty.Register("BankIdentificationCodeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility BankIdentificationCodeVisibility
        {
            get { return (Visibility) GetValue(BankIdentificationCodeVisibilityProperty); }
            set { SetValue(BankIdentificationCodeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CorrespondentAccountVisibilityProperty = DependencyProperty.Register("CorrespondentAccountVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CorrespondentAccountVisibility
        {
            get { return (Visibility) GetValue(CorrespondentAccountVisibilityProperty); }
            set { SetValue(CorrespondentAccountVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CheckingAccountVisibilityProperty = DependencyProperty.Register("CheckingAccountVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CheckingAccountVisibility
        {
            get { return (Visibility) GetValue(CheckingAccountVisibilityProperty); }
            set { SetValue(CheckingAccountVisibilityProperty, value); }
        }


	}


    public partial class CompanyDetailsView : ViewBase
    {
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
                FullNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ShortNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Inn)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                InnVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Kpp)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                KppVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.Form)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                FormVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ParentCompany)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ParentCompanyVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.AddressLegal)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AddressLegalVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.AddressPost)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AddressPostVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.BankDetailsList)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                BankDetailsListVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Company).GetProperty(nameof(HVTApp.Model.POCOs.Company.ActivityFilds)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ActivityFildsVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("FullNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FullNameVisibility
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("ShortNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ShortNameVisibility
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty InnVisibilityProperty = DependencyProperty.Register("InnVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility InnVisibility
        {
            get { return (Visibility) GetValue(InnVisibilityProperty); }
            set { SetValue(InnVisibilityProperty, value); }
        }



        public static readonly DependencyProperty KppVisibilityProperty = DependencyProperty.Register("KppVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility KppVisibility
        {
            get { return (Visibility) GetValue(KppVisibilityProperty); }
            set { SetValue(KppVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FormVisibilityProperty = DependencyProperty.Register("FormVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FormVisibility
        {
            get { return (Visibility) GetValue(FormVisibilityProperty); }
            set { SetValue(FormVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParentCompanyVisibilityProperty = DependencyProperty.Register("ParentCompanyVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParentCompanyVisibility
        {
            get { return (Visibility) GetValue(ParentCompanyVisibilityProperty); }
            set { SetValue(ParentCompanyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AddressLegalVisibilityProperty = DependencyProperty.Register("AddressLegalVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AddressLegalVisibility
        {
            get { return (Visibility) GetValue(AddressLegalVisibilityProperty); }
            set { SetValue(AddressLegalVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AddressPostVisibilityProperty = DependencyProperty.Register("AddressPostVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AddressPostVisibility
        {
            get { return (Visibility) GetValue(AddressPostVisibilityProperty); }
            set { SetValue(AddressPostVisibilityProperty, value); }
        }



        public static readonly DependencyProperty BankDetailsListVisibilityProperty = DependencyProperty.Register("BankDetailsListVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility BankDetailsListVisibility
        {
            get { return (Visibility) GetValue(BankDetailsListVisibilityProperty); }
            set { SetValue(BankDetailsListVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ActivityFildsVisibilityProperty = DependencyProperty.Register("ActivityFildsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ActivityFildsVisibility
        {
            get { return (Visibility) GetValue(ActivityFildsVisibilityProperty); }
            set { SetValue(ActivityFildsVisibilityProperty, value); }
        }


	}


    public partial class CompanyFormDetailsView : ViewBase
    {
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
                FullNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.CompanyForm).GetProperty(nameof(HVTApp.Model.POCOs.CompanyForm.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ShortNameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("FullNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FullNameVisibility
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("ShortNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ShortNameVisibility
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
        }


	}


    public partial class DocumentsRegistrationDetailsDetailsView : ViewBase
    {
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
                RegistrationNumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.DocumentsRegistrationDetails).GetProperty(nameof(HVTApp.Model.POCOs.DocumentsRegistrationDetails.RegistrationDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RegistrationDateVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty RegistrationNumberVisibilityProperty = DependencyProperty.Register("RegistrationNumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RegistrationNumberVisibility
        {
            get { return (Visibility) GetValue(RegistrationNumberVisibilityProperty); }
            set { SetValue(RegistrationNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDateVisibilityProperty = DependencyProperty.Register("RegistrationDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RegistrationDateVisibility
        {
            get { return (Visibility) GetValue(RegistrationDateVisibilityProperty); }
            set { SetValue(RegistrationDateVisibilityProperty, value); }
        }


	}


    public partial class EmployeesPositionDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }


	}


    public partial class FacilityTypeDetailsView : ViewBase
    {
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
                FullNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.FacilityType).GetProperty(nameof(HVTApp.Model.POCOs.FacilityType.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ShortNameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("FullNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FullNameVisibility
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("ShortNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ShortNameVisibility
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
        }


	}


    public partial class ActivityFieldDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ActivityField).GetProperty(nameof(HVTApp.Model.POCOs.ActivityField.ActivityFieldEnum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ActivityFieldEnumVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ActivityFieldEnumVisibilityProperty = DependencyProperty.Register("ActivityFieldEnumVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ActivityFieldEnumVisibility
        {
            get { return (Visibility) GetValue(ActivityFieldEnumVisibilityProperty); }
            set { SetValue(ActivityFieldEnumVisibilityProperty, value); }
        }


	}


    public partial class ContractDetailsView : ViewBase
    {
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
                NumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Contract).GetProperty(nameof(HVTApp.Model.POCOs.Contract.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Contract).GetProperty(nameof(HVTApp.Model.POCOs.Contract.Contragent)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ContragentVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumberVisibilityProperty = DependencyProperty.Register("NumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NumberVisibility
        {
            get { return (Visibility) GetValue(NumberVisibilityProperty); }
            set { SetValue(NumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ContragentVisibilityProperty = DependencyProperty.Register("ContragentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ContragentVisibility
        {
            get { return (Visibility) GetValue(ContragentVisibilityProperty); }
            set { SetValue(ContragentVisibilityProperty, value); }
        }


	}


    public partial class MeasureDetailsView : ViewBase
    {
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
                FullNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Measure).GetProperty(nameof(HVTApp.Model.POCOs.Measure.ShortName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ShortNameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FullNameVisibilityProperty = DependencyProperty.Register("FullNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FullNameVisibility
        {
            get { return (Visibility) GetValue(FullNameVisibilityProperty); }
            set { SetValue(FullNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShortNameVisibilityProperty = DependencyProperty.Register("ShortNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ShortNameVisibility
        {
            get { return (Visibility) GetValue(ShortNameVisibilityProperty); }
            set { SetValue(ShortNameVisibilityProperty, value); }
        }


	}


    public partial class ParameterDetailsView : ViewBase
    {
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
                ParameterGroupVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.Value)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ValueVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.ParameterRelations)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ParameterRelationsVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Parameter).GetProperty(nameof(HVTApp.Model.POCOs.Parameter.IsOrigin)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsOriginVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParameterGroupVisibilityProperty = DependencyProperty.Register("ParameterGroupVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParameterGroupVisibility
        {
            get { return (Visibility) GetValue(ParameterGroupVisibilityProperty); }
            set { SetValue(ParameterGroupVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ValueVisibilityProperty = DependencyProperty.Register("ValueVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ValueVisibility
        {
            get { return (Visibility) GetValue(ValueVisibilityProperty); }
            set { SetValue(ValueVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParameterRelationsVisibilityProperty = DependencyProperty.Register("ParameterRelationsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParameterRelationsVisibility
        {
            get { return (Visibility) GetValue(ParameterRelationsVisibilityProperty); }
            set { SetValue(ParameterRelationsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsOriginVisibilityProperty = DependencyProperty.Register("IsOriginVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsOriginVisibility
        {
            get { return (Visibility) GetValue(IsOriginVisibilityProperty); }
            set { SetValue(IsOriginVisibilityProperty, value); }
        }


	}


    public partial class ParameterGroupDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ParameterGroup).GetProperty(nameof(HVTApp.Model.POCOs.ParameterGroup.Measure)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                MeasureVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty MeasureVisibilityProperty = DependencyProperty.Register("MeasureVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility MeasureVisibility
        {
            get { return (Visibility) GetValue(MeasureVisibilityProperty); }
            set { SetValue(MeasureVisibilityProperty, value); }
        }


	}


    public partial class ProductRelationDetailsView : ViewBase
    {
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
                ParentProductParametersVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.ChildProductParameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ChildProductParametersVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.ChildProductsAmount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ChildProductsAmountVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ProductRelation).GetProperty(nameof(HVTApp.Model.POCOs.ProductRelation.IsUnique)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsUniqueVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParentProductParametersVisibilityProperty = DependencyProperty.Register("ParentProductParametersVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParentProductParametersVisibility
        {
            get { return (Visibility) GetValue(ParentProductParametersVisibilityProperty); }
            set { SetValue(ParentProductParametersVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ChildProductParametersVisibilityProperty = DependencyProperty.Register("ChildProductParametersVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ChildProductParametersVisibility
        {
            get { return (Visibility) GetValue(ChildProductParametersVisibilityProperty); }
            set { SetValue(ChildProductParametersVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ChildProductsAmountVisibilityProperty = DependencyProperty.Register("ChildProductsAmountVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ChildProductsAmountVisibility
        {
            get { return (Visibility) GetValue(ChildProductsAmountVisibilityProperty); }
            set { SetValue(ChildProductsAmountVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsUniqueVisibilityProperty = DependencyProperty.Register("IsUniqueVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsUniqueVisibility
        {
            get { return (Visibility) GetValue(IsUniqueVisibilityProperty); }
            set { SetValue(IsUniqueVisibilityProperty, value); }
        }


	}


    public partial class PersonDetailsView : ViewBase
    {
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
                SurnameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.Patronymic)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PatronymicVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Person).GetProperty(nameof(HVTApp.Model.POCOs.Person.IsMan)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsManVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty SurnameVisibilityProperty = DependencyProperty.Register("SurnameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SurnameVisibility
        {
            get { return (Visibility) GetValue(SurnameVisibilityProperty); }
            set { SetValue(SurnameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PatronymicVisibilityProperty = DependencyProperty.Register("PatronymicVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PatronymicVisibility
        {
            get { return (Visibility) GetValue(PatronymicVisibilityProperty); }
            set { SetValue(PatronymicVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsManVisibilityProperty = DependencyProperty.Register("IsManVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsManVisibility
        {
            get { return (Visibility) GetValue(IsManVisibilityProperty); }
            set { SetValue(IsManVisibilityProperty, value); }
        }


	}


    public partial class ParameterRelationDetailsView : ViewBase
    {
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
                ParameterIdVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.ParameterRelation).GetProperty(nameof(HVTApp.Model.POCOs.ParameterRelation.RequiredParameters)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RequiredParametersVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ParameterIdVisibilityProperty = DependencyProperty.Register("ParameterIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParameterIdVisibility
        {
            get { return (Visibility) GetValue(ParameterIdVisibilityProperty); }
            set { SetValue(ParameterIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RequiredParametersVisibilityProperty = DependencyProperty.Register("RequiredParametersVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RequiredParametersVisibility
        {
            get { return (Visibility) GetValue(RequiredParametersVisibilityProperty); }
            set { SetValue(RequiredParametersVisibilityProperty, value); }
        }


	}


    public partial class SalesUnitDetailsView : ViewBase
    {
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
                CostVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Product)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ProductsIncluded)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductsIncludedVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Services)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ServicesVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Facility)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                FacilityVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentConditionSet)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PaymentConditionSetVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ProductionTerm)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductionTermVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Project)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProjectVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDateExpected)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DeliveryDateExpectedVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Producer)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProducerVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.RealizationDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RealizationDateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Order)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                OrderVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.OrderPosition)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                OrderPositionVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.SerialNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SerialNumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.AssembleTerm)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AssembleTermVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.StartProductionDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StartProductionDateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PickingDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PickingDateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.EndProductionDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                EndProductionDateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Specification)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SpecificationVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsActual)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PaymentsActualVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.PaymentsPlanned)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PaymentsPlannedVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ExpectedDeliveryPeriod)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ExpectedDeliveryPeriodVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.Address)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AddressVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.CostOfShipment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CostOfShipmentVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ShipmentDateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.ShipmentPlanDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ShipmentPlanDateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SalesUnit).GetProperty(nameof(HVTApp.Model.POCOs.SalesUnit.DeliveryDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DeliveryDateVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty CostVisibilityProperty = DependencyProperty.Register("CostVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CostVisibility
        {
            get { return (Visibility) GetValue(CostVisibilityProperty); }
            set { SetValue(CostVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductVisibilityProperty = DependencyProperty.Register("ProductVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductVisibility
        {
            get { return (Visibility) GetValue(ProductVisibilityProperty); }
            set { SetValue(ProductVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductsIncludedVisibilityProperty = DependencyProperty.Register("ProductsIncludedVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductsIncludedVisibility
        {
            get { return (Visibility) GetValue(ProductsIncludedVisibilityProperty); }
            set { SetValue(ProductsIncludedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ServicesVisibilityProperty = DependencyProperty.Register("ServicesVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ServicesVisibility
        {
            get { return (Visibility) GetValue(ServicesVisibilityProperty); }
            set { SetValue(ServicesVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FacilityVisibilityProperty = DependencyProperty.Register("FacilityVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FacilityVisibility
        {
            get { return (Visibility) GetValue(FacilityVisibilityProperty); }
            set { SetValue(FacilityVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentConditionSetVisibilityProperty = DependencyProperty.Register("PaymentConditionSetVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PaymentConditionSetVisibility
        {
            get { return (Visibility) GetValue(PaymentConditionSetVisibilityProperty); }
            set { SetValue(PaymentConditionSetVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductionTermVisibilityProperty = DependencyProperty.Register("ProductionTermVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductionTermVisibility
        {
            get { return (Visibility) GetValue(ProductionTermVisibilityProperty); }
            set { SetValue(ProductionTermVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProjectVisibilityProperty = DependencyProperty.Register("ProjectVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProjectVisibility
        {
            get { return (Visibility) GetValue(ProjectVisibilityProperty); }
            set { SetValue(ProjectVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DeliveryDateExpectedVisibilityProperty = DependencyProperty.Register("DeliveryDateExpectedVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DeliveryDateExpectedVisibility
        {
            get { return (Visibility) GetValue(DeliveryDateExpectedVisibilityProperty); }
            set { SetValue(DeliveryDateExpectedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProducerVisibilityProperty = DependencyProperty.Register("ProducerVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProducerVisibility
        {
            get { return (Visibility) GetValue(ProducerVisibilityProperty); }
            set { SetValue(ProducerVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RealizationDateVisibilityProperty = DependencyProperty.Register("RealizationDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RealizationDateVisibility
        {
            get { return (Visibility) GetValue(RealizationDateVisibilityProperty); }
            set { SetValue(RealizationDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OrderVisibilityProperty = DependencyProperty.Register("OrderVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility OrderVisibility
        {
            get { return (Visibility) GetValue(OrderVisibilityProperty); }
            set { SetValue(OrderVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OrderPositionVisibilityProperty = DependencyProperty.Register("OrderPositionVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility OrderPositionVisibility
        {
            get { return (Visibility) GetValue(OrderPositionVisibilityProperty); }
            set { SetValue(OrderPositionVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SerialNumberVisibilityProperty = DependencyProperty.Register("SerialNumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SerialNumberVisibility
        {
            get { return (Visibility) GetValue(SerialNumberVisibilityProperty); }
            set { SetValue(SerialNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AssembleTermVisibilityProperty = DependencyProperty.Register("AssembleTermVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AssembleTermVisibility
        {
            get { return (Visibility) GetValue(AssembleTermVisibilityProperty); }
            set { SetValue(AssembleTermVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StartProductionDateVisibilityProperty = DependencyProperty.Register("StartProductionDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StartProductionDateVisibility
        {
            get { return (Visibility) GetValue(StartProductionDateVisibilityProperty); }
            set { SetValue(StartProductionDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PickingDateVisibilityProperty = DependencyProperty.Register("PickingDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PickingDateVisibility
        {
            get { return (Visibility) GetValue(PickingDateVisibilityProperty); }
            set { SetValue(PickingDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EndProductionDateVisibilityProperty = DependencyProperty.Register("EndProductionDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility EndProductionDateVisibility
        {
            get { return (Visibility) GetValue(EndProductionDateVisibilityProperty); }
            set { SetValue(EndProductionDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SpecificationVisibilityProperty = DependencyProperty.Register("SpecificationVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SpecificationVisibility
        {
            get { return (Visibility) GetValue(SpecificationVisibilityProperty); }
            set { SetValue(SpecificationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentsActualVisibilityProperty = DependencyProperty.Register("PaymentsActualVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PaymentsActualVisibility
        {
            get { return (Visibility) GetValue(PaymentsActualVisibilityProperty); }
            set { SetValue(PaymentsActualVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentsPlannedVisibilityProperty = DependencyProperty.Register("PaymentsPlannedVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PaymentsPlannedVisibility
        {
            get { return (Visibility) GetValue(PaymentsPlannedVisibilityProperty); }
            set { SetValue(PaymentsPlannedVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ExpectedDeliveryPeriodVisibilityProperty = DependencyProperty.Register("ExpectedDeliveryPeriodVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ExpectedDeliveryPeriodVisibility
        {
            get { return (Visibility) GetValue(ExpectedDeliveryPeriodVisibilityProperty); }
            set { SetValue(ExpectedDeliveryPeriodVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AddressVisibilityProperty = DependencyProperty.Register("AddressVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AddressVisibility
        {
            get { return (Visibility) GetValue(AddressVisibilityProperty); }
            set { SetValue(AddressVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CostOfShipmentVisibilityProperty = DependencyProperty.Register("CostOfShipmentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CostOfShipmentVisibility
        {
            get { return (Visibility) GetValue(CostOfShipmentVisibilityProperty); }
            set { SetValue(CostOfShipmentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShipmentDateVisibilityProperty = DependencyProperty.Register("ShipmentDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ShipmentDateVisibility
        {
            get { return (Visibility) GetValue(ShipmentDateVisibilityProperty); }
            set { SetValue(ShipmentDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ShipmentPlanDateVisibilityProperty = DependencyProperty.Register("ShipmentPlanDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ShipmentPlanDateVisibility
        {
            get { return (Visibility) GetValue(ShipmentPlanDateVisibilityProperty); }
            set { SetValue(ShipmentPlanDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DeliveryDateVisibilityProperty = DependencyProperty.Register("DeliveryDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DeliveryDateVisibility
        {
            get { return (Visibility) GetValue(DeliveryDateVisibilityProperty); }
            set { SetValue(DeliveryDateVisibilityProperty, value); }
        }


	}


    public partial class ServiceDetailsView : ViewBase
    {
        public ServiceDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator, ServiceDetailsViewModel ServiceDetailsViewModel) : base(regionManager, eventAggregator)
        {
            SetVisibilityProps();
			InitializeComponent();
            DataContext = ServiceDetailsViewModel;
        }

        private void SetVisibilityProps()
        {
            NotUpdateAttribute attr;


            attr = typeof(HVTApp.Model.POCOs.Service).GetProperty(nameof(HVTApp.Model.POCOs.Service.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Service).GetProperty(nameof(HVTApp.Model.POCOs.Service.Amount)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AmountVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AmountVisibilityProperty = DependencyProperty.Register("AmountVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AmountVisibility
        {
            get { return (Visibility) GetValue(AmountVisibilityProperty); }
            set { SetValue(AmountVisibilityProperty, value); }
        }


	}


    public partial class TestFriendAddressDetailsView : ViewBase
    {
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
                CityVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriendAddress).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendAddress.Street)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StreetVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriendAddress).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendAddress.StreetNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                StreetNumberVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty CityVisibilityProperty = DependencyProperty.Register("CityVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CityVisibility
        {
            get { return (Visibility) GetValue(CityVisibilityProperty); }
            set { SetValue(CityVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StreetVisibilityProperty = DependencyProperty.Register("StreetVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StreetVisibility
        {
            get { return (Visibility) GetValue(StreetVisibilityProperty); }
            set { SetValue(StreetVisibilityProperty, value); }
        }



        public static readonly DependencyProperty StreetNumberVisibilityProperty = DependencyProperty.Register("StreetNumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility StreetNumberVisibility
        {
            get { return (Visibility) GetValue(StreetNumberVisibilityProperty); }
            set { SetValue(StreetNumberVisibilityProperty, value); }
        }


	}


    public partial class TestFriendDetailsView : ViewBase
    {
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
                FriendGroupIdVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.FirstName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                FirstNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.LastName)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                LastNameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.Birthday)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                BirthdayVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.IsDeveloper)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IsDeveloperVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendAddress)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                TestFriendAddressVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendGroup)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                TestFriendGroupVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.Emails)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                EmailsVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.IdGet)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                IdGetVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriend).GetProperty(nameof(HVTApp.Model.POCOs.TestFriend.TestFriendEmailGet)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                TestFriendEmailGetVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty FriendGroupIdVisibilityProperty = DependencyProperty.Register("FriendGroupIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FriendGroupIdVisibility
        {
            get { return (Visibility) GetValue(FriendGroupIdVisibilityProperty); }
            set { SetValue(FriendGroupIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FirstNameVisibilityProperty = DependencyProperty.Register("FirstNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FirstNameVisibility
        {
            get { return (Visibility) GetValue(FirstNameVisibilityProperty); }
            set { SetValue(FirstNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty LastNameVisibilityProperty = DependencyProperty.Register("LastNameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility LastNameVisibility
        {
            get { return (Visibility) GetValue(LastNameVisibilityProperty); }
            set { SetValue(LastNameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty BirthdayVisibilityProperty = DependencyProperty.Register("BirthdayVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility BirthdayVisibility
        {
            get { return (Visibility) GetValue(BirthdayVisibilityProperty); }
            set { SetValue(BirthdayVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IsDeveloperVisibilityProperty = DependencyProperty.Register("IsDeveloperVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IsDeveloperVisibility
        {
            get { return (Visibility) GetValue(IsDeveloperVisibilityProperty); }
            set { SetValue(IsDeveloperVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TestFriendAddressVisibilityProperty = DependencyProperty.Register("TestFriendAddressVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility TestFriendAddressVisibility
        {
            get { return (Visibility) GetValue(TestFriendAddressVisibilityProperty); }
            set { SetValue(TestFriendAddressVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TestFriendGroupVisibilityProperty = DependencyProperty.Register("TestFriendGroupVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility TestFriendGroupVisibility
        {
            get { return (Visibility) GetValue(TestFriendGroupVisibilityProperty); }
            set { SetValue(TestFriendGroupVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EmailsVisibilityProperty = DependencyProperty.Register("EmailsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility EmailsVisibility
        {
            get { return (Visibility) GetValue(EmailsVisibilityProperty); }
            set { SetValue(EmailsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty IdGetVisibilityProperty = DependencyProperty.Register("IdGetVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility IdGetVisibility
        {
            get { return (Visibility) GetValue(IdGetVisibilityProperty); }
            set { SetValue(IdGetVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TestFriendEmailGetVisibilityProperty = DependencyProperty.Register("TestFriendEmailGetVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility TestFriendEmailGetVisibility
        {
            get { return (Visibility) GetValue(TestFriendEmailGetVisibilityProperty); }
            set { SetValue(TestFriendEmailGetVisibilityProperty, value); }
        }


	}


    public partial class TestFriendEmailDetailsView : ViewBase
    {
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
                EmailVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriendEmail).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendEmail.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CommentVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty EmailVisibilityProperty = DependencyProperty.Register("EmailVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility EmailVisibility
        {
            get { return (Visibility) GetValue(EmailVisibilityProperty); }
            set { SetValue(EmailVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("CommentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CommentVisibility
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
        }


	}


    public partial class TestFriendGroupDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestFriendGroup).GetProperty(nameof(HVTApp.Model.POCOs.TestFriendGroup.FriendTests)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                FriendTestsVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty FriendTestsVisibilityProperty = DependencyProperty.Register("FriendTestsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility FriendTestsVisibility
        {
            get { return (Visibility) GetValue(FriendTestsVisibilityProperty); }
            set { SetValue(FriendTestsVisibilityProperty, value); }
        }


	}


    public partial class DocumentDetailsView : ViewBase
    {
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
                RequestDocumentVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Author)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AuthorVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.SenderId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SenderIdVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.SenderEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SenderEmployeeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RecipientId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RecipientIdVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RecipientEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RecipientEmployeeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.CopyToRecipients)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CopyToRecipientsVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfSender)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RegistrationDetailsOfSenderVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.RegistrationDetailsOfRecipient)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RegistrationDetailsOfRecipientVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Document).GetProperty(nameof(HVTApp.Model.POCOs.Document.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CommentVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty RequestDocumentVisibilityProperty = DependencyProperty.Register("RequestDocumentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RequestDocumentVisibility
        {
            get { return (Visibility) GetValue(RequestDocumentVisibilityProperty); }
            set { SetValue(RequestDocumentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AuthorVisibilityProperty = DependencyProperty.Register("AuthorVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AuthorVisibility
        {
            get { return (Visibility) GetValue(AuthorVisibilityProperty); }
            set { SetValue(AuthorVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SenderIdVisibilityProperty = DependencyProperty.Register("SenderIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SenderIdVisibility
        {
            get { return (Visibility) GetValue(SenderIdVisibilityProperty); }
            set { SetValue(SenderIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SenderEmployeeVisibilityProperty = DependencyProperty.Register("SenderEmployeeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SenderEmployeeVisibility
        {
            get { return (Visibility) GetValue(SenderEmployeeVisibilityProperty); }
            set { SetValue(SenderEmployeeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RecipientIdVisibilityProperty = DependencyProperty.Register("RecipientIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RecipientIdVisibility
        {
            get { return (Visibility) GetValue(RecipientIdVisibilityProperty); }
            set { SetValue(RecipientIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RecipientEmployeeVisibilityProperty = DependencyProperty.Register("RecipientEmployeeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RecipientEmployeeVisibility
        {
            get { return (Visibility) GetValue(RecipientEmployeeVisibilityProperty); }
            set { SetValue(RecipientEmployeeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CopyToRecipientsVisibilityProperty = DependencyProperty.Register("CopyToRecipientsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CopyToRecipientsVisibility
        {
            get { return (Visibility) GetValue(CopyToRecipientsVisibilityProperty); }
            set { SetValue(CopyToRecipientsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDetailsOfSenderVisibilityProperty = DependencyProperty.Register("RegistrationDetailsOfSenderVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RegistrationDetailsOfSenderVisibility
        {
            get { return (Visibility) GetValue(RegistrationDetailsOfSenderVisibilityProperty); }
            set { SetValue(RegistrationDetailsOfSenderVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDetailsOfRecipientVisibilityProperty = DependencyProperty.Register("RegistrationDetailsOfRecipientVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RegistrationDetailsOfRecipientVisibility
        {
            get { return (Visibility) GetValue(RegistrationDetailsOfRecipientVisibilityProperty); }
            set { SetValue(RegistrationDetailsOfRecipientVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("CommentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CommentVisibility
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
        }


	}


    public partial class TestEntityDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }


	}


    public partial class TestHusbandDetailsView : ViewBase
    {
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
                WifeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestHusband).GetProperty(nameof(HVTApp.Model.POCOs.TestHusband.Children)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ChildrenVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestHusband).GetProperty(nameof(HVTApp.Model.POCOs.TestHusband.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                NameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty WifeVisibilityProperty = DependencyProperty.Register("WifeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility WifeVisibility
        {
            get { return (Visibility) GetValue(WifeVisibilityProperty); }
            set { SetValue(WifeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ChildrenVisibilityProperty = DependencyProperty.Register("ChildrenVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ChildrenVisibility
        {
            get { return (Visibility) GetValue(ChildrenVisibilityProperty); }
            set { SetValue(ChildrenVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }


	}


    public partial class TestWifeDetailsView : ViewBase
    {
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
                NVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestWife).GetProperty(nameof(HVTApp.Model.POCOs.TestWife.Husband)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                HusbandVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestWife).GetProperty(nameof(HVTApp.Model.POCOs.TestWife.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                NameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NVisibilityProperty = DependencyProperty.Register("NVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NVisibility
        {
            get { return (Visibility) GetValue(NVisibilityProperty); }
            set { SetValue(NVisibilityProperty, value); }
        }



        public static readonly DependencyProperty HusbandVisibilityProperty = DependencyProperty.Register("HusbandVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility HusbandVisibility
        {
            get { return (Visibility) GetValue(HusbandVisibilityProperty); }
            set { SetValue(HusbandVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }


	}


    public partial class TestChildDetailsView : ViewBase
    {
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
                HusbandVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestChild).GetProperty(nameof(HVTApp.Model.POCOs.TestChild.Wife)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                WifeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TestChild).GetProperty(nameof(HVTApp.Model.POCOs.TestChild.Name)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                NameVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty HusbandVisibilityProperty = DependencyProperty.Register("HusbandVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility HusbandVisibility
        {
            get { return (Visibility) GetValue(HusbandVisibilityProperty); }
            set { SetValue(HusbandVisibilityProperty, value); }
        }



        public static readonly DependencyProperty WifeVisibilityProperty = DependencyProperty.Register("WifeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility WifeVisibility
        {
            get { return (Visibility) GetValue(WifeVisibilityProperty); }
            set { SetValue(WifeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }


	}


    public partial class SumOnDateDetailsView : ViewBase
    {
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
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.SumOnDate).GetProperty(nameof(HVTApp.Model.POCOs.SumOnDate.Sum)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SumVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SumVisibilityProperty = DependencyProperty.Register("SumVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SumVisibility
        {
            get { return (Visibility) GetValue(SumVisibilityProperty); }
            set { SetValue(SumVisibilityProperty, value); }
        }


	}


    public partial class ProductDetailsView : ViewBase
    {
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
                DesignationVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.DesignationSpecial)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DesignationSpecialVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.ProductType)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductTypeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.ProductBlock)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProductBlockVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Product).GetProperty(nameof(HVTApp.Model.POCOs.Product.DependentProducts)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DependentProductsVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty DesignationVisibilityProperty = DependencyProperty.Register("DesignationVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DesignationVisibility
        {
            get { return (Visibility) GetValue(DesignationVisibilityProperty); }
            set { SetValue(DesignationVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DesignationSpecialVisibilityProperty = DependencyProperty.Register("DesignationSpecialVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DesignationSpecialVisibility
        {
            get { return (Visibility) GetValue(DesignationSpecialVisibilityProperty); }
            set { SetValue(DesignationSpecialVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductTypeVisibilityProperty = DependencyProperty.Register("ProductTypeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductTypeVisibility
        {
            get { return (Visibility) GetValue(ProductTypeVisibilityProperty); }
            set { SetValue(ProductTypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProductBlockVisibilityProperty = DependencyProperty.Register("ProductBlockVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProductBlockVisibility
        {
            get { return (Visibility) GetValue(ProductBlockVisibilityProperty); }
            set { SetValue(ProductBlockVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DependentProductsVisibilityProperty = DependencyProperty.Register("DependentProductsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DependentProductsVisibility
        {
            get { return (Visibility) GetValue(DependentProductsVisibilityProperty); }
            set { SetValue(DependentProductsVisibilityProperty, value); }
        }


	}


    public partial class OfferDetailsView : ViewBase
    {
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
                ProjectVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.ValidityDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ValidityDateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VatVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RequestDocument)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RequestDocumentVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Author)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AuthorVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.SenderId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SenderIdVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.SenderEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                SenderEmployeeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RecipientId)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RecipientIdVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RecipientEmployee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RecipientEmployeeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.CopyToRecipients)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CopyToRecipientsVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfSender)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RegistrationDetailsOfSenderVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.RegistrationDetailsOfRecipient)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RegistrationDetailsOfRecipientVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Offer).GetProperty(nameof(HVTApp.Model.POCOs.Offer.Comment)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CommentVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProjectVisibilityProperty = DependencyProperty.Register("ProjectVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProjectVisibility
        {
            get { return (Visibility) GetValue(ProjectVisibilityProperty); }
            set { SetValue(ProjectVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ValidityDateVisibilityProperty = DependencyProperty.Register("ValidityDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ValidityDateVisibility
        {
            get { return (Visibility) GetValue(ValidityDateVisibilityProperty); }
            set { SetValue(ValidityDateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty VatVisibilityProperty = DependencyProperty.Register("VatVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VatVisibility
        {
            get { return (Visibility) GetValue(VatVisibilityProperty); }
            set { SetValue(VatVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RequestDocumentVisibilityProperty = DependencyProperty.Register("RequestDocumentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RequestDocumentVisibility
        {
            get { return (Visibility) GetValue(RequestDocumentVisibilityProperty); }
            set { SetValue(RequestDocumentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AuthorVisibilityProperty = DependencyProperty.Register("AuthorVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AuthorVisibility
        {
            get { return (Visibility) GetValue(AuthorVisibilityProperty); }
            set { SetValue(AuthorVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SenderIdVisibilityProperty = DependencyProperty.Register("SenderIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SenderIdVisibility
        {
            get { return (Visibility) GetValue(SenderIdVisibilityProperty); }
            set { SetValue(SenderIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty SenderEmployeeVisibilityProperty = DependencyProperty.Register("SenderEmployeeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility SenderEmployeeVisibility
        {
            get { return (Visibility) GetValue(SenderEmployeeVisibilityProperty); }
            set { SetValue(SenderEmployeeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RecipientIdVisibilityProperty = DependencyProperty.Register("RecipientIdVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RecipientIdVisibility
        {
            get { return (Visibility) GetValue(RecipientIdVisibilityProperty); }
            set { SetValue(RecipientIdVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RecipientEmployeeVisibilityProperty = DependencyProperty.Register("RecipientEmployeeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RecipientEmployeeVisibility
        {
            get { return (Visibility) GetValue(RecipientEmployeeVisibilityProperty); }
            set { SetValue(RecipientEmployeeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CopyToRecipientsVisibilityProperty = DependencyProperty.Register("CopyToRecipientsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CopyToRecipientsVisibility
        {
            get { return (Visibility) GetValue(CopyToRecipientsVisibilityProperty); }
            set { SetValue(CopyToRecipientsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDetailsOfSenderVisibilityProperty = DependencyProperty.Register("RegistrationDetailsOfSenderVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RegistrationDetailsOfSenderVisibility
        {
            get { return (Visibility) GetValue(RegistrationDetailsOfSenderVisibilityProperty); }
            set { SetValue(RegistrationDetailsOfSenderVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RegistrationDetailsOfRecipientVisibilityProperty = DependencyProperty.Register("RegistrationDetailsOfRecipientVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RegistrationDetailsOfRecipientVisibility
        {
            get { return (Visibility) GetValue(RegistrationDetailsOfRecipientVisibilityProperty); }
            set { SetValue(RegistrationDetailsOfRecipientVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CommentVisibilityProperty = DependencyProperty.Register("CommentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CommentVisibility
        {
            get { return (Visibility) GetValue(CommentVisibilityProperty); }
            set { SetValue(CommentVisibilityProperty, value); }
        }


	}


    public partial class EmployeeDetailsView : ViewBase
    {
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
                PersonVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.PhoneNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PhoneNumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Email)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                EmailVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Company)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                CompanyVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Employee).GetProperty(nameof(HVTApp.Model.POCOs.Employee.Position)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PositionVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty PersonVisibilityProperty = DependencyProperty.Register("PersonVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PersonVisibility
        {
            get { return (Visibility) GetValue(PersonVisibilityProperty); }
            set { SetValue(PersonVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PhoneNumberVisibilityProperty = DependencyProperty.Register("PhoneNumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PhoneNumberVisibility
        {
            get { return (Visibility) GetValue(PhoneNumberVisibilityProperty); }
            set { SetValue(PhoneNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EmailVisibilityProperty = DependencyProperty.Register("EmailVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility EmailVisibility
        {
            get { return (Visibility) GetValue(EmailVisibilityProperty); }
            set { SetValue(EmailVisibilityProperty, value); }
        }



        public static readonly DependencyProperty CompanyVisibilityProperty = DependencyProperty.Register("CompanyVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility CompanyVisibility
        {
            get { return (Visibility) GetValue(CompanyVisibilityProperty); }
            set { SetValue(CompanyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PositionVisibilityProperty = DependencyProperty.Register("PositionVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PositionVisibility
        {
            get { return (Visibility) GetValue(PositionVisibilityProperty); }
            set { SetValue(PositionVisibilityProperty, value); }
        }


	}


    public partial class OrderDetailsView : ViewBase
    {
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
                NumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Order).GetProperty(nameof(HVTApp.Model.POCOs.Order.OpenOrderDate)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                OpenOrderDateVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumberVisibilityProperty = DependencyProperty.Register("NumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NumberVisibility
        {
            get { return (Visibility) GetValue(NumberVisibilityProperty); }
            set { SetValue(NumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OpenOrderDateVisibilityProperty = DependencyProperty.Register("OpenOrderDateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility OpenOrderDateVisibility
        {
            get { return (Visibility) GetValue(OpenOrderDateVisibilityProperty); }
            set { SetValue(OpenOrderDateVisibilityProperty, value); }
        }


	}


    public partial class PaymentConditionDetailsView : ViewBase
    {
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
                PartVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentCondition).GetProperty(nameof(HVTApp.Model.POCOs.PaymentCondition.DaysToPoint)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DaysToPointVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentCondition).GetProperty(nameof(HVTApp.Model.POCOs.PaymentCondition.PaymentConditionPoint)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PaymentConditionPointVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty PartVisibilityProperty = DependencyProperty.Register("PartVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PartVisibility
        {
            get { return (Visibility) GetValue(PartVisibilityProperty); }
            set { SetValue(PartVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DaysToPointVisibilityProperty = DependencyProperty.Register("DaysToPointVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DaysToPointVisibility
        {
            get { return (Visibility) GetValue(DaysToPointVisibilityProperty); }
            set { SetValue(DaysToPointVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentConditionPointVisibilityProperty = DependencyProperty.Register("PaymentConditionPointVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PaymentConditionPointVisibility
        {
            get { return (Visibility) GetValue(PaymentConditionPointVisibilityProperty); }
            set { SetValue(PaymentConditionPointVisibilityProperty, value); }
        }


	}


    public partial class PaymentDocumentDetailsView : ViewBase
    {
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
                NumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.PaymentDocument).GetProperty(nameof(HVTApp.Model.POCOs.PaymentDocument.Payments)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PaymentsVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumberVisibilityProperty = DependencyProperty.Register("NumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NumberVisibility
        {
            get { return (Visibility) GetValue(NumberVisibilityProperty); }
            set { SetValue(NumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PaymentsVisibilityProperty = DependencyProperty.Register("PaymentsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PaymentsVisibility
        {
            get { return (Visibility) GetValue(PaymentsVisibilityProperty); }
            set { SetValue(PaymentsVisibilityProperty, value); }
        }


	}


    public partial class FacilityDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.Type)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                TypeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.OwnerCompany)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                OwnerCompanyVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Facility).GetProperty(nameof(HVTApp.Model.POCOs.Facility.Address)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                AddressVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TypeVisibilityProperty = DependencyProperty.Register("TypeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility TypeVisibility
        {
            get { return (Visibility) GetValue(TypeVisibilityProperty); }
            set { SetValue(TypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty OwnerCompanyVisibilityProperty = DependencyProperty.Register("OwnerCompanyVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility OwnerCompanyVisibility
        {
            get { return (Visibility) GetValue(OwnerCompanyVisibilityProperty); }
            set { SetValue(OwnerCompanyVisibilityProperty, value); }
        }



        public static readonly DependencyProperty AddressVisibilityProperty = DependencyProperty.Register("AddressVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility AddressVisibility
        {
            get { return (Visibility) GetValue(AddressVisibilityProperty); }
            set { SetValue(AddressVisibilityProperty, value); }
        }


	}


    public partial class ProjectDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.ProjectType)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ProjectTypeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.Manager)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ManagerVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Project).GetProperty(nameof(HVTApp.Model.POCOs.Project.Notes)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                NotesVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ProjectTypeVisibilityProperty = DependencyProperty.Register("ProjectTypeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProjectTypeVisibility
        {
            get { return (Visibility) GetValue(ProjectTypeVisibilityProperty); }
            set { SetValue(ProjectTypeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ManagerVisibilityProperty = DependencyProperty.Register("ManagerVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ManagerVisibility
        {
            get { return (Visibility) GetValue(ManagerVisibilityProperty); }
            set { SetValue(ManagerVisibilityProperty, value); }
        }



        public static readonly DependencyProperty NotesVisibilityProperty = DependencyProperty.Register("NotesVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NotesVisibility
        {
            get { return (Visibility) GetValue(NotesVisibilityProperty); }
            set { SetValue(NotesVisibilityProperty, value); }
        }


	}


    public partial class UserRoleDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.UserRole).GetProperty(nameof(HVTApp.Model.POCOs.UserRole.Role)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RoleVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RoleVisibilityProperty = DependencyProperty.Register("RoleVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RoleVisibility
        {
            get { return (Visibility) GetValue(RoleVisibilityProperty); }
            set { SetValue(RoleVisibilityProperty, value); }
        }


	}


    public partial class SpecificationDetailsView : ViewBase
    {
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
                NumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Date)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DateVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Vat)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                VatVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Specification).GetProperty(nameof(HVTApp.Model.POCOs.Specification.Contract)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ContractVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NumberVisibilityProperty = DependencyProperty.Register("NumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NumberVisibility
        {
            get { return (Visibility) GetValue(NumberVisibilityProperty); }
            set { SetValue(NumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateVisibilityProperty = DependencyProperty.Register("DateVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateVisibility
        {
            get { return (Visibility) GetValue(DateVisibilityProperty); }
            set { SetValue(DateVisibilityProperty, value); }
        }



        public static readonly DependencyProperty VatVisibilityProperty = DependencyProperty.Register("VatVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility VatVisibility
        {
            get { return (Visibility) GetValue(VatVisibilityProperty); }
            set { SetValue(VatVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ContractVisibilityProperty = DependencyProperty.Register("ContractVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ContractVisibility
        {
            get { return (Visibility) GetValue(ContractVisibilityProperty); }
            set { SetValue(ContractVisibilityProperty, value); }
        }


	}


    public partial class TenderDetailsView : ViewBase
    {
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
                ProjectVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Types)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                TypesVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateOpen)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DateOpenVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateClose)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DateCloseVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.DateNotice)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                DateNoticeVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Participants)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                ParticipantsVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.Tender).GetProperty(nameof(HVTApp.Model.POCOs.Tender.Winner)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                WinnerVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty ProjectVisibilityProperty = DependencyProperty.Register("ProjectVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ProjectVisibility
        {
            get { return (Visibility) GetValue(ProjectVisibilityProperty); }
            set { SetValue(ProjectVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TypesVisibilityProperty = DependencyProperty.Register("TypesVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility TypesVisibility
        {
            get { return (Visibility) GetValue(TypesVisibilityProperty); }
            set { SetValue(TypesVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateOpenVisibilityProperty = DependencyProperty.Register("DateOpenVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateOpenVisibility
        {
            get { return (Visibility) GetValue(DateOpenVisibilityProperty); }
            set { SetValue(DateOpenVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateCloseVisibilityProperty = DependencyProperty.Register("DateCloseVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateCloseVisibility
        {
            get { return (Visibility) GetValue(DateCloseVisibilityProperty); }
            set { SetValue(DateCloseVisibilityProperty, value); }
        }



        public static readonly DependencyProperty DateNoticeVisibilityProperty = DependencyProperty.Register("DateNoticeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility DateNoticeVisibility
        {
            get { return (Visibility) GetValue(DateNoticeVisibilityProperty); }
            set { SetValue(DateNoticeVisibilityProperty, value); }
        }



        public static readonly DependencyProperty ParticipantsVisibilityProperty = DependencyProperty.Register("ParticipantsVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility ParticipantsVisibility
        {
            get { return (Visibility) GetValue(ParticipantsVisibilityProperty); }
            set { SetValue(ParticipantsVisibilityProperty, value); }
        }



        public static readonly DependencyProperty WinnerVisibilityProperty = DependencyProperty.Register("WinnerVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility WinnerVisibility
        {
            get { return (Visibility) GetValue(WinnerVisibilityProperty); }
            set { SetValue(WinnerVisibilityProperty, value); }
        }


	}


    public partial class TenderTypeDetailsView : ViewBase
    {
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
                NameVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.TenderType).GetProperty(nameof(HVTApp.Model.POCOs.TenderType.Type)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                TypeVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty NameVisibilityProperty = DependencyProperty.Register("NameVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility NameVisibility
        {
            get { return (Visibility) GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }



        public static readonly DependencyProperty TypeVisibilityProperty = DependencyProperty.Register("TypeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility TypeVisibility
        {
            get { return (Visibility) GetValue(TypeVisibilityProperty); }
            set { SetValue(TypeVisibilityProperty, value); }
        }


	}


    public partial class UserDetailsView : ViewBase
    {
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
                LoginVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Password)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PasswordVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.PersonalNumber)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                PersonalNumberVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.RoleCurrent)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RoleCurrentVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Roles)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                RolesVisibility = Visibility.Collapsed;


            attr = typeof(HVTApp.Model.POCOs.User).GetProperty(nameof(HVTApp.Model.POCOs.User.Employee)).GetCustomAttribute<NotUpdateAttribute>();
            if (attr != null && attr.RolesCantUpdate.Contains(CommonOptions.User.RoleCurrent))
                EmployeeVisibility = Visibility.Collapsed;



        }



        public static readonly DependencyProperty LoginVisibilityProperty = DependencyProperty.Register("LoginVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility LoginVisibility
        {
            get { return (Visibility) GetValue(LoginVisibilityProperty); }
            set { SetValue(LoginVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PasswordVisibilityProperty = DependencyProperty.Register("PasswordVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PasswordVisibility
        {
            get { return (Visibility) GetValue(PasswordVisibilityProperty); }
            set { SetValue(PasswordVisibilityProperty, value); }
        }



        public static readonly DependencyProperty PersonalNumberVisibilityProperty = DependencyProperty.Register("PersonalNumberVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility PersonalNumberVisibility
        {
            get { return (Visibility) GetValue(PersonalNumberVisibilityProperty); }
            set { SetValue(PersonalNumberVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RoleCurrentVisibilityProperty = DependencyProperty.Register("RoleCurrentVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RoleCurrentVisibility
        {
            get { return (Visibility) GetValue(RoleCurrentVisibilityProperty); }
            set { SetValue(RoleCurrentVisibilityProperty, value); }
        }



        public static readonly DependencyProperty RolesVisibilityProperty = DependencyProperty.Register("RolesVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility RolesVisibility
        {
            get { return (Visibility) GetValue(RolesVisibilityProperty); }
            set { SetValue(RolesVisibilityProperty, value); }
        }



        public static readonly DependencyProperty EmployeeVisibilityProperty = DependencyProperty.Register("EmployeeVisibility", typeof(Visibility), typeof(ProjectDetailsView), new PropertyMetadata((System.Windows.Visibility.Visible)));
        public Visibility EmployeeVisibility
        {
            get { return (Visibility) GetValue(EmployeeVisibilityProperty); }
            set { SetValue(EmployeeVisibilityProperty, value); }
        }


	}


}
