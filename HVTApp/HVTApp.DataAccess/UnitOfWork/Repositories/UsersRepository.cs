using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public class UsersRepository : BaseRepository<User, UserWrapper>, IUsersRepository
    {
        public UsersRepository(DbContext context, Dictionary<IBaseEntity, object> repository) : base(context, repository)
        {
        }
    }
}