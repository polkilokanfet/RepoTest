using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.SalesReport;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.CommonInfo
{
    public class CommonInfoViewModel : LoadableExportableExpandCollapseViewModel
    {
        public ObservableCollection<SalesReportUnit> Units { get; } = new ObservableCollection<SalesReportUnit>();

        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-1);
        public DateTime FinishDate { get; set; } = DateTime.Today.AddMonths(1);

        public CommonInfoViewModel(IUnityContainer container) : base(container)
        {
        }

        private IEnumerable<SalesReportUnit> _salesReportUnits;
        protected override void GetData()
        {
            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? UnitOfWork.Repository<SalesUnit>().Find(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate && !x.IsLoosen && x.Project.Manager.IsAppCurrentUser())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.OrderInTakeDate >= StartDate && x.OrderInTakeDate <= FinishDate && !x.IsLoosen);

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(x => x.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(x => x.ProductsIncluded.Contains(productIncluded));
            }

            var groups = salesUnits.OrderBy(x => x.OrderInTakeDate).GroupBy(x => x, new SalesUnitsReportComparer());

            var tenders = UnitOfWork.Repository<Tender>().Find(x => true);
            var countryUnions = UnitOfWork.Repository<CountryUnion>().Find(x => true);

            _salesReportUnits = groups.Select(x => new SalesReportUnit(x, tenders.Where(t => Equals(x.Key.Project, t.Project)), countryUnions, x.First().ActualPriceCalculationItem(UnitOfWork)));
        }

        protected override void AfterGetData()
        {
            Units.Clear();
            Units.AddRange(_salesReportUnits);
        }
    }
}