using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferUnitRepository : BaseRepository<OfferUnit>, IOfferUnitRepository
    {
        public OfferUnitRepository(DbContext context) : base(context)
        {
        }
    }
}
