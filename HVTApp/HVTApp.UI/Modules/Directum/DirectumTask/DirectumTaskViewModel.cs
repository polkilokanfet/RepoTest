using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
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
                    var startMoment = DateTime.Now;
                    var parentTask = CreateDirectumTask(new DirectimTaskRouteItem {FinishPlan = Route.Items.Max(x => x.FinishPlan)}, startMoment);
                    var directumTasks = Route.Items.Count == 1
                        ? new List<DirectumTask> {CreateDirectumTask(Route.Items.First().Model, startMoment)}
                        : Route.Items.Select(x => CreateDirectumTask(x.Model, startMoment, parentTask)).ToList();

                    UnitOfWork.Repository<DirectumTask>().AddRange(directumTasks);
                    UnitOfWork.SaveChanges();

                    var afterSaveDirectumTaskEvent = Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>();
                    directumTasks.ForEach(x => afterSaveDirectumTaskEvent.Publish(x));
                },
                () => !string.IsNullOrEmpty(Title) && Route.IsValid);
        }

        public DirectumTask CreateDirectumTask(DirectimTaskRouteItem routeItem, DateTime startMoment, DirectumTask parentDirectumTask = null)
        {
            return new DirectumTask
            {
                Author = Author,
                Performer = routeItem.Performer,
                StartAuthor = startMoment,
                Title = Title,
                FinishPlan = routeItem.FinishPlan,
                ParentTask = parentDirectumTask
            };
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
