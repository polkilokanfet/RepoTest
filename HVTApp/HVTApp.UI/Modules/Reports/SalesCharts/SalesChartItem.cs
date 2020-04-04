using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts
{
    public abstract class SalesChartItem
    {
        protected readonly List<SalesUnit> SalesUnits;

        public double Sum => SalesUnits.Sum(x => x.Cost);

        protected SalesChartItem(IEnumerable<SalesUnit> salesUnits)
        {
            SalesUnits = salesUnits.ToList();
        }
    }
}