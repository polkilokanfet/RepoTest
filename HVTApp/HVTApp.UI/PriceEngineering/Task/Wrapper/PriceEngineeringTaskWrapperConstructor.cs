using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class PriceEngineeringTaskWrapperConstructor : PriceEngineeringTaskWithStartCommandViewModel<PriceEngineeringTaskProductBlockAddedWrapper1Constructor>
    {
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

        #endregion

        #region ComplexProperties

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

        #region ctors

        protected PriceEngineeringTaskWrapperConstructor(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected PriceEngineeringTaskWrapperConstructor(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        #endregion

        protected override void InitializeProductBlockEngineerProperty()
        {
            InitializeComplexProperty(nameof(ProductBlockEngineer), Model.ProductBlockEngineer == null 
                ? null 
                : new ProductBlockStructureCostWrapperConstructor(Model.ProductBlockEngineer));
        }
    }
}