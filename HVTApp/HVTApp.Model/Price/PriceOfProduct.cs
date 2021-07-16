using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Model.Price
{
    public class PriceOfProduct : PriceBase
    {
        public override bool ContainsAnyAnalog => this.PricesOfMainBlockAndDependentBlocks.Any(price => price.ContainsAnyAnalog);
        public override bool ContainsAnyBlockWithNoLaborHours => this.PricesOfMainBlockAndDependentBlocks.Any(price => price.ContainsAnyBlockWithNoLaborHours);

        /// <summary>
        /// По какому аналогу взят прайс
        /// </summary>
        public override string Comment =>
            ContainsAnyBlockWithNoLaborHours
                ? "Содержит ПЗ по аналогам"
                : string.Empty;

        public override string CommentLaborHours =>
            ContainsAnyBlockWithNoLaborHours
                ? "Содержит блоки без н/ч."
                : string.Empty;

        public override double? LaborHours => PriceMainBlock.LaborHours 
                                              + PricesOfDependentBlocks.Sum(price => price.LaborHoursTotal);

        /// <summary>
        /// Себестоимость с учетом коэффициента упаковки
        /// </summary>
        public override double SumPriceTotal => (PriceMainBlock?.SumPriceTotal ?? 0) 
                                                + PricesOfDependentBlocks.Sum(price => price.SumPriceTotal);

        /// <summary>
        /// Стоимость блоков с фиксированной ценой
        /// </summary>
        public override double SumFixedTotal
        {
            get
            {
                if (SumFixed.HasValue)
                    return SumFixed.Value * Amount;

                var sumPriceMainBlock = PriceMainBlock?.SumFixedTotal ?? 0;

                return sumPriceMainBlock + PricesOfDependentBlocks.Sum(price => price.SumFixedTotal);
            }
        }

        /// <summary>
        /// Прайс главного блока
        /// </summary>
        public IPrice PriceMainBlock { get; private set; }

        /// <summary>
        /// Прайсы зависимых блоков
        /// </summary>
        public List<IPrice> PricesOfDependentBlocks { get; } = new List<IPrice>();

        protected IEnumerable<IPrice> PricesOfMainBlockAndDependentBlocks =>
            PriceMainBlock == null
                ? PricesOfDependentBlocks.ToList()
                : PricesOfDependentBlocks.Union(new List<IPrice> {PriceMainBlock});

        //public override List<IPrice> Prices => 
        //    new List<IPrice> { new PriceGroup(PriceMainBlock?.Name, PricesOfMainBlockAndDependentBlocks) }.ToList();

        public override List<IPrice> Prices 
            => PricesOfMainBlockAndDependentBlocks.OrderByDescending(price => price.SumTotal).ToList();


        public PriceOfProduct(Product product, DateTime targetDate, IPriceService priceService, double amount = 1, double? customFixedPrice = null)
        {
            Name = $"{product.ToString()}";
            Amount = amount;
            PriceMainBlock = new PriceOfProductBlock(product.ProductBlock, targetDate, priceService, amount);
            foreach (var dependentProduct in product.DependentProducts)
            {
                PricesOfDependentBlocks.Add(new PriceOfProduct(dependentProduct.Product, targetDate, priceService, dependentProduct.Amount));
            }

            //расстановка нестандартных фиксированных прайсов
            this.SumFixed = customFixedPrice;
        }
    }
}