using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceEngineering.Messages;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class PriceEngineeringTaskWrapper1 : WrapperBase<PriceEngineeringTask>
    {
        protected readonly IUnitOfWork UnitOfWork;

        #region SimpleProperties

        /// <summary>
        /// Id группы
        /// </summary>
        public Guid? ParentPriceEngineeringTasksId
        {
            get => Model.ParentPriceEngineeringTasksId;
            set => SetValue(value);
        }
        public Guid ParentPriceEngineeringTasksIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ParentPriceEngineeringTasksId));
        public bool ParentPriceEngineeringTasksIdIsChanged => GetIsChanged(nameof(ParentPriceEngineeringTasksId));

        /// <summary>
        /// Количество блоков продукта
        /// </summary>
        public int Amount
        {
            get { return GetValue<System.Int32>(); }
            set { SetValue(value); }
        }
        public System.Int32 AmountOriginalValue => GetOriginalValue<System.Int32>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));

        /// <summary>
        /// Id материнской задачи
        /// </summary>
        public System.Guid ParentPriceEngineeringTaskId
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }
        public System.Guid ParentPriceEngineeringTaskIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ParentPriceEngineeringTaskId));
        public bool ParentPriceEngineeringTaskIdIsChanged => GetIsChanged(nameof(ParentPriceEngineeringTaskId));

        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        /// <summary>
        /// Запрос на проверку от руководителя
        /// </summary>
        public System.Boolean RequestForVerificationFromHead
        {
            get { return GetValue<System.Boolean>(); }
            set { SetValue(value); }
        }
        public System.Boolean RequestForVerificationFromHeadOriginalValue => GetOriginalValue<System.Boolean>(nameof(RequestForVerificationFromHead));
        public bool RequestForVerificationFromHeadIsChanged => GetIsChanged(nameof(RequestForVerificationFromHead));

        /// <summary>
        /// Запрос на проверку от исполнителя
        /// </summary>
        public System.Boolean RequestForVerificationFromConstructor
        {
            get { return GetValue<System.Boolean>(); }
            set { SetValue(value); }
        }
        public System.Boolean RequestForVerificationFromConstructorOriginalValue => GetOriginalValue<System.Boolean>(nameof(RequestForVerificationFromConstructor));
        public bool RequestForVerificationFromConstructorIsChanged => GetIsChanged(nameof(RequestForVerificationFromConstructor));


        /// <summary>
        /// Статус
        /// </summary>
        public PriceEngineeringTaskStatusEnum Status => this.Model.Status;

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Бюро конструкторов
        /// </summary>
        public DesignDepartmentEmptyWrapper DesignDepartment
        {
            get => GetWrapper<DesignDepartmentEmptyWrapper>();
            set => SetComplexValue<DesignDepartment, DesignDepartmentEmptyWrapper>(DesignDepartment, value);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
	    public UserEmptyWrapper UserConstructor
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(UserConstructor, value);
        }

        /// <summary>
        /// Блок продукта от менеджера
        /// </summary>
	    public ProductBlockEmptyWrapper ProductBlockManager
        {
            get => GetWrapper<ProductBlockEmptyWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockEmptyWrapper>(ProductBlockManager, value);
        }

        /// <summary>
        /// Блок продукта от инженера-конструктора
        /// </summary>
	    public ProductBlockStructureCostWrapper ProductBlockEngineer
        {
            get => GetWrapper<ProductBlockStructureCostWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockStructureCostWrapper>(ProductBlockEngineer, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// Добавленные блоки продукта от инженера-конструктора
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskProductBlockAddedWrapper1> ProductBlocksAdded { get; private set; }

        /// <summary>
        /// Файлы технических требований
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskFileTechnicalRequirementsWrapper> FilesTechnicalRequirements { get; private set; }

        /// <summary>
        /// Файлы ответов ОГК
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskFileAnswerWrapper> FilesAnswers { get; private set; }

        ///// <summary>
        ///// Переписка
        ///// </summary>
        //public MessagesCollection Messages { get; }

        /// <summary>
        /// Статусы проработки
        /// </summary>
        public StatusesCollection Statuses { get; private set; }

        /// <summary>
        /// SalesUnits
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitEmptyWrapper> SalesUnits { get; private set; }

        #endregion

        /// <summary>
        /// Дочерние задачи
        /// ChildPriceEngineeringTasks инициализируются в дочерних классах
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel> ChildPriceEngineeringTasks { get; protected set; }

        private PriceEngineeringTaskWrapper1(IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) 
            : base(priceEngineeringTask)
        {
            UnitOfWork = unitOfWork;


        }

        protected PriceEngineeringTaskWrapper1(IUnitOfWork unitOfWork, Guid priceEngineeringTaskId)
            : this(unitOfWork, unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId))
        {
        }

        protected PriceEngineeringTaskWrapper1(IUnitOfWork unitOfWork) 
            : this(unitOfWork, new PriceEngineeringTask())
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(DesignDepartment), Model.DesignDepartment == null ? null : new DesignDepartmentEmptyWrapper(Model.DesignDepartment));
            InitializeComplexProperty(nameof(UserConstructor), Model.UserConstructor == null ? null : new UserEmptyWrapper(Model.UserConstructor));
            InitializeComplexProperty(nameof(ProductBlockManager), Model.ProductBlockManager == null ? null : new ProductBlockEmptyWrapper(Model.ProductBlockManager));

            bool validateStructureCostNumber = false;
            if (this is PriceEngineeringTaskViewModelConstructor vm)
            {
                //если задача целевая и редактируемая нужно проверять на то, чтобы в блоке был стракчакост
                if (vm.IsTarget && vm.IsEditMode)
                {
                    validateStructureCostNumber = true;
                }
            }
            InitializeComplexProperty(nameof(ProductBlockEngineer), Model.ProductBlockEngineer == null ? null : new ProductBlockStructureCostWrapper(Model.ProductBlockEngineer, validateStructureCostNumber));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.ProductBlocksAdded == null) throw new ArgumentException("ProductBlocksAdded cannot be null");
            ProductBlocksAdded = new ValidatableChangeTrackingCollection<PriceEngineeringTaskProductBlockAddedWrapper1>(Model.ProductBlocksAdded.Select(e => new PriceEngineeringTaskProductBlockAddedWrapper1(e)));
            RegisterCollection(ProductBlocksAdded, Model.ProductBlocksAdded);

            if (Model.FilesTechnicalRequirements == null) throw new ArgumentException("FilesTechnicalRequirements cannot be null");
            FilesTechnicalRequirements = new ValidatableChangeTrackingCollection<PriceEngineeringTaskFileTechnicalRequirementsWrapper>(Model.FilesTechnicalRequirements.Select(e => new PriceEngineeringTaskFileTechnicalRequirementsWrapper(e)));
            RegisterCollection(FilesTechnicalRequirements, Model.FilesTechnicalRequirements);

            if (Model.FilesAnswers == null) throw new ArgumentException("FilesAnswers cannot be null");
            FilesAnswers = new ValidatableChangeTrackingCollection<PriceEngineeringTaskFileAnswerWrapper>(Model.FilesAnswers.Select(e => new PriceEngineeringTaskFileAnswerWrapper(e)));
            RegisterCollection(FilesAnswers, Model.FilesAnswers);

            if (Model.Statuses == null) throw new ArgumentException("Statuses cannot be null");
            Statuses = new StatusesCollection(Model.Statuses);
            RegisterCollection(Statuses, Model.Statuses);

            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitEmptyWrapper>(Model.SalesUnits.Select(e => new SalesUnitEmptyWrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }
    }

    public class StatusesCollection : ValidatableChangeTrackingCollection<PriceEngineeringTaskStatusEmptyWrapper>
    {
        public StatusesCollection(IEnumerable<PriceEngineeringTaskStatus> items) 
            : base(items.Select(x => new PriceEngineeringTaskStatusEmptyWrapper(x)))
        {
        }

        public void Add(PriceEngineeringTaskStatusEnum statusEnum, string comment = null)
        {
            var status = new PriceEngineeringTaskStatus()
            {
                StatusEnum = statusEnum,
                Moment = DateTime.Now,
                Comment = comment
            };

            this.Add(new PriceEngineeringTaskStatusEmptyWrapper(status));
        }
    }
}