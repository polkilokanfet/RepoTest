using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public abstract class TaskInvoiceForPaymentViewModelBase<TTask, TItem> : ViewModelBase
        where TItem : TaskInvoiceForPaymentItemWrapperBase
        where TTask : TaskInvoiceForPaymentWrapperBase<TItem>
    {
        private TTask _task;
        private TItem _selectedItem;

        public TTask Task
        {
            get => _task;
            protected set
            {
                _task = value;
                foreach (var item in _task.Items) item.SetTceNumber(this.UnitOfWork);
                RaisePropertyChanged();
                OpenSpecificationCommand = new OpenSpecificationScanCommand(Task.Specification, Container.Resolve<IFilesStorageService>(), Container.Resolve<IMessageService>());
            }
        }

        public TItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                AfterSelectionItem();
            }
        }

        public ICommand OpenSpecificationCommand { get; private set; }

        protected virtual void AfterSelectionItem() { }

        public bool IsStarted => Task?.Model.MomentStart != null;
        public bool IsFinished => Task?.Model.MomentFinish != null;

        protected TaskInvoiceForPaymentViewModelBase(IUnityContainer container) : base(container)
        {
        }

        /// <summary>
        /// Загрузка ранее созданного задания
        /// </summary>
        /// <param name="task"></param>
        public virtual void Load(TaskInvoiceForPayment task)
        {
            var targetTask = UnitOfWork.Repository<TaskInvoiceForPayment>().GetById(task.Id);
            Task = this.GetTask(targetTask);
        }

        protected abstract TTask GetTask(TaskInvoiceForPayment taskInvoice);

        protected void SendNotifications()
        {
            foreach (var notificationUnit in this.GetNotificationUnits())
            {
                this.Container.Resolve<IEventAggregator>().GetEvent<NotificationEvent>().Publish(notificationUnit);
            }
        }

        protected abstract IEnumerable<NotificationUnit> GetNotificationUnits();
    }
}