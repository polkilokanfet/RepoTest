using System;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Products.Menus
{
    public partial class ProductsMenu : IOutlookBarGroup
    {
        public ProductsMenu(ProductsMenuViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public Uri DefaultViewUri
        {
            get
            {
                if (_xamDataTree.Nodes.Any() && !_xamDataTree.SelectionSettings.SelectedNodes.Any())
                    _xamDataTree.SelectionSettings.SelectedNodes.Add(_xamDataTree.Nodes[0]);

                var node = _xamDataTree.SelectionSettings.SelectedNodes[0];
                var navigationItem = node?.Data as INavigationItem;
                return navigationItem?.NavigationUri;
            }
        }
    }
}