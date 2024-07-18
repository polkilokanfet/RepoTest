using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public abstract class TaskInvoiceForPaymentWrapperBase<TItem> : WrapperBase<TaskInvoiceForPayment>
        where TItem : TaskInvoiceForPaymentItemWrapperBase
    {
        #region Items

        /// <summary>
        /// Строки счёта
        /// </summary>
        public IValidatableChangeTrackingCollection<TItem> Items { get; protected set; }

        protected override void InitializeCollectionProperties()
        {
            if (Model.Items == null) throw new ArgumentException("Items cannot be null");
            Items = new ValidatableChangeTrackingCollection<TItem>(Model.Items.Select(this.GetItem));
            RegisterCollection(Items, Model.Items);
        }

        protected abstract TItem GetItem(TaskInvoiceForPaymentItem item);

        #endregion

        public Specification Specification
        {
            get
            {
                if (Items == null || Items.Any() == false) return null;

                return Items.First().Model.PriceEngineeringTask != null 
                    ? Items.First().Model.PriceEngineeringTask.SalesUnits.First().Specification 
                    : Items.First().Model.TechnicalRequrements.SalesUnits.First().Specification;
            }
        }

        public string ErrorsString
        {
            get
            {
                var sb = new StringBuilder();

                var taskErrors = this.Errors.ActualErrors.Select(dataErrorInfo => dataErrorInfo.Message).ToList();
                if (taskErrors.Any())
                {
                    sb.AppendLine("Ошибки в задаче:");
                    foreach (var taskError in taskErrors)
                    {
                        sb.AppendLine($" - {taskError};");
                    }
                }

                foreach (var item in this.Items)
                {
                    var itemErrors = item.GetErrorsAll().Select(dataErrorInfo => dataErrorInfo.Message).ToList();
                    if (itemErrors.Any())
                    {
                        sb.AppendLine("Ошибки в строке счёта:");
                        foreach (var itemError in itemErrors)
                        {
                            sb.AppendLine($" - {itemError};");
                        }
                    }

                }
                return sb.ToString();
            }
        }

        public string PaymentConditionString => Model.Items.FirstOrDefault()?.PaymentCondition?.ToString();

        protected TaskInvoiceForPaymentWrapperBase(TaskInvoiceForPayment model) : base(model)
        {
            this.ErrorsChanged += (sender, args) => 
            {
                RaisePropertyChanged(nameof(ErrorsString));
            };
        }
    }
}