using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class LocalityRepository : BaseRepository<Locality>, ILocalityRepository
    {
        public LocalityRepository(DbContext context) : base(context)
        {
        }
    }
}
