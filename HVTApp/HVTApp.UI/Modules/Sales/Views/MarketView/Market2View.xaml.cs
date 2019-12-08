using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views.MarketView
{
    [RibbonTab(typeof(MarketTab))]
    public partial class Market2View : ViewBase
    {
        //private readonly Market2ViewModel _viewModel;

        public Market2View(Market2ViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            //назначаем контексты
            //_viewModel = viewModel;
            //viewModel.Load();
            this.DataContext = viewModel;

            //this.Loaded += OnLoaded;

            viewModel.ExpandCollapseEvent += expend =>
            {
                var dg = this.ProjectsGrid; //(XamDataGrid)sender;
                foreach (var o in dg.DataSource)
                {
                    dg.GetRecordFromDataItem(o, recursive: false).IsExpanded = expend;
                }
            };

        }

        //private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        //{
        //    await _viewModel.Load();
        //    this.Loaded -= OnLoaded;
        //}
    }
}
