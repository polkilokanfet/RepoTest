using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.TaskInvoiceForPayment1.Base;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentViewModelManager :
        TaskInvoiceForPaymentViewModelBase<TaskInvoiceForPaymentWrapperManager, TaskInvoiceForPaymentItemViewModelManager>
    {
        public ICommand RemoveItemCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        public TaskInvoiceForPaymentViewModelManager(IUnityContainer container) : base(container)
        {
            RemoveItemCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Удалить выделенную строку из счёта?",
                () => this.Task.Items.Remove(SelectedItem),
                () => this.SelectedItem != null);

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
                },
                () => this.Task != null && this.Task.MomentStart == null && this.Task.IsValid);

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
                () => Task?.MomentStart != null && Task.Model.MomentFinish == null && this.Task.IsValid);

            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(Task)) return;
                this.Task.PropertyChanged += (o, eventArgs) => RaiseCanExecuteChangedCommands();
            };
        }

        protected override TaskInvoiceForPaymentWrapperManager GetTask(TaskInvoiceForPayment taskInvoice, IUnitOfWork unitOfWork)
        {
            return new TaskInvoiceForPaymentWrapperManager(taskInvoice, UnitOfWork);
        }

        public void Load(Specification specification)
        {
            Task = new TaskInvoiceForPaymentWrapperManager(new TaskInvoiceForPayment(), UnitOfWork);
            foreach (var priceEngineeringTask in specification.PriceEngineeringTasks)
            {
                var taskInvoiceForPaymentItem = new TaskInvoiceForPaymentItem
                {
                    PriceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTask.Id)
                };
                var item = new TaskInvoiceForPaymentItemViewModelManager(taskInvoiceForPaymentItem, UnitOfWork);
                Task.Items.Add(item);
            }
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