using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Предложение")]
    [DesignationPlural("Предложения")]
    public partial class Offer : Document, IVatContainer
    {
        [Designation("Проект"), Required, OrderStatus(5)]
        public virtual Project Project { get; set; }

        [Designation("Срок действия"), Required, OrderStatus(4)]
        public DateTime ValidityDate { get; set; }

        [Designation("НДС"), OrderStatus(1)]
        public double Vat { get; set; } = GlobalAppProperties.Actual.Vat;

        [NotForDetailsView, NotForListView, NotForWrapper]
        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();

        public override string ToString()
        {
            return $"ТКП №{RegNumber} от {Date.ToShortDateString()} по проекту \"{Project.Name}\" для {RecipientEmployee?.Company}";
        }
    }
}