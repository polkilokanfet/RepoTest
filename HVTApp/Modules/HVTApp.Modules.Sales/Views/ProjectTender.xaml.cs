using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    public partial class ProjectTender : ViewBase
    {
        private readonly ProjectTenderViewModel _viewModel;

        public ProjectTender(IRegionManager regionManager, IEventAggregator eventAggregator, ProjectTenderViewModel viewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            _viewModel = viewModel;
            this.DataContext = _viewModel;
            this.ProjectListView.DataContext = _viewModel.ProjectListViewModel;
            this.SalesUnitListView.DataContext = _viewModel.SalesUnitListViewModel;
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _viewModel.ProjectListViewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
