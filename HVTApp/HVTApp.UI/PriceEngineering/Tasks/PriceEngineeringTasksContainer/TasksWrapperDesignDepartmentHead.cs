using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperDesignDepartmentHead : TasksWrapper<TaskViewModelDesignDepartmentHead>
    {
        public TasksWrapperDesignDepartmentHead(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override TaskViewModelDesignDepartmentHead GetChildPriceEngineeringTask(IUnityContainer container, Guid id)
        {
            return new TaskViewModelDesignDepartmentHead(container, id);
        }
    }
}