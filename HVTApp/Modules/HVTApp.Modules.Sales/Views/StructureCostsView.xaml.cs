using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabStructureCosts))]
    public partial class StructureCostsView : ViewBase
    {
        private readonly StructureCostsViewModel _viewModel;

        public StructureCostsView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<StructureCostsViewModel>();
            this.DataContext = _viewModel;
        }

        //private bool _isNavigationTarget = false;

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //var project = navigationContext.Parameters.First().Value as Project;
            //_isNavigationTarget = _viewModel.SalesUnitWrappers.First().Project.Id == project.Id;
            //return _isNavigationTarget;
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            //if (_isNavigationTarget) return;

            var project = navigationContext.Parameters.First().Value as Project;
            _viewModel.Load(project);
        }
    }
}

