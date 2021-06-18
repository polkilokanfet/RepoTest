namespace HVTApp.Model.Price
{
    public interface IProfitability
    {
        /// <summary>
        /// ���������� �����-����� �� ������������ ����� ��������
        /// </summary>
        double? LaborHours { get; }

        /// <summary>
        /// ���������� �����-����� �� ������������ ����� �������� * ����������
        /// </summary>
        double? LaborHoursTotal { get; }

        /// <summary>
        /// ���� ������ �����
        /// </summary>
        double? WageFund { get; }

        /// <summary>
        /// ���� ������ ����� * ����������
        /// </summary>
        double? WageFundTotal { get; }
    }
}