using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Infrastructure.ViewModels
{
    public abstract class LoadableExportableViewModel : ViewModelBaseCanExportToExcelSaveCustomization
    {
        private bool _isLoaded = false;

        public bool IsLoaded
        {
            get { return _isLoaded; }
            private set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        public ICommand ReloadCommand { get; }

        protected LoadableExportableViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            Load();
        }

        private void Load()
        {
            IsLoaded = false;
            Task.Run(() => { GetData(); })
                .Await(
                    () =>
                    {
                        AfterGetData();
                        IsLoaded = true;
                    }, 
                    ErrorCallback);
        }

        protected virtual void ErrorCallback(Exception exception)
        {
            Container.Resolve<IMessageService>().ShowOkMessageDialog("Exception", exception.GetAllExceptions());
        }

        /// <summary>
        /// Получение всех необходимых данных.
        /// </summary>
        protected abstract void GetData();

        /// <summary>
        /// Действия после получения всех необходимых данных.
        /// </summary>
        protected abstract void AfterGetData();
    }
}