using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.SupplyModule.Menus
{
    public partial class SupplyMenu : IOutlookBarGroup
    {
        public SupplyMenu(SupplyMenuViewModel viewModel)
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
