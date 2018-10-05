using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.PlanAndEconomy.ViewModels;
using Prism.Events;
using Prism.Regions;
using TabPriceTasks = HVTApp.Modules.PlanAndEconomy.Tabs.TabPriceTasks;

namespace HVTApp.Modules.PlanAndEconomy.Views
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
