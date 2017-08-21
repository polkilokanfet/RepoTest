using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ParametersGroupsRepository : BaseRepository<ParameterGroup, ParameterGroupWrapper>, IParametersGroupsRepository {
        public ParametersGroupsRepository(DbContext context, IGetWrapper wrappersGetter) : base(context, wrappersGetter)
        {
        }
    }
}