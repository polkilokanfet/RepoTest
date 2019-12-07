using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Director.ViewModels
{
    public class SalesGroup
    {
        public string Facility { get; }
        public string ProductType { get; }
        public string ProductDesignation { get; }
        public double Cost { get; }
        public int Amount { get; }
        public double Sum { get; set; }

        public SalesGroup(IEnumerable<SalesUnit> salesUnits)
        {
            if (salesUnits == null) throw new NullReferenceException($"{nameof(salesUnits)} не должен быть null");
            var salesUnitsList = salesUnits.ToList();
            if(!salesUnitsList.Any()) throw new ArgumentException($@"{nameof(salesUnits)} не имеет членов", nameof(salesUnits));

            Facility = salesUnitsList.First().Facility.ToString();
            ProductType = salesUnitsList.First().Product.ProductType?.ToString();
            ProductDesignation = salesUnitsList.First().Product.Designation;
            Cost = salesUnitsList.First().Cost;
            Amount = salesUnitsList.Count;
            Sum = Cost * Amount;
        }
    }
}