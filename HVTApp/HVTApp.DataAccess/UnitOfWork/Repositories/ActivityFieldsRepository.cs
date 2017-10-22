using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;

namespace HVTApp.DataAccess
{
    public class ActivityFieldsRepository : BaseRepository<ActivityField>, IActivityFieldsRepository
    {
        public ActivityFieldsRepository(DbContext context) : base(context)
        {
        }
    }
}