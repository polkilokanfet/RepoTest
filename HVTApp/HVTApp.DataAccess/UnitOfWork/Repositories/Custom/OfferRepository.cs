using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferRepository
    {
        protected override IQueryable<Offer> GetQuery()
        {
            return Context.Set<Offer>().AsQueryable()
                .Include(offer => offer.Project.Manager)
                .Include(offer => offer.SenderEmployee.Company)
                .Include(offer => offer.RecipientEmployee.Company);
        }

        public IEnumerable<Offer> GetAllOfCurrentUser()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);

            return this.GetQuery()
                .Where(offer => offer.Project.Manager.Id == GlobalAppProperties.User.Id)
                .ToList();
        }
    }
}