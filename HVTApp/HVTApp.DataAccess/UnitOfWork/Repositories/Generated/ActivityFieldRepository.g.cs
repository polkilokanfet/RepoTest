using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ActivityFieldRepository : BaseRepository<ActivityField>, IActivityFieldRepository
    {
        public ActivityFieldRepository(DbContext context) : base(context)
        {
        }
    }
}
