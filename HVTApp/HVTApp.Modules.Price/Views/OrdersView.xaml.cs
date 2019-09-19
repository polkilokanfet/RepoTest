using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.PlanAndEconomy.ViewModels;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class OrdersView
    {
        private readonly OrdersViewModel _viewModel;

        public OrdersView(OrdersViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
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
