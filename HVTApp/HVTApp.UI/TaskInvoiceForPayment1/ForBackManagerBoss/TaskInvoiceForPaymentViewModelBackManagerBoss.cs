using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.TaskInvoiceForPayment1.Base;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss
{
    public class TaskInvoiceForPaymentViewModelBackManagerBoss :
        TaskInvoiceForPaymentViewModelBase<TaskInvoiceForPaymentWrapperBackManagerBoss, TaskInvoiceForPaymentItemWrapperBackManagerBoss>
    {
        public ICommand SelectPlanMakerCommand { get; }
        public ICommand SelectBackManagerCommand { get; }
        public ICommand InstructCommand { get; }

        public bool PlanMakerIsRequired => Task != null && (Task.Model.PlanMakerIsRequired || Task.Model.MomentFinishByPlanMaker.HasValue);

        public TaskInvoiceForPaymentViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
            SelectBackManagerCommand = new DelegateLogCommand(
                () =>
                {
                    var users = UnitOfWork.Repository<User>().Find(user1 =>
                        user1.IsActual &&
                        user1.Roles.Select(role => role.Role).Contains(Role.BackManager));

                    var user = container.Resolve<ISelectService>().SelectItem(users);
                    if (user == null) return;

                    this.Task.BackManager = new UserEmptyWrapper(user);

                    RaisePropertyChanged(nameof(Task.BackManager));
                    ((DelegateLogCommand)InstructCommand).RaiseCanExecuteChanged();
                },
                () => this.Task != null && this.IsStarted && this.IsFinished == false);

            SelectPlanMakerCommand = new DelegateLogCommand(
                () =>
                {
                    var users = UnitOfWork.Repository<User>().Find(user1 =>
                        user1.IsActual &&
                        user1.Roles.Select(role => role.Role).Contains(Role.PlanMaker));

                    var user = container.Resolve<ISelectService>().SelectItem(users);
                    if (user == null) return;

                    this.Task.PlanMaker = new UserEmptyWrapper(user);

                    RaisePropertyChanged(nameof(Task.PlanMaker));
                    ((DelegateLogCommand)InstructCommand).RaiseCanExecuteChanged();
                },
                () => this.Task != null && this.Task.Model.PlanMakerIsRequired && this.IsStarted && this.IsFinished == false);

            InstructCommand = new DelegateLogCommand(
                () =>
                {
                    this.Task.AcceptChanges();
                    this.UnitOfWork.SaveChanges();

                    container.Resolve<IEventAggregator>().GetEvent<AfterSaveTaskInvoiceForPaymentEvent>().Publish(this.Task.Model);
                    SendNotifications();
                    ((DelegateLogCommand)InstructCommand).RaiseCanExecuteChanged();
                },
                () => this.Task != null && this.IsStarted && this.IsFinished == false && Task.IsValid && Task.IsChanged);

        }

        protected override TaskInvoiceForPaymentWrapperBackManagerBoss GetTask(TaskInvoiceForPayment taskInvoice)
        {
            return new TaskInvoiceForPaymentWrapperBackManagerBoss(taskInvoice);
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            if (Task.PlanMaker == null)
            {
                yield return new NotificationUnit
                {
                    TargetEntityId = this.Task.Model.Id,
                    RecipientUser = this.Task.Model.BackManager,
                    RecipientRole = Role.BackManager,
                    ActionType = NotificationActionType.TaskInvoiceForPaymentInstruct
                };
            }
            else
            {
                yield return new NotificationUnit
                {
                    TargetEntityId = this.Task.Model.Id,
                    RecipientUser = this.Task.Model.PlanMaker,
                    RecipientRole = Role.PlanMaker,
                    ActionType = NotificationActionType.TaskInvoiceForPaymentInstruct
                };
            }
        }

        public override void Load(TaskInvoiceForPayment task)
        {
            base.Load(task);
            RaisePropertyChanged(nameof(PlanMakerIsRequired));
        }
    }
}