using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts
{
    public abstract class SalesChartItem
    {
        private readonly double _sumOfAll;
        protected readonly List<SalesUnit> SalesUnits;

        public abstract string Title { get; }
        public abstract string ItemName { get; }

        public double Sum => SalesUnits.Sum(x => x.Cost);

        public double Percent => Sum / _sumOfAll;

        public double Percent100 => Percent * 100;

        protected SalesChartItem(IEnumerable<SalesUnit> salesUnits, double sumOfAll)
        {
            _sumOfAll = sumOfAll;
            SalesUnits = salesUnits.ToList();
        }
    }
}