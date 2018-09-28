using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TenderRepository
    {
        protected override IQueryable<Tender> GetQuary()
        {
            return Context.Set<Tender>().Include(x => x.Participants).Include(x => x.Winner).Include(x => x.Types);
        }
    }
}