using System.Windows;
using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TaskInvoiceForPayment1.List
{
    [RibbonTab(typeof(TabTaskInvoiceForPaymentListView))]
    public partial class TaskInvoiceForPaymentListView
    {
        private readonly TaskInvoiceForPaymentListViewModel _viewModel;

        public TaskInvoiceForPaymentListView(TaskInvoiceForPaymentListViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            this.DataContext = viewModel;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.PropertyChanged += (o, eventArgs) =>
            {
                if (eventArgs.PropertyName == nameof(_viewModel.IsShownActual))
                {
                    this.DataGrid.SetFilter("IsActual", _viewModel.IsShownActual, true);
                }

                //this.DataGrid.SetFilter("ToShow", viewModel.IsShownActual, true);
            };

            this.Loaded -= OnLoaded;
        }
    }
}
