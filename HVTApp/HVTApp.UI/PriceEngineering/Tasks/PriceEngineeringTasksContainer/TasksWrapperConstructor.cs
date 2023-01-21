using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperConstructor : TasksWrapper<TaskViewModelConstructor>
    {
        public TasksWrapperConstructor(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override IEnumerable<TaskViewModelConstructor> GetChildPriceEngineeringTasks(IUnityContainer container)
        {
            return Model.ChildPriceEngineeringTasks.Select(priceEngineeringTask => new TaskViewModelConstructor(container, priceEngineeringTask.Id));
        }
    }
}