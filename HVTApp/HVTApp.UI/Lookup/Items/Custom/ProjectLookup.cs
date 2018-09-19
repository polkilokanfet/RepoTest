using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookup
    {
        public ProjectLookup(Project project, IEnumerable<SalesUnit> salesUnits, IEnumerable<Tender> tenders) : this(project)
        {
            SalesUnits = new List<SalesUnitLookup>(salesUnits.Select(x => new SalesUnitLookup(x)));
            Tenders = new List<TenderLookup>(tenders.Select(x => new TenderLookup(x)));
        }

        //[OrderStatus(OrderStatus.Low)]
        public List<SalesUnitLookup> SalesUnits { get; private set; } = new List<SalesUnitLookup>();
        //[OrderStatus(OrderStatus.Low)]
        public List<TenderLookup> Tenders { get; private set; } = new List<TenderLookup>();
        //[OrderStatus(OrderStatus.Low)]
        public List<OfferLookup> Offers { get; private set; } = new List<OfferLookup>();
        //[OrderStatus(OrderStatus.Low)]
        public List<NoteLookup> Notes { get; private set; } = new List<NoteLookup>();

        [Designation("Сумма проекта")]
        public double Sum => SalesUnits.Sum(x => x.Cost);

        [Designation("Дата поставки")]
        public DateTime RealizationDate => SalesUnits.Any() ? SalesUnits.Select(x => x.DeliveryDateExpected).Min() : DateTime.Today.AddMonths(6);

        [Designation("Тендер")]
        public DateTime? TenderDate => Tenders.SingleOrDefault(x => x.Entity.Types.Any(tp => tp.Type == TenderTypeEnum.ToSupply))?.DateClose;

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
