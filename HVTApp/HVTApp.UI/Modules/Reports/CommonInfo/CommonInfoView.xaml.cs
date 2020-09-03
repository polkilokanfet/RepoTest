using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.CommonInfo
{
    [RibbonTab(typeof(TabCommonInfo))]
    public partial class CommonInfoView
    {
        protected override XamDataGrid DataGrid => this.LoadbleControl.Content as XamDataGrid;

        public CommonInfoView(CommonInfoViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) : base(viewModel, regionManager, eventAggregator, messageService)
        {
            InitializeComponent();
        }
    }
}
