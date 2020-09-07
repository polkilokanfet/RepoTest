using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class ContainerMonthOit : ContainerMonth
    {
        public ContainerMonthOit(IEnumerable<FlatReportItem> flatReportItems, double accuracy) : base(flatReportItems, accuracy)
        {
        }

        public ContainerMonthOit(DateTime date, double accuracy) : base(date, accuracy)
        {
        }

        /// <summary>
        /// Присвоить предложенным датам значения из контейнера
        /// </summary>
        public void FillEstimatedDates()
        {
            foreach (var item in Items.ToList())
            {
                if (Year != item.OriginalOrderInTakeDate.Year || Month != item.OriginalOrderInTakeDate.Month)
                {
                    item.EstimatedOrderInTakeDate = GetNearestDate(item.OriginalOrderInTakeDate);
                }
            }
        }

        private DateTime GetNearestDate(DateTime date)
        {
            var minDate = new DateTime(Year, Month, 1);
            var maxDate = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
            return
                Math.Abs((minDate - date).Days) < Math.Abs((maxDate - date).Days)
                    ? minDate
                    : maxDate;
        }

        protected override void FillYearAndMonth(IEnumerable<FlatReportItem> flatReportItems)
        {
            Year = Items.First().EstimatedOrderInTakeDate.Year;
            Month = Items.First().EstimatedOrderInTakeDate.Month;
        }
    }
}