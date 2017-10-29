using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferRepository : BaseRepository<Offer>, IOfferRepository
    {
        public OfferRepository(DbContext context) : base(context)
        {
        }
    }
}
