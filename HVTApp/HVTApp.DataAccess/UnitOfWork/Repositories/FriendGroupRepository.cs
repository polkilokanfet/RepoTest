using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class FriendGroupRepository : BaseRepository<TestFriendGroup>, IFriendGroupRepository
    {
        public FriendGroupRepository(DbContext context) : base(context)
        {
        }
    }
}