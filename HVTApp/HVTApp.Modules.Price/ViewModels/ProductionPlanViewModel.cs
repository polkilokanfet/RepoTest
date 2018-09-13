using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Price.ViewModels
{
    public class ProductionPlanViewModel : LoadableBindableBase
    {
        private List<SalesUnitWrapper> _unitsToPlan;
        private List<Order> _orders;
        private ProductionGroup _selectedGroupToPlan;

        public OrderLookupListViewModel OrderListViewModel { get; private set; }
        public ObservableCollection<ProductionGroup> GroupsInPlan { get; } = new ObservableCollection<ProductionGroup>();
        public ObservableCollection<ProductionGroup> GroupsToPlan { get; } = new ObservableCollection<ProductionGroup>();

        public Order SelectedOrder => OrderListViewModel.SelectedItem;

        public ProductionGroup SelectedGroupToPlan
        {
            get { return _selectedGroupToPlan; }
            set
            {
                _selectedGroupToPlan = value;
                ((DelegateCommand)SetInPlanCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand ReloadCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SetInPlanCommand { get; }
        public ICommand NewOrderCommand { get; }
        public ICommand EditOrderCommand { get; }

        public ProductionPlanViewModel(IUnityContainer container) : base(container)
        {
            OrderListViewModel = Container.Resolve<OrderLookupListViewModel>();

            Func<bool> canSave = () => _unitsToPlan != null && _unitsToPlan.Any(x => x.IsChanged);
            SaveCommand = new DelegateCommand(SaveCommand_Execute, canSave);
            
            SetInPlanCommand = new DelegateCommand(SetInPlanCommand_Execute, SetInPlanCommand_CanExecute);
            ReloadCommand = new DelegateCommand(async () => await LoadedAsyncMethod());

            NewOrderCommand = OrderListViewModel.NewItemCommand;
            EditOrderCommand = OrderListViewModel.EditItemCommand;
        }

        private async void SaveCommand_Execute()
        {
            //откатываем недозаполненные
            _unitsToPlan.Where(x => !x.SignalToStartProduction.HasValue).ToList().ForEach(x => x.RejectChanges());
            await UnitOfWork.SaveChangesAsync();
            await LoadedAsyncMethod();
        }

        private bool SetInPlanCommand_CanExecute()
        {
            return SelectedGroupToPlan?.Unit.EndProductionPlanDate != null && SelectedOrder != null;
        }

        private async void SetInPlanCommand_Execute()
        {
            //фиксируем дату действия и заказ
            SelectedGroupToPlan.SetSignalToStartProductionDone();
            //подменяем заказ
            SelectedGroupToPlan.Order = new OrderWrapper(await UnitOfWork.GetRepository<Order>().GetByIdAsync(SelectedOrder.Id));

            //переносим заказ в план производства
            GroupsInPlan.Add(SelectedGroupToPlan);
            if (GroupsToPlan.Contains(SelectedGroupToPlan))
            {
                GroupsToPlan.Remove(SelectedGroupToPlan);
            }
            else
            {
                var group = GroupsToPlan.Single(x => x.Groups.Contains(SelectedGroupToPlan));
                group.Groups.Remove(SelectedGroupToPlan);
                if (!group.Groups.Any())
                    GroupsToPlan.Remove(group);
            }
        }

        protected override async Task LoadedAsyncMethod()
        {
            _unitsToPlan?.ForEach(x => x.PropertyChanged -= OnPropertyChanged);

            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var units = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync();
            _orders = await UnitOfWork.GetRepository<Order>().GetAllAsync();

            var unitsInPlan = units.Where(x => x.SignalToStartProductionDone != null).ToList();
            _unitsToPlan = units.Except(unitsInPlan).Where(x => x.SignalToStartProduction != null).Select(x => new SalesUnitWrapper(x)).ToList();
            _unitsToPlan?.ForEach(x => x.PropertyChanged += OnPropertyChanged);

            GroupsInPlan.Clear();
            GroupsInPlan.AddRange(ProductionGroup.Grouping(unitsInPlan.Select(x => new SalesUnitWrapper(x))));

            GroupsToPlan.Clear();
            GroupsToPlan.AddRange(ProductionGroup.Grouping(_unitsToPlan));

            OrderListViewModel.SelectedLookupChanged += OrderListViewModelOnSelectedLookupChanged;
            await OrderListViewModel.LoadAsync();

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SetInPlanCommand).RaiseCanExecuteChanged();
        }

        private void OrderListViewModelOnSelectedLookupChanged(OrderLookup orderLookup)
        {
            ((DelegateCommand)SetInPlanCommand).RaiseCanExecuteChanged();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)SetInPlanCommand).RaiseCanExecuteChanged();
        }
    }
}
