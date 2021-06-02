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
        double? LaborHoursOnAmount { get; }

        /// <summary>
        /// ���� ������ �����
        /// </summary>
        double? WageFund { get; }

        /// <summary>
        /// ���� ������ ����� * ����������
        /// </summary>
        double? WageFundOnAmount { get; }
    }
}