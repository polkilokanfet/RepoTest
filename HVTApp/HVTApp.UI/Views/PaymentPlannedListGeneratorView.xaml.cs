using System.Windows;
using HVTApp.UI.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class PaymentPlannedListGeneratorView 
    {
        private readonly PaymentPlannedListGeneratorViewModel _viewModel;

        public PaymentPlannedListGeneratorView(PaymentPlannedListGeneratorViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.GeneratePaymentsCommand.Execute(null);
            Loaded -= OnLoaded;
        }
    }
}
