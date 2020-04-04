using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.ManagersSalesChart
{
    public class ManagersSalesChartItem
    {
        private readonly List<SalesUnit> _salesUnits;

        public string Manager => _salesUnits.First().Project.Manager.Employee.Person.Surname;

        public double Sum => _salesUnits.Sum(x => x.Cost);

        public ManagersSalesChartItem(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits.ToList();
        }
    }
}