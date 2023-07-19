using System;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ������ �� ������� ������ � ����� ��������� �������
    /// </summary>
    public class PriceItem
    {
        /// <summary>
        /// ������ �� ������� ������
        /// </summary>
        public PriceCalculationItem PriceCalculationItem { get; }

        /// <summary>
        /// ���� ��������� �������
        /// </summary>
        public DateTime FinishDate { get; }

        public PriceItem(PriceCalculation priceCalculation, PriceCalculationItem priceCalculationItem)
        {
            if (priceCalculation.TaskCloseMoment != null) 
                FinishDate = priceCalculation.TaskCloseMoment.Value;
            PriceCalculationItem = priceCalculationItem;
        }
    }
}