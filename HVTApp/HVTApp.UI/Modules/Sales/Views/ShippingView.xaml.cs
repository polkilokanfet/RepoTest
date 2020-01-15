using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views
{
    [RibbonTab(typeof(ShippingTab))]
    public partial class ShippingView
    {
        private readonly ShippingViewModel _viewModel;

        public ShippingView(ShippingViewModel _viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            this._viewModel = _viewModel;
            InitializeComponent();
            this.DataContext = this._viewModel;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.Load();
            this.Loaded -= OnLoaded;
        }
    }
}
