using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Annotations;
using Prism.Commands;

namespace HVTApp.Services.WpfAuthenticationService
{
    class AuthenticationWindowModel : IDialogRequestClose, INotifyPropertyChanged
    {
        private readonly List<User> _users;
        private string _login;
        private string _password;
        private UserRole _selectedRole;

        public AuthenticationWindowModel(List<User> users)
        {
            _users = users;

            User = null;

            Roles = new ObservableCollection<UserRole>();

            OkCommand = new DelegateCommand(OkCommand_Execute, OkCommand_CanExecute);
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                CheckUser();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                CheckUser();
            }
        }

        public UserRole SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                if (value != null && User != null)
                    User.RoleCurrent = value.Role;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserRole> Roles { get; set; }

        public User User { get; private set; }
        public ICommand OkCommand { get; }


        private bool OkCommand_CanExecute()
        {
            return User != null && User.Roles.Count > 0;
        }

        private void OkCommand_Execute()
        {
            CurrentUser.User = User;
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }



        private void CheckUser()
        {
            Guid password = Guid.Empty;
            if (!String.IsNullOrEmpty(_password))
                password = StringToGuidService.StringToGuidService.GetHashString(_password);
            User = _users.FirstOrDefault(x => x.Login == Login && x.Password == password);

            Roles.Clear();
            SelectedRole = null;
            if (User != null)
            {
                foreach (var role in User.Roles)
                    Roles.Add(role);
                if (User.Roles.Count > 0)
                {
                    SelectedRole = User.Roles[0];
                    User.RoleCurrent = SelectedRole.Role;
                }
            }

            ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
