using System.IO;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Modules.Director.Tabs;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesReport
{
    [RibbonTab(typeof(TabReload))]
    public partial class SalesReportView
    {
        protected override XamDataGrid DataGrid => this.LoadbleControl.Content as XamDataGrid;


        public SalesReportView(SalesReportViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) : base(viewModel, regionManager, eventAggregator, messageService)
        {
            InitializeComponent();
        }
    }
}
