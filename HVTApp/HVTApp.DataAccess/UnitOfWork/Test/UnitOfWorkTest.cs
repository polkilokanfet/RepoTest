using System;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public partial class UnitOfWorkTest : IUnitOfWork
    {
        public void Dispose()
        {
        }

        public IRepository<T> Repository<T>() where T : class, IBaseEntity
        {
            var repositoryPropertyInfo = this.GetType().GetProperties().Single(x => typeof(IRepository<T>).IsAssignableFrom(x.PropertyType));
            return (IRepository<T>)repositoryPropertyInfo.GetValue(this);
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public UnitOfWorkOperationResult SaveChanges()
        {
            throw new NotImplementedException();
        }

        public UnitOfWorkOperationResult SaveEntity<T>(T entity) where T : class, IBaseEntity
        {
            throw new NotImplementedException();
        }

        public UnitOfWorkOperationResult RemoveEntity<T>(T entity) where T : class, IBaseEntity
        {
            throw new NotImplementedException();
        }
    }
}