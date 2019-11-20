using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Modules.Director.Tabs;
using HVTApp.Modules.Director.ViewModels;
using Infragistics.Windows.DataPresenter;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Director.Views
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

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var task = _viewModel.Load(); ;
            try
            {
                await task;
            }
            catch (Exception)
            {
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Exception", task.Exception.GetAllExceptions());
            }
            this.Loaded -= OnLoaded;
        }
    }
}
