using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;


namespace HVTApp.DataAccess
{
    public partial class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IUnityContainer _container;

        public UnitOfWork(DbContext context, IUnityContainer container)
        {
            _context = context;
            _container = container;
            InitializeRepositories();
        }

        public IRepository<T> Repository<T>() where T : class, IBaseEntity
        {
            var repositoryFieldInfo = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                                    .Single(x => typeof(IRepository<T>).IsAssignableFrom(x.FieldType));
            return (IRepository<T>) repositoryFieldInfo.GetValue(this);
        }

        public async Task<int> SaveChangesAsync()
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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
