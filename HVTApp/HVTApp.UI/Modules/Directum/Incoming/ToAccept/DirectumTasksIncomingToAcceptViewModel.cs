using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum.ToAccept
{
    public class DirectumTasksIncomingToAcceptViewModel : DirectumTasksViewModelBase
    {
        private DirectumTaskLookup _selectedItem;
        public ObservableCollection<DirectumTaskLookup> Items { get; } = new ObservableCollection<DirectumTaskLookup>();

        public DirectumTaskLookup SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                ((DelegateCommand)OpenDirectumTaskCommand).RaiseCanExecuteChanged();
            }
        }

        public DirectumTasksIncomingToAcceptViewModel(IUnityContainer container) : base(container)
        {
            OpenDirectumTaskCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { { nameof(DirectumTask), SelectedItem.Entity } });
                },
                () => SelectedItem != null);

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>().Subscribe(
                task =>
                {
                    //добавляем задачу, если она новая
                    _directumTasks.ReAddById(task);

                    //если задача уже в отображаемом списке
                    if (Items.ContainsById(task))
                    {
                        Items.Where(directumTaskLookup => directumTaskLookup.Id == task.Id).ForEach(directumTaskLookup => { directumTaskLookup.Refresh(task); });
                        return;
                    }

                    //если задачу нужно принять
                    if (task.FinishPerformer.HasValue &&
                        task.StartResult.HasValue &&
                        task.Group.Author.IsAppCurrentUser() &&
                        !HaveTale(task) &&
                        !Items.ContainsById(task))
                    {
                        Items.Add(new DirectumTaskLookup(task) { Direction = "Контроль" });
                    }
                });

        }

        private List<Model.POCOs.DirectumTask> _directumTasks = new List<Model.POCOs.DirectumTask>();

        private List<DirectumTaskLookup> _directumTaskLookups;
        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            _directumTasks = UnitOfWork.Repository<Model.POCOs.DirectumTask>().GetAll();

            //задачи на проверку
            var tasksToAccept = _directumTasks
                .Where(directumTask => directumTask.FinishPerformer.HasValue && directumTask.StartResult.HasValue && directumTask.Group.Author.Id == GlobalAppProperties.User.Id && !HaveTale(directumTask))
                .Select(directumTask => new DirectumTaskLookup(directumTask) { Direction = "Контроль" });

            _directumTaskLookups = tasksToAccept
                .OrderByDescending(directumTaskLookup => directumTaskLookup.StartResult)
                .ToList();
        }

        protected override void AfterGetData()
        {
            Items.Clear();
            Items.AddRange(_directumTaskLookups);
        }

        /// <summary>
        /// Задача не является последней в цепочке последовательных задач
        /// </summary>
        /// <param name="directumTask"></param>
        /// <returns></returns>
        private bool HaveTale(Model.POCOs.DirectumTask directumTask)
        {
            return _directumTasks.Any(task => task.PreviousTask?.Id == directumTask.Id);
        }
    }
}