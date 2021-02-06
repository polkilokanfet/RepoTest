using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        //public virtual async Task<List<TEntity>> GetAllAsync()
        //{
        //    Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        //    return await GetQuary().ToListAsync();
        //}

        public virtual List<TEntity> GetAll()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuary().ToList();
        }

        //public virtual async Task<List<TEntity>> GetAllAsNoTracking()
        //{
        //    Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        //    return await GetQuary().AsNoTracking().ToListAsync();
        //}

        public virtual List<TEntity> GetAllAsNoTracking()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuary().AsNoTracking().ToList();
        }

        public virtual List<TEntity> Find(Func<TEntity, bool> predicate)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuary().AsEnumerable().Where(predicate).ToList();
        }

        public List<TEntity> FindAsNoTracking(Func<TEntity, bool> predicate)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuary().AsNoTracking().AsEnumerable().Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Context.Set<TEntity>().AddRange(entities);
        }


        public void Delete(TEntity entity)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity GetById(Guid id)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuary().SingleOrDefault(entity => entity.Id == id);
        }

        //public virtual async Task<TEntity> GetByIdAsync(Guid id)
        //{
        //    Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        //    return await GetQuary().SingleOrDefaultAsync(x => x.Id == id);
        //}

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


        protected void Loging(string methodName)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Метод {methodName} из репозитория {this.GetType().Name}");
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
        }

    }
}