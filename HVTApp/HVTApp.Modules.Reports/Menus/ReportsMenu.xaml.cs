using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Reports.Menus
{
    public partial class ReportsMenu : IOutlookBarGroup
    {
        public ReportsMenu(ReportsMenuViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public Uri DefaultViewUri
        {
            get
            {
                var node = XamDataTree.SelectionSettings.SelectedNodes[0];
                var navigationItem = node?.Data as INavigationItem;
                return navigationItem?.NavigationUri;
            }
        }
    }
}
