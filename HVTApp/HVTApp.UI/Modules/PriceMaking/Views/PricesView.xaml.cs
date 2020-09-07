using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Modules.PriceMaking.Tabs;
using HVTApp.UI.Modules.PriceMaking.ViewModels;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PriceMaking.Views
{
    [RibbonTab(typeof(TabPriceTasks))]
    public partial class PricesView
    {
        protected override XamDataGrid DataGrid => null; //this.BlocksGroupBox.Content as XamDataGrid;

        public PricesView(PricesViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) 
            : base(viewModel, regionManager, eventAggregator, messageService)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

    }
}
