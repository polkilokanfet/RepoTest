using System.ComponentModel;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.ViewModel;
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TaskInvoiceForPayment1
{
    [RibbonTab(typeof(TabForBackManagerBoss))]
    public partial class TaskInvoiceForPaymentListView
    {
        public TaskInvoiceForPaymentListView(TaskInvoiceForPaymentListViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            //назначаем контексты
            this.DataContext = viewModel;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is INotifyPropertyChanged propertyChangedViewModel)
            {
                if (propertyChangedViewModel is IIsShownActual viewModel)
                {
                    propertyChangedViewModel.PropertyChanged += (o, eventArgs) =>
                    {
                        if (eventArgs.PropertyName == nameof(viewModel.IsShownActual))
                        {
                            this.DataGrid.SetFilter("ToShow", viewModel.IsShownActual, true);
                        }

                        this.DataGrid.SetFilter("ToShow", viewModel.IsShownActual, true);
                    };
                }
            }

            this.Loaded -= OnLoaded;
        }
    }
}
