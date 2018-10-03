using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.PlanAndEconomy.ViewModels;
using Prism.Events;
using Prism.Regions;
using TabPayments = HVTApp.Modules.PlanAndEconomy.Tabs.TabPayments;

namespace HVTApp.Modules.PlanAndEconomy.Views
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
