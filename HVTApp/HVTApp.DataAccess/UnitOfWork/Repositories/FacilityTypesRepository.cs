using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class FacilityTypesRepository : BaseRepository<FacilityType>, IFacilityTypesRepository
    {
        public FacilityTypesRepository(DbContext context) : base(context)
        {
        }
    }
}