using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelBackManagerBoss : TasksViewModelVisible<TasksWrapperBackManagerBoss, TaskViewModelBackManagerBoss>
    {
        public TasksViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperBackManagerBoss GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperBackManagerBoss(priceEngineeringTasks, container);
        }
    }
}