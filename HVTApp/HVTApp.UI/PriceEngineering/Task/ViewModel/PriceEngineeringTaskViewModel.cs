using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
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

        /// <summary>
        /// Развернуть задачу при открытии?
        /// </summary>
        public abstract bool IsExpanded { get; }

        /// <summary>
        /// Развернуть дочерние задачи при открытии?
        /// </summary>
        public abstract bool IsExpendedChildPriceEngineeringTasks { get; }

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
                            if(IsEditMode)
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

        #region Commands

        public DelegateLogCommand OpenTechnicalRequrementsFileCommand { get; private set; }
        public DelegateLogCommand LoadTechnicalRequrementsFilesCommand { get; private set; }

        public DelegateLogCommand OpenAnswerFileCommand { get; private set; }
        public DelegateLogCommand LoadAnswerFilesCommand { get; private set; }

        public DelegateLogCommand SaveCommand { get; protected set; }
        public DelegateLogCommand StartCommand { get; private set; }

        public DelegateLogCommand ShowReportCommand { get; private set; }

        /// <summary>
        /// Замена продукта в SalesUnit на продукты из задачи
        /// </summary>
        public DelegateLogCommand ReplaceProductCommand { get; private set; }

        #endregion

        #region Events

        public event Action TaskIsStarted;

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


        /// <summary>
        /// Событие полного принятия проработки задачи
        /// </summary>
        public event Action<PriceEngineeringTaskViewModel> TotalAcceptedEvent;

        #endregion

        protected void InvokePriceEngineeringTaskAccepted(PriceEngineeringTaskViewModel priceEngineeringTaskViewModel)
        {
            if (this.Model.IsTotalAccepted)
            {
                this.TotalAcceptedEvent?.Invoke(this);
            }
        }

        public PriceEngineeringTaskMessenger Messenger { get; private set; }

        #region ctors

        protected PriceEngineeringTaskViewModel(IUnityContainer container, PriceEngineeringTask priceEngineeringTask) 
            : base(priceEngineeringTask, container.Resolve<IUnitOfWork>())
        {
            Container = container;
            InCtor();
        }

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
                        .Where(x => x.IsActual).ToList();
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

            StartCommand = new DelegateLogCommand(() => { StartCommandExecute(true); },
                                                () => 
                                                    this.IsValid && 
                                                    this.IsChanged && 
                                                    (Status == PriceEngineeringTaskStatusEnum.Created || Status == PriceEngineeringTaskStatusEnum.Stopped) &&
                                                    UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) != null);

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

            ReplaceProductCommand = new DelegateLogCommand(
                () =>
                {
                    if (!this.Model.SalesUnits.Any()) return;
                    
                    var getProductService = Container.Resolve<IGetProductService>();
                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    var priceEngineeringTask = unitOfWork.Repository<PriceEngineeringTask>().GetById(this.Model.Id);

                    var product = getProductService.GetProduct(unitOfWork, priceEngineeringTask.GetProduct());
                    var salesUnits = this.Model.SalesUnits
                        .Select(x => unitOfWork.Repository<SalesUnit>().GetById(x.Id))
                        .ToList();

                    var productBlocksAdded = priceEngineeringTask
                        .GetAllPriceEngineeringTasks()
                        .SelectMany(x => x.ProductBlocksAdded)
                        .Where(x => x.IsRemoved == false)
                        .ToList();

                    //Включённое оборудование на всё количество
                    var productsIncludedOnAmount = productBlocksAdded
                        .Where(x => x.IsOnBlock == false)
                        .Select(x => new ProductIncluded
                        {
                            Product = getProductService.GetProduct(unitOfWork, x.GetProduct()),
                            Amount = x.Amount
                        })
                        .ToList();


                    foreach (var salesUnit in salesUnits)
                    {
                        //заменяем продукт
                        salesUnit.Product = product;

                        //заменяем включёное оборудование
                        //удаляем старое
                        foreach (var productIncluded in 
                            salesUnit.ProductsIncluded
                                .Where(x => x.Product.ProductBlock.IsSupervision == false)
                                .ToList())
                        {
                            salesUnit.ProductsIncluded.Remove(productIncluded);
                            unitOfWork.Repository<ProductIncluded>().Delete(productIncluded);
                        }

                        //Включённое оборудование на каждый блок
                        var productsIncludedOnBlock = productBlocksAdded
                            .Where(x => x.IsOnBlock == true)
                            .Select(x => new ProductIncluded
                            {
                                Product = getProductService.GetProduct(unitOfWork, x.GetProduct()),
                                Amount = x.Amount
                            })
                            .ToList();

                        salesUnit.ProductsIncluded.AddRange(productsIncludedOnBlock);
                        salesUnit.ProductsIncluded.AddRange(productsIncludedOnAmount);
                    }

                    Container.Resolve<IMessageService>().ShowOkMessageDialog("Уведомдение",
                        unitOfWork.SaveChanges().OperationCompletedSuccessfully
                            ? $"Заменен продукт в {salesUnits.First()}"
                            : $"Не заменен продукт в {salesUnits.First()}");
                });

            #endregion

            this.PropertyChanged += (sender, args) => StartCommand.RaiseCanExecuteChanged();
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

            //реакция на полное принятие задачи менеджером
            this.TotalAcceptedEvent += viewModel => ReplaceProductCommand.Execute();
        }



        /// <summary>
        /// Старт задачи
        /// </summary>
        /// <param name="saveChanges">Сохранить в конце и принять изменения?</param>
        public void StartCommandExecute(bool saveChanges)
        {
            if (saveChanges)
            {
                if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Стартовать задачу", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                    return;
            }

            this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus
            {
                StatusEnum = PriceEngineeringTaskStatusEnum.Started
            }));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Задача запущена на проработку.");
            if (UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) != null)
            {
                if (this.FilesTechnicalRequirements.IsChanged)
                {
                    sb.AppendLine("Внесены изменения в ТЗ.");
                }

                var actualFiles = FilesTechnicalRequirements.Where(x => x.IsActual).OrderBy(x => x.CreationMoment).ToList();
                if (actualFiles.Any())
                {
                    sb.AppendLine("Актуальные файлы:");
                    foreach (var file in actualFiles)
                    {
                        sb.AppendLine($" + {file.CreationMoment} {file.Name}");
                    }
                }

                var notActualFiles = FilesTechnicalRequirements.Where(x => x.IsActual == false).OrderBy(x => x.CreationMoment).ToList();
                if (notActualFiles.Any())
                {
                    sb.AppendLine("Не актуальные файлы:");
                    foreach (var file in notActualFiles)
                    {
                        sb.AppendLine($" - {file.CreationMoment} {file.Name}");
                    }
                }

            }

            this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage
            {
                Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                Message = sb.ToString().TrimEnd('\n', '\r')
            }));


            //если запускается только конкретная задача
            if (saveChanges)
            {
                this.SaveCommand.Execute();
                Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStartedEvent>().Publish(this.Model);
            }
            //если запускаются все задачи в задании
            else
            {
                this.ChildPriceEngineeringTasks.ForEach(x => x.StartCommandExecute(false));
            }

            StartCommand.RaiseCanExecuteChanged();
            TaskIsStarted?.Invoke();
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

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}