using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    public class DirectumTasksIncomingViewModel : DirectumTasksViewModelBase
    {
        private DirectumTaskLookup _selectedItem;
        public ObservableCollection<DirectumTaskLookup> Items { get; } = new ObservableCollection<DirectumTaskLookup>();

        public DirectumTaskLookup SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OpenDirectumTaskCommand.RaiseCanExecuteChanged();
            }
        }

        public DirectumTasksIncomingViewModel(IUnityContainer container) : base(container)
        {
            OpenDirectumTaskCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { {nameof(DirectumTask), SelectedItem.Entity} });
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

                    //если задачу нужно выполнить
                    if (task.Performer.IsAppCurrentUser())
                    {
                        Items.Add(new DirectumTaskLookup(task) {Direction = "Исполнение"});
                    }

                    //если задачу нужно принять
                    if (task.FinishPerformer.HasValue &&
                        task.StartResult.HasValue &&
                        task.Group.Author.IsAppCurrentUser() && 
                        !HaveTale(task) &&
                        !Items.ContainsById(task))
                    {
                        Items.Add(new DirectumTaskLookup(task) {Direction = "Контроль"});
                    }
                });

        }

        private List<Model.POCOs.DirectumTask> _directumTasks = new List<Model.POCOs.DirectumTask>();

        private List<DirectumTaskLookup> _directumTaskLookups;
        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            _directumTasks = UnitOfWork.Repository<Model.POCOs.DirectumTask>().GetAll();

            //задачи на выполнение
            var tasksToPerform = _directumTasks
                .Where(directumTask => directumTask.Performer.Id == GlobalAppProperties.User.Id && directumTask.StartResult.HasValue)
                .Select(directumTask => new DirectumTaskLookup(directumTask) { Direction = "Исполнение" });

            //задачи на проверку
            var tasksToAccept = _directumTasks
                .Where(directumTask => directumTask.FinishPerformer.HasValue && directumTask.StartResult.HasValue && directumTask.Group.Author.Id == GlobalAppProperties.User.Id && !HaveTale(directumTask))
                .Select(directumTask => new DirectumTaskLookup(directumTask) { Direction = "Контроль" });

            _directumTaskLookups = tasksToPerform
                .Union(tasksToAccept)
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