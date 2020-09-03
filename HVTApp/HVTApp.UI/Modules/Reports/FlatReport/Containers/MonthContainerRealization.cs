using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class MonthContainerRealization : FlatReportItemMonthContainer
    {
        public MonthContainerRealization(IEnumerable<FlatReportItem> flatReportItems, double accuracy) : base(flatReportItems, accuracy)
        {
        }

        public MonthContainerRealization(DateTime date, double accuracy) : base(date, accuracy)
        {
        }


        protected override void FillYearAndMonth(IEnumerable<FlatReportItem> flatReportItems)
        {
            Year = FlatReportItems.First().EstimatedRealizationDate.Year;
            Month = FlatReportItems.First().EstimatedRealizationDate.Month;
        }
    }
}