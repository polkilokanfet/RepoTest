using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Structures
{
    /// <summary>
    /// ��������� �������������
    /// </summary>
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
    }
}