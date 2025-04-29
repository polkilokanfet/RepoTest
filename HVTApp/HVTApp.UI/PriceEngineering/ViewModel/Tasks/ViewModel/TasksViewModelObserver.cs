using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelObserver : TasksViewModelVisible<TasksWrapperObserver, TaskViewModelObserver>
    {
        public TasksViewModelObserver(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperObserver GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperObserver(priceEngineeringTasks, container);
        }

        protected override bool ChildTaskIsVisibleByDefault(PriceEngineeringTask priceEngineeringTask)
        {
            var user = GlobalAppProperties.User;
            return priceEngineeringTask.GetSuitableTasksForObserve(user).Any();
        }
    }
}