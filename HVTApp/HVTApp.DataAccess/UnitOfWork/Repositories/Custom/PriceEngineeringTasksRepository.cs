using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTasksRepository
    {
        protected override IQueryable<PriceEngineeringTasks> GetQuery()
        {
            return Context.Set<PriceEngineeringTasks>().AsQueryable()
                .Include(x => x.ChildPriceEngineeringTasks)
                .Include(x => x.UserManager);
        }
    }
}