using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public abstract class PriceEngineeringTasksViewModelVisible<TPriceEngineeringTasksWrapper, TPriceEngineeringTaskViewModel> : PriceEngineeringTasksViewModel<TPriceEngineeringTasksWrapper, TPriceEngineeringTaskViewModel>
        where TPriceEngineeringTasksWrapper : PriceEngineeringTasksContainerWrapper<TPriceEngineeringTaskViewModel>
        where TPriceEngineeringTaskViewModel : TaskViewModel<>
    {
        private Guid? _taskId;
        private bool? _allTasksAreVisible = null;

        public bool? AllTasksAreVisible
        {
            get => _allTasksAreVisible;
            set
            {
                switch (value)
                {
                    case true:
                        this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.ForEach(x => x.IsVisible = true);
                        break;
                    case false:
                        this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.ForEach(x => x.IsVisible = this.ChildTaskIsVisibleByDefault(x.Model));
                        break;
                    case null:
                        if (_taskId.HasValue)
                            this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.ForEach(x => x.IsVisible = x.Model.GetAllPriceEngineeringTasks().Any(t => t.Id == _taskId.Value));
                        break;
                }

                _allTasksAreVisible = value;
                RaisePropertyChanged();
            }
        }

        protected PriceEngineeringTasksViewModelVisible(IUnityContainer container) : base(container)
        {
        }

        public override void Load(PriceEngineeringTasks priceEngineeringTasks)
        {
            base.Load(priceEngineeringTasks);
            AllTasksAreVisible = false;
        }

        public override void Load(PriceEngineeringTask priceEngineeringTask)
        {
            base.Load(priceEngineeringTask);
            this._taskId = priceEngineeringTask.Id;
            AllTasksAreVisible = null;
        }

        private bool ChildTaskIsVisibleByDefault(PriceEngineeringTask priceEngineeringTask)
        {
            var user = GlobalAppProperties.User;
            switch (user.RoleCurrent)
            {
                case Role.SalesManager:
                    return true;
                case Role.Constructor:
                    return priceEngineeringTask.GetSuitableTasksForWork(user).Any();
                case Role.DesignDepartmentHead:
                    return priceEngineeringTask.GetSuitableTasksForInstruct(user).Any();
                default:
                    return false;
            }
        }
    }
}