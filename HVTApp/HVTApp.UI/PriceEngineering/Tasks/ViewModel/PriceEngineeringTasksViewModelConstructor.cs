using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    /// <summary>
    /// PriceEngineeringTasksViewModel для конструктора
    /// </summary>
    public class PriceEngineeringTasksViewModelConstructor : PriceEngineeringTasksViewModel<PriceEngineeringTasksContainerWrapperConstructor, PriceEngineeringTaskViewModelConstructor>
    {
        public PriceEngineeringTasksViewModelConstructor(IUnityContainer container) : base(container)
        {
        }

        protected override PriceEngineeringTasksContainerWrapperConstructor GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new PriceEngineeringTasksContainerWrapperConstructor(priceEngineeringTasks, container);
        }
    }
}