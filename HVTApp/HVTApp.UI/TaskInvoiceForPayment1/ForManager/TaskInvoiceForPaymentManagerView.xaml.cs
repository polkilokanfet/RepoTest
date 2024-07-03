using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.Editors;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    [RibbonTab(typeof(PriceCalculations.Tabs.TabPriceCalculation))]
    public partial class TaskInvoiceForPaymentManagerView
    {
        private readonly TaskInvoiceForPaymentViewModelManager _viewModel;
        public TaskInvoiceForPaymentManagerView(TaskInvoiceForPaymentViewModelManager viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
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
                    if (navigationContext.Parameters.First().Value is TaskInvoiceForPayment task)
                    {
                        _viewModel.Load(task);
                    }

                    ////загрузка калькуляции по задаче из ТСЕ
                    //else if (navigationContext.Parameters.First().Value is TechnicalRequrementsTask technicalRequrementsTask)
                    //{
                    //    _viewModel.Load(technicalRequrementsTask);
                    //}

                    ////загрузка калькуляции по юнитам
                    //else if (navigationContext.Parameters.First().Value is IEnumerable<SalesUnit> salesUnits)
                    //{
                    //    _viewModel.Load(salesUnits);
                    //}
                }
            }
        }
    }
}
