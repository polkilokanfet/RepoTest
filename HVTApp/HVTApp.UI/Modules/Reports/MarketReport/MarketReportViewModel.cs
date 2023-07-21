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

        /// <summary>
        /// Только отчетное оборудование
        /// </summary>
        public bool IsReportUnitsOnly { get; set; } = true;

        public IEnumerable<MarketReportUnit> FilteredUnits
        {
            get => _filteredUnits;
            set => _filteredUnits = value;
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
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(Fit);

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(salesUnit => salesUnit.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(salesUnit => salesUnit.ProductsIncluded.Contains(productIncluded));
            }


            var groups = salesUnits.OrderBy(salesUnit => salesUnit.OrderInTakeDate).GroupBy(x => x, new SalesUnitsReportComparer());

            var tenders = UnitOfWork.Repository<Tender>().GetAll();

            _marketReportUnits = groups.Select(x => new MarketReportUnit(x, tenders.Where(tender => Equals(x.Key.Project, tender.Project)))).ToList();
        }

        /// <summary>
        /// Подходит ли юнит?
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <returns></returns>
        private bool Fit(SalesUnit salesUnit)
        {
            if (salesUnit.IsRemoved)
                return false;

            if (IsReportUnitsOnly)
                if (salesUnit.Project.ForReport == false)
                    return false;

            if (GlobalAppProperties.UserIsManager)
                return salesUnit.Project.Manager.IsAppCurrentUser();

            return true;
        }

        protected override void AfterGetData()
        {
            _startDate = _marketReportUnits.Min(marketReportUnit => marketReportUnit.OrderInTakeDate);
            _finishDate = _marketReportUnits.Max(marketReportUnit => marketReportUnit.OrderInTakeDate);

            RefreshUnits();
        }

        private void RefreshUnits()
        {
            Units.Clear();
            Units.AddRange(_marketReportUnits.Where(marketReportUnit => marketReportUnit.OrderInTakeDate >= StartDate && marketReportUnit.OrderInTakeDate <= FinishDate));
        }
    }
}
