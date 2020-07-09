using System;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Sales.Menus
{
    public partial class SalesMenu : IOutlookBarGroup
    {
        public SalesMenu(SalesMenuViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public Uri DefaultViewUri
        {
            get
            {
                if (XamDataTree.Nodes.Any() && !XamDataTree.SelectionSettings.SelectedNodes.Any())
                    XamDataTree.SelectionSettings.SelectedNodes.Add(XamDataTree.Nodes[0]);

                var node = XamDataTree.SelectionSettings.SelectedNodes[0];
                var navigationItem = node?.Data as INavigationItem;
                return navigationItem?.NavigationUri;
            }
        }
    }
}
