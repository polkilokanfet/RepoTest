using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
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
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand)OpenDirectumTaskCommand).RaiseCanExecuteChanged();
            }
        }

        public DirectumTasksIncomingViewModel(IUnityContainer container) : base(container)
        {
            OpenDirectumTaskCommand = new DelegateCommand(
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
                        Items.Where(x => x.Id == task.Id).ForEach(x => { x.Refresh(task); });
                        return;
                    }

                    //если задачу нужно выполнить
                    if (task.Performer.Id == GlobalAppProperties.User.Id)
                    {
                        Items.Add(new DirectumTaskLookup(task) {Direction = "Исполнение"});
                    }

                    //если задачу нужно принять
                    if (task.FinishPerformer.HasValue &&
                        task.StartResult.HasValue &&
                        task.Group.Author.Id == GlobalAppProperties.User.Id && 
                        !HaveTale(task) &&
                        !Items.ContainsById(task))
                    {
                        Items.Add(new DirectumTaskLookup(task) {Direction = "Контроль"});
                    }
                });

            Load();
        }

        private List<Model.POCOs.DirectumTask> _directumTasks = new List<Model.POCOs.DirectumTask>();

        protected override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            _directumTasks = UnitOfWork.Repository<Model.POCOs.DirectumTask>().GetAll();

            //задачи на выполнение
            var tasksToPerform = _directumTasks
                .Where(x => x.Performer.Id == GlobalAppProperties.User.Id && x.StartResult.HasValue)
                .Select(x => new DirectumTaskLookup(x) {Direction = "Исполнение"});

            //задачи на проверку
            var tasksToAccept = _directumTasks
                .Where(x => x.FinishPerformer.HasValue && x.StartResult.HasValue && x.Group.Author.Id == GlobalAppProperties.User.Id && !HaveTale(x))
                .Select(x => new DirectumTaskLookup(x) {Direction = "Контроль"});

            Items.Clear();
            Items.AddRange(tasksToPerform.Union(tasksToAccept).OrderByDescending(x => x.StartResult));
        }

        /// <summary>
        /// Задача не является последней в цепочке последовательных задач
        /// </summary>
        /// <param name="directumTask"></param>
        /// <returns></returns>
        private bool HaveTale(Model.POCOs.DirectumTask directumTask)
        {
            return _directumTasks.Any(x => x.PreviousTask?.Id == directumTask.Id);
        }
    }
}