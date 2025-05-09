﻿using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    /// <summary>
    /// Получение прайса.
    /// </summary>
    public interface IPriceService : IProfitabilityService
    {
        /// <summary>
        /// Получить прайс
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="targetDate">Целевая дата</param>
        /// <param name="checkCalculations">Проверять ли калькуляции цен?</param>
        /// <returns></returns>
        Price.Price GetPrice(IUnit unit, DateTime targetDate, bool checkCalculations);

        /// <summary>
        /// Получить прайс
        /// </summary>
        /// <param name="salesUnits"></param>
        /// <param name="targetDate">Целевая дата</param>
        /// <param name="checkCalculations">Проверять ли калькуляции цен?</param>
        /// <returns></returns>
        Price.Price GetPrice(IEnumerable<SalesUnit> salesUnits, DateTime targetDate, bool checkCalculations);

        PriceCalculationItem GetPriceCalculationItem(IUnit unit);

        /// <summary>
        /// Поиск аналога для блока.
        /// </summary>
        /// <param name="blockTarget">Id целевого блока.</param>
        /// <returns></returns>
        ProductBlock GetAnalogWithPrice(ProductBlock blockTarget);
    }

    /// <summary>
    /// Получение прайса.
    /// </summary>
    public interface IProfitabilityService
    {
        /// <summary>
        /// Количество нормо-часов на изготовление всего юнита.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        double? GetLaborHoursAmount(IUnit unit);

        /// <summary>
        /// Количество нормо-часов на изготовление всего продукта.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        double? GetLaborHoursAmount(Product product);

        /// <summary>
        /// Количество нормо-часов на изготовление блока продукта.
        /// </summary>
        /// <param name="productBlock"></param>
        /// <returns></returns>
        double? GetLaborHoursAmount(ProductBlock productBlock);

        /// <summary>
        /// Фонд оплаты труда
        /// </summary>
        /// <param name="product"></param>
        /// <param name="targetDate"></param>
        /// <returns></returns>
        double? GetWageFund(Product product, DateTime targetDate);

        /// <summary>
        /// Фонд оплаты труда
        /// </summary>
        /// <param name="productBlock"></param>
        /// <param name="targetDate"></param>
        /// <returns></returns>
        double? GetWageFund(ProductBlock productBlock, DateTime targetDate);

        /// <summary>
        /// Стоимость нормо-часа
        /// </summary>
        /// <param name="targetDate"></param>
        /// <returns></returns>
        double GetLaborHoursCost(DateTime targetDate);
    }

}
