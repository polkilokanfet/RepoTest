using System.Linq;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel : WrapperWithUnitsViewModel<OfferWrapper, Offer, OfferUnit, OfferUnitWrapper, AfterSaveOfferEvent>
    {
        protected override async void AddCommand_Execute()
        {
            var offerUnit = new OfferUnit {Offer = Item.Model, Facility = Groups?.First().Facility.Model};
            if (SelectedGroup != null)
            {
                offerUnit.Cost = SelectedGroup.Cost;
                offerUnit.Facility = SelectedGroup.Facility.Model;
                offerUnit.PaymentConditionSet = SelectedGroup.PaymentConditionSet.Model;
                offerUnit.ProductionTerm = SelectedGroup.ProductionTerm;
                offerUnit.Product = SelectedGroup.Product.Model;
                foreach (var prodIncl in SelectedGroup.ProductsIncluded)
                {
                    offerUnit.ProductsIncluded.Add(new ProductIncluded {Product = prodIncl.Product.Model, Amount = prodIncl.Amount});
                }
            }
            var unit = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(offerUnit);
            if (unit == null) return;

            unit.Product = await WrapperDataService.GetRepository<Product>().GetByIdAsync(unit.Product.Id);
            unit.Facility = await WrapperDataService.GetRepository<Facility>().GetByIdAsync(unit.Facility.Id);
            unit.PaymentConditionSet = await WrapperDataService.GetRepository<PaymentConditionSet>().GetByIdAsync(unit.PaymentConditionSet.Id);

            var wrapper = new OfferUnitWrapper(unit);
            Groups.Add(new UnitsGroup(new[] { wrapper }));
            Item.Units.Add(wrapper);
            WrapperDataService.GetRepository<OfferUnit>().Add(unit);
        }
    }
}