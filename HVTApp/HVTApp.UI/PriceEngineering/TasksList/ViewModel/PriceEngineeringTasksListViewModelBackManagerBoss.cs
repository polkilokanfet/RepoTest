using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelBackManagerBoss : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemBackManagerBoss, PriceEngineeringTaskListItemBackManagerBoss>
    {
        public PriceEngineeringTasksListViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
        }

        protected override void OpenTask(NavigationParameters parameters)
        {
            RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManagerBoss>(parameters);
        }

        protected override PriceEngineeringTasksListItemBackManagerBoss GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemBackManagerBoss(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            var steps = new[]
            {
                ScriptStep.LoadToTceStart,
                ScriptStep.ProductionRequestStart,
                ScriptStep.ProductionRequestStop
            };

            return engineeringTasks
                .ChildPriceEngineeringTasks
                .SelectMany(task => task.Statuses)
                .Select(status => ScriptStep.FromValue(status.StatusEnum))
                .Any(step => steps.Contains(step));
        }
    }
}