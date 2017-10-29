using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class CostOnDateRepository : BaseRepository<CostOnDate>, ICostOnDateRepository
    {
        public CostOnDateRepository(DbContext context) : base(context)
        {
        }
    }
}
