using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Offer : Document
    {
        public virtual Tender Tender { get; set; }
        public DateTime ValidityDate { get; set; } // ���� �� ������� ��� �������������.
        public double Vat { get; set; }
    }
}