using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Курс обмена валют.
    /// </summary>
    [Designation("Курс обмена валют")]
    [DesignationPlural("Курсы обмена валют")]
    public class CurrencyExchangeRate : BaseEntity
    {
        [Designation("Дата")]
        public DateTime Date { get; set; }

        [Designation("Валюта 1")]
        public virtual Currency FirstCurrency { get; set; }

        [Designation("Валюта 2")]
        public virtual Currency SecondCurrency { get; set; }

        [Designation("Валюта 1 / Валюта 2")]
        public double ExchangeRate { get; set; }
    }
}