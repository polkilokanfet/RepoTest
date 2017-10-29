using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TestHusbandRepository : BaseRepository<TestHusband>, ITestHusbandRepository
    {
        public TestHusbandRepository(DbContext context) : base(context)
        {
        }
    }
}
