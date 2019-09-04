using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Structures
{
    public class PriceStructures : List<PriceStructure>
    {
        /// <summary>
        /// ������������� ��� ����� ������ � ������������� �����.
        /// </summary>
        public double TotalPriceFixedCostLess => this.Sum(x => x.TotalPriceFixedCostLess);

        /// <summary>
        /// ��������� ��������� ������ � ������������� �����.
        /// </summary>
        public double TotalFixedCost => this.Sum(x => x.TotalFixedCost);

        /// <summary>
        /// ������������� ����� ��� ����� ������ � ������������� �����.
        /// </summary>
        public double TotalServicePriceFixedCostLess => this.Sum(x => x.TotalPriceServiceFixedCostLess);

        /// <summary>
        /// ��������� ��������� ����� � ������������� �����.
        /// </summary>
        public double TotalServiceFixedCost => this.Sum(x => x.TotalServiceFixedCost);

        public PriceStructures(IUnit unit, DateTime targetPriceDate, int priceTerm, IEnumerable<ProductBlock> analogs)
        {
            var productBlocks = analogs as ProductBlock[] ?? analogs.ToArray();

            //��������� ������������� ��������
            this.Add(new PriceStructure(unit.Product, 1, targetPriceDate, priceTerm, productBlocks));

            //��������� ������������� ���������� ���������
            foreach (var prodIncl in unit.ProductsIncluded)
            {
                double count = (double)prodIncl.Amount / prodIncl.ParentsCount;
                this.Add(new PriceStructure(prodIncl.Product, count, targetPriceDate, priceTerm, productBlocks));
            }
        }
    }
}