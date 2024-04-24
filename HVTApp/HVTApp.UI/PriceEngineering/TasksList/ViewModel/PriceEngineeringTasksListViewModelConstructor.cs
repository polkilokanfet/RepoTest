using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelConstructor : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemConstructor, PriceEngineeringTaskListItemConstructor>
    {
        public PriceEngineeringTasksListViewModelConstructor(IUnityContainer container) : base(container)
        {
        }

        protected override PriceEngineeringTasksListItemConstructor GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemConstructor(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return engineeringTasks.GetSuitableTasksForWork(GlobalAppProperties.User).Any() ||
                   engineeringTasks.GetSuitableTasksForInspect(GlobalAppProperties.User).Any();
        }
    }
}