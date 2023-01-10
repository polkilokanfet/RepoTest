using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.PriceEngineering.Tce.Second
{
    public class TasksTceWrapper : WrapperBase<PriceEngineeringTasks>
    {
        #region TceNumber

        /// <summary>
        /// Номер ТСЕ
        /// </summary>
        public string TceNumber
        {
            get => Model.TceNumber;
            set => SetValue(value);
        }
        public string TceNumberOriginalValue => GetOriginalValue<string>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));

        #endregion

        #region CommentBackOfficeBoss

        /// <summary>
        /// Комментарий руководителя бэкофиса
        /// </summary>
        public string CommentBackOfficeBoss
        {
            get => Model.CommentBackOfficeBoss;
            set => SetValue(value);
        }
        public string CommentBackOfficeBossOriginalValue => GetOriginalValue<string>(nameof(CommentBackOfficeBoss));
        public bool CommentBackOfficeBossIsChanged => GetIsChanged(nameof(CommentBackOfficeBoss));

        #endregion

        /// <summary>
        /// BackManager
        /// </summary>
        public UserEmptyWrapper BackManager
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(BackManager, value);
        }

        /// <summary>
        /// Расчеты переменных затрат
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceCalculationEmptyWrapper> PriceCalculations { get; private set; }

        public IValidatableChangeTrackingCollection<TasksTceItem> Items { get; private set; }

        public event Action<PriceEngineeringTask> LoadFilesRequest;

        public TasksTceWrapper(PriceEngineeringTasks model) : base(model)
        {
            foreach (var item in Items)
            {
                item.LoadFilesRequest += task => this.LoadFilesRequest?.Invoke(task);
            }
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserEmptyWrapper(Model.BackManager));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.PriceCalculations == null) throw new ArgumentException("PriceCalculations cannot be null");
            PriceCalculations = new ValidatableChangeTrackingCollection<PriceCalculationEmptyWrapper>(Model.PriceCalculations.Select(x => new PriceCalculationEmptyWrapper(x)));
            RegisterCollection(PriceCalculations, Model.PriceCalculations);

            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            Items = new ValidatableChangeTrackingCollection<TasksTceItem>(Model.ChildPriceEngineeringTasks.Select(x => new TasksTceItem(x)));
            RegisterCollection(Items, Model.ChildPriceEngineeringTasks);
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                if (TceNumber == null)
                    yield return new ValidationResult("TceNumber is required", new[] { nameof(TceNumber) });
            }
        }
    }
}