using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.Tabs;
using HVTApp.Modules.PlanAndEconomy.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.Views
{
    [RibbonTab(typeof(TabOrder))]
    public partial class OrderView
    {
        private readonly OrderViewModel _viewModel;
        public OrderView(OrderViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            var order = navigationContext.Parameters.First().Value as Order;
            await _viewModel.LoadAsync(order);
        }
    }
}
