using System;
using System.Linq;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookup
    {
        [Designation("Сумма проекта")]
        public double Sum => Entity.SalesUnits.Sum(x => x.Cost);

        [Designation("Дата поставки")]
        public DateTime RealizationDate => Entity.SalesUnits.Select(x => x.DeliveryDateExpected).Min();

        [Designation("Тендер")]
        public DateTime? TenderDate
            => Entity.Tenders.SingleOrDefault(x => x.Types.Any(tp => tp.Type == TenderTypeEnum.ToSupply))?.DateClose;

        public override int CompareTo(object obj)
        {
            return RealizationDate.CompareTo(((ProjectLookup)obj).RealizationDate);
        }
    }
}
