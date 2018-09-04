using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.Services.MessageService;
using HVTApp.Services.PriceService;
using HVTApp.UI.Converter;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.ViewModels
{
    public abstract class WrapperWithUnitsViewModel<TWrapper, TEntity, TUnit, TUnitWrapper, TAfterSaveEntityEvent> :
        BaseDetailsViewModel<TWrapper, TEntity, TAfterSaveEntityEvent>
        where TEntity : class, IBaseEntity
        where TWrapper : class, IWrapper<TEntity>, IWrapperWithUnits<TUnitWrapper>
        where TAfterSaveEntityEvent : PubSubEvent<TEntity>, new()
        where TUnit : class, IBaseEntity, IUnitPoco
        where TUnitWrapper : class, IUnit, IWrapper<TUnit>
    {
        private IUnitsGroup _selectedGroup;

        protected WrapperWithUnitsViewModel(IUnityContainer container) : base(container)
        {
        }

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public IUnitsGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
                ((DelegateCommand) RemoveCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Группы
        /// </summary>
        public ObservableCollection<IUnitsGroup> Groups { get; } = new ObservableCollection<IUnitsGroup>();

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ChangeFacilityCommand { get; private set; }
        public ICommand ChangeProductCommand { get; private set; }
        public ICommand ChangePaymentsCommand { get; private set; }

        protected override void InitSpecialCommands()
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null);
            RefreshCommand = new DelegateCommand(RefreshCommand_Execute);
            ChangeFacilityCommand = new DelegateCommand<IUnitsGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<IUnitsGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<IUnitsGroup>(ChangePaymentsCommand_Execute);
        }

        protected abstract void AddCommand_Execute();

        private void RefreshCommand_Execute()
        {
            RefreshGroups();
            RefreshPrices();
        }

        private void RemoveCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            foreach (var unit in SelectedGroup.ProductUnits)
            {
                Item.Units.Remove((TUnitWrapper) unit);
                WrapperDataService.GetWrapperRepository<TUnit, TUnitWrapper>().Delete((TUnitWrapper)unit);
            }
            Groups.Remove(SelectedGroup);
            SelectedGroup = null;
        }

        private async void ChangeProductCommand_Execute(IUnitsGroup group)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(group.Product?.Model);
            if (product == null || product.Id == group.Product.Id) return;
            group.Product = await WrapperDataService.GetWrapperRepository<Product, ProductWrapper>().GetByIdAsync(product.Id);
            RefreshPrices();
        }

        private async void ChangeFacilityCommand_Execute(IUnitsGroup group)
        {
            var facilities = await WrapperDataService.GetRepository<Facility>().GetAllAsNoTrackingAsync();
            var facility = await Container.Resolve<ISelectService>().SelectItem(facilities, group.Facility?.Id);
            if (facility == null) return;
            group.Facility = await WrapperDataService.GetWrapperRepository<Facility, FacilityWrapper>().GetByIdAsync(facility.Id);
        }

        private async void ChangePaymentsCommand_Execute(IUnitsGroup group)
        {
            var sets = await WrapperDataService.GetRepository<PaymentConditionSet>().GetAllAsNoTrackingAsync();
            var set = await Container.Resolve<ISelectService>().SelectItem(sets, group.PaymentConditionSet?.Id);
            if (set == null) return;
            group.PaymentConditionSet = await WrapperDataService.GetWrapperRepository<PaymentConditionSet, PaymentConditionSetWrapper>().GetByIdAsync(set.Id);
        }

        protected override void AfterLoading()
        {
            RefreshGroups();
            RefreshPrices();
        }

        private void RefreshPrices()
        {
            var priceService = Container.Resolve<IPriceService>();
            foreach (var unit in Item.Units)
                unit.Price = priceService.GetPrice(unit.Product.Model, DateTime.Today, CommonOptions.ActualPriceTerm);
        }

        private void RefreshGroups()
        {
            Groups.Clear();
            Groups.AddRange(Item.Units.ToProductUnitGroups());
        }
    }
}