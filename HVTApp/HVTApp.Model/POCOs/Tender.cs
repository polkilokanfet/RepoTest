using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Tender : BaseEntity
    {
        public virtual Project Project { get; set; }
        public virtual List<TenderType> Types { get; set; } = new List<TenderType>();
        public DateTime DateOpen { get; set; }
        public DateTime DateClose { get; set; }
        public DateTime? DateNotice { get; set; }
        public virtual List<Company> Participants { get; set; } = new List<Company>(); //участники
        public virtual Company Winner { get; set; }

        public override string ToString()
        {
            return $"Tender {Types} of {Project}";
        }
    }

    public partial class TenderType : BaseEntity
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
        ToSupply,
        ToWork
    }

}