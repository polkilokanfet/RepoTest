using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;


namespace HVTApp.DataAccess
{
    public partial class UnitOfWork : IUnitOfWork
    {
        public IRepository<T> GetRepository<T>() where T : class, IBaseEntity
        {
            var repositoryPropertyInfo = this.GetType().GetProperties().Single(x => typeof(IRepository<T>).IsAssignableFrom(x.PropertyType));
            return (IRepository<T>) repositoryPropertyInfo.GetValue(this);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
