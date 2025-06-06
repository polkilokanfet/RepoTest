using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Price
{
    public interface IPrice : IProfitability
    {
        ProductBlock OriginalBlock { get; }

        /// <summary>
        /// �� ������ ������� ���� �����
        /// </summary>
        ProductBlock Analog { get; }

        /// <summary>
        /// ���-���� � ������ ���� ������
        /// </summary>
        bool ContainsAnyAnalog { get; }

        /// <summary>
        /// ���-���� � ������ ���� ���� ��� �����-�����
        /// </summary>
        bool ContainsAnyBlockWithNoLaborHours { get; }

        string Comment { get; }
        string CommentLaborHours { get; }

        string Name { get; }
        double Amount { get; }

        /// <summary>
        /// �� + ��
        /// </summary>
        double SumTotal { get; }

        /// <summary>
        /// ������������� � ������ ������������ �������� / ����������
        /// </summary>
        double SumPriceOnUnit { get; }

        /// <summary>
        /// ������������� � ������ ������������ ��������
        /// </summary>
        double SumPriceTotal { get; }

        /// <summary>
        /// ��������� ������ � ������������� ����� / ����������
        /// </summary>
        double SumFixedOnUnit { get; }

        /// <summary>
        /// ��������� ������ � ������������� �����
        /// </summary>
        double SumFixedTotal { get; }

        List<IPrice> Prices { get; }
    }
}