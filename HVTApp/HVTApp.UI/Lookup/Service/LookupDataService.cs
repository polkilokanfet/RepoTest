using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public abstract class LookupDataService<TLookup, TEntity> : ILookupDataService<TLookup> 
        where TEntity : class, IBaseEntity
        where TLookup : class, ILookupItem
    {
        protected readonly HvtAppContext Context;

        protected LookupDataService(HvtAppContext context)
        {
            Context = context;
        }

        private static TLookup GetLookup(TEntity entity)
        {
            return (TLookup)Activator.CreateInstance(typeof(TLookup), entity);
        }

        public async Task<TLookup> GetLookupById(Guid id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            var lookup = GetLookup(entity);
            lookup.DisplayMember = GenerateDisplayMember(entity);
            return lookup;
        }

        public virtual async Task<IEnumerable<TLookup>> GetAllLookupsAsync()
        {
            var entities = await Context.Set<TEntity>().AsNoTracking().ToListAsync();
            var lookups = new List<TLookup>();
            foreach (var entity in entities)
            {
                var lookup = GetLookup(entity);
                lookup.DisplayMember = GenerateDisplayMember(entity);
                lookups.Add(lookup);
            }
            return lookups;
        }

        public virtual string GenerateDisplayMember(TEntity entity)
        {
            return entity.ToString();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
