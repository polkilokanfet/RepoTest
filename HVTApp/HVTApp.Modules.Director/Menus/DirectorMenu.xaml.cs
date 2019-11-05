using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Director.Menus
{
    public partial class DirectorMenu : IOutlookBarGroup
    {
        public DirectorMenu(DirectorMenuViewModel viewModel)
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
