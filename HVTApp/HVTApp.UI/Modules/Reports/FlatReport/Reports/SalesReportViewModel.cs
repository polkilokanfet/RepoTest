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
        private readonly List<PriceCalculation> _priceCalculations;

        public List<SalesReportUnit> Units { get; }

        public SalesReportViewModel(IUnityContainer container, IUnitOfWork unitOfWork, List<SalesUnit> salesUnits) : base(container)
        {
            UnitOfWork = unitOfWork;
            var tenders = UnitOfWork.Repository<Tender>().GetAll();
            var countryUnions = UnitOfWork.Repository<CountryUnion>().GetAll();
            _priceCalculations = UnitOfWork.Repository<PriceCalculation>().GetAll();

            //проставляем количество родительских юнитов включенного оборудования
            var productsIncluded = salesUnits.SelectMany(salesUnit => salesUnit.ProductsIncluded).ToList();
            foreach (var productIncluded in productsIncluded)
            {
                productIncluded.ParentsCount = salesUnits.Count(salesUnit => salesUnit.ProductsIncluded.Contains(productIncluded));
            }

            var groups = salesUnits
                .OrderBy(salesUnit => salesUnit.OrderInTakeDate)
                .GroupBy(salesUnit => salesUnit, new SalesUnitsReportComparer())
                .ToList();

            var salesReportUnits = groups
                .Select(x => new SalesReportUnit(x, tenders.Where(tender => Equals(x.Key.Project, tender.Project)), countryUnions, ActualPriceCalculationItem(x.First())))
                .ToList();

            Units = new List<SalesReportUnit>(salesReportUnits);
        }

        /// <summary>
        /// Актуальный расчет ПЗ
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <returns></returns>
        private PriceCalculationItem ActualPriceCalculationItem(SalesUnit salesUnit)
        {
            var calculations = _priceCalculations
                .Where(priceCalculation => priceCalculation.PriceCalculationItems.ContainsSalesUnit(salesUnit))
                .ToList();

            //позже всех запущено на расчет
            var result = calculations
                .Where(priceCalculation => priceCalculation.TaskOpenMoment.HasValue)
                .OrderByDescending(priceCalculation => priceCalculation.TaskOpenMoment.Value)
                .FirstOrDefault(priceCalculation => priceCalculation.PriceCalculationItems.ContainsSalesUnit(salesUnit))
                ?.PriceCalculationItems.Single(priceCalculationItem => priceCalculationItem.SalesUnits.Contains(salesUnit));
            if (result != null) return result;

            //позже всех дата ОИТ
            result = calculations
                .SelectMany(priceCalculation => priceCalculation.PriceCalculationItems)
                .OrderByDescending(priceCalculationItem => priceCalculationItem.OrderInTakeDate)
                .FirstOrDefault(priceCalculationItem => priceCalculationItem.SalesUnits.Contains(salesUnit));

            return result;
        }

    }
}