using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("�����������")]
    [DesignationPlural("�����������")]
    public partial class Offer : Document, IVatContainer
    {
        [Designation("������"), Required, OrderStatus(5)]
        public virtual Project Project { get; set; }

        [Designation("���� ��������"), Required, OrderStatus(4)]
        public DateTime ValidityDate { get; set; }

        [Designation("���"), OrderStatus(1)]
        public double Vat { get; set; } = GlobalAppProperties.Actual.Vat;

        [NotForDetailsView, NotForListView, NotForWrapper]
        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();

        public override string ToString()
        {
            return $"��� �{RegNumber} �� {Date.ToShortDateString()} �� ������� \"{Project.Name}\" ��� {RecipientEmployee?.Company}";
        }
    }
}