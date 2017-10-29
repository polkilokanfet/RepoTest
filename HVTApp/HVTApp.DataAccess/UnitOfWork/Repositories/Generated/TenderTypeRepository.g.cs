using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TenderTypeRepository : BaseRepository<TenderType>, ITenderTypeRepository
    {
        public TenderTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
