using System;
using System.Linq;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookup
    {
        [Designation("Сумма проекта")]
        public double Sum => Entity.SalesUnits.Sum(x => x.Cost);

        [Designation("Дата поставки")]
        public DateTime RealizationDate => Entity.SalesUnits.Select(x => x.DeliveryDateExpected).Min();

        public override int CompareTo(object obj)
        {
            return RealizationDate.CompareTo(((ProjectLookup)obj).RealizationDate);
        }
    }
}
