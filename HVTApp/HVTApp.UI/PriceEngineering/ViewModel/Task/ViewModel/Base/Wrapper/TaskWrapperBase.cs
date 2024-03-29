using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceEngineering.Messages;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class TaskWrapperBase<TBlockAdded> : WrapperBase<PriceEngineeringTask>, IStatusesContainer
        where TBlockAdded : WrapperBase<PriceEngineeringTaskProductBlockAdded>
    {
        protected readonly IUnitOfWork UnitOfWork;

        #region SimpleProperties

        #region Amount

        /// <summary>
        /// ���������� ������ ��������
        /// </summary>
        public int Amount
        {
            get => Model.Amount;
            set => SetValue(value);
        }
        public int AmountOriginalValue => GetOriginalValue<int>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));

        #endregion

        #region ParentPriceEngineeringTaskId

        /// <summary>
        /// Id ����������� ������
        /// </summary>
        public Guid ParentPriceEngineeringTaskId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }
        public Guid ParentPriceEngineeringTaskIdOriginalValue => GetOriginalValue<Guid>(nameof(ParentPriceEngineeringTaskId));
        public bool ParentPriceEngineeringTaskIdIsChanged => GetIsChanged(nameof(ParentPriceEngineeringTaskId));

        #endregion

        ///// <summary>
        ///// Id
        ///// </summary>
        //public System.Guid Id
        //{
        //    get { return GetValue<System.Guid>(); }
        //    set { SetValue(value); }
        //}
        //public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        //public bool IdIsChanged => GetIsChanged(nameof(Id));

        #region RequestForVerificationFromHead

        /// <summary>
        /// ������ �� �������� �� ������������
        /// </summary>
        public bool RequestForVerificationFromHead
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool RequestForVerificationFromHeadOriginalValue => GetOriginalValue<bool>(nameof(RequestForVerificationFromHead));
        public bool RequestForVerificationFromHeadIsChanged => GetIsChanged(nameof(RequestForVerificationFromHead));

        #endregion

        #region RequestForVerificationFromConstructor

        /// <summary>
        /// ������ �� �������� �� �����������
        /// </summary>
        public bool RequestForVerificationFromConstructor
        {
            get { return GetValue<System.Boolean>(); }
            set { SetValue(value); }
        }
        public System.Boolean RequestForVerificationFromConstructorOriginalValue => GetOriginalValue<System.Boolean>(nameof(RequestForVerificationFromConstructor));
        public bool RequestForVerificationFromConstructorIsChanged => GetIsChanged(nameof(RequestForVerificationFromConstructor));

        #endregion


        /// <summary>
        /// ������
        /// </summary>
        public PriceEngineeringTaskStatusEnum Status => this.Model.Status;

        #endregion

        #region ComplexProperties

        /// <summary>
        /// ���� �������������
        /// </summary>
        public DesignDepartmentEmptyWrapper DesignDepartment
        {
            get => GetWrapper<DesignDepartmentEmptyWrapper>();
            set => SetComplexValue<DesignDepartment, DesignDepartmentEmptyWrapper>(DesignDepartment, value);
        }

        /// <summary>
        /// �����������
        /// </summary>
	    public UserEmptyWrapper UserConstructor
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(UserConstructor, value);
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
        public IValidatableChangeTrackingCollection<TBlockAdded> ProductBlocksAdded { get; private set; }

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

        /// <summary>
        /// �������� ������
        /// ChildPriceEngineeringTasks ���������������� � �������� �������
        /// </summary>
        public IValidatableChangeTrackingCollection<TaskViewModel> ChildPriceEngineeringTasks { get; protected set; }

        #region ctors

        private TaskWrapperBase(IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) 
            : base(priceEngineeringTask)
        {
            UnitOfWork = unitOfWork;
        }

        protected TaskWrapperBase(IUnitOfWork unitOfWork, Guid priceEngineeringTaskId)
            : this(unitOfWork, unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId))
        {
        }

        protected TaskWrapperBase(IUnitOfWork unitOfWork) 
            : this(unitOfWork, new PriceEngineeringTask())
        {
        }

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(DesignDepartment), Model.DesignDepartment == null ? null : new DesignDepartmentEmptyWrapper(Model.DesignDepartment));
            InitializeComplexProperty(nameof(UserConstructor), Model.UserConstructor == null ? null : new UserEmptyWrapper(Model.UserConstructor));

            bool validateStructureCostNumber = false;
            if (this is TaskViewModelConstructor vm)
            {
                //���� ������ ������� � ������������� ����� ��������� �� ��, ����� � ����� ��� �����������
                if (vm.IsTarget && vm.IsEditMode)
                {
                    validateStructureCostNumber = true;
                }
            }

            InitializeProductBlockEngineerProperty();
        }

        protected abstract void InitializeProductBlockEngineerProperty();

        protected override void InitializeCollectionProperties()
        {
            if (Model.ProductBlocksAdded == null) throw new ArgumentException("ProductBlocksAdded cannot be null");
            ProductBlocksAdded = new ValidatableChangeTrackingCollection<TBlockAdded>(Model.ProductBlocksAdded.Select(this.GetPriceEngineeringTaskProductBlockAddedWrapper));
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

        private TBlockAdded GetPriceEngineeringTaskProductBlockAddedWrapper(PriceEngineeringTaskProductBlockAdded p)
        {
            throw new NotImplementedException();
        }
    }
}