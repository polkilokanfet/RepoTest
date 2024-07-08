using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentWrapperManager : TaskInvoiceForPaymentWrapperBase<TaskInvoiceForPaymentItemViewModelManager>
    {
        #region SimpleProperties

        /// <summary>
        /// Старт задачи
        /// </summary>
        public DateTime MomentStart
        {
            get => Model.MomentStart;
            set => SetValue(value);
        }
        public DateTime MomentStartOriginalValue => GetOriginalValue<DateTime>(nameof(MomentStart));
        public bool MomentStartIsChanged => GetIsChanged(nameof(MomentStart));

        /// <summary>
        /// Требуется оригинал счёта
        /// </summary>
        public bool OriginalIsRequired
        {
            get => Model.OriginalIsRequired;
            set => SetValue(value);
        }
        public bool OriginalIsRequiredOriginalValue => GetOriginalValue<bool>(nameof(OriginalIsRequired));
        public bool OriginalIsRequiredIsChanged => GetIsChanged(nameof(OriginalIsRequired));

        /// <summary>
        /// Комментарий менеджера
        /// </summary>
        public string Comment
        {
            get => Model.Comment;
            set => SetValue(value);
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        #endregion

        public TaskInvoiceForPaymentWrapperManager(TaskInvoiceForPayment model, IUnitOfWork unitOfWork) : base(model, unitOfWork)
        {
        }

        protected override TaskInvoiceForPaymentItemViewModelManager GetItem(TaskInvoiceForPaymentItem item)
        {
            return new TaskInvoiceForPaymentItemViewModelManager(item, UnitOfWork);
        }
    }
}
