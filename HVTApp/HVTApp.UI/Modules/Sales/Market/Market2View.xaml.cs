using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Modules.Sales.Market.Tabs;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market
{
    [RibbonTab(typeof(MarketMainTab))]
    [RibbonTab(typeof(MarketViewTab))]
    [RibbonTab(typeof(MarketSettingsTab))]
    public partial class Market2View
    {
        private readonly Market2ViewModel _viewModel;
        private Uri _currentUri;

        protected override XamDataGrid DataGrid => this.ContentControl.Content as XamDataGrid;

        public Market2View(Market2ViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) : base(viewModel, regionManager, eventAggregator, messageService)
        {
            _viewModel = viewModel;
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

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsLoaded)
            {
                ExpandCollapseMethod(true);
                ExpandCollapseMethod(false);
            }

            this.Loaded -= OnLoaded;
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
