using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(DbContext context) : base(context)
        {
        }
    }
}