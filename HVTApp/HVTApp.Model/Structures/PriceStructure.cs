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

        public Product Product { get; }
        public double Amount { get; }

        /// <summary>
        /// ������ �������� �����.
        /// </summary>
        public ProductBlock Analog { get; set; }

        /// <summary>
        /// ������� ���� (�� ������� ���� ������ ������).
        /// </summary>
        public DateTime TargetPriceDate { get; }

        /// <summary>
        /// ���������� ���������
        /// </summary>
        public List<PriceStructure> DependentProductsPriceStructures { get; } = new List<PriceStructure>();

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
        /// ������������� ��� ����� ������ � ������������� ����� (���� ���� �����������, ������������� ����� = 0).
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
        /// ��������� ��������� ������ � ������������� �����.
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
        /// ������������� ����� ��� ����� ������ � ������������� ����� (���� ���� �����������, ������������� ����� = 0).
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
        /// ��������� ��������� ������ � ������������� �����.
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

    }
}