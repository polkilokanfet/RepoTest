using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;

namespace HVTApp.Services.ShippingService
{
    public class ShippService : IShippingService
    {
        private readonly List<Locality> _localities;

        public ShippService(IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
        {
            _localities = unitOfWork.Repository<Locality>().GetAll();
            eventAggregator.GetEvent<AfterSaveLocalityEvent>().Subscribe(locality => { _localities.ReAddById(locality); });
            eventAggregator.GetEvent<AfterRemoveLocalityEvent>().Subscribe(locality => { _localities.RemoveIfContainsById(locality); });
        }

        public int? DeliveryTerm(SalesUnit salesUnit)
        {
            if (salesUnit.Facility == null) return null;

            //адрес доставки
            var locality = salesUnit.GetDeliveryAddress()?.Locality;

            while (locality != null)
            {
                if (locality.DistanceToEkb.HasValue)
                {
                    return (int) Math.Ceiling((locality.DistanceToEkb.Value / 8 / 80));
                }
                locality = GetCapital(locality);
            }

            return null;         
        }

        /// <summary>
        /// Замена населенного пункта на столицу
        /// </summary>
        /// <param name="localityToChange"></param>
        /// <returns></returns>
        private Locality GetCapital(Locality localityToChange)
        {
            if (localityToChange.IsCountryCapital)
            {
                return null;
            }

            //меняем столицу ФО на столицу страны
            if (localityToChange.IsDistrictCapital)
            {
                return _localities.FirstOrDefault(locality => localityToChange.Region.District.Country.Id == locality.Region.District.Country.Id && locality.IsCountryCapital);
            }

            //меняем столицу региона на столицу ФО
            if (localityToChange.IsRegionCapital)
            {
                return _localities.FirstOrDefault(locality => localityToChange.Region.District.Id == locality.Region.District.Id && locality.IsDistrictCapital);
            }

            //меняем населенный пункт на столицу региона
            return _localities.FirstOrDefault(locality => localityToChange.Region.Id == locality.Region.Id && locality.IsRegionCapital);
        }
    }
}
