using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForManager
{
    public class TaskInvoiceForPaymentWrapperManager : WrapperBase<TaskInvoiceForPayment>
    {
        private readonly IUnitOfWork _unitOfWork;

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

        #region CollectionProperties

        /// <summary>
        /// Строки счёта
        /// </summary>
        public IValidatableChangeTrackingCollection<TaskInvoiceForPaymentItemViewModelManager> Items { get; private set; }

        #endregion

        public TaskInvoiceForPaymentWrapperManager(TaskInvoiceForPayment model, IUnitOfWork unitOfWork) : base(model)
        {
            _unitOfWork = unitOfWork;
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.Items == null) throw new ArgumentException("Items cannot be null");
            Items = new ValidatableChangeTrackingCollection<TaskInvoiceForPaymentItemViewModelManager>(Model.Items.Select(e => new TaskInvoiceForPaymentItemViewModelManager(e, _unitOfWork)));
            RegisterCollection(Items, Model.Items);
        }
    }
}
