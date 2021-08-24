using System.Windows;
using HVTApp.UI.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class PaymentConditionSetLookupListView1
    {
        public PaymentConditionSetLookupListView1()
        {
            InitializeComponent();
        }

        public PaymentConditionSetLookupListView1(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionSetLookupListViewModel paymentConditionSetLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = paymentConditionSetLookupListViewModel;
            paymentConditionSetLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ((PaymentConditionSetLookupListViewModel)DataContext).Load();
        }

    }
}
