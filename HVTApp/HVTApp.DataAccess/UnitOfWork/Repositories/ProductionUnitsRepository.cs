using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ProductionUnitsRepository : BaseRepository<ProductionUnit, ProductionUnitWrapper>, IProductionUnitsRepository
    {
        public ProductionUnitsRepository(DbContext context, IGetWrapper wrappersGetter) : base(context, wrappersGetter)
        {
        }
    }
}