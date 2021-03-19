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
            _localities = unitOfWork.Repository<Locality>().GetAll();
        }

        public int? DeliveryTerm(SalesUnit salesUnit)
        {
            var facility = salesUnit.Facility;
            
            if (facility == null) return null;

            var locality = salesUnit.AddressDelivery?.Locality              //адрес доставки
                           ?? facility.Address?.Locality                    //адрес объекта
                           ?? facility.OwnerCompany.AddressLegal?.Locality; //адрес владельца объекта

            //поиск адреса в голове владельца
            var company = facility.OwnerCompany.ParentCompany;
            while (company != null && locality == null)
            {
                locality = company.AddressLegal?.Locality;
                company = company.ParentCompany;
            }

            while (locality != null)
            {
                if (locality.DistanceToEkb.HasValue)
                {
                    var distance = locality.DistanceToEkb.Value;
                    return (int) Math.Ceiling((distance / 8 / 80));
                }
                locality = GetCapital(locality);
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
                return _localities.FirstOrDefault(x => locality.Region.District.Country.Id == x.Region.District.Country.Id && x.IsCountryCapital);
                //return _localities.SingleOrDefault(x => locality.Region.District.Country.Id == x.Region.District.Country.Id && x.IsCountryCapital);
            }

            //меняем столицу региона на столицу ФО
            if (locality.IsRegionCapital)
            {
                return _localities.FirstOrDefault(x => locality.Region.District.Id == x.Region.District.Id && x.IsDistrictCapital);
            }

            return _localities.FirstOrDefault(x => locality.Region.Id == x.Region.Id && x.IsRegionCapital);

        }
    }
}
