using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentViewModelManager : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskInvoiceForPaymentWrapperManager Task { get; private set; }

        public TaskInvoiceForPaymentViewModelManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Загрузка ранее созданного задания
        /// </summary>
        /// <param name="task"></param>
        public void Load(TaskInvoiceForPayment task)
        {
            var targetTask = _unitOfWork.Repository<TaskInvoiceForPayment>().GetById(task.Id);
            Task = new TaskInvoiceForPaymentWrapperManager(targetTask, _unitOfWork);
            RaisePropertyChanged(nameof(Task));
        }
    }
}