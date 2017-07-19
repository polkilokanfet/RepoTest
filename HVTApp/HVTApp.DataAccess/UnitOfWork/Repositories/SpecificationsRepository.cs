using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class SpecificationsRepository : BaseRepository<Specification, SpecificationWrapper>, ISpecificationsRepository
    {
        public SpecificationsRepository(DbContext context, IGetWrapper getWrapper) : base(context, getWrapper)
        {
        }
    }
}