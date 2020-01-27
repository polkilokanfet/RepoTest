using Microsoft.Practices.Unity;

namespace HVTApp.Infrastructure.ViewModels
{
    public abstract class LoadableBindableBaseCanExportToExcel : ViewModelBaseCanExportToExcel
    {

        private bool _isLoaded;

        protected LoadableBindableBaseCanExportToExcel(IUnityContainer container) : base(container)
        {
        }

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