using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelBackManager : TasksViewModelVisible<TasksWrapperBackManager, TaskViewModelBackManager>
    {
        public bool NumberIsReadOnly
        {
            get
            {
                return this.TasksWrapper.ChildTasks.Any(x => x.Status.Equals(ScriptStep.LoadToTceStart)) == false;
            }
        }

        public TasksViewModelBackManager(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperBackManager GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            var tasksWrapperBackManager = new TasksWrapperBackManager(priceEngineeringTasks, container);
            foreach (var task in tasksWrapperBackManager.ChildTasks)
            {
                task.SavedEvent += () =>
                {
                    this.TasksWrapper.AcceptChanges();
                    this.UnitOfWork.SaveChanges();
                };
                task.LoadToTceFinishedEvent += () =>
                {
                    this.TasksWrapper.AcceptChanges();
                    this.UnitOfWork.SaveChanges();
                };
            }
            return tasksWrapperBackManager;
        }

        protected override bool ChildTaskIsVisibleByDefault(PriceEngineeringTask priceEngineeringTask)
        {
            var user = GlobalAppProperties.User;
            var backManager = priceEngineeringTask.GetPriceEngineeringTasks(this.UnitOfWork).BackManager;
            return backManager != null && user.Id == backManager.Id;

        }
    }
}