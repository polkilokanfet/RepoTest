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
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Price.ViewModels
{
    public class ProductionPlanViewModel : OrderLookupListViewModel
    {
        private IUnitOfWork _unitOfWork;

        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitsWrappers;
        private ProductionGroup _selectedGroupPotential;

        public ObservableCollection<ProductionGroup> GroupsInOrder { get; } = new ObservableCollection<ProductionGroup>();
        public ObservableCollection<ProductionGroup> GroupsPotential { get; } = new ObservableCollection<ProductionGroup>();

        public ProductionGroup SelectedGroupPotential
        {
            get { return _selectedGroupPotential; }
            set
            {
                if (Equals(_selectedGroupPotential, value)) return;
                _selectedGroupPotential = value;
                ((DelegateCommand)AddInOrderCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ICommand ReloadCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand AddInOrderCommand { get; }

        public ProductionPlanViewModel(IUnityContainer container) : base(container)
        {
            Func<bool> canSave = () => _salesUnitsWrappers != null && _salesUnitsWrappers.IsChanged && _salesUnitsWrappers.IsValid;
            SaveCommand = new DelegateCommand(SaveCommand_Execute, canSave);
            
            AddInOrderCommand = new DelegateCommand(AddInOrderCommand_Execute, AddInOrderCommand_CanExecute);
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        private async void SaveCommand_Execute()
        {
            _salesUnitsWrappers.AcceptChanges();
            await _unitOfWork.SaveChangesAsync();
        }

        private bool AddInOrderCommand_CanExecute()
        {
            return SelectedGroupPotential != null && SelectedItem != null;
        }

        private async void AddInOrderCommand_Execute()
        {
            //фиксируем дату действия и заказ
            SelectedGroupPotential.SetSignalToStartProductionDone();
            //подменяем заказ
            SelectedGroupPotential.Order = new OrderWrapper(await _unitOfWork.Repository<Order>().GetByIdAsync(SelectedItem.Id));
            //ставим предполагаемую дату производства
            SelectedGroupPotential.Date = SelectedGroupPotential.Unit.DeliveryDateExpected;
            //ставим позиции заказа
            int pos = 1;
            if (SelectedGroupPotential.Groups.Any())
            {
                foreach (var productionGroup in SelectedGroupPotential.Groups)
                {
                    productionGroup.Position = (pos++).ToString();
                }
            }
            else
            {
                SelectedGroupPotential.Position = (pos).ToString();
            }

            //переносим заказ в план производства
            GroupsInOrder.Add(SelectedGroupPotential);
            if (GroupsPotential.Contains(SelectedGroupPotential))
            {
                GroupsPotential.Remove(SelectedGroupPotential);
            }
            else
            {
                //удаляем в подгруппах
                var group = GroupsPotential.Single(x => x.Groups.Contains(SelectedGroupPotential));
                group.Groups.Remove(SelectedGroupPotential);
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
