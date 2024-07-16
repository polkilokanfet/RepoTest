using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

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
                RaisePropertyChanged();
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
        public void Load(TaskInvoiceForPayment task)
        {
            var targetTask = UnitOfWork.Repository<TaskInvoiceForPayment>().GetById(task.Id);
            Task = this.GetTask(targetTask);
        }

        protected abstract TTask GetTask(TaskInvoiceForPayment taskInvoice);
    }
}