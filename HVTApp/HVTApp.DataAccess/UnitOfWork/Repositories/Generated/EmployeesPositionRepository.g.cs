using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class EmployeesPositionRepository : BaseRepository<EmployeesPosition>, IEmployeesPositionRepository
    {
        public EmployeesPositionRepository(DbContext context) : base(context)
        {
        }
    }
}
