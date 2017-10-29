using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TestFriendRepository : BaseRepository<TestFriend>, ITestFriendRepository
    {
        public TestFriendRepository(DbContext context) : base(context)
        {
        }
    }
}
