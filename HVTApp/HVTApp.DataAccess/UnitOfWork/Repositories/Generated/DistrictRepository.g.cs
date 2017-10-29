using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(DbContext context) : base(context)
        {
        }
    }
}
