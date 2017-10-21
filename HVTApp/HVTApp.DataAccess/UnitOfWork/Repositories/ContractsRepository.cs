using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ContractsRepository : BaseRepository<Contract>, IContractsRepository {
        public ContractsRepository(DbContext context) : base(context)
        {
        }
    }
}