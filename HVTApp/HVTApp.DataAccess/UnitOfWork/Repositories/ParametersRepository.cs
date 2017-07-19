using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ParametersRepository : BaseRepository<Parameter, ParameterWrapper>, IParametersRepository
    {
        public ParametersRepository(DbContext context, IGetWrapper getWrapper) : base(context, getWrapper)
        {
        }
    }
}