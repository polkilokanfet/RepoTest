using HVTApp.Infrastructure;
using Infragistics.Windows.DataPresenter;

namespace HVTApp.UI.Modules.Reports.CommonInfo
{
    //[RibbonTab(typeof(TabCommonInfo))]
    public partial class CommonInfoView
    {
        protected XamDataGrid DataGrid => this.LoadableControl.Content as XamDataGrid;

        public CommonInfoView()
        {
            InitializeComponent();
        }
    }
}
