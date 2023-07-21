using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class SalesChartViewModel : ViewModelBase
    {
        private List<SalesUnit> _salesUnits;
        private DateTime _startDate;
        private DateTime _finishDate;

        public ObservableCollection<SumOnMonth> SumsOnMonths { get; } = new ObservableCollection<SumOnMonth>();

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if(Equals(_startDate, value)) return;
                _startDate = value;
                Load();
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                if (Equals(_finishDate, value)) return;
                _finishDate = value;
                Load();
            }
        }

        public SalesChartViewModel(IUnityContainer container) : base(container)
        {
            _salesUnits = GlobalAppProperties.UserIsManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && !x.IsLoosen && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && !x.IsLoosen);

            _startDate = _salesUnits.Min(x => x.OrderInTakeDate);
            _finishDate = _salesUnits.Max(x => x.OrderInTakeDate);

            Load();
        }

        private void Load()
        {
            var sumsOnMonths = _salesUnits.Where(x => x.OrderInTakeDate >= _startDate && x.OrderInTakeDate <= _finishDate).GroupBy(x => new
                {
                    x.OrderInTakeDate.Year,
                    x.OrderInTakeDate.Month
                })
                .Select(x => new SumOnMonth(x))
                .ToList();

            //добавл€ем нулевые мес€цы
            var date = new DateTime(_startDate.Year, _startDate.Month, 1);
            while (date <= _finishDate)
            {
                if (!sumsOnMonths.Any(x => x.Date.Year == date.Year && x.Date.Month == date.Month))
                {
                    sumsOnMonths.Add(new SumOnMonth(new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))));
                }

                date = date.AddMonths(1);
            }
            
            SumsOnMonths.Clear();
            SumsOnMonths.AddRange(sumsOnMonths.OrderBy(x => x.Date));
        }
    }

    public class SumOnMonth
    {
        public SumOnMonth(IEnumerable<SalesUnit> salesUnits)
        {
            Sum = salesUnits.Sum(x => x.Cost) / 1000000;
            var date = salesUnits.First().OrderInTakeDate;
            Date = new DateTime(date.Year, date.Month, salesUnits.Max(x => x.OrderInTakeDate).Day);
        }

        public SumOnMonth(DateTime date)
        {
            Date = date;
        }

        public double Sum { get; } = 0;
        public DateTime Date { get; }
    }
}