using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.Modules.Sales.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class OffersContainer : BaseContainerViewModelWithFilterByProject<Offer, OfferLookup, SelectedOfferChangedEvent, AfterSaveOfferEvent, AfterRemoveOfferEvent, OfferView>
    {
        private List<OfferUnit> _offerUnits;

        public OffersContainer(IUnityContainer container, ISelectedProjectItemChanged vm) : base(container, vm)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Subscribe(offer => _offerUnits.RemoveIfContainsById(offer));
            eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Subscribe(offer => _offerUnits.ReAddById(offer));
        }

        protected override OfferLookup MakeLookup(Offer offer)
        {
            var offerUnits = _offerUnits.Where(offerUnit => offerUnit.Offer.Id == offer.Id);
            return new OfferLookup(offer, offerUnits, Container);
        }

        protected override IEnumerable<OfferLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            _offerUnits = GlobalAppProperties.User.RoleCurrent == Role.Admin
                ? unitOfWork.Repository<OfferUnit>().GetAll()
                : ((IOfferUnitRepository)unitOfWork.Repository<OfferUnit>()).GetAllOfCurrentUser().ToList();
            return _offerUnits
                .Select(offerUnit => offerUnit.Offer)
                .Distinct()
                .OrderByDescending(offer => offer.Date)
                .Select(MakeLookup);
        }

        protected override IEnumerable<OfferLookup> GetActualLookups(Project project)
        {
            return AllLookups
                .Where(offerLookup => offerLookup.Project.Id == project.Id)
                .OrderByDescending(offerLookup => offerLookup.Entity.Date);
        }

        protected override bool CanBeShown(Offer offer)
        {
            return SelectedProject != null && 
                   SelectedProject.Id == offer.Project.Id;
        }

        public override void EditSelectedItem()
        {
            var parameters = new NavigationParameters { { nameof(Offer), this.SelectedItem.Entity }, { "edit", true } };
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<OfferView>(parameters);
        }
    }
}