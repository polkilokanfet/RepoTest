using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.PriceMaking.Menus
{
    public partial class PriceMakingMenu : IOutlookBarGroup
    {
        public PriceMakingMenu(PriceMakingMenuViewModel viewModel)
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
