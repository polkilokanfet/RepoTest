using System.Data.Entity;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class FriendGroupRepository : BaseRepository<FriendGroupTest>, IFriendGroupRepository
    {
        public FriendGroupRepository(DbContext context) : base(context)
        {
        }
    }
}