using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1;

namespace HVTApp.UI.PriceEngineering
{
    public partial class PriceEngineeringTaskWrapper1 : WrapperBase<PriceEngineeringTask>
    {
        public PriceEngineeringTaskWrapper1(PriceEngineeringTask model) : base(model) { }

        #region SimpleProperties

        /// <summary>
        /// Id группы
        /// </summary>
        public System.Guid ParentPriceEngineeringTasksId
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }
        public System.Guid ParentPriceEngineeringTasksIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ParentPriceEngineeringTasksId));
        public bool ParentPriceEngineeringTasksIdIsChanged => GetIsChanged(nameof(ParentPriceEngineeringTasksId));
        /// <summary>
        /// Количество блоков продукта
        /// </summary>
        public System.Int32 Amount
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
        #endregion
        #region ComplexProperties
        /// <summary>
        /// Конструктор
        /// </summary>
        public UserWrapper UserConstructor
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(UserConstructor, value); }
        }
        /// <summary>
        /// Блок продукта от менеджера
        /// </summary>
        public ProductBlockWrapper ProductBlockManager
        {
            get { return GetWrapper<ProductBlockWrapper>(); }
            set { SetComplexValue<ProductBlock, ProductBlockWrapper>(ProductBlockManager, value); }
        }
        /// <summary>
        /// Блок продукта от инженера-конструктора
        /// </summary>
        public ProductBlockWrapper ProductBlockEngineer
        {
            get { return GetWrapper<ProductBlockWrapper>(); }
            set { SetComplexValue<ProductBlock, ProductBlockWrapper>(ProductBlockEngineer, value); }
        }
        #endregion

        #region CollectionProperties

        /// <summary>
        /// Добавленные блоки продукта от инженера-конструктора
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskProductBlockAddedWrapper> ProductBlocksAdded { get; private set; }
        
        /// <summary>
        /// Файлы технических требований
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskFileTechnicalRequirementsWrapper> FilesTechnicalRequirements { get; private set; }
        
        /// <summary>
        /// Файлы ответов ОГК
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskFileAnswerWrapper> FilesAnswers { get; private set; }
        
        /// <summary>
        /// Переписка
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskMessageWrapper> Messages { get; private set; }
        
        /// <summary>
        /// Дочерние задачи
        /// </summary>
        public IValidatableChangeTrackingCollection<Model.Wrapper.PriceEngineeringTaskWrapper> ChildPriceEngineeringTasks { get; private set; }
        
        /// <summary>
        /// Статусы проработки
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskStatusWrapper> Statuses { get; private set; }
        
        /// <summary>
        /// SalesUnits
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitEmptyWrapper> SalesUnits { get; private set; }

        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(UserConstructor), Model.UserConstructor == null ? null : new UserWrapper(Model.UserConstructor));
            InitializeComplexProperty<ProductBlockWrapper>(nameof(ProductBlockManager), Model.ProductBlockManager == null ? null : new ProductBlockWrapper(Model.ProductBlockManager));
            InitializeComplexProperty<ProductBlockWrapper>(nameof(ProductBlockEngineer), Model.ProductBlockEngineer == null ? null : new ProductBlockWrapper(Model.ProductBlockEngineer));
        }
        protected override void InitializeCollectionProperties()
        {
            if (Model.ProductBlocksAdded == null) throw new ArgumentException("ProductBlocksAdded cannot be null");
            ProductBlocksAdded = new ValidatableChangeTrackingCollection<PriceEngineeringTaskProductBlockAddedWrapper>(Model.ProductBlocksAdded.Select(e => new PriceEngineeringTaskProductBlockAddedWrapper(e)));
            RegisterCollection(ProductBlocksAdded, Model.ProductBlocksAdded);
            
            if (Model.FilesTechnicalRequirements == null) throw new ArgumentException("FilesTechnicalRequirements cannot be null");
            FilesTechnicalRequirements = new ValidatableChangeTrackingCollection<PriceEngineeringTaskFileTechnicalRequirementsWrapper>(Model.FilesTechnicalRequirements.Select(e => new PriceEngineeringTaskFileTechnicalRequirementsWrapper(e)));
            RegisterCollection(FilesTechnicalRequirements, Model.FilesTechnicalRequirements);
            
            if (Model.FilesAnswers == null) throw new ArgumentException("FilesAnswers cannot be null");
            FilesAnswers = new ValidatableChangeTrackingCollection<PriceEngineeringTaskFileAnswerWrapper>(Model.FilesAnswers.Select(e => new PriceEngineeringTaskFileAnswerWrapper(e)));
            RegisterCollection(FilesAnswers, Model.FilesAnswers);
            
            if (Model.Messages == null) throw new ArgumentException("Messages cannot be null");
            Messages = new ValidatableChangeTrackingCollection<PriceEngineeringTaskMessageWrapper>(Model.Messages.Select(e => new PriceEngineeringTaskMessageWrapper(e)));
            RegisterCollection(Messages, Model.Messages);
            
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<Model.Wrapper.PriceEngineeringTaskWrapper>(Model.ChildPriceEngineeringTasks.Select(e => new Model.Wrapper.PriceEngineeringTaskWrapper(e)));
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
            
            if (Model.Statuses == null) throw new ArgumentException("Statuses cannot be null");
            Statuses = new ValidatableChangeTrackingCollection<PriceEngineeringTaskStatusWrapper>(Model.Statuses.Select(e => new PriceEngineeringTaskStatusWrapper(e)));
            RegisterCollection(Statuses, Model.Statuses);
            
            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitEmptyWrapper>(Model.SalesUnits.Select(e => new SalesUnitEmptyWrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }
    }
}