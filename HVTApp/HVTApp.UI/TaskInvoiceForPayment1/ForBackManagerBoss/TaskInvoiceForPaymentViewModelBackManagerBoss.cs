using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
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
                    this.UnitOfWork.SaveChanges();

                    RaisePropertyChanged(nameof(Task.BackManager));
                    container.Resolve<IEventAggregator>().GetEvent<AfterSaveTaskInvoiceForPaymentEvent>().Publish(this.Task.Model);
                },
                () => this.Task != null && this.IsStarted && this.IsFinished == false);

        }

        protected override TaskInvoiceForPaymentWrapperBackManagerBoss GetTask(TaskInvoiceForPayment taskInvoice)
        {
            return new TaskInvoiceForPaymentWrapperBackManagerBoss(taskInvoice);
        }
    }
}