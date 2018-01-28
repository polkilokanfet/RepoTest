using System.Threading.Tasks;

namespace HVTApp.Infrastructure.Interfaces.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticationAsync();
    }
}
