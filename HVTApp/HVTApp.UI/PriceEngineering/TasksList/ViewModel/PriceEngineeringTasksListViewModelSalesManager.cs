using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelSalesManager : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemSalesManager, PriceEngineeringTaskListItemSalesManager>
    {
        public PriceEngineeringTasksListViewModelSalesManager(IUnityContainer container) : base(container)
        {
        }

        protected override void OpenTask(NavigationParameters parameters)
        {
            RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(parameters);
        }

        protected override PriceEngineeringTasksListItemSalesManager GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemSalesManager(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return engineeringTasks.UserManager.Id == GlobalAppProperties.User.Id;
        }
    }
}