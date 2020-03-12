using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Structures
{
    public class PriceStructure
    {
        private readonly int _priceTerm;

        public Product Product { get; }
        public double Amount { get; }

        /// <summary>
        /// Аналог целевого блока.
        /// </summary>
        public ProductBlock Analog { get; set; }

        /// <summary>
        /// Целевая дата (на которую идет расчет прайса).
        /// </summary>
        public DateTime TargetPriceDate { get; }

        /// <summary>
        /// Включенные структуры
        /// </summary>
        public List<PriceStructure> DependentProductsPriceStructures { get; } = new List<PriceStructure>();

        /// <summary>
        /// Прайс, близжайший к целевой дате
        /// </summary>
        public SumOnDate Price
        {
            get
            {
                if (Product.ProductBlock.Prices.Any())
                    return Product.ProductBlock.Prices.GetClosedSumOnDate(TargetPriceDate);

                if (Product.ProductBlock.FixedCosts.Any())
                    return Product.ProductBlock.FixedCosts.GetClosedSumOnDate(TargetPriceDate);

                return Analog.Prices.GetClosedSumOnDate(TargetPriceDate);
            }
        }

        public double CostService { get; set; }

        /// <summary>
        /// У продукта нет прайса, взят прайс аналога
        /// </summary>
        public bool IsAnalogPrice => !Product.ProductBlock.Prices.Any() && !Product.ProductBlock.FixedCosts.Any();

        /// <summary>
        /// Прайс устарел
        /// </summary>
        public bool IsOldPrice => Price.Date.AddDays(_priceTerm) < DateTime.Today;

        /// <summary>
        /// Фиксированная цена на блок (например, шеф-монтаж)
        /// </summary>
        public SumOnDate FixedCost
        {
            get
            {
                if (!Product.ProductBlock.FixedCosts.Any()) return null;

                var costsBefore = Product.ProductBlock.FixedCosts.Where(x => x.Date <= TargetPriceDate).ToList();
                return costsBefore.Any() 
                    ? costsBefore.OrderBy(x => x.Date).Last() 
                    : Product.ProductBlock.FixedCosts.OrderBy(x => x.Date).First();
            }
        }

        public bool IsFixedCost => FixedCost != null;

        /// <summary>
        /// Себестоимость без учета блоков с фиксированной ценой (если цена фиксирована, себестоимость блока = 0).
        /// </summary>
        public double TotalPriceFixedCostLess
        {
            get
            {
                var price = FixedCost == null ? Price.Sum * Amount : 0;
                return price + DependentProductsPriceStructures.Sum(x => x.TotalPriceFixedCostLess);
            }
        }

        /// <summary>
        /// Суммарная стоимость блоков с фиксированной ценой.
        /// </summary>
        public double TotalFixedCost
        {
            get
            {
                double fixedCost = FixedCost?.Sum ?? 0;
                return fixedCost * Amount + DependentProductsPriceStructures.Sum(x => x.TotalFixedCost);
            }
        }

        /// <summary>
        /// Себестоимость услуг без учета блоков с фиксированной ценой (если цена фиксирована, себестоимость блока = 0).
        /// </summary>
        public double TotalPriceServiceFixedCostLess
        {
            get
            {
                var price = Product.ProductBlock.IsService && FixedCost == null ? Price.Sum * Amount : 0;
                return price + DependentProductsPriceStructures.Sum(x => x.TotalPriceServiceFixedCostLess);
            }
        }

        /// <summary>
        /// Суммарная стоимость блоков с фиксированной ценой.
        /// </summary>
        public double TotalServiceFixedCost
        {
            get
            {
                double fixedCost = 0;

                if(Product.ProductBlock.IsService)
                    fixedCost = FixedCost?.Sum ?? 0;

                return fixedCost * Amount + DependentProductsPriceStructures.Sum(x => x.TotalServiceFixedCost);
            }
        }

        public PriceStructure(Product product, double amount, DateTime targetPriceDate, int priceTerm)
        {
            _priceTerm = priceTerm;

            Product = product;
            Amount = amount;
            TargetPriceDate = targetPriceDate;
        }

    }
}