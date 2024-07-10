using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss
{
    public class TaskInvoiceForPaymentWrapperBackManagerBoss : TaskInvoiceForPaymentWrapperBase<TaskInvoiceForPaymentItemViewModelBackManagerBoss>
    {
        /// <summary>
        /// Исполнитель
        /// </summary>
        public UserEmptyWrapper BackManager
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(BackManager, value);
        }

        public TaskInvoiceForPaymentWrapperBackManagerBoss(TaskInvoiceForPayment model, IUnitOfWork unitOfWork) : base(model, unitOfWork)
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserEmptyWrapper(Model.BackManager));
        }


        protected override TaskInvoiceForPaymentItemViewModelBackManagerBoss GetItem(TaskInvoiceForPaymentItem item)
        {
            return new TaskInvoiceForPaymentItemViewModelBackManagerBoss(item, UnitOfWork);
        }
    }
}