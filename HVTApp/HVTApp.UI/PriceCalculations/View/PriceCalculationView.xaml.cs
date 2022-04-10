using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
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

            if (navigationContext.Parameters.Any())
            {
                if (navigationContext.Parameters.Count() == 1)
                {
                    //загрузка калькуляции 
                    if (navigationContext.Parameters.First().Value is PriceCalculation priceCalculation)
                    {
                        _viewModel.Load(priceCalculation);
                    }

                    //загрузка калькуляции по задаче из ТСЕ
                    else if (navigationContext.Parameters.First().Value is TechnicalRequrementsTask technicalRequrementsTask)
                    {
                        _viewModel.Load(technicalRequrementsTask);
                    }

                    //загрузка калькуляции по юнитам
                    else if (navigationContext.Parameters.First().Value is IEnumerable<SalesUnit> salesUnits)
                    {
                        _viewModel.Load(salesUnits);
                    }

                    //загрузка калькуляции по юнитам
                    else if (navigationContext.Parameters.First().Value is PriceEngineeringTasks priceEngineeringTasks)
                    {
                        _viewModel.Load(priceEngineeringTasks);
                    }

                }
                else if (navigationContext.Parameters.Count() == 2)
                {
                    //создание копии калькуляции
                    if (navigationContext.Parameters.First().Value is PriceCalculation priceCalculation)
                    {
                        if (navigationContext.Parameters.Last().Value == null)
                        {
                            _viewModel.CreateCopy(priceCalculation);

                        }
                        else if (navigationContext.Parameters.Last().Value is TechnicalRequrementsTask technicalRequrementsTask)
                        {
                            _viewModel.CreateCopy(priceCalculation, technicalRequrementsTask);
                        }
                    }
                }
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

            _viewModel.MeregeCommand.RaiseCanExecuteChanged();
        }

    }
}
