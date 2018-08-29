using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    public partial class PaymentsPlannedView : ViewBase
    {
        private readonly PaymentsPlannedViewModel _paymentsPlannedViewModel;

        public PaymentsPlannedView(PaymentsPlannedViewModel paymentsPlannedViewModel, IRegionManager regionManager, 
            IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            _paymentsPlannedViewModel = paymentsPlannedViewModel;
            DataContext = paymentsPlannedViewModel;
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _paymentsPlannedViewModel.LoadAllAsync();
            Loaded -= OnLoaded;
        }
    }
}
