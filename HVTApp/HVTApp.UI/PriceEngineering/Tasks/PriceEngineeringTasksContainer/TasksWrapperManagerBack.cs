using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperManagerBack : TasksWrapper<TaskViewModelManagerBack>
    {
        public TasksWrapperManagerBack(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override TaskViewModelManagerBack GetChildPriceEngineeringTask(IUnityContainer container, Guid id)
        {
            return new TaskViewModelManagerBack(container, id);
        }
    }
}