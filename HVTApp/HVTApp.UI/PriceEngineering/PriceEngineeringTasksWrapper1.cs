using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksWrapper1 : WrapperBase<PriceEngineeringTasks>
    {
        public PriceEngineeringTasksWrapper1(PriceEngineeringTasks model, IUnityContainer container, IUnitOfWork unitOfWork) : base(model)
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(Model.ChildPriceEngineeringTasks.Select(e => PriceEngineeringTaskViewModelFactory.GetInstance(container, unitOfWork, e)));
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
        }

        #region SimpleProperties

        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id => Model.Id;

        /// <summary>
        /// Проработать до
        /// </summary>
        public DateTime WorkUpTo
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public DateTime WorkUpToOriginalValue => GetOriginalValue<DateTime>(nameof(WorkUpTo));
        public bool WorkUpToIsChanged => GetIsChanged(nameof(WorkUpTo));

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Менеджер
        /// </summary>
        public UserEmptyWrapper UserManager
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(UserManager, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// Файлы технических требований (общие)
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper> FilesTechnicalRequirements { get; private set; }
        
        /// <summary>
        /// Задачи
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel> ChildPriceEngineeringTasks { get; private set; }

        /// <summary>
        /// Расчеты переменных затрат
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceCalculationWrapper> PriceCalculations { get; private set; }

        #endregion
        
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(UserManager), Model.UserManager == null ? null : new UserEmptyWrapper(Model.UserManager));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.FilesTechnicalRequirements == null) throw new ArgumentException("FilesTechnicalRequirements cannot be null");
            FilesTechnicalRequirements = new ValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper>(Model.FilesTechnicalRequirements.Select(e => new PriceEngineeringTasksFileTechnicalRequirementsWrapper(e)));
            RegisterCollection(FilesTechnicalRequirements, Model.FilesTechnicalRequirements);

            if (Model.PriceCalculations == null) throw new ArgumentException("PriceCalculations cannot be null");
            PriceCalculations = new ValidatableChangeTrackingCollection<PriceCalculationWrapper>(Model.PriceCalculations.Select(e => new PriceCalculationWrapper(e)));
            RegisterCollection(PriceCalculations, Model.PriceCalculations);
        }
    }
}