using System;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.Settings.ViewModels
{
    public class PasswordViewModel : BindableBase
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private readonly User _user;

        private string _passOld;
        private string _passNew;
        private string _passAgain;

        public ICommand OkCommand { get; set; }

        public string PassOld
        {
            get { return _passOld; }
            set
            {
                _passOld = value;
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public string PassNew
        {
            get { return _passNew; }
            set
            {
                _passNew = value;
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public string PassAgain
        {
            get { return _passAgain; }
            set
            {
                _passAgain = value;
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public PasswordViewModel(IUnityContainer container)
        {
            _container = container;
            _unitOfWork = container.Resolve<IUnitOfWork>();
            _user = _unitOfWork.Repository<User>().Find(x => x.Id == CommonOptions.User.Id).First();

            OkCommand = new DelegateCommand(OkCommandExecute, OkCommandCanExecute);
        }

        private async void OkCommandExecute()
        {
            _user.Password = StringToGuid.GetHashString(PassNew);
            await _unitOfWork.SaveChangesAsync();
            _container.Resolve<IMessageService>().ShowOkMessageDialog("Пароль изменен", "Пароль успешно изменен.");
            PassOld = String.Empty;
            PassNew = string.Empty;
            PassAgain = string.Empty;
        }

        private bool OkCommandCanExecute()
        {
            return !string.IsNullOrEmpty(PassOld) &&
                   !string.IsNullOrEmpty(PassNew) &&
                   !string.IsNullOrEmpty(PassAgain) &&
                   StringToGuid.GetHashString(PassOld) == _user.Password &&
                   PassNew == PassAgain;
        }
    }
}
