using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
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
        public DelegateLogCommand InstructPriceEngineeringTaskCommand { get; private set; }

        /// <summary>
        /// Принять проработку
        /// </summary>
        public DelegateLogConfirmationCommand AcceptPriceEngineeringTaskCommand { get; private set; }

        /// <summary>
        /// Отклонить проработку
        /// </summary>
        public DelegateLogConfirmationCommand RejectPriceEngineeringTaskCommand { get; private set; }

        #endregion

        public override bool IsTarget => 
            DesignDepartment != null && 
            DesignDepartment.Head.Id == GlobalAppProperties.User.Id;

        public bool AllowInstruction =>
            IsTarget &&
            !Status.Equals(ScriptStep2.FinishedByConstructor) &&
            !Status.Equals(ScriptStep2.VerificationAcceptedByHead) &&
            !Status.Equals(ScriptStep2.VerificationRequestedByConstructor) &&
            !Status.Equals(ScriptStep2.Created) &&
            !Status.Equals(ScriptStep2.Stopped) &&
            !Status.Equals(ScriptStep2.Accepted);

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

                    var needVerification = MessageDialogResult.Yes == Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Проверка", "Хотите проверить результаты проработки?", defaultNo: true);

                    this.Instruct(user, needVerification);
                },
                () => AllowInstruction);

            AcceptPriceEngineeringTaskCommand = new DelegateLogConfirmationCommand(
                Container.Resolve<IMessageService>(),
                "Вы уверены, что хотите принять результаты проработки?",
                () =>
                {
                    this.Statuses.Add(ScriptStep2.VerificationAcceptedByHead);
                    this.Statuses.Add(ScriptStep2.FinishedByConstructor);
                    this.SaveCommand_ExecuteMethod();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskVerificationAcceptedByHeadEvent>().Publish(this.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(this.Model);
                },
                () => ScriptStep2.VerificationAcceptedByHead.AllowDoStep(this.Status));

            RejectPriceEngineeringTaskCommand = new DelegateLogConfirmationCommand(
                Container.Resolve<IMessageService>(),
                "Вы уверены, что хотите отправить задачу на доработку исполнителю?",
                () =>
                {
                    this.Statuses.Add(ScriptStep2.VerificationRejectedByHead);
                    this.SaveCommand_ExecuteMethod();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskVerificationRejectedByHeadEvent>().Publish(this.Model);
                },
                () => ScriptStep2.VerificationRejectedByHead.AllowDoStep(this.Status));

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
            this.SaveCommand_ExecuteMethod();
            Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskInstructedEvent>().Publish(this.Model);
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