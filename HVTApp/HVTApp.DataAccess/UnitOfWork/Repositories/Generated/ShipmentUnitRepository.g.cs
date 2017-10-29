using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ShipmentUnitRepository : BaseRepository<ShipmentUnit>, IShipmentUnitRepository
    {
        public ShipmentUnitRepository(DbContext context) : base(context)
        {
        }
    }
}
