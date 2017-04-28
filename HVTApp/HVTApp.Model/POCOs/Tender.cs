using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Tender : BaseEntity
    {
        public TenderType Type { get; set; }
        public virtual Project Project { get; set; }
        public double Sum { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public DateTime? DateNotice { get; set; }
        public virtual List<Company> Participants { get; set; } //участники
        public virtual Company Winner { get; set; }
        public virtual List<TenderUnit> TenderUnits { get; set; }
        public virtual List<Offer> Offers { get; set; }
    }

    public enum TenderType
    {
        ToProject,
        ToWork,
        ToSupply,
        ToWorkAndSupply
    }

    public class TenderUnit : BaseEntity
    {
        public virtual Tender Tender { get; set; }
        public virtual SalesUnit ParentSalesUnit { get; set; }
        public virtual SalesUnit ChildSalesUnit { get; set; }
    }

}