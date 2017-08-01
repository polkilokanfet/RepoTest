using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class RequiredDependentEquipmentsParametersRepository : BaseRepository<RequiredDependentEquipmentsParameters, RequiredDependentEquipmentsParametersWrapper>, IRequiredDependentEquipmentsParametersRepository
    {
        public RequiredDependentEquipmentsParametersRepository(DbContext context, IGetWrapper getWrapper) : base(context, getWrapper)
        {
        }
    }
}