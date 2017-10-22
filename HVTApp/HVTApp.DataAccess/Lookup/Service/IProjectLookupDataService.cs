using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.DataAccess.Lookup
{
    public interface IProjectLookupDataService
    {
        Task<IEnumerable<ProjectLookup>> GetProjectLookupsAsync();
    }
}