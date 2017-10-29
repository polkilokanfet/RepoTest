using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TestFriendGroupRepository : BaseRepository<TestFriendGroup>, ITestFriendGroupRepository
    {
        public TestFriendGroupRepository(DbContext context) : base(context)
        {
        }
    }
}
