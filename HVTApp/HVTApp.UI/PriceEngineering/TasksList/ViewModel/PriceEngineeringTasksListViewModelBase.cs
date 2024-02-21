using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Items;
using HVTApp.UI.PriceEngineering.Tce.List.ViewModel;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public abstract class PriceEngineeringTasksListViewModelBase<TTasks, TTask> : ViewModelBaseCanExportToExcel, IIsShownActual
        where TTask : PriceEngineeringTaskListItemBase
        where TTasks : PriceEngineeringTasksListItemBase<TTask>
    {
        private object _selectedItem;
        private bool _isShownActual = true;

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (this.SetProperty(ref _selectedItem, value))
                {
                    OpenCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Показаны только актульные задачи
        /// </summary>
        public bool IsShownActual
        {
            get => _isShownActual;
            set => this.SetProperty(ref _isShownActual, value);
        }

        public IEnumerable<TTasks> Items { get; } = new ObservableCollection<TTasks>();

        public DelegateLogCommand LoadCommand { get; }

        public DelegateLogCommand OpenCommand { get; }

        protected PriceEngineeringTasksListViewModelBase(IUnityContainer container) : base(container)
        {
            LoadCommand = new DelegateLogCommand(Load);

            OpenCommand = new DelegateLogCommand(
                () =>
                {
                    object parameter = null;
                    if (SelectedItem is TTasks tTasks)
                    {
                        parameter = tTasks.Entity;
                    }
                    else if (SelectedItem is TTask tTask)
                    {
                        parameter = tTask.Entity;
                    }

                    if (parameter != null)
                    {
                        var parameters = new NavigationParameters {{nameof(PriceEngineeringTasks), parameter}};
                        switch (GlobalAppProperties.User.RoleCurrent)
                        {
                            case Role.SalesManager:
                                RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(parameters);
                                break;

                            case Role.Constructor:
                                RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewConstructor>(parameters);
                                break;

                            case Role.BackManager:
                                RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManager>(parameters);
                                break;

                            case Role.BackManagerBoss:
                                RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManagerBoss>(parameters);
                                break;

                            case Role.DesignDepartmentHead:
                                RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewDesignDepartmentHead>(parameters);
                                break;

                            case Role.PlanMaker:
                                RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewPlanMaker>(parameters);
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                },
                () => SelectedItem != null);

            container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Subscribe(OnItem);
            //container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTasksStartedEvent>().Subscribe(OnItem);

            container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Subscribe(OnItemChild);

            container.Resolve<IEventAggregator>().GetEvent<NotificationEvent>().Subscribe(OnItemChild2);

            Load();
        }

        private void OnItemChild2(NotificationUnit notificationUnit)
        {
            switch (notificationUnit.ActionType)
            {
                case NotificationActionType.PriceEngineeringTaskStart:
                case NotificationActionType.PriceEngineeringTaskStop:
                case NotificationActionType.PriceEngineeringTaskInstructToConstructor:
                case NotificationActionType.PriceEngineeringTaskFinish:
                case NotificationActionType.PriceEngineeringTaskAccept:
                case NotificationActionType.PriceEngineeringTaskRejectByManager:
                case NotificationActionType.PriceEngineeringTaskRejectByConstructorToManager:
                case NotificationActionType.PriceEngineeringTaskSendMessage:
                case NotificationActionType.PriceEngineeringTaskFinishGoToVerification:
                case NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead:
                case NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                    this.OnItemChild(this.UnitOfWork.Repository<PriceEngineeringTask>().GetById(notificationUnit.TargetEntityId));
                    break;
            }
        }

        private void OnItemChild(PriceEngineeringTask priceEngineeringTask)
        {
            var item = Items.SingleOrDefault(x => x.ChildPriceEngineeringTasks.ContainsById(priceEngineeringTask));
            if (item != null)
            {
                var itemChild = item.ChildPriceEngineeringTasks.Single(x => x.Entity.Id == priceEngineeringTask.Id);
                itemChild.Refresh(priceEngineeringTask);
                item.Refresh();
            }
        }

        protected abstract TTasks GetItem(PriceEngineeringTasks model);

        private void OnItem(PriceEngineeringTasks priceEngineeringTasks)
        {
            priceEngineeringTasks = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasks.Id);
            if (IsSuitable(priceEngineeringTasks))
            {
                if (Items.ContainsById(priceEngineeringTasks))
                {
                    var item = Items.Single(x => x.Entity.Id == priceEngineeringTasks.Id);
                    item.Refresh(priceEngineeringTasks);
                }
                else
                {
                    ((IList<TTasks>)Items).Insert(0, this.GetItem(priceEngineeringTasks));
                }
            }
        }

        /// <summary>
        /// Сборка задач подходит этой ViewModel
        /// </summary>
        /// <param name="engineeringTasks"></param>
        /// <returns></returns>
        protected abstract bool IsSuitable(PriceEngineeringTasks engineeringTasks);

        protected virtual IEnumerable<PriceEngineeringTasks> GetSuitableTasks()
        {
            return UnitOfWork.Repository<PriceEngineeringTasks>().Find(IsSuitable);
        }

        protected void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            ((ICollection<TTasks>)Items).Clear();

            var tasks = GetSuitableTasks()
                //.OrderByDescending(x => x.WorkUpTo)
                .Select(this.GetItem)
                .OrderBy(x => x.TermPriority);

            ((ICollection<TTasks>)Items).AddRange(tasks);
        }
    }

    public interface IIsShownActual
    {
        bool IsShownActual { get; }
    }
}