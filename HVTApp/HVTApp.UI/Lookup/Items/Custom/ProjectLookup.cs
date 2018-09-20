using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookup
    {
        public ProjectLookup(Project project, IEnumerable<SalesUnit> salesUnits, IEnumerable<Tender> tenders, IEnumerable<Offer> offers) : this(project)
        {
            SalesUnits = new List<SalesUnitLookup>(salesUnits.Select(x => new SalesUnitLookup(x)));
            Tenders = new List<TenderLookup>(tenders.Select(x => new TenderLookup(x)));
            Offers = new List<OfferLookup>(offers.Select(x => new OfferLookup(x)));
        }

        //[OrderStatus(OrderStatus.Low)]
        public List<SalesUnitLookup> SalesUnits { get; } = new List<SalesUnitLookup>();
        //[OrderStatus(OrderStatus.Low)]
        public List<TenderLookup> Tenders { get; } = new List<TenderLookup>();
        //[OrderStatus(OrderStatus.Low)]
        public List<OfferLookup> Offers { get; } = new List<OfferLookup>();

        [Designation("Сумма проекта")]
        public double Sum => SalesUnits.Sum(x => x.Cost);

        [Designation("Дата поставки")]
        public DateTime RealizationDate => SalesUnits.Any() ? SalesUnits.Select(x => x.DeliveryDateExpected).Min() : DateTime.Today.AddMonths(6);

        [Designation("Тендер")]
        public DateTime? TenderDate => Tenders.Where(x => x.Entity.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).OrderBy(x => x.DateClose).Last()?.DateClose;

        [Designation("Объекты"), OrderStatus(10)]
        public List<FacilityLookup> Facilities => SalesUnits?.Select(x => x.Facility).Distinct(new FacilityComparer()).ToList();

        public override int CompareTo(object obj)
        {
            return RealizationDate.CompareTo(((ProjectLookup)obj).RealizationDate);
        }

        internal class FacilityComparer : IEqualityComparer<FacilityLookup>
        {
            public bool Equals(FacilityLookup x, FacilityLookup y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(FacilityLookup obj)
            {
                return 0;
            }
        }
    }
}
