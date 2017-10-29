using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class FacilityTypeRepository : BaseRepository<FacilityType>, IFacilityTypeRepository
    {
        public FacilityTypeRepository(DbContext context) : base(context)
        {
        }
    }
}
