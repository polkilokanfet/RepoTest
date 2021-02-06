using System;
using Microsoft.Practices.Unity;

namespace HVTApp.Infrastructure
{
    public interface IModelsStore
    {
        IUnitOfWork UnitOfWork { get; }
        void Refresh();
        event Action IsRefreshed;
    }

    public class ModelsStore : IModelsStore
    {
        private readonly IUnityContainer _container;

        public IUnitOfWork UnitOfWork { get; private set; }

        public ModelsStore(IUnityContainer container)
        {
            _container = container;
            UnitOfWork = _container.Resolve<IUnitOfWork>();
        }

        public void Refresh()
        {
            UnitOfWork = _container.Resolve<IUnitOfWork>();
            IsRefreshed?.Invoke();
        }

        public event Action IsRefreshed;
    }
}