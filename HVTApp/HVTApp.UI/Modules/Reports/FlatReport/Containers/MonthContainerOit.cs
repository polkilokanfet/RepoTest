using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class MonthContainerOit : FlatReportItemMonthContainer
    {
        public MonthContainerOit(IEnumerable<FlatReportItem> flatReportItems, double targetSum, double accuracy) : base(flatReportItems, targetSum, accuracy)
        {
        }

        public MonthContainerOit(DateTime date, double targetSum, double accuracy) : base(date, targetSum, accuracy)
        {
        }

        /// <summary>
        /// Присвоить предложенным датам значения из контейнера
        /// </summary>
        public void FillEstimatedOrderInTakeDates()
        {
            foreach (var flatReportItem in FlatReportItems.ToList())
            {
                if (Year != flatReportItem.OriginalOrderInTakeDate.Year || Month != flatReportItem.OriginalOrderInTakeDate.Month)
                {
                    flatReportItem.EstimatedOrderInTakeDate = GetNearestDate(flatReportItem.OriginalOrderInTakeDate);
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
            Year = FlatReportItems.First().EstimatedOrderInTakeDate.Year;
            Month = FlatReportItems.First().EstimatedOrderInTakeDate.Month;
        }
    }
}