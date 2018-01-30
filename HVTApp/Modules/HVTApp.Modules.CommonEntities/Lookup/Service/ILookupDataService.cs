using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.UI.Lookup
{
    public interface ILookupDataService<TLookup> : IDisposable
        where TLookup : class, ILookupItem
    {
        Task<TLookup> GetLookupById(Guid id);
        Task<IEnumerable<TLookup>> GetAllLookupsAsync();
    }
}