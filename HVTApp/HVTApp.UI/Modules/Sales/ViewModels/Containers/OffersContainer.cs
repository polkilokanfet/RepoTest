using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class OffersContainer : BaseContainerFilt<Offer, OfferLookup, SelectedOfferChangedEvent, AfterSaveOfferEvent, AfterRemoveOfferEvent, Project, SelectedProjectChangedEvent>
    {
        private List<OfferUnit> _offerUnits;

        public OffersContainer(IUnityContainer container) : base(container)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Subscribe(offer => _offerUnits.RemoveIfContainsById(offer));
            eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Subscribe(offer => _offerUnits.ReAddById(offer));
        }

        protected override OfferLookup MakeLookup(Offer offer)
        {
            var offerUnits = _offerUnits.Where(x => x.Offer.Id == offer.Id);
            return new OfferLookup(offer, offerUnits, Container);
        }

        protected override IEnumerable<OfferLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            _offerUnits = unitOfWork.Repository<OfferUnit>().Find(x => x.Offer.Project.Manager.IsAppCurrentUser());
            return _offerUnits
                .Select(x => x.Offer)
                .Distinct()
                .OrderBy(x => x.Date)
                .Select(MakeLookup);
        }

        protected override IEnumerable<OfferLookup> GetActualLookups(Project project)
        {
            return AllLookups.Where(offerLookup => offerLookup.Project.Id == project.Id);
        }

        protected override bool CanBeShown(Offer offer)
        {
            return Filter != null && Filter.Id == offer.Project.Id;
        }
    }
}