using System.Data.Entity;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public class FriendGroupRepository : BaseRepository<TestFriendGroup, TestFriendGroupWrapper>, IFriendGroupRepository
    {
        public FriendGroupRepository(DbContext context) : base(context)
        {
        }
    }
}