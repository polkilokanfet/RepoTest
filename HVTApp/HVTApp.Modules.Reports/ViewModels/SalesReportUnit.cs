using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Modules.Reports.ViewModels
{
    public class SalesReportUnit
    {
        private readonly SalesUnit _salesUnit;
        private readonly List<Tender> _tenders;

        public string OrderNumber => _salesUnit.Order?.Number;
        public string OrderPosition => _salesUnit.OrderPosition;
        public Company FacilityOwner => _salesUnit.Facility.OwnerCompany;
        public Company Contragent => _salesUnit.Specification?.Contract.Contragent;
        public string ContragentType
        {
            get
            {
                if (Contragent == null)
                    return "нет данных";

                if (Equals(FacilityOwner, Contragent) || FacilityOwner.ParentCompanies().Contains(Contragent))
                    return "конечный заказчик";

                var tender = _tenders.FirstOrDefault(x => Equals(x.Winner, Contragent));
                if (tender != null)
                    return tender.Types.ToString();

                return "посредник";
            }
        }
        public Facility Facility => _salesUnit.Facility;
        public Country Country => _salesUnit.Facility.Address?.Locality.Region.District.Country;
        public District District => _salesUnit.Facility.Address?.Locality.Region.District;
        public string Segment
        {
            get
            {
                var actEnums = new List<ActivityFieldEnum> {ActivityFieldEnum.Fuel, ActivityFieldEnum.RailWay, ActivityFieldEnum.ElectricityDistribution, ActivityFieldEnum.ElectricityTransmission, ActivityFieldEnum.ElectricityGeneration};
                foreach (var act in actEnums)
                {
                    var af = Facility.OwnerCompany.ActivityFilds.FirstOrDefault(x => Equals(x.ActivityFieldEnum, act));
                    if (af != null) return af.Name;
                }
                
                return String.Empty;
            }
        }
        public ProductType ProductType => _salesUnit.Product.ProductType;
        public string Designation => _salesUnit.Product.Designation;
        public string Status
        {
            get { return "-"; }
        }
        public double? Vat => _salesUnit.Specification?.Vat;
        public double Cost => _salesUnit.Cost;
        public double? CostDelivery => _salesUnit.CostDelivery;



        public SalesReportUnit(SalesUnit salesUnit, IEnumerable<Tender> tenders)
        {
            _salesUnit = salesUnit;
            _tenders = new List<Tender>(tenders);
        }
    }
}