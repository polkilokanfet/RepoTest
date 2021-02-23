using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using DirectumTaskWrapper = HVTApp.Model.Wrapper.DirectumTaskWrapper;

namespace HVTApp.UI.Modules.Directum
{
    public class DirectumTaskViewModel : ViewModelBase
    {
        private bool _taskIsNew = false;
        private bool _taskIsSubTask = false;
        private DirectumTaskRouteWrapper _route = new DirectumTaskRouteWrapper(new DirectumTaskRoute());
        private DirectumTaskWrapper _directumTask = new DirectumTaskWrapper(new Model.POCOs.DirectumTask {Group = new DirectumTaskGroup {StartAuthor = DateTime.Now} });
        private DirectumTaskMessageWrapper _message;
        private Model.POCOs.DirectumTask _parentTask;
        private readonly IMessageService _messageService;
        private readonly string _rootFilesDirectoryPath = GlobalAppProperties.Actual.DirectumAttachmentsPath;

        //необходимо при согласовании последовательных задач
        private List<DirectumTaskMessageWrapper> _messagesSeria;
        private DirectumTaskGroupFileWrapper _selectedFile;

        /// <summary>
        /// Задача
        /// </summary>
        public DirectumTaskWrapper DirectumTask
        {
            get => _directumTask;
            private set
            {
                _directumTask = value;
                OnPropertyChanged();
                ((DelegateCommand)AddFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Маршрут
        /// </summary>
        public DirectumTaskRouteWrapper Route
        {
            get => _route;
            private set
            {
                _route = value;
                OnPropertyChanged();
            }
        }

        public DirectumTaskGroupFileWrapper SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)OpenFileCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public bool TaskIsNew
        {
            get => _taskIsNew;
            private set
            {
                _taskIsNew = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllowEditTitle));
                OnPropertyChanged(nameof(AllowSubTask));
                OnPropertyChanged(nameof(AllowStop));
                ((DelegateCommand)AddFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
            }
        }

        public bool TaskIsSubTask
        {
            get => _taskIsSubTask;
            private set
            {
                _taskIsSubTask = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllowEditTitle));
                ((DelegateCommand)AddFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Разрешено ли редактировать тему задачи
        /// </summary>
        public bool AllowEditTitle => TaskIsNew && !TaskIsSubTask;

        /// <summary>
        /// Разрешено ли создавать подзадачи
        /// </summary>
        public bool AllowSubTask => !TaskIsNew;

        /// <summary>
        /// Разрешение на выполнение задачи
        /// </summary>
        public bool AllowPerform =>
            DirectumTask?.Performer != null && 
            DirectumTask.Performer.IsAppCurrentUser() && 
            !DirectumTask.FinishPerformer.HasValue &&
            !DirectumTask.Group.IsStoped;

        /// <summary>
        /// Разрешение на принятие выполнения задачи
        /// </summary>
        public bool AllowAccept => 
            DirectumTask?.Performer != null && 
            DirectumTask.Group.Author.IsAppCurrentUser() && 
            DirectumTask.FinishPerformer.HasValue && 
            !DirectumTask.FinishAuthor.HasValue &&
            !DirectumTask.NextTasks.Any();

        /// <summary>
        /// Разрешение на принятие или выполнение задачи
        /// </summary>
        public bool AllowPerformOrAccept => AllowPerform || AllowAccept;

        /// <summary>
        /// Разрешение на остановку задачи
        /// </summary>
        public bool AllowStop => 
            !TaskIsNew && 
            !DirectumTask.Group.IsStoped &&
            !DirectumTask.FinishAuthor.HasValue &&
            DirectumTask.Group.Author.IsAppCurrentUser();

        public DirectumTaskMessageWrapper Message
        {
            get => _message;
            private set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        #region ICommand

        /// <summary>
        /// Выбор маршрута
        /// </summary>
        public ICommand RouteCommand { get; }

        /// <summary>
        /// Старт задачи
        /// </summary>
        public ICommand StartCommand { get; }

        /// <summary>
        /// Остановка задачи
        /// </summary>
        public ICommand StopCommand { get; }

        /// <summary>
        /// Выполнение задачи
        /// </summary>
        public ICommand PerformCommand { get; }

        /// <summary>
        /// Согласование выполнения задачи
        /// </summary>
        public ICommand AcceptCommand { get; }

        /// <summary>
        /// Отклонение выполнененной задачи
        /// </summary>
        public ICommand RejectCommand { get; }

        /// <summary>
        /// Создать подзадачу
        /// </summary>
        public ICommand SubTaskCommand { get; }

        /// <summary>
        /// Загрузить приложение в задачу
        /// </summary>
        public ICommand AddFileCommand { get; }

        /// <summary>
        /// Удалить приложение из задачи
        /// </summary>
        public ICommand RemoveFileCommand { get; }


        /// <summary>
        /// Открыть приложение
        /// </summary>
        public ICommand OpenFileCommand { get; }

        #endregion

        public DirectumTaskViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            RouteCommand = new DelegateCommand(
                () =>
                {
                    var viewModel = new DirectumTaskRouteViewModel(Container, Route, TaskIsNew);
                    var dr = Container.Resolve<IDialogService>().ShowDialog(viewModel);

                    if (dr.HasValue && dr.Value)
                    {
                        Route.AcceptChanges();
                        DirectumTask.Performer = Route.Items.First().Performer;
                        OnPropertyChanged(nameof(Route));
                    }
                    else
                    {
                        Route.RejectChanges();
                    }

                    ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                });

            StartCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Информация", "Вы уверены, что хотите стартовать задачу?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var directumTaskGroup = new DirectumTaskGroup
                    {
                        Id = DirectumTask.Group.Id,
                        Author = unitOfWork.Repository<User>().GetById(DirectumTask.Group.Author.Id),
                        StartAuthor = DateTime.Now,
                        Title = DirectumTask.Group.Title,
                        Message = DirectumTask.Group.Message,
                        Files = DirectumTask.Model.Group.Files.ToList()
                    };
                    User user = unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                    directumTaskGroup.Files.ForEach(file => file.Author = user);

                    var directumTasks = new List<Model.POCOs.DirectumTask>();
                    //если маршрут параллельный
                    if (Route.IsParallel)
                    {
                        directumTasks = Route.Items.OrderBy(routeItemWrapper => routeItemWrapper.Performer.ToString()).Select(
                            routeItem => new Model.POCOs.DirectumTask
                            {
                                Group = directumTaskGroup,
                                Performer = unitOfWork.Repository<User>().GetById(routeItem.Performer.Id),
                                FinishPlan = routeItem.FinishPlan,
                                ParentTask = _parentTask == null ? null : unitOfWork.Repository<Model.POCOs.DirectumTask>().GetById(_parentTask.Id)
                            }).ToList();
                    }
                    //если маршрут последовательный
                    else
                    {
                        var items = Route.Items.OrderBy(x => x.FinishPlan).ToList();
                        Model.POCOs.DirectumTask prevTask = null;
                        foreach (var item in items)
                        {
                            var directumTask = new Model.POCOs.DirectumTask
                            {
                                Group = directumTaskGroup,
                                Performer = unitOfWork.Repository<User>().GetById(item.Performer.Id),
                                FinishPlan = item.FinishPlan,
                                ParentTask = _parentTask == null ? null : unitOfWork.Repository<Model.POCOs.DirectumTask>().GetById(_parentTask.Id),
                                PreviousTask = prevTask
                            };
                            prevTask = directumTask;
                            directumTasks.Add(directumTask);
                        }
                    }

                    unitOfWork.Repository<Model.POCOs.DirectumTask>().AddRange(directumTasks);
                    AddFiles();
                    RemoveFiles();
                    unitOfWork.SaveChanges();

                    var afterSaveDirectumTaskEvent = Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>();
                    var afterStartDirectumTaskEvent = Container.Resolve<IEventAggregator>().GetEvent<AfterStartDirectumTaskEvent>();

                    directumTasks.ForEach(directumTask =>
                    {
                        afterSaveDirectumTaskEvent.Publish(directumTask);
                        afterStartDirectumTaskEvent.Publish(directumTask);
                    });

                    DirectumTask = null; //костыль, чтобы не возмущался при выходе
                    GoBackCommand.Execute(null);
                },
                () => !string.IsNullOrEmpty(DirectumTask?.Group.Title) && !string.IsNullOrEmpty(DirectumTask.Group.Message) && Route.IsValid);

            StopCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Информация", "Вы уверены, что хотите прекратить задачу?\nБудут прекращены все параллельные предыдущие и последующие задачи в цепочке", defaultNo: true);
                    if (dr != MessageDialogResult.Yes) return;

                    var moment = DateTime.Now;
                    var message = "Остановлено.";

                    var tasks = new List<DirectumTaskWrapper> {DirectumTask};
                    tasks = tasks
                        .Union(DirectumTask.NextTasks)
                        .Union(DirectumTask.PreviousTasks)
                        .Union(DirectumTask.ParallelTasks)
                        .ToList();

                    foreach (var task in tasks)
                    {
                        task.Group.IsStoped = true;
                        if (!task.FinishAuthor.HasValue)
                            task.FinishAuthor = moment;
                        task.Messages.Add(new DirectumTaskMessageWrapper(new DirectumTaskMessage())
                        {
                            Author = DirectumTask.Group.Author,
                            Moment = moment,
                            Message = message
                        });
                    }

                    DirectumTask.AcceptChanges();
                    UnitOfWork.SaveChanges();

                    ((DelegateCommand)PerformCommand).RaiseCanExecuteChanged();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterStopDirectumTaskEvent>().Publish(DirectumTask.Model);

                    GoBackCommand.Execute(null);
                });

            PerformCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Информация", "Вы уверены, что хотите выполнить задачу?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    var moment = DateTime.Now;
                    Message.Moment = moment;
                    DirectumTask.FinishPerformer = moment;
                    DirectumTask.AcceptChanges();
                    UnitOfWork.SaveChanges();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>().Publish(DirectumTask.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<AfterPerformDirectumTaskEvent>().Publish(DirectumTask.Model);

                    GoBackCommand.Execute(null);
                },
                () => AllowPerform && DirectumTask.IsValid);

            AcceptCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Информация", "Вы уверены, что хотите принять выполнение задачи?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    var moment = DateTime.Now;
                    if (DirectumTask.PreviousTask == null)
                    {
                        Message.Moment = moment;
                        DirectumTask.FinishAuthor = moment;
                    }
                    else
                    {
                        var task = DirectumTask;
                        while (task != null)
                        {
                            task.FinishAuthor = moment;
                            task = task.PreviousTask;
                        }
                        _messagesSeria.ForEach(messageWrapper => messageWrapper.Moment = moment);
                    }
                    DirectumTask.AcceptChanges();
                    UnitOfWork.SaveChanges();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>().Publish(DirectumTask.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<AfterAcceptDirectumTaskEvent>().Publish(DirectumTask.Model);

                    GoBackCommand.Execute(null);
                },
                () => AllowAccept && DirectumTask.IsValid);

            RejectCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Информация", "Вы уверены, что хотите вернуть на доработку задачу?", defaultNo: true);
                    if (dr != MessageDialogResult.Yes) return;

                    var moment = DateTime.Now;
                    if (DirectumTask.PreviousTask == null)
                    {
                        Message.Moment = moment;
                        DirectumTask.FinishPerformer = default(DateTime?);
                    }
                    else
                    {
                        var task = DirectumTask;
                        while (task != null)
                        {
                            task.FinishPerformer = default(DateTime?);
                            task = task.PreviousTask;
                        }
                        _messagesSeria.ForEach(messageWrapper => messageWrapper.Moment = moment);
                    }

                    DirectumTask.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>().Publish(DirectumTask.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<AfterRejectDirectumTaskEvent>().Publish(DirectumTask.Model);

                    GoBackCommand.Execute(null);
                },
                () => AllowAccept && DirectumTask.IsValid);

            SubTaskCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters()
                    {
                        { nameof(Model.POCOs.DirectumTask), DirectumTask.Model },
                        { nameof(bool.GetType), true }
                    });
                },
                () => !TaskIsNew);

            AddFileCommand = new DelegateCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        User currentUser = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                        UserWrapper currentUserWrapper = new UserWrapper(currentUser);
                        //копируем каждый файл
                        foreach (var path in openFileDialog.FileNames)
                        {
                            var fileWrapper = new DirectumTaskGroupFileWrapper(new DirectumTaskGroupFile())
                            {
                                Name = Path.GetFileNameWithoutExtension(path).LimitLengh(255),
                                Author = currentUserWrapper
                            };
                            _filesToAdd.Add(fileWrapper, path);
                            this.DirectumTask.Group.Files.Add(fileWrapper);
                        }
                    }
                },
                () => DirectumTask != null && TaskIsNew && !TaskIsSubTask);

            RemoveFileCommand = new DelegateCommand(
                () =>
                {
                    //диалог
                    var dr = _messageService.ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите удалить выделенное приложение?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    if (_filesToAdd.ContainsKey(SelectedFile))
                    {
                        _filesToAdd.Remove(SelectedFile);
                    }
                    else
                    {
                        _filesToRemove.Add(SelectedFile, null);
                    }

                    this.DirectumTask.Group.Files.Remove(SelectedFile);
                    SelectedFile = null;
                }, 
                () => DirectumTask != null && TaskIsNew && !TaskIsSubTask && SelectedFile != null);

            OpenFileCommand = new DelegateCommand(
                () =>
                {
                    if (_filesToAdd.ContainsKey(SelectedFile))
                    {
                        Process.Start(_filesToAdd[SelectedFile]);
                    }
                    else
                    {
                        FilesStorage.OpenFileFromStorage(SelectedFile.Id, _messageService, _rootFilesDirectoryPath);
                    }
                }, 
                () => SelectedFile != null);

        }

        private readonly Dictionary<DirectumTaskGroupFileWrapper, string> _filesToAdd = new Dictionary<DirectumTaskGroupFileWrapper, string>();
        private readonly Dictionary<DirectumTaskGroupFileWrapper, string> _filesToRemove = new Dictionary<DirectumTaskGroupFileWrapper, string>();

        private void AddFiles()
        {
            foreach (var keyValuePair in _filesToAdd)
            {
                try
                {
                    string path = keyValuePair.Value;
                    var file = keyValuePair.Key;
                    File.Copy(path, $"{_rootFilesDirectoryPath}\\{file.Id}{Path.GetExtension(path)}");
                }
                catch (Exception e)
                {
                    _messageService.ShowOkMessageDialog(e.GetType().Name, e.GetAllExceptions());
                }
            }

            _filesToAdd.Clear();
        }

        private void RemoveFiles()
        {
            foreach (var keyValuePair in _filesToRemove)
            {
                try
                {
                    string path = keyValuePair.Value;
                    var file = keyValuePair.Key;

                    //удаление
                    FileInfo fileInfo = FilesStorage.FindFile(file.Id, _rootFilesDirectoryPath);
                    File.Delete(fileInfo.FullName);

                    UnitOfWork.Repository<DirectumTaskGroupFile>().Delete(SelectedFile.Model);
                }
                catch (Exception e)
                {
                    _messageService.ShowOkMessageDialog(e.GetType().Name, e.GetAllExceptions());
                }
            }

            _filesToRemove.Clear();
        }

        #region Load

        /// <summary>
        /// Создание новой задачи
        /// </summary>
        public void Load()
        {
            TaskIsNew = true;
            DirectumTask.Group.Title = "Введите тему задачи";
            DirectumTask.Group.Message = "Сформулируйте суть задачи";
            DirectumTask.Group.Author = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
            DirectumTask.IsMain = true;
        }

        /// <summary>
        /// Загрузка существующей задачи по группе
        /// </summary>
        /// <param name="directumTaskGroup">Группа задач.</param>
        public void Load(DirectumTaskGroup directumTaskGroup)
        {
            var tasks = UnitOfWork.Repository<Model.POCOs.DirectumTask>()
                .Find(directumTask => directumTask.Group.Id == directumTaskGroup.Id)
                .OrderBy(directumTask => directumTask.FinishPlan)
                .ToList();

            if (tasks.Any(directumTask => directumTask.PreviousTask != null))
            {
                Load(tasks.Last());
            }
            else if (tasks.Any(directumTask => !directumTask.FinishAuthor.HasValue))
            {
                Load(tasks.First(directumTask => !directumTask.FinishAuthor.HasValue));
            }
            else
            {
                Load(tasks.First());
            }
        }

        /// <summary>
        /// Загрузка подзадачи
        /// </summary>
        /// <param name="parentTask"></param>
        /// <param name="isSubTask"></param>
        public void Load(Model.POCOs.DirectumTask parentTask, bool isSubTask)
        {
            _parentTask = parentTask;
            Load();
            DirectumTask.Group.Title = $"{_parentTask.Group.Title} [подзадача]";
            DirectumTask.Group.Message = parentTask.Group.Message;
            DirectumTask.ParentTask = new DirectumTaskWrapper(_parentTask);
            TaskIsSubTask = true;
        }

        /// <summary>
        /// Загрузка существующей задачи
        /// </summary>
        /// <param name="directumTask">Задача для загрузки</param>
        public void Load(Model.POCOs.DirectumTask directumTask)
        {
            TaskIsNew = false;

            DirectumTask = new DirectumTaskWrapper(UnitOfWork.Repository<Model.POCOs.DirectumTask>().GetById(directumTask.Id));
            DirectumTask.IsMain = true;
            InjectTasks(DirectumTask);
            InjectParallelTasks(DirectumTask);

            //если есть возможность выполнить задачу
            if (AllowPerform)
            {
                Message = new DirectumTaskMessageWrapper(new DirectumTaskMessage())
                {
                    Author = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)),
                    Moment = DateTime.Now,
                    Message = "Выполнено."
                };

                DirectumTask.Messages.Add(Message);
                Message.PropertyChanged += (sender, args) => { ((DelegateCommand)PerformCommand).RaiseCanExecuteChanged(); };
                ((DelegateCommand)PerformCommand).RaiseCanExecuteChanged();
            }

            //если есть возможность принять задачу
            if (AllowAccept)
            {
                //если задача параллельная
                if (DirectumTask.PreviousTask == null)
                {
                    Message = new DirectumTaskMessageWrapper(new DirectumTaskMessage())
                    {
                        Author = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)),
                        Moment = DateTime.Now,
                        Message = "Принято."
                    };

                    DirectumTask.Messages.Add(Message);
                    Message.PropertyChanged += (sender, args) => { ((DelegateCommand)AcceptCommand).RaiseCanExecuteChanged(); };
                    ((DelegateCommand)AcceptCommand).RaiseCanExecuteChanged();
                }

                //если задача последняя в цепочке последовательных задач
                else
                {
                    _messagesSeria = new List<DirectumTaskMessageWrapper>();
                    var targetTask = DirectumTask;
                    var author = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
                    var moment = DateTime.Now;
                    while (targetTask != null)
                    {
                        var message = new DirectumTaskMessageWrapper(new DirectumTaskMessage())
                        {
                            Author = author,
                            Moment = moment
                        };
                        targetTask.Messages.Add(message);
                        _messagesSeria.Add(message);
                        targetTask = targetTask.PreviousTask;
                    }

                    Message = _messagesSeria.First();
                    Message.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(DirectumTaskMessage.Message))
                            _messagesSeria.ForEach(messageWrapper => messageWrapper.Message = Message.Message);
                    };

                    Message.Message = "Принято.";
                }
            }

            GetRoute();
            CheckStartPerformer();

            OnPropertyChanged(nameof(AllowPerformOrAccept));
        }

        #endregion
        
        /// <summary>
        /// Загрузка маршрута
        /// </summary>
        private void GetRoute()
        {
            //параллельные
            var parallelTasks = DirectumTask.Model.Parallel.ToList();
            parallelTasks.Add(DirectumTask.Model);

            var route = new DirectumTaskRoute();
            route.Items.AddRange(parallelTasks
                .OrderBy(x => x.FinishPlan)
                .Select(x => new DirectumTaskRouteItem
                {
                    FinishPlan = x.FinishPlan,
                    Performer = x.Performer,
                })
                .ToList());

            //предыдущие
            var task = DirectumTask.Model.PreviousTask;
            while (task != null)
            {
                route.Items.Add(new DirectumTaskRouteItem()
                {
                    FinishPlan = task.FinishPlan,
                    Performer = task.Performer
                });
                task = task.PreviousTask;
            }

            var items = route.Items.OrderBy(x => x.FinishPlan).ToList();
            route.Items.Clear();
            route.Items.AddRange(items);

            Route = new DirectumTaskRouteWrapper(route);
        }

        /// <summary>
        /// Простановка времени начала исполнения задачи
        /// </summary>
        private void CheckStartPerformer()
        {
            if (DirectumTask.StartPerformer.HasValue)
                return;

            if (!DirectumTask.Performer.IsAppCurrentUser())
                return;

            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var directumTask = unitOfWork.Repository<Model.POCOs.DirectumTask>().GetById(DirectumTask.Id);
            directumTask.StartPerformer = DateTime.Now;
            unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Внедряем в задачи все дочерние задачи
        /// </summary>
        /// <param name="directumTask"></param>
        /// <returns></returns>
        private DirectumTaskWrapper InjectTasks(DirectumTaskWrapper directumTask, bool injectPreviousTask = true)
        {
            //внедряем предыдущие задачи
            if (injectPreviousTask && directumTask.PreviousTask != null)
            {
                InjectTasks(directumTask.PreviousTask);
            }

            //внедряем последующие задачи
            foreach (var nextTask in UnitOfWork.Repository<Model.POCOs.DirectumTask>().Find(x => x.PreviousTask?.Id == directumTask.Id))
            {
                directumTask.NextTasks.Add(InjectTasks(new DirectumTaskWrapper(nextTask), false));
            }

            //внедряем дочерние задачи
            directumTask.ChildTasks.AddRange(UnitOfWork
                .Repository<Model.POCOs.DirectumTask>()
                .Find(x => Equals(x.ParentTask, directumTask.Model))
                .OrderBy(x => x.FinishPlan)
                .Select(x => InjectTasks(new DirectumTaskWrapper(x))));
            directumTask.ChildTasks.ForEach(x => x.ShowPreviousTask = false);
            directumTask.ChildTasks.ForEach(x => x.ShowNextTask = false);

            return directumTask;
        }

        private DirectumTaskWrapper InjectParallelTasks(DirectumTaskWrapper directumTask)
        {
            if (directumTask.PreviousTask != null)
                return directumTask;

            var parallelTasks = UnitOfWork
                .Repository<Model.POCOs.DirectumTask>()
                .Find(x => Equals(x.Group, directumTask.Model.Group))
                .Where(x => !Equals(x.Id, directumTask.Id))
                .OrderBy(x => x.FinishPlan)
                .Select(x => new DirectumTaskWrapper(x))
                .ToList();

            //если есть предыдущие задачи в параллельных, значит эти задачи не параллельные
            if (parallelTasks.Any(x => x.PreviousTask != null))
                return directumTask;

            if (parallelTasks.Any(x => x.Model.Parallel.Any()))
                return directumTask;

            directumTask.ParallelTasks.AddRange(parallelTasks);
            directumTask.Model.Parallel.AddRange(parallelTasks.Select(x => x.Model));
            parallelTasks.ForEach(x => InjectTasks(x));

            return directumTask;
        }
    }
}
