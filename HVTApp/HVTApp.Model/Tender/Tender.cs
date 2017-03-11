using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class Tender : BaseEntity
    {
        public TenderType Type { get; set; }
        public virtual Project Project { get; set; }
        public double Sum { get; set; }
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public virtual List<Company> Participants { get; set; }
        public virtual Company Winner { get; set; }
        public virtual List<TenderInfo> TenderUnits { get; set; }
    }
}