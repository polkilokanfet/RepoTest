using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class PriceEngineeringTasksContainerWrapperDesignDepartmentHead : PriceEngineeringTasksContainerWrapper<TaskViewModelDesignDepartmentHead>
    {
        public PriceEngineeringTasksContainerWrapperDesignDepartmentHead(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override IEnumerable<TaskViewModelDesignDepartmentHead> GetChildPriceEngineeringTasks(IUnityContainer container)
        {
            return Model.ChildPriceEngineeringTasks.Select(priceEngineeringTask => new TaskViewModelDesignDepartmentHead(container, priceEngineeringTask.Id));
        }
    }
}