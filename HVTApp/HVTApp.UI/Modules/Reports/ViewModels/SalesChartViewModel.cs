using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class SalesChartViewModel : ViewModelBase
    {
        public List<SumOnMonth> SumsOnMonths { get; }
        public SalesChartViewModel(IUnityContainer container) : base(container)
        {
            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && !x.IsLoosen && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && !x.IsLoosen);

            SumsOnMonths = salesUnits.GroupBy(x => new
                {
                    x.OrderInTakeDate.Year,
                    x.OrderInTakeDate.Month
                })
                .OrderBy(x => x.Key.Year).ThenBy(x => x.Key.Month)
                .Select(x => new SumOnMonth(x))
                .ToList();
        }
    }

    public class SumOnMonth
    {
        public SumOnMonth(IEnumerable<SalesUnit> salesUnits)
        {
            Sum = salesUnits.Sum(x => x.Cost) / 1000000;
            var date = salesUnits.First().OrderInTakeDate;
            Date = new DateTime(date.Year, date.Month, 28);
        }

        public double Sum { get; }
        public DateTime Date { get; }
    }
}