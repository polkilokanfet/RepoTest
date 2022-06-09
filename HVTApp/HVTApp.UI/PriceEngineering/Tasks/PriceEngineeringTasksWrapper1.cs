using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksWrapper1 : WrapperBase<PriceEngineeringTasks>
    {
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
        public IValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper> FilesTechnicalRequirements { get; }

        /// <summary>
        /// Расчеты переменных затрат
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceCalculationWrapper> PriceCalculations { get; }

        #endregion

        /// <summary>
        /// Задачи
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel> ChildPriceEngineeringTasks { get; }


        #region Events

        /// <summary>
        /// Событие сохранения любой из входящих в сборник задач
        /// </summary>
        public event Action<PriceEngineeringTask> PriceEngineeringTaskSaved;

        /// <summary>
        /// Событие принятия любой из входящих в сборник задач
        /// </summary>
        public event Action<PriceEngineeringTask> PriceEngineeringTaskAccepted;

        #endregion

        private PriceEngineeringTasksWrapper1(PriceEngineeringTasks model) : base(model)
        {
            #region InitializeComplexProperties

            InitializeComplexProperty(nameof(UserManager), Model.UserManager == null ? null : new UserEmptyWrapper(Model.UserManager));

            #endregion

            #region InitializeCollectionProperties

            if (Model.FilesTechnicalRequirements == null) throw new ArgumentException("FilesTechnicalRequirements cannot be null");
            FilesTechnicalRequirements = new ValidatableChangeTrackingCollection<PriceEngineeringTasksFileTechnicalRequirementsWrapper>(Model.FilesTechnicalRequirements.Select(e => new PriceEngineeringTasksFileTechnicalRequirementsWrapper(e)));
            RegisterCollection(FilesTechnicalRequirements, Model.FilesTechnicalRequirements);

            if (Model.PriceCalculations == null) throw new ArgumentException("PriceCalculations cannot be null");
            PriceCalculations = new ValidatableChangeTrackingCollection<PriceCalculationWrapper>(Model.PriceCalculations.Select(e => new PriceCalculationWrapper(e)));
            RegisterCollection(PriceCalculations, Model.PriceCalculations);

            #endregion
        }

        public PriceEngineeringTasksWrapper1(PriceEngineeringTasks model, IUnityContainer container) : this(model)
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(Model.ChildPriceEngineeringTasks.Select(e => PriceEngineeringTaskViewModelFactory.GetInstance(container, e)));
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);

            EnumerableExtensions.ForEach(ChildPriceEngineeringTasks
                    .SelectMany(x => x.GetAllPriceEngineeringTaskViewModels()), x =>
                {
                    x.PriceEngineeringTaskSaved += task => this.PriceEngineeringTaskSaved?.Invoke(task);
                    x.PriceEngineeringTaskAccepted += task => this.PriceEngineeringTaskAccepted?.Invoke(task);
                });
        }

        public PriceEngineeringTasksWrapper1(IEnumerable<PriceEngineeringTaskViewModelManager> taskList) : this(new PriceEngineeringTasks())
        {
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(new List<PriceEngineeringTaskViewModelManager>());
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
            ChildPriceEngineeringTasks.AddRange(taskList);

            foreach (var vm in ChildPriceEngineeringTasks.SelectMany(x => x.GetAllPriceEngineeringTaskViewModels().ToList()))
            {
                vm.PriceEngineeringTaskSaved += task => this.PriceEngineeringTaskSaved?.Invoke(task);
                vm.PriceEngineeringTaskAccepted += task => this.PriceEngineeringTaskAccepted?.Invoke(task);
            }
        }
    }
}