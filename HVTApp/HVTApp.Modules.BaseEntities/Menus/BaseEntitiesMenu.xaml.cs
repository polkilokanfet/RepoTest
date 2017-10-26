using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.BaseEntities.Menus
{
    public partial class BaseEntitiesMenu : IOutlookBarGroup
    {
        public BaseEntitiesMenu(BaseEntitiesMenuViewModel viewModel)
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
