using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.CommonEntities.Menus
{
    /// <summary>
    /// Interaction logic for CommonEntitiesMenu.xaml
    /// </summary>
    public partial class CommonEntitiesMenu : IOutlookBarGroup
    {
        public CommonEntitiesMenu(CommonEntitiesMenuViewModel viewModel)
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
