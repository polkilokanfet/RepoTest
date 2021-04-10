using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public abstract class TestBaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        private readonly TestData _testData;

        protected TestBaseRepository(TestData testData)
        {
            _testData = testData;
        }

        public TEntity[] GetAll()
        {
            return _testData.GetAll<TEntity>().ToArray();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var task = Task<List<TEntity>>.Factory.StartNew(() => _testData.GetAll<TEntity>().ToList());
            return await task;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return (await GetAllAsync()).Single(x => x.Id == id);
        }

        public TEntity[] Find(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).ToArray();
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(Guid id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public void Reload(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity[] FindAsNoTracking(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity[] GetAllAsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}