using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.PriceEngineering.Wrapper;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelBase : WrapperBase<PriceEngineeringTask>
    {
        protected readonly IUnitOfWork UnitOfWork;

        #region ctors

        private TaskViewModelBase(IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask)
            : base(priceEngineeringTask)
        {
            UnitOfWork = unitOfWork;
        }

        protected TaskViewModelBase(IUnitOfWork unitOfWork, Guid priceEngineeringTaskId)
            : this(unitOfWork, unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId))
        {
        }

        protected TaskViewModelBase(IUnitOfWork unitOfWork)
            : this(unitOfWork, new PriceEngineeringTask())
        {
        }

        #endregion

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

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(DesignDepartment), Model.DesignDepartment == null ? null : new DesignDepartmentEmptyWrapper(Model.DesignDepartment));
        }

        protected override void InitializeCollectionProperties()
        {
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
}