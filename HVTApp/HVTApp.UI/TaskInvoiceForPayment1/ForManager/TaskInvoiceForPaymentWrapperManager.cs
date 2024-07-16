using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentWrapperManager : TaskInvoiceForPaymentWrapperBase<TaskInvoiceForPaymentItemWrapperManager>
    {
        #region SimpleProperties

        /// <summary>
        /// Старт задачи
        /// </summary>
        public DateTime? MomentStart
        {
            get => Model.MomentStart;
            set => SetValue(value);
        }
        public DateTime? MomentStartOriginalValue => GetOriginalValue<DateTime?>(nameof(MomentStart));
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

        public PaymentCondition PaymentCondition
        {
            get => Model.Items.FirstOrDefault()?.PaymentCondition;
            set
            {
                foreach (var item in this.Items)
                {
                    if (item.PaymentCondition?.Model.Id == value.Id) continue;
                    item.PaymentCondition = new PaymentConditionEmptyWrapper(value);
                }
                RaisePropertyChanged();
            }
        }

        public TaskInvoiceForPaymentWrapperManager(TaskInvoiceForPayment model) : base(model)
        {
        }

        protected override TaskInvoiceForPaymentItemWrapperManager GetItem(TaskInvoiceForPaymentItem item)
        {
            return new TaskInvoiceForPaymentItemWrapperManager(item);
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (this.PaymentCondition == null)
                yield return new ValidationResult("Не выбрано условие платежа.", new[] {nameof(PaymentCondition)});
        }
    }
}
