using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    /// <summary>
    /// PriceEngineeringTasksViewModel для конструктора
    /// </summary>
    public class PriceEngineeringTasksViewModelConstructor : PriceEngineeringTasksViewModelVisible<TasksWrapperConstructor, TaskViewModelConstructor>
    {
        public PriceEngineeringTasksViewModelConstructor(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperConstructor GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperConstructor(priceEngineeringTasks, container);
        }
    }
}