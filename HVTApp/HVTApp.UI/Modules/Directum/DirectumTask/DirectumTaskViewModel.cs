using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    public class DirectumTaskViewModel : ViewModelBase
    {
        private bool _taskIsNew = false;
        private bool _taskIsSubTask = false;
        private DirectumTaskRouteWrapper _route = new DirectumTaskRouteWrapper(new DirectumTaskRoute());
        private DirectumTaskWrapper _directumTask = new DirectumTaskWrapper(new Model.POCOs.DirectumTask {Group = new DirectumTaskGroup {StartAuthor = DateTime.Now} });
        private DirectumTaskMessageWrapper _message;

        public DirectumTaskWrapper DirectumTask
        {
            get { return _directumTask; }
            private set
            {
                _directumTask = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Маршрут
        /// </summary>
        public DirectumTaskRouteWrapper Route
        {
            get { return _route; }
            private set
            {
                _route = value;
                OnPropertyChanged();
            }
        }

        public bool TaskIsNew
        {
            get { return _taskIsNew; }
            private set
            {
                _taskIsNew = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllowEditTitle));
                OnPropertyChanged(nameof(AllowSubTask));
            }
        }

        public bool TaskIsSubTask
        {
            get { return _taskIsSubTask; }
            private set
            {
                _taskIsSubTask = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllowEditTitle));
            }
        }

        public bool AllowEditTitle => TaskIsNew && !TaskIsSubTask;

        public bool AllowSubTask => !TaskIsNew;

        public bool AllowPerform => DirectumTask?.Performer != null && DirectumTask.Performer.Id == GlobalAppProperties.User.Id && !DirectumTask.FinishPerformer.HasValue;

        public bool AllowAccept => DirectumTask?.Performer != null && DirectumTask.Group.Author.Id == GlobalAppProperties.User.Id && DirectumTask.FinishPerformer.HasValue && !DirectumTask.FinishAuthor.HasValue;

        public bool AllowPerformOrAccept => AllowPerform || AllowAccept;

        /// <summary>
        /// Выбор маршрута
        /// </summary>
        public ICommand RouteCommand { get; }

        /// <summary>
        /// Старт задачи
        /// </summary>
        public ICommand StartCommand { get; }

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

        public DirectumTaskViewModel(IUnityContainer container) : base(container)
        {
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
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var directumTaskGroup = new DirectumTaskGroup
                    {
                        Author = unitOfWork.Repository<User>().GetById(DirectumTask.Group.Author.Id),
                        StartAuthor = DateTime.Now,
                        Title = DirectumTask.Group.Title,
                        Message = DirectumTask.Group.Message
                    };

                    var directumTasks = Route.Items.Select(
                        routeItem => new Model.POCOs.DirectumTask
                        {
                            Group = directumTaskGroup,
                            Performer = unitOfWork.Repository<User>().GetById(routeItem.Performer.Id),
                            FinishPlan = routeItem.FinishPlan,
                            ParentTask = _parentTask == null ? null : unitOfWork.Repository<Model.POCOs.DirectumTask>().GetById(_parentTask.Id)
                        }).ToList();

                    unitOfWork.Repository<Model.POCOs.DirectumTask>().AddRange(directumTasks);
                    unitOfWork.SaveChanges();

                    var afterSaveDirectumTaskEvent = Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>();
                    directumTasks.ForEach(x => afterSaveDirectumTaskEvent.Publish(x));

                    DirectumTask = null; //костыль, чтобы не возмущался при выходе
                    GoBackCommand.Execute(null);
                },
                () => !string.IsNullOrEmpty(DirectumTask?.Group.Title) && !string.IsNullOrEmpty(DirectumTask.Group.Message) && Route.IsValid);

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

                    GoBackCommand.Execute(null);
                },
                () => AllowPerform && DirectumTask.IsValid);

            AcceptCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Информация", "Вы уверены, что хотите принять выполнение задачи?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    var moment = DateTime.Now;
                    Message.Moment = moment;
                    DirectumTask.FinishAuthor = moment;
                    DirectumTask.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>().Publish(DirectumTask.Model);
                    GoBackCommand.Execute(null);
                },
                () => AllowAccept && DirectumTask.IsValid);

            RejectCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Информация", "Вы уверены, что хотите вернуть на доработку задачу?", defaultNo: true);
                    if (dr != MessageDialogResult.Yes) return;

                    var moment = DateTime.Now;
                    Message.Moment = moment;
                    DirectumTask.FinishPerformer = default(DateTime?);
                    DirectumTask.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>().Publish(DirectumTask.Model);
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
        }

        /// <summary>
        /// Создание новой задачи
        /// </summary>
        public void Load()
        {
            TaskIsNew = true;
            DirectumTask.Group.Title = "Новая задача";
            DirectumTask.Group.Message = "Сделай всё красиво.";
            DirectumTask.Group.Author = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
        }

        public DirectumTaskMessageWrapper Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Загрузка существующей задачи
        /// </summary>
        public void Load(DirectumTaskGroup directumTaskGroup)
        {
            var tasks = UnitOfWork.Repository<Model.POCOs.DirectumTask>().Find(x => x.Group.Id == directumTaskGroup.Id);
            if(tasks.Any(x => !x.FinishAuthor.HasValue))
                Load(tasks.First(x => !x.FinishAuthor.HasValue));
            else
                Load(tasks.First());
        }

        private Model.POCOs.DirectumTask _parentTask;

        public void Load(Model.POCOs.DirectumTask parentTask, bool isSubTask)
        {
            _parentTask = parentTask;
            Load();
            DirectumTask.Group.Title = $"{_parentTask.Group.Title} [подзадача]";
            TaskIsSubTask = true;
        }


        /// <summary>
        /// Загрузка существующей задачи
        /// </summary>
        /// <param name="directumTask"></param>
        public void Load(Model.POCOs.DirectumTask directumTask)
        {
            TaskIsNew = false;

            DirectumTask = new DirectumTaskWrapper(UnitOfWork.Repository<Model.POCOs.DirectumTask>().GetById(directumTask.Id));
            InjectTasks(DirectumTask);

            //если есть возможность выполнить задачу
            if (!DirectumTask.FinishPerformer.HasValue && DirectumTask.Performer.Id == GlobalAppProperties.User.Id)
            {
                Message = new DirectumTaskMessageWrapper(new DirectumTaskMessage())
                {
                    Author = new UserWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id)),
                    Moment = DateTime.Now,
                    Message = "Задание выполнено, командир."
                };

                DirectumTask.Messages.Add(Message);
                Message.PropertyChanged += (sender, args) => { ((DelegateCommand)PerformCommand).RaiseCanExecuteChanged(); };
                ((DelegateCommand)PerformCommand).RaiseCanExecuteChanged();
            }

            //если есть возможность принять задачу
            if (DirectumTask.FinishPerformer.HasValue && !DirectumTask.FinishAuthor.HasValue && DirectumTask.Group.Author.Id == GlobalAppProperties.User.Id)
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

            GetRoute();

            OnPropertyChanged(nameof(AllowPerformOrAccept));
        }

        private void GetRoute()
        {
            var parallelTasks = DirectumTask.Model.ParallelTasks.ToList();
            parallelTasks.Add(DirectumTask.Model);

            var route = new DirectumTaskRoute();
            route.Items.AddRange(parallelTasks
                .OrderBy(x => x.FinishPlan)
                .Select(x => new DirectumTaskRouteItem
                {
                    FinishPlan = x.FinishPlan,
                    Performer = x.Performer,
                    Index = 1
                })
                .ToList());
            Route = new DirectumTaskRouteWrapper(route);
        }


        /// <summary>
        /// Внедряем в задачи все дочерние задачи
        /// </summary>
        /// <param name="directumTask"></param>
        /// <returns></returns>
        private DirectumTaskWrapper InjectTasks(DirectumTaskWrapper directumTask)
        {
            directumTask.ChildTasks.AddRange(UnitOfWork
                .Repository<Model.POCOs.DirectumTask>()
                .Find(x => Equals(x.ParentTask, directumTask.Model))
                .Select(x => InjectTasks(new DirectumTaskWrapper(x))));

            var parallelTasks = UnitOfWork
                .Repository<Model.POCOs.DirectumTask>()
                .Find(x => Equals(x.Group, directumTask.Model.Group))
                .Where(x => !Equals(x.Id, directumTask.Id))
                .Select(x => new DirectumTaskWrapper(x))
                .ToList();

            if (parallelTasks.Any(x => x.Model.ParallelTasks.Any()))
                return directumTask;

            directumTask.ParallelTasks.AddRange(parallelTasks);
            directumTask.Model.ParallelTasks.AddRange(parallelTasks.Select(x => x.Model));
            parallelTasks.ForEach(x => InjectTasks(x));

            return directumTask;
        }
    }
}
