using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TestEntityRepository : BaseRepository<TestEntity>, ITestEntityRepository
    {
        public TestEntityRepository(DbContext context) : base(context)
        {
        }
    }
}
