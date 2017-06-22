using System;
using System.Collections.Generic;

namespace HVTApp.Model.POCOs
{
    public class Offer : Document
    {
        public virtual Project Project { get; set; }
        public virtual Tender Tender { get; set; }
        public DateTime ValidityDate { get; set; } // ���� �� ������� ��� �������������.
        public virtual List<ProductOfferUnit> OfferUnits { get; set; } = new List<ProductOfferUnit>();
    }

}