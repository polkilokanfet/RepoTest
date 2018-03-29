using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public class Price
    {
        public Price(ProductBlock productBlock, SumOnDate sumOnDate, DateTime targetDate)
        {
            ProductBlock = productBlock;
            SumOnDate = sumOnDate;
            TargetDate = targetDate;

            if (SumOnDate == null)
            {
                Status = PriceStatus.Miss;
            }
            else
            {
                Status = SumOnDate.Date.AddDays(90) > targetDate ? PriceStatus.Actual : PriceStatus.NotActual;
            }
        }

        public ProductBlock ProductBlock { get; }
        public SumOnDate SumOnDate { get; }
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