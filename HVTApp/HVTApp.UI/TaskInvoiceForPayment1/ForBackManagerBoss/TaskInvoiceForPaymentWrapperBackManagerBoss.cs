using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss
{
    public class TaskInvoiceForPaymentWrapperBackManagerBoss : TaskInvoiceForPaymentWrapperBase<TaskInvoiceForPaymentItemWrapperBackManagerBoss>
    {
        /// <summary>
        /// Исполнитель
        /// </summary>
        public UserEmptyWrapper PlanMaker
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(PlanMaker, value);
        }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public UserEmptyWrapper BackManager
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(BackManager, value);
        }

        public TaskInvoiceForPaymentWrapperBackManagerBoss(TaskInvoiceForPayment model) : base(model)
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(PlanMaker), Model.BackManager == null ? null : new UserEmptyWrapper(Model.PlanMaker));
            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserEmptyWrapper(Model.BackManager));
        }


        protected override TaskInvoiceForPaymentItemWrapperBackManagerBoss GetItem(TaskInvoiceForPaymentItem item)
        {
            return new TaskInvoiceForPaymentItemWrapperBackManagerBoss(item);
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (this.PlanMaker == null && Model.PlanMakerIsRequired)
                yield return new ValidationResult("Назначьте исполнителя - плановика.", new[] { nameof(PlanMaker) });

            if (this.BackManager == null)
                yield return new ValidationResult("Назначьте исполнителя - экономиста.", new[] { nameof(BackManager) });
        }
    }
}