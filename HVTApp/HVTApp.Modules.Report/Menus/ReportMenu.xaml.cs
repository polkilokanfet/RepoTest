using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Report.Menus
{
    public partial class ReportMenu : IOutlookBarGroup
    {
        public ReportMenu(ReportMenuViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public Uri DefaultViewUri
        {
            get
            {
                var node = _xamDataTree.SelectionSettings.SelectedNodes[0];
                var navigationItem = node?.Data as INavigationItem;
                return navigationItem?.NavigationUri;
            }
        }
    }
}
