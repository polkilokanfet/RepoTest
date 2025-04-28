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
    public class PriceEngineeringTasksListViewModelConstructor : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemConstructor, PriceEngineeringTaskListItemConstructor>
    {
        public PriceEngineeringTasksListViewModelConstructor(IUnityContainer container) : base(container)
        {
        }

        protected override void OpenTask(NavigationParameters parameters)
        {
            if (SelectedItem is PriceEngineeringTasksListItemConstructor tasks)
            {
                if (tasks.Entity.GetSuitableTasksForInspect(GlobalAppProperties.User).Any())
                {
                    RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewInspector>(parameters);
                    return;
                }
            }
            else if (SelectedItem is PriceEngineeringTaskListItemConstructor task)
            {
                if (task.Entity.UserConstructor?.Id != GlobalAppProperties.User.Id &&
                    task.Entity.UserConstructorInspector?.Id == GlobalAppProperties.User.Id)
                {
                    RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewInspector>(parameters);
                    return;
                }
            }

            RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewConstructor>(parameters);
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