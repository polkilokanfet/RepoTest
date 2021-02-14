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
            get => _isLoaded;
            protected set
            {
                _isLoaded = value;
                OnPropertyChanged();

                if (_isLoaded)
                {
                    LoadComplited?.Invoke();
                }
            }
        }

        /// <summary>
        /// Загрузка завершена
        /// </summary>
        public event Action LoadComplited;

        public ICommand ReloadCommand { get; }

        protected LoadableExportableViewModel(IUnityContainer container, bool loadDataInCtor = true) : base(container)
        {
            ReloadCommand = new DelegateCommand(ReloadCommand_Execute);
            if (loadDataInCtor)
            {
                Load();
            }
        }

        protected virtual void ReloadCommand_Execute()
        {
            Load();
        }

        private void Load()
        {
            IsLoaded = false;
            BeforeGetData();
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
        /// Действия до получения всех необходимых данных.
        /// </summary>
        protected virtual void BeforeGetData()
        {
        }

        /// <summary>
        /// Действия после получения всех необходимых данных.
        /// </summary>
        protected abstract void AfterGetData();
    }
}