using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class ReferenceItem
    {
        public string Facility { get; }
        public string FacilityOwner { get; }
        public string ProductType { get; }
        public string Product { get; }
        public int Amount { get; }
        public DateTime RealizationDate { get; }
        public string Numbers { get; }
        public string Manager { get; }
        public string Region { get; }
        public string District { get; }
        public string Country { get; }

        public ReferenceItem(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnit = salesUnits.First();
            Facility = salesUnit.Facility.ToString();
            FacilityOwner = $"{salesUnit.Facility.OwnerCompany.FullName} ({salesUnit.Facility.OwnerCompany.Form.ShortName})";
            ProductType = salesUnit.Product.ProductType.ToString();
            Product = salesUnit.Product.ToString();
            Amount = salesUnits.Count();
            RealizationDate = salesUnit.RealizationDateCalculated;
            Manager = salesUnit.Project.Manager.ToString();

            var region = salesUnit.Facility.Address?.Locality.Region;
            var company = salesUnit.Facility.OwnerCompany;
            while (company != null && region == null)
            {
                region = company.AddressLegal?.Locality.Region;
                company = company.ParentCompany;
            }

            if (region != null)
            {
                Region = region.Name;
                District = region.District.ToString();
                Country = region.District.Country.ToString();
            }

            var sb = new StringBuilder();
            foreach (var unit in salesUnits)
            {
                sb.Append(unit.SerialNumber).Append("; ");
            }
            Numbers = sb.ToString();
        }
    }
}