using System;

namespace HVTApp.UI.Wrapper
{
    public partial class SalesUnitWrapper : IUnit
    {
        public double Price { get; set; }
        public event Action PriceChanged;
    }
}