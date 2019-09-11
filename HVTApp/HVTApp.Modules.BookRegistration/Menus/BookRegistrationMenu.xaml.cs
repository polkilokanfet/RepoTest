using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.BookRegistration.Menus
{
    public partial class BookRegistrationMenu : IOutlookBarGroup
    {
        public BookRegistrationMenu(BookRegistrationMenuViewModel viewModel)
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
