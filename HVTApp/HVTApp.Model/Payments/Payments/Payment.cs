using System;

namespace HVTApp.Model
{
    public class Payment : BaseEntity
    {
        public DateTime Date { get; set; }
        public double Summ { get; set; }
        public string Comment { get; set; }
    }
}