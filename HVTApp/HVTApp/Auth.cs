using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Services.DialogService;
using HVTApp.Services.WpfAuthenticationService;
using Microsoft.Practices.Unity;

namespace HVTApp
{
    public class Auth
    {
        private readonly IUnityContainer _container;

        public Auth()
        {
            _container = new UnityContainer();
            _container.RegisterType<IAuthenticationService, AuthenticationService>();
            _container.RegisterType<IUnitOfWork, UnitOfWork>();
            _container.RegisterType<IDialogService, DialogService>();
            _container.RegisterType<DbContext, HvtAppContext>();
        }

        public async Task<User> GetCurrentUser()
        {
            var aus = (AuthenticationService)_container.Resolve<IAuthenticationService>();
            if (await aus.AuthenticationAsync())
                return aus.User;

            Application.Current.Shutdown();
            return null;
        }
    }
}