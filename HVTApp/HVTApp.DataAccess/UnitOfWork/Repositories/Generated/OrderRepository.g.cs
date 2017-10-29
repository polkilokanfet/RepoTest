using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
    }
}
