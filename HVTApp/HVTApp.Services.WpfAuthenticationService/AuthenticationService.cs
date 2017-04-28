using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;

namespace HVTApp.Services.WpfAuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;

        public User User { get; private set; }
        public AuthenticationService(IUnitOfWork unitOfWork, IDialogService dialogService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;

            _dialogService.Register<AuthenticationWindowModel, AuthenticationWindow>();
        }

        public bool Authentication()
        {
            AuthenticationWindowModel authenticationWindowModel = new AuthenticationWindowModel(_unitOfWork.Users.GetAll().Select(x => x.Model).ToList());
            bool? result = _dialogService.ShowDialog(authenticationWindowModel);
            if (result.HasValue && result.Value)
            {
                User = authenticationWindowModel.User;
                return true;
            }

            return false;
        }
    }
}
