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
            return GetQuery().ToList();
        }

        //public virtual async Task<List<TEntity>> GetAllAsNoTracking()
        //{
        //    Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        //    return await GetQuary().AsNoTracking().ToListAsync();
        //}

        public virtual List<TEntity> GetAllAsNoTracking()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuery().AsNoTracking().ToList();
        }

        public virtual List<TEntity> Find(Func<TEntity, bool> predicate)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuery().AsEnumerable().Where(predicate).ToList();
        }

        public List<TEntity> FindAsNoTracking(Func<TEntity, bool> predicate)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuery().AsNoTracking().AsEnumerable().Where(predicate).ToList();
        }

        private UnitOfWorkOperationResult VoidAction<T>(Action<T> action, T entity)
        {
            UnitOfWorkOperationResult result;
            try
            {
                action.Invoke(entity);
                result = new UnitOfWorkOperationResult();
            }
            catch (Exception e)
            {
                result = new UnitOfWorkOperationResult(e);
                this.OperationFailedEvent?.Invoke(result);
#if DEBUG
                //throw;
#endif
            }

            return result;
        }

        public virtual UnitOfWorkOperationResult Add(TEntity entity)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return VoidAction(baseEntity => Context.Set<TEntity>().Add(baseEntity), entity);
        }

        public virtual UnitOfWorkOperationResult AddRange(IEnumerable<TEntity> entities)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return VoidAction(ee => Context.Set<TEntity>().AddRange(ee), entities);
        }


        public virtual UnitOfWorkOperationResult Delete(TEntity entity)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return VoidAction(en => Context.Set<TEntity>().Remove(en), entity);
        }

        public virtual UnitOfWorkOperationResult DeleteRange(IEnumerable<TEntity> entities)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return VoidAction(ee => Context.Set<TEntity>().RemoveRange(ee), entities);
        }

        public TEntity GetById(Guid id)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return GetQuery().SingleOrDefault(entity => entity.Id == id);
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

        protected virtual IQueryable<TEntity> GetQuery()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public event Action<UnitOfWorkOperationResult> OperationFailedEvent; 

        protected void Loging(string methodName)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Метод {methodName} из репозитория {this.GetType().Name}");
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
        }

    }
}