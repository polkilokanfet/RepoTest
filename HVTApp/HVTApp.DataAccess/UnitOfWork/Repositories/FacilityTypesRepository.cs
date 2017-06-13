using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class FacilityTypesRepository : BaseRepository<FacilityType, FacilityTypeWrapper>, IFacilityTypesRepository
    {
        public FacilityTypesRepository(DbContext context, Dictionary<IBaseEntity, IWrapper<IBaseEntity>> wrappersRepository) : base(context, wrappersRepository)
        {
        }
    }
}