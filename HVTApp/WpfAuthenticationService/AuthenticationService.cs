using System.Linq;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Model;

namespace WpfAuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public User User { get; private set; }
        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Authentication()
        {
            User = _unitOfWork.UsersRepository.GetAll().First();
            return false;
        }
    }
}
