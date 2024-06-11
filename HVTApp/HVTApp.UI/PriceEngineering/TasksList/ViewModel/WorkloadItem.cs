using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class WorkloadItem : BindableBase
    {
        private readonly PriceEngineeringTasksListViewModelDesignDepartmentHead _viewModel;
        private bool _showUsersTasks = true;

        public User User { get; }

        /// <summary>
        /// Задачи в проработке
        /// </summary>
        public IEnumerable<PriceEngineeringTasksListItemDesignDepartmentHead> TasksInWork =>
            _viewModel.Items.Where(listItem => listItem.Entity.ChildPriceEngineeringTasks.SelectMany(task => task.GetAllPriceEngineeringTasks()).Any(task => task.UserConstructor?.Id == User.Id && task.IsInProcessByConstructor));

        public int Workload => TasksInWork.Count();

        public bool ShowUsersTasks
        {
            get => _showUsersTasks;
            set
            {
                if (this.SetProperty(ref _showUsersTasks, value))
                    ShowUsersTasksIsChanged?.Invoke();
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
                RaisePropertyChanged(nameof(TasksInWork));
                RaisePropertyChanged(nameof(Workload));
            }
        }
    }
}