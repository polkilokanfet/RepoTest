using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Structures
{
    public class PriceStructure
    {
        private readonly int _priceTerm;
        private readonly IEnumerable<ProductBlock> _analogs;

        public Product Product { get; }
        public double Amount { get; }

        /// <summary>
        /// ������ �������� �����.
        /// </summary>
        public ProductBlock Analog => IsAnalogPrice ? GetAnalogWithPrice() : null;

        /// <summary>
        /// ������� ���� (�� ������� ���� ������ ������).
        /// </summary>
        public DateTime TargetPriceDate { get; }

        /// <summary>
        /// ���������� ���������
        /// </summary>
        public List<PriceStructure> ChildPriceStructures { get; } = new List<PriceStructure>();

        /// <summary>
        /// �����, ���������� � ������� ����
        /// </summary>
        public SumOnDate Price => GetClosedSumOnDate(IsAnalogPrice ? Analog.Prices : Product.ProductBlock.Prices, TargetPriceDate);

        public double CostService { get; set; }

        /// <summary>
        /// � �������� ��� ������, ���� ����� �������
        /// </summary>
        public bool IsAnalogPrice => !Product.ProductBlock.Prices.Any();

        /// <summary>
        /// ����� �������
        /// </summary>
        public bool IsOldPrice => Price.Date.AddDays(_priceTerm) < DateTime.Today;

        /// <summary>
        /// ������������� ���� �� ���� (��������, ���-������)
        /// </summary>
        public SumOnDate FixedCost => Product.ProductBlock.FixedCosts.Any(x => x.Date <= TargetPriceDate)
            ? Product.ProductBlock.FixedCosts.Where(x => x.Date <= TargetPriceDate).OrderBy(x => x.Date).Last()
            : null;

        public bool IsFixedCost => FixedCost != null;

        /// <summary>
        /// ������������� ��� ����� ������ � ������������� ����� (���� ���� �����������, ������������� ����� = 0).
        /// </summary>
        public double TotalPriceFixedCostLess
        {
            get
            {
                var price = FixedCost == null ? Price.Sum * Amount : 0;
                return price + ChildPriceStructures.Sum(x => x.TotalPriceFixedCostLess);
            }
        }

        /// <summary>
        /// ��������� ��������� ������ � ������������� �����.
        /// </summary>
        public double TotalFixedCost
        {
            get
            {
                double fixedCost = FixedCost?.Sum ?? 0;
                return fixedCost * Amount + ChildPriceStructures.Sum(x => x.TotalFixedCost);
            }
        }

        /// <summary>
        /// ������������� ����� ��� ����� ������ � ������������� ����� (���� ���� �����������, ������������� ����� = 0).
        /// </summary>
        public double TotalPriceServiceFixedCostLess
        {
            get
            {
                var price = Product.ProductBlock.IsService && FixedCost == null ? Price.Sum * Amount : 0;
                return price + ChildPriceStructures.Sum(x => x.TotalPriceServiceFixedCostLess);
            }
        }

        /// <summary>
        /// ��������� ��������� ������ � ������������� �����.
        /// </summary>
        public double TotalServiceFixedCost
        {
            get
            {
                double fixedCost = 0;

                if(Product.ProductBlock.IsService)
                    fixedCost = FixedCost?.Sum ?? 0;

                return fixedCost * Amount + ChildPriceStructures.Sum(x => x.TotalServiceFixedCost);
            }
        }

        public PriceStructure(Product product, double amount, DateTime targetPriceDate, int priceTerm, IEnumerable<ProductBlock> analogs)
        {
            _priceTerm = priceTerm;
            _analogs = analogs;
            Product = product;
            Amount = amount;
            TargetPriceDate = targetPriceDate;
            
            //��������� �������� ���������
            foreach (var dependentProduct in Product.DependentProducts)
            {
                ChildPriceStructures.Add(new PriceStructure(dependentProduct.Product, dependentProduct.Amount, TargetPriceDate, _priceTerm, _analogs));
            }
        }

        /// <summary>
        /// ���������� ��������� � ���� �����.
        /// </summary>
        /// <param name="sumsOnDates"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private SumOnDate GetClosedSumOnDate(IEnumerable<SumOnDate> sumsOnDates, DateTime date)
        {
            var sumOnDates = sumsOnDates as SumOnDate[] ?? sumsOnDates.ToArray();
            var dif = sumOnDates.Select(x => Math.Abs((x.Date - date).Days)).Min();
            return sumOnDates.First(x => x.Date == date.AddDays(-dif) || x.Date == date.AddDays(dif));
        }

        /// <summary>
        /// ����� ������� ��� �����.
        /// </summary>
        /// <returns></returns>
        private ProductBlock GetAnalogWithPrice()
        {
            var blocks = _analogs.ToList();
            var targetBlock = Product.ProductBlock;
            blocks.Remove(targetBlock);

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var block in blocks)
            {
                double dif = 0;

                //����� ���������
                var intParams = block.Parameters.Intersect(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in intParams)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));
                    dif += 1.0 / path.Parameters.Count;
                }

                //������������� ���������
                var difParams1 = block.Parameters.Except(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams1)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(block.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                var difParams2 = targetBlock.Parameters.Except(block.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams2)
                {
                    //Single ���� �� ����������, �� ���� ������ ������ � ������������ �����
                    var path = parameter.Paths().First(x => x.Parameters.AllContainsIn(targetBlock.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                dic.Add(block, dif);
            }

            return dic.OrderByDescending(x => x.Value).First(x => x.Key.Prices.Any()).Key;
        }
    }
}