using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookupDataService
    {
        public override async Task<IEnumerable<ProjectLookup>> GetAllLookupsAsync()
        {
            var projects = await Context.Set<Project>().AsNoTracking().ToListAsync();
            var salesUnits = await Context.Set<SalesUnit>().AsNoTracking().ToListAsync();
            var tenders = await Context.Set<Tender>().AsNoTracking().ToListAsync();
            var offers = await Context.Set<Offer>().AsNoTracking().ToListAsync();

            return projects.Select(x => new ProjectLookup(x, salesUnits.Where(su => Equals(su.Project, x)),
                tenders.Where(t => Equals(t.Project, x)), offers.Where(o => Equals(o.Project, x))));

        }
    }
}