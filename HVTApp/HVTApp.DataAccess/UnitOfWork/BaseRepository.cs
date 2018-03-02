using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected readonly DbContext Context;

        public BaseRepository(DbContext context)
        {
            Context = context;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsNoTrackingAsync()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> FindAsync(Func<TEntity, bool> predicate)
        {
            return await Task.Factory.StartNew<List<TEntity>>(() => Context.Set<TEntity>().Where(predicate).ToList());
        }


        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }


        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
    }
}