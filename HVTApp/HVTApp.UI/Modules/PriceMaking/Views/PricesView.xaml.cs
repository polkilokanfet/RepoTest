using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PlanAndEconomy.Tabs;
using HVTApp.UI.Modules.PriceMaking.ViewModels;
using Prism.Events;
using Prism.Regions;
using TabPriceTasks = HVTApp.UI.Modules.PriceMaking.Tabs.TabPriceTasks;

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

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _pricesViewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
