using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TenderUnitRepository : BaseRepository<TenderUnit>, ITenderUnitRepository
    {
        public TenderUnitRepository(DbContext context) : base(context)
        {
        }
    }
}
