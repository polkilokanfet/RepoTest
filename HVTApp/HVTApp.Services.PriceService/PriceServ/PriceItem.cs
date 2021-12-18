using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService.PriceServ
{
    /// <summary>
    /// ������ �� ������� ������ � ����� ��������� �������
    /// </summary>
    internal class PriceItem
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