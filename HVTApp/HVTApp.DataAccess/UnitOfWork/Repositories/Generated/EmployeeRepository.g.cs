using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context)
        {
        }
    }
}
