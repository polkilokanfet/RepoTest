using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class OffersRepository : BaseRepository<Offer>, IOffersRepository
    {
        public OffersRepository(DbContext context) : base(context)
        {
        }
    }
}