using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class FlatReportItemManagerContainer
    {
        public string Manager { get; }
        public int Year { get; }
        public double Sum { get; }

        public FlatReportItemManagerContainer(IEnumerable<FlatReportItem> items)
        {
            Manager = items.First().Manager;
            Year = items.First().EstimatedOrderInTakeDate.Year;
            Sum = items.Sum(x => x.Sum);
        }
    }
}