using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
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
                ((DelegateCommand)ChangeCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IProductUnitsGroup> Groups { get; } = new ObservableCollection<IProductUnitsGroup>();

        public ICommand ChangeCommand { get; private set; }

        protected override void InitSpecialCommands()
        {
            ChangeCommand = new DelegateCommand(ChangeCommand_Execute, () => SelectedGroup != null);
        }

        protected override void AfterLoading()
        {
            RefreshGroups();

            var priceService = Container.Resolve<IPriceService>();
            foreach (var offerUnit in Item.OfferUnits)
                offerUnit.Price = priceService.GetPrice(offerUnit.Product.Model, DateTime.Today, CommonOptions.ActualPriceTerm);
        }

        private void RefreshGroups()
        {
            Groups.Clear();
            Groups.AddRange(Item.OfferUnits.ToProductUnitGroups());
        }

        private async void ChangeCommand_Execute()
        {
            var entity = ((OfferUnitWrapper) SelectedGroup.ProductUnits.First()).Model;
            entity = await Container.Resolve<IUpdateDetailsService>().GetEntity(entity);

            if (entity == null) return;

            foreach (var productUnit in SelectedGroup.ProductUnits)
            {
                productUnit.Facility = new FacilityWrapper(await WrapperDataService.GetRepository<Facility>().GetByIdAsync(entity.Facility.Id));
                productUnit.Product = new ProductWrapper(await WrapperDataService.GetRepository<Product>().GetByIdAsync(entity.Product.Id));
                productUnit.Cost = entity.Cost;
            }

            RefreshGroups();
        }
    }
}