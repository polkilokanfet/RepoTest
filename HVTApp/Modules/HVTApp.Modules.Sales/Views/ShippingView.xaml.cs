using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(ShippingTab))]
    public partial class ShippingView
    {
        private readonly ShippingViewModel _shippingViewModel;

        public ShippingView(ShippingViewModel shippingViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _shippingViewModel = shippingViewModel;
            InitializeComponent();
            this.DataContext = _shippingViewModel;
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _shippingViewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
