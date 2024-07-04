using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentViewModelManager : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private TaskInvoiceForPaymentWrapperManager _task;

        public TaskInvoiceForPaymentWrapperManager Task
        {
            get => _task;
            private set
            {
                _task = value;
                RaisePropertyChanged();
            }
        }

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
        }

        public void Load(Specification specification)
        {
            Task = new TaskInvoiceForPaymentWrapperManager(new TaskInvoiceForPayment(), _unitOfWork);
            foreach (var priceEngineeringTask in specification.PriceEngineeringTasks)
            {
                var item = new TaskInvoiceForPaymentItemViewModelManager(new TaskInvoiceForPaymentItem(), _unitOfWork);
                item.Model.PriceEngineeringTask = _unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTask.Id);
                Task.Items.Add(new TaskInvoiceForPaymentItemViewModelManager(new TaskInvoiceForPaymentItem(), _unitOfWork));
            }
        }
    }
}