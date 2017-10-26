using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public abstract class LookupDataService<TLookup, TEntity> : ILookupDataService<TLookup> 
        where TEntity : class, IBaseEntity
        where TLookup : class, ILookupItem, new()
    {
        protected readonly Func<HvtAppContext> ContextCreator;

        protected LookupDataService(Func<HvtAppContext> contextCreator)
        {
            ContextCreator = contextCreator;
        }

        public virtual async Task<IEnumerable<TLookup>> GetAllLookupsAsync()
        {
            using (var ctx = ContextCreator())
            {
                var entities = await ctx.Set<TEntity>().AsNoTracking().ToListAsync();
                var lookups = new List<TLookup>();
                foreach (var entity in entities)
                {
                    lookups.Add(new TLookup {Id = entity.Id, DisplayMember = entity.ToString()});
                }
                return lookups;
            }
        }
    }
}
