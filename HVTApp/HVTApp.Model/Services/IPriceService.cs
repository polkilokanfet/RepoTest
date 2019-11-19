using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Structures;

namespace HVTApp.Model.Services
{
    /// <summary>
    /// Получение прайса.
    /// </summary>
    public interface IPriceService
    {
        /// <summary>
        /// Возвращает прайс на оборудование.
        /// </summary>
        /// <param name="product">Целевой продукт.</param>
        /// <param name="date">Дата прайса.</param>
        /// <param name="actualTerm">Срок актуальности прайса.</param>
        /// <param name="errors">Словарь возвращенных ошибок.</param>
        /// <returns></returns>
        double GetPrice(Product product, DateTime date, int actualTerm, PriceErrors errors = null);
        double GetPrice(ProductBlock block, DateTime date, int actualTerm, PriceErrors errors = null);
        PriceStructure GetPriceStructure(Product product, double amount, DateTime targetPriceDate, int priceTerm);
        PriceStructures GetPriceStructures(IUnit unit, DateTime targetPriceDate, int priceTerm);

        /// <summary>
        /// Поиск аналога для блока.
        /// </summary>
        /// <param name="blockTarget">Id целевого блока.</param>
        /// <returns></returns>
        ProductBlock GetAnalogWithPrice(ProductBlock blockTarget);

    }
}
