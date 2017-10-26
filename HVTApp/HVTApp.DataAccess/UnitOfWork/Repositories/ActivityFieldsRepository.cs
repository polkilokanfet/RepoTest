using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ActivityFieldsRepository : BaseRepository<ActivityField>, IActivityFieldsRepository
    {
        public ActivityFieldsRepository(DbContext context) : base(context)
        {
        }
    }
}