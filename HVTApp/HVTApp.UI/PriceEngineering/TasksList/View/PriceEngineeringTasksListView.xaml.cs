using System;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.Tabs;
using HVTApp.UI.PriceEngineering.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.View
{
    [RibbonTab(typeof(TabPriceEngineeringTasks))]
    public partial class PriceEngineeringTasksListView : ViewBase
    {
        public PriceEngineeringTasksListView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelSalesManager>();
                    break;
                case Role.Constructor:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelConstructor>();
                    break;
                case Role.DesignDepartmentHead:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelDesignDepartmentHead>();
                    break;
                case Role.BackManager:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelBackManager>();
                    break;
                case Role.BackManagerBoss:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelBackManagerBoss>();
                    break;
                case Role.PlanMaker:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelPlanMaker>();
                    break;
                case Role.Admin:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelAdmin>();
                    break;
                default:
                    throw new NotImplementedException();
            }

            InitializeComponent();
        }
    }
}
