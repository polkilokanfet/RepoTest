using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
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
        private ProductIncludedWrapper _selectedProductIncluded;

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
                ((DelegateCommand)AddProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged();
                OnPropertyChanged(nameof(PriceErrors));
            }
        }

        public ProductIncludedWrapper SelectedProductIncluded
        {
            get { return _selectedProductIncluded; }
            set
            {
                if (Equals(_selectedProductIncluded, value)) return;
                _selectedProductIncluded = value;
                ((DelegateCommand)RemoveProductIncludedCommand)?.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Группы
        /// </summary>
        public ObservableCollection<IUnitsGroup> Groups { get; } = new ObservableCollection<IUnitsGroup>();

        #region Commands

        
        #region ICommand

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ChangeFacilityCommand { get; private set; }
        public ICommand ChangeProductCommand { get; private set; }
        public ICommand ChangePaymentsCommand { get; private set; }

        public ICommand AddProductIncludedCommand { get; private set; }
        public ICommand RemoveProductIncludedCommand { get; private set; }

        #endregion

        protected override void InitSpecialCommands()
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null);
            RefreshCommand = new DelegateCommand(RefreshCommand_Execute);
            ChangeFacilityCommand = new DelegateCommand<IUnitsGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<IUnitsGroup>(ChangeProductCommand_Execute);
            ChangePaymentsCommand = new DelegateCommand<IUnitsGroup>(ChangePaymentsCommand_Execute);

            AddProductIncludedCommand = new DelegateCommand(AddProductIncludedCommand_Execute, () => SelectedGroup != null);
            RemoveProductIncludedCommand = new DelegateCommand(RemoveProductIncludedCommand_Execute, () => SelectedProductIncluded != null);
        }

        private async void AddProductIncludedCommand_Execute()
        {
            var productIncluded = new ProductIncluded();
            productIncluded = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(productIncluded);
            if (productIncluded == null) return;
            productIncluded.Product = await WrapperDataService.GetRepository<Product>().GetByIdAsync(productIncluded.Product.Id);
            SelectedGroup.ProductsIncluded.Add(new ProductIncludedWrapper(productIncluded));
            await RefreshPrices();
        }

        private async void RemoveProductIncludedCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;
            SelectedGroup.ProductsIncluded.Remove(SelectedProductIncluded);
            await RefreshPrices();
        }

        protected abstract void AddCommand_Execute();

        private async void RefreshCommand_Execute()
        {
            RefreshGroups();
            await RefreshPrices();
        }

        private void RemoveCommand_Execute()
        {
            if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            foreach (var unit in SelectedGroup.Units)
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
            await RefreshPrices();
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


        #endregion


        protected override async Task AfterLoading()
        {
            RefreshGroups();
            await RefreshPrices();
        }

        private void RefreshGroups()
        {
            Groups.Clear();
            Groups.AddRange(Item.Units.ToProductUnitGroups());
        }

        private async Task RefreshPrices()
        {
            foreach (var group in Groups)
                await RefreshPrice(group);
        }

        private readonly PriceErrors _priceErrors = new PriceErrors();

        public string PriceErrors
        {
            get
            {
                var blocks = new List<ProductBlock>();
                if (SelectedGroup == null)
                {
                    foreach (var unitsGroup in Groups)
                    {
                        blocks.AddRange(unitsGroup.Product.Model.GetBlocks());
                        foreach (var pi in unitsGroup.ProductsIncluded)
                        {
                            blocks.AddRange(pi.Product.Model.GetBlocks());
                        }
                    }
                }
                else
                {
                    blocks.AddRange(SelectedGroup.Product.Model.GetBlocks());
                    foreach (var pi in SelectedGroup.ProductsIncluded)
                    {
                        blocks.AddRange(pi.Product.Model.GetBlocks());
                    }
                }

                return _priceErrors.Print(blocks);
            }
        }

        /// <summary>
        /// Возвращает дату для расчета прайса.
        /// </summary>
        /// <returns></returns>
        protected abstract DateTime GetDate();

        private async Task RefreshPrice(IUnitsGroup group)
        {
            if (group == null) return;

            var priceService = Container.Resolve<IPriceService>();
            var price = await priceService.GetPrice(group.Product.Model, GetDate(), CommonOptions.ActualPriceTerm, _priceErrors);
            foreach (var productIncluded in group.ProductsIncluded)
            {
                price += productIncluded.Amount * await priceService.GetPrice(productIncluded.Product.Model, DateTime.Today, CommonOptions.ActualPriceTerm, _priceErrors);
            }
            group.Price = price;
            OnPropertyChanged(nameof(PriceErrors));
        }
    }

}