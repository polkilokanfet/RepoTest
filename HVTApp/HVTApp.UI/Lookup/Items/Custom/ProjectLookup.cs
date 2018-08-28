using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookup
    {
        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            SalesUnits = unitOfWork.GetRepository<SalesUnit>().Find(x => Equals(this.Entity, x.Project)).Select(x => new SalesUnitLookup(x)).ToList();
            foreach (var salesUnitLookup in SalesUnits)
                await salesUnitLookup.LoadOther(unitOfWork);

            Tenders = unitOfWork.GetRepository<Tender>().Find(x => Equals(this.Entity, x.Project)).Select(x => new TenderLookup(x)).ToList();
            foreach (var tenderLookup in Tenders)
                await tenderLookup.LoadOther(unitOfWork);

            Offers = unitOfWork.GetRepository<Offer>().Find(x => Equals(this.Entity, x.Project)).Select(x => new OfferLookup(x)).ToList();
            foreach (var offerLookup in Offers)
                await offerLookup.LoadOther(unitOfWork);
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
