using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TestWifeRepository : BaseRepository<TestWife>, ITestWifeRepository
    {
        public TestWifeRepository(DbContext context) : base(context)
        {
        }
    }
}
