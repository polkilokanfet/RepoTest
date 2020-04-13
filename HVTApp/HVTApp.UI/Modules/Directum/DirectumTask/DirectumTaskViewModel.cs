using System;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Directum
{
    public class DirectumTaskViewModel : ViewModelBase
    {
        private User _author;
        private string _title;

        /// <summary>
        /// Задача
        /// </summary>
        //public DirectumTaskWrapper DirectumTask { get; private set; }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
                ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
            }
        }

        public User Author
        {
            get { return _author; }
            private set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Маршрут
        /// </summary>
        public DirectumTaskRouteWrapper Route { get; private set; } = new DirectumTaskRouteWrapper(new DirectumTaskRoute());

        /// <summary>
        /// Выбор маршрута
        /// </summary>
        public ICommand RouteCommand { get; }

        /// <summary>
        /// Старт задачи
        /// </summary>
        public ICommand StartCommand { get; }

        public DirectumTaskViewModel(IUnityContainer container) : base(container)
        {
            RouteCommand = new DelegateCommand(
                () =>
                {
                    var viewModel = new DirectumTaskRouteViewModel(Container, Route);
                    var dr = Container.Resolve<IDialogService>().ShowDialog(viewModel);

                    if (dr.HasValue && dr.Value)
                    {
                        Route.AcceptChanges();
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
                    var directumTaskGroup = new DirectumTaskGroup()
                    {
                        Author = unitOfWork.Repository<User>().GetById(Author.Id),
                        StartAuthor = DateTime.Now,
                        Title = Title
                    };

                    var directumTasks = Route.Items.Select(
                        routeItem => new DirectumTask
                        {
                            Group = directumTaskGroup,
                            Performer = unitOfWork.Repository<User>().GetById(routeItem.Performer.Id),
                            FinishPlan = routeItem.FinishPlan,
                        }).ToList();

                    unitOfWork.Repository<DirectumTask>().AddRange(directumTasks);
                    unitOfWork.SaveChanges();

                    var afterSaveDirectumTaskEvent = Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>();
                    directumTasks.ForEach(x => afterSaveDirectumTaskEvent.Publish(x));

                    GoBackCommand.Execute(null);
                },
                () => !string.IsNullOrEmpty(Title) && Route.IsValid);
        }

        public void Load()
        {
            Title = "Новая задача";
            Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
        }

        public void Load(Guid directumTaskId)
        {
            throw new NotImplementedException();
        }
    }
}
