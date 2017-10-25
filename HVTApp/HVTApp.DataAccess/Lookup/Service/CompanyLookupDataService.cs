using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HVTApp.DataAccess.Lookup
{
    public class CompanyLookupDataService : LookupDataService<CompanyLookup>, ICompanyLookupDataService
    {
        public CompanyLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }

        public override async Task<IEnumerable<CompanyLookup>> GetAllLookupsAsync()
        {
            using (var ctx = ContextCreator())
            {
                var companies = await ctx.Companies.AsNoTracking().Include(x => x.ParentCompany).ToListAsync();
                var lookups = companies.Select(x => new CompanyLookup { Id = x.Id, DisplayMember = x.FullName }).ToList();
                foreach (var companyLookup in lookups)
                {
                    var childCompanies = companies.Where(x => x.ParentCompany != null && x.ParentCompany.Id == companyLookup.Id).ToList();
                    companyLookup.ChildCompanies = childCompanies.Select(x => lookups.Single(l => l.Id == x.Id));
                    companyLookup.ChildCompanies.ToList().ForEach(x => x.ParentCompany = companyLookup);
                }
                return lookups;
            }
        }
    }
}