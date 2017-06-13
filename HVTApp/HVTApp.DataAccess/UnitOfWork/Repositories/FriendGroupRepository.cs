using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class FriendGroupRepository : BaseRepository<TestFriendGroup, TestFriendGroupWrapper>, IFriendGroupRepository
    {
        public FriendGroupRepository(DbContext context, Dictionary<IBaseEntity, IWrapper<IBaseEntity>> wrappersRepository) : base(context, wrappersRepository)
        {
        }
    }
}