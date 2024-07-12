using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    [RibbonTab(typeof(TabForBackManagerBoss))]
    public partial class TaskInvoiceForPaymentBackManagerView
    {
        private readonly TaskInvoiceForPaymentViewModelBackManager _viewModel;
        public TaskInvoiceForPaymentBackManagerView(TaskInvoiceForPaymentViewModelBackManager viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
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
                    //загрузка по задаче
                    if (navigationContext.Parameters.First().Value is TaskInvoiceForPayment task)
                    {
                        _viewModel.Load(task);
                    }
                }
            }
        }
    }
}
