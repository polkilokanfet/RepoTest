using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PaymentConditionsSet
{
    public partial class PaymentConditionsSetView
    {
        public PaymentConditionsSetView()
        {
            InitializeComponent();
        }

        public PaymentConditionsSetView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionsSetViewModel viewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
