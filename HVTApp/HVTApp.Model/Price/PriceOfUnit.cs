namespace HVTApp.Model.Price
{
    public class PriceOfUnit : PriceBase
    {
        /// <summary>
        /// ПЗ на единицу
        /// </summary>
        protected double? UnitPrice { get; set; }

        public override double SumPriceTotal => KUp * UnitPrice * Amount ?? 0;

        public override bool ContainsAnyAnalog { get; } = false;

    }
}