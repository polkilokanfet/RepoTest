using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.UI.Lookup
{
    public interface ILookupDataService<TLookup> 
        where TLookup : class, ILookupItem, new()
    {
        Task<IEnumerable<TLookup>> GetAllLookupsAsync();
    }
}