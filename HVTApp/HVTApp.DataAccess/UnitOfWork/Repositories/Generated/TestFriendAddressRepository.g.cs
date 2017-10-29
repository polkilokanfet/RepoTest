using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TestFriendAddressRepository : BaseRepository<TestFriendAddress>, ITestFriendAddressRepository
    {
        public TestFriendAddressRepository(DbContext context) : base(context)
        {
        }
    }
}
