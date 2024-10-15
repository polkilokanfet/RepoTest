using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public class UpdateStructureCostNumberTaskViewModel : WrapperBase<UpdateStructureCostNumberTask>
    {
        public UpdateStructureCostNumberTaskViewModel(UpdateStructureCostNumberTask model) : base(model) { }

        public DateTime? MomentFinish
        {
            get => Model.MomentFinish;
            set => SetValue(value);
        }
        public DateTime? MomentFinishOriginalValue => GetOriginalValue<DateTime?>(nameof(MomentFinish));
        public bool MomentFinishIsChanged => GetIsChanged(nameof(MomentFinish));

        public bool? IsAccepted
        {
            get => Model.IsAccepted;
            set => SetValue(value);
        }
        public bool? IsAcceptedOriginalValue => GetOriginalValue<bool?>(nameof(IsAccepted));
        public bool IsAcceptedIsChanged => GetIsChanged(nameof(IsAccepted));
    }

    public abstract class TaskViewModelBaseDesignDepartmentHead : TaskViewModelBaseInspector
    {
        #region SimpleProperties

        #region RequestForVerificationFromHead

        /// <summary>
        /// Запрос на проверку от руководителя
        /// </summary>
        public bool RequestForVerificationFromHead
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool RequestForVerificationFromHeadOriginalValue => GetOriginalValue<bool>(nameof(RequestForVerificationFromHead));
        public bool RequestForVerificationFromHeadIsChanged => GetIsChanged(nameof(RequestForVerificationFromHead));

        #endregion

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Конструктор
        /// </summary>
        public new UserEmptyWrapper UserConstructor
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(UserConstructor, value);
        }

        #endregion

        public IValidatableChangeTrackingCollection<UpdateStructureCostNumberTaskViewModel> UpdateStructureCostNumberTasks { get; private set; }


        protected TaskViewModelBaseDesignDepartmentHead(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected TaskViewModelBaseDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        public override void InitializeComplexProperties()
        {
            base.InitializeComplexProperties();
            InitializeComplexProperty(nameof(UserConstructor), Model.UserConstructor == null ? null : new UserEmptyWrapper(Model.UserConstructor));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.UpdateStructureCostNumberTasks == null) throw new ArgumentException($"{nameof(Model.UpdateStructureCostNumberTasks)} cannot be null");
            UpdateStructureCostNumberTasks = new ValidatableChangeTrackingCollection<UpdateStructureCostNumberTaskViewModel>(Model.UpdateStructureCostNumberTasks.Select(e => new UpdateStructureCostNumberTaskViewModel(e)));
            RegisterCollection(UpdateStructureCostNumberTasks, Model.UpdateStructureCostNumberTasks);
        }
    }
}