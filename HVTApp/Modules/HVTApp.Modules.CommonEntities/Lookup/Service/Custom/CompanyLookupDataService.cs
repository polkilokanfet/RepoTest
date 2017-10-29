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
        public override async Task<IEnumerable<CompanyLookup>> GetAllLookupsAsync()
        {
            var lookups = new List<CompanyLookup>();
            var companies =
                await Context.CompanyDbSet.AsNoTracking().Include(x => x.ParentCompany).Include(x => x.Form).ToListAsync();
            foreach (var company in companies)
            {
                var lookup = new CompanyLookup
                {
                    Id = company.Id,
                    DisplayMember = GenerateDisplayMember(company),
                    CompanyForm = new CompanyFormLookup {Id = company.Form.Id, DisplayMember = company.Form.FullName}
                };
                if (company.ParentCompany != null)
                    lookup.ParentCompany = new CompanyLookup
                    {
                        Id = company.ParentCompany.Id,
                        DisplayMember = GenerateDisplayMember(company.ParentCompany)
                    };

                lookups.Add(lookup);
            }
            return lookups;
        }
    }
}