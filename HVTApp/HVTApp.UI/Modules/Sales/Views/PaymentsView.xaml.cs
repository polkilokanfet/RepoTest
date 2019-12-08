using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views
{
    [RibbonTab(typeof(PaymentsTab))]
    public partial class PaymentsView
    {
        private readonly PaymentsViewModel _paymentsViewModel;

        public PaymentsView(PaymentsViewModel paymentsViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _paymentsViewModel = paymentsViewModel;
            InitializeComponent();
            this.DataContext = _paymentsViewModel;
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _paymentsViewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
