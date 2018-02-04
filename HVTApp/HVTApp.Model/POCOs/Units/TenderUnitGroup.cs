using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class TenderUnitGroup : BaseEntity
    {
        private TenderUnit TenderUnit => TenderUnits.FirstOrDefault();

        public virtual Product Product => TenderUnit?.Product;
        public virtual Facility Facility => TenderUnit?.Facility;
        public double Cost => TenderUnit.Cost;
        public int Amount => TenderUnits.Count;

        public virtual List<TenderUnit> TenderUnits { get; set; } = new List<TenderUnit>();
    }
}