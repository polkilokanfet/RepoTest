using System;
using System.Linq;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelInspector : TaskViewModelBaseDesignDepartmentHead
    {
        #region Commands

        /// <summary>
        /// Принять проработку
        /// </summary>
        public ICommandRaiseCanExecuteChanged AcceptPriceEngineeringTaskCommand { get; }

        /// <summary>
        /// Отклонить проработку конструктору
        /// </summary>
        public ICommandRaiseCanExecuteChanged RejectPriceEngineeringTaskCommand { get; }

        #endregion

        public override bool IsTarget => this.Model.UserConstructorInspector?.Id == GlobalAppProperties.User.Id;

        #region ctors

        public TaskViewModelInspector(IUnityContainer container, Guid priceEngineeringTaskId)
            : base(container, priceEngineeringTaskId)
        {
            //Вложенные дочерние задачи
            var vms = Model.ChildPriceEngineeringTasks.Select(engineeringTask => new TaskViewModelInspector(container, engineeringTask.Id));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(vms);

            #region Commands

            AcceptPriceEngineeringTaskCommand = new DoStepCommandAcceptByHead(this, container);
            RejectPriceEngineeringTaskCommand = new DoStepCommandRejectByHeadToConstructor(this, container);

            #endregion
        }

        #endregion
    }
}