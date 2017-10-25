using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HVTApp.DataAccess.Lookup
{
    public abstract class LookupDataService<T> : ILookupDataService<T>
    {
        protected readonly Func<HvtAppContext> ContextCreator;

        protected LookupDataService(Func<HvtAppContext> contextCreator)
        {
            ContextCreator = contextCreator;
        }

        public abstract Task<IEnumerable<T>> GetAllLookupsAsync();
    }
}
