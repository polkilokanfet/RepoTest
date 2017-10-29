using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PartRepository : BaseRepository<Part>, IPartRepository
    {
        public PartRepository(DbContext context) : base(context)
        {
        }
    }
}
