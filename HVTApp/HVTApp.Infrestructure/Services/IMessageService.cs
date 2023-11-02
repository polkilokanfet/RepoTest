namespace HVTApp.Infrastructure.Services
{
    public interface IMessageService
    {
        void Message(string title, string message);
        bool ConfirmationDialog(string title, string message, bool defaultYes = false, bool defaultNo = false);
        bool ConfirmationDialog(string message, bool defaultYes = false, bool defaultNo = false);
    }
}
