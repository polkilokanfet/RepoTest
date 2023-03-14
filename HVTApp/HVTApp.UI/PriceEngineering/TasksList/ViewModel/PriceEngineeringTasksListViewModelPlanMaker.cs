using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelPlanMaker : 
        PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemPlanMaker, PriceEngineeringTaskListItemPlanMaker>
    {
        public PriceEngineeringTasksListViewModelPlanMaker(IUnityContainer container) : base(container)
        {
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