using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.Lookup;
using HVTApp.UI.Tabs;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectsView
    {
        private readonly IUnityContainer _container;
        private readonly ProjectsViewModel _projectsViewModel;

        public ProjectsView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _container = container;
            InitializeComponent();
            _projectsViewModel = container.Resolve<ProjectsViewModel>();
            this.DataContext = _projectsViewModel;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var units = await _container.Resolve<IUnitOfWork>().Repository<SalesUnit>().GetAllAsync();
            units = units.Where(x => x.Project.Manager.Id == CommonOptions.User.Id).ToList();
            var projects = units.Select(x => x.Project).Distinct();
            var lookups = projects.Select(x => new ProjectLookup(x)).ToList();
            foreach (var projectLookup in lookups)
            {
                projectLookup.SalesUnits.AddRange(units.Where(x => x.Project.Id == projectLookup.Id).Select(x => new SalesUnitLookup(x)));
            }
            _projectsViewModel.Load(lookups.OrderBy(x => x.RealizationDate));
            Loaded -= OnLoaded;
        }
    }
}
