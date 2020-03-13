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
        /// ����������� ��������
        /// "���������� ����������� ���.������"
        /// </summary>
        private const double KUp = 1.03;

        /// <summary>
        /// ������� �����������
        /// </summary>
        public bool HasCalculation { get; } = false;

        private string _analog;
        /// <summary>
        /// �� ������ ������� ���� �����
        /// </summary>
        public string Analog
        {
            get
            {
                if (_analog != null) return _analog;
                if (Prices.Any(x => x.Analog != null)) return "���� �������";
                return null;
            }
        }

        public string Name { get; set; }

        public double Amount { get; private set; } = 1;

        public double SumTotal => Amount * (SumPriceTotal + SumFixedTotal);

        private double? _sumPrice;

        /// <summary>
        /// ������������� � ������ ������������ ��������
        /// </summary>
        public double SumPriceTotal
        {
            get
            {
                return KUp * (_sumPrice * Amount ?? 0 + (PricesOfBlocks.Sum(x => x.SumPriceTotal * x.Amount) + PricesProductsIncluded.Sum(x => x.SumPriceTotal * x.Amount)));
            }
        }

        private double? _sumFixed;

        /// <summary>
        /// ��������� ������ � ������������� �����
        /// </summary>
        public double SumFixedTotal
        {
            get
            {
                return _sumFixed * Amount ?? 0 + (PricesOfBlocks.Sum(x => x.SumFixedTotal * x.Amount) + PricesProductsIncluded.Sum(x => x.SumFixedTotal * x.Amount));
            }
        }

        public List<Price> PricesOfBlocks { get; } = new List<Price>();

        public List<Price> PricesProductsIncluded { get; } = new List<Price>();

        public List<Price> Prices => PricesOfBlocks.Union(PricesProductsIncluded).OrderByDescending(x => x.SumTotal).ToList();

        public Price(IUnit unit, DateTime targetDate, IPriceService priceService)
        {
            Name = unit.Product.ToString();
            var productsIncluded = unit.ProductsIncluded;

            //���� ���� �����������
            var priceByCalculations = priceService.GetPriceByCalculations(unit);
            if (priceByCalculations != null)
            {
                HasCalculation = true;
                _sumPrice = priceByCalculations.Value;

                productsIncluded = unit.ProductsIncluded.Where(x => x.Product.HasBlockWithFixedCost).ToList();
            }
            else
            {
                InitByProduct(unit.Product, targetDate, priceService);
            }

            //���������� ������������
            foreach (var productIncluded in productsIncluded)
            {
                double amount = (double)productIncluded.Amount / (double)productIncluded.ParentsCount;
                PricesProductsIncluded.Add(new Price(productIncluded.Product, targetDate, priceService, amount));
            }            
        }

        public Price(Product product, DateTime targetDate, IPriceService priceService, double amount = 1)
        {
            Name = product.ToString();
            InitByProduct(product, targetDate, priceService, amount);
        }

        private void InitByProduct(Product product, DateTime targetDate, IPriceService priceService, double amount = 1)
        {
            Amount = amount;
            PricesOfBlocks.Add(new Price(product.ProductBlock, targetDate, priceService, amount));
            foreach (var dependentProduct in product.DependentProducts)
            {
                PricesOfBlocks.Add(new Price(dependentProduct.Product, targetDate, priceService, dependentProduct.Amount));
            }
        }

        public Price(ProductBlock productBlock, DateTime targetDate, IPriceService priceService, double amount = 1)
        {
            Amount = amount;

            if (productBlock.HasPrice || productBlock.HasFixedPrice)
            {
                //������������� �� ������/������������� ����
                InitByBlock(productBlock, targetDate);
            }
            else
            {
                //������������� �� �������
                InitByBlock(priceService.GetAnalogWithPrice(productBlock), targetDate, productBlock);
            }
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

            //�� ������������� ����
            if (productBlock.HasFixedPrice)
            {
                _sumFixed = productBlock.FixedCosts.GetClosedSumOnDate(targetDate).Sum;
                return;
            }

            //�� ������
            if (productBlock.HasPrice)
            {
                _sumPrice = productBlock.Prices.GetClosedSumOnDate(targetDate).Sum;
            }
        }
    }
}