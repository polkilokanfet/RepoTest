using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelBackManagerBoss : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemBackManagerBoss, PriceEngineeringTaskListItemBackManagerBoss>
    {
        public PriceEngineeringTasksListViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
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
                ScriptStep.StopProductionRequest
            };

            return engineeringTasks
                .ChildPriceEngineeringTasks
                .SelectMany(task => task.Statuses)
                .Select(status => ScriptStep.FromValue(status.StatusEnum))
                .Any(step => steps.Contains(step));
        }
    }
}