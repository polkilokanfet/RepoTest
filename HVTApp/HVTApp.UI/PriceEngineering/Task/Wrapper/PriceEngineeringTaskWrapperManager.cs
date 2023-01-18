using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class PriceEngineeringTaskWrapperManager : PriceEngineeringTaskWithStartCommandViewModel<PriceEngineeringTaskProductBlockAddedWrapper1Manager>
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
        /// Блок продукта от менеджера
        /// </summary>
        public ProductBlockEmptyWrapper ProductBlockManager
        {
            get => GetWrapper<ProductBlockEmptyWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockEmptyWrapper>(ProductBlockManager, value);
        }

        #endregion

        #region CollectionProperties

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

        protected PriceEngineeringTaskWrapperManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected PriceEngineeringTaskWrapperManager(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        #endregion

        protected override void InitializeProductBlockEngineerProperty()
        {
            InitializeComplexProperty(nameof(ProductBlockEngineer), Model.ProductBlockEngineer == null
                ? null
                : new ProductBlockStructureCostWrapperConstructor(Model.ProductBlockEngineer));
        }

        //protected override PriceEngineeringTaskProductBlockAddedWrapper1 GetPriceEngineeringTaskProductBlockAddedWrapper(PriceEngineeringTaskProductBlockAdded p)
        //{
        //    return new PriceEngineeringTaskProductBlockAddedWrapper1Manager(p);
        //}
    }
}