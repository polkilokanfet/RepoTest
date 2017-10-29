using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class AdditionalSalesUnitsRepository : BaseRepository<AdditionalSalesUnits>, IAdditionalSalesUnitsRepository
    {
        public AdditionalSalesUnitsRepository(DbContext context) : base(context)
        {
        }
    }
}
