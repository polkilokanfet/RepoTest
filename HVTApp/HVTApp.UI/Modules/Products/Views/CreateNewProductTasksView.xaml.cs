using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Products.Tabs;
using HVTApp.UI.Modules.Products.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.Views
{
    [RibbonTab(typeof(TabCreateNewProductTasks))]
    public partial class CreateNewProductTasksView
    {
        private readonly CreateNewProductTasksViewModel _viewModel;

        public CreateNewProductTasksView(CreateNewProductTasksViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            this.DataContext = _viewModel;
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _viewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
