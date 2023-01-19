using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class TaskWrapperConstructor : TaskViewModelWithStartCommand
    {
        #region SimpleProperties

        /// <summary>
        /// Id ������
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
        /// �����������
        /// </summary>
        public UserEmptyWrapper UserConstructor
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(UserConstructor, value);
        }

        /// <summary>
        /// ���� �������� �� ���������
        /// </summary>
        public ProductBlockEmptyWrapper ProductBlockManager
        {
            get => GetWrapper<ProductBlockEmptyWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockEmptyWrapper>(ProductBlockManager, value);
        }

        /// <summary>
        /// ���� �������� �� ��������-������������
        /// </summary>
        public ProductBlockStructureCostWrapper ProductBlockEngineer
        {
            get => GetWrapper<ProductBlockStructureCostWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockStructureCostWrapper>(ProductBlockEngineer, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// ����������� ����� �������� �� ��������-������������
        /// </summary>
        public IValidatableChangeTrackingCollection<TaskProductBlockAddedWrapperConstructor> ProductBlocksAdded { get; private set; }

        /// <summary>
        /// ����� ����������� ����������
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskFileTechnicalRequirementsWrapper> FilesTechnicalRequirements { get; private set; }

        /// <summary>
        /// ����� ������� ���
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskFileAnswerWrapper> FilesAnswers { get; private set; }

        ///// <summary>
        ///// ���������
        ///// </summary>
        //public MessagesCollection Messages { get; }

        /// <summary>
        /// ������� ����������
        /// </summary>
        public StatusesCollection Statuses { get; private set; }

        /// <summary>
        /// SalesUnits
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitEmptyWrapper> SalesUnits { get; private set; }

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