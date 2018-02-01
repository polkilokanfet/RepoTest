using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Сумма на какую-либо дату
    /// </summary>
    public partial class CostOnDate : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual double Cost { get; set; }
    }

    public partial class Cost : BaseEntity
    {
        public virtual Currency Currency { get; set; }
        public double Sum { get; set; }
    }

    /// <summary>
    /// Валюта
    /// </summary>
    public partial class Currency : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }

    /// <summary>
    /// Курс обмена валют.
    /// </summary>
    public partial class ExchangeCurrencyRate : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual Currency FirstCurrency { get; set; }
        public double FirstCurrencyValue { get; set; }
        public virtual Currency SecondCurrency { get; set; }
        public double SecondCurrencyValue { get; set; }
    }
}