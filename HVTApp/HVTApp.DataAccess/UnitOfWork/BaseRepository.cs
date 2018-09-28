using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
            return await GetQuary().ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsNoTrackingAsync()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return await GetQuary().AsNoTracking().ToListAsync();
        }

        public virtual List<TEntity> Find(Func<TEntity, bool> predicate)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return GetQuary().Where(predicate).ToList();
        }


        public void Add(TEntity entity)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Context.Set<TEntity>().AddRange(entities);
        }


        public void Delete(TEntity entity)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            return await GetQuary().SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Reload(TEntity entity)
        {
            //if (Context.Entry(entity).State == EntityState.Detached)
            //    //if (Context.Set<TEntity>().Local.All(x => x.Id != entity.Id))
            //    Context.Set<TEntity>().Attach(entity);
            //var entry = Context.Entry(entity);
            //entry.Reload();
        }

        protected virtual IQueryable<TEntity> GetQuary()
        {
            return Context.Set<TEntity>().AsQueryable();
        }
    }
}