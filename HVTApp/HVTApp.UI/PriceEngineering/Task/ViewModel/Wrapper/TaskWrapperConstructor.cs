using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class TaskWrapperConstructor : TaskViewModelWithStartCommand
    {
        #region SimpleProperties

        /// <summary>
        /// Запрос на проверку от исполнителя
        /// </summary>
        public bool RequestForVerificationFromConstructor
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool RequestForVerificationFromConstructorOriginalValue => GetOriginalValue<bool>(nameof(RequestForVerificationFromConstructor));
        public bool RequestForVerificationFromConstructorIsChanged => GetIsChanged(nameof(RequestForVerificationFromConstructor));

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Блок продукта от инженера-конструктора
        /// </summary>
        public new ProductBlockStructureCostWrapperConstructor ProductBlockEngineer
        {
            get => GetWrapper<ProductBlockStructureCostWrapperConstructor>();
            set => SetComplexValue<ProductBlock, ProductBlockStructureCostWrapperConstructor>(ProductBlockEngineer, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// Добавленные блоки продукта от инженера-конструктора
        /// </summary>
        public new IValidatableChangeTrackingCollection<TaskProductBlockAddedWrapperConstructor> ProductBlocksAdded { get; private set; }

        #endregion

        #region ctors

        protected TaskWrapperConstructor(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected TaskWrapperConstructor(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        #endregion

        public override void InitializeComplexProperties()
        {
            base.InitializeComplexProperties();

            InitializeComplexProperty(nameof(ProductBlockEngineer), Model.ProductBlockEngineer == null 
                ? null 
                : new ProductBlockStructureCostWrapperConstructor(Model.ProductBlockEngineer));
        }

        protected override void InitializeCollectionProperties()
        {
            base.InitializeCollectionProperties();

            if (Model.ProductBlocksAdded == null) throw new ArgumentException("ProductBlocksAdded cannot be null");
            ProductBlocksAdded = new ValidatableChangeTrackingCollection<TaskProductBlockAddedWrapperConstructor>(Model.ProductBlocksAdded.Select(x => new TaskProductBlockAddedWrapperConstructor(x)));
            RegisterCollection(ProductBlocksAdded, Model.ProductBlocksAdded);
        }
    }
}