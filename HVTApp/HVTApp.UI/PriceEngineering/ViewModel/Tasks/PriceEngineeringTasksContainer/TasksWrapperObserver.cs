using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperObserver : TasksWrapper<TaskViewModelObserver>
    {
        public TasksWrapperObserver(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override TaskViewModelObserver GetChildPriceEngineeringTask(IUnityContainer container, Guid childTaskId)
        {
            return new TaskViewModelObserver(container, childTaskId);
        }
    }
}