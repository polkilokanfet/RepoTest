using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelBackManager : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemBackManager, PriceEngineeringTaskListItemBackManager>
    {
        public PriceEngineeringTasksListViewModelBackManager(IUnityContainer container) : base(container)
        {
        }

        protected override PriceEngineeringTasksListItemBackManager GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemBackManager(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return engineeringTasks.BackManager?.Id == GlobalAppProperties.User.Id;
        }
    }
}