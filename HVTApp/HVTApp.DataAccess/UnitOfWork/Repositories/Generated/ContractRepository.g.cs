using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
        public ContractRepository(DbContext context) : base(context)
        {
        }
    }
}
