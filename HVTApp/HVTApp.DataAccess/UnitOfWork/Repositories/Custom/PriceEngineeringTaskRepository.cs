using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTaskRepository
    {
        protected override IQueryable<PriceEngineeringTask> GetQuery()
        {
            return Context.Set<PriceEngineeringTask>().AsQueryable()
                .Include(x => x.Statuses)
                .Include(x => x.ChildPriceEngineeringTasks)
                .Include(x => x.UserConstructor);
        }
    }
}