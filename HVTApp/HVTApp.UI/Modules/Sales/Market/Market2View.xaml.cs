using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Market.Tabs;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market
{
    [RibbonTab(typeof(MarketTab))]
    [RibbonTab(typeof(MarketViewTab))]
    [RibbonTab(typeof(MarketSettingsTab))]
    public partial class Market2View
    {
        private Uri _currentUri;

        protected override XamDataGrid DataGrid => this.ContentControl.Content as XamDataGrid;

        public Market2View(Market2ViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) 
            : base(viewModel, regionManager, eventAggregator)
        {
            InitializeComponent();

            //приложения проекта
            viewModel.SelectedProjectItemChanged += projectItem =>
            {
                if (projectItem != null)
                {
                    _currentUri = new Uri(PathGetter.GetPath(projectItem.Project));
                    this.Browser.Source = _currentUri;
                }
            };
        }

        private void GoBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Browser.CanGoBack && this.Browser.Source != _currentUri)
                Browser.GoBack();
        }

        private void GoForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Browser.CanGoForward)
                Browser.GoForward();
        }
    }
}
