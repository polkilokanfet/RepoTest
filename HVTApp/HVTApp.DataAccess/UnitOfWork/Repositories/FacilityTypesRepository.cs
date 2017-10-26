using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class FacilityTypesRepository : BaseRepository<FacilityType>, IFacilityTypesRepository
    {
        public FacilityTypesRepository(DbContext context) : base(context)
        {
        }
    }
}