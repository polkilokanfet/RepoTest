using System;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferLookupListViewModel
    {
        protected override Offer GetNewItem()
        {
            return new Offer {ValidityDate = DateTime.Today.AddDays(60)};
        }

        protected override void SubscribesToEvents()
        {
            EventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Subscribe(OnSaveOfferUnit);
        }

        private void OnSaveOfferUnit(OfferUnit offerUnit)
        {
            var offerLookup = Lookups.SingleOrDefault(x => x.Id == offerUnit.Offer.Id);
            if (offerLookup == null) return;

            var offerUnitLookup = offerLookup.OfferUnits.GetById(offerUnit);
            offerUnitLookup?.Refresh(offerUnit);

            if (offerUnitLookup == null)
            {
                offerLookup.OfferUnits.Add(new OfferUnitLookup(offerUnit));
            }

            offerLookup.Refresh();
        }
    }
}