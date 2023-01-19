using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public  abstract partial class TaskViewModel : TaskViewModelBase, IDisposable
    {
        protected readonly IUnityContainer Container;
        private TaskViewModel _parent;
        private PriceEngineeringTaskFileTechnicalRequirementsWrapper _selectedTechnicalRequirementsFile;
        private PriceEngineeringTaskFileAnswerWrapper _selectedFileAnswer;
        private bool _isVisible = true;

        #region Props

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (Equals(_isVisible, value))
                    return;
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Эта задача подходит текущему пользователю
        /// </summary>
        public abstract bool IsTarget { get; }

        /// <summary>
        /// Задача в режиме редактирования
        /// </summary>
        public abstract bool IsEditMode { get; }

        /// <summary>
        /// Можно ли редактировать добавленные блоки
        /// </summary>
        public virtual bool AllowEditAddedBlocks => false;

        public PriceEngineeringTaskMessenger Messenger { get; private set; }

        /// <summary>
        /// Родительское задание
        /// </summary>
        public TaskViewModel Parent
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
                            if (IsEditMode)
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
            get => _selectedTechnicalRequirementsFile;
            set
            {
                if (Equals(value, _selectedTechnicalRequirementsFile)) return;
                _selectedTechnicalRequirementsFile = value;
                SelectedTechnicalRequrementsFileIsChanged?.Invoke();
            }
        }

        public PriceEngineeringTaskFileAnswerWrapper SelectedFileAnswer
        {
            get => _selectedFileAnswer;
            set
            {
                if (Equals(value, _selectedFileAnswer)) return;
                _selectedFileAnswer = value;
                SelectedAnswerFileIsChanged?.Invoke();
            }
        }

        #endregion

        #region Commands

        public DelegateLogCommand OpenTechnicalRequirementsFileCommand { get; private set; }
        public DelegateLogCommand LoadTechnicalRequirementsFilesCommand { get; private set; }

        public DelegateLogCommand OpenAnswerFileCommand { get; private set; }
        public DelegateLogCommand LoadAnswerFilesCommand { get; private set; }

        public DelegateLogCommand SaveCommand { get; private set; }

        public DelegateLogCommand ShowReportCommand { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Событие изменения выбранного файла ТЗ
        /// </summary>
        protected event Action SelectedTechnicalRequrementsFileIsChanged;

        /// <summary>
        /// Событие изменения выбранного файла ответа ОГК
        /// </summary>
        protected event Action SelectedAnswerFileIsChanged;

        #endregion

        #region ctors

        /// <summary>
        /// Для загрузки и редактирования существующей задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTaskId"></param>
        protected TaskViewModel(IUnityContainer container, Guid priceEngineeringTaskId) 
            : base(container.Resolve<IUnitOfWork>(), priceEngineeringTaskId)
        {
            Container = container;
            InCtor();
        }

        /// <summary>
        /// Для создания новой задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        protected TaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            Container = container;
            InCtor();
        }

        /// <summary>
        /// Метод запускается в конце каждого конструктора
        /// </summary>
        protected virtual void  InCtor()
        {
            #region Commands

            OpenTechnicalRequirementsFileCommand = new DelegateLogCommand(
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

            LoadTechnicalRequirementsFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var files = this.Model.FilesTechnicalRequirements
                        .Where(x => x.IsActual).ToList();
                    if (files.Any())
                        Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath);
                });

            OpenAnswerFileCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //если файл уже в хранилище
                        if (string.IsNullOrEmpty(SelectedFileAnswer.Path))
                        {
                            Container.Resolve<IFilesStorageService>().OpenFileFromStorage(SelectedFileAnswer.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath, SelectedFileAnswer.Name);
                        }
                        //если файл еще не загружен в хранилище
                        else
                        {
                            Process.Start(SelectedFileAnswer.Path);
                        }
                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при открытии файла", e.PrintAllExceptions());
                    }
                },
                () => SelectedFileAnswer != null);


            LoadAnswerFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var files = this.Model.FilesAnswers
                        .Where(fileAnswer => fileAnswer.IsActual).ToList();
                    if (files.Any())
                        Container.Resolve<IFilesStorageService>().CopyFilesFromStorage(files, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath);
                });

            SaveCommand = new DelegateLogCommand(SaveCommand_ExecuteMethod, SaveCommand_CanExecuteMethod);

            ShowReportCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IDialogService>().Show(this.Model, $"Отчет по проработке блока {this.Model.ProductBlockEngineer.Designation}");
                    //if (GlobalAppProperties.User.Login == "sivkov")
                    //{
                    //    var blocks = this.ProductBlocksAdded.Select(x => x.Model).ToList();
                    //    Container.Resolve<IJsonService>().WriteJsonFile(blocks, Path.Combine(@"D:\test.json"));
                    //}
                });

            #endregion

            //синхронизация сообщений
            Messenger = new PriceEngineeringTaskMessenger(this, Container);

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(IsEditMode));
                OnPropertyChanged(nameof(AllowEditAddedBlocks));
            };
        }

        #endregion

        protected virtual void SaveCommand_ExecuteMethod()
        {
            this.AcceptChanges();
            UnitOfWork.SaveChanges();
            Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
        }

        protected virtual bool SaveCommand_CanExecuteMethod()
        {
            return this.IsValid && this.IsChanged;
        }

        public IEnumerable<TaskViewModel> GetAllPriceEngineeringTaskViewModels()
        {
            yield return this;

            foreach (var childPriceEngineeringTask in ChildPriceEngineeringTasks)
            {
                foreach (var engineeringTaskViewModel in childPriceEngineeringTask.GetAllPriceEngineeringTaskViewModels())
                {
                    yield return engineeringTaskViewModel;
                }
            }
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
            Messenger.Dispose();
        }
    }

    public abstract partial class TaskViewModel : IStatusesContainer
    {
        #region SimpleProperties

        #region Amount

        /// <summary>
        /// Количество блоков продукта
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
        /// Id материнской задачи
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
        /// Запрос на проверку от руководителя
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
        /// Запрос на проверку от исполнителя
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
        /// Статус
        /// </summary>
        public PriceEngineeringTaskStatusEnum Status => this.Model.Status;

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Бюро конструкторов
        /// </summary>
        public DesignDepartmentEmptyWrapper DesignDepartment
        {
            get => GetWrapper<DesignDepartmentEmptyWrapper>();
            set => SetComplexValue<DesignDepartment, DesignDepartmentEmptyWrapper>(DesignDepartment, value);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
	    public UserEmptyWrapper UserConstructor
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(UserConstructor, value);
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

        /// <summary>
        /// Дочерние задачи
        /// ChildPriceEngineeringTasks инициализируются в дочерних классах
        /// </summary>
        public IValidatableChangeTrackingCollection<TaskViewModel> ChildPriceEngineeringTasks { get; protected set; }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(DesignDepartment), Model.DesignDepartment == null ? null : new DesignDepartmentEmptyWrapper(Model.DesignDepartment));
            InitializeComplexProperty(nameof(UserConstructor), Model.UserConstructor == null ? null : new UserEmptyWrapper(Model.UserConstructor));
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