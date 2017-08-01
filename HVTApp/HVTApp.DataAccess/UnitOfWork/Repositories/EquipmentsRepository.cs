using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class EquipmentsRepository : BaseRepository<Equipment, EquipmentWrapper>, IEquipmentsRepository
    {
        public EquipmentsRepository(DbContext context, IGetWrapper getWrapper) : base(context, getWrapper)
        {
        }
    }
}