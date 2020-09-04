using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Model
{
    public class Price
    {
        /// <summary>
        /// Коэффициент упаковки
        /// "гениальное изобретение фин.отдела"
        /// </summary>
        private const double KUp = 1.03;

        /// <summary>
        /// Имеется калькуляция
        /// </summary>
        public bool HasCalculation { get; } = false;

        private string _analog;
        /// <summary>
        /// По какому аналогу взят прайс
        /// </summary>
        public string Analog
        {
            get
            {
                if (HasCalculation) return "По калькуляции.";
                if (_analog != null) return _analog;
                if (Prices.Any(x => x.Analog != null)) return "Присутствуют аналоги";
                return null;
            }
        }

        public string Name { get; set; }

        public double Amount { get; private set; } = 1;

        public double SumTotal => SumPriceTotal + SumFixedTotal;

        private double? _sumPrice;

        /// <summary>
        /// Себестоимость с учетом коэффициента упаковки
        /// </summary>
        public double SumPriceTotal
        {
            get
            {
                if (_sumPrice != null)
                    return KUp * _sumPrice.Value * Amount;

                var sumPriceMainBlock = PriceMainBlock?.SumPriceTotal ?? 0;

                return sumPriceMainBlock 
                    + PricesOfDependentBlocks.Sum(x => x.SumPriceTotal) 
                    + PricesProductsIncluded.Sum(x => x.SumPriceTotal);
            }
        }

        private double? _sumFixed;
        /// <summary>
        /// Стоимость блоков с фиксированной ценой
        /// </summary>
        public double SumFixedTotal
        {
            get
            {
                if (_sumFixed.HasValue)
                    return _sumFixed.Value * Amount;

                var sumPriceMainBlock = PriceMainBlock?.SumFixedTotal ?? 0;

                return sumPriceMainBlock 
                    + PricesOfDependentBlocks.Sum(x => x.SumFixedTotal) 
                    + PricesProductsIncluded.Sum(x => x.SumFixedTotal);
            }
        }

        /// <summary>
        /// Прайс главного блока
        /// </summary>
        public Price PriceMainBlock { get; private set; }

        /// <summary>
        /// Прайсы зависимых блоков
        /// </summary>
        public List<Price> PricesOfDependentBlocks { get; } = new List<Price>();

        /// <summary>
        /// Прайсы включенного оборудования
        /// </summary>
        public List<Price> PricesProductsIncluded { get; } = new List<Price>();

        public List<Price> Prices
        {
            get
            {
                var prices = PricesProductsIncluded.ToList();
                if (HasCalculation)
                {
                    prices.Add(new Price(PriceMainBlock.Name, PricesOfDependentBlocks));
                }
                else
                {
                    if (PriceMainBlock != null) 
                        prices.Add(new Price(PriceMainBlock.Name, PricesOfDependentBlocks.Union(new List<Price> { PriceMainBlock })));
                }

                return prices.OrderByDescending(x => x.SumTotal).ToList();
            }
        }

        public Price(IUnit unit, DateTime targetDate, IPriceService priceService)
        {
            Name = unit.Product.ToString();
            var productsIncluded = unit.ProductsIncluded;

            //если есть калькуляция
            var priceCalculationItem = priceService.GetPriceCalculationItem(unit);
            if (priceCalculationItem != null)
            {
                HasCalculation = true;
                PriceMainBlock = new Price(unit.Product.ToString(), 1);
                PricesOfDependentBlocks = priceCalculationItem.StructureCosts
                    .Where(x => x.UnitPrice.HasValue)
                    .Select(x => new Price($"{x.Comment} ({x.Number})", x.Amount, x.UnitPrice.Value))
                    .ToList();

                productsIncluded = unit.ProductsIncluded.Where(x => x.Product.HasBlockWithFixedCost).ToList();
            }
            else
            {
                InitByProduct(unit.Product, targetDate, priceService);
            }

            //включенное оборудование
            PricesProductsIncluded = productsIncluded
                .Select(x => new Price(x.Product, targetDate, priceService, x.AmountOnUnit))
                .ToList();
        }

        public Price(Product product, DateTime targetDate, IPriceService priceService, double amount = 1)
        {
            Name = product.ToString();
            InitByProduct(product, targetDate, priceService, amount);
        }

        private void InitByProduct(Product product, DateTime targetDate, IPriceService priceService, double amount = 1)
        {
            Amount = amount;
            PriceMainBlock = new Price(product.ProductBlock, targetDate, priceService, amount);
            foreach (var dependentProduct in product.DependentProducts)
            {
                PricesOfDependentBlocks.Add(new Price(dependentProduct.Product, targetDate, priceService, dependentProduct.Amount));
            }
        }

        public Price(ProductBlock productBlock, DateTime targetDate, IPriceService priceService, double amount = 1)
        {
            Amount = amount;

            if (productBlock.HasPrice || productBlock.HasFixedPrice)
                //инициализация по прайсу/фиксированной цене
                InitByBlock(productBlock, targetDate);
            else
                //инициализация по аналогу
                InitByBlock(priceService.GetAnalogWithPrice(productBlock), targetDate, productBlock);
        }

        private void InitByBlock(ProductBlock productBlock, DateTime targetDate, ProductBlock originalBlock = null)
        {
            if (originalBlock == null)
            {
                Name = productBlock.ToString();                
            }
            else
            {
                Name = originalBlock.ToString();
                _analog = productBlock.ToString();
            }

            //по фиксированной цене
            if (productBlock.HasFixedPrice)
            {
                _sumFixed = productBlock.FixedCosts.GetClosedSumOnDate(targetDate).Sum;
                return;
            }

            //по прайсу
            if (productBlock.HasPrice)
            {
                _sumPrice = productBlock.Prices.GetClosedSumOnDate(targetDate).Sum;
            }
        }

        public Price(string name, IEnumerable<Price> pricesProductsIncluded)
        {
            Name = name;
            PricesProductsIncluded = pricesProductsIncluded.ToList();
        }

        public Price(string name, double amount, double? price = null)
        {
            Name = name;
            Amount = amount;
            _sumPrice = price;
        }
    }
}