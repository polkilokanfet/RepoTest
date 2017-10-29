using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class LocalityTypeRepository : BaseRepository<LocalityType>, ILocalityTypeRepository
    {
        public LocalityTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
