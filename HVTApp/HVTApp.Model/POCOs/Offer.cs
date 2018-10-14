using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("�����������")]
    [DesignationPlural("�����������")]
    public partial class Offer : Document
    {
        [Designation("������"), Required, OrderStatus(5)]
        public virtual Project Project { get; set; }

        [Designation("���� ��������"), Required, OrderStatus(4)]
        public DateTime ValidityDate { get; set; }

        [Designation("���"), OrderStatus(1)]
        public double Vat { get; set; } = GlobalAppProperties.Actual.Vat;
    }
}