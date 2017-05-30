using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class FacilitiesRepository : BaseRepository<Facility, FacilityWrapper>, IFacilitiesRepository
    {
        public FacilitiesRepository(DbContext context, Dictionary<IBaseEntity, object> wrappersRepository) : base(context, wrappersRepository)
        {
        }
    }
}