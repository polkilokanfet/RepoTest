using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Tender : BaseEntity
    {
        public TenderType Type { get; set; }
        public virtual Project Project { get; set; }
        public virtual Cost Sum { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public DateTime? DateNotice { get; set; }
        public virtual List<Company> Participants { get; set; } = new List<Company>(); //участники
        public virtual Company Winner { get; set; }
        public virtual List<TenderUnit> TenderUnits { get; set; } = new List<TenderUnit>();
        public virtual List<Offer> Offers { get; set; } = new List<Offer>();
    }

    public class TenderType : BaseEntity
    {
        public string Name { get; set; }
        public TenderTypeEnum Type { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum TenderTypeEnum
    {
        ToProject,
        ToWork,
        ToSupply,
        ToWorkAndSupply
    }

}