using System;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.PlanAndEconomy.Menus
{
    public partial class PlanAndEconomyMenu : IOutlookBarGroup
    {
        public PlanAndEconomyMenu(PlanAndEconomyMenuViewModel viewModel)
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
