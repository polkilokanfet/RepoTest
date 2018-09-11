using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabSave)), RibbonTab(typeof(ProductionTab))]
    public partial class ProjectView
    {
        private readonly ProjectDetailsViewModel _projectViewModel;
        public ProjectView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _projectViewModel = container.Resolve<ProjectDetailsViewModel>();
            this.DataContext = _projectViewModel;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var project = new Project();
            if (navigationContext.Parameters != null && navigationContext.Parameters.Any())
                project = (Project)navigationContext.Parameters.First().Value;
            await _projectViewModel.LoadAsync(project);
        }
    }
}

