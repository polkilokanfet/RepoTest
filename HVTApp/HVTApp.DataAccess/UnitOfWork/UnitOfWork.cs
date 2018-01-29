using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HVTApp.Infrastructure;


namespace HVTApp.DataAccess
{
    public partial class UnitOfWork : IUnitOfWork
    {
        public IRepository<T> GetRepository<T>() where T : class, IBaseEntity
        {
            var repositoryFieldInfo = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                                    .Single(x => typeof(IRepository<T>).IsAssignableFrom(x.FieldType));
            return (IRepository<T>) repositoryFieldInfo.GetValue(this);
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
