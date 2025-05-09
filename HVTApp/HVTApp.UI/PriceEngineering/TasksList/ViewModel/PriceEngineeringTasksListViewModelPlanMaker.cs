using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelPlanMaker : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemPlanMaker, PriceEngineeringTaskListItemPlanMaker>
    {
        public PriceEngineeringTasksListViewModelPlanMaker(IUnityContainer container) : base(container)
        {
            container.Resolve<IEventAggregator>().GetEvent<AfterUploadDocumentationInTeamCenterEvent>().Subscribe(
                priceEngineeringTask =>
                {
                    foreach (var item in Items)
                    {
                        foreach (var priceEngineeringTaskListItemPlanMaker in item.ChildPriceEngineeringTasks)
                        {
                            if (priceEngineeringTaskListItemPlanMaker.Id != priceEngineeringTask.Id) continue;
                            priceEngineeringTaskListItemPlanMaker.RefreshIsUploadedDocumentationToTeamCenter();
                            item.RefreshIsUploadedDocumentationToTeamCenter();
                        }
                    }
                });
        }

        protected override void OpenTask(NavigationParameters parameters)
        {
            RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewPlanMaker>(parameters);
        }

        protected override PriceEngineeringTasksListItemPlanMaker GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemPlanMaker(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return engineeringTasks.GetSuitableTasksForOpenOrder(GlobalAppProperties.User).Any();
        }
    }
}