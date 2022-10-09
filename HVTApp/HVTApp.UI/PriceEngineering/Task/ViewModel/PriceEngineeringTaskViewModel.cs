using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
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
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskViewModel : PriceEngineeringTaskWrapper1, IDisposable
    {
        protected readonly IUnityContainer Container;
        private PriceEngineeringTaskViewModel _parent;
        private PriceEngineeringTaskFileTechnicalRequirementsWrapper _selectedTechnicalRequrementsFile;
        private PriceEngineeringTaskFileAnswerWrapper _selectedFileAnswer;
        private PriceEngineeringTaskProductBlockAddedWrapper1 _selectedBlockAdded;

        #region Props

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

        /// <summary>
        /// Статус
        /// </summary>
        public PriceEngineeringTaskStatusEnum Status => this.Model.Status;

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
                            if (IsEditMode)
                                this.FilesTechnicalRequirements.Add(file);
                        }
                    }
                };
            }
        }

        public PriceEngineeringTaskProductBlockAddedWrapper1 SelectedBlockAdded
        {
            get => _selectedBlockAdded;
            set
            {
                if (Equals(_selectedBlockAdded, value)) return;
                _selectedBlockAdded = value;
                SelectedBlockAddedIsChanged?.Invoke();
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

        public DelegateLogCommand OpenTechnicalRequrementsFileCommand { get; private set; }
        public DelegateLogCommand LoadTechnicalRequrementsFilesCommand { get; private set; }

        public DelegateLogCommand OpenAnswerFileCommand { get; private set; }
        public DelegateLogCommand LoadAnswerFilesCommand { get; private set; }

        public DelegateLogCommand SaveCommand { get; protected set; }

        public DelegateLogCommand ShowReportCommand { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Событие изменения выбранного добавленного блока
        /// </summary>
        protected event Action SelectedBlockAddedIsChanged;

        /// <summary>
        /// Событие изменения выбранного файла ТЗ
        /// </summary>
        protected event Action SelectedTechnicalRequrementsFileIsChanged;

        /// <summary>
        /// Событие изменения выбранного файла ответа ОГК
        /// </summary>
        protected event Action SelectedAnswerFileIsChanged;

        #endregion

        public PriceEngineeringTaskMessenger Messenger { get; private set; }

        #region ctors

        /// <summary>
        /// Для загрузки и редактирования существующей задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTaskId"></param>
        protected PriceEngineeringTaskViewModel(IUnityContainer container, Guid priceEngineeringTaskId) 
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
        protected PriceEngineeringTaskViewModel(IUnityContainer container, IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            Container = container;
            InCtor();
        }

        #endregion

        /// <summary>
        /// Метод запускается в конце каждого конструктора
        /// </summary>
        protected virtual void  InCtor()
        {
            this.Statuses.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(Status));

            //если задача в процессе создания, нужно добавить соответствующий статус
            if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) == null)
            {
                this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus {StatusEnum = PriceEngineeringTaskStatusEnum.Created}));
            }

            #region Commands

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

            LoadTechnicalRequrementsFilesCommand = new DelegateLogCommand(
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

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    this.LoadNewTechnicalRequirementFilesInStorage();
                    this.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
                });

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

            this.Statuses.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(IsEditMode));

            //синхронизация сообщений
            Messenger = new PriceEngineeringTaskMessenger(Container, this);
            Messenger.SendedMessageInNewTask += (authorId, moment, message1) =>
            {
                var message = new PriceEngineeringTaskMessage
                {
                    Author = UnitOfWork.Repository<User>().GetById(authorId),
                    Moment = moment,
                    Message = message1
                };
                var messageWrapper = new PriceEngineeringTaskMessageWrapper(message);
                this.Messages.Add(messageWrapper);
            };

            this.Statuses.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(AllowEditAddedBlocks));
        }



        /// <summary>
        /// Загрузить все добавленные файлы ТЗ в хранилище
        /// </summary>
        public void LoadNewTechnicalRequirementFilesInStorage()
        {
            foreach (var fileWrapper in this.FilesTechnicalRequirements.AddedItems)
            {
                var destFileName = $"{GlobalAppProperties.Actual.TechnicalRequrementsFilesPath}\\{fileWrapper.Id}{Path.GetExtension(fileWrapper.Path)}";
                if (File.Exists(destFileName) == false && string.IsNullOrEmpty(fileWrapper.Path) == false)
                {
                    File.Copy(fileWrapper.Path, destFileName);
                    fileWrapper.Path = null;
                }
            }

            foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
            {
                childPriceEngineeringTask.LoadNewTechnicalRequirementFilesInStorage();
            }
        }

        public IEnumerable<PriceEngineeringTaskViewModel> GetAllPriceEngineeringTaskViewModels()
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

        protected void SetStatus(PriceEngineeringTaskStatusEnum status, string message)
        {
            this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus
            {
                StatusEnum = status
            }));

            this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage
            {
                Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                Message = message
            }));
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}