using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Курс обмена валют.
    /// </summary>
    public partial class CurrencyExchangeRate : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual Currency FirstCurrency { get; set; }
        public double FirstCurrencyValue { get; set; }
        public virtual Currency SecondCurrency { get; set; }
        public double SecondCurrencyValue { get; set; }
    }
}