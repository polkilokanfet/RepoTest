using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.ViewModels.Groups;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class ProductionPlanViewModel : OrderLookupListViewModel
    {
        private IUnitOfWork _unitOfWork;

        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitsWrappers;

        public GroupsContainer<ProductionGroup> GroupsInOrder { get; } = new GroupsContainer<ProductionGroup>();
        public GroupsContainer<ProductionGroup> GroupsPotential { get; } = new GroupsContainer<ProductionGroup>();

        public ICommand ReloadCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand AddInOrderCommand { get; }

        public ICommand ShowProductStructureCommand { get; }

        public ProductionPlanViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(async () =>
                {
                    _salesUnitsWrappers.AcceptChanges();
                    await _unitOfWork.SaveChangesAsync();
                }, 
                () => _salesUnitsWrappers != null && _salesUnitsWrappers.IsChanged && _salesUnitsWrappers.IsValid);
            
            AddInOrderCommand = new DelegateCommand(AddInOrderCommand_Execute, () => GroupsPotential.SelectedGroup != null && SelectedItem != null);
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());

            ShowProductStructureCommand = new DelegateCommand(() =>
                {
                    var productStructure = new ProductStructureViewModel(GroupsPotential.SelectedGroup.Unit.Model);
                    Container.Resolve<IDialogService>().Show(productStructure);
                }, 
                () => GroupsPotential.SelectedGroup != null);
        }

        private async void AddInOrderCommand_Execute()
        {
            //фиксируем дату действия и заказ
            GroupsPotential.SelectedGroup.SetSignalToStartProductionDone();
            //подменяем заказ
            GroupsPotential.SelectedGroup.Order = new OrderWrapper(await _unitOfWork.Repository<Order>().GetByIdAsync(SelectedItem.Id));
            //ставим предполагаемую дату производства
            GroupsPotential.SelectedGroup.EndProductionPlanDate = GroupsPotential.SelectedGroup.Unit.DeliveryDateExpected;
            //ставим позиции заказа
            int pos = 1;
            if (GroupsPotential.SelectedGroup.Groups.Any())
            {
                foreach (var productionGroup in GroupsPotential.SelectedGroup.Groups)
                {
                    productionGroup.OrderPosition = (pos++).ToString();
                }
            }
            else
            {
                GroupsPotential.SelectedGroup.OrderPosition = (pos).ToString();
            }

            //переносим заказ в план производства
            GroupsInOrder.Add(GroupsPotential.SelectedGroup);
            if (GroupsPotential.Contains(GroupsPotential.SelectedGroup))
            {
                GroupsPotential.Remove(GroupsPotential.SelectedGroup);
            }
            else
            {
                //удаляем в подгруппах
                var group = GroupsPotential.Single(x => x.Groups.Contains(GroupsPotential.SelectedGroup));
                group.Groups.Remove(GroupsPotential.SelectedGroup);
                if (!group.Groups.Any())
                    GroupsPotential.Remove(group);
            }
        }

        public override async Task LoadAsync()
        {
            await base.LoadAsync();

            _salesUnitsWrappers?.ForEach(x => x.PropertyChanged -= OnPropertyChanged);

            _unitOfWork = Container.Resolve<IUnitOfWork>();
            _salesUnitsWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>((await _unitOfWork.Repository<SalesUnit>().GetAllAsync()).Select(x => new SalesUnitWrapper(x)));
            _salesUnitsWrappers?.ForEach(x => x.PropertyChanged += OnPropertyChanged);

            var unitsInPlan = _salesUnitsWrappers.Where(x => x.SignalToStartProductionDone != null).ToList();
            var unitsToPlan = _salesUnitsWrappers.Except(unitsInPlan).Where(x => x.SignalToStartProduction != null);

            this.SelectedLookupChanged += OnSelectedOrderChanged;

            GroupsPotential.Clear();
            GroupsPotential.AddRange(ProductionGroup.Grouping(unitsToPlan));
            GroupsPotential.SelectedGroupChanged += group =>
            {
                ((DelegateCommand)AddInOrderCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)ShowProductStructureCommand).RaiseCanExecuteChanged();
            };
        }

        private void OnSelectedOrderChanged(OrderLookup orderLookup)
        {
            GroupsInOrder.Clear();
            if (SelectedItem == null) return;

            var unitsInOrder = _salesUnitsWrappers.Where(x => x.Order != null && x.Order.Id == SelectedItem.Id);
            GroupsInOrder.AddRange(ProductionGroup.Grouping(unitsInOrder));

            ((DelegateCommand)AddInOrderCommand).RaiseCanExecuteChanged();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)AddInOrderCommand).RaiseCanExecuteChanged();
        }
    }
}
