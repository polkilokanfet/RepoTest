using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.TaskInvoiceForPayment1.Base;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentViewModelBackManager :
        TaskInvoiceForPaymentViewModelBase<TaskInvoiceForPaymentWrapperBackManager, TaskInvoiceForPaymentItemWrapperBackManager>
    {
        public ICommand LoadInvoiceForPaymentCommand { get; private set; }
        public ICommand FinishCommand { get; }

        public TaskInvoiceForPaymentViewModelBackManager(IUnityContainer container) : base(container)
        {
            FinishCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите завершить задачу?",
                () =>
                {
                    if (((OpenInvoiceForPaymentCommand)this.OpenInvoiceForPaymentCommand).ContainsInStorage() == false)
                    {
                        container.Resolve<IMessageService>().Message("Уведомление", "Загрузите сначала скан счёта.");
                        return;
                    }
                    this.Task.MomentFinish = DateTime.Now;
                    this.Task.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    ((DelegateLogConfirmationCommand) FinishCommand).RaiseCanExecuteChanged();
                    ((LoadInvoiceForPaymentCommand)LoadInvoiceForPaymentCommand).RaiseCanExecuteChanged();
                    SendNotifications();
                },
                () => 
                    this.Task != null && this.IsStarted == true && this.IsFinished == false && this.Task.IsValid);
        }

        protected override TaskInvoiceForPaymentWrapperBackManager GetTask(TaskInvoiceForPayment taskInvoice)
        {
            return new TaskInvoiceForPaymentWrapperBackManager(taskInvoice);
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                TargetEntityId = this.Task.Model.Id,
                RecipientUser = this.Task.Model.Items.First().SalesUnits.First().Project.Manager,
                RecipientRole = Role.SalesManager,
                ActionType = NotificationActionType.TaskInvoiceForPaymentFinish
            };
        }

        public override void Load(TaskInvoiceForPayment task)
        {
            base.Load(task);

            this.Task.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.Task.IsValid))
                    ((DelegateLogConfirmationCommand) FinishCommand).RaiseCanExecuteChanged();
            };

            LoadInvoiceForPaymentCommand = new LoadInvoiceForPaymentCommand(
                this.Task.Model,
                this.Container.Resolve<IFilesStorageService>(),
                this.Container.Resolve<IMessageService>(),
                () => this.IsStarted == true && IsFinished == false);
        }
    }
}