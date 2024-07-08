using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.ForManager;
using Prism.Mvvm;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public abstract class TaskInvoiceForPaymentViewModelBase<TTask, TItem> : BindableBase
        where TItem : TaskInvoiceForPaymentItemViewModelBase
        where TTask : TaskInvoiceForPaymentWrapperBase<TItem>
    {
        protected readonly IUnitOfWork UnitOfWork;
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

        protected TaskInvoiceForPaymentViewModelBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Загрузка ранее созданного задания
        /// </summary>
        /// <param name="task"></param>
        public void Load(TaskInvoiceForPayment task)
        {
            var targetTask = UnitOfWork.Repository<TaskInvoiceForPayment>().GetById(task.Id);
            Task = this.GetTask(targetTask, UnitOfWork);
        }

        protected abstract TTask GetTask(TaskInvoiceForPayment taskInvoice, IUnitOfWork unitOfWork);
    }
}