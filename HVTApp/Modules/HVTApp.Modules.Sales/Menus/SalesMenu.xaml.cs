using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Sales.Menus
{
    /// <summary>
    /// Interaction logic for SalesMenu.xaml
    /// </summary>
    public partial class SalesMenu : IOutlookBarGroup
    {
        public SalesMenu(SalesMenuViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public Uri DefaultViewUri {
            get
            {
                var node = _xamDataTree.SelectionSettings.SelectedNodes[0];
                var navigationItem = node?.Data as INavigationItem;
                return navigationItem?.NavigationUri;
            }
        }
    }
}
