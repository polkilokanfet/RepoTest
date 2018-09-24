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

        public List<SalesUnitLookup> SalesUnits { get; } = new List<SalesUnitLookup>();
        public List<TenderLookup> Tenders { get; } = new List<TenderLookup>();
        public List<OfferLookup> Offers { get; } = new List<OfferLookup>();

        [Designation("Сумма проекта"), OrderStatus(7)]
        public double Sum => SalesUnits.Sum(x => x.Cost);

        [Designation("Дата поставки"), OrderStatus(6)]
        public DateTime RealizationDate => SalesUnits.Any() ? SalesUnits.Select(x => x.DeliveryDateExpected).Min() : DateTime.Today.AddMonths(6);

        [Designation("Тендер"), OrderStatus(5)]
        public DateTime? TenderDate
        {
            get
            {
                if (!Tenders.Any()) return null;
                var supply = Tenders.Where(x => x.Entity.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).ToList();
                return !supply.Any() ? null : supply.OrderBy(x => x.DateClose).Last()?.DateClose;
            }
        }

        [Designation("Объекты"), OrderStatus(10)]
        public List<FacilityLookup> Facilities => SalesUnits?.Select(x => x.Facility).Distinct(new FacilityComparer()).ToList();


        [Designation("Подрядчик"), OrderStatus(4)]
        public CompanyLookup Builder
        {
            get
            {
                if (Tenders.Any())
                {
                    var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork)).OrderBy(x => x.DateClose);
                    return tenders.LastOrDefault()?.Winner;
                }
                return null;
            }
        }

        [Designation("Проектировщик"), OrderStatus(3)]
        public CompanyLookup ProjectMaker
        {
            get
            {
                if (Tenders.Any())
                {
                    var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToProject)).OrderBy(x => x.DateClose);
                    return tenders.LastOrDefault()?.Winner;
                }
                return null;
            }
        }

        [Designation("Поставщик"), OrderStatus(2)]
        public CompanyLookup Sypplier
        {
            get
            {
                if (Tenders.Any())
                {
                    var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToProject)).OrderBy(x => x.DateClose);
                    return tenders.LastOrDefault()?.Winner;
                }
                return null;
            }
        }

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
