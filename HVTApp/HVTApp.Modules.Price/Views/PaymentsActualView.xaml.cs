using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.PlanAndEconomy.Tabs;
using HVTApp.Modules.PlanAndEconomy.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.Views
{
    [RibbonTab(typeof(TabPaymentDocuments))]
    public partial class PaymentsActualView
    {
        private readonly PaymentsActualViewModel _viewModel;

        public PaymentsActualView(PaymentsActualViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
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
