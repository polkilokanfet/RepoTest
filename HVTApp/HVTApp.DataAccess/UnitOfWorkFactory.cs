using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IUnityContainer _container;

        public UnitOfWorkFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IUnitOfWork GetUnitOfWork()
        {
            return _container.Resolve<IUnitOfWork>();
        }
    }
}