using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HVTApp.DataAccess.Lookup
{
    public class LookupDataService : IProjectLookupDataService, ICompanyLookupDataService
    {
        private readonly Func<HvtAppContext> _contextCreator;

        public LookupDataService(Func<HvtAppContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<ProjectLookup>> GetProjectLookupsAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Projects.AsNoTracking().Select(x => new ProjectLookup()
                {
                    Id = x.Id,
                    DisplayMember = x.Name
                }).ToListAsync();
            }
        }

        public async Task<IEnumerable<CompanyLookup>> GetCompanyLookupsAsync()
        {
            using (var ctx = _contextCreator())
            {
                var companies = await ctx.Companies.AsNoTracking().Include(x => x.ParentCompany).ToListAsync();
                var lookups = companies.Select(x => new CompanyLookup() {Id = x.Id, DisplayMember = x.FullName});
                foreach (var companyLookup in lookups)
                {
                    var childCompanies = companies.Where(x => x.ParentCompany.Id == companyLookup.Id);
                    companyLookup.ChildCompanies = childCompanies.Select(x => lookups.Single(l => l.Id == x.Id));
                }
                return lookups;
            }
        }
    }
}
