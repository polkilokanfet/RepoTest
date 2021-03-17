using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService
{
    internal class PriceItems
    {
        private readonly Dictionary<Guid, PriceItem> _items = new Dictionary<Guid, PriceItem>();

        public bool IsEmpty => _items.Any() == false;

        public double? Price => IsEmpty
            ? null
            : this.PriceCalculationItem.StructureCosts.Sum(structureCost => structureCost.Total);

        public PriceCalculationItem PriceCalculationItem => IsEmpty
            ? null
            : _items
                .Select(x => x.Value)
                .OrderBy(priceItem => priceItem.FinishDate)
                .Last()
                .PriceCalculationItem;

        public PriceItems(IEnumerable<PriceItem> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public void Add(PriceItem item)
        {
            this.Remove(item.PriceCalculationItem);
            _items.Add(item.PriceCalculationItem.Id, item);
        }

        public bool Remove(PriceCalculationItem item)
        {
            if (_items.ContainsKey(item.Id))
            {
                _items.Remove(item.Id);
                return true;
            }

            return false;
        }
    }
}