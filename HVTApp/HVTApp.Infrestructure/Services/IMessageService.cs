namespace HVTApp.Infrastructure.Services
{
    public interface IMessageService
    {
        void ShowOkMessageDialog(string title, string message);
        MessageDialogResult ShowYesNoMessageDialog(string title, string message, bool defaultYes = false, bool defaultNo = false);
        MessageDialogResult ShowYesNoMessageDialog(string message, bool defaultYes = false, bool defaultNo = false);
    }
}
