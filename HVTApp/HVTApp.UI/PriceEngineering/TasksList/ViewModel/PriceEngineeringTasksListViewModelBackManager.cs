using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelBackManager : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemBackManager, PriceEngineeringTaskListItemBackManager>
    {
        public PriceEngineeringTasksListViewModelBackManager(IUnityContainer container) : base(container)
        {
        }

        protected override void OpenTask(NavigationParameters parameters)
        {
            RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManager>(parameters);
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