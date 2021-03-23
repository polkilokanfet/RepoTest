using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    public class DirectumTasksOutgoingViewModel : DirectumTasksViewModelBase
    {
        private DirectumTaskGroupLookup _selectedItem;
        public ObservableCollection<DirectumTaskGroupLookup> Items { get; } = new ObservableCollection<DirectumTaskGroupLookup>();

        public DirectumTaskGroupLookup SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                ((DelegateCommand)OpenDirectumTaskCommand).RaiseCanExecuteChanged();
            }
        }

        public DirectumTasksOutgoingViewModel(IUnityContainer container) : base(container)
        {

            OpenDirectumTaskCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters { {nameof(DirectumTaskGroup), SelectedItem.Entity} });
                },
                () => SelectedItem != null);


            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>().Subscribe(
                task =>
                {
                    if (Items.SelectMany(directumTaskGroupLookup => directumTaskGroupLookup.DirectumTasks).ContainsById(task))
                    {
                        var lookup = Items.Single(directumTaskGroupLookup => directumTaskGroupLookup.DirectumTasks.ContainsById(task));
                        RefreshLookup(lookup, task.Group);
                        return;
                    }

                    if (task.Group.Author.IsAppCurrentUser())
                    {
                        Items.Add(RefreshLookup(new DirectumTaskGroupLookup(task.Group), task.Group));
                    }
                });
        }

        private DirectumTaskGroupLookup RefreshLookup(DirectumTaskGroupLookup taskGroupLookup, DirectumTaskGroup taskGroup)
        {
            taskGroupLookup.DirectumTasks.Clear();
            taskGroupLookup.DirectumTasks.AddRange(Container.Resolve<IUnitOfWork>().Repository<Model.POCOs.DirectumTask>().Find(directumTask => directumTask.Group.Id == taskGroup.Id));
            taskGroupLookup.Refresh(taskGroup);
            return taskGroupLookup;
        }

        private List<DirectumTaskGroupLookup> _groups;
        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var tasks = ((IDirectumTaskRepository)UnitOfWork.Repository<Model.POCOs.DirectumTask>()).GetAllOfCurrentUser().ToList();
            _groups = tasks
                .Select(directumTask => directumTask.Group)
                .Distinct()
                .Select(directumTaskGroup => new DirectumTaskGroupLookup(directumTaskGroup))
                .OrderByDescending(directumTaskGroupLookup => directumTaskGroupLookup.StartAuthor)
                .ToList();
            foreach (var taskGroup in _groups)
            {
                taskGroup.DirectumTasks.AddRange(tasks.Where(directumTask => directumTask.Group.Id == taskGroup.Id));
            }
        }

        protected override void AfterGetData()
        {
            Items.Clear();
            Items.AddRange(_groups);
        }
    }
}