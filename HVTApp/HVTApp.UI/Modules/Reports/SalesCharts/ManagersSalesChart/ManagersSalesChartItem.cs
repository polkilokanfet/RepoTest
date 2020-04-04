using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ManagersSalesChart
{
    public class ManagersSalesChartItem : SalesChartItem
    {
        public string Manager => SalesUnits.First().Project.Manager.Employee.Person.Surname;

        public ManagersSalesChartItem(IEnumerable<SalesUnit> salesUnits) : base(salesUnits)
        {
        }
    }
}