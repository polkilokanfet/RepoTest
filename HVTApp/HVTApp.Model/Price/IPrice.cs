using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Price
{
    public interface IPrice : IProfitability
    {
        /// <summary>
        /// ���-���� � ������ ���� ������
        /// </summary>
        bool ContainsAnyAnalog { get; }

        /// <summary>
        /// �� ������ ������� ���� �����
        /// </summary>
        ProductBlock Analog { get; }

        string Comment { get; }

        string Name { get; }
        double Amount { get; }

        /// <summary>
        /// �� + ��
        /// </summary>
        double SumTotal { get; }

        /// <summary>
        /// ������������� � ������ ������������ ��������
        /// </summary>
        double SumPriceTotal { get; }

        /// <summary>
        /// ��������� ������ � ������������� �����
        /// </summary>
        double SumFixedTotal { get; }

        List<IPrice> Prices { get; }
    }
}