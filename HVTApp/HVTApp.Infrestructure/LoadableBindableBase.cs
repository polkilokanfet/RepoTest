using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.Infrastructure
{
    public abstract class LoadableBindableBase : BindableBase
    {
        protected readonly IUnityContainer Container;
        protected IUnitOfWork UnitOfWork;

        protected LoadableBindableBase(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = container.Resolve<IUnitOfWork>();
        }

        private bool _isLoaded;
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                if (Equals(_isLoaded, value))
                    return;
                _isLoaded = value;
                OnPropertyChanged();
            }
        }


        public void Load()
        {
            IsLoaded = false;
            LoadedMethod();
            IsLoaded = true;
        }

        protected abstract void LoadedMethod();
    }
}
