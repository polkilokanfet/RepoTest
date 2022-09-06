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
        public IValidatableChangeTrackingCollection<PriceCalculationEmptyWrapper> PriceCalculations { get; }

        public IValidatableChangeTrackingCollection<TasksTceItem> Items { get; }

        public event Action<PriceEngineeringTask> LoadFilesRequest;

        public TasksTceWrapper(PriceEngineeringTasks model) : base(model)
        {
            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserEmptyWrapper(Model.BackManager));

            if (Model.PriceCalculations == null) throw new ArgumentException("PriceCalculations cannot be null");
            PriceCalculations = new ValidatableChangeTrackingCollection<PriceCalculationEmptyWrapper>(Model.PriceCalculations.Select(x => new PriceCalculationEmptyWrapper(x)));
            RegisterCollection(PriceCalculations, Model.PriceCalculations);

            Items = new ValidatableChangeTrackingCollection<TasksTceItem>(model.ChildPriceEngineeringTasks.Select(x => new TasksTceItem(x)));
            RegisterCollection(Items, Model.ChildPriceEngineeringTasks);

            foreach (var item in Items)
            {
                item.LoadFilesRequest += task => this.LoadFilesRequest?.Invoke(task);
            }
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