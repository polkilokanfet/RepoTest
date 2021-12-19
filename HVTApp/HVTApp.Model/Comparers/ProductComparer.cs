using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Comparers
{
    /// <summary>
    /// ��������� �� ���������� �������� (����������� ����������)
    /// </summary>
    public class ProductComparer : IComparer<Product>
    {
        public int Compare(Product productX, Product productY)
        {
            //���� ���� ����������� ����������
            var voltageX = productX.GetVoltageParameter();
            var voltageY = productY.GetVoltageParameter();

            if (voltageX != null && voltageY != null)
            {
                return voltageX.Rang - voltageY.Rang;
            }

            if (voltageX != null)
            {
                return -1;
            }

            if (voltageY != null)
            {
                return 1;
            }

            //���� ��� ������������ ����������
            return 0;
        }
    }
}