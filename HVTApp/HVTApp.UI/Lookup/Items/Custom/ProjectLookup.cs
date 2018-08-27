using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookup
    {
        public ProjectLookup(Project project, IEnumerable<SalesUnit> salesUnits, IEnumerable<Tender> tenders,
                             IEnumerable<Offer> offers) : base(project)
        {
            SalesUnits = salesUnits.Select(x => new SalesUnitLookup(x)).ToList();
            Tenders = tenders.Select(x => new TenderLookup(x)).ToList();
            Offers = offers.Select(x => new OfferLookup(x)).ToList();
        }

        public List<SalesUnitLookup> SalesUnits { get; set; }
        public List<TenderLookup> Tenders { get; set; }
        public List<OfferLookup> Offers { get; set; } 

        [Designation("Сумма проекта")]
        public double Sum => SalesUnits.Sum(x => x.Cost);

        [Designation("Дата поставки")]
        public DateTime RealizationDate
        {
            get
            {
                return SalesUnits.Any() ? SalesUnits.Select(x => x.DeliveryDateExpected).Min() : DateTime.Today.AddMonths(6);
            }
        }

        [Designation("Тендер")]
        public DateTime? TenderDate
            => Tenders.SingleOrDefault(x => x.Entity.Types.Any(tp => tp.Type == TenderTypeEnum.ToSupply))?.DateClose;

        public override int CompareTo(object obj)
        {
            return RealizationDate.CompareTo(((ProjectLookup)obj).RealizationDate);
        }
    }
}
