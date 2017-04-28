using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Сумма на какую-либо дату
    /// </summary>
    public class SumOnDate : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Sum { get; set; }
    }

    public class SumAndVat : BaseEntity
    {
        public virtual Currency Currency { get; set; }
        public double Sum { get; set; }
        public double Vat { get; set; }
    }

    /// <summary>
    /// Валюта
    /// </summary>
    public class Currency : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }

    /// <summary>
    /// Курс обмена валют.
    /// </summary>
    public class ExchangeCurrencyRate : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual Currency FirstCurrency { get; set; }
        public double FirstCurrencyValue { get; set; }
        public virtual Currency SecondCurrency { get; set; }
        public double SecondCurrencyValue { get; set; }
    }
}