using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService
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
        /// <param name="errorDictionary">Словарь возвращенных ошибок.</param>
        /// <returns></returns>
        double GetPrice(Product product, DateTime date, int actualTerm, Dictionary<ProductBlock, string> errorDictionary = null);
        double GetPrice(ProductBlock block, DateTime date, int actualTerm, ref string errorMsg);
    }
}
