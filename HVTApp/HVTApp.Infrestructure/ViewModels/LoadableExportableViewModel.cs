using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Infrastructure.ViewModels
{
    public abstract class LoadableExportableViewModel : ViewModelBaseCanExportToExcelSaveCustomization
    {
        /// <summary>
        /// ���� ���������� � ������ ������ �������� ������
        /// </summary>
        private bool _loadingInProcess = false;
        private bool _isLoaded = false;

        public bool IsLoaded
        {
            get => _isLoaded;
            protected set
            {
                _isLoaded = value;
                RaisePropertyChanged();

                if (_isLoaded)
                {
                    LoadComplited?.Invoke();
                }
            }
        }

        /// <summary>
        /// �������� ���������
        /// </summary>
        public event Action LoadComplited;

        public ICommand ReloadCommand { get; }

        protected LoadableExportableViewModel(IUnityContainer container, bool loadDataInCtor = true) : base(container)
        {
            ReloadCommand = new DelegateCommand(ReloadCommand_Execute, ReloadCommand_CanExecute);

            if (loadDataInCtor)
            {
                Load();
            }
        }

        private bool ReloadCommand_CanExecute()
        {
            return _loadingInProcess == false;
        }

        protected virtual void ReloadCommand_Execute()
        {
            Load();
        }

        private void Load()
        {
            _loadingInProcess = true;
            ((DelegateCommand)ReloadCommand).RaiseCanExecuteChanged();

            IsLoaded = false;
            BeforeGetData();
            Task.Run(() => { GetData(); })
                .Await(
                    () =>
                    {
                        AfterGetData();
                        IsLoaded = true;
                        _loadingInProcess = false;
                        ((DelegateCommand)ReloadCommand).RaiseCanExecuteChanged();
                    }, 
                    ErrorCallback);
        }

        protected virtual void ErrorCallback(Exception exception)
        {
            Container.Resolve<IMessageService>().Message("Exception", exception.PrintAllExceptions());
        }

        /// <summary>
        /// ��������� ���� ����������� ������.
        /// </summary>
        protected abstract void GetData();

        /// <summary>
        /// �������� �� ��������� ���� ����������� ������.
        /// </summary>
        protected virtual void BeforeGetData()
        {
        }

        /// <summary>
        /// �������� ����� ��������� ���� ����������� ������.
        /// </summary>
        protected abstract void AfterGetData();
    }
}