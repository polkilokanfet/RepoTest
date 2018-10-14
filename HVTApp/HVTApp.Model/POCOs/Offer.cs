using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Предложение")]
    [DesignationPlural("Предложения")]
    public partial class Offer : Document
    {
        [Designation("Проект"), Required, OrderStatus(5)]
        public virtual Project Project { get; set; }

        [Designation("Срок действия"), Required, OrderStatus(4)]
        public DateTime ValidityDate { get; set; }

        [Designation("НДС"), OrderStatus(1)]
        public double Vat { get; set; } = GlobalAppProperties.Actual.Vat;
    }
}