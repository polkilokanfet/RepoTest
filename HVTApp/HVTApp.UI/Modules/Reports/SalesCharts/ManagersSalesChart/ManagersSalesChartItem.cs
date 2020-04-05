using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ManagersSalesChart
{
    public class ManagersSalesChartItem : SalesChartItem
    {
        public ManagersSalesChartItem(IEnumerable<SalesUnit> salesUnits, double sumOfAll) : base(salesUnits, sumOfAll)
        {
        }

        public override string ItemName => SalesUnits.First().Project.Manager.Employee.Person.Surname;

        public override string Title => "Менеджер";
    }
}