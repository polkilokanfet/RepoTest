using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.ShippingService
{
    public class ShippService : IShippingService
    {
        private readonly List<Locality> _localities;

        public ShippService(IUnitOfWork unitOfWork)
        {
            _localities = unitOfWork.Repository<Locality>().Find(x => true);
        }

        public int? DeliveryTerm(SalesUnit salesUnit)
        {
            if (salesUnit.Facility == null)
                return null;

            //адрес владельца объекта
            var locality = salesUnit.Facility.OwnerCompany.AddressLegal?.Locality;

            //адрес объекта
            if(salesUnit.Facility.Address != null)
                locality = salesUnit.Facility.Address.Locality;

            //адрес доставки
            if (salesUnit.AddressDelivery != null)
                locality = salesUnit.AddressDelivery.Locality;

            if(locality != null)
            { 
                do
                {
                    if (locality.DistanceToEkb.HasValue)
                    {
                        var distance = locality.DistanceToEkb.Value;
                        return (int) Math.Ceiling((distance / 8 / 80));
                    }
                    locality = GetCapital(locality);
                } while (locality != null);
            }

            return null;         
        }

        private Locality GetCapital(Locality locality)
        {
            if (locality.IsCountryCapital)
            {
                return null;
            }

            //меняем столицу ФО на столицу страны
            if (locality.IsDistrictCapital)
            {
                return _localities.SingleOrDefault(x => locality.Region.District.Country.Id == x.Region.District.Country.Id && x.IsCountryCapital);
            }

            //меняем столицу региона на столицу ФО
            if (locality.IsRegionCapital)
            {
                return _localities.SingleOrDefault(x => locality.Region.District.Id == x.Region.District.Id && x.IsDistrictCapital);
            }

            return _localities.SingleOrDefault(x => locality.Region.Id == x.Region.Id && x.IsRegionCapital);

        }
    }
}
