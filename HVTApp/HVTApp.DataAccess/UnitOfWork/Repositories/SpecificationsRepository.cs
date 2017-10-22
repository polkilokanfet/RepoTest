using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;

namespace HVTApp.DataAccess
{
    public class SpecificationsRepository : BaseRepository<Specification>, ISpecificationsRepository
    {
        public SpecificationsRepository(DbContext context) : base(context)
        {
        }
    }
}