using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Price.Menus
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
                var node = _xamDataTree.SelectionSettings.SelectedNodes[0];
                var navigationItem = node?.Data as INavigationItem;
                return navigationItem?.NavigationUri;
            }
        }
    }
}
