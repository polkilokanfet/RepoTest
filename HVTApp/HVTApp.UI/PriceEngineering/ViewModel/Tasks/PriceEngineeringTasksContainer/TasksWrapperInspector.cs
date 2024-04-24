using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperInspector : TasksWrapper<TaskViewModelInspector>
    {
        public TasksWrapperInspector(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override TaskViewModelInspector GetChildPriceEngineeringTask(IUnityContainer container, Guid childTaskId)
        {
            return new TaskViewModelInspector(container, childTaskId);
        }
    }
}