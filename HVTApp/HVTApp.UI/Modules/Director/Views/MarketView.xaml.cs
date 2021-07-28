using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Modules.Director.Tabs;
using HVTApp.UI.Modules.Director.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Director.Views
{
    [RibbonTab(typeof(TabReload))]
    public partial class MarketView
    {
        private readonly IUnityContainer _container;
        private readonly MarketViewModel _viewModel;
        public MarketView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _container = container;
            InitializeComponent();
            _viewModel = container.Resolve<MarketViewModel>();
            this.DataContext = _viewModel;
            this.Loaded += OnLoaded;
            _viewModel.ExpandCollapseEvent += expend =>
            {
                var dg = this.DataGrid; //(XamDataGrid)sender;
                foreach (var o in dg.DataSource)
                {
                    dg.GetRecordFromDataItem(o, recursive: false).IsExpanded = expend;
                }
            };
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                _viewModel.Load();
            }
            catch (Exception e)
            {
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Exception", e.PrintAllExceptions());
            }
            this.Loaded -= OnLoaded;
        }
    }
}
