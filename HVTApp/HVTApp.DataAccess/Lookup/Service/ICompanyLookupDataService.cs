using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.DataAccess.Lookup
{
    public interface ICompanyLookupDataService
    {
        Task<IEnumerable<CompanyLookup>> GetCompanyLookupsAsync();
    }
}