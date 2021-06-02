namespace HVTApp.Model.Price
{
    public class PriceOfUnit : PriceBase
    {
        /// <summary>
        /// �� �� �������
        /// </summary>
        protected double? UnitPrice { get; set; }

        public override double SumPriceTotal => KUp * UnitPrice * Amount ?? 0;

        public override bool ContainsAnyAnalog { get; } = false;

    }
}