using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Messenger.Menus
{
    public partial class MessengerMenu : IOutlookBarGroup
    {
        public MessengerMenu(MessengerMenuViewModel viewModel)
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
