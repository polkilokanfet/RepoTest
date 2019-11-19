using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Structures
{
    /// <summary>
    /// Структуры себестоимости
    /// </summary>
    public class PriceStructures : List<PriceStructure>
    {
        /// <summary>
        /// Себестоимость без учета блоков с фиксированной ценой.
        /// </summary>
        public double TotalPriceFixedCostLess => this.Sum(x => x.TotalPriceFixedCostLess);

        /// <summary>
        /// Суммарная стоимость блоков с фиксированной ценой.
        /// </summary>
        public double TotalFixedCost => this.Sum(x => x.TotalFixedCost);

        /// <summary>
        /// Себестоимость услуг без учета блоков с фиксированной ценой.
        /// </summary>
        public double TotalServicePriceFixedCostLess => this.Sum(x => x.TotalPriceServiceFixedCostLess);

        /// <summary>
        /// Суммарная стоимость услуг с фиксированной ценой.
        /// </summary>
        public double TotalServiceFixedCost => this.Sum(x => x.TotalServiceFixedCost);

        public PriceStructures(IUnit unit, DateTime targetPriceDate, int priceTerm, Func<Guid, ProductBlock> getAnalog)
        {
            //структура себестоимости продукта
            this.Add(new PriceStructure(unit.Product, 1, targetPriceDate, priceTerm, getAnalog));

            //структура себестоимости включенных продуктов
            foreach (var prodIncl in unit.ProductsIncluded)
            {
                double count = (double)prodIncl.Amount / prodIncl.ParentsCount;
                this.Add(new PriceStructure(prodIncl.Product, count, targetPriceDate, priceTerm, getAnalog));
            }
        }
    }
}