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
                    if (Items.ContainsById(task))
                    {
                        var lookup = Items.Single(x => x.Id == task.Id);
                        lookup.Refresh(task);
                        return;
                    }

                    if (task.Performer.Id == GlobalAppProperties.User.Id)
                    {
                        Items.Add(new DirectumTaskLookup(task));
                    }
                });

            Load();
        }

        protected override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var tasks = UnitOfWork.Repository<Model.POCOs.DirectumTask>().Find(x => x.Performer.Id == GlobalAppProperties.User.Id);

            Items.Clear();
            Items.AddRange(tasks.Select(x => new DirectumTaskLookup(x)));
        }
    }
}