using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TestChildRepository : BaseRepository<TestChild>, ITestChildRepository
    {
        public TestChildRepository(DbContext context) : base(context)
        {
        }
    }
}
