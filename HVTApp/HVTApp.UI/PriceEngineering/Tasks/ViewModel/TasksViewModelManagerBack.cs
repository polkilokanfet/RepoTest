using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelManagerBack : TasksViewModelVisible<TasksWrapperManagerBack, TaskViewModelManagerBack>
    {
        public TasksViewModelManagerBack(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperManagerBack GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperManagerBack(priceEngineeringTasks, container);
        }
    }
}