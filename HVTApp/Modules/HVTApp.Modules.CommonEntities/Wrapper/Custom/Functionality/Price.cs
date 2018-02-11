using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public class Price
    {
        public Price(ProductBlock productBlock, CostOnDate costOnDate, DateTime targetDate)
        {
            ProductBlock = productBlock;
            CostOnDate = costOnDate;
            TargetDate = targetDate;

            if (CostOnDate == null)
            {
                Status = PriceStatus.Miss;
            }
            else
            {
                Status = CostOnDate.Date.AddDays(90) > targetDate ? PriceStatus.Actual : PriceStatus.NotActual;
            }
        }

        public ProductBlock ProductBlock { get; }
        public CostOnDate CostOnDate { get; }
        public DateTime TargetDate { get; }
        public PriceStatus Status { get; }

        public enum PriceStatus
        {
            Actual,
            NotActual,
            Miss
        }
    }
}