using System.IO;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Director.Tabs;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesReport
{
    [RibbonTab(typeof(TabReload))]
    public partial class SalesReportView
    {
        protected override string FileName => "salesReportCustomisation.xml";

        protected override XamDataGrid DataGrid => (XamDataGrid)this.LoadbleControl.Content;


        public SalesReportView(SalesReportViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(viewModel, regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
