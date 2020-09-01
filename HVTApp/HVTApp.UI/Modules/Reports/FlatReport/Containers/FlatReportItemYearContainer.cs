using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HVTApp.UI.Modules.Reports.FlatReport.Containers
{
    public class FlatReportItemYearContainer
    {
        public ObservableCollection<FlatReportItemMonthContainer> MonthContainers { get; }
        public int Year => MonthContainers.First().Year;
        public double Sum => MonthContainers.Sum(x => x.CurrentSum);

        public FlatReportItemYearContainer(IEnumerable<FlatReportItemMonthContainer> monthContainers)
        {
            MonthContainers = new ObservableCollection<FlatReportItemMonthContainer>(monthContainers);
        }
    }
}