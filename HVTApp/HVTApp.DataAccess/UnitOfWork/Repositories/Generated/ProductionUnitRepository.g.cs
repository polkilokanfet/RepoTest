using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductionUnitRepository : BaseRepository<ProductionUnit>, IProductionUnitRepository
    {
        public ProductionUnitRepository(DbContext context) : base(context)
        {
        }
    }
}
