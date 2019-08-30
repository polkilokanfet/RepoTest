using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersContainer : BaseContainerProjectReact<Offer, OfferLookup, 
        SelectedOfferChangedEvent, AfterSaveOfferEvent, AfterRemoveOfferEvent, OffersCollection>
    {
        public OffersContainer(IUnityContainer container) : base(container)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();

            eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Subscribe(AfterRemoveOfferUnitEventExecute);
            eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Subscribe(AfterSaveOfferUnitEventExecute);
        }

        protected override OffersCollection GetItemsCollection(IUnitOfWork unitOfWork)
        {
            var offers = unitOfWork.Repository<Offer>().Find(x => x.Project.Manager.IsAppCurrentUser());
            return new OffersCollection(offers);
        }

        /// <summary>
        /// ������� �� ���������� ������ ���
        /// </summary>
        /// <param name="offerUnit"></param>
        private void AfterSaveOfferUnitEventExecute(OfferUnit offerUnit)
        {
            ////������� ���
            //var offer = _offers.GetById(offerUnit.Offer);

            ////������� - ����� �������� �������������� ������
            //if (offer == null) return;

            ////��������� ��� ���������
            //if (offer.OfferUnits.ContainsById(offerUnit))
            //{
            //    offer.OfferUnits.GetById(offerUnit)?.Refresh(offerUnit);
            //}
            //else
            //{
            //    offer.OfferUnits.Add(new OfferUnitLookup(offerUnit));
            //}

            ////��������� ������� ���
            //offer.Refresh();
        }

        /// <summary>
        /// ������� �� �������� ������ ���
        /// </summary>
        /// <param name="offerUnit"></param>
        private void AfterRemoveOfferUnitEventExecute(OfferUnit offerUnit)
        {
            //var offer = _offers.GetById(offerUnit.Offer);
            //if (offer == null) return;
            //var lookup = offer.OfferUnits.GetById(offerUnit);
            //offer.OfferUnits.Remove(lookup);
            //offer.Refresh();
        }

        protected override IEnumerable<OfferLookup> GetActualForProjectItems(Project project)
        {
            return AllItems.Where(x => x.Project.Id == project.Id).OrderByDescending(x => x.Date).Select(x => new OfferLookup(x));
        }
    }
}