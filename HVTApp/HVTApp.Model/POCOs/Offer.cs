using System;
using System.Collections.Generic;

namespace HVTApp.Model.POCOs
{
    public partial class Offer : Document
    {
        public virtual Project Project { get; set; }
        public DateTime ValidityDate { get; set; } // ���� �� ������� ��� �������������.
        public double Vat { get; set; }
        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();
    }
}