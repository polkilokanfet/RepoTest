using System.Data.Entity;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class ActivityFieldsRepository : BaseRepository<ActivityField>, IActivityFieldsRepository
    {
        public ActivityFieldsRepository(DbContext context) : base(context)
        {
        }
    }
}