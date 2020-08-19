using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PriceMaking.Tabs;
using HVTApp.UI.Modules.PriceMaking.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PriceMaking.Views
{
    [RibbonTab(typeof(TabPriceTasks))]
    public partial class PricesView
    {
        private readonly PricesViewModel _pricesViewModel;

        public PricesView(PricesViewModel pricesViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _pricesViewModel = pricesViewModel;
            InitializeComponent();
            this.DataContext = pricesViewModel;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _pricesViewModel.Load();
            this.Loaded -= OnLoaded;
        }
    }
}
