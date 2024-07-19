using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.TaskInvoiceForPayment1.Base;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentViewModelManager :
        TaskInvoiceForPaymentViewModelBase<TaskInvoiceForPaymentWrapperManager, TaskInvoiceForPaymentItemWrapperManager>
    {
        public ICommand RemoveItemCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ChoosePaymentConditionCommand { get; }

        public TaskInvoiceForPaymentViewModelManager(IUnityContainer container) : base(container)
        {
            RemoveItemCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Удалить выделенную строку из счёта?",
                () => this.Task.Items.Remove(SelectedItem),
                () => this.SelectedItem != null && this.IsStarted == false);

            StartCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Стартовать задание?",
                () =>
                {
                    this.Task.MomentStart = DateTime.Now;
                    this.Task.AcceptChanges();
                    this.UnitOfWork.SaveEntity(this.Task.Model);
                    RaiseCanExecuteChangedCommands();
                    RaisePropertyChanged(nameof(IsStarted));
                    SendNotifications();
                },
                () => this.Task != null && this.IsStarted == false && this.Task.IsValid);

            StopCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Остановить задание?",
                () =>
                {
                    this.Task.MomentStart = null;
                    this.Task.AcceptChanges();
                    this.UnitOfWork.SaveEntity(this.Task.Model);
                    RaiseCanExecuteChangedCommands();
                    RaisePropertyChanged(nameof(IsStarted));
                },
                () => this.IsStarted && this.IsFinished == false && this.Task.IsValid);

            ChoosePaymentConditionCommand = new DelegateLogCommand(
                () =>
                {
                    var conditions = this.Task.Model.Items
                        .SelectMany(item => item.SalesUnits)
                        .SelectMany(salesUnit => salesUnit.PaymentConditionSet.PaymentConditions)
                        .Distinct()
                        .OrderBy(paymentCondition => paymentCondition);

                    var condition = container.Resolve<ISelectService>().SelectItem(conditions);
                    if (condition == null) return;
                    this.Task.PaymentCondition = condition;
                },
                () => this.IsStarted == false);

            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Task)) return;
                this.Task.PropertyChanged += (o, eventArgs) => RaiseCanExecuteChangedCommands();
            };
        }

        protected override TaskInvoiceForPaymentWrapperManager GetTask(TaskInvoiceForPayment taskInvoice)
        {
            return new TaskInvoiceForPaymentWrapperManager(taskInvoice);
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            var users = UnitOfWork.Repository<User>().Find(user => user.IsActual && user.Roles.Select(role => role.Role).Contains(Role.BackManagerBoss));
            foreach (var user in users)
            {
                yield return new NotificationUnit
                {
                    TargetEntityId = this.Task.Model.Id,
                    RecipientUser = user,
                    RecipientRole = Role.BackManagerBoss,
                    ActionType = NotificationActionType.TaskInvoiceForPaymentStart
                };
            }
        }

        public void Load(Specification specification)
        {
            var invoice = new TaskInvoiceForPayment();

            foreach (var priceEngineeringTask in specification.PriceEngineeringTasks)
            {
                var taskInvoiceForPaymentItem = new TaskInvoiceForPaymentItem();
                taskInvoiceForPaymentItem.PriceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTask.Id);
                invoice.Items.Add(taskInvoiceForPaymentItem);
            }

            foreach (var technicalRequrements in specification.TechnicalRequrements)
            {
                var taskInvoiceForPaymentItem = new TaskInvoiceForPaymentItem();
                taskInvoiceForPaymentItem.TechnicalRequrements = UnitOfWork.Repository<TechnicalRequrements>().GetById(technicalRequrements.Id);
                invoice.Items.Add(taskInvoiceForPaymentItem);
            }

            Task = new TaskInvoiceForPaymentWrapperManager(invoice);
        }

        protected override void AfterSelectionItem()
        {
            ((DelegateLogConfirmationCommand)RemoveItemCommand).RaiseCanExecuteChanged();
        }

        private void RaiseCanExecuteChangedCommands()
        {
            ((DelegateLogConfirmationCommand)StartCommand).RaiseCanExecuteChanged();
            ((DelegateLogConfirmationCommand)StopCommand).RaiseCanExecuteChanged();
        }
    }
}