using System;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelObserver : TaskViewModel
    {
        public override bool IsTarget => Model.DesignDepartment.Head.Id == GlobalAppProperties.User.Id ||
                                         Model.DesignDepartment.Observers.ContainsById(GlobalAppProperties.User);
        
        public TaskViewModelObserver(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
            //Вложенные дочерние задачи
            var childTasks = Model.ChildPriceEngineeringTasks.Select(engineeringTask => new TaskViewModelObserver(container, engineeringTask.Id));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(childTasks);
        }
    }
}