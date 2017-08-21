using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class RequiredDependentProductssParametersRepository : BaseRepository<RequiredDependentProductsParameters, RequiredDependentProductsParametersWrapper>, IRequiredDependentProductssParametersRepository
    {
        public RequiredDependentProductssParametersRepository(DbContext context, IGetWrapper wrappersGetter) : base(context, wrappersGetter)
        {
        }
    }
}