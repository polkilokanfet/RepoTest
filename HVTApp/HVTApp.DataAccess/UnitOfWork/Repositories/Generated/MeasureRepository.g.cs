using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class MeasureRepository : BaseRepository<Measure>, IMeasureRepository
    {
        public MeasureRepository(DbContext context) : base(context)
        {
        }
    }
}
