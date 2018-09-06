using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Services;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel : WrapperWithUnitsViewModel<OfferWrapper, Offer, OfferUnit, OfferUnitWrapper, AfterSaveOfferEvent>
    {
        protected override async void AddCommand_Execute()
        {
            var unit = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(new OfferUnit { Offer = Item.Model });
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