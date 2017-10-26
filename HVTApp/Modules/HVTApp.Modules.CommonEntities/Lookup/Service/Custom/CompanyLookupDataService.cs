using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CompanyLookupDataService
    {
        public CompanyLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }

        public override async Task<IEnumerable<CompanyLookup>> GetAllLookupsAsync()
        {
            var lookups = new List<CompanyLookup>();
            using (var ctx = ContextCreator())
            {
                var companies = await ctx.Companies.AsNoTracking().Include(x => x.ParentCompany).Include(x => x.Form).ToListAsync();
                foreach (var company in companies)
                {
                    var lookup = new CompanyLookup()
                    {
                        Id = company.Id,
                        DisplayMember = GenerateDisplayMember(company),
                        CompanyForm = new CompanyFormLookup() { Id = company.Form.Id, DisplayMember = company.Form.FullName }
                    };
                    lookups.Add(lookup);
                }
                return lookups;
            }
        }
    }
}