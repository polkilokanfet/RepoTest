using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ActivityFieldsRepository : BaseRepository<ActivityField, ActivityFieldWrapper>, IActivityFieldsRepository
    {
        public ActivityFieldsRepository(DbContext context, Dictionary<IBaseEntity, object> wrappersRepository) : base(context, wrappersRepository)
        {
        }
    }
}