using System;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.ViewModels;
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
            var projects = await _container.Resolve<IUnitOfWork>().Repository<Project>().GetAllAsNoTrackingAsync();
            _projectsViewModel.Load(projects.Where(x => x.Manager.Id == CommonOptions.User.Id));
            Loaded -= OnLoaded;
        }
    }
}
