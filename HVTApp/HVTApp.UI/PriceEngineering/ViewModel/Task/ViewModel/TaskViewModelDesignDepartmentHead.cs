using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
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
        /// �������� ���������� ������
        /// </summary>
        public DelegateLogCommand InstructPriceEngineeringTaskCommand { get; }

        /// <summary>
        /// �������� �������� ������
        /// </summary>
        public DelegateLogCommand InstructInspectorCommand { get; }

        /// <summary>
        /// ������� ����������
        /// </summary>
        public ICommandRaiseCanExecuteChanged AcceptPriceEngineeringTaskCommand { get; }

        /// <summary>
        /// ��������� ���������� ������������
        /// </summary>
        public ICommandRaiseCanExecuteChanged RejectPriceEngineeringTaskCommand { get; }

        /// <summary>
        /// ��������� ���������� ���������
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
            !Status.Equals(ScriptStep.VerificationAccept) &&
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
            //��������� �������� ������
            var vms = Model.ChildPriceEngineeringTasks.Select(engineeringTask => new TaskViewModelDesignDepartmentHead(container, engineeringTask.Id));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(vms);

            #region Commands

            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var user = Container.Resolve<ISelectService>().SelectItem(DesignDepartment.Staff.Where(x => x.IsActual));
                    if (user == null) return;

                    var needVerification = Container.Resolve<IMessageService>().ConfirmationDialog("��������", "������ ��������� ���������� ����������?", defaultNo: true);

                    this.Instruct(user, needVerification);
                },
                () => AllowInstruction);

            InstructInspectorCommand = new DelegateLogCommand(
                () =>
                {
                    var users = DesignDepartment.Staff
                        .Where(user1 => user1.IsActual)
                        .Except(new[] {this.Model.UserConstructor});
                    var user = Container.Resolve<ISelectService>().SelectItem(users);
                    if (user == null) return;
                    this.UserConstructorInspector = new UserEmptyWrapper(user);
                    this.SaveCommand.Execute();

                    Container.Resolve<IEventAggregator>().GetEvent<NotificationEvent>().Publish(new NotificationUnit
                    {
                        ActionType = NotificationActionType.PriceEngineeringTaskInstructInspector,
                        RecipientRole = Role.Constructor,
                        RecipientUser = Model.UserConstructorInspector,
                        TargetEntityId = Model.Id
                    });

                    Messenger.SendMessage($"�������� �����������: {user}", true);
                }, 
                () => 
                    this.IsTarget && 
                    this.Status.Equals(ScriptStep.VerificationRequestByConstructor) && this.Model.VerificationIsRequested);

            AcceptPriceEngineeringTaskCommand = new DoStepCommandAcceptByHead(this, container);
            RejectPriceEngineeringTaskCommand = new DoStepCommandRejectVerificationByDesignDepartmentHead(this, container);
            RejectPriceEngineeringTaskCommandToManager = new DoStepCommandRejectByHeadToManager(this, container);

            #endregion
        }

        #endregion

        public void Instruct(User user, bool needVerification)
        {
            if (user == null || AllowInstruction == false)
                return;

            var sb = new StringBuilder($"�������� �����������: {user.Employee.Person}.");
            if (needVerification)
                sb.Append("\n������ ����������� ��������� ��������� ������ �����������.");

            this.RequestForVerificationFromHead = needVerification;
            this.UserConstructor = new UserEmptyWrapper(this.UnitOfWork.Repository<User>().GetById(user.Id));
            Messenger.SendMessage(sb.ToString(), false);
            this.SaveCommand.Execute();

            var notificationUnit = new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskInstructToConstructor,
                RecipientRole = Role.Constructor,
                RecipientUser = UserConstructor.Model,
                SenderRole = GlobalAppProperties.User.RoleCurrent,
                SenderUser = GlobalAppProperties.User,
                TargetEntityId = Model.Id
            };

            Container.Resolve<IEventAggregator>().GetEvent<NotificationEvent>().Publish(notificationUnit);
        }

        /// <summary>
        /// ��� ������, ���������� ������� ����� �������� ������������
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