using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI
{
    public static class Ext
    {
        public static PriceCalculationItem ActualPriceCalculationItem(this SalesUnit unit, IUnitOfWork unitOfWork)
        {
            var salesUnit = unitOfWork.Repository<SalesUnit>().GetById(unit.Id);
            var calculations = unitOfWork.Repository<PriceCalculation>().Find(x => x.PriceCalculationItems.ContainsSalesUnit(salesUnit));

            //позже всех запущено на расчет
            var result = calculations
                .Where(x => x.TaskOpenMoment.HasValue)
                .OrderByDescending(x => x.TaskOpenMoment.Value)
                .FirstOrDefault(x => x.PriceCalculationItems.ContainsSalesUnit(salesUnit))
                ?.PriceCalculationItems.Single(x => x.SalesUnits.Contains(salesUnit));
            if (result != null) return result;

            //позже всех дата ОИТ
            result = calculations
                .SelectMany(x => x.PriceCalculationItems)
                .OrderByDescending(x => x.OrderInTakeDate)
                .FirstOrDefault(x => x.SalesUnits.Contains(salesUnit));

            return result;
        }

        public static bool ContainsSalesUnit(this IEnumerable<PriceCalculationItem> items, SalesUnit salesUnit)
        {
            return items.SelectMany(x => x.SalesUnits).ContainsById(salesUnit);
        }

        public static string Voltage(this Product product)
        {
            return product.ProductBlock.Parameters.FirstOrDefault(x => Equals(x.ParameterGroup, GlobalAppProperties.Actual.VoltageGroup))?.Value;
        }
    }
}