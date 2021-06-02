using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Price
{
    public interface IPrice : IProfitability
    {
        /// <summary>
        /// Где-либо в прайсе есть аналог
        /// </summary>
        bool ContainsAnyAnalog { get; }

        /// <summary>
        /// По какому аналогу взят прайс
        /// </summary>
        ProductBlock Analog { get; }

        string Comment { get; }

        string Name { get; }
        double Amount { get; }

        /// <summary>
        /// ПЗ + ФЗ
        /// </summary>
        double SumTotal { get; }

        /// <summary>
        /// Себестоимость с учетом коэффициента упаковки
        /// </summary>
        double SumPriceTotal { get; }

        /// <summary>
        /// Стоимость блоков с фиксированной ценой
        /// </summary>
        double SumFixedTotal { get; }

        List<IPrice> Prices { get; }
    }
}