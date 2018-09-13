using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;

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

        public async Task<bool> AuthenticationAsync()
        {
            var users = await _unitOfWork.Repository<User>().GetAllAsync();
            var authenticationWindowModel = new AuthenticationWindowModel(users);
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
