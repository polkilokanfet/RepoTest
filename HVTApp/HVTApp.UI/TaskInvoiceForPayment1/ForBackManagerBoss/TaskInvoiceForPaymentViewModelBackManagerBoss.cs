using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.TaskInvoiceForPayment1.Base;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss
{
    public class TaskInvoiceForPaymentViewModelBackManagerBoss :
        TaskInvoiceForPaymentViewModelBase<TaskInvoiceForPaymentWrapperBackManagerBoss, TaskInvoiceForPaymentItemViewModelBackManagerBoss>
    {
        public ICommand InstructCommand { get; }

        public TaskInvoiceForPaymentViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
            InstructCommand = new DelegateLogCommand(
                () =>
                {
                    var users = UnitOfWork.Repository<User>().Find(user =>
                        user.IsActual &&
                        user.Roles.Select(role => role.Role).Contains(Role.BackManager));

                    var backManager = container.Resolve<ISelectService>().SelectItem(users);
                    if (backManager == null) return;

                    this.Task.BackManager = new UserEmptyWrapper(backManager);
                    this.Task.AcceptChanges();
                    this.UnitOfWork.SaveEntity(this.Task.Model);
                },
                () => this.Task != null);

        }

        protected override TaskInvoiceForPaymentWrapperBackManagerBoss GetTask(TaskInvoiceForPayment taskInvoice, IUnitOfWork unitOfWork)
        {
            return new TaskInvoiceForPaymentWrapperBackManagerBoss(taskInvoice, unitOfWork);
        }
    }
}