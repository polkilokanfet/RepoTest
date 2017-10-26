using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ContractsRepository : BaseRepository<Contract>, IContractsRepository {
        public ContractsRepository(DbContext context) : base(context)
        {
        }
    }
}