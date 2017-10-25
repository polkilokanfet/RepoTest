using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HVTApp.DataAccess.Lookup
{
    public class ProjectLookupDataService : LookupDataService<ProjectLookup>, IProjectLookupDataService
    {
        public ProjectLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }

        public override async Task<IEnumerable<ProjectLookup>> GetAllLookupsAsync()
        {
            using (var ctx = ContextCreator())
            {
                return await ctx.Projects.AsNoTracking().Select(x => new ProjectLookup()
                {
                    Id = x.Id,
                    DisplayMember = x.Name
                }).ToListAsync();
            }
        }
    }
}