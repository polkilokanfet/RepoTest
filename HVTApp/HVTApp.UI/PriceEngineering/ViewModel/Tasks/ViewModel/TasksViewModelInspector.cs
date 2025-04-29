using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;
using System.Linq;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelInspector : TasksViewModelVisible<TasksWrapperInspector, TaskViewModelInspector>
    {
        public TasksViewModelInspector(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperInspector GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperInspector(priceEngineeringTasks, container);
        }

        protected override bool ChildTaskIsVisibleByDefault(PriceEngineeringTask priceEngineeringTask)
        {
            var user = GlobalAppProperties.User;
            return priceEngineeringTask.GetSuitableTasksForInspect(user).Any();
        }
    }
}