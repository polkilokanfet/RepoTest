using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class ReferenceItem
    {
        public string Facility { get; }
        public string FacilityOwner { get; }
        public string ProductType { get; }
        public string Voltage { get; }
        public string Product { get; }
        public int Amount { get; }
        public DateTime RealizationDate { get; }
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
            FacilityOwner = owners.ConvertToString();
            ProductType = salesUnit.Product.ProductType.ToString();
            Product = salesUnit.Product.Designation;
            Voltage = salesUnit.Product.ProductBlock.Parameters.FirstOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.VoltageGroup))?.Value;
            Amount = salesUnits.Count();
            RealizationDate = salesUnit.RealizationDateCalculated;
            Order = salesUnit.Order?.ToString();
            Manager = salesUnit.Project.Manager.ToString();

            var region = salesUnit.Facility.GetRegion();
            if (region != null)
            {
                Region = region.Name;
                District = region.District.ToString();
                Country = region.District.Country.ToString();
            }

            Numbers = salesUnits.Select(x => x.SerialNumber).ConvertToString();
        }
    }
}