using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    /// <summary>
    /// Получение прайса.
    /// </summary>
    public interface IPriceService
    {
        /// <summary>
        /// Получить прайс
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="targetDate">Целевая дата</param>
        /// <returns></returns>
        Price GetPrice(IUnit unit, DateTime targetDate);

        /// <summary>
        /// Получить прайс по калькуляциям
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        double? GetPriceByCalculations(IUnit unit);

        PriceCalculationItem GetPriceCalculationItem(IUnit unit);

        /// <summary>
        /// Поиск аналога для блока.
        /// </summary>
        /// <param name="blockTarget">Id целевого блока.</param>
        /// <returns></returns>
        ProductBlock GetAnalogWithPrice(ProductBlock blockTarget);
    }
}
