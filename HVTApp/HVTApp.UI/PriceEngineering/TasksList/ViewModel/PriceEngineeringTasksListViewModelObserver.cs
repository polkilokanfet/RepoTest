using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelObserver : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemObserver, PriceEngineeringTaskListItemObserver>
    {
        public PriceEngineeringTasksListViewModelObserver(IUnityContainer container) : base(container)
        {
        }

        protected override void OpenTask(NavigationParameters parameters)
        {
            RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewObserver>(parameters);
        }

        protected override PriceEngineeringTasksListItemObserver GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemObserver(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return engineeringTasks.GetSuitableTasksForObserve(GlobalAppProperties.User).Any();
        }
    }
}