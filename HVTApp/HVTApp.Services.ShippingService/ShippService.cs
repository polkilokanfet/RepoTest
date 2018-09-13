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

        public void SetShippingTerm(SalesUnit salesUnit)
        {
            Locality locality = salesUnit.Facility.OwnerCompany.AddressLegal?.Locality;
            if(salesUnit.Facility.Address != null)
                locality = salesUnit.Facility.Address.Locality;
            if (salesUnit.Address != null)
                locality = salesUnit.Address.Locality;

            if(locality == null) return;

            do
            {
                if (locality.DistanceToEkb.HasValue)
                {
                    var distance = locality.DistanceToEkb.Value;
                    salesUnit.ExpectedDeliveryPeriodCalculated = (int) Math.Ceiling((distance / 8 / 80));
                    return;
                }
                locality = GetCapital(locality);
            } while (locality != null);            
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
