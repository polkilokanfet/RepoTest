using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.DataAccess.Lookup
{
    public interface ILookupDataService<T>
    {
        Task<IEnumerable<T>> GetAllLookupsAsync();
    }
}