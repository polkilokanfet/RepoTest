using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;

namespace HVTApp.DataAccess
{
    public class OffersRepository : BaseRepository<Offer>, IOffersRepository
    {
        public OffersRepository(DbContext context) : base(context)
        {
        }
    }
}