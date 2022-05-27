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

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public class PriceEngineeringTaskTceWrapper1 : WrapperBase<PriceEngineeringTaskTce>
    {
        #region SimpleProperties

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

        #region ComplexProperties

        /// <summary>
        /// Исполнитель
        /// </summary>
        public UserEmptyWrapper BackManager
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(BackManager, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// Связанные задачи верхнего уровня
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskWrapper1> PriceEngineeringTaskList { get; private set; }

        /// <summary>
        /// Версии стракчакостов
        /// </summary>
        public IValidatableChangeTrackingCollection<TceStructureCostVersion> SccVersions { get; private set; }

        /// <summary>
        /// История проработки
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskTceStoryItemWrapper> Story { get; private set; }



        #endregion

        public PriceEngineeringTaskTceWrapper1(PriceEngineeringTaskTce model) : base(model)
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserEmptyWrapper(Model.BackManager));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.PriceEngineeringTaskList == null) throw new ArgumentException("PriceEngineeringTaskList cannot be null");
            PriceEngineeringTaskList = new ValidatableChangeTrackingCollection<PriceEngineeringTaskWrapper1>(Model.PriceEngineeringTaskList.Select(e => new PriceEngineeringTaskWrapper1(e)));
            RegisterCollection(PriceEngineeringTaskList, Model.PriceEngineeringTaskList);

            if (Model.SccVersions == null) throw new ArgumentException("SccVersions cannot be null");
            SccVersions = new ValidatableChangeTrackingCollection<TceStructureCostVersion>(Model.SccVersions.Select(e => new TceStructureCostVersion(e, Model.PriceEngineeringTaskList)));
            RegisterCollection(SccVersions, Model.SccVersions);

            if (Model.StoryItems == null) throw new ArgumentException("Story cannot be null");
            Story = new ValidatableChangeTrackingCollection<PriceEngineeringTaskTceStoryItemWrapper>(Model.StoryItems.Select(e => new PriceEngineeringTaskTceStoryItemWrapper(e)));
            RegisterCollection(Story, Model.StoryItems);
        }


        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                if (string.IsNullOrWhiteSpace(TceNumber))
                {
                    yield return new ValidationResult("TceNumber is required", new[] { nameof(TceNumber) });
                }
            }
        }

    }
}