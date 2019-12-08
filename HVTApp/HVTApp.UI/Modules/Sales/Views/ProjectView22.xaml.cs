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
    //[RibbonTab(typeof(TabCrudUnits))]
    public partial class ProjectView
    {
        private readonly ProjectViewModel2 _viewModel;

        public ProjectView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = container.Resolve<ProjectViewModel2>();
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters != null && navigationContext.Parameters.Any())
            {
                var project = (Project)navigationContext.Parameters.First().Value;
                _viewModel.Load(project.Id);
            }
            else
            {
                //await _viewModel.LoadAsync(new Project(), true);
            }
        }
    }
}

