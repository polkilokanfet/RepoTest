using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class CostRepository : BaseRepository<Cost>, ICostRepository
    {
        public CostRepository(DbContext context) : base(context)
        {
        }
    }
}
