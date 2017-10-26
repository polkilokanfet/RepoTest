using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;


namespace HVTApp.DataAccess
{
    public class ProductionUnitsRepository : BaseRepository<ProductionUnit>, IProductionUnitsRepository
    {
        public ProductionUnitsRepository(DbContext context) : base(context)
        {
        }
    }
}