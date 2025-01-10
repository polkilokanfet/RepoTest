using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.ProductionViewModelEntities
{
    public class ProductionGroup
    {
        public SalesUnit SalesUnit => ProductionItems.First().Model;

        public IEnumerable<ProductionItem> ProductionItems { get; }

        public int Amount => ProductionItems.Count();

        public DateTime EndProductionDateExpected => ProductionItems.First().EndProductionDateExpected;

        public ProductionGroup(IEnumerable<ProductionItem> productionItems)
        {
            if (productionItems == null)
                throw new ArgumentNullException(nameof(productionItems));
            if (productionItems.Any() == false)
                throw new ArgumentException(nameof(productionItems));

            ProductionItems = new List<ProductionItem>(productionItems);
        }

        public bool IsProduced => ProductionItems.All(productionItem => productionItem.IsProduced);

        public int DifExpected => ProductionItems.First().DifExpected;

        public int DifContract => ProductionItems.First().DifContract;
    }
}