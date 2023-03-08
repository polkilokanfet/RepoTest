using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperManager : TasksWrapper<TaskViewModelManager>
    {
        #region SimpleProperties

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
        /// Расчеты переменных затрат
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceCalculationWrapper> PriceCalculations { get; private set; }

        #endregion

        #region ctors

        /// <summary>
        /// Для загрузки и редактирования существующей ТСП
        /// </summary>
        /// <param name="model"></param>
        /// <param name="container"></param>
        public TasksWrapperManager(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
            foreach (var priceEngineeringTaskViewModel in ChildTasks)
            {
                if (priceEngineeringTaskViewModel is TaskViewModelManagerOld vmOld)
                {
                    vmOld.TaskAcceptedByManagerAction += task =>
                    {
                        var priceEngineeringTasks = container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(Model.Id);
                        if (priceEngineeringTasks.IsAccepted)
                        {
                            AllTasksAcceptedByManagerAction?.Invoke();
                        }
                    };
                }
            }
        }

        /// <summary>
        /// Для создания новой ТСП
        /// </summary>
        /// <param name="taskList"></param>
        /// <param name="manager"></param>
        public TasksWrapperManager(IEnumerable<TaskViewModelManager> taskList, User manager) : base()
        {
            if (taskList == null || taskList.Any() == false)
                throw new ArgumentException();

            ChildTasks = new ValidatableChangeTrackingCollection<TaskViewModelManager>(new List<TaskViewModelManager>());
            RegisterCollection(ChildTasks, Model.ChildPriceEngineeringTasks);
            ChildTasks.AddRange(taskList);

            this.UserManager = new UserEmptyWrapper(manager);
            this.WorkUpTo = DateTime.Now.AddDays(3);
        }
        
        #endregion


        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(UserManager), Model.UserManager == null ? null : new UserEmptyWrapper(Model.UserManager));
        }

        protected override void InitializeCollectionProperties()
        {
            base.InitializeCollectionProperties();

            if (Model.PriceCalculations == null) throw new ArgumentException("PriceCalculations cannot be null");
            PriceCalculations = new ValidatableChangeTrackingCollection<PriceCalculationWrapper>(Model.PriceCalculations.Select(e => new PriceCalculationWrapper(e)));
            RegisterCollection(PriceCalculations, Model.PriceCalculations);
        }

        protected override TaskViewModelManager GetChildPriceEngineeringTask(IUnityContainer container, Guid id)
        {
            return new TaskViewModelManagerOld(container, this.Model.ChildPriceEngineeringTasks.Single(x => x.Id == id));
        }


        /// <summary>
        /// Событие возникающее, когда менеджер принял все задачи
        /// </summary>
        public event Action AllTasksAcceptedByManagerAction;

    }
}