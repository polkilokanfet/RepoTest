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

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public IRepository<T> GetRepository<T>() where T : class, IBaseEntity
        {
            var repositoryPropertyInfo = this.GetType().GetProperties().Single(x => typeof(IRepository<T>).IsAssignableFrom(x.PropertyType));
            return (IRepository<T>)repositoryPropertyInfo.GetValue(this);
        }

    }
}