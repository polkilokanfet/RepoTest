using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class OffersRepository : BaseRepository<Offer, OfferWrapper>, IOffersRepository
    {
        public OffersRepository(DbContext context) : base(context)
        {
        }
    }
}