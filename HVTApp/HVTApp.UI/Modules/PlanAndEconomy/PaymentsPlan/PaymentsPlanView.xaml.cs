using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan
{
    [RibbonTab(typeof(TabPayments))]
    public partial class PaymentsPlanView
    {
        public PaymentsPlanView()
        {
            InitializeComponent();
        }

        public PaymentsPlanView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = new PaymentsPlanViewModel(container, true);
        }
    }
}
