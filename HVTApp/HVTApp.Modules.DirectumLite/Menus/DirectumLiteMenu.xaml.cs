using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.DirectumLite.Menus
{
    public partial class DirectumLiteMenu : IOutlookBarGroup
    {
        public DirectumLiteMenu(DirectumLiteMenuViewModel viewModel)
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
