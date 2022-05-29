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
        #region SimpleProperties

        /// <summary>
        /// Номер ТСЕ
        /// </summary>
        public string TceNumber
        {
            get => Model.TceNumber;
            set => SetValue(value);
        }
        public string TceNumberOriginalValue => GetOriginalValue<System.String>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));

        #endregion

        #region ComplexProperties

        /// <summary>
        /// BackManager
        /// </summary>
        public UserEmptyWrapper BackManager
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(BackManager, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// Расчеты переменных затрат
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceCalculationWrapper> PriceCalculations { get; private set; }

        #endregion

        public IValidatableChangeTrackingCollection<TasksTceItem> Items { get; }

        public TasksTceWrapper(PriceEngineeringTasks model) : base(model)
        {

            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserEmptyWrapper(Model.BackManager));

            if (Model.PriceCalculations == null) throw new ArgumentException("PriceCalculations cannot be null");
            PriceCalculations = new ValidatableChangeTrackingCollection<PriceCalculationWrapper>(Model.PriceCalculations.Select(x => new PriceCalculationWrapper(x)));
            RegisterCollection(PriceCalculations, Model.PriceCalculations);

            Items = new ValidatableChangeTrackingCollection<TasksTceItem>(model.ChildPriceEngineeringTasks.Select(x => new TasksTceItem(x)));
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

    public class TasksTceItem : WrapperBase<PriceEngineeringTask>
    {
        public List<ISccContainer> SccVersions { get; } = new List<ISccContainer>();

        public TasksTceItem(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask)
        {
            foreach (var task in priceEngineeringTask.GetAllPriceEngineeringTasks())
            {
                SccVersions.Add(new SccVersionWrapperTask(task));
                foreach (var blockAdded in task.ProductBlocksAdded)
                {
                    SccVersions.Add(new SccVersionWrapperBlockAdded(blockAdded));
                }
            }
        }
    }

    public interface ISccContainer
    {
        IValidatableChangeTrackingCollection<SccVersionWrapper> StructureCostVersions { get; }
        SccVersionWrapper TargetSccVersion { get; }
    }

    public class SccVersionWrapperTask : WrapperBase<PriceEngineeringTask>, ISccContainer
    {
        /// <summary>
        /// Версии SCC
        /// </summary>
        public IValidatableChangeTrackingCollection<SccVersionWrapper> StructureCostVersions { get; }

        public SccVersionWrapper TargetSccVersion { get; }

        public SccVersionWrapperTask(PriceEngineeringTask model) : base(model)
        {
            if (Model.StructureCostVersions == null) throw new ArgumentException("StructureCostVersions cannot be null");
            StructureCostVersions = new ValidatableChangeTrackingCollection<SccVersionWrapper>(Model.StructureCostVersions.Select(x => new SccVersionWrapper(x, model.ProductBlockEngineer.ToString())));
            RegisterCollection(StructureCostVersions, Model.StructureCostVersions);

            var originalStructureCostNumber = model.ProductBlockEngineer.StructureCostNumber;
            var structureCostVersionWrapper = StructureCostVersions.FirstOrDefault(x => x.OriginalStructureCostNumber == originalStructureCostNumber);
            if (structureCostVersionWrapper != null)
            {
                TargetSccVersion = structureCostVersionWrapper;
            }
            else
            {
                TargetSccVersion = new SccVersionWrapper(new StructureCostVersion
                {
                    OriginalStructureCostNumber = originalStructureCostNumber
                }, model.ProductBlockEngineer.ToString());
                this.StructureCostVersions.Add(TargetSccVersion);
            }
        }
    }

    public class SccVersionWrapperBlockAdded : WrapperBase<PriceEngineeringTaskProductBlockAdded>, ISccContainer
    {
        /// <summary>
        /// Версии SCC
        /// </summary>
        public IValidatableChangeTrackingCollection<SccVersionWrapper> StructureCostVersions { get; }

        public SccVersionWrapper TargetSccVersion { get; }

        public SccVersionWrapperBlockAdded(PriceEngineeringTaskProductBlockAdded model) : base(model)
        {
            if (Model.StructureCostVersions == null) throw new ArgumentException("StructureCostVersions cannot be null");
            StructureCostVersions = new ValidatableChangeTrackingCollection<SccVersionWrapper>(Model.StructureCostVersions.Select(x => new SccVersionWrapper(x, Model.ProductBlock.ToString())));
            RegisterCollection(StructureCostVersions, Model.StructureCostVersions);

            var originalStructureCostNumber = model.ProductBlock.StructureCostNumber;
            var structureCostVersionWrapper = StructureCostVersions.FirstOrDefault(x => x.OriginalStructureCostNumber == originalStructureCostNumber);
            if (structureCostVersionWrapper != null)
            {
                TargetSccVersion = structureCostVersionWrapper;
            }
            else
            {
                TargetSccVersion = new SccVersionWrapper(new StructureCostVersion
                {
                    OriginalStructureCostNumber = originalStructureCostNumber}, Model.ProductBlock.ToString());
                this.StructureCostVersions.Add(TargetSccVersion);
            }
        }
    }
}