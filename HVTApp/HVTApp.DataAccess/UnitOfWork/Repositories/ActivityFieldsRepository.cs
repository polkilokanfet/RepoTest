using System.Data.Entity;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public class ActivityFieldsRepository : BaseRepository<ActivityField, ActivityFieldWrapper>, IActivityFieldsRepository
    {
        public ActivityFieldsRepository(DbContext context) : base(context)
        {
        }
    }
}