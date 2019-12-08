using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class PasswordViewModel : BindableBase
    {
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
            var unitOfWork = container.Resolve<IUnitOfWork>();
            var user = unitOfWork.Repository<User>().Find(x => x.IsAppCurrentUser()).First();

            OkCommand = new DelegateCommand(
                async () =>
                {
                    user.Password = StringToGuid.GetHashString(PassNew);
                    await unitOfWork.SaveChangesAsync();
                    container.Resolve<IMessageService>().ShowOkMessageDialog("Пароль изменен", "Пароль успешно изменен.");

                    PassOld = string.Empty;
                    PassNew = string.Empty;
                    PassAgain = string.Empty;
                },

                () => !string.IsNullOrEmpty(PassOld) &&
                      !string.IsNullOrEmpty(PassNew) &&
                      !string.IsNullOrEmpty(PassAgain) &&
                      StringToGuid.GetHashString(PassOld) == user.Password 
                      && PassNew == PassAgain);
        }

    }
}
