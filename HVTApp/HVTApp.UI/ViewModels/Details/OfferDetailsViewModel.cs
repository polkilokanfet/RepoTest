using System;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel
    {
        private IProductUnitsGroup _selectedGroup;

        public IProductUnitsGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IProductUnitsGroup> Groups { get; } = new ObservableCollection<IProductUnitsGroup>();

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ChangeFacilityCommand { get; private set; }
        public ICommand ChangeProductCommand { get; private set; }

        protected override void InitSpecialCommands()
        {
            AddCommand = new DelegateCommand(AddCommand_Execute);
            RemoveCommand= new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null);
            RefreshCommand = new DelegateCommand(RefreshCommand_Execute);
            ChangeFacilityCommand = new DelegateCommand<IProductUnitsGroup>(ChangeFacilityCommand_Execute);
            ChangeProductCommand = new DelegateCommand<IProductUnitsGroup>(ChangeProductCommand_Execute);
        }

        private async void AddCommand_Execute()
        {
            var offerUnit = await Container.Resolve<IUpdateDetailsService>().GetEntity(new OfferUnit {Offer = Item.Model});
            if (offerUnit == null) return;

            offerUnit.Product = await WrapperDataService.GetRepository<Product>().GetByIdAsync(offerUnit.Product.Id);
            offerUnit.Facility = await WrapperDataService.GetRepository<Facility>().GetByIdAsync(offerUnit.Facility.Id);

            var wrapper = new OfferUnitWrapper(offerUnit);
            Groups.Add(new ProductUnitsGroup(new []{wrapper}));
            WrapperDataService.GetRepository<OfferUnit>().Add(offerUnit);
        }

        private void RefreshCommand_Execute()
        {
            RefreshGroups();
        }

        private void RemoveCommand_Execute()
        {
            if(Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить?") == MessageDialogResult.No)
                return;

            foreach (var unit in SelectedGroup.ProductUnits)
            {
                Item.OfferUnits.Remove((OfferUnitWrapper) unit);
                WrapperDataService.GetWrapperRepository<OfferUnit, OfferUnitWrapper>().Delete((OfferUnitWrapper)unit);
            }
            Groups.Remove(SelectedGroup);
            SelectedGroup = null;
        }

        private async void ChangeProductCommand_Execute(IProductUnitsGroup group)
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(group.Product?.Model);
            if(product == null || product.Id == group.Product.Id) return;
            group.Product = await WrapperDataService.GetWrapperRepository<Product, ProductWrapper>().GetByIdAsync(product.Id);
            RefreshPrices();
        }

        private async void ChangeFacilityCommand_Execute(IProductUnitsGroup group)
        {
            var facilities = await WrapperDataService.GetRepository<Facility>().GetAllAsNoTrackingAsync();
            var facility = await Container.Resolve<ISelectService>().SelectItem(facilities, group.Facility?.Id);
            if (facility == null) return;
            group.Facility = await WrapperDataService.GetWrapperRepository<Facility, FacilityWrapper>().GetByIdAsync(facility.Id);
        }

        protected override void AfterLoading()
        {
            RefreshGroups();
            RefreshPrices();
        }

        private void RefreshPrices()
        {
            var priceService = Container.Resolve<IPriceService>();
            foreach (var offerUnit in Item.OfferUnits)
                offerUnit.Price = priceService.GetPrice(offerUnit.Product.Model, DateTime.Today, CommonOptions.ActualPriceTerm);
        }

        private void RefreshGroups()
        {
            Groups.Clear();
            Groups.AddRange(Item.OfferUnits.ToProductUnitGroups());
        }
    }
}