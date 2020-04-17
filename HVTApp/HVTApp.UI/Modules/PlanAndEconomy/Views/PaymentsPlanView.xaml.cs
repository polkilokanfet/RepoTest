using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PlanAndEconomy.Tabs;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.Views
{
    [RibbonTab(typeof(TabPayments))]
    public partial class PaymentsPlanView
    {
        public PaymentsPlanView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = new PaymentsPlanViewModel(container, true);
        }
    }
}
