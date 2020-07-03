using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.Reference
{
    public class ReferenceItem
    {
        public string Facility { get; }
        public string FacilityOwner { get; }
        public string ProductType { get; }
        public string Voltage { get; }
        public string Product { get; }
        public int Amount { get; }
        public DateTime ShipmentDate { get; }
        public string Order { get; }
        public string Numbers { get; }
        public string Manager { get; }
        public string Region { get; }
        public string District { get; }
        public string Country { get; }

        public ReferenceItem(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnit = salesUnits.First();
            Facility = salesUnit.Facility.ToString();
            var owners = new List<Company> { salesUnit.Facility.OwnerCompany };
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwner = owners.ToStringEnum();
            ProductType = salesUnit.Product.ProductType.ToString();
            Product = salesUnit.Product.Designation;
            Voltage = salesUnit.Product.Voltage();
            Amount = salesUnits.Count();
            ShipmentDate = salesUnit.ShipmentDateCalculated;
            Order = salesUnit.Order?.ToString();
            Manager = salesUnit.Project.Manager.ToString();

            var region = salesUnit.Facility.GetRegion();
            if (region != null)
            {
                Region = region.Name;
                District = region.District.ToString();
                Country = region.District.Country.ToString();
            }

            Numbers = salesUnits.Select(x => x.SerialNumber).ToStringEnum();
        }
    }
}