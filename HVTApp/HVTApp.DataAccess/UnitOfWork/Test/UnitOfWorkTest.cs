using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess
{
    public partial class UnitOfWorkTest : IUnitOfWork
    {
        public void Dispose()
        {
        }

        public int Complete()
        {
            return 0;
        }

        public IRepository<T> GetRepository<T>() where T : class, IBaseEntity
        {
            var repositoryPropertyInfo = this.GetType().GetProperties().Single(x => typeof(IRepository<T>).IsAssignableFrom(x.PropertyType));
            return (IRepository<T>)repositoryPropertyInfo.GetValue(this);
        }

    }
}