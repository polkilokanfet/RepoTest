using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Modules.Reports.MarketReport
{
    public class ProducersMarket
    {
        public string Producer { get; }

        public ProducersMarket(IEnumerable<MarketReportUnit> units)
        {
            Producer = units.First().Producer;
        }
    }
}