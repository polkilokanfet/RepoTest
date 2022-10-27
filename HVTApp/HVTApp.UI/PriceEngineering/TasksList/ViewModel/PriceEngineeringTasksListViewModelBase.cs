using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Items;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public abstract class PriceEngineeringTasksListViewModelBase<TTasks, TTask> : ViewModelBaseCanExportToExcel
        where TTask : PriceEngineeringTaskListItemBase
        where TTasks : PriceEngineeringTasksListItemBase<TTask>
    {
        private object _selectedItem;

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

        public IEnumerable<TTasks> Items { get; } = new ObservableCollection<TTasks>();

        public DelegateLogCommand LoadCommand { get; }

        public DelegateLogCommand OpenCommand { get; }

        protected PriceEngineeringTasksListViewModelBase(IUnityContainer container) : base(container)
        {
            LoadCommand = new DelegateLogCommand(Load);

            OpenCommand = new DelegateLogCommand(
                () =>
                {
                    object paremeter = null;
                    if (SelectedItem is TTasks tTasks)
                    {
                        paremeter = tTasks.Entity;
                    }
                    else if (SelectedItem is TTask tTask)
                    {
                        paremeter = tTask.Entity;
                    }

                    if (paremeter != null)
                    {
                        RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksView>(new NavigationParameters
                        {
                            { nameof(PriceEngineeringTasks), paremeter }
                        });
                    }
                },
                () => SelectedItem != null);

            container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Subscribe(OnItem);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTasksStartedEvent>().Subscribe(OnItem);

            container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Subscribe(OnItemChild);

            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStartedEvent>().Subscribe(OnItemChild);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskInstructedEvent>().Subscribe(OnItemChild);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedEvent>().Subscribe(OnItemChild);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedEvent>().Subscribe(OnItemChild);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Subscribe(OnItemChild);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Subscribe(OnItemChild);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStoppedEvent>().Subscribe(OnItemChild);
            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedGoToVerificationEvent>().Subscribe(OnItemChild);

            Load();
        }

        protected abstract TTasks GetItem(PriceEngineeringTasks model);

        private void OnItemChild(PriceEngineeringTask priceEngineeringTask)
        {
            var item = Items.SingleOrDefault(x => x.ChildPriceEngineeringTasks.ContainsById(priceEngineeringTask));
            if (item != null)
            {
                var itemChild = item.ChildPriceEngineeringTasks.Single(x => x.Entity.Id == priceEngineeringTask.Id);
                itemChild.Refresh(priceEngineeringTask);
                item.Refresh();
            }

            //var task = priceEngineeringTask;
            //while (task.ParentPriceEngineeringTasksId.HasValue == false)
            //{
            //    if (task.ParentPriceEngineeringTaskId.HasValue)
            //    {
            //        task = UnitOfWork.Repository<PriceEngineeringTask>().GetById(task.ParentPriceEngineeringTaskId.Value);
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

            //var priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().GetById(task.ParentPriceEngineeringTasksId.Value);

            //if (priceEngineeringTasks != null)
            //    OnItem(priceEngineeringTasks);
        }

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
        /// ������ ����� �������� ���� ViewModel
        /// </summary>
        /// <param name="engineeringTasks"></param>
        /// <returns></returns>
        protected abstract bool IsSuitable(PriceEngineeringTasks engineeringTasks);

        //private bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        //{
        //    switch (GlobalAppProperties.User.RoleCurrent)
        //    {
        //        case Role.SalesManager:
        //            return engineeringTasks.UserManager.Id == GlobalAppProperties.User.Id;
        //        case Role.Constructor:
        //            return engineeringTasks.GetSuitableTasksForWork(GlobalAppProperties.User).Any();
        //        case Role.DesignDepartmentHead:
        //            return engineeringTasks.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any();
        //    }
        //    return false;
        //}

        protected void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            ((ICollection<TTasks>)Items).Clear();
            var priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(IsSuitable);
            ((ICollection<TTasks>)Items).AddRange(priceEngineeringTasks.OrderByDescending(x => x.WorkUpTo).Select(this.GetItem));
        }
    }
}