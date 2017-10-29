using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(DbContext context) : base(context)
        {
        }
    }
}
