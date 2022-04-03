using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskViewModel : WrapperBase<PriceEngineeringTask>, IDisposable
    {
        protected readonly IUnityContainer Container;
        protected readonly IUnitOfWork UnitOfWork;
        private PriceEngineeringTaskViewModel _parent;
        private PriceEngineeringTaskFileTechnicalRequirementsWrapper _selectedTechnicalRequrementsFile;

        #region Wrapper

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
	    public ProductBlockEmptyWrapper ProductBlockEngineer
        {
            get => GetWrapper<ProductBlockEmptyWrapper>();
            set => SetComplexValue<ProductBlock, ProductBlockEmptyWrapper>(ProductBlockEngineer, value);
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
        public IValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel> ChildPriceEngineeringTasks { get; private set; }
        
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

        private void InitializeChildPriceEngineeringTasks(IEnumerable<PriceEngineeringTask> priceEngineeringTasks)
        {
            if (Model.ChildPriceEngineeringTasks == null) throw new ArgumentException("ChildPriceEngineeringTasks cannot be null");
            var engineeringTaskViewModels = priceEngineeringTasks.Select(priceEngineeringTask => PriceEngineeringTaskViewModelFactory.GetInstance(Container, UnitOfWork, priceEngineeringTask));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(engineeringTaskViewModels);
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
        }
        
        #endregion

        #region Commands

        public DelegateLogCommand SendMessageCommand { get; private set; }

        public DelegateLogCommand OpenTechnicalRequrementsFileCommand { get; private set; }

        public DelegateLogCommand StartCommand { get; private set; }

        #endregion

        /// <summary>
        /// Задача в режиме редактирования
        /// </summary>
        public bool IsEditMode { get; } = true;

        /// <summary>
        /// Родительское задание
        /// </summary>
        public PriceEngineeringTaskViewModel Parent
        {
            get => _parent;
            set
            {
                _parent = value;

                if (_parent == null) return;

                //подписываемся на событие добавления нового ТЗ в родительское задание
                _parent.FilesTechnicalRequirements.CollectionChanged += (sender, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (var file in args.NewItems.Cast<PriceEngineeringTaskFileTechnicalRequirementsWrapper>())
                        {
                            if(IsEditMode)
                                this.FilesTechnicalRequirements.Add(file);
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Выбранный файл ТЗ
        /// </summary>
        public PriceEngineeringTaskFileTechnicalRequirementsWrapper SelectedTechnicalRequrementsFile
        {
            get => _selectedTechnicalRequrementsFile;
            set
            {
                if (Equals(value, _selectedTechnicalRequrementsFile)) return;
                _selectedTechnicalRequrementsFile = value;
                SelectedTechnicalRequrementsFileIsChanged?.Invoke();
            }
        }

        /// <summary>
        /// Событие изменения выбранного файла ТЗ
        /// </summary>
        protected event Action SelectedTechnicalRequrementsFileIsChanged;

        public PriceEngineeringTaskMessageWrapper Message { get; private set; }

        public ObservableCollection<MessageViewModel> MessagesAll { get; } = new ObservableCollection<MessageViewModel>();

        #region ctors

        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask)
        {
            Container = container;
            UnitOfWork = unitOfWork;

            InitializeChildPriceEngineeringTasks(Model.ChildPriceEngineeringTasks);
            InCtor();
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
                var priceEngineeringTaskViewModel = PriceEngineeringTaskViewModelFactory.GetInstance(Container, UnitOfWork, dependentProduct.Product);
                this.ChildPriceEngineeringTasks.Add(priceEngineeringTaskViewModel);
                priceEngineeringTaskViewModel.Parent = this;
            }
        }

        private PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork) : base(new PriceEngineeringTask())
        {
            Container = container;
            UnitOfWork = unitOfWork;

            InitializeChildPriceEngineeringTasks(new List<PriceEngineeringTask>());
            InCtor();
        }

        #endregion

        /// <summary>
        /// Метод запускается в конце каждого конструктора
        /// </summary>
        protected virtual void InCtor()
        {
            #region Message

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
                () => Message != null && Message.IsValid && Message.IsChanged && string.IsNullOrEmpty(Message.Message) == false);

            Message = new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
            {
                Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                Message = String.Empty
            });

            Message.PropertyChanged += (sender, args) => this.SendMessageCommand.RaiseCanExecuteChanged();

            ReloadMessagesAll();
            
            #endregion

            OpenTechnicalRequrementsFileCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //если файл уже в хранилище
                        if (string.IsNullOrEmpty(SelectedTechnicalRequrementsFile.Path))
                        {
                            Container.Resolve<IFilesStorageService>().OpenFileFromStorage(SelectedTechnicalRequrementsFile.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath, SelectedTechnicalRequrementsFile.Name);
                        }
                        //если файл еще не загружен в хранилище
                        else
                        {
                            Process.Start(SelectedTechnicalRequrementsFile.Path);
                        }
                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при открытии файла ТЗ", e.PrintAllExceptions());
                    }

                },
                () => SelectedTechnicalRequrementsFile != null);

            StartCommand = new DelegateLogCommand(
                () =>
                {
                    this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus()
                    {
                        StatusEnum = PriceEngineeringTaskStatusEnum.Started
                    }));

                    this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id), 
                        Message = "Стартована задача."
                    }));

                    this.AcceptChanges();
                    UnitOfWork.SaveChanges();
                },
                () => this.IsValid && this.IsChanged);

            this.PropertyChanged += (sender, args) => StartCommand.RaiseCanExecuteChanged();

            //костыль для красной рамки при пустом списке файлов ТЗ
            this.FilesTechnicalRequirements.CollectionChanged +=
                (sender, args) => OnPropertyChanged(nameof(FilesTechnicalRequirements));
        }

        private void ReloadMessagesAll()
        {
            MessagesAll.Clear();
            MessagesAll.AddRange(this.Model.Messages.Select(x => new MessageViewModel(x.Message, x.Author, x.Moment)).OrderByDescending(x => x.Moment));
        }

        /// <summary>
        /// Вернуть все добавленные файлы ТЗ
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTaskFileTechnicalRequirementsWrapper> GetAllNewFilesTechnicalRequirements()
        {
            foreach (var fileWrapper in this.FilesTechnicalRequirements.AddedItems)
            {
                yield return fileWrapper;
            }

            foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
            {
                foreach (var fileWrapper in childPriceEngineeringTask.GetAllNewFilesTechnicalRequirements())
                {
                    yield return fileWrapper;
                }
            }
        }


        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}