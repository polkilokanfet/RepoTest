using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookup
    {
        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            OfferUnits = unitOfWork.GetRepository<OfferUnit>().Find(x => Equals(this.Entity, x.Offer)).Select(x => new OfferUnitLookup(x)).ToList();
            foreach (var offerUnitLookup in OfferUnits)
                await offerUnitLookup.LoadOther(unitOfWork);
        }

        public List<OfferUnitLookup> OfferUnits { get; private set; }

        [Designation("Компания")]
        public CompanyLookup Company => new CompanyLookup(Entity.RecipientEmployee.Company);

        public double? Sum => OfferUnits?.Sum(x => x.Cost);
    }
}