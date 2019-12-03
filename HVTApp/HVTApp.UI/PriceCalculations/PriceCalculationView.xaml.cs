using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Prism.Events;
using Prism.Regions;
using ViewBase = HVTApp.Infrastructure.ViewBase;

namespace HVTApp.UI.PriceCalculations
{
    [RibbonTab(typeof(TabPriceCalculation))]
    public partial class PriceCalculationView : ViewBase
    {
        private readonly PriceCalculationViewModel _viewModel;
        public PriceCalculationView(PriceCalculationViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            _viewModel = viewModel;

            //назначаем контексты
            this.DataContext = viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters.First().Value is PriceCalculation)
            {
                var priceCalculation = navigationContext.Parameters.First().Value as PriceCalculation;
                _viewModel.Load(priceCalculation);
            }

            if (navigationContext.Parameters.First().Value is IEnumerable<SalesUnit>)
            {
                var salesUnits = navigationContext.Parameters.First().Value as IEnumerable<SalesUnit>;
                _viewModel.Load(salesUnits);
            }

            //var dg = this.Groups; //(XamDataGrid)sender;
            //foreach (var o in dg.DataSource)
            //{
            //    dg.GetRecordFromDataItem(o, recursive: false).IsExpanded = true;
            //}
        }
    }
}
