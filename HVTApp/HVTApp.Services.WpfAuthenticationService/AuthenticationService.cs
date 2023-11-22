using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.WpfAuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;

        public AuthenticationService(IUnitOfWork unitOfWork, IDialogService dialogService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;

            _dialogService.Register<AuthenticationWindowModel, AuthenticationWindow>();
        }

        public User GetAuthenticationUser()
        {
            var users = _unitOfWork.Repository<User>().GetAll();
            var authenticationWindowModel = new AuthenticationWindowModel(users);
            bool? result = _dialogService.ShowDialog(authenticationWindowModel);
            if (result.HasValue && result.Value)
            {
                return authenticationWindowModel.User;
            }

            return null;
        }
    }
}
