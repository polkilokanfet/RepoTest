namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface IEmailService
    {
        void SendMail(string to, string subject, string body);
    }
}