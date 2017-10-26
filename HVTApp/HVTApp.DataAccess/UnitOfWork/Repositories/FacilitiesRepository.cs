using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class FacilitiesRepository : BaseRepository<Facility>, IFacilitiesRepository
    {
        public FacilitiesRepository(DbContext context) : base(context)
        {
        }
    }
}