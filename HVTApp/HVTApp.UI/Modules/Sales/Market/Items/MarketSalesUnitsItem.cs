using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Market.Items
{
    public class MarketSalesUnitsItem : BindableBase
    {
        public MarketProjectItem MarketProjectItem { get; }
        public IEnumerable<SalesUnit> SalesUnits { get; }

        public string Facility => SalesUnits.First().Facility.ToString();
        public string ProductType => SalesUnits.First().Product.ProductType.Name;
        public string Designation => SalesUnits.First().Product.ProductBlock.CategoryOrDesignation;
        public int Amount => SalesUnits.Count();
        public double Cost => SalesUnits.First().Cost;
        public double CostTotal => SalesUnits.Sum(salesUnit => salesUnit.Cost);
        public DateTime OrderInTakeDate => SalesUnits.First().OrderInTakeDate;
        public DateTime RealizationDateCalculated => SalesUnits.First().RealizationDateCalculated;
        public string Comment => SalesUnits.First().Comment;

        public string FacilitiesOwners
        {
            get
            {
                var owners = new List<Company> { SalesUnits.First().Facility.OwnerCompany };
                owners.AddRange(owners.First().ParentCompanies());
                return owners.ToStringEnum();
            }
        }

        public MarketSalesUnitsItem(IEnumerable<SalesUnit> salesUnits, MarketProjectItem marketProjectItem)
        {
            MarketProjectItem = marketProjectItem;
            SalesUnits = salesUnits;
        }

        public class Comparer : MarketViewBaseComparer
        {
            public override bool OtherEquals(SalesUnit first, SalesUnit second)
            {
                if (!Equals(first.RealizationDateCalculated, second.RealizationDateCalculated)) return false;
                if (!Equals(first.OrderInTakeDate, second.OrderInTakeDate)) return false;
                if (!Equals(first.Cost, second.Cost)) return false;
                if (!Equals(first.Comment, second.Comment)) return false;
                if (!Equals(first.Facility.Id, second.Facility.Id)) return false;
                if (!Equals(first.Product.Id, second.Product.Id)) return false;
                if (!Equals(first.OrderInTakeDate, second.OrderInTakeDate)) return false;
                if (!Equals(first.RealizationDateCalculated, second.RealizationDateCalculated)) return false;

                return true;
            }
        }
    }
}