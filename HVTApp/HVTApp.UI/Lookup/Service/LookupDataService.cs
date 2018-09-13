using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public abstract class LookupDataService<TLookup, TEntity> : ILookupDataService<TLookup> 
        where TEntity : class, IBaseEntity
        where TLookup : class, ILookupItemNavigation<TEntity>
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected LookupDataService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected async Task<TLookup> GetLookup(TEntity entity)
        {
            var lookup = (TLookup)Activator.CreateInstance(typeof(TLookup), entity);
            await lookup.LoadOther(UnitOfWork);
            return lookup;
        }

        public async Task<TLookup> GetLookupById(Guid id)
        {
            var entity = await UnitOfWork.Repository<TEntity>().GetByIdAsync(id);
            var lookup = await GetLookup(entity);
            lookup.DisplayMember = GenerateDisplayMember(entity);
            return lookup;
        }

        public virtual async Task<IEnumerable<TLookup>> GetAllLookupsAsync()
        {
            var entities = await UnitOfWork.Repository<TEntity>().GetAllAsNoTrackingAsync();
            var lookups = new List<TLookup>();
            foreach (var entity in entities)
            {
                var lookup = await GetLookup(entity);
                lookup.DisplayMember = GenerateDisplayMember(entity);
                lookups.Add(lookup);
            }
            return lookups;
        }

        public virtual string GenerateDisplayMember(TEntity entity)
        {
            return entity.ToString();
        }

        public void Delete(TLookup lookup)
        {
             UnitOfWork.Repository<TEntity>().Delete(lookup.Entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }

        public void Reload(TLookup lookup)
        {
            UnitOfWork.Repository<TEntity>().Reload(lookup.Entity);
            lookup.Refresh(lookup.Entity);
        }
    }
}
