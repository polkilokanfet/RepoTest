using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperConstructor : TasksWrapper<TaskViewModelConstructor>
    {
        public TasksWrapperConstructor(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override TaskViewModelConstructor GetChildPriceEngineeringTask(IUnityContainer container, Guid id)
        {
            return new TaskViewModelConstructor(container, id, null);
        }
    }
}