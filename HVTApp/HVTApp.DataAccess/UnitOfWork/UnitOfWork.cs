using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
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

        //public async Task<int> SaveChangesAsync()
        //{
        //    return await _context.SaveChangesAsync();
        //}

        public void SaveChanges()
        {
#if DEBUG
            _context.SaveChanges();
#else
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var text = e.GetAllExceptions() + string.Join(Environment.NewLine, e.EntityValidationErrors.Select(x => x.ToString()));
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Обратитесь к разработчику", text);
                throw;
            }
            catch (Exception e)
            {
                var text = e.GetAllExceptions();
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Обратитесь к разработчику", text);
                throw;
            }

#endif
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
