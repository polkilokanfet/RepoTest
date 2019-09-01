using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(SalesCRUD)), RibbonTab(typeof(TabProjects))]
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
        }

        //private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        //{
        //    await _viewModel.Load();
        //    this.Loaded -= OnLoaded;
        //}
    }
}
