using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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
            get { return _selectedItem; }
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
                    if (Items.SelectMany(x => x.DirectumTasks).ContainsById(task))
                    {
                        var lookup = Items.Single(x => x.DirectumTasks.ContainsById(task));
                        RefreshLookup(lookup, task.Group);
                        return;
                    }

                    if (task.Group.Author.Id == GlobalAppProperties.User.Id)
                    {
                        Items.Add(RefreshLookup(new DirectumTaskGroupLookup(task.Group), task.Group));
                    }
                });

            Load();
        }

        private DirectumTaskGroupLookup RefreshLookup(DirectumTaskGroupLookup taskGroupLookup, DirectumTaskGroup taskGroup)
        {
            taskGroupLookup.DirectumTasks.Clear();
            taskGroupLookup.DirectumTasks.AddRange(Container.Resolve<IUnitOfWork>().Repository<Model.POCOs.DirectumTask>().Find(x => x.Group.Id == taskGroup.Id));
            taskGroupLookup.Refresh(taskGroup);
            return taskGroupLookup;
        }

        protected override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var tasks = UnitOfWork.Repository<Model.POCOs.DirectumTask>().Find(x => x.Group.Author.Id == GlobalAppProperties.User.Id);
            var groups = tasks.Select(x => x.Group).Distinct().Select(x => new DirectumTaskGroupLookup(x)).OrderByDescending(x => x.StartAuthor).ToList();
            foreach (var taskGroup in groups)
            {
                taskGroup.DirectumTasks.AddRange(tasks.Where(x => x.Group.Id == taskGroup.Id));
            }

            Items.Clear();
            Items.AddRange(groups);
        }
    }
}