using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    /// <summary>
    /// PriceEngineeringTasksViewModel ��� ������������
    /// </summary>
    public class TasksViewModelConstructor : TasksViewModelVisible<TasksWrapperConstructor, TaskViewModelConstructor>
    {
        public TasksViewModelConstructor(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperConstructor GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperConstructor(priceEngineeringTasks, container);
        }

        protected override bool ChildTaskIsVisibleByDefault(PriceEngineeringTask priceEngineeringTask)
        {
            var user = GlobalAppProperties.User;
            return priceEngineeringTask.GetSuitableTasksForWork(user).Any();
        }
    }
}