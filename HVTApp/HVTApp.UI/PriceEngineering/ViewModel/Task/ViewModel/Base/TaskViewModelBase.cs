using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelBase : WrapperBase<PriceEngineeringTask>
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IUnityContainer Container;

        #region ctors

        private TaskViewModelBase(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask)
            : base(priceEngineeringTask)
        {
            Container = container;
            UnitOfWork = unitOfWork;

            this.PropertyChanged += (sender, args) =>
            {
                this.RaiseCommandsCanExecuteChanged();
            };

            this.InCtor();
        }

        protected TaskViewModelBase(IUnityContainer container, IUnitOfWork unitOfWork, Guid priceEngineeringTaskId)
            : this(container, unitOfWork, unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId))
        {
        }

        protected TaskViewModelBase(IUnityContainer container, IUnitOfWork unitOfWork)
            : this(container, unitOfWork, new PriceEngineeringTask())
        {
        }

        /// <summary>
        /// ����� ����������� � ����� ������� ������������
        /// </summary>
        protected virtual void InCtor() { }

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

        /// <summary>
        /// ������
        /// </summary>
        public ScriptStep2 Status => this.Model.Status;

        #endregion

        #region ComplexProperties

        /// <summary>
        /// �����������
        /// </summary>
        public User UserConstructor => Model.UserConstructor;

        /// <summary>
        /// ���� �������������
        /// </summary>
        public DesignDepartment DesignDepartment => Model.DesignDepartment;

        /// <summary>
        /// ���� �������� �� ��������-������������
        /// </summary>
	    public ProductBlockEmptyWrapper ProductBlockEngineer => Model.ProductBlockEngineer == null ? null : new ProductBlockEmptyWrapper(Model.ProductBlockEngineer);

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

        /// <summary>
        /// ������� ����������
        /// </summary>
        public StatusesCollection Statuses { get; private set; }

        /// <summary>
        /// SalesUnits
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitEmptyWrapper> SalesUnits { get; private set; }

        /// <summary>
        /// ����������� ����� �������� �� ��������-������������
        /// </summary>
        public IEnumerable<TaskProductBlockAddedWrapperConstructor> ProductBlocksAdded => Model.ProductBlocksAdded.Select(x => new TaskProductBlockAddedWrapperConstructor(x));

        #endregion

        /// <summary>
        /// �������� ������
        /// ChildPriceEngineeringTasks ���������������� � �������� �������
        /// </summary>
        public IValidatableChangeTrackingCollection<TaskViewModel> ChildPriceEngineeringTasks { get; protected set; }

        #region InitializeProperties

        public override void InitializeComplexProperties()
        {
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

        #endregion

        /// <summary>
        /// �������� ���� ������ �� ����������� ����������
        /// </summary>
        private void RaiseCommandsCanExecuteChanged()
        {
            this.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => typeof(ICommandRaiseCanExecuteChanged).IsAssignableFrom(x.PropertyType))
                .Select(x => x.GetValue(this))
                .Where(x => x != null)
                .Cast<ICommandRaiseCanExecuteChanged>()
                .ForEach(x => x.RaiseCanExecuteChanged());
        }
    }
}