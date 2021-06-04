using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Price
{
    public class PriceGroup : PriceBase
    {
        public override bool ContainsAnyAnalog => Prices.Any(price => price.ContainsAnyAnalog);

        public override string Comment => 
            ContainsAnyAnalog
                ? "Присутствуют аналоги"
                : null;

        /// <summary>
        /// Себестоимость с учетом коэффициента упаковки
        /// </summary>
        public override double SumPriceTotal => Prices.Sum(price => price.SumPriceTotal);

        /// <summary>
        /// Стоимость блоков с фиксированной ценой
        /// </summary>
        public override double SumFixedTotal => Prices.Sum(price => price.SumFixedTotal);

        public override List<IPrice> Prices { get; protected set; }

        public PriceGroup(string name, IEnumerable<IPrice> prices)
        {
            Name = name;
            Prices = prices.OrderByDescending(price => price.SumTotal).ToList();
        }
    }
}