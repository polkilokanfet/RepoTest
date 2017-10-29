using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class SalesUnitRepository : BaseRepository<SalesUnit>, ISalesUnitRepository
    {
        public SalesUnitRepository(DbContext context) : base(context)
        {
        }
    }
}
