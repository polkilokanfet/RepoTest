using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.Tce.List.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.Tce.List.View
{
    [RibbonTab(typeof(TechnicalRequrementsTasksModule.Tabs.TabTechnicalRequrementsTasksView))]
    public partial class PriceEngineeringTasksTceView
    {
        public PriceEngineeringTasksTceView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                this.DataContext = container.Resolve<PriceEngineeringTasksTceViewModelFrontManager>();
            }
            else if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                this.DataContext = container.Resolve<PriceEngineeringTasksTceViewModelBackManager>();
            }
            else if (GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss)
            {
                this.DataContext = container.Resolve<PriceEngineeringTasksTceViewModelBackManagerBoss>();
            }
        }
    }
}
