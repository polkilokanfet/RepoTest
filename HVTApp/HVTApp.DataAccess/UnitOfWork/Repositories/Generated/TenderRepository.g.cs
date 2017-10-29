using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TenderRepository : BaseRepository<Tender>, ITenderRepository
    {
        public TenderRepository(DbContext context) : base(context)
        {
        }
    }
}
