using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class ProjectsView
    {
        private readonly ProjectsViewModel _projectsViewModel;

        public ProjectsView(ProjectsViewModel projectsViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _projectsViewModel = projectsViewModel;
            this.DataContext = _projectsViewModel;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _projectsViewModel.LoadAsync();
            Loaded -= OnLoaded;
        }
    }
}
