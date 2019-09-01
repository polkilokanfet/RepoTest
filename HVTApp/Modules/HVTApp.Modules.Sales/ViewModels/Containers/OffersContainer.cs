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

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersContainer : BaseContainerFilt<Offer, OfferLookup, SelectedOfferChangedEvent, AfterSaveOfferEvent, AfterRemoveOfferEvent, Project, SelectedProjectChangedEvent>
    {
        private List<OfferUnit> _offerUnits;

        public OffersContainer(IUnityContainer container) : base(container)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();

            // Реакция на удаление строки ТКП
            eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Subscribe(offerUnit => _offerUnits.RemoveById(offerUnit));

            // Реакция на сохранение строки ТКП
            eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Subscribe(offerUnit => _offerUnits.ReAddById(offerUnit));
        }

        protected override IEnumerable<Offer> GetItems(IUnitOfWorkDisplay unitOfWork)
        {
            _offerUnits = unitOfWork.Repository<OfferUnit>().Find(x => x.Offer.Project.Manager.IsAppCurrentUser());
            return _offerUnits.Select(x => x.Offer).Distinct();
        }

        protected override IEnumerable<OfferLookup> GetActualLookups(Project project)
        {
            var offers = AllItems.Where(offerUnit => offerUnit.Project.Id == project.Id);
            return offers.OrderBy(x => x.Date).Select(MakeLookup);
        }

        /// <summary>
        /// Создание отображения ТКП, которое самостоятельно следит за изменениями в ТКП
        /// </summary>
        /// <param name="offer"> ТКП </param>
        /// <returns> Отображение ТКП </returns>
        private OfferLookup MakeLookup(Offer offer)
        {
            var units = _offerUnits.Where(offerUnit => offerUnit.Offer.Id == offer.Id);
            return new OfferLookup(offer, units, Container);
        }
    }
}