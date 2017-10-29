using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class FacilityRepository : BaseRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(DbContext context) : base(context)
        {
        }
    }
}
