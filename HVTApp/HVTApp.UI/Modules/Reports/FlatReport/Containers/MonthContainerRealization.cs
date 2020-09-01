using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class MonthContainerRealization : FlatReportItemMonthContainer
    {
        public MonthContainerRealization(IEnumerable<FlatReportItem> flatReportItems, double targetSum, double accuracy) : base(flatReportItems, targetSum, accuracy)
        {
        }

        public MonthContainerRealization(DateTime date, double targetSum, double accuracy) : base(date, targetSum, accuracy)
        {
        }


        protected override void FillYearAndMonth(IEnumerable<FlatReportItem> flatReportItems)
        {
            Year = FlatReportItems.First().EstimatedRealizationDate.Year;
            Month = FlatReportItems.First().EstimatedRealizationDate.Month;
        }
    }
}