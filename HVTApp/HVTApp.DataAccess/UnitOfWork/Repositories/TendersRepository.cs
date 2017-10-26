using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;


namespace HVTApp.DataAccess
{
    public class TendersRepository : BaseRepository<Tender>, ITendersRepository
    {
        public TendersRepository(DbContext context) : base(context)
        {
        }
    }
}