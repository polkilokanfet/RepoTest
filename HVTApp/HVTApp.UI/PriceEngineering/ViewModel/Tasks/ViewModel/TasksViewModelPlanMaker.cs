using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelPlanMaker : TasksViewModelVisible<TasksWrapperPlanMaker, TaskViewModelPlanMaker>
    {
        public bool NumberIsReadOnly
        {
            get
            {
                if (this.TasksWrapper == null) return true;
                return string.IsNullOrWhiteSpace(this.TasksWrapper.TceNumber) == false ||
                       this.TasksWrapper.ChildTasks.Any(x => x.Status.Equals(ScriptStep.ProductionRequestStart)) ==
                       false;
            }
        }

        public TasksViewModelPlanMaker(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperPlanMaker GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            var tasksWrapperBackManager = new TasksWrapperPlanMaker(priceEngineeringTasks, container);
            foreach (var task in tasksWrapperBackManager.ChildTasks)
            {
                task.SavedEvent += () =>
                {
                    this.TasksWrapper.AcceptChanges();
                    this.UnitOfWork.SaveChanges();
                };
                task.ProductionRequestFinishedEvent += () =>
                {
                    this.TasksWrapper.AcceptChanges();
                    this.UnitOfWork.SaveChanges();
                };
            }
            return tasksWrapperBackManager;
        }
    }
}