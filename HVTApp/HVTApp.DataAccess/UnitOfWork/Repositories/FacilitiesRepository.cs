using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;

namespace HVTApp.DataAccess
{
    public class FacilitiesRepository : BaseRepository<Facility>, IFacilitiesRepository
    {
        public FacilitiesRepository(DbContext context) : base(context)
        {
        }
    }
}