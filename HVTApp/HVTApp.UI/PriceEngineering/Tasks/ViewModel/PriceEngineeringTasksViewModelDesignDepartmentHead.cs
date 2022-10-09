using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    /// <summary>
    /// PriceEngineeringTasksViewModel для руководителя КБ
    /// </summary>
    public class PriceEngineeringTasksViewModelDesignDepartmentHead : PriceEngineeringTasksViewModel<PriceEngineeringTasksContainerWrapperDesignDepartmentHead, PriceEngineeringTaskViewModelDesignDepartmentHead>
    {
        public PriceEngineeringTasksViewModelDesignDepartmentHead(IUnityContainer container) : base(container)
        {
        }

        protected override PriceEngineeringTasksContainerWrapperDesignDepartmentHead GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new PriceEngineeringTasksContainerWrapperDesignDepartmentHead(priceEngineeringTasks, container);
        }
    }
}