using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService
{
    public class PriceService : IPriceService
    {
        public double GetPrice(Product product, DateTime date)
        {
            return GetBlocks(product).Sum(b => GetPrice(b, date));
        }

        public double GetPrice(ProductBlock block, DateTime date)
        {
            var prices = block.Prices.Where(pr => pr.Date <= date).ToList();
            if (!prices.Any())
                throw new ArgumentOutOfRangeException(nameof(date), $"Для {block} нет прайса раньше {date.ToShortDateString()}");
            return prices.OrderBy(x => x.Date).Last().Sum;
        }

        private IEnumerable<ProductBlock> GetBlocks(Product product)
        {
            var result = new List<ProductBlock>();
            result.Add(product.ProductBlock);
            foreach (var dependentProduct in product.DependentProducts)
            {
                for (int i = 0; i < dependentProduct.Amount; i++)
                {
                    result.AddRange(GetBlocks(dependentProduct.Product));
                }
            }
            return result;
        }

    }
}