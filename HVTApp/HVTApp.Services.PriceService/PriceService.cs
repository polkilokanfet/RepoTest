using System;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService
{
    public class PriceService : IPriceService
    {
        public double GetPrice(Product product, DateTime date)
        {
            return product.GetBlocks().Sum(b => GetPrice(b, date));
        }

        public double GetPrice(ProductBlock block, DateTime date)
        {
            var prices = block.Prices.Where(pr => pr.Date <= date).ToList();
            if (!prices.Any())
                throw new ArgumentOutOfRangeException(nameof(date), $"Для {block} нет прайса раньше {date.ToShortDateString()}");
            return prices.OrderBy(x => x.Date).Last().Sum;
        }

    }
}