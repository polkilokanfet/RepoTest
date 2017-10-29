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
        protected readonly HvtAppContext Context;

        protected LookupDataService(HvtAppContext context)
        {
            Context = context;
        }

        public async Task<TLookup> GetLookupById(Guid id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            return new TLookup
            {
                Id = entity.Id,
                DisplayMember = GenerateDisplayMember(entity)
            };
        }

        public virtual async Task<IEnumerable<TLookup>> GetAllLookupsAsync()
        {
            var entities = await Context.Set<TEntity>().AsNoTracking().ToListAsync();
            var lookups = new List<TLookup>();
            foreach (var entity in entities)
            {
                lookups.Add(new TLookup
                {
                    Id = entity.Id,
                    DisplayMember = GenerateDisplayMember(entity)
                });
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
