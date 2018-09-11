using System;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("���")]
    [DesignationPlural("���")]
    public partial class Offer : Document
    {
        [Designation("������")]
        public virtual Project Project { get; set; }

        [Designation("���� ��������")]
        public DateTime ValidityDate { get; set; }

        [Designation("���")]
        public double Vat { get; set; }
    }
}