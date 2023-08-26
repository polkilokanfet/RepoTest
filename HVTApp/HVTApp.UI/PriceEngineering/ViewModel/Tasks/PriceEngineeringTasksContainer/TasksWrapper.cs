using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public abstract class TasksWrapper<TPriceEngineeringTaskViewModel> : TasksWrapper0
        where TPriceEngineeringTaskViewModel : TaskViewModel
    {
        /// <summary>
        /// Задачи
        /// </summary>
        public IValidatableChangeTrackingCollection<TPriceEngineeringTaskViewModel> ChildTasks { get; protected set; }

        #region ctors

        /// <summary>
        /// Для создания новой ТСП
        /// </summary>
        protected TasksWrapper() : base(new PriceEngineeringTasks())
        {
        }

        /// <summary>
        /// Для загрузки существующей ТСП
        /// </summary>
        /// <param name="model"></param>
        /// <param name="container"></param>
        protected TasksWrapper(PriceEngineeringTasks model, IUnityContainer container) : base(model)
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            ChildTasks = new ValidatableChangeTrackingCollection<TPriceEngineeringTaskViewModel>(GetChildPriceEngineeringTasks(container));
            //ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TPriceEngineeringTaskViewModel>(this.Model.ChildPriceEngineeringTasks.Select(x => container.Resolve<TasksWrapperFactory>().GetTaskViewModel<TPriceEngineeringTaskViewModel>(x.Id)));
            ////RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
        }

        #endregion

        /// <summary>
        /// Получение ViewModel задачи из этого сборника
        /// </summary>
        /// <param name="container"></param>
        /// <param name="childTaskId"></param>
        /// <returns></returns>
        protected abstract TPriceEngineeringTaskViewModel GetChildPriceEngineeringTask(IUnityContainer container, Guid childTaskId);

        protected virtual IEnumerable<TPriceEngineeringTaskViewModel> GetChildPriceEngineeringTasks(IUnityContainer container)
        {
            return this.Model.ChildPriceEngineeringTasks.Select(task => this.GetChildPriceEngineeringTask(container, task.Id));
        }
    }

    //public class TasksWrapperFactory
    //{
    //    private readonly IUnityContainer _container;

    //    public TasksWrapperFactory(IUnityContainer container)
    //    {
    //        _container = container;
    //    }

    //    public TTaskViewModel GetTaskViewModel<TTaskViewModel>(Guid id) where TTaskViewModel : TaskViewModel
    //    {
    //        if (typeof(TTaskViewModel) == typeof(TaskViewModelConstructor))
    //        {
    //            return new TaskViewModelConstructor(_container, id) as TTaskViewModel;
    //        }
    //        if (typeof(TTaskViewModel) == typeof(TaskViewModelDesignDepartmentHead))
    //        {
    //            return new TaskViewModelDesignDepartmentHead(_container, id) as TTaskViewModel;
    //        }

    //        throw new NotImplementedException();
    //    }
    //}
}