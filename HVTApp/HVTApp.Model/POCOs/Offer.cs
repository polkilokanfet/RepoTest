using System;
using System.Collections.Generic;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("ТКП")]
    [DesignationPlural("Предложения")]
    public class Offer : Document
    {
        [Designation("Проект")]
        public virtual Project Project { get; set; }

        [Designation("Срок действия")]
        public DateTime ValidityDate { get; set; } // Дата до которой ТКП действительно.

        [Designation("НДС")]
        public double Vat { get; set; }

        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();
    }
}