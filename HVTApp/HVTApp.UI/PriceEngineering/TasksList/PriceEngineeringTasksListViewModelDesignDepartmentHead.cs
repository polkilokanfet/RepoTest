using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksListViewModelDesignDepartmentHead : ViewModelBaseCanExportToExcel
    {
        private PriceEngineeringTaskLookup _selectedPriceEngineeringTask;

        public ObservableCollection<PriceEngineeringTaskLookup> PriceEngineeringTasks { get; } = new ObservableCollection<PriceEngineeringTaskLookup>();

        public PriceEngineeringTaskLookup SelectedPriceEngineeringTask
        {
            get => _selectedPriceEngineeringTask;
            set
            {
                _selectedPriceEngineeringTask = value;
                OpenTaskCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand LoadCommand { get; }
        public DelegateLogCommand OpenTaskCommand { get; }

        public PriceEngineeringTasksListViewModelDesignDepartmentHead(IUnityContainer container) : base(container)
        {
            LoadCommand = new DelegateLogCommand(Load);

            OpenTaskCommand = new DelegateLogCommand(
                () =>
                {
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceEngineeringTasksView>(new NavigationParameters
                    {
                        { nameof(PriceEngineeringTask), this.SelectedPriceEngineeringTask.Entity }
                    });
                },
                () => this.SelectedPriceEngineeringTask != null);

            #region IEventAggregator

            container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Subscribe(OnPriceEngineeringTasks);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTasksStartedEvent>().Subscribe(OnPriceEngineeringTasks);

            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStartedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskInstructedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStoppedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedGoToVerificationEvent>().Subscribe(OnPriceEngineeringTask);

            #endregion

            Load();
        }

        public void Load()
        {
            this.PriceEngineeringTasks.Clear();

            var priceEngineeringTasks = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>()
                .GetAll()
                .SelectMany(priceEngineeringTask => priceEngineeringTask.GetSuitableTasksForInstruct(GlobalAppProperties.User))
                .Distinct()
                .ToList();

            PriceEngineeringTasks.AddRange(priceEngineeringTasks.Select(priceEngineeringTask => new PriceEngineeringTaskLookup(priceEngineeringTask)));
            SelectedPriceEngineeringTask = null;
        }

        private void OnPriceEngineeringTask(PriceEngineeringTask priceEngineeringTask)
        {
            var lookup = this.PriceEngineeringTasks.SingleOrDefault(x => x.Id == priceEngineeringTask.Id);
            lookup?.Refresh(priceEngineeringTask);
        }

        private void OnPriceEngineeringTasks(PriceEngineeringTasks priceEngineeringTasks)
        {
            priceEngineeringTasks = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasks.Id);
            var suitableTasks = priceEngineeringTasks.GetSuitableTasksForInstruct(GlobalAppProperties.User);

            foreach (var suitableTask in suitableTasks)
            {
                if (PriceEngineeringTasks.Select(x => x.Entity.Id).Contains(suitableTask.Id))
                {
                    this.OnPriceEngineeringTask(suitableTask);
                    continue;
                }

                var lookup = new PriceEngineeringTaskLookup(suitableTask);
                this.PriceEngineeringTasks.Add(lookup);
            }
        }
    }
}