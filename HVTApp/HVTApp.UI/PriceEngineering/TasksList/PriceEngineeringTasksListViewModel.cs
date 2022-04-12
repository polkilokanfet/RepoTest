using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksListViewModel : PriceEngineeringTasksLookupListViewModel
    {
        public bool UserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool UserIsDesignDepartmentHead => GlobalAppProperties.User.RoleCurrent == Role.DesignDepartmentHead;
        public bool UserIsConstructor => GlobalAppProperties.User.RoleCurrent == Role.Constructor;

        public DelegateLogCommand LoadCommand { get; }

        public DelegateLogCommand OpenTaskCommand { get; }

        public PriceEngineeringTasksListViewModel(IUnityContainer container) : base(container)
        {
            LoadCommand = new DelegateLogCommand(Load);

            OpenTaskCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksView>(new NavigationParameters
                    {
                        { nameof(PriceEngineeringTasks), this.SelectedItem }
                    });
                }, 
                () => SelectedLookup != null);
            this.SelectedLookupChanged += lookup => OpenTaskCommand.RaiseCanExecuteChanged();

            container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Subscribe(OnPriceEngineeringTasks);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTasksStartedEvent>().Subscribe(OnPriceEngineeringTasks);

            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStartedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskInstructedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Subscribe(OnPriceEngineeringTask);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStoppedEvent>().Subscribe(OnPriceEngineeringTask);

            Load();
        }

        private void OnPriceEngineeringTask(PriceEngineeringTask priceEngineeringTask)
        {
            PriceEngineeringTask task = priceEngineeringTask;
            while (task.ParentPriceEngineeringTasksId.HasValue == false)
            {
                if (task.ParentPriceEngineeringTaskId.HasValue)
                {
                    task = UnitOfWork.Repository<PriceEngineeringTask>().GetById(task.ParentPriceEngineeringTaskId.Value);
                }
                else
                {
                    return;
                }
            }

            PriceEngineeringTasks priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(task.ParentPriceEngineeringTasksId.Value);

            if (priceEngineeringTasks != null)
                OnPriceEngineeringTasks(priceEngineeringTasks);
        }

        private void OnPriceEngineeringTasks(PriceEngineeringTasks priceEngineeringTasks)
        {
            priceEngineeringTasks = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasks.Id);
            if (IsSuitable(priceEngineeringTasks))
            {
                if (Lookups.Select(x => x.Entity.Id).Contains(priceEngineeringTasks.Id))
                {
                    var lookup = Lookups.Single(x => x.Entity.Id == priceEngineeringTasks.Id);
                    lookup.Refresh(priceEngineeringTasks);
                }
                else
                {
                    var lookup = new PriceEngineeringTasksLookup(priceEngineeringTasks);
                    this.LookupsCollection.Add(lookup);
                }
            }
        }

        private bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                    return engineeringTasks.UserManager.Id == GlobalAppProperties.User.Id;
                case Role.Constructor:
                    return engineeringTasks.GetSuitableTasksForWork(GlobalAppProperties.User).Any();
                case Role.DesignDepartmentHead:
                    return engineeringTasks.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any();
            }
            return false;
        }

        public override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(IsSuitable);
            this.Load(priceEngineeringTasks.OrderByDescending(x => x.WorkUpTo));
        }
    }
}