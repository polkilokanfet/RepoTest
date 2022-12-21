using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Tce.Second.View;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.Tce.List.ViewModel
{
    public abstract class PriceEngineeringTasksTceViewModel : PriceEngineeringTasksLookupListViewModel
    {
        public DelegateLogCommand EditCommand { get; }

        public DelegateLogCommand ReloadCommand { get; }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.User.RoleCurrent == Role.BackManager;
        public bool CurrentUserIsBackManagerBoss => GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss;

        protected PriceEngineeringTasksTceViewModel(IUnityContainer container) : base(container)
        {
            Load();

            this.SelectedLookupChanged += lookup =>
            {
                EditCommand.RaiseCanExecuteChanged();
            };

            #region Commands

            EditCommand = new DelegateLogCommand(
                () =>
                {
                    //RegionManager.RequestNavigateContentRegion<TasksTceView>(new NavigationParameters { { nameof(PriceEngineeringTasks), SelectedItem } });
                    RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksView>(new NavigationParameters { { nameof(PriceEngineeringTasks), SelectedItem } });
                },
                () => SelectedItem != null);

            ReloadCommand = new DelegateLogCommand(Load);

            #endregion
        }

        /// <summary>
        /// Подходит ли задача конкретно этой VM
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        protected abstract bool TaskIsActual(PriceEngineeringTasks tasks);

        public override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var tasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(TaskIsActual);
            this.Load(tasks.OrderByDescending(x => x.StartMoment));
        }

        protected override void OnAfterSaveEntity(PriceEngineeringTasks tasks)
        {
            if (TaskIsActual(tasks))
            {
                var selectedLookup = this.SelectedLookup;
                base.OnAfterSaveEntity(tasks);
                this.SelectedLookup = selectedLookup;
            }
        }

    }
}