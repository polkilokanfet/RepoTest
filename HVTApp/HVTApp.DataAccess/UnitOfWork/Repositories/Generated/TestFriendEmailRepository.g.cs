using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TestFriendEmailRepository : BaseRepository<TestFriendEmail>, ITestFriendEmailRepository
    {
        public TestFriendEmailRepository(DbContext context) : base(context)
        {
        }
    }
}
