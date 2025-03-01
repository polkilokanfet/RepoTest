using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.Modules.Director.Tabs;
using HVTApp.UI.Modules.Reports.SalesReport;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.FlatReport.Comparator
{
    [RibbonTab(typeof(TabReload))]
    public partial class BudgetComparisionView : IDataContext
    {
        //protected override XamDataGrid DataGrid => this.LoadbleControl.Content as XamDataGrid;

        public BudgetComparisionView()
        {
            InitializeComponent();
        }

        public BudgetComparisionView(BudgetComparisionViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
