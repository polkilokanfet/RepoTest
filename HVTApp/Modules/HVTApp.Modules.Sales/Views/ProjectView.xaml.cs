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
    [RibbonTab(typeof(TabCrudUnits))]
    public partial class ProjectView
    {
        private readonly ProjectViewModel _viewModel;

        public ProjectView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<ProjectViewModel>();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters != null && navigationContext.Parameters.Any())
            {
                var project = (Project)navigationContext.Parameters.First().Value;
                await _viewModel.LoadAsync(project, false);
            }
            else
            {
                await _viewModel.LoadAsync(new Project(), true);
            }
        }
    }
}

