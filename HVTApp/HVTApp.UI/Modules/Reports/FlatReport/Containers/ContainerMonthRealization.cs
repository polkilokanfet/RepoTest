using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class ContainerMonthRealization : ContainerMonth
    {
        public ContainerMonthRealization(IEnumerable<FlatReportItem> flatReportItems, double accuracy) : base(flatReportItems, accuracy)
        {
        }

        public ContainerMonthRealization(DateTime date, double accuracy) : base(date, accuracy)
        {
        }


        protected override void FillYearAndMonth(IEnumerable<FlatReportItem> flatReportItems)
        {
            Year = Items.First().EstimatedRealizationDate.Year;
            Month = Items.First().EstimatedRealizationDate.Month;
        }
    }
}