using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class SumAndVat : BaseEntity
    {
        public double Sum { get; set; }
        public double Vat { get; set; }
    }
}