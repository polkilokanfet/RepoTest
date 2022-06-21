using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TechnicalRequrementsTaskRepository
    {
        protected override IQueryable<TechnicalRequrementsTask> GetQuery()
        {
            return Context.Set<TechnicalRequrementsTask>().AsQueryable()
                .Include(x => x.Requrements);
        }
    }
}