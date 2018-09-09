using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <param name="errors">Словарь возвращенных ошибок.</param>
        /// <returns></returns>
        Task<double> GetPrice(Product product, DateTime date, int actualTerm, PriceErrors errors = null);
        Task<double> GetPrice(ProductBlock block, DateTime date, int actualTerm, PriceErrors errors = null);
    }
}
