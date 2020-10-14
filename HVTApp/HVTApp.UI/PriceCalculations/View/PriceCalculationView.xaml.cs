using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.ViewModel;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.Editors;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceCalculations.View
{
    [RibbonTab(typeof(Tabs.TabPriceCalculation))]
    public partial class PriceCalculationView 
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
                _viewModel.Load((PriceCalculation)navigationContext.Parameters.First().Value);
            }

            if (navigationContext.Parameters.First().Value is TechnicalRequrementsTask)
            {
                _viewModel.Load((TechnicalRequrementsTask)navigationContext.Parameters.First().Value);
            }

            if (navigationContext.Parameters.First().Value is IEnumerable<SalesUnit>)
            {
                _viewModel.Load((IEnumerable<SalesUnit>)navigationContext.Parameters.First().Value);
            }

            //var dg = this.Groups; //(XamDataGrid)sender;
            //foreach (var o in dg.DataSource)
            //{
            //    dg.GetRecordFromDataItem(o, recursive: false).IsExpanded = true;
            //}
        }

        void IsCheckedValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (Equals(e.OldValue, e.NewValue))
                return;

            var editor = sender as XamCheckEditor;
            ((PriceCalculationItem2Wrapper)(((DataRecord)editor.DataContext).DataItem)).IsChecked = editor.IsChecked.Value;

            ((DelegateCommand) _viewModel.MeregeCommand).RaiseCanExecuteChanged();
        }

    }
}
