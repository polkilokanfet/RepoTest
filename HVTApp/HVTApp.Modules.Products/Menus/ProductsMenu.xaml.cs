﻿using System;
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
                if (_xamDataTree.Nodes.Count > 0 && _xamDataTree.SelectionSettings.SelectedNodes.Count == 0)
                    _xamDataTree.SelectionSettings.SelectedNodes.Add(_xamDataTree.Nodes[0]);

                var node = _xamDataTree.SelectionSettings.SelectedNodes[0];
                var navigationItem = node?.Data as INavigationItem;
                return navigationItem?.NavigationUri;
            }
        }
    }
}