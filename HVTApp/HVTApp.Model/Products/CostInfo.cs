using System;

namespace HVTApp.Model
{
    public class CostInfo : BaseEntity
    {
        public double Cost { get; set; }
        public double CostPrice { get; set; } // Себестоимость.
        public double Vat { get; set; }
    }
}