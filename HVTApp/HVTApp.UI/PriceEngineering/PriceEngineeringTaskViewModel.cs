using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskViewModel : WrapperBase<PriceEngineeringTask>, IDisposable
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;

        #region SimpleProperties
        /// <summary>
        /// Id ������
        /// </summary>
        public System.Guid ParentPriceEngineeringTasksId
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }
        public System.Guid ParentPriceEngineeringTasksIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ParentPriceEngineeringTasksId));
        public bool ParentPriceEngineeringTasksIdIsChanged => GetIsChanged(nameof(ParentPriceEngineeringTasksId));
        /// <summary>
        /// ���������� ������ ��������
        /// </summary>
        public System.Int32 Amount
        {
            get { return GetValue<System.Int32>(); }
            set { SetValue(value); }
        }
        public System.Int32 AmountOriginalValue => GetOriginalValue<System.Int32>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));
        /// <summary>
        /// Id ����������� ������
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
	    public ProductBlockEmptyWrapper ProductBlockEngineer
        {
            get => GetWrapper<ProductBlockEmptyWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockEmptyWrapper>(ProductBlockEngineer, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// ����������� ����� �������� �� ��������-������������
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskProductBlockAddedWrapper> ProductBlocksAdded { get; private set; }
        
        /// <summary>
        /// ����� ����������� ����������
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskFileTechnicalRequirementsWrapper> FilesTechnicalRequirements { get; private set; }
        
        /// <summary>
        /// ����� ������� ���
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskFileAnswerWrapper> FilesAnswers { get; private set; }
        
        /// <summary>
        /// ���������
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskMessageWrapper> Messages { get; private set; }
        
        /// <summary>
        /// �������� ������
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel> ChildPriceEngineeringTasks { get; private set; }
        
        /// <summary>
        /// ������� ����������
        /// </summary>
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskStatusWrapper> Statuses { get; private set; }
        
        /// <summary>
        /// SalesUnits
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitEmptyWrapper> SalesUnits { get; private set; }

        #endregion

        #region Commands

        public DelegateLogCommand SendMessageCommand { get; private set; }

        #endregion

        public PriceEngineeringTaskMessageWrapper Message { get; private set; }

        public ObservableCollection<MessageViewModel> MessagesAll { get; } = new ObservableCollection<MessageViewModel>();

        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask)
        {
            Container = container;
            UnitOfWork = unitOfWork;

            InitializeChildPriceEngineeringTasks(Model.ChildPriceEngineeringTasks);
            InitializeCommands();
        }

        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : this(container, unitOfWork, salesUnits.First().Product)
        {
            this.SalesUnits.AddRange(salesUnits.Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit)));
        }

        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : this(container, unitOfWork)
        {
            ProductBlockEngineer = ProductBlockManager = new ProductBlockEmptyWrapper(product.ProductBlock);

            foreach (var dependentProduct in product.DependentProducts)
            {
                this.ChildPriceEngineeringTasks.Add(PriceEngineeringTaskViewModelFactory.GetInstance(Container, UnitOfWork, dependentProduct.Product));
            }
        }

        private PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork) : base(new PriceEngineeringTask())
        {
            Container = container;
            UnitOfWork = unitOfWork;

            InitializeChildPriceEngineeringTasks(new List<PriceEngineeringTask>());
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SendMessageCommand = new DelegateLogCommand(
                () =>
                {
                    var priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Model.Id);
                    if (priceEngineeringTask != null)
                    {
                        IUnitOfWork unitOfWork = Container.Resolve<IUnitOfWork>();
                        var message = new PriceEngineeringTaskMessage
                        {
                            Author = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                            Message = this.Message.Message
                        };
                        unitOfWork.Repository<PriceEngineeringTask>().GetById(this.Model.Id).Messages.Add(message);
                        unitOfWork.SaveChanges();
                    }
                    else
                    {
                        Message.Moment = DateTime.Now;
                        this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage
                        {
                            Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                            Message = this.Message.Message
                        }));
                    }

                    this.Message.Message = string.Empty;
                    ReloadMessagesAll();
                },
                () => Message != null && Message.IsValid && Message.IsChanged);

            Message = new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
            {
                Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                Message = String.Empty
            });

            Message.PropertyChanged += (sender, args) => this.SendMessageCommand.RaiseCanExecuteChanged();

            ReloadMessagesAll();
        }

        private void ReloadMessagesAll()
        {
            MessagesAll.Clear();
            MessagesAll.AddRange(this.Model.Messages.Select(x => new MessageViewModel(x.Message, x.Author, x.Moment)).OrderBy(x => x.Moment));
        }

        private void InitializeChildPriceEngineeringTasks(IEnumerable<PriceEngineeringTask> priceEngineeringTasks)
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            var engineeringTaskViewModels = priceEngineeringTasks.Select(priceEngineeringTask => PriceEngineeringTaskViewModelFactory.GetInstance(Container, UnitOfWork, priceEngineeringTask));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(engineeringTaskViewModels);
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(UserConstructor), Model.UserConstructor == null ? null : new UserEmptyWrapper(Model.UserConstructor));
            InitializeComplexProperty(nameof(ProductBlockManager), Model.ProductBlockManager == null ? null : new ProductBlockEmptyWrapper(Model.ProductBlockManager));
            InitializeComplexProperty(nameof(ProductBlockEngineer), Model.ProductBlockEngineer == null ? null : new ProductBlockEmptyWrapper(Model.ProductBlockEngineer));
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
            
            if (Model.Statuses == null) throw new ArgumentException("Statuses cannot be null");
            Statuses = new ValidatableChangeTrackingCollection<PriceEngineeringTaskStatusWrapper>(Model.Statuses.Select(e => new PriceEngineeringTaskStatusWrapper(e)));
            RegisterCollection(Statuses, Model.Statuses);
            
            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitEmptyWrapper>(Model.SalesUnits.Select(e => new SalesUnitEmptyWrapper(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}