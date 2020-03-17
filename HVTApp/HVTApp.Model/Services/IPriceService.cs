using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Structures;

namespace HVTApp.Model.Services
{
    /// <summary>
    /// Получение прайса.
    /// </summary>
    public interface IPriceService
    {
        Price GetPrice(IUnit unit, DateTime targetDate);

        /// <summary>
        /// Получить прайс по калбкуляциям
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
