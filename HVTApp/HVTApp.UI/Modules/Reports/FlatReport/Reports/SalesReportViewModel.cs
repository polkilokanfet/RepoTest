using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.SalesReport;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Reports.FlatReport.Reports
{
    public class SalesReportViewModel : ViewModelBaseCanExportToExcel
    {
        public List<SalesReportUnit> Units { get; }

        public SalesReportViewModel(IUnityContainer container, IUnitOfWork unitOfWork, List<SalesUnit> salesUnits) : base(container)
        {
            UnitOfWork = unitOfWork;

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(x => x.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(x => x.ProductsIncluded.Contains(productIncluded));
            }

            var groups = salesUnits.OrderBy(x => x.OrderInTakeDate).GroupBy(x => x, new SalesUnitsReportComparer());

            var tenders = UnitOfWork.Repository<Tender>().GetAll();
            var countryUnions = UnitOfWork.Repository<CountryUnion>().GetAll();

            var salesReportUnits = groups
                .Select(x => new SalesReportUnit(x, tenders.Where(t => Equals(x.Key.Project, t.Project)), countryUnions, x.First().ActualPriceCalculationItem(UnitOfWork)))
                .ToList();

            Units = new List<SalesReportUnit>(salesReportUnits);
        }
    }
}