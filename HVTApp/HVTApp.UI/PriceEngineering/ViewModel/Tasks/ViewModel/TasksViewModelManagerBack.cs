using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using HVTApp.UI.PriceEngineering.Tce.Second;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelManagerBack : TasksViewModelVisible<TasksWrapperBackManager, TaskViewModelBackManager>
    {
        public TasksTceWrapper TasksTceWrapper { get; }

        public TasksViewModelManagerBack(IUnityContainer container) : base(container)
        {
            TasksTceWrapper = new TasksTceWrapper(this.TasksWrapper.Model);
        }

        protected override TasksWrapperBackManager GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperBackManager(priceEngineeringTasks, container);
        }
    }
}