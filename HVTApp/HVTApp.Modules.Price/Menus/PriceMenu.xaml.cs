using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Price.Menus
{
    public partial class PriceMenu : IOutlookBarGroup
    {
        public PriceMenu(PriceMenuViewModel viewModel)
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
