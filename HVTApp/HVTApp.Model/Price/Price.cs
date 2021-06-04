using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Model.Price
{
    public class Price : PriceBase
    {
        /// <summary>
        /// Имеется калькуляция
        /// </summary>
        public bool HasCalculation { get; protected set; } = false;

        public override bool ContainsAnyAnalog => Prices.Any(price => price.ContainsAnyAnalog);

        public override string Comment
        {
            get
            {
                if (HasCalculation) return "По калькуляции";
                return ContainsAnyAnalog
                    ? "Присутствуют аналоги" 
                    : null;
            }
        }

        /// <summary>
        /// Себестоимость с учетом коэффициента упаковки
        /// </summary>
        public override double SumPriceTotal
        {
            get
            {
                var sumPriceMainBlock = PriceMainBlock?.SumPriceTotal ?? 0;
                return sumPriceMainBlock
                       //+ PricesOfDependentBlocks.Sum(price => price.SumPriceTotal) 
                       + PricesProductsIncluded.Sum(price => price.SumPriceTotal);
            }
        }

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

                return sumPriceMainBlock 
                    + PricesOfDependentBlocks.Sum(price => price.SumFixedTotal) 
                    + PricesProductsIncluded.Sum(price => price.SumFixedTotal);
            }
        }

        /// <summary>
        /// Прайс главного блока
        /// </summary>
        public IPrice PriceMainBlock { get; }

        /// <summary>
        /// Прайсы зависимых блоков
        /// </summary>
        public List<IPrice> PricesOfDependentBlocks { get; } = new List<IPrice>();

        /// <summary>
        /// Прайсы включенного оборудования
        /// </summary>
        public List<IPrice> PricesProductsIncluded { get; } = new List<IPrice>();

        public override List<IPrice> Prices
        {
            get
            {
                var result = PricesProductsIncluded.ToList();
                
                //если есть калькуляция
                if (HasCalculation)
                {
                    result.Add(new PriceGroup(PriceMainBlock.Name, PricesOfDependentBlocks));
                }
                //если её нет
                else
                {
                    if (PriceMainBlock != null)
                        result.Add(PriceMainBlock);
                }

                return result.OrderByDescending(price => price.SumTotal).ToList();
            }
        }

        public Price(IUnit unit, DateTime targetDate, IPriceService priceService)
        {
            Name = unit.Product.ToString();
            IEnumerable<ProductIncluded> productsIncluded = unit.ProductsIncluded;

            //если есть калькуляция
            var priceCalculationItem = priceService.GetPriceCalculationItem(unit);
            if (priceCalculationItem != null)
            {
                HasCalculation = true;

                //заглушка на прайс основного блока
                PriceMainBlock = new PriceStub(unit.Product.ToString(), 1);

                //заглушки на прайсы зависимых блоков
                PricesOfDependentBlocks =
                    priceCalculationItem.StructureCosts
                        .Where(structureCost => structureCost.UnitPrice.HasValue)
                        .Select(structureCost => new PriceStub($"{structureCost.Comment}", structureCost.Amount, structureCost.UnitPrice.Value, structureCost.Number))
                        .Cast<IPrice>()
                        .ToList();

                //оставляем включенное оборудование только с фиксированной ценой (напр. шеф-монтаж)
                productsIncluded = productsIncluded.Where(productIncluded => productIncluded.Product.HasBlockWithFixedCost);
            }
            //если калькуляции нет, нужно инициировать по продукту
            else
            {
                Amount = 1;
                PriceMainBlock = new PriceOfProduct(unit.Product, targetDate, priceService, Amount);
                foreach (var dependentProduct in unit.Product.DependentProducts)
                {
                    PricesOfDependentBlocks.Add(new PriceOfProduct(dependentProduct.Product, targetDate, priceService, dependentProduct.Amount));
                }
            }

            //включенное оборудование
            foreach (var productIncluded in productsIncluded)
            {
                var price = new PriceOfProduct(productIncluded.Product, targetDate, priceService, productIncluded.AmountOnUnit, productIncluded.CustomFixedPrice);
                PricesProductsIncluded.Add(price);
            }
        }
    }
}