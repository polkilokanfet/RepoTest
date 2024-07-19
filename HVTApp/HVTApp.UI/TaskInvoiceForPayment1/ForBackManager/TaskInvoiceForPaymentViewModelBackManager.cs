using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.TaskInvoiceForPayment1.Base;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class TaskInvoiceForPaymentViewModelBackManager :
        TaskInvoiceForPaymentViewModelBase<TaskInvoiceForPaymentWrapperBackManager, TaskInvoiceForPaymentItemWrapperBackManager>
    {
        public ICommand FinishCommand { get; }

        public TaskInvoiceForPaymentViewModelBackManager(IUnityContainer container) : base(container)
        {
            FinishCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите завершить задачу?",
                () =>
                {
                    this.Task.MomentFinish = DateTime.Now;
                    this.Task.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    ((DelegateLogConfirmationCommand) FinishCommand).RaiseCanExecuteChanged();
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

            //назначение з/з, если их нет
            foreach (var item in Task.Items)
            {
                int position = 1;
                foreach (var salesUnit in item.SalesUnits)
                {
                    if (salesUnit.Order == null)
                    {
                        salesUnit.Order = new OrderWrapperTip(new Order(), true);
                        salesUnit.OrderPosition = position.ToString();
                        position++;
                    }
                }
            }

            this.Task.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(this.Task.IsValid))
                    ((DelegateLogConfirmationCommand) FinishCommand).RaiseCanExecuteChanged();
            };
        }
    }
}