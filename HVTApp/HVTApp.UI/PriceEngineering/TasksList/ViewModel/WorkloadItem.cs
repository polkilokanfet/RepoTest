using System;
using System.Linq;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class WorkloadItem : BindableBase
    {
        private readonly PriceEngineeringTasksListViewModelDesignDepartmentHead _viewModel;
        private bool _showUsersTasks = true;

        public User User { get; }

        public int Workload
        {
            get
            {
                return _viewModel.Items
                    .SelectMany(x => x.ChildPriceEngineeringTasks)
                    .Select(x => x.Entity)
                    .Count(priceEngineeringTask =>
                        priceEngineeringTask.UserConstructor?.Id == User.Id &&
                        priceEngineeringTask.InProcessByConstructor);
            }
        }

        public bool ShowUsersTasks
        {
            get => _showUsersTasks;
            set
            {
                if (this.SetProperty(ref _showUsersTasks, value))
                {
                    ShowUsersTasksIsChanged?.Invoke();
                }
            }
        }

        public event Action ShowUsersTasksIsChanged;

        public WorkloadItem(User user, PriceEngineeringTasksListViewModelDesignDepartmentHead viewModel, IEventAggregator eventAggregator)
        {
            _viewModel = viewModel;
            User = user;
            eventAggregator.GetEvent<AfterSavePriceEngineeringTaskEvent>().Subscribe(OnSavePriceEngineeringTaskEvent);
        }

        private void OnSavePriceEngineeringTaskEvent(PriceEngineeringTask priceEngineeringTask)
        {
            if (priceEngineeringTask.UserConstructor?.Id == User.Id)
            {
                RaisePropertyChanged(nameof(Workload));
            }
        }
    }
}