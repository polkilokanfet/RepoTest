using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferRepository
    {
        protected override IQueryable<Offer> GetQuary()
        {
            return Context.Set<Offer>().AsQueryable()
                .Include(x => x.Project)
                .Include(x => x.SenderEmployee.Company)
                .Include(x => x.RecipientEmployee.Company);
        }
    }
}