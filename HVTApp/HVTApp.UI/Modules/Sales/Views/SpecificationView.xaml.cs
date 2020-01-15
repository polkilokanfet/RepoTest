using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCrudUnits))]
    public partial class SpecificationView
    {
        private readonly SpecificationViewModel _viewModel;
        public SpecificationView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<SpecificationViewModel>();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            var project = navigationContext.Parameters.First().Value as Project;
            var specification = navigationContext.Parameters.First().Value as Specification;

            if (project != null) _viewModel.Load(new Specification(), true, project);
            if (specification != null) _viewModel.Load(specification, false);
        }
    }
}

