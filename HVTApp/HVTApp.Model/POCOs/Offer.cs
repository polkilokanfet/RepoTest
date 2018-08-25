using System;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("ТКП")]
    [DesignationPlural("ТКП")]
    public class Offer : Document
    {
        [Designation("Проект")]
        public virtual Project Project { get; set; }

        [Designation("Срок действия")]
        public DateTime ValidityDate { get; set; }

        [Designation("НДС")]
        public double Vat { get; set; }
    }
}