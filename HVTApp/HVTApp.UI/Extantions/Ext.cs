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
        /// <summary>
        /// Актуальный расчет ПЗ
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
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

        public static Company GetWinner(this IEnumerable<Tender> tenders, TenderTypeEnum tenderType)
        {
            if (tenders == null || !tenders.Any())
                return null;

            return tenders
                .Where(x => x.Types.Select(t => t.Type).Contains(tenderType))
                .OrderBy(x => x.DateClose)
                .LastOrDefault()?.Winner;
        }

        /// <summary>
        /// Сворачивает позиции с "1,2,3" к "1-3"
        /// </summary>
        /// <param name="positionsEnumerable"></param>
        /// <returns></returns>
        public static string GetOrderPositions(this IEnumerable<string> positionsEnumerable)
        {
            var positions = new List<int>();
            foreach (var position in positionsEnumerable)
            {
                int i;
                var result = int.TryParse(position, out i);
                if (result)
                    positions.Add(i);
            }

            positions.Sort();

            if (positions.Count > 1 && positions.Count == (positions.Last() - positions.First() + 1))
                return $"{positions.First()}-{positions.Last()}";

            return positions.Select(x => x.ToString()).ToStringEnum();
        }

    }
}