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

namespace HVTApp.UI.Modules.Directum.ToPerform
{
    public class DirectumTasksIncomingToPerformViewModel : DirectumTasksViewModelBase
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

        public DirectumTasksIncomingToPerformViewModel(IUnityContainer container) : base(container)
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

                    //если задачу нужно выполнить
                    if (task.Performer.IsAppCurrentUser())
                    {
                        Items.Add(new DirectumTaskLookup(task) { Direction = "Исполнение" });
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

            _directumTaskLookups = tasksToPerform
                .OrderByDescending(directumTaskLookup => directumTaskLookup.StartResult)
                .ToList();
        }

        protected override void AfterGetData()
        {
            Items.Clear();
            Items.AddRange(_directumTaskLookups);
        }
    }
}