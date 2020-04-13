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
    public class DirectumTasksOutgoingViewModel : ViewModelBase
    {
        public ObservableCollection<DirectumTaskLookup> Items { get; } = new ObservableCollection<DirectumTaskLookup>();

        public ICommand ReloadCommand { get; }
        public ICommand CreateDirectumTaskCommand { get; }

        public DirectumTasksOutgoingViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);

            CreateDirectumTaskCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<DirectumTaskView>(new NavigationParameters());
                });

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDirectumTaskEvent>().Subscribe(
                task =>
                {
                    if (Items.ContainsById(task))
                    {
                        var lookup = Items.Single(x => x.Id == task.Id);
                        lookup.Refresh(task);
                        return;
                    }

                    if (task.Group.Author.Id == GlobalAppProperties.User.Id)
                    {
                        Items.Add(new DirectumTaskLookup(task));
                    }
                });

            Load();
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var tasks = UnitOfWork.Repository<DirectumTask>().Find(x => x.Group.Author.Id == GlobalAppProperties.User.Id);

            Items.Clear();
            Items.AddRange(tasks.Select(x => new DirectumTaskLookup(x)));
        }
    }
}