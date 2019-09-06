using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookup
    {
        public OfferLookup(Offer offer, IEnumerable<OfferUnit> offerUnits) : base(offer)
        {
            OfferUnits.AddRange(offerUnits.Select(x => new OfferUnitLookup(x)));
        }

        public List<OfferUnitLookup> OfferUnits { get; } = new List<OfferUnitLookup>();

        [Designation("Компания"), OrderStatus(100)]
        public CompanyLookup Company => new CompanyLookup(Entity.RecipientEmployee.Company);

        [Designation("Сумма"), OrderStatus(90)]
        public double? Sum => OfferUnits?.Sum(x => x.Cost);




        public OfferLookup(Offer offer, IEnumerable<OfferUnit> offerUnits, IUnityContainer container) : this(offer, offerUnits)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();

            //реакция на изменение в ТКП
            eventAggregator.GetEvent<AfterSaveOfferEvent>().Subscribe(entity =>
            {
                if (entity.Id == this.Id)
                    this.Refresh(entity);
            });

            //реакция на изменение в юните ТКП
            eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Subscribe(offerUnit =>
            {
                if (this.Id != offerUnit.Offer.Id) return;
                OfferUnits.RemoveIfContainsById(offerUnit);
                OfferUnits.Add(new OfferUnitLookup(offerUnit));
                this.Refresh();
            });


            //реакция на удаление юнита ТКП
            eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Subscribe(offerUnit =>
            {
                if (this.Id != offerUnit.Offer.Id) return;
                OfferUnits.RemoveById(offerUnit);
                this.Refresh();
            });

        }
    }
}