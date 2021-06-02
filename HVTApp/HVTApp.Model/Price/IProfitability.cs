namespace HVTApp.Model.Price
{
    public interface IProfitability
    {
        /// <summary>
        ///  оличество нормо-часов на изготовление всего продукта
        /// </summary>
        double? LaborHours { get; }

        /// <summary>
        ///  оличество нормо-часов на изготовление всего продукта * количество
        /// </summary>
        double? LaborHoursOnAmount { get; }

        /// <summary>
        /// ‘онд оплаты труда
        /// </summary>
        double? WageFund { get; }

        /// <summary>
        /// ‘онд оплаты труда * количество
        /// </summary>
        double? WageFundOnAmount { get; }
    }
}