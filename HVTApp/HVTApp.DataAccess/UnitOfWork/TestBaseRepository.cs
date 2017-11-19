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

        public Task<List<TEntity>> GetAllAsync()
        {
            var task = new Task<List<TEntity>>(() => _testData.GetAll<TEntity>().ToList());
            return task;
        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            var entities = GetAllAsync().Result;
            return new Task<TEntity>(() => entities.Single(x => x.Id == id));
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
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
    }
}