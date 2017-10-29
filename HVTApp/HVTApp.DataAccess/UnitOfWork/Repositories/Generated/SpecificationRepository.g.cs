using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class SpecificationRepository : BaseRepository<Specification>, ISpecificationRepository
    {
        public SpecificationRepository(DbContext context) : base(context)
        {
        }
    }
}
