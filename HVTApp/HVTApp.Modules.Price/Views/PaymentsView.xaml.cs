using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Price.Tabs;
using HVTApp.Modules.Price.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Price.Views
{
    [RibbonTab(typeof(TabPayments))]
    public partial class PaymentsView
    {
        private readonly PaymentsViewModel _viewModel;

        public PaymentsView(PaymentsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
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
