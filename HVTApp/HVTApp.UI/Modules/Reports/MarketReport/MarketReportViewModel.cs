using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.MarketReport
{
    public class MarketReportViewModel : LoadableExportableViewModel
    {
        private List<MarketReportUnit> _marketReportUnits;
        private DateTime _startDate;
        private DateTime _finishDate;
        private IEnumerable<MarketReportUnit> _filteredUnits;

        public ObservableCollection<MarketReportUnit> Units { get; } = new ObservableCollection<MarketReportUnit>();

        public IEnumerable<MarketReportUnit> FilteredUnits
        {
            get { return _filteredUnits; }
            set { _filteredUnits = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (Equals(_startDate, value)) return;
                _startDate = value;
                RefreshUnits();
            }
        }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set
            {
                if (Equals(_finishDate, value)) return;
                _finishDate = value;
                RefreshUnits();
            }
        }

        public MarketReportViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager 
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport && x.Project.Manager.IsAppCurrentUser()) 
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport);

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(x => x.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(x => x.ProductsIncluded.Contains(productIncluded));
            }


            var groups = salesUnits.OrderBy(x => x.OrderInTakeDate).GroupBy(x => x, new SalesUnitsReportComparer());

            var tenders = UnitOfWork.Repository<Tender>().Find(x => true);

            _marketReportUnits = groups.Select(x => new MarketReportUnit(x, tenders.Where(t => Equals(x.Key.Project, t.Project)))).ToList();
        }

        protected override void AfterGetData()
        {
            _startDate = _marketReportUnits.Min(x => x.OrderInTakeDate);
            _finishDate = _marketReportUnits.Max(x => x.OrderInTakeDate);

            RefreshUnits();
        }

        private void RefreshUnits()
        {
            Units.Clear();
            Units.AddRange(_marketReportUnits.Where(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate));
        }
    }
}
