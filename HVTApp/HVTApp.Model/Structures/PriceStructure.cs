using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Structures
{
    public class PriceStructures : List<PriceStructure>
    {
        public double Total => this.Sum(x => x.Total);

        public PriceStructures(IUnitPoco salesUnit, DateTime targetPriceDate, int priceTerm, IEnumerable<ProductBlock> analogs)
        {
            var productBlocks = analogs as ProductBlock[] ?? analogs.ToArray();

            //��������� ������������� ��������
            this.Add(new PriceStructure(salesUnit.Product, 1, targetPriceDate, priceTerm, productBlocks));

            //��������� ������������� ���������� ���������
            foreach (var prodIncl in salesUnit.ProductsIncluded)
            {
                double count = (double)prodIncl.Amount / prodIncl.ParentsCount;
                this.Add(new PriceStructure(prodIncl.Product, count, targetPriceDate, priceTerm, productBlocks));
            }
        }
    }

    public class PriceStructure
    {
        private readonly int _priceTerm;
        private readonly IEnumerable<ProductBlock> _analogs;

        public Product Product { get; }
        public double Amount { get; }

        public ProductBlock Analog => IsAnalogPrice ? GetAnalogWithPrice() : null;

        /// <summary>
        /// ������� ���� (�� ������� ���� ������ ������).
        /// </summary>
        public DateTime TargetPriceDate { get; }

        /// <summary>
        /// ���������� ���������
        /// </summary>
        public List<PriceStructure> Childs { get; } = new List<PriceStructure>();

        /// <summary>
        /// �����, ���������� � ������� ����
        /// </summary>
        public SumOnDate Price => GetClosedSumOnDate(IsAnalogPrice ? Analog.Prices : Product.ProductBlock.Prices, TargetPriceDate);

        /// <summary>
        /// � �������� ��� ������, ���� ����� �������
        /// </summary>
        public bool IsAnalogPrice => !Product.ProductBlock.Prices.Any();

        /// <summary>
        /// ����� �������
        /// </summary>
        public bool IsOldPrice => Price.Date.AddDays(_priceTerm) < DateTime.Today;

        public double Total => Price.Sum * Amount + Childs.Sum(x => x.Total);

        public PriceStructure(Product product, double amount, DateTime targetPriceDate, int priceTerm, IEnumerable<ProductBlock> analogs)
        {
            _priceTerm = priceTerm;
            _analogs = analogs;
            Product = product;
            Amount = amount;
            TargetPriceDate = targetPriceDate;
            GenerateChilds();
        }

        private void GenerateChilds()
        {
            foreach (var dependentProduct in Product.DependentProducts)
            {
                Childs.Add(new PriceStructure(dependentProduct.Product, dependentProduct.Amount, TargetPriceDate, _priceTerm, _analogs));
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

            var dic2 = dic.OrderByDescending(x => x.Value).ToList();
            var result = dic.OrderByDescending(x => x.Value).First(x => x.Key.Prices.Any()).Key;

            return dic.OrderByDescending(x => x.Value).First(x => x.Key.Prices.Any()).Key;
        }
    }
}