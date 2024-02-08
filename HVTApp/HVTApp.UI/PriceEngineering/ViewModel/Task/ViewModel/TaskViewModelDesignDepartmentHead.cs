using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelDesignDepartmentHead : TaskViewModelBaseDesignDepartmentHead
    {
        #region Commands

        /// <summary>
        /// Поручить проработку задачи
        /// </summary>
        public DelegateLogCommand InstructPriceEngineeringTaskCommand { get; }

        /// <summary>
        /// Принять проработку
        /// </summary>
        public ICommandRaiseCanExecuteChanged AcceptPriceEngineeringTaskCommand { get; }

        /// <summary>
        /// Отклонить проработку конструктору
        /// </summary>
        public ICommandRaiseCanExecuteChanged RejectPriceEngineeringTaskCommand { get; }

        /// <summary>
        /// Отклонить проработку менеджеру
        /// </summary>
        public ICommandRaiseCanExecuteChanged RejectPriceEngineeringTaskCommandToManager { get; }

        #endregion

        public override bool IsTarget => 
            DesignDepartment != null && 
            DesignDepartment.Head.Id == GlobalAppProperties.User.Id;

        public bool AllowInstruction =>
            IsTarget &&
            !Status.Equals(ScriptStep.RejectByHead) &&
            !Status.Equals(ScriptStep.FinishByConstructor) &&
            !Status.Equals(ScriptStep.VerificationAcceptByHead) &&
            !Status.Equals(ScriptStep.VerificationRequestByConstructor) &&
            !Status.Equals(ScriptStep.Create) &&
            !Status.Equals(ScriptStep.Stop) &&
            !Status.Equals(ScriptStep.LoadToTceStart) &&
            !Status.Equals(ScriptStep.LoadToTceFinish) &&
            !Status.Equals(ScriptStep.ProductionRequestStart) &&
            !Status.Equals(ScriptStep.ProductionRequestFinish) &&
            !Status.Equals(ScriptStep.Accept);

        #region ctors

        public TaskViewModelDesignDepartmentHead(IUnityContainer container, Guid priceEngineeringTaskId) 
            : base(container, priceEngineeringTaskId)
        {
            //Вложенные дочерние задачи
            var vms = Model.ChildPriceEngineeringTasks.Select(engineeringTask => new TaskViewModelDesignDepartmentHead(container, engineeringTask.Id));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(vms);

            #region Commands

            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var user = Container.Resolve<ISelectService>().SelectItem(DesignDepartment.Staff);
                    if (user == null) return;

                    var needVerification = Container.Resolve<IMessageService>().ConfirmationDialog("Проверка", "Хотите проверить результаты проработки?", defaultNo: true);

                    this.Instruct(user, needVerification);
                },
                () => AllowInstruction);

            AcceptPriceEngineeringTaskCommand = new DoStepCommandAcceptByHead(this, container);
            RejectPriceEngineeringTaskCommand = new DoStepCommandRejectByHeadToConstructor(this, container);
            RejectPriceEngineeringTaskCommandToManager = new DoStepCommandRejectByHeadToManager(this, container);

            #endregion
        }

        #endregion

        public void Instruct(User user, bool needVerification)
        {
            if (user == null || AllowInstruction == false)
                return;

            var sb = new StringBuilder($"Назначен исполнитель: {user.Employee.Person}.");
            if (needVerification)
                sb.Append("\nСчитаю необходимым проверить результат работы исполнителя.");

            this.RequestForVerificationFromHead = needVerification;
            this.UserConstructor = new UserEmptyWrapper(this.UnitOfWork.Repository<User>().GetById(user.Id));
            Messenger.SendMessage(sb.ToString());
            this.SaveCommand.Execute();

            var notificationUnit = new NotificationUnit
            {
                ActionType = EventServiceActionType.PriceEngineeringTaskInstructToConstructor,
                RecipientRole = Role.Constructor,
                RecipientUser = UserConstructor.Model,
                SenderRole = GlobalAppProperties.User.RoleCurrent,
                SenderUser = GlobalAppProperties.User,
                TargetEntityId = Model.Id
            };

            Container.Resolve<IEventAggregator>().GetEvent<NotificationEvent>().Publish(notificationUnit);
        }

        /// <summary>
        /// Все задачи, проработку которых может поручить пользователь
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TaskViewModelDesignDepartmentHead> GetSuitableTasksForInstruct()
        {
            if (this.IsTarget && this.AllowInstruction)
                yield return this;

            foreach (var child in this.ChildPriceEngineeringTasks.Cast<TaskViewModelDesignDepartmentHead>())
            {
                foreach (var child2 in child.GetSuitableTasksForInstruct())
                {
                    yield return child2;
                }
            }
        }
    }
}