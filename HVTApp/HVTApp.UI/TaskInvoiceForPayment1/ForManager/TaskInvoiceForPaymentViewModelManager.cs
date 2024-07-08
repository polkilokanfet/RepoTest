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

        public TaskInvoiceForPaymentViewModelManager(IUnitOfWork unitOfWork, IUnityContainer container) : base(unitOfWork)
        {
            RemoveItemCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите удалить выделенную строку из счёта?",
                () => this.Task.Items.Remove(SelectedItem),
                () => this.SelectedItem != null);
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
    }
}