using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public abstract class TaskInvoiceForPaymentWrapperBase<TItem> : WrapperBase<TaskInvoiceForPayment>
        where TItem : TaskInvoiceForPaymentItemViewModelBase
    {
        protected readonly IUnitOfWork UnitOfWork;

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

        protected TaskInvoiceForPaymentWrapperBase(TaskInvoiceForPayment model, IUnitOfWork unitOfWork) : base(model)
        {
            UnitOfWork = unitOfWork;
        }
    }
}