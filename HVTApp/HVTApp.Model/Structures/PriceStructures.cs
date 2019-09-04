using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Structures
{
    public class PriceStructures : List<PriceStructure>
    {
        /// <summary>
        /// —ебестоимость без учета блоков с фиксированной ценой.
        /// </summary>
        public double TotalPriceFixedCostLess => this.Sum(x => x.TotalPriceFixedCostLess);

        /// <summary>
        /// —уммарна€ стоимость блоков с фиксированной ценой.
        /// </summary>
        public double TotalFixedCost => this.Sum(x => x.TotalFixedCost);

        /// <summary>
        /// —ебестоимость услуг без учета блоков с фиксированной ценой.
        /// </summary>
        public double TotalServicePriceFixedCostLess => this.Sum(x => x.TotalPriceServiceFixedCostLess);

        /// <summary>
        /// —уммарна€ стоимость услуг с фиксированной ценой.
        /// </summary>
        public double TotalServiceFixedCost => this.Sum(x => x.TotalServiceFixedCost);

        public PriceStructures(IUnit unit, DateTime targetPriceDate, int priceTerm, IEnumerable<ProductBlock> analogs)
        {
            var productBlocks = analogs as ProductBlock[] ?? analogs.ToArray();

            //структура себестоимости продукта
            this.Add(new PriceStructure(unit.Product, 1, targetPriceDate, priceTerm, productBlocks));

            //структура себестоимости включенных продуктов
            foreach (var prodIncl in unit.ProductsIncluded)
            {
                double count = (double)prodIncl.Amount / prodIncl.ParentsCount;
                this.Add(new PriceStructure(prodIncl.Product, count, targetPriceDate, priceTerm, productBlocks));
            }
        }
    }
}