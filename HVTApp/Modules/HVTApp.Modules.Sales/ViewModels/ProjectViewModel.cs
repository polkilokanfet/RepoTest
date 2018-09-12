using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectViewModel : ProjectDetailsViewModel
    {
        public ProjectViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<IUnitsDatedGroup> GetGroups()
        {
            return Item.Units.ToProductUnitGroups();
        }

        protected override async void AddCommand_Execute()
        {
            var salesUnit = new SalesUnitWrapper(new SalesUnit { Project = Item.Model });
            var viewModel = new SalesUnitsViewModel(salesUnit, Container, WrapperDataService);
            if (SelectedGroup != null)
            {
                viewModel.ViewModel.Item.Cost = SelectedGroup.Cost;
                viewModel.ViewModel.Item.Facility = SelectedGroup.Facility;
                viewModel.ViewModel.Item.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
                viewModel.ViewModel.Item.ProductionTerm = SelectedGroup.ProductionTerm;
                viewModel.ViewModel.Item.Product = SelectedGroup.Product;
                viewModel.ViewModel.Item.DeliveryDateExpected = SelectedGroup.DeliveryDateExpected;
                foreach (var prodIncl in SelectedGroup.ProductsIncluded)
                {
                    var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
                    viewModel.ViewModel.Item.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
                }
            }

            var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
            if (!result.HasValue || !result.Value)
                return;

            var wrappers = new List<SalesUnitWrapper>();
            for (int i = 0; i < viewModel.Amount; i++)
            {
                var unit = (SalesUnit)viewModel.ViewModel.Item.Model.Clone();
                unit.Id = Guid.NewGuid();
                unit.ProductsIncluded = new List<ProductIncluded>();
                var unitWrapper = new SalesUnitWrapper(unit);
                this.Item.Units.Add(unitWrapper);
                wrappers.Add(unitWrapper);
            }
            var group = new UnitsDatedGroup(wrappers);
            Groups.Add(group);
            await RefreshPrices();
            SelectedGroup = group;
        }

        protected override async Task AfterLoading()
        {
            await base.AfterLoading();
            if (Item.Manager == null) Item.Manager = await WrapperDataService.GetWrapperRepository<User, UserWrapper>().GetByIdAsync(CommonOptions.User.Id);
        }

        protected override DateTime GetDate()
        {
            var oitDate = Item.Units.Min(x => x.OrderInTakeDate);
            return oitDate < DateTime.Today ? oitDate : DateTime.Today;
        }
    }
}