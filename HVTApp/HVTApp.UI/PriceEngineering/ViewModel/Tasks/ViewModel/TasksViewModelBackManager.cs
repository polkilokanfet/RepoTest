using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelBackManager : TasksViewModelVisible<TasksWrapperBackManager, TaskViewModelBackManager>
    {
        public TasksViewModelBackManager(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperBackManager GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperBackManager(priceEngineeringTasks, container);
        }
    }
}