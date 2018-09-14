namespace HVTApp.Infrastructure.Services
{
    public interface IMessageService
    {
        void ShowOkMessageDialog(string title, string message);
        MessageDialogResult ShowYesNoMessageDialog(string title, string message);
    }
}
