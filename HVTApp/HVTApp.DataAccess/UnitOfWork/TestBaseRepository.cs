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

        public List<TEntity> GetAll()
        {
            return _testData.GetAll<TEntity>().ToList();
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

        public List<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public UnitOfWorkOperationResult Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public UnitOfWorkOperationResult AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public UnitOfWorkOperationResult Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public UnitOfWorkOperationResult DeleteRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public event Action<UnitOfWorkOperationResult> OperationFailedEvent;

        public TEntity GetById(Guid id)
        {
            return GetAll().Single(x => x.Id == id);
        }

        public void Reload(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindAsNoTracking(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAllAsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}