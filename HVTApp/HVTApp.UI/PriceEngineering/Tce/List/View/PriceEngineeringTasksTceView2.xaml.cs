using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.Tce.List.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.Tce.List.View
{
    [RibbonTab(typeof(TechnicalRequrementsTasksModule.Tabs.TabTechnicalRequrementsTasksView))]
    public partial class PriceEngineeringTasksTceView2 : ViewBase
    {
        public PriceEngineeringTasksTceView2(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            if (GlobalAppProperties.UserIsManager)
            {
                this.DataContext = container.Resolve<PriceEngineeringTasksTceViewModelFrontManager>();
            }
            else if (GlobalAppProperties.UserIsBackManager)
            {
                this.DataContext = container.Resolve<PriceEngineeringTasksTceViewModelBackManager>();
            }
            else if (GlobalAppProperties.UserIsBackManagerBoss)
            {
                this.DataContext = container.Resolve<PriceEngineeringTasksTceViewModelBackManagerBoss>();
            }
        }
    }
}
