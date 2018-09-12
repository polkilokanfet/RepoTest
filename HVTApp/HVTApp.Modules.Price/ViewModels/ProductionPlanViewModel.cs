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
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Price.ViewModels
{
    public class ProductionPlanViewModel : LoadableBindableBase
    {
        private List<SalesUnitWrapper> _unitsToPlan;
        private ProductionGroup _selectedGroupToPlan;
        private Order _selectedOrder;

        public ObservableCollection<ProductionGroup> GroupsInPlan { get; } = new ObservableCollection<ProductionGroup>();
        public ObservableCollection<ProductionGroup> GroupsToPlan { get; } = new ObservableCollection<ProductionGroup>();
        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                ((DelegateCommand)SetInPlanCommand).RaiseCanExecuteChanged();
            }
        }

        public ProductionGroup SelectedGroupToPlan
        {
            get { return _selectedGroupToPlan; }
            set
            {
                _selectedGroupToPlan = value;
                ((DelegateCommand)SetInPlanCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand SetInPlanCommand { get; }

        public ProductionPlanViewModel(IUnityContainer container) : base(container)
        {
            Action save = async () => await UnitOfWork.SaveChangesAsync();
            Func<bool> canSave = () => _unitsToPlan != null && _unitsToPlan.Any(x => x.IsChanged);
            SaveCommand = new DelegateCommand(save, canSave);
            
            SetInPlanCommand = new DelegateCommand(SetInPlanCommand_Execute, SetInPlanCommand_CanExecute);
        }

        private bool SetInPlanCommand_CanExecute()
        {
            return SelectedGroupToPlan?.Unit.EndProductionPlanDate != null;
        }

        private void SetInPlanCommand_Execute()
        {
            SelectedGroupToPlan.SetSignalToStartProductionDone();
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

            var unitsInPlan = units.Where(x => x.SignalToStartProductionDone != null).ToList();
            _unitsToPlan = units.Except(unitsInPlan).Where(x => x.SignalToStartProduction != null).Select(x => new SalesUnitWrapper(x)).ToList();
            _unitsToPlan?.ForEach(x => x.PropertyChanged += OnPropertyChanged);

            GroupsInPlan.Clear();
            GroupsInPlan.AddRange(ProductionGroup.Grouping(unitsInPlan.Select(x => new SalesUnitWrapper(x))));

            GroupsToPlan.Clear();
            GroupsToPlan.AddRange(ProductionGroup.Grouping(_unitsToPlan));

            var orders = await UnitOfWork.GetRepository<Order>().GetAllAsNoTrackingAsync();
            Orders.Clear();
            Orders.AddRange(orders.OrderBy(x => x.OpenOrderDate));
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
